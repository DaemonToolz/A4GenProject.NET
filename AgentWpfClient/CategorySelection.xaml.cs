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

namespace AgentWpfClient
{
    /// <summary>
    /// Interaction logic for CategorySelection.xaml
    /// </summary>
    public partial class CategorySelection : UserControl{

     
        public static readonly DependencyProperty CategoryNameProperty = DependencyProperty.Register("CategoryName", typeof(String), typeof(CategorySelection), new FrameworkPropertyMetadata(string.Empty));

        public String CategoryName
        {
            get { return GetValue(CategoryNameProperty).ToString(); }
            set { SetValue(CategoryNameProperty, value); }
        }


        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register("ImageSource", typeof(String), typeof(CategorySelection), new FrameworkPropertyMetadata(string.Empty));

        public String ImageSource
        {
            get { return GetValue(CategoryNameProperty).ToString(); }
            set { SetValue(CategoryNameProperty, value); }
        }


        public CategorySelection(){
            InitializeComponent();
            this.DataContext = this;
        }
    }
}
