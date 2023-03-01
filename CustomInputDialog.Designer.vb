Partial Class CustomInputDialog
    Inherits System.Windows.Forms.Form

    ''' <summary>
    ''' Designer variable used to keep track of non-visual components.
    ''' </summary>
    Private components As System.ComponentModel.IContainer = Nothing

    ''' <summary>
    ''' Disposes resources used by the form
    ''' </summary>
    ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If components IsNot Nothing Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    ''' <summary>
    ''' This method is required for Windows Forms designer support.
    ''' Do not change the method contents inside the source code editor. The Forms designer might
    ''' not be able to load this method if it was changed manually.
    ''' </summary>
    Private Sub InitializeComponent()
        Me.inputTextBox = New System.Windows.Forms.TextBox()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.splitContainer = New System.Windows.Forms.SplitContainer()
        Me.lblContent = New System.Windows.Forms.Label()
        Me.lblMainInstruction = New System.Windows.Forms.Label()
        CType(Me.splitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitContainer.Panel1.SuspendLayout()
        Me.splitContainer.Panel2.SuspendLayout()
        Me.splitContainer.SuspendLayout()
        Me.SuspendLayout()
        '
        'inputTextBox
        '
        Me.inputTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.inputTextBox.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.inputTextBox.Location = New System.Drawing.Point(12, 78)
        Me.inputTextBox.Name = "inputTextBox"
        Me.inputTextBox.Size = New System.Drawing.Size(381, 23)
        Me.inputTextBox.TabIndex = 2
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnCancel.AutoSize = True
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnCancel.Location = New System.Drawing.Point(304, 13)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(88, 23)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnOK.AutoSize = True
        Me.btnOK.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnOK.Location = New System.Drawing.Point(209, 13)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(88, 23)
        Me.btnOK.TabIndex = 0
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'DotNetCore fixes
        '
#If NETCOREAPP Then
        Me.inputTextBox.Location = New System.Drawing.Point(12, 101)
        Me.inputTextBox.Size = New System.Drawing.Size(436, 23)
        Me.btnCancel.Location = New System.Drawing.Point(362, 17)
        Me.btnOK.Location = New System.Drawing.Point(267, 17)
#End If
        '
        'splitContainer
        '
        Me.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.splitContainer.IsSplitterFixed = True
        Me.splitContainer.Location = New System.Drawing.Point(0, 0)
        Me.splitContainer.Name = "splitContainer"
        Me.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splitContainer.Panel1
        '
        Me.splitContainer.Panel1.BackColor = System.Drawing.SystemColors.Window
        Me.splitContainer.Panel1.Controls.Add(Me.inputTextBox)
        Me.splitContainer.Panel1.Controls.Add(Me.lblContent)
        Me.splitContainer.Panel1.Controls.Add(Me.lblMainInstruction)
        '
        'splitContainer.Panel2
        '
        Me.splitContainer.Panel2.Controls.Add(Me.btnCancel)
        Me.splitContainer.Panel2.Controls.Add(Me.btnOK)
        Me.splitContainer.Size = New System.Drawing.Size(405, 170)
        Me.splitContainer.SplitterDistance = 115
        Me.splitContainer.SplitterWidth = 1
        Me.splitContainer.TabIndex = 0
        '
        'lblContent
        '
        Me.lblContent.AutoSize = True
        Me.lblContent.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblContent.Location = New System.Drawing.Point(12, 45)
        Me.lblContent.MaximumSize = New System.Drawing.Size(384, 0)
        Me.lblContent.MinimumSize = New System.Drawing.Size(384, 0)
        Me.lblContent.Name = "lblContent"
        Me.lblContent.Size = New System.Drawing.Size(384, 15)
        Me.lblContent.TabIndex = 1
        '
        'lblMainInstruction
        '
        Me.lblMainInstruction.AutoSize = True
        Me.lblMainInstruction.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.lblMainInstruction.ForeColor = System.Drawing.Color.FromArgb(0, 51, 153)
        Me.lblMainInstruction.Location = New System.Drawing.Point(12, 11)
        Me.lblMainInstruction.MaximumSize = New System.Drawing.Size(381, 0)
        Me.lblMainInstruction.MinimumSize = New System.Drawing.Size(381, 0)
        Me.lblMainInstruction.Name = "lblMainInstruction"
        Me.lblMainInstruction.Size = New System.Drawing.Size(381, 20)
        Me.lblMainInstruction.TabIndex = 0
        '
        'CustomInputDialog
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(405, 170)
        Me.Controls.Add(Me.splitContainer)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "CustomInputDialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "CustomInputDialog"
        Me.splitContainer.Panel1.ResumeLayout(False)
        Me.splitContainer.Panel1.PerformLayout()
        Me.splitContainer.Panel2.ResumeLayout(False)
        Me.splitContainer.Panel2.PerformLayout()
        CType(Me.splitContainer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitContainer.ResumeLayout(False)
        Me.ResumeLayout(False)
    End Sub
    Private lblMainInstruction As System.Windows.Forms.Label
    Private lblContent As System.Windows.Forms.Label
    Private inputTextBox As System.Windows.Forms.TextBox
    Private splitContainer As System.Windows.Forms.SplitContainer
    Private WithEvents btnOK As System.Windows.Forms.Button
    Private btnCancel As System.Windows.Forms.Button
End Class
