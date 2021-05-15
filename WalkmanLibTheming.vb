Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms

Partial Public Class WalkmanLib
#Region "ApplyTheme"
    Shared Sub ApplyTheme(theme As Theme, form As Form, Optional allowSetOwnerDraw As Boolean = False)
        form.ForeColor = theme.FormFG
        form.BackColor = theme.FormBG
        ApplyTheme(theme, form.Controls, allowSetOwnerDraw)
    End Sub
    Shared Sub ApplyTheme(theme As Theme, controls As Collections.IEnumerable, Optional allowSetOwnerDraw As Boolean = False)
        '                                 for ToolStripItemCollection
        For Each item As ToolStripItem In controls.OfType(Of ToolStripItem)
            Select Case item.GetType()
                Case GetType(ToolStripButton)
                    item.ForeColor = theme.ToolStripButtonFG
                    item.BackColor = theme.ToolStripButtonBG
                Case GetType(ToolStripComboBox)
                    item.ForeColor = theme.ToolStripComboBoxFG
                    item.BackColor = theme.ToolStripComboBoxBG
                Case GetType(ToolStripDropDownButton)
                    item.ForeColor = theme.ToolStripDropDownButtonFG
                    item.BackColor = theme.ToolStripDropDownButtonBG
                    DirectCast(item, ToolStripDropDownButton).DropDown.ForeColor = theme.ToolStripDropDownFG
                    DirectCast(item, ToolStripDropDownButton).DropDown.BackColor = theme.ToolStripDropDownBG
                    ApplyTheme(theme, DirectCast(item, ToolStripDropDownButton).DropDownItems, allowSetOwnerDraw)
                Case GetType(ToolStripMenuItem) ' inherits ToolStripDropDownItem
                    item.ForeColor = theme.ToolStripMenuItemFG
                    item.BackColor = theme.ToolStripMenuItemBG
                    DirectCast(item, ToolStripMenuItem).DropDown.ForeColor = theme.ToolStripDropDownFG
                    DirectCast(item, ToolStripMenuItem).DropDown.BackColor = theme.ToolStripDropDownBG
                    ApplyTheme(theme, DirectCast(item, ToolStripMenuItem).DropDownItems, allowSetOwnerDraw)
                Case GetType(ToolStripProgressBar)
                    item.ForeColor = theme.ToolStripProgressBarFG
                    item.BackColor = theme.ToolStripProgressBarBG
                Case GetType(ToolStripSeparator)
                    item.ForeColor = theme.ToolStripSeparatorFG
                    item.BackColor = theme.ToolStripSeparatorBG
                Case GetType(ToolStripStatusLabel)
                    item.ForeColor = theme.ToolStripStatusLabelFG
                    item.BackColor = theme.ToolStripStatusLabelBG
                Case GetType(ToolStripSplitButton)
                    item.ForeColor = theme.ToolStripSplitButtonFG
                    item.BackColor = theme.ToolStripSplitButtonBG
                    DirectCast(item, ToolStripSplitButton).DropDown.ForeColor = theme.ToolStripDropDownFG
                    DirectCast(item, ToolStripSplitButton).DropDown.BackColor = theme.ToolStripDropDownBG
                    ApplyTheme(theme, DirectCast(item, ToolStripSplitButton).DropDownItems, allowSetOwnerDraw)
                Case GetType(ToolStripTextBox)
                    item.ForeColor = theme.ToolStripTextBoxFG
                    item.BackColor = theme.ToolStripTextBoxBG
                Case Else
                    item.ForeColor = theme.OtherFG
                    item.BackColor = theme.OtherBG
            End Select
        Next

        For Each ctl As Control In controls.OfType(Of Control)
            Select Case ctl.GetType()
                Case GetType(Button)
                    ctl.ForeColor = theme.ButtonFG
                    ctl.BackColor = theme.ButtonBG
                    If theme.ButtonBG = SystemColors.Control Then
                        DirectCast(ctl, Button).UseVisualStyleBackColor = True
                    End If
                Case GetType(Label)
                    ctl.ForeColor = theme.LabelFG
                    ctl.BackColor = theme.LabelBG
                Case GetType(LinkLabel)
                    ctl.ForeColor = theme.LinkLabelFG
                    ctl.BackColor = theme.LinkLabelBG
                    DirectCast(ctl, LinkLabel).LinkColor = theme.LinkLabelLinkColor
                    DirectCast(ctl, LinkLabel).VisitedLinkColor = theme.LinkLabelVisitedLinkColor
                    DirectCast(ctl, LinkLabel).ActiveLinkColor = theme.LinkLabelActiveLinkColor
                Case GetType(ComboBox)
                    ctl.ForeColor = theme.ComboBoxFG
                    ctl.BackColor = theme.ComboBoxBG
                Case GetType(CheckBox)
                    ctl.ForeColor = theme.CheckBoxFG
                    ctl.BackColor = theme.CheckBoxBG
                Case GetType(RadioButton)
                    ctl.ForeColor = theme.RadioButtonFG
                    ctl.BackColor = theme.RadioButtonBG
                Case GetType(TextBox)
                    If DirectCast(ctl, TextBox).ReadOnly Then
                        ctl.ForeColor = theme.TextBoxReadOnlyFG
                        ctl.BackColor = theme.TextBoxReadOnlyBG
                    Else
                        ctl.ForeColor = theme.TextBoxFG
                        ctl.BackColor = theme.TextBoxBG
                    End If
                Case GetType(NumericUpDown)
                    ctl.ForeColor = theme.NumericUpDownFG
                    ctl.BackColor = theme.NumericUpDownBG
                Case GetType(DomainUpDown)
                    ctl.ForeColor = theme.DomainUpDownFG
                    ctl.BackColor = theme.DomainUpDownBG
                Case GetType(ListView)
                    If allowSetOwnerDraw Then DirectCast(ctl, ListView).OwnerDraw = theme.ListViewOwnerDraw
                    ctl.ForeColor = theme.ListViewFG
                    ctl.BackColor = theme.ListViewBG
                Case GetType(ListBox)
                    ctl.ForeColor = theme.ListBoxFG
                    ctl.BackColor = theme.ListBoxBG
                Case GetType(CheckedListBox)
                    ctl.ForeColor = theme.CheckedListBoxFG
                    ctl.BackColor = theme.CheckedListBoxBG
                Case GetType(TreeView)
                    ctl.ForeColor = theme.TreeViewFG
                    ctl.BackColor = theme.TreeViewBG
                    DirectCast(ctl, TreeView).LineColor = theme.TreeViewNodeLineColor
                Case GetType(PictureBox)
                    ctl.ForeColor = theme.PictureBoxFG
                    ctl.BackColor = theme.PictureBoxBG
                Case GetType(TrackBar)
                    ctl.ForeColor = theme.TrackBarFG
                    ctl.BackColor = theme.TrackBarBG
                Case GetType(ProgressBar)
                    ctl.ForeColor = theme.ProgressBarFG
                    ctl.BackColor = theme.ProgressBarBG
                Case GetType(PropertyGrid)
                    ctl.ForeColor = theme.PropertyGridFG
                    ctl.BackColor = theme.PropertyGridBG
                Case GetType(GroupBox)
                    ctl.ForeColor = theme.GroupBoxFG
                    ctl.BackColor = theme.GroupBoxBG
                    ApplyTheme(theme, DirectCast(ctl, GroupBox).Controls, allowSetOwnerDraw)

                Case GetType(SplitContainer)
                    ctl.ForeColor = theme.SplitContainerFG
                    ctl.BackColor = theme.SplitContainerBG
                    ApplyTheme(theme, DirectCast(ctl, SplitContainer).Controls, allowSetOwnerDraw)
                Case GetType(SplitterPanel)
                    ctl.ForeColor = theme.SplitterPanelFG
                    ctl.BackColor = theme.SplitterPanelBG
                    ApplyTheme(theme, DirectCast(ctl, SplitterPanel).Controls, allowSetOwnerDraw)

                Case GetType(TabControl)
                    If allowSetOwnerDraw Then DirectCast(ctl, TabControl).DrawMode =
                        If(theme.TabControlOwnerDraw, TabDrawMode.OwnerDrawFixed, TabDrawMode.Normal)
                    ctl.ForeColor = theme.TabControlFG
                    ctl.BackColor = theme.TabControlBG
                    ApplyTheme(theme, DirectCast(ctl, TabControl).Controls, allowSetOwnerDraw)
                Case GetType(TabPage)
                    ctl.ForeColor = theme.TabPageFG
                    ctl.BackColor = theme.TabPageBG
                    If theme.TabPageBG = Color.Transparent Then
                        DirectCast(ctl, TabPage).UseVisualStyleBackColor = True
                    End If
                    ApplyTheme(theme, DirectCast(ctl, TabPage).Controls, allowSetOwnerDraw)

                Case GetType(MenuStrip)
                    ctl.ForeColor = theme.MenuStripFG
                    ctl.BackColor = theme.MenuStripBG
                    ApplyTheme(theme, DirectCast(ctl, MenuStrip).Items, allowSetOwnerDraw)
                Case GetType(StatusStrip)
                    ctl.ForeColor = theme.StatusStripFG
                    ctl.BackColor = theme.StatusStripBG
                    ApplyTheme(theme, DirectCast(ctl, StatusStrip).Items, allowSetOwnerDraw)
                Case GetType(ToolStrip)
                    ctl.ForeColor = theme.ToolStripFG
                    ctl.BackColor = theme.ToolStripBG
                    ApplyTheme(theme, DirectCast(ctl, ToolStrip).Items, allowSetOwnerDraw)
                Case GetType(ContextMenuStrip)
                    ctl.ForeColor = theme.ContextMenuStripFG
                    ctl.BackColor = theme.ContextMenuStripBG
                    ApplyTheme(theme, DirectCast(ctl, ContextMenuStrip).Items, allowSetOwnerDraw)

                Case Else
                    ctl.ForeColor = theme.OtherFG
                    ctl.BackColor = theme.OtherBG
            End Select
        Next
    End Sub
