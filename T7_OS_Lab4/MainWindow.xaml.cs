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
        }

        private void Badd_Click(object sender, RoutedEventArgs e)
        {
            _tree.Add(TextBoxNewIdentifier.Text);
            TextBoxNewIdentifier.Clear();
            TreeViewTree.Items.Clear();
            TreeViewTree.Items.Add(_tree.ToTreeViewItem());
        }
    }
}
