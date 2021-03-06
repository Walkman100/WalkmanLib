Partial Class CustomMsgBoxForm
    Inherits System.Windows.Forms.Form

    ''' <summary>
    ''' Designer variable used to keep track of non-visual components.
    ''' </summary>
    Private components As System.ComponentModel.IContainer

    ''' <summary>
    ''' Disposes resources used by the form.
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
        Me.btnAccept = New System.Windows.Forms.Button()
        Me.btnAnswerMid = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.splitContainer = New System.Windows.Forms.SplitContainer()
        Me.txtMain = New System.Windows.Forms.TextBox()
        Me.pbxMain = New System.Windows.Forms.PictureBox()
        CType(Me.splitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitContainer.Panel1.SuspendLayout()
        Me.splitContainer.Panel2.SuspendLayout()
        Me.splitContainer.SuspendLayout()
        CType(Me.pbxMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnAccept
        '
        Me.btnAccept.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnAccept.AutoSize = True
        Me.btnAccept.Location = New System.Drawing.Point(156, 8)
        Me.btnAccept.Name = "btnAccept"
        Me.btnAccept.Size = New System.Drawing.Size(75, 23)
        Me.btnAccept.TabIndex = 0
        Me.btnAccept.Text = "Yes"
        Me.btnAccept.UseVisualStyleBackColor = True
        '
        'btnAnswerMid
        '
        Me.btnAnswerMid.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnAnswerMid.AutoSize = True
        Me.btnAnswerMid.Location = New System.Drawing.Point(237, 8)
        Me.btnAnswerMid.Name = "btnAnswerMid"
        Me.btnAnswerMid.Size = New System.Drawing.Size(75, 23)
        Me.btnAnswerMid.TabIndex = 1
        Me.btnAnswerMid.Text = "No"
        Me.btnAnswerMid.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnCancel.AutoSize = True
        Me.btnCancel.Location = New System.Drawing.Point(318, 8)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
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
        Me.splitContainer.Panel1.Controls.Add(Me.txtMain)
        Me.splitContainer.Panel1.Controls.Add(Me.pbxMain)
        '
        'splitContainer.Panel2
        '
        Me.splitContainer.Panel2.Controls.Add(Me.btnAccept)
        Me.splitContainer.Panel2.Controls.Add(Me.btnCancel)
        Me.splitContainer.Panel2.Controls.Add(Me.btnAnswerMid)
        Me.splitContainer.Size = New System.Drawing.Size(405, 123)
        Me.splitContainer.SplitterDistance = 78
        Me.splitContainer.SplitterWidth = 1
        Me.splitContainer.TabIndex = 0
        '
        'txtMain
        '
        Me.txtMain.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMain.BackColor = System.Drawing.SystemColors.Window
        Me.txtMain.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtMain.Location = New System.Drawing.Point(59, 34)
        Me.txtMain.MaximumSize = New System.Drawing.Size(334, 0)
        Me.txtMain.MinimumSize = New System.Drawing.Size(334, 13)
        Me.txtMain.Multiline = True
        Me.txtMain.Name = "txtMain"
        Me.txtMain.ReadOnly = True
        Me.txtMain.Size = New System.Drawing.Size(334, 13)
        Me.txtMain.TabIndex = 0
        Me.txtMain.Text = "text"
        '
        'pbxMain
        '
        Me.pbxMain.Location = New System.Drawing.Point(21, 25)
        Me.pbxMain.Name = "pbxMain"
        Me.pbxMain.Size = New System.Drawing.Size(32, 32)
        Me.pbxMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbxMain.TabIndex = 0
        Me.pbxMain.TabStop = False
        '
        'CustomMsgBoxForm
        '
        Me.AcceptButton = Me.btnAccept
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(405, 123)
        Me.Controls.Add(Me.splitContainer)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "CustomMsgBoxForm"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.splitContainer.Panel1.ResumeLayout(False)
        Me.splitContainer.Panel1.PerformLayout()
        Me.splitContainer.Panel2.ResumeLayout(False)
        Me.splitContainer.Panel2.PerformLayout()
        CType(Me.splitContainer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitContainer.ResumeLayout(False)
        CType(Me.pbxMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
    End Sub
    Private txtMain As System.Windows.Forms.TextBox
    Private pbxMain As System.Windows.Forms.PictureBox
    Private splitContainer As System.Windows.Forms.SplitContainer
    Private WithEvents btnCancel As System.Windows.Forms.Button
    Private WithEvents btnAnswerMid As System.Windows.Forms.Button
    Private WithEvents btnAccept As System.Windows.Forms.Button
End Class
