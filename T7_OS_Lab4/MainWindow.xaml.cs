using System.Windows;
using System.Windows.Controls;

namespace T7_OS_Lab4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly BinaryTree _tree = new BinaryTree();
        private readonly double _marginTreeLeft;
        private readonly double _marginTreeTop;
        private readonly double _marginTreeRight;
        private readonly double _marginTreeBottom;
        //private Thickness _marginWithPath;
        //private Thickness _marginWithoutPath;

        private bool _rightHidden;
        private bool _pathHidden;

        public MainWindow()
        {
            InitializeComponent();

            _marginTreeLeft = TreeViewLeft.Margin.Left;
            _marginTreeTop = TreeViewLeft.Margin.Top;
            _marginTreeRight = TreeViewLeft.Margin.Right;
            _marginTreeBottom = TreeViewLeft.Margin.Bottom;


            _tree.Add("a");
            _tree.Add("b");
            //_tree.Add("c");

            //_tree.Add("mmm");
            //_tree.Add("ccc");
            //_tree.Add("aaa");
            //_tree.Add("ddd");
            //_tree.Add("eee");
            //_tree.Add("eea");
            //_tree.Add("nnn");
            //_tree.Add("ooo");
            //_tree.Add("ppp");
            //_tree.Add("ggg");
            //_tree.Add("gga");
            //_tree.Add("hhh");
            //_tree.Add("dda");
            //_tree.Add("nna");
            //_tree.Add("ooa");
            //TreeViewLeft.Items.Add(_tree.ToTreeViewItem());

            HideRight();
            HidePath();
        }

        private void HidePath()
        {
            if (!_pathHidden)
            {
                ListViewPath.Visibility = Visibility.Collapsed;
                TreeViewLeft.Margin = new Thickness(_marginTreeLeft, _marginTreeTop, _marginTreeLeft, _marginTreeBottom);
                _pathHidden = true;
            }
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
                GridWindow.ColumnDefinitions.Add(new ColumnDefinition {Width = new GridLength(100, GridUnitType.Star)});
                GridWindow.ColumnDefinitions.Add(new ColumnDefinition {Width = new GridLength(0, GridUnitType.Star)});

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
            if (!_tree.WasFound)
            {
                _tree.Add(TextBoxNewIdentifier.Text);
                TreeViewLeft.Items.Clear();
                TreeViewLeft.Items.Add(_tree.ToTreeViewItem());
                StatusBarTextBlock.Text = $"{TextBoxNewIdentifier.Text} - successfully added";
            }
            else StatusBarTextBlock.Text = $"{TextBoxNewIdentifier.Text} - already exist";
            TextBoxNewIdentifier.Clear();

        }

        private void ButtonFind_Click(object sender, RoutedEventArgs e)
        {
            var path = _tree.Find(TextBoxNewIdentifier.Text);
            ListViewPath.Items.Clear();
            foreach (var t in path)
                ListViewPath.Items.Add(new ListViewItem { Content = t });
            StatusBarTextBlock.Text = _tree.WasFound ? $"{TextBoxNewIdentifier.Text} - was found" : $"{TextBoxNewIdentifier.Text} - wasn't found";
            TextBoxNewIdentifier.Clear();
            ShowPath();
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
            TextBoxNewIdentifier.Clear();
            if (_tree.WasFound)
                ShowRight();
        }

        private void TextBoxNewIdentifier_TextChanged(object sender, TextChangedEventArgs e)
        {
            HideRight();
            HidePath();
            //MessageBox.Show("Changed");
        }
    }
}
