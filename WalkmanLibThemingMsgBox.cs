using System.Windows.Forms;

public partial class CustomMsgBoxForm {
    public void ApplyTheme(WalkmanLib.Theme theme) {
        WalkmanLib.ApplyTheme(theme, this);
        splitContainer.Panel1.BackColor = theme.CustomMsgBoxTopPanel;
        txtMain.BackColor = theme.CustomMsgBoxTopPanel;
        pbxMain.BackColor = theme.CustomMsgBoxTopPanel;
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
}
