Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System.Windows.Forms
Imports Microsoft.VisualBasic

Public Enum WinVersionStyle
    WinXP
    Win7
    Win10
End Enum

Public Partial Class CustomMsgBoxForm
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

    Private Enum enumFormLevel
        Critical
        Exclamation
        Information
        Question
    End Enum

    Dim FormLevel As enumFormLevel = Nothing
    Public Button1Text As String = Nothing
    Public Button2Text As String = Nothing
    Public Button3Text As String = Nothing
    Public WinVersion As WinVersionStyle = WinVersionStyle.Win10
    Public DialogResultString As String = Nothing
    
    Public WriteOnly Property Buttons() As MsgBoxStyle
        Set(value As MsgBoxStyle)
            If value <> 0 Then
                If value.HasFlag(MsgBoxStyle.Critical) Then
                    FormLevel = enumFormLevel.Critical
                ElseIf value.HasFlag(MsgBoxStyle.Exclamation) Then
                    FormLevel = enumFormLevel.Exclamation
                ElseIf value.HasFlag(MsgBoxStyle.Information) Then
                    FormLevel = enumFormLevel.Information
                ElseIf value.HasFlag(MsgBoxStyle.Question) Then
                    FormLevel = enumFormLevel.Question
                End If
                
                If value.HasFlag(MsgBoxStyle.AbortRetryIgnore) Then
                    Button1Text = "Abort"
                    Button2Text = "Retry"
                    Button3Text = "Ignore"
                ElseIf value.HasFlag(MsgBoxStyle.RetryCancel) Then
                    Button1Text = "Retry"
                    Button3Text = "Cancel"
                ElseIf value.HasFlag(MsgBoxStyle.YesNoCancel) Then
                    Button1Text = "Yes"
                    Button2Text = "No"
                    Button3Text = "Cancel"
                ElseIf value.HasFlag(MsgBoxStyle.YesNo) Then
                    Button1Text = "Yes"
                    Button3Text = "No"
                ElseIf value.HasFlag(MsgBoxStyle.OkCancel) Then
                    Button1Text = "Ok"
                    Button3Text = "Cancel"
                ElseIf value.HasFlag(MsgBoxStyle.OkOnly) Then
                    Button1Text = "Ok"
                End If
            Else
                Button1Text = "Ok"
            End If
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
        
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CustomMsgBoxForm))
        Me.Icon = DirectCast(resources.GetObject(WinVersion.ToString & "_" & FormLevel.ToString), System.Drawing.Icon)
        If FormLevel <> Nothing Then pbxMain.Image = Me.Icon.ToBitmap
        Try
            Me.Icon = Owner.Icon
        Catch
            'Me.Icon = DirectCast(pbxMain.Image, System.Drawing.Image)
            ' doesn't work, and it's already set above anyway
            If FormLevel = Nothing Then
                Me.ShowIcon = False
            End If
        End Try
        
        Select Case FormLevel
            Case enumFormLevel.Critical
                My.Computer.Audio.PlaySystemSound(System.Media.SystemSounds.Beep)
            Case enumFormLevel.Exclamation
                My.Computer.Audio.PlaySystemSound(System.Media.SystemSounds.Exclamation)
            Case enumFormLevel.Information
                My.Computer.Audio.PlaySystemSound(System.Media.SystemSounds.Asterisk)
            Case enumFormLevel.Question
                My.Computer.Audio.PlaySystemSound(System.Media.SystemSounds.Question)
        End Select
        
        If lblMain.Height > 96 Then
            Me.Height = 242 + (lblMain.Height - 96)
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
                btnAccept.Location = New System.Drawing.Point(btnAccept.Location.X - (btnAnswerMid.Width - 75), btnAccept.Location.Y)
            End If
        Else
            btnAnswerMid.Visible = False
        End If
        If Button3Text <> Nothing Then
            btnCancel.Text = Button3Text
            btnCancel.Visible = True
            If btnCancel.Width > 75 Then ' move the other two buttons to the left, as btnCancel is anchored right
                btnAnswerMid.Location = New System.Drawing.Point(btnAnswerMid.Location.X - (btnCancel.Width - 75), btnAnswerMid.Location.Y)
                btnAccept.Location = New System.Drawing.Point(btnAccept.Location.X - (btnCancel.Width - 75), btnAccept.Location.Y)
            End If
        Else
            btnCancel.Visible = False
        End If
    End Sub
    
    Private Function GetDialogResult(buttonText As String) As DialogResult
        Select Case buttonText
            Case "Abort"
                Return DialogResult.Abort
            Case "Cancel"
                Return DialogResult.Cancel
            Case "Ignore"
                Return DialogResult.Ignore
            Case "No"
                Return DialogResult.No
            Case "Ok"
                Return DialogResult.OK
            Case "Retry"
                Return DialogResult.Retry
            Case "Yes"
                Return DialogResult.Yes
            Case Else
                Return DialogResult.None
        End Select
    End Function
    
    Private Sub Accept_Click() Handles btnAccept.Click
        Me.DialogResult = GetDialogResult(Button1Text)
        DialogResultString = Button1Text ' for use with custom buttons
        If GetDialogResult(Button1Text) = DialogResult.None Then
            Me.Close
        End If
    End Sub
    
    Private Sub AnswerMid_Click() Handles btnAnswerMid.Click
        Me.DialogResult = GetDialogResult(Button2Text)
        DialogResultString = Button2Text
        If GetDialogResult(Button2Text) = DialogResult.None Then
            Me.Close
        End If
    End Sub
    
    Private Sub Cancel_Click() Handles btnCancel.Click
        Me.DialogResult = GetDialogResult(Button3Text)
        DialogResultString = Button3Text
        If GetDialogResult(Button3Text) = DialogResult.None Then
            Me.Close
        End If
    End Sub
