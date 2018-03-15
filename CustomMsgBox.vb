Imports Microsoft.VisualBasic
Public Partial Class CustomMsgBox
    
    Public Prompt As String = Nothing
    Public Buttons As MsgBoxStyle = 0
    Public Title As String = Nothing
    
    Public Sub New()
        ' The Me.InitializeComponent call is required for Windows Forms designer support.
        Me.InitializeComponent()
        
        Me.lblMain.Text = Prompt
        If Me.Title <> Nothing Then
            Me.Text = Me.Title
        Else
            Try
                Me.Text = Owner.Text
            Catch
                Me.Text = "CustomMsgBox"
            End Try
        End If
        
        If Me.Buttons <> 0 Then
            If Me.Buttons.HasFlag(MsgBoxStyle.Critical) Then
                
            ElseIf Me.Buttons.HasFlag(MsgBoxStyle.Exclamation)
                
            ElseIf Me.Buttons.HasFlag(MsgBoxStyle.Information)
                
            ElseIf Me.Buttons.HasFlag(MsgBoxStyle.Question)
                
            End If
            
            If Me.Buttons.HasFlag(MsgBoxStyle.AbortRetryIgnore) Then
                
            ElseIf Me.Buttons.HasFlag(MsgBoxStyle.RetryCancel)
                
            ElseIf Me.Buttons.HasFlag(MsgBoxStyle.YesNoCancel)
                
            ElseIf Me.Buttons.HasFlag(MsgBoxStyle.YesNo)
                
            ElseIf Me.Buttons.HasFlag(MsgBoxStyle.OkCancel)
                
            ElseIf Me.Buttons.HasFlag(MsgBoxStyle.OkOnly)
                
            End If
        End If
    End Sub
    
    Sub Accept_Click() Handles btnAccept.Click
        Me.DialogResult = DialogResult.Yes
    End Sub
    
    Sub AnswerMid_Click() Handles btnAnswerMid.Click
        Me.DialogResult = DialogResult.No
    End Sub
    
    Sub Cancel_Click() Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub
    
    Sub BtnAnswerMidClick(sender As Object, e As System.EventArgs)
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