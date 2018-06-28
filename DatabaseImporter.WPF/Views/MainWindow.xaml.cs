using System.Windows;
using DatabaseImporter.Common.Database.Connection;
using DatabaseImporter.WPF.Infastructure;
using DatabaseImporter.WPF.Infastructure.Utilities;

namespace DatabaseImporter.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.LoadWindowContext();
            
        }
        
    }
}
