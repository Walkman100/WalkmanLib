Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports System.Windows.Forms

Partial Public Class CustomInputDialog
    Public Sub New()
        Application.EnableVisualStyles()
        ' The Me.InitializeComponent call is required for Windows Forms designer support.
        Me.InitializeComponent()
    End Sub

    Public Property MainInstruction As String
        Get
            Return lblMainInstruction.Text
        End Get
        Set(value As String)
            lblMainInstruction.Text = value
        End Set
    End Property
    Public Property Content As String
        Get
            Return lblContent.Text
        End Get
        Set(value As String)
            lblContent.Text = value
        End Set
    End Property
    Public Property Input As String
        Get
            Return inputTextBox.Text
        End Get
        Set(value As String)
            inputTextBox.Text = value
        End Set
    End Property
    Public Property MaxLength As Integer
        Get
            Return inputTextBox.MaxLength
        End Get
        Set(value As Integer)
            inputTextBox.MaxLength = value
        End Set
    End Property
    Public Property UsePasswordMasking As Boolean
        Get
            Return inputTextBox.UseSystemPasswordChar
        End Get
        Set(value As Boolean)
            inputTextBox.UseSystemPasswordChar = value
        End Set
    End Property

    Private Sub SizeDialog()
        Const mainInstructionNormalHeight As Integer = 25
        Const contentNormalHeight As Integer = 21
        Const windowNormalHeight As Integer = 209

        Dim modifier As Integer = 0
        If String.IsNullOrWhiteSpace(MainInstruction) Then
            modifier -= 36
            lblContent.Location = New Drawing.Point(lblContent.Location.X, lblContent.Location.Y - 35)
        ElseIf lblMainInstruction.Height > mainInstructionNormalHeight Then
            modifier = lblMainInstruction.Height - mainInstructionNormalHeight
            lblContent.Location = New Drawing.Point(lblContent.Location.X, lblContent.Location.Y + modifier)
        End If
        If String.IsNullOrWhiteSpace(Content) Then
            modifier -= 30
        ElseIf lblContent.Height > contentNormalHeight Then
            modifier += lblContent.Height - contentNormalHeight
        End If

        If modifier <> 0 Then '                    limit size reduction to 51
            Height = windowNormalHeight + Math.Max(modifier, -51)
        End If
    End Sub

    Private Sub CustomInputDialog_Load() Handles Me.Load
        SizeDialog()

        If Me.Owner IsNot Nothing Then
            Me.CenterToParent()
        Else
            Me.CenterToScreen()
        End If
    End Sub
    Private Sub CustomInputDialog_Shown() Handles Me.Shown
        If Me.Visible Then
            inputTextBox.Focus()
        End If
    End Sub

    Private Sub btnOK_Click() Handles btnOK.Click
        DialogResult = DialogResult.OK
    End Sub
End Class

Partial Public Class WalkmanLib
    Public Shared Function InputDialog(ByRef input As String, Optional mainInstruction As String = Nothing, Optional title As String = Nothing, Optional content As String = Nothing,
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
        Dim result As DialogResult = inputForm.ShowDialog()
        If result = DialogResult.OK Then input = inputForm.Input

        Return result
    End Function
End Class
