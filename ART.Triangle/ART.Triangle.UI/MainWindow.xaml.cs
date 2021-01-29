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
using ART.Triangle.BL;

namespace ART.Triangle.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnCalc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BL.Models.Triangle triangle = new BL.Models.Triangle();
                triangle.SideA = Convert.ToDouble(txtSideA.Text);
                triangle.SideB = Convert.ToDouble(txtSideB.Text);

                TriangleManager.CalcSideC(triangle);

                lblSideC.Content = triangle.SideC.ToString("n2");

                TriangleManager.Insert(triangle);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
