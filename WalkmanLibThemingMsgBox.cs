using System.Windows.Forms;

public partial class CustomMsgBoxForm {
    public void ApplyTheme(WalkmanLib.Theme theme) {
        WalkmanLib.ApplyTheme(theme, this);
        splitContainer.Panel1.BackColor = theme.CustomMsgBoxTopPanel;
        txtMain.BackColor = theme.CustomMsgBoxTopPanel;
        pbxMain.BackColor = theme.CustomMsgBoxTopPanel;
    }
}

public partial class CustomInputDialog {
    public void ApplyTheme(WalkmanLib.Theme theme) {
        WalkmanLib.ApplyTheme(theme, this);
        splitContainer.Panel1.BackColor = theme.CustomMsgBoxTopPanel;
        lblMainInstruction.BackColor = theme.CustomMsgBoxTopPanel;
        lblContent.BackColor = theme.CustomMsgBoxTopPanel;

        lblMainInstruction.ForeColor = theme.DialogHeadingText;
    }
}

public partial class WalkmanLib {
    public static DialogResult CustomMsgBox(string text, Theme theme, string caption = null, MessageBoxButtons buttons = 0, MessageBoxIcon style = 0,
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
        formToShow.ApplyTheme(theme);
        return formToShow.ShowDialog();
    }

    public static string CustomMsgBox(string text, Theme theme, string caption, string customButton1, string customButton2 = null, string customButton3 = null,
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
        formToShow.ApplyTheme(theme);
        formToShow.ShowDialog();

        return formToShow.DialogResultString;
    }

    public static DialogResult InputDialog(ref string input, Theme theme, string mainInstruction = null, string title = null, string content = null,
                                           bool usePasswordMasking = false, int maxLength = short.MaxValue, Form owner = null) {
        var inputForm = new CustomInputDialog() {
            MainInstruction = mainInstruction,
            Content = content,
            Text = title,
            Input = input,
            UsePasswordMasking = usePasswordMasking,
            MaxLength = maxLength,
            Owner = owner,
        };
        inputForm.ApplyTheme(theme);
        var result = inputForm.ShowDialog();
        if (result == DialogResult.OK)
            input = inputForm.Input;

        return result;
    }
}
