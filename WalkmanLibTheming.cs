using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public partial class WalkmanLib {
    #region ApplyTheme
    public static void ApplyTheme(Theme theme, Form form, bool allowSetOwnerDraw = false) {
        form.ForeColor = theme.FormFG;
        form.BackColor = theme.FormBG;
        ApplyTheme(theme, form.Controls, allowSetOwnerDraw);
    }
    public static void ApplyTheme(Theme theme, System.Collections.IEnumerable controls, bool allowSetOwnerDraw = false) {
        //                             for ToolStripItemCollection
        foreach (ToolStripItem item in controls.OfType<ToolStripItem>()) {
            Type type = item.GetType();
            if (type == typeof(ToolStripButton)) {
                item.ForeColor = theme.ToolStripButtonFG;
                item.BackColor = theme.ToolStripButtonBG;
            } else if (type == typeof(ToolStripComboBox)) {
                item.ForeColor = theme.ToolStripComboBoxFG;
                item.BackColor = theme.ToolStripComboBoxBG;
            } else if (type == typeof(ToolStripDropDownButton)) {
                item.ForeColor = theme.ToolStripDropDownButtonFG;
                item.BackColor = theme.ToolStripDropDownButtonBG;
                ((ToolStripDropDownButton)item).DropDown.ForeColor = theme.ToolStripDropDownFG;
                ((ToolStripDropDownButton)item).DropDown.BackColor = theme.ToolStripDropDownBG;
                ApplyTheme(theme, ((ToolStripDropDownButton)item).DropDownItems, allowSetOwnerDraw);
            } else if (type == typeof(ToolStripMenuItem)) { // inherits ToolStripDropDownItem
                item.ForeColor = theme.ToolStripMenuItemFG;
                item.BackColor = theme.ToolStripMenuItemBG;
                ((ToolStripMenuItem)item).DropDown.ForeColor = theme.ToolStripDropDownFG;
                ((ToolStripMenuItem)item).DropDown.BackColor = theme.ToolStripDropDownBG;
                ApplyTheme(theme, ((ToolStripMenuItem)item).DropDownItems, allowSetOwnerDraw);
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
                ApplyTheme(theme, ((ToolStripSplitButton)item).DropDownItems, allowSetOwnerDraw);
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
                if (theme.ButtonBG == SystemColors.Control)
                    ((Button)ctl).UseVisualStyleBackColor = true;
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
            } else if (type == typeof(CheckBox)) {
                ctl.ForeColor = theme.CheckBoxFG;
                ctl.BackColor = theme.CheckBoxBG;
            } else if (type == typeof(RadioButton)) {
                ctl.ForeColor = theme.RadioButtonFG;
                ctl.BackColor = theme.RadioButtonBG;
            } else if (type == typeof(TextBox)) {
                if (((TextBox)ctl).ReadOnly) {
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
                if (allowSetOwnerDraw) ((ListView)ctl).OwnerDraw = theme.ListViewOwnerDraw;
                ctl.ForeColor = theme.ListViewFG;
                ctl.BackColor = theme.ListViewBG;
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
                ApplyTheme(theme, ((GroupBox)ctl).Controls, allowSetOwnerDraw);

            } else if (type == typeof(SplitContainer)) {
                ctl.ForeColor = theme.SplitContainerFG;
                ctl.BackColor = theme.SplitContainerBG;
                ApplyTheme(theme, ((SplitContainer)ctl).Controls, allowSetOwnerDraw);
            } else if (type == typeof(SplitterPanel)) {
                ctl.ForeColor = theme.SplitterPanelFG;
                ctl.BackColor = theme.SplitterPanelBG;
                ApplyTheme(theme, ((SplitterPanel)ctl).Controls, allowSetOwnerDraw);

            } else if (type == typeof(TabControl)) {
                if (allowSetOwnerDraw) ((TabControl)ctl).DrawMode = 
                        theme.TabControlOwnerDraw ? TabDrawMode.OwnerDrawFixed : TabDrawMode.Normal;
                ctl.ForeColor = theme.TabControlFG;
                ctl.BackColor = theme.TabControlBG;
                ApplyTheme(theme, ((TabControl)ctl).Controls, allowSetOwnerDraw);
            } else if (type == typeof(TabPage)) {
                ctl.ForeColor = theme.TabPageFG;
                ctl.BackColor = theme.TabPageBG;
                if (theme.TabPageBG == Color.Transparent)
                    ((TabPage)ctl).UseVisualStyleBackColor = true;
                ApplyTheme(theme, ((TabPage)ctl).Controls, allowSetOwnerDraw);

            } else if (type == typeof(MenuStrip)) {
                ctl.ForeColor = theme.MenuStripFG;
                ctl.BackColor = theme.MenuStripBG;
                ApplyTheme(theme, ((MenuStrip)ctl).Items, allowSetOwnerDraw);
            } else if (type == typeof(StatusStrip)) {
                ctl.ForeColor = theme.StatusStripFG;
                ctl.BackColor = theme.StatusStripBG;
                ApplyTheme(theme, ((StatusStrip)ctl).Items, allowSetOwnerDraw);
            } else if (type == typeof(ToolStrip)) {
                ctl.ForeColor = theme.ToolStripFG;
                ctl.BackColor = theme.ToolStripBG;
                ApplyTheme(theme, ((ToolStrip)ctl).Items, allowSetOwnerDraw);
            } else if (type == typeof(ContextMenuStrip)) {
                ctl.ForeColor = theme.ContextMenuStripFG;
                ctl.BackColor = theme.ContextMenuStripBG;
                ApplyTheme(theme, ((ContextMenuStrip)ctl).Items, allowSetOwnerDraw);

            } else {
                ctl.ForeColor = theme.OtherFG;
                ctl.BackColor = theme.OtherBG;
            }
        }
    }
    #endregion

    #region Theme class
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
#pragma warning disable CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
    public class Theme {
#pragma warning restore CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
        public Color FormFG;
        public Color FormBG;
        public Color ButtonFG;
        public Color ButtonBG;
        public Color LabelFG;
        public Color LabelBG;
        public Color LinkLabelFG;
        public Color LinkLabelBG;
        public Color LinkLabelLinkColor;
        public Color LinkLabelVisitedLinkColor;
        public Color LinkLabelActiveLinkColor;
        public Color ComboBoxFG;
        public Color ComboBoxBG;
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
        public CustomPaint.ListViewColors ListViewColumnColors;
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

        public Color OtherFG;
        public Color OtherBG;

        public static bool operator !=(Theme left, Theme right) {
            return !left.Equals(right);
        }

        public static bool operator ==(Theme left, Theme right) {
            return left.Equals(right);
        }
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
                    ToolStripTextBoxFG = SystemColors.WindowText,
                    ToolStripTextBoxBG = SystemColors.Window,
                    TreeViewFG = SystemColors.WindowText,
                    TreeViewBG = SystemColors.Window,

                    FormFG = SystemColors.ControlText,
                    FormBG = SystemColors.Control,
                    ButtonFG = SystemColors.ControlText,
                    ButtonBG = SystemColors.Control,
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
                    ListViewColumnColors = new CustomPaint.ListViewColors() {
                        ColumnText = SystemColors.ControlText,
                        ColumnBackground = SystemColors.Window
                    },
                    TabControlTabColors = new CustomPaint.TabControlColors() {
                        TabText = SystemColors.ControlText,
                        ActiveTab = SystemColors.Control,
                        InactiveTab = SystemColors.Control,
                        TabStripBackground = SystemColors.Control
                    },
                    ToolStripItemDisabledText = Color.FromArgb(unchecked((int)0xFF6D6D6D))
                };
            }
        }

        public static Theme Inverted {
            get {
                return new Theme() {
                    ComboBoxFG = SystemColors.Window,
                    ComboBoxBG = SystemColors.WindowText,
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
                    ToolStripTextBoxFG = SystemColors.Window,
                    ToolStripTextBoxBG = SystemColors.WindowText,
                    TreeViewFG = SystemColors.Window,
                    TreeViewBG = SystemColors.WindowText,

                    FormFG = SystemColors.Control,
                    FormBG = SystemColors.ControlText,
                    ButtonFG = SystemColors.Control,
                    ButtonBG = SystemColors.ControlText,
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
                    ListViewColumnColors = new CustomPaint.ListViewColors() {
                        ColumnText = SystemColors.Control,
                        ColumnBackground = SystemColors.ControlText
                    },
                    TabControlTabColors = new CustomPaint.TabControlColors() {
                        TabText = SystemColors.Control,
                        ActiveTab = SystemColors.ControlText,
                        InactiveTab = SystemColors.ControlDarkDark,
                        TabStripBackground = SystemColors.ControlText
                    },
                    ToolStripItemDisabledText = Color.FromArgb(unchecked((int)0xFFB2B2B2))
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
                    CheckBoxFG = SystemColors.Control,
                    CheckBoxBG = SystemColors.ControlDarkDark,
                    CheckedListBoxFG = SystemColors.Control,
                    CheckedListBoxBG = SystemColors.ControlDarkDark,
                    ComboBoxFG = SystemColors.Control,
                    ComboBoxBG = SystemColors.ControlDarkDark,
                    ContextMenuStripFG = SystemColors.Control,
                    ContextMenuStripBG = SystemColors.ControlDarkDark,
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
                    ListViewColumnColors = new CustomPaint.ListViewColors() {
                        ColumnText = SystemColors.Control,
                        ColumnBackground = SystemColors.ControlDarkDark
                    },
                    TabControlTabColors = new CustomPaint.TabControlColors() {
                        TabText = SystemColors.Control,
                        ActiveTab = SystemColors.ControlDarkDark,
                        InactiveTab = SystemColors.ControlDark,
                        TabStripBackground = SystemColors.ControlDarkDark
                    },
                    ToolStripItemDisabledText = Color.FromArgb(unchecked((int)0xFFB2B2B2))
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
                    CheckBoxFG = textColor,
                    CheckBoxBG = backColor,
                    CheckedListBoxFG = textColor,
                    CheckedListBoxBG = backColor,
                    ComboBoxFG = textColor,
                    ComboBoxBG = backColor,
                    ContextMenuStripFG = altTextColor,
                    ContextMenuStripBG = altBackColor,
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
                    ToolStripDropDownFG = altTextColor,
                    ToolStripDropDownBG = altBackColor,
                    ToolStripDropDownButtonFG = altTextColor,
                    ToolStripDropDownButtonBG = altBackColor,
                    ToolStripMenuItemFG = altTextColor,
                    ToolStripMenuItemBG = altBackColor,
                    ToolStripProgressBarFG = altTextColor,
                    ToolStripProgressBarBG = altBackColor,
                    ToolStripSeparatorFG = altTextColor,
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
                    ListViewColumnColors = new CustomPaint.ListViewColors() {
                        ColumnText = textColor,
                        ColumnBackground = Color.FromArgb(unchecked((int)0xFF444449))
                    },
                    TabControlTabColors = new CustomPaint.TabControlColors() {
                        TabText = textColor,
                        ActiveTab = backColor,
                        InactiveTab = altBackColor,
                        TabStripBackground = backColor
                    },
                    ToolStripItemDisabledText = Color.FromArgb(unchecked((int)0xFFB2B2B2))
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
                    CheckBoxFG = Color.Blue,
                    CheckBoxBG = Color.Magenta,
                    CheckedListBoxFG = Color.Blue,
                    CheckedListBoxBG = Color.Magenta,
                    ComboBoxFG = Color.Blue,
                    ComboBoxBG = Color.Magenta,
                    ContextMenuStripFG = Color.Blue,
                    ContextMenuStripBG = Color.Magenta,
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
                    ListViewColumnColors = new CustomPaint.ListViewColors() {
                        ColumnText = Color.Blue,
                        ColumnBackground = Color.Magenta
                    },
                    TabControlTabColors = new CustomPaint.TabControlColors() {
                        TabText = Color.Blue,
                        ActiveTab = Color.Magenta,
                        InactiveTab = Color.Violet,
                        TabStripBackground = Color.Magenta
                    },
                    ToolStripItemDisabledText = Color.Green
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
        }
        // in c#, event handler signatures must exactly match the event delegate signature
        public static void ListView_DrawCustomColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e) => ListView_DrawCustomColumnHeader(sender, e, null);
        public static void ListView_DrawCustomColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e, ListViewColors? listViewColors = null) {
            // https://stackoverflow.com/a/42181044/2999220
            var listView = (ListView)sender;
            ListViewColors colors;
            if (listViewColors.HasValue) {
                colors = listViewColors.Value;
            } else {
                colors = (ListViewColors)listView.Tag;
            }

            using (var sf = new StringFormat() {
                    LineAlignment = StringAlignment.Center,
                    FormatFlags = StringFormatFlags.NoWrap,
                    Trimming = StringTrimming.EllipsisCharacter
                }) {
                switch (e.Header.TextAlign) {
                    case HorizontalAlignment.Left: {
                        sf.Alignment = listView.RightToLeft == RightToLeft.No ? StringAlignment.Near : StringAlignment.Far;
                        break;
                    }
                    case HorizontalAlignment.Right: {
                        sf.Alignment = listView.RightToLeft == RightToLeft.Yes ? StringAlignment.Near : StringAlignment.Far;
                        break;
                    }
                    case HorizontalAlignment.Center: {
                        sf.Alignment = StringAlignment.Center;
                        break;
                    }
                }

                var colRect = new Rectangle() {
                    X = e.Bounds.X + 1,
                    Y = e.Bounds.Y + 1,
                    Width = e.Bounds.Width - 1,
                    Height = e.Bounds.Height
                };

                e.Graphics.FillRectangle(new SolidBrush(colors.ColumnBackground), colRect);
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
            if (tabControlColors.HasValue) {
                colors = tabControlColors.Value;
            } else {
                colors = (TabControlColors)tabCtl.Tag;
            }

            // draw tab strip background

            Rectangle firstTabRect = tabCtl.GetTabRect(0);
            Rectangle tabStripBounds = tabCtl.Bounds;
            Rectangle allTabBounds = firstTabRect;

            if (tabCtl.Alignment == TabAlignment.Top) {
                tabStripBounds.Y = 0;
            } else if (tabCtl.Alignment == TabAlignment.Bottom) {
                tabStripBounds.Y = firstTabRect.Y;
            } else if (tabCtl.Alignment == TabAlignment.Left) {
                tabStripBounds.X = 0;
            } else if (tabCtl.Alignment == TabAlignment.Right) {
                tabStripBounds.X = firstTabRect.X;
            }

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
                    LineAlignment = StringAlignment.Center
                }) {
                tabRect = tabCtl.GetTabRect(e.Index);

                if (tabCtl.Alignment == TabAlignment.Top) {
                    if (tabCtl.SelectedIndex == e.Index) tabRect.Y -= 1; else tabRect.Y += 1;
                } else if (tabCtl.Alignment == TabAlignment.Bottom) {
                    if (tabCtl.SelectedIndex == e.Index) tabRect.Y += 1; else tabRect.Y -= 1;
                } else if (tabCtl.Alignment == TabAlignment.Left) {
                    sf.FormatFlags = StringFormatFlags.DirectionVertical;
                    if (tabCtl.SelectedIndex == e.Index) tabRect.X -= 2;
                } else if (tabCtl.Alignment == TabAlignment.Right) {
                    sf.FormatFlags = StringFormatFlags.DirectionVertical;
                    if (tabCtl.SelectedIndex != e.Index) tabRect.X -= 2;
                }
                if (!sf.FormatFlags.HasFlag(StringFormatFlags.DirectionVertical)) {
                    tabRect.Width += 2;
                    tabRect.X -= 1;
                } // sometimes tab text overflows tabRect and makes a second line - bad!

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
                this.getDisabledColor = e => disabledColor;
            }

            protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e) {
                // https://bytes.com/topic/visual-basic-net/answers/590331-specify-custom-disabled-text-color-toolstripprofessionalrendere
                if (e.Item.Enabled) {
                    base.OnRenderItemText(e);
                } else {
                    Color color = e.Item.Enabled ? e.TextColor : getDisabledColor(e.Item);

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
