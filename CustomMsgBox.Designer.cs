public partial class CustomMsgBoxForm : System.Windows.Forms.Form {

    /// <summary>
    /// Designer variable used to keep track of non-visual components.
    /// </summary>
    private System.ComponentModel.IContainer components;

    /// <summary>
    /// Disposes resources used by the form.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
        if (disposing) {
            if (components != null) {
                components.Dispose();
            }
        }
        base.Dispose(disposing);
    }

    /// <summary>
    /// This method is required for Windows Forms designer support.
    /// Do not change the method contents inside the source code editor. The Forms designer might
    /// not be able to load this method if it was changed manually.
    /// </summary>
    private void InitializeComponent() {
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnAnswerMid = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.txtMain = new System.Windows.Forms.TextBox();
            this.pbxMain = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxMain)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAccept
            // 
            this.btnAccept.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnAccept.AutoSize = true;
            this.btnAccept.Location = new System.Drawing.Point(156, 8);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(75, 23);
            this.btnAccept.TabIndex = 0;
            this.btnAccept.Text = "Yes";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += delegate { this.Accept_Click(); };
            // 
            // btnAnswerMid
            // 
            this.btnAnswerMid.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnAnswerMid.AutoSize = true;
            this.btnAnswerMid.Location = new System.Drawing.Point(237, 8);
            this.btnAnswerMid.Name = "btnAnswerMid";
            this.btnAnswerMid.Size = new System.Drawing.Size(75, 23);
            this.btnAnswerMid.TabIndex = 1;
            this.btnAnswerMid.Text = "No";
            this.btnAnswerMid.UseVisualStyleBackColor = true;
            this.btnAnswerMid.Click += delegate { this.AnswerMid_Click(); };
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCancel.AutoSize = true;
            this.btnCancel.Location = new System.Drawing.Point(318, 8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += delegate { this.Cancel_Click(); };
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer.IsSplitterFixed = true;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.BackColor = System.Drawing.SystemColors.Window;
            this.splitContainer.Panel1.Controls.Add(this.txtMain);
            this.splitContainer.Panel1.Controls.Add(this.pbxMain);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.btnAccept);
            this.splitContainer.Panel2.Controls.Add(this.btnCancel);
            this.splitContainer.Panel2.Controls.Add(this.btnAnswerMid);
            this.splitContainer.Size = new System.Drawing.Size(405, 123);
            this.splitContainer.SplitterDistance = 78;
            this.splitContainer.SplitterWidth = 1;
            this.splitContainer.TabIndex = 0;
            // 
            // txtMain
            // 
            this.txtMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMain.BackColor = System.Drawing.SystemColors.Window;
            this.txtMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMain.Location = new System.Drawing.Point(59, 34);
            this.txtMain.MaximumSize = new System.Drawing.Size(334, 0);
            this.txtMain.MinimumSize = new System.Drawing.Size(334, 13);
            this.txtMain.Multiline = true;
            this.txtMain.Name = "txtMain";
            this.txtMain.ReadOnly = true;
            this.txtMain.Size = new System.Drawing.Size(334, 13);
            this.txtMain.TabIndex = 0;
            this.txtMain.Text = "text";
            // 
            // pbxMain
            // 
            this.pbxMain.Location = new System.Drawing.Point(21, 25);
            this.pbxMain.Name = "pbxMain";
            this.pbxMain.Size = new System.Drawing.Size(32, 32);
            this.pbxMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxMain.TabIndex = 0;
            this.pbxMain.TabStop = false;
            // 
            // CustomMsgBoxForm
            // 
            this.AcceptButton = this.btnAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(405, 123);
            this.Controls.Add(this.splitContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomMsgBoxForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbxMain)).EndInit();
            this.ResumeLayout(false);

    }

    private System.Windows.Forms.TextBox txtMain;
    private System.Windows.Forms.PictureBox pbxMain;
    private System.Windows.Forms.SplitContainer splitContainer;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnAnswerMid;
    private System.Windows.Forms.Button btnAccept;
}
