using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#if NETCOREAPP
using System.Text.Json;
#else
// add a reference to System.Web.Extensions
using System.Web.Script.Serialization;
#endif

public partial class WalkmanLib {
    #region ApplyTheme
    public static void ApplyTheme(Theme theme, Form form, bool allowSetOwnerDraw = false, bool allowSetFlatStyle = true) {
        form.ForeColor = theme.FormFG;
        form.BackColor = theme.FormBG;
        ApplyTheme(theme, form.Controls, allowSetOwnerDraw, allowSetFlatStyle);
    }
    public static void ApplyTheme(Theme theme, System.Collections.IEnumerable controls, bool allowSetOwnerDraw = false, bool allowSetFlatStyle = true) {
        //                             for ToolStripItemCollection
        foreach (ToolStripItem item in controls.OfType<ToolStripItem>()) {
            Type type = item.GetType();
            if (type == typeof(ToolStripButton)) {
                item.ForeColor = theme.ToolStripButtonFG;
                item.BackColor = theme.ToolStripButtonBG;
            } else if (type == typeof(ToolStripComboBox)) {
                item.ForeColor = theme.ToolStripComboBoxFG;
                item.BackColor = theme.ToolStripComboBoxBG;
                if (allowSetFlatStyle)
                    ((ToolStripComboBox)item).FlatStyle = theme.ToolStripComboBoxFlatStyle;
            } else if (type == typeof(ToolStripDropDownButton)) {
                item.ForeColor = theme.ToolStripDropDownButtonFG;
                item.BackColor = theme.ToolStripDropDownButtonBG;
                ((ToolStripDropDownButton)item).DropDown.ForeColor = theme.ToolStripDropDownFG;
                ((ToolStripDropDownButton)item).DropDown.BackColor = theme.ToolStripDropDownBG;
                ApplyTheme(theme, ((ToolStripDropDownButton)item).DropDownItems, allowSetOwnerDraw, allowSetFlatStyle);
            } else if (type == typeof(ToolStripMenuItem)) { // inherits ToolStripDropDownItem
                item.ForeColor = theme.ToolStripMenuItemFG;
                item.BackColor = theme.ToolStripMenuItemBG;
                ((ToolStripMenuItem)item).DropDown.ForeColor = theme.ToolStripDropDownFG;
                ((ToolStripMenuItem)item).DropDown.BackColor = theme.ToolStripDropDownBG;
                ApplyTheme(theme, ((ToolStripMenuItem)item).DropDownItems, allowSetOwnerDraw, allowSetFlatStyle);
            } else if (type == typeof(ToolStripProgressBar)) {
                item.ForeColor = theme.ToolStripProgressBarFG;
                item.BackColor = theme.ToolStripProgressBarBG;
            } else if (type == typeof(ToolStripSeparator)) {
                item.ForeColor = theme.ToolStripSeparatorFG;
                item.BackColor = theme.ToolStripSeparatorBG;
            } else if (type == typeof(ToolStripStatusLabel)) {
                item.ForeColor = theme.ToolStripStatusLabelFG;
                item.BackColor = theme.ToolStripStatusLabelBG;
            } else if (type == typeof(ToolStripSplitButton)) {
                item.ForeColor = theme.ToolStripSplitButtonFG;
                item.BackColor = theme.ToolStripSplitButtonBG;
                ((ToolStripSplitButton)item).DropDown.ForeColor = theme.ToolStripDropDownFG;
                ((ToolStripSplitButton)item).DropDown.BackColor = theme.ToolStripDropDownBG;
                ApplyTheme(theme, ((ToolStripSplitButton)item).DropDownItems, allowSetOwnerDraw, allowSetFlatStyle);
            } else if (type == typeof(ToolStripTextBox)) {
                item.ForeColor = theme.ToolStripTextBoxFG;
                item.BackColor = theme.ToolStripTextBoxBG;
            } else {
                item.ForeColor = theme.OtherFG;
                item.BackColor = theme.OtherBG;
            }
        }

        foreach (Control ctl in controls.OfType<Control>()) {
            Type type = ctl.GetType();
            if (type == typeof(Button)) {
                ctl.ForeColor = theme.ButtonFG;
                ctl.BackColor = theme.ButtonBG;
                var btn = (Button)ctl;
                if (theme.ButtonBG == SystemColors.Control)
                    btn.UseVisualStyleBackColor = true;
                if (allowSetFlatStyle) {
                    btn.FlatStyle = theme.ButtonFlatStyle;
                    btn.FlatAppearance.MouseOverBackColor = theme.ButtonFlatMouseOver;
                    btn.FlatAppearance.MouseDownBackColor = theme.ButtonFlatMouseDown;
                }
            } else if (type == typeof(Label)) {
                ctl.ForeColor = theme.LabelFG;
                ctl.BackColor = theme.LabelBG;
            } else if (type == typeof(LinkLabel)) {
                ctl.ForeColor = theme.LinkLabelFG;
                ctl.BackColor = theme.LinkLabelBG;
                ((LinkLabel)ctl).LinkColor = theme.LinkLabelLinkColor;
                ((LinkLabel)ctl).VisitedLinkColor = theme.LinkLabelVisitedLinkColor;
                ((LinkLabel)ctl).ActiveLinkColor = theme.LinkLabelActiveLinkColor;
            } else if (type == typeof(ComboBox)) {
                ctl.ForeColor = theme.ComboBoxFG;
                ctl.BackColor = theme.ComboBoxBG;
                if (allowSetFlatStyle)
                    ((ComboBox)ctl).FlatStyle = theme.ComboBoxFlatStyle;
            } else if (type == typeof(CheckBox)) {
                ctl.ForeColor = theme.CheckBoxFG;
                ctl.BackColor = theme.CheckBoxBG;
            } else if (type == typeof(RadioButton)) {
                ctl.ForeColor = theme.RadioButtonFG;
                ctl.BackColor = theme.RadioButtonBG;
            } else if (type == typeof(TextBox)) {
                if (((TextBox)ctl).BorderStyle == BorderStyle.None) {
                    ctl.ForeColor = theme.LabelFG;
                    ctl.BackColor = theme.LabelBG;
                } else if (((TextBox)ctl).ReadOnly) {
                    ctl.ForeColor = theme.TextBoxReadOnlyFG;
                    ctl.BackColor = theme.TextBoxReadOnlyBG;
                } else {
                    ctl.ForeColor = theme.TextBoxFG;
                    ctl.BackColor = theme.TextBoxBG;
                }
            } else if (type == typeof(NumericUpDown)) {
                ctl.ForeColor = theme.NumericUpDownFG;
                ctl.BackColor = theme.NumericUpDownBG;
            } else if (type == typeof(DomainUpDown)) {
                ctl.ForeColor = theme.DomainUpDownFG;
                ctl.BackColor = theme.DomainUpDownBG;
            } else if (type == typeof(ListView)) {
                ctl.ForeColor = theme.ListViewFG;
                ctl.BackColor = theme.ListViewBG;
                if (allowSetOwnerDraw) { // https://stackoverflow.com/a/17464722/2999220
                    var bm = new Bitmap(ctl.ClientSize.Width, ctl.ClientSize.Height);
                    Graphics.FromImage(bm).Clear(ctl.BackColor);
                    ctl.BackgroundImage = bm;

                    ((ListView)ctl).OwnerDraw = theme.ListViewOwnerDraw;
                    ((ListView)ctl).GridLines = !theme.ListViewOwnerDraw;
                }
            } else if (type == typeof(ListBox)) {
                ctl.ForeColor = theme.ListBoxFG;
                ctl.BackColor = theme.ListBoxBG;
            } else if (type == typeof(CheckedListBox)) {
                ctl.ForeColor = theme.CheckedListBoxFG;
                ctl.BackColor = theme.CheckedListBoxBG;
            } else if (type == typeof(TreeView)) {
                ctl.ForeColor = theme.TreeViewFG;
                ctl.BackColor = theme.TreeViewBG;
                ((TreeView)ctl).LineColor = theme.TreeViewNodeLineColor;
            } else if (type == typeof(PictureBox)) {
                ctl.ForeColor = theme.PictureBoxFG;
                ctl.BackColor = theme.PictureBoxBG;
            } else if (type == typeof(TrackBar)) {
                ctl.ForeColor = theme.TrackBarFG;
                ctl.BackColor = theme.TrackBarBG;
            } else if (type == typeof(ProgressBar)) {
                ctl.ForeColor = theme.ProgressBarFG;
                ctl.BackColor = theme.ProgressBarBG;
            } else if (type == typeof(PropertyGrid)) {
                ctl.ForeColor = theme.PropertyGridFG;
                ctl.BackColor = theme.PropertyGridBG;
            } else if (type == typeof(GroupBox)) {
                ctl.ForeColor = theme.GroupBoxFG;
                ctl.BackColor = theme.GroupBoxBG;
                ApplyTheme(theme, ((GroupBox)ctl).Controls, allowSetOwnerDraw, allowSetFlatStyle);

            } else if (type == typeof(Panel)) {
                ctl.ForeColor = theme.PanelFG;
                ctl.BackColor = theme.PanelBG;
                ApplyTheme(theme, ((Panel)ctl).Controls, allowSetOwnerDraw, allowSetFlatStyle);

            } else if (type == typeof(SplitContainer)) {
                ctl.ForeColor = theme.SplitContainerFG;
                ctl.BackColor = theme.SplitContainerBG;
                ApplyTheme(theme, ((SplitContainer)ctl).Controls, allowSetOwnerDraw, allowSetFlatStyle);
            } else if (type == typeof(SplitterPanel)) {
                ctl.ForeColor = theme.SplitterPanelFG;
                ctl.BackColor = theme.SplitterPanelBG;
                ApplyTheme(theme, ((SplitterPanel)ctl).Controls, allowSetOwnerDraw, allowSetFlatStyle);

            } else if (type == typeof(TabControl)) {
                if (allowSetOwnerDraw)
                    ((TabControl)ctl).DrawMode = theme.TabControlOwnerDraw ? TabDrawMode.OwnerDrawFixed : TabDrawMode.Normal;
                ctl.ForeColor = theme.TabControlFG;
                ctl.BackColor = theme.TabControlBG;
                ApplyTheme(theme, ((TabControl)ctl).Controls, allowSetOwnerDraw, allowSetFlatStyle);
            } else if (type == typeof(TabPage)) {
                ctl.ForeColor = theme.TabPageFG;
                ctl.BackColor = theme.TabPageBG;
                if (theme.TabPageBG == Color.Transparent)
                    ((TabPage)ctl).UseVisualStyleBackColor = true;
                ApplyTheme(theme, ((TabPage)ctl).Controls, allowSetOwnerDraw, allowSetFlatStyle);

            } else if (type == typeof(MenuStrip)) {
                ctl.ForeColor = theme.MenuStripFG;
                ctl.BackColor = theme.MenuStripBG;
                ApplyTheme(theme, ((MenuStrip)ctl).Items, allowSetOwnerDraw, allowSetFlatStyle);
            } else if (type == typeof(StatusStrip)) {
                ctl.ForeColor = theme.StatusStripFG;
                ctl.BackColor = theme.StatusStripBG;
                ApplyTheme(theme, ((StatusStrip)ctl).Items, allowSetOwnerDraw, allowSetFlatStyle);
            } else if (type == typeof(ToolStrip)) {
                ctl.ForeColor = theme.ToolStripFG;
                ctl.BackColor = theme.ToolStripBG;
                ApplyTheme(theme, ((ToolStrip)ctl).Items, allowSetOwnerDraw, allowSetFlatStyle);
            } else if (type == typeof(ContextMenuStrip)) {
                ctl.ForeColor = theme.ContextMenuStripFG;
                ctl.BackColor = theme.ContextMenuStripBG;
                ApplyTheme(theme, ((ContextMenuStrip)ctl).Items, allowSetOwnerDraw, allowSetFlatStyle);

            } else {
                ctl.ForeColor = theme.OtherFG;
                ctl.BackColor = theme.OtherBG;
            }
        }
    }
    public static void FixComboBoxFlatBackground(Theme theme, System.Collections.IEnumerable controls) {
        if (theme.ComboBoxFlatStyle == FlatStyle.Standard)
            return;
        foreach (Control ctl in controls.OfType<Control>()) {
            if (ctl is ComboBox cbx) {
                if (cbx.DropDownStyle == ComboBoxStyle.DropDownList) { // fix FlatStyle.Standard messing up background color
                    cbx.BeginInvoke(new Action(() => cbx.DropDownStyle = ComboBoxStyle.DropDown));
                    cbx.BeginInvoke(new Action(() => cbx.DropDownStyle = ComboBoxStyle.DropDownList));
                }
            } else if (ctl is ToolStrip ts) {
                FixComboBoxFlatBackground(theme, ts.Items);
            }
            FixComboBoxFlatBackground(theme, ctl.Controls);
        }

        foreach (ToolStripItem item in controls.OfType<ToolStripItem>()) {
            if (item is ToolStripComboBox cbx) {
                if (cbx.DropDownStyle == ComboBoxStyle.DropDownList) {
                    cbx.GetCurrentParent().BeginInvoke(new Action(() => cbx.DropDownStyle = ComboBoxStyle.DropDown));
                    cbx.GetCurrentParent().BeginInvoke(new Action(() => cbx.DropDownStyle = ComboBoxStyle.DropDownList));
                }
            } else if (item is ToolStripDropDownItem tsdd) {
                FixComboBoxFlatBackground(theme, tsdd.DropDownItems);
            }
        }
    }

