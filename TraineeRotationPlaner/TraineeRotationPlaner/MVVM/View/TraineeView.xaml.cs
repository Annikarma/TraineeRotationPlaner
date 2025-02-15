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
using TraineeRotationPlaner.MVVM.ViewModel;

namespace TraineeRotationPlaner.MVVM.View
{
    /// <summary>
    /// Interaktionslogik für TraineeView.xaml
    /// </summary>
    public partial class TraineeView : UserControl
    {
        public TraineeView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        //private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    TextBox textBox = sender as TextBox;
        //    if (textBox != null && textBox.Text == textBox.Tag.ToString()) // Der Standardtext ist im Tag gespeichert
        //    {
        //        textBox.Text = ""; // Text löschen
        //        textBox.Foreground = new SolidColorBrush(Colors.Black); // Textfarbe auf schwarz setzen
        //    }
        //}

        //private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    TextBox textBox = sender as TextBox;
        //    if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
        //    {
        //        textBox.Text = textBox.Tag.ToString(); // Text zurücksetzen, wenn TextBox leer ist
        //        textBox.Foreground = new SolidColorBrush(Colors.Gray); // Textfarbe zurücksetzen
        //    }
        //}


    }
}

