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
            Return txtMain.Text
        End Get
        Set(value As String)
            txtMain.Text = value
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

    Public ReadOnly Property MainText() As TextBox
        Get
            Return txtMain
        End Get
    End Property

    Public Property FormLevel() As MessageBoxIcon
    Private Function getFormLevelString() As String
        Select Case FormLevel
            Case MessageBoxIcon.Error
                Return "Error"
            Case MessageBoxIcon.Question
                Return "Question"
            Case MessageBoxIcon.Exclamation
                Return "Exclamation"
            Case MessageBoxIcon.Information
                Return "Information"
        End Select
        Return ""
    End Function

    Public Button1Text As String = Nothing
    Public Button2Text As String = Nothing
    Public Button3Text As String = Nothing
    Public WinVersion As WinVersionStyle = WinVersionStyle.Win10
    Public DialogResultString As String = Nothing

    Public WriteOnly Property Buttons() As MessageBoxButtons
        Set(value As MessageBoxButtons)
            Select Case value
                Case MessageBoxButtons.OK
                    Button1Text = "OK"
                Case MessageBoxButtons.OKCancel
                    Button1Text = "OK"
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
        Me.Icon = DirectCast(resources.GetObject(WinVersion.ToString & "_" & getFormLevelString()), Drawing.Icon)
        If FormLevel <> MessageBoxIcon.None Then pbxMain.Image = Me.Icon.ToBitmap()
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

        ' as TextBox doesn't have an AutoSize property like Label does, we have to do it manually
        Using g As Drawing.Graphics = txtMain.CreateGraphics()
            Dim sizeF As Drawing.SizeF = g.MeasureString(txtMain.Text, txtMain.Font, New Drawing.SizeF(txtMain.MaximumSize.Width, Single.MaxValue))
            txtMain.Height = CType(Math.Ceiling(sizeF.Height / txtMain.Font.Height), Integer) * txtMain.Font.Height ' restrain to line height
        End Using

        If txtMain.Height > 13 Then
            Me.Height = 162 + (txtMain.Height - 13)
        End If

        Dim showB1 As Boolean = Not String.IsNullOrWhiteSpace(Button1Text)
        Dim showB2 As Boolean = Not String.IsNullOrWhiteSpace(Button2Text)
        Dim showB3 As Boolean = Not String.IsNullOrWhiteSpace(Button3Text)

        btnAccept.Visible = showB1
        btnAccept.Text = Button1Text
        btnAnswerMid.Visible = showB2
        btnAnswerMid.Text = Button2Text
        btnCancel.Visible = showB3
        btnCancel.Text = Button3Text

        Const maxTotalWidth As Integer = 242
        Const buttonSpacing As Integer = 8

        Dim currentTotalWidth As Integer = If(showB1, btnAccept.Width, 0) + If(showB2, btnAnswerMid.Width, 0) + If(showB3, btnCancel.Width, 0)
        If showB1 AndAlso showB2 AndAlso showB3 Then
            currentTotalWidth += buttonSpacing * 2
        ElseIf showB1 AndAlso showB2 OrElse showB1 AndAlso showB3 OrElse showB2 AndAlso showB3 Then
            currentTotalWidth += buttonSpacing
        End If

        If currentTotalWidth <= maxTotalWidth Then ' if enough space, align to left point
            Const button1LeftStartX As Integer = 152

            If showB1 Then setButtonX(btnAccept, button1LeftStartX)

            If showB1 AndAlso showB2 Then
                setButtonX(btnAnswerMid, getButtonRightX(btnAccept) + buttonSpacing)
            ElseIf showB2 Then
                setButtonX(btnAnswerMid, button1LeftStartX)
            End If

            If showB2 AndAlso showB3 Then
                setButtonX(btnCancel, getButtonRightX(btnAnswerMid) + buttonSpacing)
            ElseIf showB1 AndAlso showB3 Then
                setButtonX(btnCancel, getButtonRightX(btnAccept) + buttonSpacing)
            ElseIf showB3 Then
                setButtonX(btnCancel, button1LeftStartX)
            End If
        Else ' if not enough space, align right
            Const button3rightX As Integer = 393

            If showB3 Then setButtonX(btnCancel, button3rightX - btnCancel.Width)

            If showB3 AndAlso showB2 Then
                setButtonX(btnAnswerMid, btnCancel.Location.X - btnAnswerMid.Width - buttonSpacing)
            ElseIf showB2 Then
                setButtonX(btnAnswerMid, button3rightX - btnAnswerMid.Width)
            End If

            If showB2 AndAlso showB1 Then
                setButtonX(btnAccept, btnAnswerMid.Location.X - btnAccept.Width - buttonSpacing)
            ElseIf showB3 AndAlso showB1 Then
                setButtonX(btnAccept, btnCancel.Location.X - btnAccept.Width - buttonSpacing)
            ElseIf showB1 Then
                setButtonX(btnAccept, button3rightX - btnAccept.Width)
            End If
        End If

        If Me.Owner IsNot Nothing Then
            Me.CenterToParent()
        Else
            Me.CenterToScreen()
        End If
        btnAccept.Select()
    End Sub
    Function getButtonRightX(btn As Button) As Integer
        Return btn.Location.X + btn.Width
    End Function
    Sub setButtonX(btn As Button, x As Integer)
        btn.Location = New Drawing.Point(x, btn.Location.Y)
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
        Return formToShow.ShowDialog()
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
