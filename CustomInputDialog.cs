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

    private void CustomInputDialog_Load(object sendder, EventArgs e) {
        CenterToScreen();
    }

    private void btnOK_Click(object sender, EventArgs e) {
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
