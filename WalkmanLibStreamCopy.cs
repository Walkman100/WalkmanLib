using System;
using System.ComponentModel;
using System.IO;

public partial class WalkmanLib {
    // disable if NoOokii is defined (default for tests project)
#if !NoOokii

        ''' <summary>
        ''' Asynchronously Copy a stream with a progress dialog. Uses <see cref="Ookii.Dialogs.ProgressDialog"/> for the dialog.
        ''' <br />NOTE: As this function exits when the copy process starts, the streams must NOT be closed e.g. by a <see langword="Using"/> statement.
        ''' Streams are always disposed if this function succeeds in starting the dialog.
        ''' </summary>
        ''' <param name="source">Stream to copy from. Must support Reading</param>
        ''' <param name="target">Stream to copy to. Must support Writing</param>
        ''' <param name="description">Optional description to display in the ProgressDialog.</param>
        ''' <param name="title">Optional title to use for the ProgressDialog.</param>
        ''' <param name="onComplete">Optional event handler to run when the Asnyc operation is complete</param>
        Shared Sub StreamCopy(source As Stream, target As Stream, Optional description As String = " ", Optional title As String = "Copying...",
                              Optional onComplete As RunWorkerCompletedEventHandler = Nothing)
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
            AddHandler progressDialog.RunWorkerCompleted, onComplete

            progressDialog.Show(New Object() {progressDialog, source, target})
        End Sub

        Private Shared Sub doStreamCopy(sender As Object, e As DoWorkEventArgs)
            Dim inputs As Object() = DirectCast(e.Argument, Object())

            Dim progressDialog As Ookii.Dialogs.ProgressDialog = DirectCast(inputs(0), Ookii.Dialogs.ProgressDialog)
            Dim sourceStream As Stream = DirectCast(inputs(1), Stream)
            Dim targetStream As Stream = DirectCast(inputs(2), Stream)

            Try ' double-buffer code/idea thanks to https://stackoverflow.com/a/26556205/2999220
                Dim bufferSize As Integer = 1024 * 1024
                Dim buffer(bufferSize) As Byte
                Dim buffer2(bufferSize) As Byte
                Dim swap As Boolean = False
                Dim oldPercent As Integer = 0
                Dim newPercent As Integer = 0
                Dim bytesRead As Integer = 0

                Dim len As Long = sourceStream.Length
                Dim flen As Single = len
                Dim writer As Threading.Tasks.Task = Nothing
                Dim size As Long = 0

                While size < len
                    If progressDialog.CancellationPending Then
                        Throw New OperationCanceledException("Operation was canceled by the user")
                    End If

                    newPercent = CType(size / flen * 100, Integer)
                    If newPercent <> oldPercent Then
                        progressDialog.ReportProgress(newPercent, "Progress: " & newPercent & "%", Nothing)
                        oldPercent = newPercent
                    End If

                    bytesRead = sourceStream.Read(If(swap, buffer, buffer2), 0, bufferSize)
                    If writer IsNot Nothing Then writer.Wait()
                    writer = targetStream.WriteAsync(If(swap, buffer, buffer2), 0, bytesRead)
                    swap = Not swap
                    size += bytesRead

                    If progressDialog.CancellationPending Then
                        Throw New OperationCanceledException("Operation was canceled by the user")
                    End If
                End While
                If writer IsNot Nothing Then writer.Wait()
            Finally
                progressDialog.ReportProgress(100, "Flushing data to disk...", Nothing)
                If sourceStream IsNot Nothing Then sourceStream.Dispose()
                If targetStream IsNot Nothing Then targetStream.Dispose()
            End Try
        End Sub

#endif
}
