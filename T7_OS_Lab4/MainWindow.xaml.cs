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
        private int _marginTreeLeft;

        public MainWindow()
        {
            InitializeComponent();
            //_tree.Add("bbb");
            _tree.Add("mmm");
            _tree.Add("ccc");
            _tree.Add("aaa");
            _tree.Add("ddd");
            _tree.Add("eee");
            _tree.Add("eea");
            _tree.Add("nnn");
            _tree.Add("ooo");
            _tree.Add("ppp");
            _tree.Add("ggg");
            _tree.Add("gga");
            _tree.Add("hhh");
            _tree.Add("dda");
            _tree.Add("nna");
            _tree.Add("ooa");
            TreeViewLeft.Items.Add(_tree.ToTreeViewItem());
        }

        private void HidePath()
        {
            ListViewPath.Visibility = Visibility.Visible;
            TreeViewLeft.Margin = new Thickness(10, 10, 147, 55);
        }

        private void ShowPath()
        {
            ListViewPath.Visibility = Visibility.Collapsed;
            TreeViewLeft.Margin = new Thickness(10, 10, 10, 55);
        }

        private void Badd_Click(object sender, RoutedEventArgs e)
        {
            HidePath();
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
            ShowPath();
            var path = _tree.Find(TextBoxNewIdentifier.Text);
            ListViewPath.Items.Clear();
            foreach (var t in path)
                ListViewPath.Items.Add(new ListViewItem { Content = t });
            StatusBarTextBlock.Text = _tree.WasFound ? $"{TextBoxNewIdentifier.Text} - was found" : $"{TextBoxNewIdentifier.Text} - wasn't found";
            TextBoxNewIdentifier.Clear();
            //TreeViewTree.Items.Clear();
            //TreeViewTree.Items.Add(_tree.ToTreeViewItem());
        }

        private void ButtonRemove_Click(object sender, RoutedEventArgs e)
        {
            HidePath();
            _tree.Find(TextBoxNewIdentifier.Text);
            if (_tree.WasFound)
            {
                _tree.Remove(TextBoxNewIdentifier.Text);
                TreeViewLeft.Items.Clear();
                TreeViewLeft.Items.Add(_tree.ToTreeViewItem());
                StatusBarTextBlock.Text = $"{TextBoxNewIdentifier.Text} - successfully removed";
            }
            else StatusBarTextBlock.Text = $"{TextBoxNewIdentifier.Text} - no such identifier";
            TextBoxNewIdentifier.Clear();
        }
    }
}
