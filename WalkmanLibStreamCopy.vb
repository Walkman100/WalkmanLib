Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports System.ComponentModel
Imports System.IO

Partial Public Class WalkmanLib

    ' disable if NoOokii is defined (default for tests project)
#If NoOokii = 0 Then
    ''' <summary>
    ''' Asynchronously Copy a stream with a progress dialog. Uses <see cref="Ookii.Dialogs.ProgressDialog"/> for the dialog.
    ''' <br />NOTE: As this function exits when the copy process starts, the streams must NOT be closed e.g. by a <see langword="Using"/> statement.
    ''' Streams are always disposed if this function succeeds in starting the dialog.
    ''' </summary>
    ''' <param name="source">Stream to copy from. Must support Reading</param>
    ''' <param name="target">Stream to copy to. Must support Writing</param>
    ''' <param name="description">Optional description to display in the ProgressDialog.</param>
    ''' <param name="title">Optional title to use for the ProgressDialog.</param>
    Shared Sub StreamCopy(source As Stream, target As Stream, Optional description As String = " ", Optional title As String = "Copy")
        If source Is Nothing Then Throw New ArgumentNullException("source")
        If target Is Nothing Then Throw New ArgumentNullException("target")

        If Not source.CanRead OrElse Not target.CanWrite Then
            Throw New InvalidOperationException("Either Read from Source or Write to Target isn't possible!")
        ElseIf source.Length = 0 Then
            target.Write(New Byte() {}, 0, 0)
            target.SetLength(0)

            source.Dispose()
            target.Dispose()
            Return
        End If

        Dim progressDialog As New Ookii.Dialogs.ProgressDialog With {
            .Text = " ",
            .Description = description,
            .WindowTitle = title,
            .CancellationText = "Cancelling...",
            .ShowTimeRemaining = True
        }

        AddHandler progressDialog.DoWork, AddressOf doStreamCopy
        AddHandler progressDialog.RunWorkerCompleted, AddressOf streamCopyCompleted

        progressDialog.Show(New Object() {progressDialog, source, target})
    End Sub

    Private Shared Sub doStreamCopy(sender As Object, e As DoWorkEventArgs)
        Dim inputs As Object() = DirectCast(e.Argument, Object())

        Dim progressDialog As Ookii.Dialogs.ProgressDialog = DirectCast(inputs(0), Ookii.Dialogs.ProgressDialog)
        Dim sourceStream As Stream = DirectCast(inputs(1), Stream)
        Dim targetStream As Stream = DirectCast(inputs(2), Stream)

        Try
            Dim bufferSize As Integer = 4096
            Dim buffer(bufferSize) As Byte
            Dim oldPercent As Integer = 0
            For index As Long = 0 To sourceStream.Length Step bufferSize
                If progressDialog.CancellationPending Then
                    Exit For
                End If

                Dim read As Integer = sourceStream.Read(buffer, 0, bufferSize)
                targetStream.Write(buffer, 0, read)

                Dim progressPercent As Integer = CType(index / sourceStream.Length * 100, Integer)
                If progressPercent > oldPercent Then
                    progressDialog.ReportProgress(progressPercent, "Progress: " & progressPercent & "%", Nothing)
                    oldPercent = progressPercent
                End If

                If progressDialog.CancellationPending Then
                    Exit For
                End If
            Next

            progressDialog.ReportProgress(100, "Flushing data to disk...", Nothing)
        Finally
            If sourceStream IsNot Nothing Then sourceStream.Dispose()
            If targetStream IsNot Nothing Then targetStream.Dispose()
        End Try
    End Sub

    Private Shared Sub streamCopyCompleted(sender As Object, e As RunWorkerCompletedEventArgs)
        If e.Cancelled Then
            Throw New OperationCanceledException("Stream copy was Cancelled")
        ElseIf e.Error IsNot Nothing Then
            Throw e.Error
        End If
    End Sub
#End If
End Class
