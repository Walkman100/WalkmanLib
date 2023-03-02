using System;
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

    public static byte CustomMsgBoxBTN(string text, Theme theme, string caption, string customButton1, string customButton2 = null, string customButton3 = null,
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

        return formToShow.DialogResultButtonPressed;
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

    public static void ErrorDialog(Exception ex, Theme theme, string errorMessage = "There was an error! Error message: ", bool showMsgBox = true, Form ownerForm = null, bool? showErrorBlockingWindow = null) {
        Application.EnableVisualStyles(); // affects when in a console app
        if (showMsgBox && WalkmanLib.CustomMsgBox($"{errorMessage}{ex.Message}{Environment.NewLine}Show full stacktrace? (For sending to developer/making bugreport)",
                                                  theme, "Error!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, ownerForm: ownerForm) != DialogResult.Yes)
            return;

        var frmBugReport = new Form() {
            Width = 600,
            Height = 525,
            StartPosition = FormStartPosition.CenterParent,
            ShowIcon = false,
            Text = "Full error trace"
        };
        var txtBugReport = new TextBox() {
            Multiline = true,
            ScrollBars = ScrollBars.Vertical,
            Dock = DockStyle.Fill
        };
        frmBugReport.Controls.Add(txtBugReport);
        WalkmanLib.ApplyTheme(theme, frmBugReport);

        try {
            txtBugReport.Text = "";
            while (ex != null) {
                if (ex.ToString() != null)          txtBugReport.Text += $"ToString:{Environment.NewLine}{ex}{Environment.NewLine}{Environment.NewLine}";
                if (ex.GetBaseException() != null)  txtBugReport.Text += $"BaseException:{Environment.NewLine}{ex.GetBaseException()}{Environment.NewLine}{Environment.NewLine}";
                if (ex.GetType() != null)           txtBugReport.Text += $"Type: {ex.GetType()}{Environment.NewLine}";
                if (ex.Message != null)             txtBugReport.Text += $"Message: {ex.Message}{Environment.NewLine}{Environment.NewLine}";
                if (ex.StackTrace != null)          txtBugReport.Text += $"StackTrace:{Environment.NewLine}{ex.StackTrace}{Environment.NewLine}{Environment.NewLine}";
                if (ex is System.ComponentModel.Win32Exception win32ex) {
                                                    txtBugReport.Text += $"ErrorCode: 0x{win32ex.ErrorCode.ToString("X")}{Environment.NewLine}";
                                                    txtBugReport.Text += $"NativeErrorCode: 0x{win32ex.NativeErrorCode.ToString("X")}{Environment.NewLine}";
                }
                if (ex is System.IO.FileNotFoundException fileNotFoundEx) {
                                                    txtBugReport.Text += $"FileName: {fileNotFoundEx.FileName}{Environment.NewLine}";
                                                    txtBugReport.Text += $"FusionLog: {fileNotFoundEx.FusionLog}{Environment.NewLine}";
                }
                if (ex.Source != null)              txtBugReport.Text += $"Source: {ex.Source}{Environment.NewLine}";
                if (ex.TargetSite != null)          txtBugReport.Text += $"TargetSite: {ex.TargetSite}{Environment.NewLine}";
                                                    txtBugReport.Text += $"HashCode: 0x{ex.GetHashCode().ToString("X")}{Environment.NewLine}";
                                                    txtBugReport.Text += $"HResult: 0x{ex.HResult.ToString("X")}{Environment.NewLine}{Environment.NewLine}";
                foreach (object key in ex.Data.Keys) {
                                                    txtBugReport.Text += $"Data({key}): {ex.Data[key]}{Environment.NewLine}";
                }
                if (ex.InnerException != null)      txtBugReport.Text += $"{Environment.NewLine}InnerException:{Environment.NewLine}";
                ex = ex.InnerException;
            }
        } catch (Exception ex2) {
            txtBugReport.Text += $"Error getting exception data!{Environment.NewLine}{Environment.NewLine}{ex2}";
        }

        try {
            if (showErrorBlockingWindow.HasValue && showErrorBlockingWindow.Value)
                frmBugReport.ShowDialog();
            else if (showErrorBlockingWindow.HasValue)
                frmBugReport.Show();
            else
                System.Threading.Tasks.Task.Run(() => frmBugReport.ShowDialog());
        } catch (Exception ex2) {
            MessageBox.Show($"Error showing window: {ex2}", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}