    public static void InitCustomRenderers(System.Collections.IEnumerable controls) {
        foreach (Control ctl in controls) {
            if (ctl is ListView listView) {
                listView.DrawColumnHeader += CustomPaint.ListView_DrawCustomColumnHeader;
                listView.DrawItem += CustomPaint.ListView_DrawItemWithCustomGrid;
                listView.DrawSubItem += CustomPaint.ListView_DrawSubItemWithCustomGrid;
            } else if (ctl is TabControl tabControl) {
                tabControl.DrawItem += CustomPaint.TabControl_DrawCustomItem;
            }
            InitCustomRenderers(ctl.Controls);
        }
    }
    public static void ApplyThemeRenderer(Theme theme, System.Collections.IEnumerable controls) {
        if (theme.ToolStripRenderMode != ToolStripManagerRenderMode.Custom)
            ToolStripManager.RenderMode = theme.ToolStripRenderMode;
        else
            ToolStripManager.Renderer = new CustomPaint.ToolStripSystemRendererWithDisabled(theme.ToolStripItemDisabledText);

        ApplyThemeRendererRecursive(theme, controls);
    }
    private static void ApplyThemeRendererRecursive(Theme theme, System.Collections.IEnumerable controls) {
        foreach (Control ctl in controls) {
            if (ctl is ListView listView)
                listView.Tag = theme.ListViewCustomColors;
            else if (ctl is TabControl tabControl)
                tabControl.Tag = theme.TabControlTabColors;

            ApplyThemeRendererRecursive(theme, ctl.Controls);
        }
    }
    #endregion

    #region Save / Load Theme
#if NETCOREAPP

    public static void SaveTheme(string path, Theme theme) =>
        System.IO.File.WriteAllText(path, JsonSerializer.Serialize(theme, jsonSerializerOptions));
    public static Theme LoadTheme(string path) =>
        JsonSerializer.Deserialize<Theme>(System.IO.File.ReadAllText(path), jsonSerializerOptions);

    private static readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions() {
        IncludeFields = true, // if on .NetCore3.1 add package System.Text.Json >= 5.0.2
        Converters = {
            new CustomColorSerializer(),
        }
    };
    private class CustomColorSerializer : System.Text.Json.Serialization.JsonConverter<Color> {
        public override Color Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options) {
            if (reader.TokenType == JsonTokenType.StartObject)
                reader.Read();

            int a = 0, r = 0, g = 0, b = 0;
            while (reader.TokenType != JsonTokenType.EndObject) {
                if (reader.TokenType == JsonTokenType.PropertyName) {
                    string propertyName = reader.GetString().ToUpperInvariant();
                    reader.Read();

                    switch (propertyName) {
                        case "A":
                            a = reader.GetInt32();
                            break;
                        case "R":
                            r = reader.GetInt32();
                            break;
                        case "G":
                            g = reader.GetInt32();
                            break;
                        case "B":
                            b = reader.GetInt32();
                            break;
                    }
                }
                reader.Read();
            }

            return Color.FromArgb(a, r, g, b);
        }

        public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options) {
            writer.WriteStartObject();
            writer.WriteNumber("A", value.A);
            writer.WriteNumber("R", value.R);
            writer.WriteNumber("G", value.G);
            writer.WriteNumber("B", value.B);
            writer.WriteEndObject();
        }
    }

#else

    public static void SaveTheme(string path, Theme theme) {
        var jss = new JavaScriptSerializer();
        jss.RegisterConverters(new[] { new CustomColorSerializer() });

        System.IO.File.WriteAllText(path, jss.Serialize(theme));
    }
    public static Theme LoadTheme(string path) {
        var jss = new JavaScriptSerializer();
        jss.RegisterConverters(new[] { new CustomColorSerializer() });

        return jss.Deserialize<Theme>(System.IO.File.ReadAllText(path));
    }

    private class CustomColorSerializer : JavaScriptConverter {
        public override IEnumerable<Type> SupportedTypes => new[] { typeof(Color) };
        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer) =>
            Color.FromArgb((int)dictionary["A"], (int)dictionary["R"], (int)dictionary["G"], (int)dictionary["B"]);
        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer) {
            if (obj is Color c) {
                return new Dictionary<string, object>() {
                    { "A", c.A }, { "R", c.R }, { "G", c.G }, { "B", c.B }
                };
            } else {
                throw new ArgumentException("Supplied object is not a Color: " + obj.GetType().FullName);
            }
        }
    }

