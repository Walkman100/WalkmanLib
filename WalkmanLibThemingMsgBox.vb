Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports System.Windows.Forms

Partial Public Class CustomMsgBoxForm
    Public Sub ApplyTheme(theme As WalkmanLib.Theme)
        WalkmanLib.ApplyTheme(theme, Me)
        Me.splitContainer.Panel1.BackColor = theme.CustomMsgBoxTopPanel
        Me.txtMain.BackColor = theme.CustomMsgBoxTopPanel
        Me.pbxMain.BackColor = theme.CustomMsgBoxTopPanel
    End Sub
End Class

Partial Public Class CustomInputDialog
    Public Sub ApplyTheme(theme As WalkmanLib.Theme)
        WalkmanLib.ApplyTheme(theme, Me)
        Me.splitContainer.Panel1.BackColor = theme.CustomMsgBoxTopPanel
        Me.lblMainInstruction.BackColor = theme.CustomMsgBoxTopPanel
        Me.lblContent.BackColor = theme.CustomMsgBoxTopPanel

        Me.lblMainInstruction.ForeColor = theme.DialogHeadingText
    End Sub
End Class

Partial Public Class WalkmanLib
    Shared Function CustomMsgBox(text As String, theme As WalkmanLib.Theme, Optional caption As String = Nothing, Optional buttons As MessageBoxButtons = 0, Optional style As MessageBoxIcon = 0,
                                 Optional winVersion As WinVersionStyle = WinVersionStyle.Win10, Optional ownerForm As Form = Nothing) As DialogResult
        Dim formToShow As New CustomMsgBoxForm() With {
            .Prompt = text,
            .Title = caption,
            .Buttons = buttons,
            .FormLevel = style,
            .WinVersion = winVersion,
            .Owner = ownerForm,
            .ShowInTaskbar = False
        }
        formToShow.ApplyTheme(theme)
        Return formToShow.ShowDialog()
    End Function

    Shared Function CustomMsgBox(text As String, theme As WalkmanLib.Theme, caption As String, customButton1 As String, Optional customButton2 As String = Nothing, Optional customButton3 As String = Nothing,
                                 Optional style As MessageBoxIcon = 0, Optional winVersion As WinVersionStyle = WinVersionStyle.Win10, Optional ownerForm As Form = Nothing) As String
        Dim formToShow As New CustomMsgBoxForm() With {
            .Prompt = text,
            .Title = caption,
            .Button1Text = customButton1,
            .Button2Text = customButton2,
            .Button3Text = customButton3,
            .FormLevel = style,
            .WinVersion = winVersion,
            .Owner = ownerForm,
            .ShowInTaskbar = False
        }
        formToShow.ApplyTheme(theme)
        formToShow.ShowDialog()

        Return formToShow.DialogResultString
    End Function

    Shared Function CustomMsgBoxBTN(text As String, theme As WalkmanLib.Theme, caption As String, customButton1 As String, Optional customButton2 As String = Nothing, Optional customButton3 As String = Nothing,
                                    Optional style As MessageBoxIcon = 0, Optional winVersion As WinVersionStyle = WinVersionStyle.Win10, Optional ownerForm As Form = Nothing) As Byte
        Dim formToShow As New CustomMsgBoxForm() With {
            .Prompt = text,
            .Title = caption,
            .Button1Text = customButton1,
            .Button2Text = customButton2,
            .Button3Text = customButton3,
            .FormLevel = style,
            .WinVersion = winVersion,
            .Owner = ownerForm,
            .ShowInTaskbar = False
        }
        formToShow.ApplyTheme(theme)
        formToShow.ShowDialog()

        Return formToShow.DialogResultButtonPressed
    End Function

    Shared Function InputDialog(ByRef input As String, theme As WalkmanLib.Theme, Optional mainInstruction As String = Nothing, Optional title As String = Nothing, Optional content As String = Nothing,
                                Optional usePasswordMasking As Boolean = False, Optional maxLength As Integer = Short.MaxValue, Optional ownerForm As Form = Nothing) As DialogResult
        Dim inputForm As New CustomInputDialog() With {
            .MainInstruction = mainInstruction,
            .Content = content,
            .Text = title,
            .Input = input,
            .UsePasswordMasking = usePasswordMasking,
            .MaxLength = maxLength,
            .Owner = ownerForm
        }
        inputForm.ApplyTheme(theme)
        Dim result As DialogResult = inputForm.ShowDialog()
        If result = DialogResult.OK Then input = inputForm.Input

        Return result
    End Function

    Shared Sub ErrorDialog(ex As Exception, theme As WalkmanLib.Theme, Optional errorMessage As String = "There was an error! Error message: ", Optional showMsgBox As Boolean = True, Optional ownerForm As Form = Nothing, Optional showErrorBlockingWindow As Boolean? = Nothing)
        Application.EnableVisualStyles() ' affects when in a console app
        If showMsgBox AndAlso WalkmanLib.CustomMsgBox(errorMessage & ex.Message & Environment.NewLine & "Show full stacktrace? (For sending to developer/making bugreport)",
                                                      theme, "Error!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, ownerForm:=ownerForm) <> DialogResult.Yes Then
            Exit Sub
        End If

        Dim frmBugReport As New Form With {
            .Width = 600,
            .Height = 525,
            .StartPosition = FormStartPosition.CenterParent,
            .ShowIcon = False,
            .Text = "Full error trace"
        }
        Dim txtBugReport As New TextBox With {
            .Multiline = True,
            .ScrollBars = ScrollBars.Vertical,
            .Dock = DockStyle.Fill
        }
        frmBugReport.Controls.Add(txtBugReport)
        WalkmanLib.ApplyTheme(theme, frmBugReport)

        Try
            txtBugReport.Text = ""
            While ex IsNot Nothing
                If ex.ToString() IsNot Nothing Then         txtBugReport.Text &= "ToString:" & Environment.NewLine & ex.ToString() & Environment.NewLine & Environment.NewLine
                If ex.GetBaseException() IsNot Nothing Then txtBugReport.Text &= "BaseException:" & Environment.NewLine & ex.GetBaseException().ToString() & Environment.NewLine & Environment.NewLine
                If ex.GetType() IsNot Nothing Then          txtBugReport.Text &= "Type: " & ex.GetType().ToString() & Environment.NewLine
                If ex.Message IsNot Nothing Then            txtBugReport.Text &= "Message: " & ex.Message & Environment.NewLine & Environment.NewLine
                If ex.StackTrace IsNot Nothing Then         txtBugReport.Text &= "StackTrace:" & Environment.NewLine & ex.StackTrace & Environment.NewLine & Environment.NewLine
                If TypeOf ex Is System.ComponentModel.Win32Exception Then
                                                            txtBugReport.Text &= "ErrorCode: 0x" & DirectCast(ex, System.ComponentModel.Win32Exception).ErrorCode.ToString("X") & Environment.NewLine
                                                            txtBugReport.Text &= "NativeErrorCode: 0x" & DirectCast(ex, System.ComponentModel.Win32Exception).NativeErrorCode.ToString("X") & Environment.NewLine
                End If
                If TypeOf ex Is IO.FileNotFoundException Then
                                                            txtBugReport.Text &= "FileName: " & DirectCast(ex, IO.FileNotFoundException).FileName & Environment.NewLine
                                                            txtBugReport.Text &= "FusionLog: " & DirectCast(ex, IO.FileNotFoundException).FusionLog & Environment.NewLine
                End If
                If ex.Source IsNot Nothing Then             txtBugReport.Text &= "Source: " & ex.Source & Environment.NewLine
                If ex.TargetSite IsNot Nothing Then         txtBugReport.Text &= "TargetSite: " & ex.TargetSite.ToString() & Environment.NewLine
                                                            txtBugReport.Text &= "HashCode: 0x" & ex.GetHashCode().ToString("X") & Environment.NewLine
                                                            txtBugReport.Text &= "HResult: 0x" & ex.HResult.ToString("X") & Environment.NewLine & Environment.NewLine
                For Each key As Object In ex.Data.Keys
                                                            txtBugReport.Text &= "Data(" & key.ToString() & "): " & ex.Data(key).ToString() & Environment.NewLine
                Next
                If ex.InnerException IsNot Nothing Then     txtBugReport.Text &= Environment.NewLine & "InnerException:" & Environment.NewLine
                ex = ex.InnerException
            End While
        Catch ex2 As Exception
            txtBugReport.Text &= "Error getting exception data!" & Environment.NewLine & Environment.NewLine & ex2.ToString()
        End Try

        Try
            If showErrorBlockingWindow.HasValue AndAlso showErrorBlockingWindow.Value Then
                frmBugReport.ShowDialog()
            ElseIf showErrorBlockingWindow.HasValue Then
                frmBugReport.Show()
            Else
                Threading.Tasks.Task.Run(Sub() frmBugReport.ShowDialog())
            End If
        Catch ex2 As Exception
            MessageBox.Show("Error showing window: " & ex2.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub
End Class
