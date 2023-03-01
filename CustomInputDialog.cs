using System;
using System.Windows.Forms;

public partial class CustomInputDialog : Form {
    public CustomInputDialog() {
        Application.EnableVisualStyles();
        InitializeComponent();
    }

    public string MainInstruction {
        get => lblMainInstruction.Text;
        set => lblMainInstruction.Text = value;
    }
    public string Content {
        get => lblContent.Text;
        set => lblContent.Text = value;
    }
    public string Input {
        get => inputTextBox.Text;
        set => inputTextBox.Text = value;
    }
    public int MaxLength {
        get => inputTextBox.MaxLength;
        set => inputTextBox.MaxLength = value;
    }
    public bool UsePasswordMasking {
        get => inputTextBox.UseSystemPasswordChar;
        set => inputTextBox.UseSystemPasswordChar = value;
    }

    private void SizeDialog() {
        const int mainInstructionNormalHeight = 25;
        const int contentNormalHeight = 21;
        const int windowNormalHeight = 209;

        int modifier = 0;
        if (string.IsNullOrWhiteSpace(MainInstruction)) {
            modifier -= 36;
            lblContent.Location = new System.Drawing.Point(lblContent.Location.X, lblContent.Location.Y - 35);
        } else if (lblMainInstruction.Height > mainInstructionNormalHeight) {
            modifier = lblMainInstruction.Height - mainInstructionNormalHeight;
            lblContent.Location = new System.Drawing.Point(lblContent.Location.X, lblContent.Location.Y + modifier);
        }
        if (string.IsNullOrWhiteSpace(Content))
            modifier -= 30;
        else if (lblContent.Height > contentNormalHeight)
            modifier += lblContent.Height - contentNormalHeight;

        if (modifier != 0) //                           limit size reduction to 51
            this.Height = windowNormalHeight + Math.Max(modifier, -51);
    }

    private void CustomInputDialog_Load(object _, EventArgs __) {
        SizeDialog();

        if (Owner != null)
            CenterToParent();
        else
            CenterToScreen();
    }
    private void CustomInputDialog_Shown(object _, EventArgs __) {
        if (this.Visible)
            inputTextBox.Focus();
    }

    private void btnOK_Click(object _, EventArgs __) {
        DialogResult = DialogResult.OK;
    }
}

public partial class WalkmanLib {
    public static DialogResult InputDialog(ref string input, string mainInstruction = null, string title = null, string content = null, bool usePasswordMasking = false, int maxLength = short.MaxValue, Form owner = null) {
        var inputForm = new CustomInputDialog() {
            MainInstruction = mainInstruction,
            Content = content,
            Text = title,
            Input = input,
            UsePasswordMasking = usePasswordMasking,
            MaxLength = maxLength,
            Owner = owner,
        };
        var result = inputForm.ShowDialog();
        if (result == DialogResult.OK)
            input = inputForm.Input;
        return result;
    }
}
