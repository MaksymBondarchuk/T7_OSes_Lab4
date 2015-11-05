using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace T7_OS_Lab4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly BinaryTree _tree = new BinaryTree();

        public MainWindow()
        {
            InitializeComponent();
            _tree.Add("mmm");
            _tree.Add("aaa");
            _tree.Add("bbb");
            _tree.Add("ccc");
            _tree.Add("ddd");
            _tree.Add("eee");
            _tree.Add("nnn");
            _tree.Add("ooo");
            _tree.Add("ppp");
            _tree.Add("zzz");
            _tree.Add("rrr");
            _tree.Add("xxx");
            _tree.Add("ggg");
            _tree.Add("hhh");
            _tree.Add("dda");
            TreeViewTree.Items.Add(_tree.ToTreeViewItem());
        }

        private void Badd_Click(object sender, RoutedEventArgs e)
        {
            _tree.Add(TextBoxNewIdentifier.Text);
            TextBoxNewIdentifier.Clear();
            TreeViewTree.Items.Clear();
            TreeViewTree.Items.Add(_tree.ToTreeViewItem());
        }

        private void ButtonFind_Click(object sender, RoutedEventArgs e)
        {
            var path = _tree.Find(TextBoxNewIdentifier.Text);
            ListViewPath.Items.Clear();
            foreach (var t in path)
                ListViewPath.Items.Add(new ListViewItem { Content = t });
            TextBoxWasFound.Text = _tree.WasFound ? $"{TextBoxNewIdentifier.Text} - was found" : $"{TextBoxNewIdentifier.Text} - wasn't found";
            TextBoxNewIdentifier.Clear();
            //TreeViewTree.Items.Clear();
            //TreeViewTree.Items.Add(_tree.ToTreeViewItem());
        }
    }
}
