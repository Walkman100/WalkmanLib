using System;
using System.Windows.Forms;

public enum WinVersionStyle {
    WinXP,
    Win7,
    Win10
}

public partial class CustomMsgBoxForm {
    public CustomMsgBoxForm() {
        this.Shown += (_, _) => MeShown();
        Application.EnableVisualStyles();
        // The Me.InitializeComponent call is required for Windows Forms designer support.
        InitializeComponent();
    }

    // properties

    public string Prompt {
        get {
            return txtMain.Text;
        }
        set {
            txtMain.Text = value;
        }
    }

    public string Title {
        get {
            return Text;
        }
        set {
            Text = value;
        }
    }

    public TextBox MainText {
        get {
            return txtMain;
        }
    }

    public MessageBoxIcon FormLevel { get; set; }
    private string getFormLevelString() {
        return FormLevel switch {
            MessageBoxIcon.Error => "Error",
            MessageBoxIcon.Question => "Question",
            MessageBoxIcon.Exclamation => "Exclamation",
            MessageBoxIcon.Information => "Information",
            _ => ""
        };
    }

    public string Button1Text = null;
    public string Button2Text = null;
    public string Button3Text = null;
    public WinVersionStyle WinVersion = WinVersionStyle.Win10;
    public string DialogResultString = null;

    public MessageBoxButtons Buttons {
        set {
            switch (value) {
                case MessageBoxButtons.OK: {
                    Button1Text = "Ok";
                    break;
                }
                case MessageBoxButtons.OKCancel: {
                    Button1Text = "Ok";
                    Button3Text = "Cancel";
                    break;
                }
                case MessageBoxButtons.AbortRetryIgnore: {
                    Button1Text = "Abort";
                    Button2Text = "Retry";
                    Button3Text = "Ignore";
                    break;
                }
                case MessageBoxButtons.YesNoCancel: {
                    Button1Text = "Yes";
                    Button2Text = "No";
                    Button3Text = "Cancel";
                    break;
                }
                case MessageBoxButtons.YesNo: {
                    Button1Text = "Yes";
                    Button3Text = "No";
                    break;
                }
                case MessageBoxButtons.RetryCancel: {
                    Button1Text = "Retry";
                    Button3Text = "Cancel";
                    break;
                }
            }
        }
    }

    private void MeShown() {
        if (this.Text == null) {
            try {
                this.Text = Owner.Text;
            } catch {
                this.Text = "CustomMsgBox";
            }
        }

        var resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomMsgBoxForm));
        this.Icon = (System.Drawing.Icon)resources.GetObject(WinVersion.ToString() + "_" + getFormLevelString());
        if (FormLevel != MessageBoxIcon.None) pbxMain.Image = this.Icon.ToBitmap();
        try {
            this.Icon = Owner.Icon;
        } catch {
            // this.Icon = (System.Drawing.Image)pbxMain.Image
            // doesn't work, and it's already set above anyway
            if (FormLevel == MessageBoxIcon.None) {
                this.ShowIcon = false;
            }
        }

        switch (FormLevel) {
            case MessageBoxIcon.Error:
                System.Media.SystemSounds.Beep.Play(); break;
            case MessageBoxIcon.Exclamation:
                System.Media.SystemSounds.Exclamation.Play(); break;
            case MessageBoxIcon.Information:
                System.Media.SystemSounds.Asterisk.Play(); break;
            case MessageBoxIcon.Question:
                System.Media.SystemSounds.Question.Play(); break;
        }

        // as TextBox doesn't have an AutoSize property like Label does, we have to do it manually
        using (System.Drawing.Graphics g = txtMain.CreateGraphics()) {
            System.Drawing.SizeF sizeF = g.MeasureString(txtMain.Text, txtMain.Font, new System.Drawing.SizeF(txtMain.MaximumSize.Width, float.MaxValue));
            txtMain.Height = (int)Math.Round(sizeF.Height / txtMain.Font.Height) * txtMain.Font.Height; // restrain to line height
        }

        if (txtMain.Height > 13) {
            this.Height = 162 + (txtMain.Height - 13);
        }

        if (Button1Text != null) {
            btnAccept.Text = Button1Text;
            btnAccept.Visible = true;
        } else {
            btnAccept.Visible = false;
        }
        if (Button2Text != null) {
            btnAnswerMid.Text = Button2Text;
            btnAnswerMid.Visible = true;
            if (btnAnswerMid.Width > 75) { // move btnAccept to the left, as btnAnswerMid is anchored right
                btnAccept.Location = new System.Drawing.Point(btnAccept.Location.X - (btnAnswerMid.Width - 75), btnAccept.Location.Y);
            }
        } else {
            btnAnswerMid.Visible = false;
        }
        if (Button3Text != null) {
            btnCancel.Text = Button3Text;
            btnCancel.Visible = true;
            if (btnCancel.Width > 75) { // move the other two buttons to the left, as btnCancel is anchored right
                btnAnswerMid.Location = new System.Drawing.Point(btnAnswerMid.Location.X - (btnCancel.Width - 75), btnAnswerMid.Location.Y);
                btnAccept.Location = new System.Drawing.Point(btnAccept.Location.X - (btnCancel.Width - 75), btnAccept.Location.Y);
            }
        } else {
            btnCancel.Visible = false;
        }

        if (this.Owner != null) {
            this.CenterToParent();
        } else {
            this.CenterToScreen();
        }
        btnAccept.Select();
    }

    private DialogResult GetDialogResult(string buttonText) =>
        Enum.TryParse(buttonText, true, out DialogResult result) ? result : DialogResult.None;

    private void Accept_Click() {
        this.DialogResult = GetDialogResult(Button1Text);
        DialogResultString = Button1Text; // for use with custom buttons
        if (GetDialogResult(Button1Text) == DialogResult.None) {
            this.Close();
        }
    }

    private void AnswerMid_Click() {
        this.DialogResult = GetDialogResult(Button2Text);
        DialogResultString = Button2Text;
        if (GetDialogResult(Button2Text) == DialogResult.None) {
            this.Close();
        }
    }

    private void Cancel_Click() {
        this.DialogResult = GetDialogResult(Button3Text);
        DialogResultString = Button3Text;
        if (GetDialogResult(Button3Text) == DialogResult.None) {
            this.Close();
        }
    }
}

