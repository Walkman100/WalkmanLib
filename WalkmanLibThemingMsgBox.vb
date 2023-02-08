Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System.Windows.Forms

Public Partial Class CustomMsgBoxForm
    Public Sub ApplyTheme(theme As WalkmanLib.Theme)
        WalkmanLib.ApplyTheme(theme, Me)
        Me.splitContainer.Panel1.BackColor = theme.CustomMsgBoxTopPanel
        Me.txtMain.BackColor = theme.CustomMsgBoxTopPanel
        Me.pbxMain.BackColor = theme.CustomMsgBoxTopPanel
    End Sub
End Class

Public Partial Class WalkmanLib
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

    Shared Function CustomMsgBox(text As String, theme As WalkmanLib.Theme, caption As String, customButton1 As String, Optional customButton2 As String = Nothing, Optional custemButton3 As String = Nothing,
                                 Optional style As MessageBoxIcon = 0, Optional winVersion As WinVersionStyle = WinVersionStyle.Win10, Optional ownerForm As Form = Nothing) As String
        Dim formToShow As New CustomMsgBoxForm() With {
            .Prompt = text,
            .Title = caption,
            .Button1Text = customButton1,
            .Button2Text = customButton2,
            .Button3Text = custemButton3,
            .FormLevel = style,
            .WinVersion = winVersion,
            .Owner = ownerForm,
            .ShowInTaskbar = False
        }
        formToShow.ApplyTheme(theme)
        formToShow.ShowDialog()

        Return formToShow.DialogResultString
    End Function
End Class