#endif
    #endregion

    #region Theme class
    public enum ThemeName {
        Default,
        System_Dark,
        Dark,
        Inverted,
        Test
    }

    public static Theme GetTheme(ThemeName themeName) =>
        themeName switch {
            ThemeName.Default => Theme.Default,
            ThemeName.System_Dark => Theme.SystemDark,
            ThemeName.Dark => Theme.Dark,
            ThemeName.Inverted => Theme.Inverted,
            ThemeName.Test => Theme.Test,
            _ => throw new ApplicationException("Invalid Theme Name: " + themeName.ToString()),
        };

#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
#pragma warning disable CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
    public class Theme {
#pragma warning restore CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
        public Color FormFG;
        public Color FormBG;
        public Color CustomMsgBoxTopPanel;
        public Color ButtonFG;
        public Color ButtonBG;
        public FlatStyle ButtonFlatStyle;
        public Color ButtonFlatMouseOver;
        public Color ButtonFlatMouseDown;
        public Color LabelFG;
        public Color LabelBG;
        public Color LinkLabelFG;
        public Color LinkLabelBG;
        public Color LinkLabelLinkColor;
        public Color LinkLabelVisitedLinkColor;
        public Color LinkLabelActiveLinkColor;
        public Color DialogHeadingText;
        public Color ComboBoxFG;
        public Color ComboBoxBG;
        public FlatStyle ComboBoxFlatStyle;
        public Color CheckBoxFG;
        public Color CheckBoxBG;
        public Color RadioButtonFG;
        public Color RadioButtonBG;
        public Color TextBoxFG;
        public Color TextBoxBG;
        public Color TextBoxReadOnlyFG;
        public Color TextBoxReadOnlyBG;
        public Color NumericUpDownFG;
        public Color NumericUpDownBG;
        public Color DomainUpDownFG;
        public Color DomainUpDownBG;
        public Color ListViewFG;
        public Color ListViewBG;
        public bool ListViewOwnerDraw;
        public CustomPaint.ListViewColors ListViewCustomColors;
        public Color ListBoxFG;
        public Color ListBoxBG;
        public Color CheckedListBoxFG;
        public Color CheckedListBoxBG;
        public Color TreeViewFG;
        public Color TreeViewBG;
        public Color TreeViewNodeLineColor;
        public Color PictureBoxFG;
        public Color PictureBoxBG;
        public Color TrackBarFG;
        public Color TrackBarBG;
        public Color ProgressBarFG;
        public Color ProgressBarBG;
        public Color PropertyGridFG;
        public Color PropertyGridBG;
        public Color PanelFG;
        public Color PanelBG;
        public Color GroupBoxFG;
        public Color GroupBoxBG;
        public Color SplitContainerFG;
        public Color SplitContainerBG;
        public Color SplitterPanelFG;
        public Color SplitterPanelBG;
        public Color TabControlFG;
        public Color TabControlBG;
        public bool TabControlOwnerDraw;
        public CustomPaint.TabControlColors TabControlTabColors;
        public Color TabPageFG;
        public Color TabPageBG;
        public Color MenuStripFG;
        public Color MenuStripBG;
        public Color StatusStripFG;
        public Color StatusStripBG;
        public Color ToolStripFG;
        public Color ToolStripBG;
        public Color ContextMenuStripFG;
        public Color ContextMenuStripBG;

        public Color ToolStripButtonFG;
        public Color ToolStripButtonBG;
        public Color ToolStripComboBoxFG;
        public Color ToolStripComboBoxBG;
        public FlatStyle ToolStripComboBoxFlatStyle;
        public Color ToolStripDropDownFG;
        public Color ToolStripDropDownBG;
        public Color ToolStripDropDownButtonFG;
        public Color ToolStripDropDownButtonBG;
        public Color ToolStripItemDisabledText;
        public Color ToolStripMenuItemFG;
        public Color ToolStripMenuItemBG;
        public Color ToolStripProgressBarFG;
        public Color ToolStripProgressBarBG;
        public Color ToolStripSeparatorFG;
        public Color ToolStripSeparatorBG;
        public Color ToolStripStatusLabelFG;
        public Color ToolStripStatusLabelBG;
        public Color ToolStripSplitButtonFG;
        public Color ToolStripSplitButtonBG;
        public Color ToolStripTextBoxFG;
        public Color ToolStripTextBoxBG;
        public ToolStripManagerRenderMode ToolStripRenderMode;
        public WalkmanLib.PreferredAppMode SystemAppMode;

        public Color OtherFG;
        public Color OtherBG;

        public static bool operator !=(Theme left, Theme right) => !left.Equals(right);
        public static bool operator ==(Theme left, Theme right) => left.Equals(right);
        public override bool Equals(object obj) {
            if (obj == null)
                return false;

            Type srcType = GetType();
            Type dstType = obj.GetType();
            if (srcType != dstType)
                return false;

            foreach (System.Reflection.FieldInfo fi in srcType.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)) {
                object srcValue = srcType.GetField(fi.Name).GetValue(this);
                object dstValue = dstType.GetField(fi.Name).GetValue(obj);

                if (srcValue != dstValue && (srcValue == null || !srcValue.Equals(dstValue))) {
                    return false;
                }
            }
            return true;
        }

        public static Theme Default {
            get {
                return new Theme() {
                    ComboBoxFG = SystemColors.WindowText,
                    ComboBoxBG = SystemColors.Window,
                    ComboBoxFlatStyle = FlatStyle.Standard,
                    CheckedListBoxFG = SystemColors.WindowText,
                    CheckedListBoxBG = SystemColors.Window,
                    DomainUpDownFG = SystemColors.WindowText,
                    DomainUpDownBG = SystemColors.Window,
                    ListViewFG = SystemColors.WindowText,
                    ListViewBG = SystemColors.Window,
                    ListBoxFG = SystemColors.WindowText,
                    ListBoxBG = SystemColors.Window,
                    NumericUpDownFG = SystemColors.WindowText,
                    NumericUpDownBG = SystemColors.Window,
                    TextBoxFG = SystemColors.WindowText,
                    TextBoxBG = SystemColors.Window,
                    ToolStripComboBoxFG = SystemColors.WindowText,
                    ToolStripComboBoxBG = SystemColors.Window,
                    ToolStripComboBoxFlatStyle = FlatStyle.Standard,
                    ToolStripTextBoxFG = SystemColors.WindowText,
                    ToolStripTextBoxBG = SystemColors.Window,
                    TreeViewFG = SystemColors.WindowText,
                    TreeViewBG = SystemColors.Window,

                    FormFG = SystemColors.ControlText,
                    FormBG = SystemColors.Control,
                    ButtonFG = SystemColors.ControlText,
                    ButtonBG = SystemColors.Control,
                    ButtonFlatStyle = FlatStyle.Standard,
                    ButtonFlatMouseOver = Color.Empty,
                    ButtonFlatMouseDown = Color.Empty,
                    CheckBoxFG = SystemColors.ControlText,
                    CheckBoxBG = SystemColors.Control,
                    GroupBoxFG = SystemColors.ControlText,
                    GroupBoxBG = SystemColors.Control,
                    LabelFG = SystemColors.ControlText,
                    LabelBG = SystemColors.Control,
                    LinkLabelFG = SystemColors.ControlText,
                    LinkLabelBG = SystemColors.Control,
                    MenuStripFG = SystemColors.ControlText,
                    MenuStripBG = SystemColors.Control,
                    PanelFG = SystemColors.ControlText,
                    PanelBG = SystemColors.Control,
                    PropertyGridFG = SystemColors.ControlText,
                    PropertyGridBG = SystemColors.Control,
                    PictureBoxFG = SystemColors.ControlText,
                    PictureBoxBG = SystemColors.Control,
                    RadioButtonFG = SystemColors.ControlText,
                    RadioButtonBG = SystemColors.Control,
                    StatusStripFG = SystemColors.ControlText,
                    StatusStripBG = SystemColors.Control,
                    SplitContainerFG = SystemColors.ControlText,
                    SplitContainerBG = SystemColors.Control,
                    SplitterPanelFG = SystemColors.ControlText,
                    SplitterPanelBG = SystemColors.Control,
                    ToolStripButtonFG = SystemColors.ControlText,
                    ToolStripButtonBG = SystemColors.Control,
                    ToolStripDropDownButtonFG = SystemColors.ControlText,
                    ToolStripDropDownButtonBG = SystemColors.Control,
                    ToolStripMenuItemFG = SystemColors.ControlText,
                    ToolStripMenuItemBG = SystemColors.Control,
                    ToolStripStatusLabelFG = SystemColors.ControlText,
                    ToolStripStatusLabelBG = SystemColors.Control,
                    ToolStripSplitButtonFG = SystemColors.ControlText,
                    ToolStripSplitButtonBG = SystemColors.Control,
                    TabControlFG = SystemColors.ControlText,
                    TabControlBG = SystemColors.Control,
                    ContextMenuStripFG = SystemColors.ControlText,
                    ContextMenuStripBG = default,
                    ToolStripFG = SystemColors.ControlText,
                    ToolStripBG = default,
                    ToolStripDropDownFG = SystemColors.ControlText,
                    ToolStripDropDownBG = default,

                    LinkLabelLinkColor = Color.FromArgb(unchecked((int)0xFF0000FF)),
                    LinkLabelVisitedLinkColor = Color.FromArgb(unchecked((int)0xFF800080)),
                    LinkLabelActiveLinkColor = Color.Red,
                    CustomMsgBoxTopPanel = SystemColors.Window,
                    DialogHeadingText = Color.FromArgb(unchecked((int)0xFF003399)),
                    ProgressBarFG = SystemColors.Highlight,
                    ProgressBarBG = SystemColors.Control,
                    TabPageFG = SystemColors.ControlText,
                    TabPageBG = Color.Transparent,
                    TextBoxReadOnlyFG = SystemColors.WindowText,
                    TextBoxReadOnlyBG = SystemColors.Control,
                    ToolStripProgressBarFG = SystemColors.Highlight,
                    ToolStripProgressBarBG = SystemColors.Control,
                    ToolStripSeparatorFG = SystemColors.ControlDark,
                    ToolStripSeparatorBG = SystemColors.Control,
                    TrackBarFG = SystemColors.WindowText,
                    TrackBarBG = SystemColors.Control,
                    TreeViewNodeLineColor = Color.Black,
                    OtherFG = SystemColors.ControlText,
                    OtherBG = SystemColors.Control,

                    ListViewOwnerDraw = false,
                    TabControlOwnerDraw = false,
                    ListViewCustomColors = new CustomPaint.ListViewColors() {
                        ColumnText = SystemColors.ControlText,
                        ColumnBackground = Color.FromArgb(unchecked((int)0xFFF2F2F2)),
                        GridLines = Color.FromArgb(unchecked((int)0xFFF0F0F0))
                    },
                    TabControlTabColors = new CustomPaint.TabControlColors() {
                        TabText = SystemColors.ControlText,
                        ActiveTab = SystemColors.Control,
                        InactiveTab = SystemColors.Control,
                        TabStripBackground = SystemColors.Control
                    },
                    ToolStripItemDisabledText = Color.FromArgb(unchecked((int)0xFF6D6D6D)),
                    ToolStripRenderMode = ToolStripManagerRenderMode.Professional,
                    SystemAppMode = PreferredAppMode.Default
                };
            }
        }

        public static Theme Inverted {
            get {
                return new Theme() {
                    ComboBoxFG = SystemColors.Window,
                    ComboBoxBG = SystemColors.WindowText,
                    ComboBoxFlatStyle = FlatStyle.Popup,
                    CheckedListBoxFG = SystemColors.Window,
                    CheckedListBoxBG = SystemColors.WindowText,
                    DomainUpDownFG = SystemColors.Window,
                    DomainUpDownBG = SystemColors.WindowText,
                    ListViewFG = SystemColors.Window,
                    ListViewBG = SystemColors.WindowText,
                    ListBoxFG = SystemColors.Window,
                    ListBoxBG = SystemColors.WindowText,
                    NumericUpDownFG = SystemColors.Window,
                    NumericUpDownBG = SystemColors.WindowText,
                    TextBoxFG = SystemColors.Window,
                    TextBoxBG = SystemColors.WindowText,
                    ToolStripComboBoxFG = SystemColors.Window,
                    ToolStripComboBoxBG = SystemColors.WindowText,
                    ToolStripComboBoxFlatStyle = FlatStyle.Popup,
                    ToolStripTextBoxFG = SystemColors.Window,
                    ToolStripTextBoxBG = SystemColors.WindowText,
                    TreeViewFG = SystemColors.Window,
                    TreeViewBG = SystemColors.WindowText,

                    FormFG = SystemColors.Control,
                    FormBG = SystemColors.ControlText,
                    ButtonFG = SystemColors.Control,
                    ButtonBG = SystemColors.ControlText,
                    ButtonFlatStyle = FlatStyle.Flat,
                    ButtonFlatMouseOver = Color.FromArgb(75, Color.White),
                    ButtonFlatMouseDown = Color.Empty,
                    CheckBoxFG = SystemColors.Control,
                    CheckBoxBG = SystemColors.ControlText,
                    ContextMenuStripFG = SystemColors.Control,
                    ContextMenuStripBG = SystemColors.ControlText,
                    GroupBoxFG = SystemColors.Control,
                    GroupBoxBG = SystemColors.ControlText,
                    LabelFG = SystemColors.Control,
                    LabelBG = SystemColors.ControlText,
                    LinkLabelFG = SystemColors.Control,
                    LinkLabelBG = SystemColors.ControlText,
                    MenuStripFG = SystemColors.Control,
                    MenuStripBG = SystemColors.ControlText,
                    PanelFG = SystemColors.Control,
                    PanelBG = SystemColors.ControlText,
                    PropertyGridFG = SystemColors.Control,
                    PropertyGridBG = SystemColors.ControlText,
                    PictureBoxFG = SystemColors.Control,
                    PictureBoxBG = SystemColors.ControlText,
                    RadioButtonFG = SystemColors.Control,
                    RadioButtonBG = SystemColors.ControlText,
                    StatusStripFG = SystemColors.Control,
                    StatusStripBG = SystemColors.ControlText,
                    SplitContainerFG = SystemColors.Control,
                    SplitContainerBG = SystemColors.ControlText,
                    SplitterPanelFG = SystemColors.Control,
                    SplitterPanelBG = SystemColors.ControlText,
                    ToolStripFG = SystemColors.Control,
                    ToolStripBG = SystemColors.ControlText,
                    ToolStripButtonFG = SystemColors.Control,
                    ToolStripButtonBG = SystemColors.ControlText,
                    ToolStripDropDownFG = SystemColors.Control,
                    ToolStripDropDownBG = SystemColors.ControlText,
                    ToolStripDropDownButtonFG = SystemColors.Control,
                    ToolStripDropDownButtonBG = SystemColors.ControlText,
                    ToolStripMenuItemFG = SystemColors.Control,
                    ToolStripMenuItemBG = SystemColors.ControlText,
                    ToolStripStatusLabelFG = SystemColors.Control,
                    ToolStripStatusLabelBG = SystemColors.ControlText,
                    ToolStripSplitButtonFG = SystemColors.Control,
                    ToolStripSplitButtonBG = SystemColors.ControlText,
                    TabControlFG = SystemColors.Control,
                    TabControlBG = SystemColors.ControlText,

                    LinkLabelLinkColor = Color.Cyan,
                    LinkLabelVisitedLinkColor = Color.Purple,
                    LinkLabelActiveLinkColor = Color.Red,
                    CustomMsgBoxTopPanel = SystemColors.ControlText,
                    DialogHeadingText = Color.Cyan,
                    ProgressBarFG = SystemColors.Highlight,
                    ProgressBarBG = SystemColors.ControlText,
                    TabPageFG = SystemColors.Control,
                    TabPageBG = SystemColors.ControlText,
                    TextBoxReadOnlyFG = SystemColors.Window,
                    TextBoxReadOnlyBG = SystemColors.ControlText,
                    ToolStripProgressBarFG = SystemColors.Highlight,
                    ToolStripProgressBarBG = SystemColors.ControlText,
                    ToolStripSeparatorFG = SystemColors.ControlDark,
                    ToolStripSeparatorBG = SystemColors.ControlDarkDark,
                    TrackBarFG = SystemColors.Window,
                    TrackBarBG = SystemColors.ControlText,
                    TreeViewNodeLineColor = SystemColors.Window,
                    OtherFG = SystemColors.Control,
                    OtherBG = SystemColors.ControlText,

                    ListViewOwnerDraw = true,
                    TabControlOwnerDraw = true,
                    ListViewCustomColors = new CustomPaint.ListViewColors() {
                        ColumnText = SystemColors.Control,
                        ColumnBackground = Color.FromArgb(unchecked((int)0xFF303030)),
                        GridLines = SystemColors.ControlDarkDark
                    },
                    TabControlTabColors = new CustomPaint.TabControlColors() {
                        TabText = SystemColors.Control,
                        ActiveTab = SystemColors.ControlText,
                        InactiveTab = SystemColors.ControlDarkDark,
                        TabStripBackground = SystemColors.ControlText
                    },
                    ToolStripItemDisabledText = Color.FromArgb(unchecked((int)0xFFB2B2B2)),
                    ToolStripRenderMode = ToolStripManagerRenderMode.Custom,
                    SystemAppMode = PreferredAppMode.ForceDark
                };
            }
        }

        public static Theme SystemDark {
            get {
                return new Theme() {
                    FormFG = SystemColors.Control,
                    FormBG = SystemColors.ControlDarkDark,
                    ButtonFG = SystemColors.Control,
                    ButtonBG = SystemColors.ControlDarkDark,
                    ButtonFlatStyle = FlatStyle.Standard,
                    ButtonFlatMouseOver = Color.Empty,
                    ButtonFlatMouseDown = Color.Empty,
                    CheckBoxFG = SystemColors.Control,
                    CheckBoxBG = SystemColors.ControlDarkDark,
                    CheckedListBoxFG = SystemColors.Control,
                    CheckedListBoxBG = SystemColors.ControlDarkDark,
                    ComboBoxFG = SystemColors.Control,
                    ComboBoxBG = SystemColors.ControlDarkDark,
                    ComboBoxFlatStyle = FlatStyle.Popup,
                    ContextMenuStripFG = SystemColors.Control,
                    ContextMenuStripBG = SystemColors.ControlDarkDark,
                    CustomMsgBoxTopPanel = SystemColors.ControlDarkDark,
                    DialogHeadingText = Color.Blue,
                    DomainUpDownFG = SystemColors.Control,
                    DomainUpDownBG = SystemColors.ControlDarkDark,
                    GroupBoxFG = SystemColors.Control,
                    GroupBoxBG = SystemColors.ControlDarkDark,
                    LabelFG = SystemColors.Control,
                    LabelBG = SystemColors.ControlDarkDark,
                    LinkLabelFG = SystemColors.Control,
                    LinkLabelBG = SystemColors.ControlDarkDark,
                    LinkLabelLinkColor = Color.FromArgb(unchecked((int)0xFF0000FF)),
                    LinkLabelVisitedLinkColor = Color.FromArgb(unchecked((int)0xFF800080)),
                    LinkLabelActiveLinkColor = Color.Red,
                    ListBoxFG = SystemColors.Control,
                    ListBoxBG = SystemColors.ControlDarkDark,
                    ListViewFG = SystemColors.Control,
                    ListViewBG = SystemColors.ControlDarkDark,
                    MenuStripFG = SystemColors.Control,
                    MenuStripBG = SystemColors.ControlDarkDark,
                    NumericUpDownFG = SystemColors.Control,
                    NumericUpDownBG = SystemColors.ControlDarkDark,
                    PanelFG = SystemColors.Control,
                    PanelBG = SystemColors.ControlDarkDark,
                    PictureBoxFG = SystemColors.Control,
                    PictureBoxBG = SystemColors.ControlDarkDark,
                    ProgressBarFG = SystemColors.Control,
                    ProgressBarBG = SystemColors.ControlDarkDark,
                    PropertyGridFG = SystemColors.Control,
                    PropertyGridBG = SystemColors.ControlDarkDark,
                    RadioButtonFG = SystemColors.Control,
                    RadioButtonBG = SystemColors.ControlDarkDark,
                    SplitContainerFG = SystemColors.Control,
                    SplitContainerBG = SystemColors.ControlDarkDark,
                    SplitterPanelFG = SystemColors.Control,
                    SplitterPanelBG = SystemColors.ControlDarkDark,
                    StatusStripFG = SystemColors.Control,
                    StatusStripBG = SystemColors.ControlDarkDark,
                    TabControlFG = SystemColors.Control,
                    TabControlBG = SystemColors.ControlDarkDark,
                    TabPageFG = SystemColors.Control,
                    TabPageBG = SystemColors.ControlDarkDark,
                    TextBoxFG = SystemColors.Control,
                    TextBoxBG = SystemColors.ControlDarkDark,
                    TextBoxReadOnlyFG = SystemColors.Control,
                    TextBoxReadOnlyBG = SystemColors.ControlDarkDark,
                    ToolStripFG = SystemColors.Control,
                    ToolStripBG = SystemColors.ControlDarkDark,
                    ToolStripButtonFG = SystemColors.Control,
                    ToolStripButtonBG = SystemColors.ControlDarkDark,
                    ToolStripComboBoxFG = SystemColors.Control,
                    ToolStripComboBoxBG = SystemColors.ControlDarkDark,
                    ToolStripComboBoxFlatStyle = FlatStyle.Popup,
                    ToolStripDropDownFG = SystemColors.Control,
                    ToolStripDropDownBG = SystemColors.ControlDarkDark,
                    ToolStripDropDownButtonFG = SystemColors.Control,
                    ToolStripDropDownButtonBG = SystemColors.ControlDarkDark,
                    ToolStripMenuItemFG = SystemColors.Control,
                    ToolStripMenuItemBG = SystemColors.ControlDarkDark,
                    ToolStripProgressBarFG = SystemColors.Control,
                    ToolStripProgressBarBG = SystemColors.ControlDarkDark,
                    ToolStripSeparatorFG = SystemColors.Control,
                    ToolStripSeparatorBG = SystemColors.ControlDarkDark,
                    ToolStripSplitButtonFG = SystemColors.Control,
                    ToolStripSplitButtonBG = SystemColors.ControlDarkDark,
                    ToolStripStatusLabelFG = SystemColors.Control,
                    ToolStripStatusLabelBG = SystemColors.ControlDarkDark,
                    ToolStripTextBoxFG = SystemColors.Control,
                    ToolStripTextBoxBG = SystemColors.ControlDarkDark,
                    TrackBarFG = SystemColors.Control,
                    TrackBarBG = SystemColors.ControlDarkDark,
                    TreeViewFG = SystemColors.Control,
                    TreeViewBG = SystemColors.ControlDarkDark,
                    TreeViewNodeLineColor = SystemColors.Window,
                    OtherFG = SystemColors.Control,
                    OtherBG = SystemColors.ControlDarkDark,

                    ListViewOwnerDraw = false,
                    TabControlOwnerDraw = false,
                    ListViewCustomColors = new CustomPaint.ListViewColors() {
                        ColumnText = SystemColors.Control,
                        ColumnBackground = Color.FromArgb(unchecked((int)0xFF808080)),
                        GridLines = SystemColors.ControlDark
                    },
                    TabControlTabColors = new CustomPaint.TabControlColors() {
                        TabText = SystemColors.Control,
                        ActiveTab = SystemColors.ControlDarkDark,
                        InactiveTab = SystemColors.ControlDark,
                        TabStripBackground = SystemColors.ControlDarkDark
                    },
                    ToolStripItemDisabledText = Color.FromArgb(unchecked((int)0xFFB2B2B2)),
                    ToolStripRenderMode = ToolStripManagerRenderMode.Custom,
                    SystemAppMode = PreferredAppMode.AllowDark
                };
            }
        }

        public static Theme Dark {
            get {
                var textColor = Color.FromArgb(unchecked((int)0xFFCFCFCF));
                var backColor = Color.FromArgb(unchecked((int)0xFF303030));
                var altTextColor = Color.FromArgb(unchecked((int)0xFFFFFFFF));
                var altBackColor = Color.FromArgb(unchecked((int)0xFF515259));

                return new Theme() {
                    FormFG = textColor,
                    FormBG = backColor,
                    ButtonFG = textColor,
                    ButtonBG = backColor,
                    ButtonFlatStyle = FlatStyle.Flat,
                    ButtonFlatMouseOver = Color.FromArgb(50, Color.White),
                    ButtonFlatMouseDown = Color.Empty,
                    CheckBoxFG = textColor,
                    CheckBoxBG = backColor,
                    CheckedListBoxFG = textColor,
                    CheckedListBoxBG = backColor,
                    ComboBoxFG = textColor,
                    ComboBoxBG = backColor,
                    ComboBoxFlatStyle = FlatStyle.Popup,
                    ContextMenuStripFG = altTextColor,
                    ContextMenuStripBG = altBackColor,
                    CustomMsgBoxTopPanel = Color.FromArgb(unchecked((int)0xFF444449)),
                    DialogHeadingText = Color.DarkTurquoise,
                    DomainUpDownFG = textColor,
                    DomainUpDownBG = backColor,
                    GroupBoxFG = textColor,
                    GroupBoxBG = backColor,
                    LabelFG = textColor,
                    LabelBG = backColor,
                    LinkLabelFG = textColor,
                    LinkLabelBG = backColor,
                    LinkLabelLinkColor = Color.Cyan,
                    LinkLabelVisitedLinkColor = Color.Purple,
                    LinkLabelActiveLinkColor = Color.Red,
                    ListBoxFG = textColor,
                    ListBoxBG = backColor,
                    ListViewFG = textColor,
                    ListViewBG = backColor,
                    MenuStripFG = altTextColor,
                    MenuStripBG = altBackColor,
                    NumericUpDownFG = textColor,
                    NumericUpDownBG = backColor,
                    PanelFG = textColor,
                    PanelBG = backColor,
                    PictureBoxFG = textColor,
                    PictureBoxBG = backColor,
                    ProgressBarFG = textColor,
                    ProgressBarBG = backColor,
                    PropertyGridFG = textColor,
                    PropertyGridBG = backColor,
                    RadioButtonFG = textColor,
                    RadioButtonBG = backColor,
                    SplitContainerFG = textColor,
                    SplitContainerBG = backColor,
                    SplitterPanelFG = textColor,
                    SplitterPanelBG = backColor,
                    StatusStripFG = altTextColor,
                    StatusStripBG = altBackColor,
                    TabControlFG = textColor,
                    TabControlBG = backColor,
                    TabPageFG = textColor,
                    TabPageBG = backColor,
                    TextBoxFG = textColor,
                    TextBoxBG = backColor,
                    TextBoxReadOnlyFG = textColor,
                    TextBoxReadOnlyBG = Color.FromArgb(unchecked((int)0xFF202020)),
                    ToolStripFG = altTextColor,
                    ToolStripBG = altBackColor,
                    ToolStripButtonFG = altTextColor,
                    ToolStripButtonBG = altBackColor,
                    ToolStripComboBoxFG = altTextColor,
                    ToolStripComboBoxBG = altBackColor,
                    ToolStripComboBoxFlatStyle = FlatStyle.Popup,
                    ToolStripDropDownFG = altTextColor,
                    ToolStripDropDownBG = altBackColor,
                    ToolStripDropDownButtonFG = altTextColor,
                    ToolStripDropDownButtonBG = altBackColor,
                    ToolStripMenuItemFG = altTextColor,
                    ToolStripMenuItemBG = altBackColor,
                    ToolStripProgressBarFG = altTextColor,
                    ToolStripProgressBarBG = altBackColor,
                    ToolStripSeparatorFG = textColor,
                    ToolStripSeparatorBG = altBackColor,
                    ToolStripSplitButtonFG = altTextColor,
                    ToolStripSplitButtonBG = altBackColor,
                    ToolStripStatusLabelFG = altTextColor,
                    ToolStripStatusLabelBG = altBackColor,
                    ToolStripTextBoxFG = altTextColor,
                    ToolStripTextBoxBG = altBackColor,
                    TrackBarFG = textColor,
                    TrackBarBG = backColor,
                    TreeViewFG = textColor,
                    TreeViewBG = backColor,
                    TreeViewNodeLineColor = textColor,
                    OtherFG = textColor,
                    OtherBG = backColor,

                    ListViewOwnerDraw = true,
                    TabControlOwnerDraw = true,
                    ListViewCustomColors = new CustomPaint.ListViewColors() {
                        ColumnText = textColor,
                        ColumnBackground = Color.FromArgb(unchecked((int)0xFF444449)),
                        GridLines = SystemColors.ControlDarkDark
                    },
                    TabControlTabColors = new CustomPaint.TabControlColors() {
                        TabText = textColor,
                        ActiveTab = backColor,
                        InactiveTab = altBackColor,
                        TabStripBackground = backColor
                    },
                    ToolStripItemDisabledText = Color.FromArgb(unchecked((int)0xFFB2B2B2)),
                    ToolStripRenderMode = ToolStripManagerRenderMode.Custom,
                    SystemAppMode = PreferredAppMode.ForceDark
                };
            }
        }

        public static Theme Test {
            get {
                return new Theme() {
                    FormFG = Color.Blue,
                    FormBG = Color.Magenta,
                    ButtonFG = Color.Blue,
                    ButtonBG = Color.Magenta,
                    ButtonFlatStyle = FlatStyle.Flat,
                    ButtonFlatMouseOver = Color.FromArgb(100, Color.White),
                    ButtonFlatMouseDown = Color.Green,
                    CheckBoxFG = Color.Blue,
                    CheckBoxBG = Color.Magenta,
                    CheckedListBoxFG = Color.Blue,
                    CheckedListBoxBG = Color.Magenta,
                    ComboBoxFG = Color.Blue,
                    ComboBoxBG = Color.Magenta,
                    ComboBoxFlatStyle = FlatStyle.Popup,
                    ContextMenuStripFG = Color.Blue,
                    ContextMenuStripBG = Color.Magenta,
                    CustomMsgBoxTopPanel = Color.Magenta,
                    DialogHeadingText = Color.Black,
                    DomainUpDownFG = Color.Blue,
                    DomainUpDownBG = Color.Magenta,
                    GroupBoxFG = Color.Blue,
                    GroupBoxBG = Color.Magenta,
                    LabelFG = Color.Blue,
                    LabelBG = Color.Magenta,
                    LinkLabelFG = Color.Blue,
                    LinkLabelBG = Color.Magenta,
                    LinkLabelLinkColor = Color.Maroon,
                    LinkLabelVisitedLinkColor = Color.Purple,
                    LinkLabelActiveLinkColor = Color.Green,
                    ListBoxFG = Color.Blue,
                    ListBoxBG = Color.Magenta,
                    ListViewFG = Color.Blue,
                    ListViewBG = Color.Magenta,
                    MenuStripFG = Color.Blue,
                    MenuStripBG = Color.Magenta,
                    NumericUpDownFG = Color.Blue,
                    NumericUpDownBG = Color.Magenta,
                    PanelFG = Color.Blue,
                    PanelBG = Color.Magenta,
                    PictureBoxFG = Color.Blue,
                    PictureBoxBG = Color.Magenta,
                    ProgressBarFG = Color.Blue,
                    ProgressBarBG = Color.Magenta,
                    PropertyGridFG = Color.Blue,
                    PropertyGridBG = Color.Magenta,
                    RadioButtonFG = Color.Blue,
                    RadioButtonBG = Color.Magenta,
                    SplitContainerFG = Color.Blue,
                    SplitContainerBG = Color.Magenta,
                    SplitterPanelFG = Color.Blue,
                    SplitterPanelBG = Color.Magenta,
                    StatusStripFG = Color.Blue,
                    StatusStripBG = Color.Magenta,
                    TabControlFG = Color.Blue,
                    TabControlBG = Color.Magenta,
                    TabPageFG = Color.Blue,
                    TabPageBG = Color.Magenta,
                    TextBoxFG = Color.Blue,
                    TextBoxBG = Color.Magenta,
                    TextBoxReadOnlyFG = Color.Blue,
                    TextBoxReadOnlyBG = Color.Magenta,
                    ToolStripFG = Color.Blue,
                    ToolStripBG = Color.Magenta,
                    ToolStripButtonFG = Color.Blue,
                    ToolStripButtonBG = Color.Magenta,
                    ToolStripComboBoxFG = Color.Blue,
                    ToolStripComboBoxBG = Color.Magenta,
                    ToolStripComboBoxFlatStyle = FlatStyle.Popup,
                    ToolStripDropDownFG = Color.Blue,
                    ToolStripDropDownBG = Color.Magenta,
                    ToolStripDropDownButtonFG = Color.Blue,
                    ToolStripDropDownButtonBG = Color.Magenta,
                    ToolStripMenuItemFG = Color.Blue,
                    ToolStripMenuItemBG = Color.Magenta,
                    ToolStripProgressBarFG = Color.Blue,
                    ToolStripProgressBarBG = Color.Magenta,
                    ToolStripSeparatorFG = Color.Blue,
                    ToolStripSeparatorBG = Color.Magenta,
                    ToolStripSplitButtonFG = Color.Blue,
                    ToolStripSplitButtonBG = Color.Magenta,
                    ToolStripStatusLabelFG = Color.Blue,
                    ToolStripStatusLabelBG = Color.Magenta,
                    ToolStripTextBoxFG = Color.Blue,
                    ToolStripTextBoxBG = Color.Magenta,
                    TrackBarFG = Color.Blue,
                    TrackBarBG = Color.Magenta,
                    TreeViewFG = Color.Blue,
                    TreeViewBG = Color.Magenta,
                    TreeViewNodeLineColor = Color.Green,
                    OtherFG = Color.Blue,
                    OtherBG = Color.Magenta,

                    ListViewOwnerDraw = true,
                    TabControlOwnerDraw = true,
                    ListViewCustomColors = new CustomPaint.ListViewColors() {
                        ColumnText = Color.Blue,
                        ColumnBackground = Color.HotPink,
                        GridLines = Color.MediumVioletRed
                    },
                    TabControlTabColors = new CustomPaint.TabControlColors() {
                        TabText = Color.Blue,
                        ActiveTab = Color.Magenta,
                        InactiveTab = Color.Violet,
                        TabStripBackground = Color.Magenta
                    },
                    ToolStripItemDisabledText = Color.Green,
                    ToolStripRenderMode = ToolStripManagerRenderMode.Custom,
                    SystemAppMode = PreferredAppMode.AllowDark
                };
            }
        }
    }
    #endregion

    #region CustomPaint class
    public class CustomPaint {
        public static void ListView_DrawDefaultItem(object sender, DrawListViewItemEventArgs e) {
            e.DrawDefault = true;
        }
        public static void ListView_DrawDefaultSubItem(object sender, DrawListViewSubItemEventArgs e) {
            e.DrawDefault = true;
        }

        public struct ListViewColors {
            public Color ColumnText;
            public Color ColumnBackground;
            public Color GridLines;
        }
        public static void ListView_DrawItemWithCustomGrid(object sender, DrawListViewItemEventArgs e) {
            if (e.Item.ListView.View == View.Details)
                e.DrawDefault = false;
            else
                e.DrawDefault = true;
        }
        // in c#, event handler signatures must exactly match the event delegate signature
        public static void ListView_DrawSubItemWithCustomGrid(object sender, DrawListViewSubItemEventArgs e) => ListView_DrawSubItemWithCustomGrid(sender, e, null);
        public static void ListView_DrawSubItemWithCustomGrid(object sender, DrawListViewSubItemEventArgs e, ListViewColors? listViewColors = null) {
            e.DrawDefault = false;
            var listView = (ListView)sender;
            // can't use default e.DrawBackground() & e.DrawText() as they don't account for images or checkboxes

            Rectangle fixedBounds = e.Bounds;
            if (e.ColumnIndex == 0 && listView.Columns[e.ColumnIndex].DisplayIndex != 0)  // first column was reordered, e.Bounds are incorrect
                foreach (ColumnHeader column in listView.Columns.OfType<ColumnHeader>().OrderBy(c => c.DisplayIndex)) {
                    if (column.Index == 0)
                        break;
                    fixedBounds.Offset(column.Width, 0);
                }
            Rectangle bounds = fixedBounds;
            bounds.Offset(3, 0);
            bounds.Width -= 1;

            if (e.ColumnIndex == 0) { // first column - draw Checkbox & Image
                if (listView.CheckBoxes) {
                    var cbState = e.Item.Checked ? System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal : System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal;
                    var cbSize = CheckBoxRenderer.GetGlyphSize(e.Graphics, cbState);
                    var cbPoint = new Point(bounds.X, bounds.Y + (bounds.Height - cbSize.Height) / 2 - 1);
                    CheckBoxRenderer.DrawCheckBox(e.Graphics, cbPoint, cbState);

                    bounds.Offset(cbSize.Width + 3, 0);
                    bounds.Width -= cbSize.Width + 3;
                }
                if (e.Item.ImageList != null) {
                    if (e.Item.ImageIndex != -1)
                        e.Item.ImageList.Draw(e.Graphics, bounds.Location, e.Item.ImageIndex);
                    if (e.Item.ImageKey != null && e.Item.ImageList.Images.ContainsKey(e.Item.ImageKey))
                        e.Item.ImageList.Draw(e.Graphics, bounds.Location, e.Item.ImageList.Images.IndexOfKey(e.Item.ImageKey));

                    // if images are shown & item is selected, transparency highlight image separately
                    if (e.Item.Selected)
                        using (var brush = new SolidBrush(Color.FromArgb(130, SystemColors.Highlight)))
                            e.Graphics.FillRectangle(brush, new Rectangle(location: bounds.Location, size: e.Item.ImageList.ImageSize));

                    bounds.Offset(e.Item.ImageList.ImageSize.Width, 0);
                    bounds.Width -= e.Item.ImageList.ImageSize.Width;
                }
            }

            // adjust for drawing background
            if (e.ColumnIndex != 0) bounds.Offset(-2, 0); else bounds.Width -= 2;
            if (e.Item.Selected)
                e.Graphics.FillRectangle(SystemBrushes.Highlight, bounds);
            else
                using (var bgBrush = new SolidBrush(e.SubItem.BackColor))
                    e.Graphics.FillRectangle(bgBrush, bounds);
            // reset for drawing text
            if (e.ColumnIndex != 0) bounds.Offset(2, 0); else bounds.Width += 2;

            if (e.ColumnIndex != 0) bounds.Width -= 4;
            Color textColor = e.Item.Selected ? SystemColors.HighlightText : e.SubItem.ForeColor;
            TextFormatFlags flags = TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis;
            if (e.Header.TextAlign == HorizontalAlignment.Right) flags |= TextFormatFlags.Right;
            if (e.Header.TextAlign == HorizontalAlignment.Center) flags |= TextFormatFlags.HorizontalCenter;
            TextRenderer.DrawText(e.Graphics, e.SubItem.Text, e.SubItem.Font, bounds, textColor, flags);

            ListViewColors colors;
            if (listViewColors.HasValue)
                colors = listViewColors.Value;
            else
                colors = (ListViewColors)listView.Tag;

            bool groupsShown = listView.ShowGroups && listView.Groups != null && listView.Groups.Count > 0 && Application.RenderWithVisualStyles;
            using (var customGridPen = new Pen(colors.GridLines, 1)) {

                // Horizontal grid lines:
                if (!groupsShown) {
                    e.Graphics.DrawLine(customGridPen, fixedBounds.Left, fixedBounds.Bottom - 1, fixedBounds.Right, fixedBounds.Bottom - 1);
                } else { // items in groups have 2-pixel wide horizontal separator...
                    e.Graphics.DrawLine(customGridPen, fixedBounds.Left, fixedBounds.Bottom, fixedBounds.Right, fixedBounds.Bottom);
                    if (e.Item.Group != null && e.Item.Group.Items.Count > 0 && (e.Item.Group.Items[0] == e.Item))
                        e.Graphics.DrawLine(customGridPen, fixedBounds.Left, fixedBounds.Top - 1, fixedBounds.Right, fixedBounds.Top - 1);
                    // if item is first in group, then draw top horizontal gridline
                }

                // Vertical grid lines:
                e.Graphics.DrawLine(customGridPen, fixedBounds.Right, fixedBounds.Top, fixedBounds.Right, fixedBounds.Bottom - 1);

                // last column - draw horizontal lines to the right
                if (listView.Columns[e.ColumnIndex].DisplayIndex == listView.Columns.Count - 1 && fixedBounds.Right < listView.ClientSize.Width) {
                    if (!groupsShown) {
                        e.Graphics.DrawLine(customGridPen, fixedBounds.Right, fixedBounds.Bottom - 1, listView.ClientSize.Width, fixedBounds.Bottom - 1);
                    } else {
                        e.Graphics.DrawLine(customGridPen, fixedBounds.Right, fixedBounds.Bottom, listView.ClientSize.Width, fixedBounds.Bottom);
                        if (e.Item.Group != null && e.Item.Group.Items.Count > 0 && e.Item.Group.Items[0] == e.Item)
                            e.Graphics.DrawLine(customGridPen, fixedBounds.Right, fixedBounds.Top - 1, listView.ClientSize.Width, fixedBounds.Top - 1);
                    }
                }

                // last row - draw lines below it
                if (!groupsShown && e.ItemIndex == listView.Items.Count - 1) {
                    int lastY = fixedBounds.Bottom;
                    while (lastY < listView.ClientSize.Height) {
                        lastY += fixedBounds.Height;

                        // Horizontal grid lines:
                        e.Graphics.DrawLine(customGridPen, fixedBounds.Left, lastY - 1, fixedBounds.Right, lastY - 1);

                        // Vertical grid lines:
                        e.Graphics.DrawLine(customGridPen, fixedBounds.Right, lastY - fixedBounds.Height - 1, fixedBounds.Right, lastY - 1);

                        // last column - horizontal lines to the right
                        if (listView.Columns[e.ColumnIndex].DisplayIndex == listView.Columns.Count - 1 && fixedBounds.Right < listView.ClientSize.Width)
                            e.Graphics.DrawLine(customGridPen, fixedBounds.Right, lastY - 1, listView.ClientSize.Width, lastY - 1);
                    }
                }
            }
        }
        public static void ListView_DrawCustomColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e) => ListView_DrawCustomColumnHeader(sender, e, null);
        public static void ListView_DrawCustomColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e, ListViewColors? listViewColors = null) {
            // https://stackoverflow.com/a/42181044/2999220
            var listView = (ListView)sender;
            ListViewColors colors;
            if (listViewColors.HasValue)
                colors = listViewColors.Value;
            else
                colors = (ListViewColors)listView.Tag;

            using (var sf = new StringFormat() {
                LineAlignment = StringAlignment.Center,
                FormatFlags = StringFormatFlags.NoWrap,
                Trimming = StringTrimming.EllipsisCharacter
            }) {
                sf.Alignment = e.Header.TextAlign switch {
                    HorizontalAlignment.Left => listView.RightToLeft == RightToLeft.No ? StringAlignment.Near : StringAlignment.Far,
                    HorizontalAlignment.Right => listView.RightToLeft == RightToLeft.Yes ? StringAlignment.Near : StringAlignment.Far,
                    HorizontalAlignment.Center => StringAlignment.Center,
                    _ => sf.Alignment,
                };

                var colRect = new Rectangle() {
                    X = e.Bounds.X + 1,
                    Y = e.Bounds.Y,
                    Width = e.Bounds.Width - 1,
                    Height = e.Bounds.Height
                };

                e.Graphics.FillRectangle(new SolidBrush(colors.ColumnBackground), colRect);
                colRect.Offset(x: 3, y: 0);
                colRect.Width -= 7;
                colRect.Height -= 1;
                e.Graphics.DrawString(e.Header.Text, listView.Font, new SolidBrush(colors.ColumnText), colRect, sf);
            }
        }

        public struct TabControlColors {
            public Color TabText;
            public Color ActiveTab;
            public Color InactiveTab;
            public Color TabStripBackground;
        }
        public static void TabControl_DrawCustomItem(object sender, DrawItemEventArgs e) => TabControl_DrawCustomItem(sender, e, null);
        public static void TabControl_DrawCustomItem(object sender, DrawItemEventArgs e, TabControlColors? tabControlColors = null) {
            var tabCtl = (TabControl)sender;
            TabControlColors colors;
            if (tabControlColors.HasValue)
                colors = tabControlColors.Value;
            else
                colors = (TabControlColors)tabCtl.Tag;

            // draw tab strip background

            Rectangle firstTabRect = tabCtl.GetTabRect(0);
            Rectangle tabStripBounds = tabCtl.Bounds;
            Rectangle allTabBounds = firstTabRect;

            if (tabCtl.Alignment == TabAlignment.Top)
                tabStripBounds.Y = 0;
            else if (tabCtl.Alignment == TabAlignment.Bottom)
                tabStripBounds.Y = firstTabRect.Y;
            else if (tabCtl.Alignment == TabAlignment.Left)
                tabStripBounds.X = 0;
            else if (tabCtl.Alignment == TabAlignment.Right)
                tabStripBounds.X = firstTabRect.X;

            if (tabCtl.Alignment == TabAlignment.Top || tabCtl.Alignment == TabAlignment.Bottom) {
                tabStripBounds.X = 0;
                tabStripBounds.Height = firstTabRect.Height + 2;

                allTabBounds.Width = 0;
                foreach (TabPage tabPage in tabCtl.TabPages)
                    allTabBounds.Width += tabCtl.GetTabRect(tabCtl.TabPages.IndexOf(tabPage)).Width;
            } else if (tabCtl.Alignment == TabAlignment.Left || tabCtl.Alignment == TabAlignment.Right) {
                tabStripBounds.Y = 0;
                tabStripBounds.Width = firstTabRect.Width + 2;

                allTabBounds.Height = 0;
                foreach (TabPage tabPage in tabCtl.TabPages)
                    allTabBounds.Height += tabCtl.GetTabRect(tabCtl.TabPages.IndexOf(tabPage)).Height;
            }

            var tabStripBackground = new Region(tabStripBounds);
            tabStripBackground.Xor(allTabBounds);
            var brush = new SolidBrush(colors.TabStripBackground);
            e.Graphics.FillRegion(brush, tabStripBackground);

            // draw tab background

            Brush backgroundBrush = new SolidBrush(colors.InactiveTab);
            Rectangle tabRect = tabCtl.GetTabRect(e.Index);
            if (tabCtl.SelectedIndex == e.Index) {
                backgroundBrush = new SolidBrush(colors.ActiveTab);
                if (tabCtl.Alignment == TabAlignment.Top) {
                    tabRect.Height += 2;
                } else if (tabCtl.Alignment == TabAlignment.Bottom) {
                    tabRect.Y -= 2;
                    tabRect.Height += 2;
                } else if (tabCtl.Alignment == TabAlignment.Left) {
                    tabRect.Width += 2;
                } else if (tabCtl.Alignment == TabAlignment.Right) {
                    tabRect.X -= 2;
                    tabRect.Width += 2;
                }
            }
            e.Graphics.FillRectangle(backgroundBrush, tabRect);

            // draw tab text

            using (var sf = new StringFormat() {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center,
                FormatFlags = StringFormatFlags.NoWrap,
                Trimming = StringTrimming.EllipsisCharacter
            }) {
                tabRect = tabCtl.GetTabRect(e.Index);

                if (tabCtl.Alignment == TabAlignment.Top) {
                    if (tabCtl.SelectedIndex == e.Index) tabRect.Y -= 1; else tabRect.Y += 1;
                } else if (tabCtl.Alignment == TabAlignment.Bottom) {
                    if (tabCtl.SelectedIndex == e.Index) tabRect.Y += 1; else tabRect.Y -= 1;
                } else if (tabCtl.Alignment == TabAlignment.Left) {
                    sf.FormatFlags |= StringFormatFlags.DirectionVertical;
                    if (tabCtl.SelectedIndex == e.Index) tabRect.X -= 2;
                } else if (tabCtl.Alignment == TabAlignment.Right) {
                    sf.FormatFlags |= StringFormatFlags.DirectionVertical;
                    if (tabCtl.SelectedIndex != e.Index) tabRect.X -= 2;
                }

                // sometimes tab text overflows tabRect and adds ellipsis at the end when there is enough space - increase tabRect width to fix
                if (!sf.FormatFlags.HasFlag(StringFormatFlags.DirectionVertical)) {
                    tabRect.Width += 2;
                    tabRect.X -= 1;
                }

                e.Graphics.DrawString(tabCtl.TabPages[e.Index].Text, tabCtl.Font, new SolidBrush(colors.TabText), tabRect, sf);
            }
        }

        public class ToolStripSystemRendererWithDisabled : ToolStripSystemRenderer {
            private readonly Func<ToolStripItem, Color> getDisabledColor = e => (Color)e.Tag;
            public ToolStripSystemRendererWithDisabled() { }
            public ToolStripSystemRendererWithDisabled(Func<ToolStripItem, Color> getDisabledColor) {
                this.getDisabledColor = getDisabledColor;
            }
            public ToolStripSystemRendererWithDisabled(Color disabledColor) {
                this.getDisabledColor = _ => disabledColor;
            }

            // draw separator with assigned ForeColor
            protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e) {
                var pen = new Pen(e.Item.ForeColor);
                var bounds = new Rectangle(Point.Empty, e.Item.Size);

                if (e.Vertical) {
                    if (bounds.Height >= 4) bounds.Inflate(0, -2);
                    int num = bounds.Width / 2;
                    e.Graphics.DrawLine(pen, num, bounds.Top, num, bounds.Bottom);
                } else {
                    if (bounds.Width >= 4) bounds.Inflate(-2, 0);
                    int num = bounds.Height / 2;
                    e.Graphics.DrawLine(pen, bounds.Left, num, bounds.Right, num);
                }
            }

            protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e) {
                // https://bytes.com/topic/visual-basic-net/answers/590331-specify-custom-disabled-text-color-toolstripprofessionalrendere
                if (e.Item.Enabled) {
                    base.OnRenderItemText(e);
                } else {
                    Color color = getDisabledColor(e.Item);

                    Rectangle textRectangle = e.TextRectangle;
                    if (e.TextDirection != ToolStripTextDirection.Horizontal && textRectangle.Width > 0 && textRectangle.Height > 0) {
                        var size = new Size(width: textRectangle.Height, height: textRectangle.Width);

                        using (var bitmap = new Bitmap(size.Width, size.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb))
                        using (var g = Graphics.FromImage(bitmap)) {
                            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                            TextRenderer.DrawText(g, e.Text, e.TextFont, new Rectangle(Point.Empty, size), color, e.TextFormat);

                            bitmap.RotateFlip(e.TextDirection == ToolStripTextDirection.Vertical90 ? RotateFlipType.Rotate90FlipNone : RotateFlipType.Rotate270FlipNone);
                            e.Graphics.DrawImage(bitmap, textRectangle);
                        }
                    } else {
                        TextRenderer.DrawText(e.Graphics, e.Text, e.TextFont, textRectangle, color, e.TextFormat);
                    }
                }
            }
        }
    }
    #endregion
}
