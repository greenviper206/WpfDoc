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
using System.Windows.Shapes;

namespace WpfDoc
{
    /// <summary>
    /// DocWindow.xaml 的互動邏輯
    /// </summary>
    public partial class DocWindow : Window
    {
        Color fontColor = Colors.Black;
        public DocWindow()
        {
            InitializeComponent();
            FontcolorPicker.SelectedColor = fontColor;

            foreach (FontFamily font in Fonts.SystemFontFamilies)
            {
                FontFamilyComboBox.Items.Add(font);
            }
            FontFamilyComboBox.SelectedItem = this.FontFamily;
        }

        private void NewFile_Executed(object sender, ExecutedRoutedEventArgs e)
        { 

        }

        private void OpenFile_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void SaveFile_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }
    }
}