#End Region

#Region "Theme class"
    Public Class Theme
        Public FormFG As Color
        Public FormBG As Color
        Public ButtonFG As Color
        Public ButtonBG As Color
        Public LabelFG As Color
        Public LabelBG As Color
        Public LinkLabelFG As Color
        Public LinkLabelBG As Color
        Public LinkLabelLinkColor As Color
        Public LinkLabelVisitedLinkColor As Color
        Public LinkLabelActiveLinkColor As Color
        Public ComboBoxFG As Color
        Public ComboBoxBG As Color
        Public CheckBoxFG As Color
        Public CheckBoxBG As Color
        Public RadioButtonFG As Color
        Public RadioButtonBG As Color
        Public TextBoxFG As Color
        Public TextBoxBG As Color
        Public TextBoxReadOnlyFG As Color
        Public TextBoxReadOnlyBG As Color
        Public NumericUpDownFG As Color
        Public NumericUpDownBG As Color
        Public DomainUpDownFG As Color
        Public DomainUpDownBG As Color
        Public ListViewFG As Color
        Public ListViewBG As Color
        Public ListViewOwnerDraw As Boolean
        Public ListViewColumnColors As CustomPaint.ListViewColors
        Public ListBoxFG As Color
        Public ListBoxBG As Color
        Public CheckedListBoxFG As Color
        Public CheckedListBoxBG As Color
        Public TreeViewFG As Color
        Public TreeViewBG As Color
        Public TreeViewNodeLineColor As Color
        Public PictureBoxFG As Color
        Public PictureBoxBG As Color
        Public TrackBarFG As Color
        Public TrackBarBG As Color
        Public ProgressBarFG As Color
        Public ProgressBarBG As Color
        Public PropertyGridFG As Color
        Public PropertyGridBG As Color
        Public GroupBoxFG As Color
        Public GroupBoxBG As Color
        Public SplitContainerFG As Color
        Public SplitContainerBG As Color
        Public SplitterPanelFG As Color
        Public SplitterPanelBG As Color
        Public TabControlFG As Color
        Public TabControlBG As Color
        Public TabControlOwnerDraw As Boolean
        Public TabControlTabColors As CustomPaint.TabControlColors
        Public TabPageFG As Color
        Public TabPageBG As Color
        Public MenuStripFG As Color
        Public MenuStripBG As Color
        Public StatusStripFG As Color
        Public StatusStripBG As Color
        Public ToolStripFG As Color
        Public ToolStripBG As Color
        Public ContextMenuStripFG As Color
        Public ContextMenuStripBG As Color

        Public ToolStripButtonFG As Color
        Public ToolStripButtonBG As Color
        Public ToolStripComboBoxFG As Color
        Public ToolStripComboBoxBG As Color
        Public ToolStripDropDownFG As Color
        Public ToolStripDropDownBG As Color
        Public ToolStripDropDownButtonFG As Color
        Public ToolStripDropDownButtonBG As Color
        Public ToolStripMenuItemFG As Color
        Public ToolStripMenuItemBG As Color
        Public ToolStripProgressBarFG As Color
        Public ToolStripProgressBarBG As Color
        Public ToolStripSeparatorFG As Color
        Public ToolStripSeparatorBG As Color
        Public ToolStripStatusLabelFG As Color
        Public ToolStripStatusLabelBG As Color
        Public ToolStripSplitButtonFG As Color
        Public ToolStripSplitButtonBG As Color
        Public ToolStripTextBoxFG As Color
        Public ToolStripTextBoxBG As Color

        Public OtherFG As Color
        Public OtherBG As Color

        Public Shared Operator <>(left As Theme, right As Theme) As Boolean
            Return Not left.Equals(right)
        End Operator
        Public Shared Operator =(left As Theme, right As Theme) As Boolean
            Return left.Equals(right)
        End Operator
        Public Overrides Function Equals(obj As Object) As Boolean
            If obj Is Nothing Then Return False

            Dim srcType As Type = [GetType]()
            Dim dstType As Type = obj.GetType
            If srcType IsNot dstType Then Return False

            For Each fi As Reflection.FieldInfo In srcType.GetFields(Reflection.BindingFlags.Public Or Reflection.BindingFlags.Instance)
                Dim srcValue As Object = srcType.GetField(fi.Name).GetValue(Me)
                Dim dstValue As Object = dstType.GetField(fi.Name).GetValue(obj)

                If srcValue IsNot dstValue AndAlso (srcValue Is Nothing OrElse Not srcValue.Equals(dstValue)) Then
                    Return False
                End If
            Next
            Return True
        End Function

        Public Shared ReadOnly Property [Default] As Theme
            Get
                Return New Theme With {
                    .ComboBoxFG = SystemColors.WindowText,
                    .ComboBoxBG = SystemColors.Window,
                    .CheckedListBoxFG = SystemColors.WindowText,
                    .CheckedListBoxBG = SystemColors.Window,
                    .DomainUpDownFG = SystemColors.WindowText,
                    .DomainUpDownBG = SystemColors.Window,
                    .ListViewFG = SystemColors.WindowText,
                    .ListViewBG = SystemColors.Window,
                    .ListBoxFG = SystemColors.WindowText,
                    .ListBoxBG = SystemColors.Window,
                    .NumericUpDownFG = SystemColors.WindowText,
                    .NumericUpDownBG = SystemColors.Window,
                    .TextBoxFG = SystemColors.WindowText,
                    .TextBoxBG = SystemColors.Window,
                    .ToolStripComboBoxFG = SystemColors.WindowText,
                    .ToolStripComboBoxBG = SystemColors.Window,
                    .ToolStripTextBoxFG = SystemColors.WindowText,
                    .ToolStripTextBoxBG = SystemColors.Window,
                    .TreeViewFG = SystemColors.WindowText,
                    .TreeViewBG = SystemColors.Window,
 _
                    .FormFG = SystemColors.ControlText,
                    .FormBG = SystemColors.Control,
                    .ButtonFG = SystemColors.ControlText,
                    .ButtonBG = SystemColors.Control,
                    .CheckBoxFG = SystemColors.ControlText,
                    .CheckBoxBG = SystemColors.Control,
                    .GroupBoxFG = SystemColors.ControlText,
                    .GroupBoxBG = SystemColors.Control,
                    .LabelFG = SystemColors.ControlText,
                    .LabelBG = SystemColors.Control,
                    .LinkLabelFG = SystemColors.ControlText,
                    .LinkLabelBG = SystemColors.Control,
                    .MenuStripFG = SystemColors.ControlText,
                    .MenuStripBG = SystemColors.Control,
                    .PropertyGridFG = SystemColors.ControlText,
                    .PropertyGridBG = SystemColors.Control,
                    .PictureBoxFG = SystemColors.ControlText,
                    .PictureBoxBG = SystemColors.Control,
                    .RadioButtonFG = SystemColors.ControlText,
                    .RadioButtonBG = SystemColors.Control,
                    .StatusStripFG = SystemColors.ControlText,
                    .StatusStripBG = SystemColors.Control,
                    .SplitContainerFG = SystemColors.ControlText,
                    .SplitContainerBG = SystemColors.Control,
                    .SplitterPanelFG = SystemColors.ControlText,
                    .SplitterPanelBG = SystemColors.Control,
                    .ToolStripButtonFG = SystemColors.ControlText,
                    .ToolStripButtonBG = SystemColors.Control,
                    .ToolStripDropDownButtonFG = SystemColors.ControlText,
                    .ToolStripDropDownButtonBG = SystemColors.Control,
                    .ToolStripMenuItemFG = SystemColors.ControlText,
                    .ToolStripMenuItemBG = SystemColors.Control,
                    .ToolStripStatusLabelFG = SystemColors.ControlText,
                    .ToolStripStatusLabelBG = SystemColors.Control,
                    .ToolStripSplitButtonFG = SystemColors.ControlText,
                    .ToolStripSplitButtonBG = SystemColors.Control,
                    .TabControlFG = SystemColors.ControlText,
                    .TabControlBG = SystemColors.Control,
                    .ContextMenuStripFG = SystemColors.ControlText,
                    .ContextMenuStripBG = Nothing,
                    .ToolStripFG = SystemColors.ControlText,
                    .ToolStripBG = Nothing,
                    .ToolStripDropDownFG = SystemColors.ControlText,
                    .ToolStripDropDownBG = Nothing,
 _
                    .LinkLabelLinkColor = Color.FromArgb(&HFF0000FF),
                    .LinkLabelVisitedLinkColor = Color.FromArgb(&HFF800080),
                    .LinkLabelActiveLinkColor = Color.Red,
                    .ProgressBarFG = SystemColors.Highlight,
                    .ProgressBarBG = SystemColors.Control,
                    .TabPageFG = SystemColors.ControlText,
                    .TabPageBG = Color.Transparent,
                    .TextBoxReadOnlyFG = SystemColors.WindowText,
                    .TextBoxReadOnlyBG = SystemColors.Control,
                    .ToolStripProgressBarFG = SystemColors.Highlight,
                    .ToolStripProgressBarBG = SystemColors.Control,
                    .ToolStripSeparatorFG = SystemColors.ControlDark,
                    .ToolStripSeparatorBG = SystemColors.Control,
                    .TrackBarFG = SystemColors.WindowText,
                    .TrackBarBG = SystemColors.Control,
                    .TreeViewNodeLineColor = Color.Black,
                    .OtherFG = SystemColors.ControlText,
                    .OtherBG = SystemColors.Control,
 _
                    .ListViewOwnerDraw = False,
                    .TabControlOwnerDraw = False,
                    .ListViewColumnColors = New CustomPaint.ListViewColors With {
                        .ColumnText = SystemColors.ControlText,
                        .ColumnBackground = SystemColors.Window
                    },
                    .TabControlTabColors = New CustomPaint.TabControlColors With {
                        .TabText = SystemColors.ControlText,
                        .ActiveTab = SystemColors.Control,
                        .InactiveTab = SystemColors.Control,
                        .TabStripBackground = SystemColors.Control
                    }
                }
            End Get
        End Property

        Public Shared ReadOnly Property Inverted As Theme
            Get
                Return New Theme With {
                    .ComboBoxFG = SystemColors.Window,
                    .ComboBoxBG = SystemColors.WindowText,
                    .CheckedListBoxFG = SystemColors.Window,
                    .CheckedListBoxBG = SystemColors.WindowText,
                    .DomainUpDownFG = SystemColors.Window,
                    .DomainUpDownBG = SystemColors.WindowText,
                    .ListViewFG = SystemColors.Window,
                    .ListViewBG = SystemColors.WindowText,
                    .ListBoxFG = SystemColors.Window,
                    .ListBoxBG = SystemColors.WindowText,
                    .NumericUpDownFG = SystemColors.Window,
                    .NumericUpDownBG = SystemColors.WindowText,
                    .TextBoxFG = SystemColors.Window,
                    .TextBoxBG = SystemColors.WindowText,
                    .ToolStripComboBoxFG = SystemColors.Window,
                    .ToolStripComboBoxBG = SystemColors.WindowText,
                    .ToolStripTextBoxFG = SystemColors.Window,
                    .ToolStripTextBoxBG = SystemColors.WindowText,
                    .TreeViewFG = SystemColors.Window,
                    .TreeViewBG = SystemColors.WindowText,
 _
                    .FormFG = SystemColors.Control,
                    .FormBG = SystemColors.ControlText,
                    .ButtonFG = SystemColors.Control,
                    .ButtonBG = SystemColors.ControlText,
                    .CheckBoxFG = SystemColors.Control,
                    .CheckBoxBG = SystemColors.ControlText,
                    .ContextMenuStripFG = SystemColors.Control,
                    .ContextMenuStripBG = SystemColors.ControlText,
                    .GroupBoxFG = SystemColors.Control,
                    .GroupBoxBG = SystemColors.ControlText,
                    .LabelFG = SystemColors.Control,
                    .LabelBG = SystemColors.ControlText,
                    .LinkLabelFG = SystemColors.Control,
                    .LinkLabelBG = SystemColors.ControlText,
                    .MenuStripFG = SystemColors.Control,
                    .MenuStripBG = SystemColors.ControlText,
                    .PropertyGridFG = SystemColors.Control,
                    .PropertyGridBG = SystemColors.ControlText,
                    .PictureBoxFG = SystemColors.Control,
                    .PictureBoxBG = SystemColors.ControlText,
                    .RadioButtonFG = SystemColors.Control,
                    .RadioButtonBG = SystemColors.ControlText,
                    .StatusStripFG = SystemColors.Control,
                    .StatusStripBG = SystemColors.ControlText,
                    .SplitContainerFG = SystemColors.Control,
                    .SplitContainerBG = SystemColors.ControlText,
                    .SplitterPanelFG = SystemColors.Control,
                    .SplitterPanelBG = SystemColors.ControlText,
                    .ToolStripFG = SystemColors.Control,
                    .ToolStripBG = SystemColors.ControlText,
                    .ToolStripButtonFG = SystemColors.Control,
                    .ToolStripButtonBG = SystemColors.ControlText,
                    .ToolStripDropDownFG = SystemColors.Control,
                    .ToolStripDropDownBG = SystemColors.ControlText,
                    .ToolStripDropDownButtonFG = SystemColors.Control,
                    .ToolStripDropDownButtonBG = SystemColors.ControlText,
                    .ToolStripMenuItemFG = SystemColors.Control,
                    .ToolStripMenuItemBG = SystemColors.ControlText,
                    .ToolStripStatusLabelFG = SystemColors.Control,
                    .ToolStripStatusLabelBG = SystemColors.ControlText,
                    .ToolStripSplitButtonFG = SystemColors.Control,
                    .ToolStripSplitButtonBG = SystemColors.ControlText,
                    .TabControlFG = SystemColors.Control,
                    .TabControlBG = SystemColors.ControlText,
 _
                    .LinkLabelLinkColor = Color.Cyan,
                    .LinkLabelVisitedLinkColor = Color.Purple,
                    .LinkLabelActiveLinkColor = Color.Red,
                    .ProgressBarFG = SystemColors.Highlight,
                    .ProgressBarBG = SystemColors.ControlText,
                    .TabPageFG = SystemColors.Control,
                    .TabPageBG = SystemColors.ControlText,
                    .TextBoxReadOnlyFG = SystemColors.Window,
                    .TextBoxReadOnlyBG = SystemColors.ControlText,
                    .ToolStripProgressBarFG = SystemColors.Highlight,
                    .ToolStripProgressBarBG = SystemColors.ControlText,
                    .ToolStripSeparatorFG = SystemColors.ControlDark,
                    .ToolStripSeparatorBG = SystemColors.ControlDarkDark,
                    .TrackBarFG = SystemColors.Window,
                    .TrackBarBG = SystemColors.ControlText,
                    .TreeViewNodeLineColor = SystemColors.Window,
                    .OtherFG = SystemColors.Control,
                    .OtherBG = SystemColors.ControlText,
 _
                    .ListViewOwnerDraw = True,
                    .TabControlOwnerDraw = True,
                    .ListViewColumnColors = New CustomPaint.ListViewColors With {
                        .ColumnText = SystemColors.Control,
                        .ColumnBackground = SystemColors.ControlText
                    },
                    .TabControlTabColors = New CustomPaint.TabControlColors With {
                        .TabText = SystemColors.Control,
                        .ActiveTab = SystemColors.ControlText,
                        .InactiveTab = SystemColors.ControlDarkDark,
                        .TabStripBackground = SystemColors.ControlText
                    }
                }
            End Get
        End Property

        Public Shared ReadOnly Property SystemDark As Theme
            Get
                Return New Theme With {
                    .FormFG = SystemColors.Control,
                    .FormBG = SystemColors.ControlDarkDark,
                    .ButtonFG = SystemColors.Control,
                    .ButtonBG = SystemColors.ControlDarkDark,
                    .CheckBoxFG = SystemColors.Control,
                    .CheckBoxBG = SystemColors.ControlDarkDark,
                    .CheckedListBoxFG = SystemColors.Control,
                    .CheckedListBoxBG = SystemColors.ControlDarkDark,
                    .ComboBoxFG = SystemColors.Control,
                    .ComboBoxBG = SystemColors.ControlDarkDark,
                    .ContextMenuStripFG = SystemColors.Control,
                    .ContextMenuStripBG = SystemColors.ControlDarkDark,
                    .DomainUpDownFG = SystemColors.Control,
                    .DomainUpDownBG = SystemColors.ControlDarkDark,
                    .GroupBoxFG = SystemColors.Control,
                    .GroupBoxBG = SystemColors.ControlDarkDark,
                    .LabelFG = SystemColors.Control,
                    .LabelBG = SystemColors.ControlDarkDark,
                    .LinkLabelFG = SystemColors.Control,
                    .LinkLabelBG = SystemColors.ControlDarkDark,
                    .LinkLabelLinkColor = Color.FromArgb(&HFF0000FF),
                    .LinkLabelVisitedLinkColor = Color.FromArgb(&HFF800080),
                    .LinkLabelActiveLinkColor = Color.Red,
                    .ListBoxFG = SystemColors.Control,
                    .ListBoxBG = SystemColors.ControlDarkDark,
                    .ListViewFG = SystemColors.Control,
                    .ListViewBG = SystemColors.ControlDarkDark,
                    .MenuStripFG = SystemColors.Control,
                    .MenuStripBG = SystemColors.ControlDarkDark,
                    .NumericUpDownFG = SystemColors.Control,
                    .NumericUpDownBG = SystemColors.ControlDarkDark,
                    .PictureBoxFG = SystemColors.Control,
                    .PictureBoxBG = SystemColors.ControlDarkDark,
                    .ProgressBarFG = SystemColors.Control,
                    .ProgressBarBG = SystemColors.ControlDarkDark,
                    .PropertyGridFG = SystemColors.Control,
                    .PropertyGridBG = SystemColors.ControlDarkDark,
                    .RadioButtonFG = SystemColors.Control,
                    .RadioButtonBG = SystemColors.ControlDarkDark,
                    .SplitContainerFG = SystemColors.Control,
                    .SplitContainerBG = SystemColors.ControlDarkDark,
                    .SplitterPanelFG = SystemColors.Control,
                    .SplitterPanelBG = SystemColors.ControlDarkDark,
                    .StatusStripFG = SystemColors.Control,
                    .StatusStripBG = SystemColors.ControlDarkDark,
                    .TabControlFG = SystemColors.Control,
                    .TabControlBG = SystemColors.ControlDarkDark,
                    .TabPageFG = SystemColors.Control,
                    .TabPageBG = SystemColors.ControlDarkDark,
                    .TextBoxFG = SystemColors.Control,
                    .TextBoxBG = SystemColors.ControlDarkDark,
                    .TextBoxReadOnlyFG = SystemColors.Control,
                    .TextBoxReadOnlyBG = SystemColors.ControlDarkDark,
                    .ToolStripFG = SystemColors.Control,
                    .ToolStripBG = SystemColors.ControlDarkDark,
                    .ToolStripButtonFG = SystemColors.Control,
                    .ToolStripButtonBG = SystemColors.ControlDarkDark,
                    .ToolStripComboBoxFG = SystemColors.Control,
                    .ToolStripComboBoxBG = SystemColors.ControlDarkDark,
                    .ToolStripDropDownFG = SystemColors.Control,
                    .ToolStripDropDownBG = SystemColors.ControlDarkDark,
                    .ToolStripDropDownButtonFG = SystemColors.Control,
                    .ToolStripDropDownButtonBG = SystemColors.ControlDarkDark,
                    .ToolStripMenuItemFG = SystemColors.Control,
                    .ToolStripMenuItemBG = SystemColors.ControlDarkDark,
                    .ToolStripProgressBarFG = SystemColors.Control,
                    .ToolStripProgressBarBG = SystemColors.ControlDarkDark,
                    .ToolStripSeparatorFG = SystemColors.Control,
                    .ToolStripSeparatorBG = SystemColors.ControlDarkDark,
                    .ToolStripSplitButtonFG = SystemColors.Control,
                    .ToolStripSplitButtonBG = SystemColors.ControlDarkDark,
                    .ToolStripStatusLabelFG = SystemColors.Control,
                    .ToolStripStatusLabelBG = SystemColors.ControlDarkDark,
                    .ToolStripTextBoxFG = SystemColors.Control,
                    .ToolStripTextBoxBG = SystemColors.ControlDarkDark,
                    .TrackBarFG = SystemColors.Control,
                    .TrackBarBG = SystemColors.ControlDarkDark,
                    .TreeViewFG = SystemColors.Control,
                    .TreeViewBG = SystemColors.ControlDarkDark,
                    .TreeViewNodeLineColor = SystemColors.Window,
                    .OtherFG = SystemColors.Control,
                    .OtherBG = SystemColors.ControlDarkDark,
 _
                    .ListViewOwnerDraw = False,
                    .TabControlOwnerDraw = False,
                    .ListViewColumnColors = New CustomPaint.ListViewColors With {
                        .ColumnText = SystemColors.Control,
                        .ColumnBackground = SystemColors.ControlDarkDark
                    },
                    .TabControlTabColors = New CustomPaint.TabControlColors With {
                        .TabText = SystemColors.Control,
                        .ActiveTab = SystemColors.ControlDarkDark,
                        .InactiveTab = SystemColors.ControlDark,
                        .TabStripBackground = SystemColors.ControlDarkDark
                    }
                }
            End Get
        End Property

        Public Shared ReadOnly Property Dark As Theme
            Get
                Dim textColor As Color = Color.FromArgb(&HFFCFCFCF)
                Dim backColor As Color = Color.FromArgb(&HFF303030)
                Dim altTextColor As Color = Color.FromArgb(&HFFFFFFFF)
                Dim altBackColor As Color = Color.FromArgb(&HFF515259)

                Return New Theme With {
                    .FormFG = textColor,
                    .FormBG = backColor,
                    .ButtonFG = textColor,
                    .ButtonBG = backColor,
                    .CheckBoxFG = textColor,
                    .CheckBoxBG = backColor,
                    .CheckedListBoxFG = textColor,
                    .CheckedListBoxBG = backColor,
                    .ComboBoxFG = textColor,
                    .ComboBoxBG = backColor,
                    .ContextMenuStripFG = altTextColor,
                    .ContextMenuStripBG = altBackColor,
                    .DomainUpDownFG = textColor,
                    .DomainUpDownBG = backColor,
                    .GroupBoxFG = textColor,
                    .GroupBoxBG = backColor,
                    .LabelFG = textColor,
                    .LabelBG = backColor,
                    .LinkLabelFG = textColor,
                    .LinkLabelBG = backColor,
                    .LinkLabelLinkColor = Color.Cyan,
                    .LinkLabelVisitedLinkColor = Color.Purple,
                    .LinkLabelActiveLinkColor = Color.Red,
                    .ListBoxFG = textColor,
                    .ListBoxBG = backColor,
                    .ListViewFG = textColor,
                    .ListViewBG = backColor,
                    .MenuStripFG = altTextColor,
                    .MenuStripBG = altBackColor,
                    .NumericUpDownFG = textColor,
                    .NumericUpDownBG = backColor,
                    .PictureBoxFG = textColor,
                    .PictureBoxBG = backColor,
                    .ProgressBarFG = textColor,
                    .ProgressBarBG = backColor,
                    .PropertyGridFG = textColor,
                    .PropertyGridBG = backColor,
                    .RadioButtonFG = textColor,
                    .RadioButtonBG = backColor,
                    .SplitContainerFG = textColor,
                    .SplitContainerBG = backColor,
                    .SplitterPanelFG = textColor,
                    .SplitterPanelBG = backColor,
                    .StatusStripFG = altTextColor,
                    .StatusStripBG = altBackColor,
                    .TabControlFG = textColor,
                    .TabControlBG = backColor,
                    .TabPageFG = textColor,
                    .TabPageBG = backColor,
                    .TextBoxFG = textColor,
                    .TextBoxBG = backColor,
                    .TextBoxReadOnlyFG = textColor,
                    .TextBoxReadOnlyBG = Color.FromArgb(&HFF202020),
                    .ToolStripFG = altTextColor,
                    .ToolStripBG = altBackColor,
                    .ToolStripButtonFG = altTextColor,
                    .ToolStripButtonBG = altBackColor,
                    .ToolStripComboBoxFG = altTextColor,
                    .ToolStripComboBoxBG = altBackColor,
                    .ToolStripDropDownFG = altTextColor,
                    .ToolStripDropDownBG = altBackColor,
                    .ToolStripDropDownButtonFG = altTextColor,
                    .ToolStripDropDownButtonBG = altBackColor,
                    .ToolStripMenuItemFG = altTextColor,
                    .ToolStripMenuItemBG = altBackColor,
                    .ToolStripProgressBarFG = altTextColor,
                    .ToolStripProgressBarBG = altBackColor,
                    .ToolStripSeparatorFG = altTextColor,
                    .ToolStripSeparatorBG = altBackColor,
                    .ToolStripSplitButtonFG = altTextColor,
                    .ToolStripSplitButtonBG = altBackColor,
                    .ToolStripStatusLabelFG = altTextColor,
                    .ToolStripStatusLabelBG = altBackColor,
                    .ToolStripTextBoxFG = altTextColor,
                    .ToolStripTextBoxBG = altBackColor,
                    .TrackBarFG = textColor,
                    .TrackBarBG = backColor,
                    .TreeViewFG = textColor,
                    .TreeViewBG = backColor,
                    .TreeViewNodeLineColor = textColor,
                    .OtherFG = textColor,
                    .OtherBG = backColor,
 _
                    .ListViewOwnerDraw = False,
                    .TabControlOwnerDraw = False,
                    .ListViewColumnColors = New CustomPaint.ListViewColors With {
                        .ColumnText = textColor,
                        .ColumnBackground = Color.FromArgb(&HFF444449)
                    },
                    .TabControlTabColors = New CustomPaint.TabControlColors With {
                        .TabText = textColor,
                        .ActiveTab = backColor,
                        .InactiveTab = altBackColor,
                        .TabStripBackground = backColor
                    }
                }
            End Get
        End Property

        Public Shared ReadOnly Property Test As Theme
            Get
                Return New Theme With {
                    .FormFG = Color.Blue,
                    .FormBG = Color.Magenta,
                    .ButtonFG = Color.Blue,
                    .ButtonBG = Color.Magenta,
                    .CheckBoxFG = Color.Blue,
                    .CheckBoxBG = Color.Magenta,
                    .CheckedListBoxFG = Color.Blue,
                    .CheckedListBoxBG = Color.Magenta,
                    .ComboBoxFG = Color.Blue,
                    .ComboBoxBG = Color.Magenta,
                    .ContextMenuStripFG = Color.Blue,
                    .ContextMenuStripBG = Color.Magenta,
                    .DomainUpDownFG = Color.Blue,
                    .DomainUpDownBG = Color.Magenta,
                    .GroupBoxFG = Color.Blue,
                    .GroupBoxBG = Color.Magenta,
                    .LabelFG = Color.Blue,
                    .LabelBG = Color.Magenta,
                    .LinkLabelFG = Color.Blue,
                    .LinkLabelBG = Color.Magenta,
                    .LinkLabelLinkColor = Color.Maroon,
                    .LinkLabelVisitedLinkColor = Color.Purple,
                    .LinkLabelActiveLinkColor = Color.Green,
                    .ListBoxFG = Color.Blue,
                    .ListBoxBG = Color.Magenta,
                    .ListViewFG = Color.Blue,
                    .ListViewBG = Color.Magenta,
                    .MenuStripFG = Color.Blue,
                    .MenuStripBG = Color.Magenta,
                    .NumericUpDownFG = Color.Blue,
                    .NumericUpDownBG = Color.Magenta,
                    .PictureBoxFG = Color.Blue,
                    .PictureBoxBG = Color.Magenta,
                    .ProgressBarFG = Color.Blue,
                    .ProgressBarBG = Color.Magenta,
                    .PropertyGridFG = Color.Blue,
                    .PropertyGridBG = Color.Magenta,
                    .RadioButtonFG = Color.Blue,
                    .RadioButtonBG = Color.Magenta,
                    .SplitContainerFG = Color.Blue,
                    .SplitContainerBG = Color.Magenta,
                    .SplitterPanelFG = Color.Blue,
                    .SplitterPanelBG = Color.Magenta,
                    .StatusStripFG = Color.Blue,
                    .StatusStripBG = Color.Magenta,
                    .TabControlFG = Color.Blue,
                    .TabControlBG = Color.Magenta,
                    .TabPageFG = Color.Blue,
                    .TabPageBG = Color.Magenta,
                    .TextBoxFG = Color.Blue,
                    .TextBoxBG = Color.Magenta,
                    .TextBoxReadOnlyFG = Color.Blue,
                    .TextBoxReadOnlyBG = Color.Magenta,
                    .ToolStripFG = Color.Blue,
                    .ToolStripBG = Color.Magenta,
                    .ToolStripButtonFG = Color.Blue,
                    .ToolStripButtonBG = Color.Magenta,
                    .ToolStripComboBoxFG = Color.Blue,
                    .ToolStripComboBoxBG = Color.Magenta,
                    .ToolStripDropDownFG = Color.Blue,
                    .ToolStripDropDownBG = Color.Magenta,
                    .ToolStripDropDownButtonFG = Color.Blue,
                    .ToolStripDropDownButtonBG = Color.Magenta,
                    .ToolStripMenuItemFG = Color.Blue,
                    .ToolStripMenuItemBG = Color.Magenta,
                    .ToolStripProgressBarFG = Color.Blue,
                    .ToolStripProgressBarBG = Color.Magenta,
                    .ToolStripSeparatorFG = Color.Blue,
                    .ToolStripSeparatorBG = Color.Magenta,
                    .ToolStripSplitButtonFG = Color.Blue,
                    .ToolStripSplitButtonBG = Color.Magenta,
                    .ToolStripStatusLabelFG = Color.Blue,
                    .ToolStripStatusLabelBG = Color.Magenta,
                    .ToolStripTextBoxFG = Color.Blue,
                    .ToolStripTextBoxBG = Color.Magenta,
                    .TrackBarFG = Color.Blue,
                    .TrackBarBG = Color.Magenta,
                    .TreeViewFG = Color.Blue,
                    .TreeViewBG = Color.Magenta,
                    .TreeViewNodeLineColor = Color.Green,
                    .OtherFG = Color.Blue,
                    .OtherBG = Color.Magenta,
 _
                    .ListViewOwnerDraw = True,
                    .TabControlOwnerDraw = True,
                    .ListViewColumnColors = New CustomPaint.ListViewColors With {
                        .ColumnText = Color.Blue,
                        .ColumnBackground = Color.Magenta
                    },
                    .TabControlTabColors = New CustomPaint.TabControlColors With {
                        .TabText = Color.Blue,
                        .ActiveTab = Color.Magenta,
                        .InactiveTab = Color.Violet,
                        .TabStripBackground = Color.Magenta
                    }
                }
            End Get
        End Property
    End Class