public partial class WalkmanLib {
    /// <summary>Shows a custom messagebox</summary>
    /// <param name="text">The text to display in the message box.</param>
    /// <param name="caption">The text to display in the title bar of the message box.</param>
    /// <param name="buttons">One of the <see cref="MessageBoxButtons"/> values that specifies which buttons to display in the message box.</param>
    /// <param name="style">One of the following: <see cref="MessageBoxIcon.Error"/>, <see cref="MessageBoxIcon.Question"/>, <see cref="MessageBoxIcon.Exclamation"/> or <see cref="MessageBoxIcon.Information"/>.</param>
    /// <param name="winVersion">Windows version to use style icons from. Default: <see cref="WinVersionStyle.Win10"/></param>
    /// <param name="ownerForm">Used to set the Window's Icon. Set to <see langword="this"/> to copy the current form's icon</param>
    /// <returns>The button the user clicked on.</returns>
    public static DialogResult CustomMsgBox(string text, string caption = null, MessageBoxButtons buttons = 0, MessageBoxIcon style = 0, 
                                            WinVersionStyle winVersion = WinVersionStyle.Win10, Form ownerForm = null) {
        var formToShow = new CustomMsgBoxForm() {
            Prompt = text,
            Title = caption,
            Buttons = buttons,
            FormLevel = style,
            WinVersion = winVersion,
            Owner = ownerForm,
            ShowInTaskbar = false
        };
        return formToShow.ShowDialog();
    }

    /// <summary>Shows a custom messagebox with custom buttons</summary>
    /// <param name="text">The text to display in the message box.</param>
    /// <param name="caption">The text to display in the title bar of the message box.</param>
    /// <param name="customButton1">Text to show on the first button</param>
    /// <param name="customButton2">Text to show on the second button. If left out or set to <see langword="null"/>, this button will be hidden.</param>
    /// <param name="customButton3">Text to show on the third button. If left out or set to <see langword="null"/>, this button will be hidden.</param>
    /// <param name="style">One of the following: <see cref="MessageBoxIcon.Error"/>, <see cref="MessageBoxIcon.Question"/>, <see cref="MessageBoxIcon.Exclamation"/> or <see cref="MessageBoxIcon.Information"/>.</param>
    /// <param name="winVersion">Windows version to use style icons from. Default: <see cref="WinVersionStyle.Win10"/></param>
    /// <param name="ownerForm">Used to set the Window's Icon. Set to <see langword="this"/> to copy the current form's icon</param>
    /// <returns>Text of the button the user clicked on.</returns>
    public static string CustomMsgBox(string text, string caption, string customButton1, string customButton2 = null, string customButton3 = null, 
                                      MessageBoxIcon style = 0, WinVersionStyle winVersion = WinVersionStyle.Win10, Form ownerForm = null) {
        var formToShow = new CustomMsgBoxForm() {
            Prompt = text,
            Title = caption,
            Button1Text = customButton1,
            Button2Text = customButton2,
            Button3Text = customButton3,
            FormLevel = style,
            WinVersion = winVersion,
            Owner = ownerForm,
            ShowInTaskbar = false
        };

        formToShow.ShowDialog();
        return formToShow.DialogResultString;
    }
}

// visual styles is enabled in a Windows Forms Application with the following:
//namespace My {
//    internal class MyApplication : Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase {
//        public MyApplication() : base(Microsoft.VisualBasic.ApplicationServices.AuthenticationMode.Windows) {
//            base.EnableVisualStyles = true;
//        }
//        protected override void OnCreateMainForm() {
//            base.MainForm = MyProject.Forms.CustomMsgBoxForm;
//        }
//    }
//}
