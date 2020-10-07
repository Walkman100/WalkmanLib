Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports System.Windows.Forms

Public Enum WinVersionStyle
    WinXP
    Win7
    Win10
End Enum

Partial Public Class CustomMsgBoxForm
    Public Sub New()
        Application.EnableVisualStyles()
        ' The Me.InitializeComponent call is required for Windows Forms designer support.
        Me.InitializeComponent()
    End Sub

    ' properties

    Public Property Prompt() As String
        Get
            Return lblMain.Text
        End Get
        Set(value As String)
            lblMain.Text = value
        End Set
    End Property

    Public Property Title() As String
        Get
            Return Me.Text
        End Get
        Set(value As String)
            Me.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Only <see cref="MessageBoxIcon.Error"/>, <see cref="MessageBoxIcon.Question"/>, <see cref="MessageBoxIcon.Exclamation"/> or <see cref="MessageBoxIcon.Information"/> are valid!
    ''' </summary>
    Public Property FormLevel() As MessageBoxIcon

    Public Button1Text As String = Nothing
    Public Button2Text As String = Nothing
    Public Button3Text As String = Nothing
    Public WinVersion As WinVersionStyle = WinVersionStyle.Win10
    Public DialogResultString As String = Nothing

    Public WriteOnly Property Buttons() As MessageBoxButtons
        Set(value As MessageBoxButtons)
            Select Case value
                Case MessageBoxButtons.OK
                    Button1Text = "Ok"
                Case MessageBoxButtons.OKCancel
                    Button1Text = "Ok"
                    Button3Text = "Cancel"
                Case MessageBoxButtons.AbortRetryIgnore
                    Button1Text = "Abort"
                    Button2Text = "Retry"
                    Button3Text = "Ignore"
                Case MessageBoxButtons.YesNoCancel
                    Button1Text = "Yes"
                    Button2Text = "No"
                    Button3Text = "Cancel"
                Case MessageBoxButtons.YesNo
                    Button1Text = "Yes"
                    Button3Text = "No"
                Case MessageBoxButtons.RetryCancel
                    Button1Text = "Retry"
                    Button3Text = "Cancel"
            End Select
        End Set
    End Property

    ' subs & functions

    Private Sub MeShown() Handles Me.Shown
        If Me.Text = Nothing Then
            Try
                Me.Text = Owner.Text
            Catch
                Me.Text = "CustomMsgBox"
            End Try
        End If

        Dim resources As New System.ComponentModel.ComponentResourceManager(GetType(CustomMsgBoxForm))
        Me.Icon = DirectCast(resources.GetObject(WinVersion.ToString & "_" & FormLevel.ToString), Drawing.Icon)
        If FormLevel <> MessageBoxIcon.None Then pbxMain.Image = Me.Icon.ToBitmap
        Try
            Me.Icon = Owner.Icon
        Catch
            'Me.Icon = DirectCast(pbxMain.Image, System.Drawing.Image)
            ' doesn't work, and it's already set above anyway
            If FormLevel = MessageBoxIcon.None Then
                Me.ShowIcon = False
            End If
        End Try

        Select Case FormLevel
            Case MessageBoxIcon.Error
                My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Beep)
            Case MessageBoxIcon.Exclamation
                My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Exclamation)
            Case MessageBoxIcon.Information
                My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Asterisk)
            Case MessageBoxIcon.Question
                My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Question)
        End Select

        If lblMain.Height > 13 Then
            Me.Height = 162 + (lblMain.Height - 13)
        End If

        If Button1Text <> Nothing Then
            btnAccept.Text = Button1Text
            btnAccept.Visible = True
        Else
            btnAccept.Visible = False
        End If
        If Button2Text <> Nothing Then
            btnAnswerMid.Text = Button2Text
            btnAnswerMid.Visible = True
            If btnAnswerMid.Width > 75 Then ' move btnAccept to the left, as btnAnswerMid is anchored right
                btnAccept.Location = New Drawing.Point(btnAccept.Location.X - (btnAnswerMid.Width - 75), btnAccept.Location.Y)
            End If
        Else
            btnAnswerMid.Visible = False
        End If
        If Button3Text <> Nothing Then
            btnCancel.Text = Button3Text
            btnCancel.Visible = True
            If btnCancel.Width > 75 Then ' move the other two buttons to the left, as btnCancel is anchored right
                btnAnswerMid.Location = New Drawing.Point(btnAnswerMid.Location.X - (btnCancel.Width - 75), btnAnswerMid.Location.Y)
                btnAccept.Location = New Drawing.Point(btnAccept.Location.X - (btnCancel.Width - 75), btnAccept.Location.Y)
            End If
        Else
            btnCancel.Visible = False
        End If
    End Sub

    Private Function GetDialogResult(buttonText As String) As DialogResult
        Dim result As DialogResult
        If [Enum].TryParse(buttonText, True, result) Then
            Return result
        Else
            Return DialogResult.None
        End If
    End Function

    Private Sub Accept_Click() Handles btnAccept.Click
        Me.DialogResult = GetDialogResult(Button1Text)
        DialogResultString = Button1Text ' for use with custom buttons
        If GetDialogResult(Button1Text) = DialogResult.None Then
            Me.Close()
        End If
    End Sub

    Private Sub AnswerMid_Click() Handles btnAnswerMid.Click
        Me.DialogResult = GetDialogResult(Button2Text)
        DialogResultString = Button2Text
        If GetDialogResult(Button2Text) = DialogResult.None Then
            Me.Close()
        End If
    End Sub

    Private Sub Cancel_Click() Handles btnCancel.Click
        Me.DialogResult = GetDialogResult(Button3Text)
        DialogResultString = Button3Text
        If GetDialogResult(Button3Text) = DialogResult.None Then
            Me.Close()
        End If
    End Sub
End Class

Partial Public Class WalkmanLib
    ''' <summary>Shows a custom messagebox</summary>
    ''' <param name="text">The text to display in the message box.</param>
    ''' <param name="caption">The text to display in the title bar of the message box.</param>
    ''' <param name="buttons">One of the <see cref="MessageBoxButtons"/> values that specifies which buttons to display in the message box.</param>
    ''' <param name="style">One of the following: <see cref="MessageBoxIcon.Error"/>, <see cref="MessageBoxIcon.Question"/>, <see cref="MessageBoxIcon.Exclamation"/> or <see cref="MessageBoxIcon.Information"/>.</param>
    ''' <param name="winVersion">Windows version to use style icons from. Default: <see cref="WinVersionStyle.Win10"/></param>
    ''' <param name="ownerForm">Used to set the Window's Icon. Set to <see langword="Me"/> to copy the current form's icon</param>
    ''' <returns>The button the user clicked on.</returns>
    Shared Function CustomMsgBox(text As String, Optional caption As String = Nothing, Optional buttons As MessageBoxButtons = 0, Optional style As MessageBoxIcon = 0,
                                  Optional winVersion As WinVersionStyle = WinVersionStyle.Win10, Optional ownerForm As Form = Nothing) As DialogResult
        Dim formToShow As New CustomMsgBoxForm With {
            .Prompt = text,
            .Title = caption,
            .Buttons = buttons,
            .FormLevel = style,
            .WinVersion = winVersion,
            .Owner = ownerForm,
            .ShowInTaskbar = False
        }
        Return formToShow.ShowDialog
    End Function

    ''' <summary>Shows a custom messagebox with custom buttons</summary>
    ''' <param name="text">The text to display in the message box.</param>
    ''' <param name="caption">The text to display in the title bar of the message box.</param>
    ''' <param name="customButton1">Text to show on the first button</param>
    ''' <param name="customButton2">Text to show on the second button. If left out or set to <see langword="Nothing"/>, this button will be hidden.</param>
    ''' <param name="customButton3">Text to show on the third button. If left out or set to <see langword="Nothing"/>, this button will be hidden.</param>
    ''' <param name="style">One of the following: <see cref="MessageBoxIcon.Error"/>, <see cref="MessageBoxIcon.Question"/>, <see cref="MessageBoxIcon.Exclamation"/> or <see cref="MessageBoxIcon.Information"/>.</param>
    ''' <param name="winVersion">Windows version to use style icons from. Default: <see cref="WinVersionStyle.Win10"/></param>
    ''' <param name="ownerForm">Used to set the Window's Icon. Set to <see langword="Me"/> to copy the current form's icon</param>
    ''' <returns>Text of the button the user clicked on.</returns>
    Shared Function CustomMsgBox(text As String, caption As String, customButton1 As String, Optional customButton2 As String = Nothing, Optional customButton3 As String = Nothing,
                                  Optional style As MessageBoxIcon = 0, Optional winVersion As WinVersionStyle = WinVersionStyle.Win10, Optional ownerForm As Form = Nothing) As String
        Dim formToShow As New CustomMsgBoxForm With {
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

        formToShow.ShowDialog()
        Return formToShow.DialogResultString
    End Function
End Class

' visual styles is enabled in a Windows Forms Application with the following:
'Namespace My
'    Partial Class MyApplication
'        Public Sub New()
'            Me.EnableVisualStyles = True
'        End Sub
'        Protected Overrides Sub OnCreateMainForm()
'            Me.MainForm = My.Forms.CustomMsgBoxForm
'        End Sub
'    End Class
'End Namespace
