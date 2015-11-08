using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace T7_OS_Lab4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private BinaryTree _tree = new BinaryTree();
        private readonly double _marginTreeLeft;
        private readonly double _marginTreeTop;
        private readonly double _marginTreeRight;
        private readonly double _marginTreeBottom;

        private bool _rightHidden;
        private bool _pathHidden;

        public MainWindow()
        {
            InitializeComponent();

            _marginTreeLeft = TreeViewLeft.Margin.Left;
            _marginTreeTop = TreeViewLeft.Margin.Top;
            _marginTreeRight = TreeViewLeft.Margin.Right;
            _marginTreeBottom = TreeViewLeft.Margin.Bottom;

            HideRight();
            HidePath();
            TextBoxNewIdentifier.Focus();
        }

        private void HidePath()
        {
            if (_pathHidden) return;
            ListViewPath.Visibility = Visibility.Collapsed;
            TreeViewLeft.Margin = new Thickness(_marginTreeLeft, _marginTreeTop, _marginTreeLeft, _marginTreeBottom);
            _pathHidden = true;
        }

        private void ShowPath()
        {
            ListViewPath.Visibility = Visibility.Visible;
            TreeViewLeft.Margin = new Thickness(_marginTreeLeft, _marginTreeTop, _marginTreeRight, _marginTreeBottom);
            _pathHidden = false;
        }

        private void HideRight()
        {
            if (!_rightHidden)
            {
                GridWindow.ColumnDefinitions.Clear();
                GridWindow.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100, GridUnitType.Star) });
                GridWindow.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0, GridUnitType.Star) });

                TreeViewLeft.Items.Clear();
                TreeViewLeft.Items.Add(_tree.ToTreeViewItem());
            }
            _rightHidden = true;
        }

        private void ShowRight()
        {
            GridWindow.ColumnDefinitions.Clear();
            GridWindow.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50, GridUnitType.Star) });
            GridWindow.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50, GridUnitType.Star) });
            _rightHidden = false;
        }

        private void Badd_Click(object sender, RoutedEventArgs e)
        {
            _tree.Find(TextBoxNewIdentifier.Text);
            var identifier = TextBoxNewIdentifier.Text;
            if (!_tree.WasFound)
            {
                _tree.Add(TextBoxNewIdentifier.Text);
                TreeViewLeft.Items.Clear();
                TreeViewLeft.Items.Add(_tree.ToTreeViewItem());
            }
            TextBoxNewIdentifier.Clear();
            StatusBarTextBlock.Text = !_tree.WasFound ? $"{identifier} - successfully added" : $"{identifier} - already exist";
            TextBoxNewIdentifier.Focus();
        }

        private void ButtonFind_Click(object sender, RoutedEventArgs e)
        {
            var path = _tree.Find(TextBoxNewIdentifier.Text);
            ListViewPath.Items.Clear();
            foreach (var t in path)
                ListViewPath.Items.Add(new ListViewItem { Content = t });
            var identifier = TextBoxNewIdentifier.Text;
            TextBoxNewIdentifier.Clear();
            StatusBarTextBlock.Text = _tree.WasFound ? $"{identifier} - was found" : $"{identifier} - wasn't found";
            ShowPath();
            TextBoxNewIdentifier.Focus();
        }

        private void ButtonRemove_Click(object sender, RoutedEventArgs e)
        {
            _tree.Find(TextBoxNewIdentifier.Text);
            if (_tree.WasFound)
            {
                _tree.Remove(TextBoxNewIdentifier.Text);
                TreeViewRight.Items.Clear();
                TreeViewRight.Items.Add(_tree.ToTreeViewItem());
                StatusBarTextBlock.Text = $"{TextBoxNewIdentifier.Text} - successfully removed";
            }
            else StatusBarTextBlock.Text = $"{TextBoxNewIdentifier.Text} - no such identifier";

            var identifier = TextBoxNewIdentifier.Text;
            TextBoxNewIdentifier.Clear();
            if (_tree.WasFound)
                ShowRight();
            StatusBarTextBlock.Text = _tree.WasFound ? $"{identifier} - successfully removed" : $"{identifier} - no such identifier";
            TextBoxNewIdentifier.Focus();
        }

        private void TextBoxNewIdentifier_TextChanged(object sender, TextChangedEventArgs e)
        {
            var text = TextBoxNewIdentifier.Text;

            if (3 < text.Length)
            {
                text = text.Substring(0, 3);
                TextBoxNewIdentifier.Text = text;
                TextBoxNewIdentifier.SelectionStart = 3;
            }

            if (text.Length == 3)
            {
                ButtonAdd.IsEnabled = true;
                ButtonFind.IsEnabled = true;
                ButtonRemove.IsEnabled = true;
            }
            else
            {
                ButtonAdd.IsEnabled = false;
                ButtonFind.IsEnabled = false;
                ButtonRemove.IsEnabled = false;
            }

            HideRight();
            HidePath();
            StatusBarTextBlock.Text = "";
        }

        private void TreeViewLeft_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _tree = new BinaryTree();
            TreeViewLeft.Items.Clear();
            TreeViewLeft.Items.Add(_tree.ToTreeViewItem());
            HideRight();
            StatusBarTextBlock.Text = "Tree deleted";
        }
    }
}