End Class

Public Partial Class WalkmanLib
    ''' <summary>Shows a custom messagebox</summary>
    ''' <param name="Prompt">The text to display in the messagebox window</param>
    ''' <param name="Buttons">Buttons and style of messagebox to show. A bitwise combination of the enumeration values. Default: OkOnly</param>
    ''' <param name="Title">Title of the messagebox window. If left out or set to Nothing, then title will be set to the owner form title or CustomMsgBox.</param>
    ''' <param name="WinVersion">Windows version to use style icons from. Default: WinVersionStyle.Win10</param>
    ''' <returns>The button the user clicked on.</returns>
    Shared Function CustomMsgBox(Prompt As String, Optional Buttons As MsgBoxStyle = 0, _
      Optional Title As String = Nothing, Optional WinVersion As WinVersionStyle = WinVersionStyle.Win10) As DialogResult
        Dim formToShow As New CustomMsgBoxForm With {
            .Prompt = Prompt,
            .Buttons = Buttons,
            .Title = Title,
            .WinVersion = WinVersion
        }
        Return formToShow.ShowDialog
    End Function
    
    ''' <summary>Shows a custom messagebox with custom buttons</summary>
    ''' <param name="Prompt">The text to display in the messagebox window</param>
    ''' <param name="customButton1">Text to show on the first button</param>
    ''' <param name="customButton2">Text to show on the second button. If left out or set to Nothing, this button will be hidden.</param>
    ''' <param name="customButton3">Text to show on the third button. If left out or set to Nothing, this button will be hidden.</param>
    ''' <param name="Style">Style of messagebox to show. Default: 0</param>
    ''' <param name="Title">Title of the messagebox window. If left out or set to Nothing, then title will be set to the owner form title or CustomMsgBox.</param>
    ''' <param name="WinVersion">Windows version to use style icons from. Default: WinVersionStyle.Win10</param>
    ''' <returns>Text of the button the user clicked on.</returns>
    Shared Function CustomMsgBox(Prompt As String, customButton1 As String, Optional customButton2 As String = Nothing, Optional customButton3 As String = Nothing, Optional Style As MsgBoxStyle = 0, _
      Optional Title As String = Nothing, Optional WinVersion As WinVersionStyle = WinVersionStyle.Win10) As String
        Dim formToShow As New CustomMsgBoxForm With {
            .Prompt = Prompt,
            .Buttons = Style,
            .Button1Text = customButton1,
            .Button2Text = customButton2,
            .Button3Text = customButton3,
            .Title = Title,
            .WinVersion = WinVersion
        }
        ' .Buttons = Style above is required to set the formlevel

        formToShow.ShowDialog
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
