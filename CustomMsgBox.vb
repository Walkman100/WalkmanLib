Imports Microsoft.VisualBasic
Public Partial Class CustomMsgBoxForm
    
    Public Sub New()
        System.Windows.Forms.Application.EnableVisualStyles()
        ' The Me.InitializeComponent call is required for Windows Forms designer support.
        Me.InitializeComponent()
        
    End Sub
    
    Public Property Prompt() As String
        Get
            Return lblMain.Text
        End Get
        Set
            lblMain.Text = value
        End Set
    End Property
    
    Public Property Title() As String
        Get
            Return Me.Text
        End Get
        Set
            Me.Text = value
        End Set
    End Property
    
    Public Enum WinVersionStyle
        Win7
        Win10
    End Enum
    
    Dim FormLevel As String = Nothing
    Public Button1Text As String = Nothing
    Public Button2Text As String = Nothing
    Public Button3Text As String = Nothing
    Public WinVersion As WinVersionStyle = WinVersionStyle.Win10
    
    Public WriteOnly Property Buttons() As MsgBoxStyle
        Set
            If value <> 0 Then
                If value.HasFlag(MsgBoxStyle.Critical) Then
                    FormLevel = "Critical"
                ElseIf value.HasFlag(MsgBoxStyle.Exclamation)
                    FormLevel = "Exclamation"
                ElseIf value.HasFlag(MsgBoxStyle.Information)
                    FormLevel = "Information"
                ElseIf value.HasFlag(MsgBoxStyle.Question)
                    FormLevel = "Question"
                End If
                
                If value.HasFlag(MsgBoxStyle.AbortRetryIgnore) Then
                    Button1Text = "Abort"
                    Button2Text = "Retry"
                    Button3Text = "Ignore"
                ElseIf value.HasFlag(MsgBoxStyle.RetryCancel)
                    Button1Text = "Retry"
                    Button3Text = "Cancel"
                ElseIf value.HasFlag(MsgBoxStyle.YesNoCancel)
                    Button1Text = "Yes"
                    Button2Text = "No"
                    Button3Text = "Cancel"
                ElseIf value.HasFlag(MsgBoxStyle.YesNo)
                    Button1Text = "Yes"
                    Button3Text = "Cancel"
                ElseIf value.HasFlag(MsgBoxStyle.OkCancel)
                    Button1Text = "Ok"
                    Button3Text = "Cancel"
                ElseIf value.HasFlag(MsgBoxStyle.OkOnly)
                    Button1Text = "Ok"
                End If
            Else
                Button1Text = "Ok"
            End If
        End Set
    End Property
    
    Private Sub MeShown() Handles Me.Shown
        If Me.Text = Nothing Then
            Try
                Me.Text = Owner.Text
            Catch
                Me.Text = "CustomMsgBox"
            End Try
        End If
        
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CustomMsgBoxForm))
        Me.Icon = CType(resources.GetObject(WinVersion.ToString & "_" & FormLevel), System.Drawing.Icon)
        If FormLevel <> Nothing Then pbxMain.Image = Me.Icon.ToBitmap
        Try
            Me.Icon = Owner.Icon
        Catch
            'Me.Icon = CType(pbxMain.Image, System.Drawing.Icon)
            ' doesn't work, and it's already set above anyway
            If FormLevel = Nothing Then
                Me.ShowIcon = False
            End If
        End Try
        
        If Button1Text <> Nothing Then
            btnAccept.Text = Button1Text
            btnAccept.Visible = True
        Else
            btnAccept.Visible = False
        End If
        If Button2Text <> Nothing Then
            btnAnswerMid.Text = Button2Text
            btnAnswerMid.Visible = True
        Else
            btnAnswerMid.Visible = False
        End If
        If Button3Text <> Nothing Then
            btnCancel.Text = Button3Text
            btnCancel.Visible = True
        Else
            btnCancel.Visible = False
        End If
    End Sub
    
    Private Function GetDialogResult(buttonText As String) As System.Windows.Forms.DialogResult
        Select Case buttonText
            Case "Abort"
                Return System.Windows.Forms.DialogResult.Abort
            Case "Cancel"
                Return System.Windows.Forms.DialogResult.Cancel
            Case "Ignore"
                Return System.Windows.Forms.DialogResult.Ignore
            Case "No"
                Return System.Windows.Forms.DialogResult.No
            Case "None"
                Return System.Windows.Forms.DialogResult.None
            Case "Ok"
                Return System.Windows.Forms.DialogResult.OK
            Case "Retry"
                Return System.Windows.Forms.DialogResult.Retry
            Case "Yes"
                Return System.Windows.Forms.DialogResult.Yes
            Case Else
                Return System.Windows.Forms.DialogResult.None
        End Select
    End Function
    
    Private Sub Accept_Click() Handles btnAccept.Click
        Me.DialogResult = GetDialogResult(Button1Text)
    End Sub
    
    Private Sub AnswerMid_Click() Handles btnAnswerMid.Click
        Me.DialogResult = GetDialogResult(Button2Text)
    End Sub
    
    Private Sub Cancel_Click() Handles btnCancel.Click
        Me.DialogResult = GetDialogResult(Button3Text)
    End Sub
    
    Private Sub BtnAnswerMidClick(sender As Object, e As System.EventArgs) Handles btnAnswerMid.Click
        MsgBox("test", MsgBoxStyle.Question)
        MsgBox("test", MsgBoxStyle.Information)
        MsgBox("test", MsgBoxStyle.Exclamation)
        MsgBox("test", MsgBoxStyle.Critical)
        MsgBox("test", MsgBoxStyle.YesNoCancel + MsgBoxStyle.Critical, "hi")
        MsgBox("some extremely long text  Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse ut tellus consectetur, semper metus vitae, tincidunt enim. Phasellus nisi sem, ultrices ut nisi pretium, dignissim scelerisque lorem. Sed semper sodales eleifend. Proin scelerisque faucibus nunc, ac dictum risus rhoncus viverra. Pellentesque ut risus augue. Nam mollis sem sed urna pharetra, lacinia sollicitudin neque scelerisque. Morbi at ligula finibus, vehicula lectus ut, faucibus nibh. Mauris eu tempor turpis. Proin congue, sem a mattis venenatis, tellus sapien dapibus libero, nec commodo velit mauris eu risus. Nam ultrices fermentum nulla sed pretium. Vivamus eleifend in massa at mattis. Maecenas eget euismod neque, vel commodo dui. Nam massa velit, imperdiet at efficitur ac, faucibus id urna." _
            & vbNewLine & vbNewLine & _
            "Quisque gravida quam blandit risus eleifend, eget gravida magna egestas. Aliquam erat volutpat. Praesent commodo in nunc eu tempus. In ac metus sit amet eros porttitor aliquet. Duis volutpat magna lectus, cursus luctus eros pellentesque et. Duis non sapien quis nisi fringilla convallis. Phasellus egestas magna justo, sed semper nibh aliquet sit amet. In ultrices ultrices neque ut vulputate. Nam quis vulputate diam. Nullam id tellus eu ligula varius fringilla eu eu elit. Nullam sodales finibus nunc, nec suscipit ligula ultricies at. ", _
            MsgBoxStyle.AbortRetryIgnore + MsgBoxStyle.Information, "hi2")
    End Sub
End Class

' visual styles in enabled in a Windows Forms Application with the following:
'Namespace My
'    Partial Class MyApplication
'        Public Sub New()
'            Me.EnableVisualStyles = True
'        End Sub
'        Protected Overrides Sub OnCreateMainForm()
'            Me.MainForm = My.Forms.CustomMsgBox
'        End Sub
'    End Class
'End Namespace