#End Region

#Region "CustomPaint class"
    Class CustomPaint
        Public Shared Sub ListView_DrawDefaultItem(sender As Object, e As DrawListViewItemEventArgs)
            e.DrawDefault = True
        End Sub
        Public Shared Sub ListView_DrawDefaultSubItem(sender As Object, e As DrawListViewSubItemEventArgs)
            e.DrawDefault = True
        End Sub

        Public Structure ListViewColors
            Public ColumnText As Color
            Public ColumnBackground As Color
        End Structure
        Public Shared Sub ListView_DrawCustomColumnHeader(sender As Object, e As DrawListViewColumnHeaderEventArgs, Optional listViewColors? As ListViewColors = Nothing)
            ' https://stackoverflow.com/a/42181044/2999220
            Dim listView As ListView = DirectCast(sender, ListView)
            Dim colors As ListViewColors
            If listViewColors.HasValue Then
                colors = listViewColors.Value
            Else
                colors = DirectCast(listView.Tag, ListViewColors)
            End If

            Using sf As New StringFormat() With {
                    .LineAlignment = StringAlignment.Center,
                    .FormatFlags = StringFormatFlags.NoWrap,
                    .Trimming = StringTrimming.EllipsisCharacter
                }
                Select Case e.Header.TextAlign
                    Case HorizontalAlignment.Left
                        sf.Alignment = If(listView.RightToLeft = RightToLeft.No, StringAlignment.Near, StringAlignment.Far)
                    Case HorizontalAlignment.Right
                        sf.Alignment = If(listView.RightToLeft = RightToLeft.Yes, StringAlignment.Near, StringAlignment.Far)
                    Case HorizontalAlignment.Center
                        sf.Alignment = StringAlignment.Center
                End Select

                Dim colRect As New Rectangle() With {
                    .X = e.Bounds.X + 1,
                    .Y = e.Bounds.Y + 1,
                    .Width = e.Bounds.Width - 1,
                    .Height = e.Bounds.Height
                }

                e.Graphics.FillRectangle(New SolidBrush(colors.ColumnBackground), colRect)
                e.Graphics.DrawString(e.Header.Text, listView.Font, New SolidBrush(colors.ColumnText), colRect, sf)
            End Using
        End Sub

        Public Structure TabControlColors
            Public TabText As Color
            Public ActiveTab As Color
            Public InactiveTab As Color
            Public TabStripBackground As Color
        End Structure
        Public Shared Sub TabControl_DrawCustomItem(sender As Object, e As DrawItemEventArgs, Optional tabControlColors? As TabControlColors = Nothing)
            Dim tabCtl As TabControl = DirectCast(sender, TabControl)
            Dim colors As TabControlColors
            If tabControlColors.HasValue Then
                colors = tabControlColors.Value
            Else
                colors = DirectCast(tabCtl.Tag, TabControlColors)
            End If

            ' draw tab strip background

            Dim firstTabRect As Rectangle = tabCtl.GetTabRect(0)
            Dim tabStripBounds As Rectangle = tabCtl.Bounds
            Dim allTabBounds As Rectangle = firstTabRect

            If tabCtl.Alignment = TabAlignment.Top Then
                tabStripBounds.Y = 0
            ElseIf tabCtl.Alignment = TabAlignment.Bottom Then
                tabStripBounds.Y = firstTabRect.Y
            ElseIf tabCtl.Alignment = TabAlignment.Left Then
                tabStripBounds.X = 0
            ElseIf tabCtl.Alignment = TabAlignment.Right Then
                tabStripBounds.X = firstTabRect.X
            End If

            If tabCtl.Alignment = TabAlignment.Top OrElse tabCtl.Alignment = TabAlignment.Bottom Then
                tabStripBounds.X = 0
                tabStripBounds.Height = firstTabRect.Height + 2

                allTabBounds.Width = 0
                For Each tabPage As TabPage In tabCtl.TabPages
                    allTabBounds.Width += tabCtl.GetTabRect(tabCtl.TabPages.IndexOf(tabPage)).Width
                Next
            ElseIf tabCtl.Alignment = TabAlignment.Left OrElse tabCtl.Alignment = TabAlignment.Right Then
                tabStripBounds.Y = 0
                tabStripBounds.Width = firstTabRect.Width + 2

                allTabBounds.Height = 0
                For Each tabPage As TabPage In tabCtl.TabPages
                    allTabBounds.Height += tabCtl.GetTabRect(tabCtl.TabPages.IndexOf(tabPage)).Height
                Next
            End If

            Dim tabStripBackground As New Region(tabStripBounds)
            tabStripBackground.Xor(allTabBounds)
            Dim brush As New SolidBrush(colors.TabStripBackground)
            e.Graphics.FillRegion(brush, tabStripBackground)

            ' draw tab background

            Dim backgroundBrush As Brush = New SolidBrush(colors.InactiveTab)
            Dim tabRect As Rectangle = tabCtl.GetTabRect(e.Index)
            If tabCtl.SelectedIndex = e.Index Then
                backgroundBrush = New SolidBrush(colors.ActiveTab)
                If tabCtl.Alignment = TabAlignment.Top Then
                    tabRect.Height += 2
                ElseIf tabCtl.Alignment = TabAlignment.Bottom Then
                    tabRect.Y -= 2
                    tabRect.Height += 2
                ElseIf tabCtl.Alignment = TabAlignment.Left Then
                    tabRect.Width += 2
                ElseIf tabCtl.Alignment = TabAlignment.Right Then
                    tabRect.X -= 2
                    tabRect.Width += 2
                End If
            End If
            e.Graphics.FillRectangle(backgroundBrush, tabRect)

            ' draw tab text

            Using sf As New StringFormat() With {
                    .Alignment = StringAlignment.Center,
                    .LineAlignment = StringAlignment.Center
                }
                tabRect = tabCtl.GetTabRect(e.Index)

                If tabCtl.Alignment = TabAlignment.Top Then
                    If tabCtl.SelectedIndex = e.Index Then tabRect.Y -= 1 Else tabRect.Y += 1
                ElseIf tabCtl.Alignment = TabAlignment.Bottom Then
                    If tabCtl.SelectedIndex = e.Index Then tabRect.Y += 1 Else tabRect.Y -= 1
                ElseIf tabCtl.Alignment = TabAlignment.Left Then
                    sf.FormatFlags = StringFormatFlags.DirectionVertical
                    If tabCtl.SelectedIndex = e.Index Then tabRect.X -= 2
                ElseIf tabCtl.Alignment = TabAlignment.Right Then
                    sf.FormatFlags = StringFormatFlags.DirectionVertical
                    If tabCtl.SelectedIndex <> e.Index Then tabRect.X -= 2
                End If

                e.Graphics.DrawString(tabCtl.TabPages(e.Index).Text, tabCtl.Font, New SolidBrush(colors.TabText), tabRect, sf)
            End Using
        End Sub
    End Class
#End Region
End Class
