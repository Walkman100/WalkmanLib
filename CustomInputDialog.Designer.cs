partial class CustomInputDialog {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
        if (disposing && (components != null)) {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
        this.inputTextBox = new System.Windows.Forms.TextBox();
        this.btnCancel = new System.Windows.Forms.Button();
        this.btnOK = new System.Windows.Forms.Button();
        this.splitContainer = new System.Windows.Forms.SplitContainer();
        this.lblContent = new System.Windows.Forms.Label();
        this.lblMainInstruction = new System.Windows.Forms.Label();
        ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
        this.splitContainer.Panel1.SuspendLayout();
        this.splitContainer.Panel2.SuspendLayout();
        this.splitContainer.SuspendLayout();
        this.SuspendLayout();
        // 
        // inputTextBox
        // 
        this.inputTextBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        this.inputTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.inputTextBox.Location = new System.Drawing.Point(12, 78);
        this.inputTextBox.Name = "inputTextBox";
        this.inputTextBox.Size = new System.Drawing.Size(381, 23);
        this.inputTextBox.TabIndex = 2;
        // 
        // btnCancel
        // 
        this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Right;
        this.btnCancel.AutoSize = true;
        this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.btnCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
        this.btnCancel.Location = new System.Drawing.Point(304, 13);
        this.btnCancel.Name = "btnCancel";
        this.btnCancel.Size = new System.Drawing.Size(88, 23);
        this.btnCancel.TabIndex = 1;
        this.btnCancel.Text = "Cancel";
        this.btnCancel.UseVisualStyleBackColor = true;
        // 
        // btnOK
        // 
        this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Right;
        this.btnOK.AutoSize = true;
        this.btnOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
        this.btnOK.Location = new System.Drawing.Point(209, 13);
        this.btnOK.Name = "btnOK";
        this.btnOK.Size = new System.Drawing.Size(88, 23);
        this.btnOK.TabIndex = 0;
        this.btnOK.Text = "OK";
        this.btnOK.UseVisualStyleBackColor = true;
        this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
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
        this.splitContainer.Panel1.Controls.Add(this.inputTextBox);
        this.splitContainer.Panel1.Controls.Add(this.lblContent);
        this.splitContainer.Panel1.Controls.Add(this.lblMainInstruction);
        // 
        // splitContainer.Panel2
        // 
        this.splitContainer.Panel2.Controls.Add(this.btnCancel);
        this.splitContainer.Panel2.Controls.Add(this.btnOK);
        this.splitContainer.Size = new System.Drawing.Size(405, 170);
        this.splitContainer.SplitterDistance = 115;
        this.splitContainer.SplitterWidth = 1;
        this.splitContainer.TabIndex = 0;
        // 
        // lblContent
        // 
        this.lblContent.AutoSize = true;
        this.lblContent.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.lblContent.Location = new System.Drawing.Point(12, 45);
        this.lblContent.MaximumSize = new System.Drawing.Size(384, 0);
        this.lblContent.MinimumSize = new System.Drawing.Size(384, 0);
        this.lblContent.Name = "lblContent";
        this.lblContent.Size = new System.Drawing.Size(384, 15);
        this.lblContent.TabIndex = 1;
        // 
        // lblMainInstruction
        // 
        this.lblMainInstruction.AutoSize = true;
        this.lblMainInstruction.Font = new System.Drawing.Font("Segoe UI", 11F);
        this.lblMainInstruction.ForeColor = System.Drawing.Color.FromArgb(0, 51, 153);
        this.lblMainInstruction.Location = new System.Drawing.Point(12, 11);
        this.lblMainInstruction.MaximumSize = new System.Drawing.Size(381, 0);
        this.lblMainInstruction.MinimumSize = new System.Drawing.Size(381, 0);
        this.lblMainInstruction.Name = "lblMainInstruction";
        this.lblMainInstruction.Size = new System.Drawing.Size(381, 20);
        this.lblMainInstruction.TabIndex = 0;
        // 
        // CustomInputDialog
        // 
        this.AcceptButton = this.btnOK;
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.CancelButton = this.btnCancel;
        this.ClientSize = new System.Drawing.Size(405, 170);
        this.Controls.Add(this.splitContainer);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "CustomInputDialog";
        this.ShowInTaskbar = false;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "CustomInputDialog";
        this.Load += new System.EventHandler(this.CustomInputDialog_Load);
        this.splitContainer.Panel1.ResumeLayout(false);
        this.splitContainer.Panel1.PerformLayout();
        this.splitContainer.Panel2.ResumeLayout(false);
        this.splitContainer.Panel2.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
        this.splitContainer.ResumeLayout(false);
        this.ResumeLayout(false);
    }
    #endregion

    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.TextBox inputTextBox;
    private System.Windows.Forms.SplitContainer splitContainer;
    private System.Windows.Forms.Label lblMainInstruction;
    private System.Windows.Forms.Label lblContent;
}
