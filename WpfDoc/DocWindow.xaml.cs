using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
            FontColorPicker.SelectedColor = fontColor;
            foreach (FontFamily fontFamily in Fonts.SystemFontFamilies)
            {
                FontFamilyComboBox.Items.Add(fontFamily.Source);
            }
            FontFamilyComboBox.SelectedIndex = 1;

            FontSizeComboBox.ItemsSource = new List<double>()
            {
                8,9,10,11,12,14,16,18,20,22,24,26,28,36,48,72
            };
            FontSizeComboBox.SelectedIndex = 4;
        }

        private void NewFile_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DocWindow docWindow = new DocWindow();
            docWindow.Show();
        }

        private void OpenFile_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            {
                openFileDialog.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
                openFileDialog.DefaultExt = ".rtf";
                openFileDialog.Multiselect = false;
            };

            if (openFileDialog.ShowDialog() == true)
            {
                TextRange range = new TextRange(Edit.Document.ContentStart, Edit.Document.ContentEnd);
                FileStream fileStream = new FileStream(openFileDialog.FileName, FileMode.Open);
                range.Load(fileStream, DataFormats.Rtf);
                fileStream.Close();
            }
        }

        private void SaveFile_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            {
                saveFileDialog.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
                saveFileDialog.DefaultExt = ".rtf";
                saveFileDialog.AddExtension = true;
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                TextRange range = new TextRange(Edit.Document.ContentStart, Edit.Document.ContentEnd);
                FileStream fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create);
                range.Save(fileStream, DataFormats.Rtf);
                fileStream.Close();
            }
        }
        private void DelAll_Clicked(object sender, RoutedEventArgs e)
        {
            Edit.Document.Blocks.Clear();
        }

        private void Edit_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var property_bold = Edit.Selection.GetPropertyValue(TextElement.FontWeightProperty);
            Bold.IsChecked = (property_bold != DependencyProperty.UnsetValue) && (property_bold.Equals(FontWeights.Bold));

            var property_italic = Edit.Selection.GetPropertyValue(TextElement.FontStyleProperty);
            Italic.IsChecked = (property_italic != DependencyProperty.UnsetValue) && (property_italic.Equals(FontStyles.Italic));

            var property_underline = Edit.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            Underline.IsChecked = (property_underline != DependencyProperty.UnsetValue) && (property_underline.Equals(TextDecorations.Underline));

            var property_fontfamily = Edit.Selection.GetPropertyValue(TextElement.FontFamilyProperty);
            FontFamilyComboBox.SelectedItem = property_fontfamily;

            var property_fontsize = Edit.Selection.GetPropertyValue(TextElement.FontSizeProperty);
            FontSizeComboBox.SelectedItem = property_fontsize;

            var property_fontcolor = Edit.Selection.GetPropertyValue(TextElement.ForegroundProperty);
            FontColorPicker.SelectedColor = ((SolidColorBrush)property_fontcolor).Color;
        }

        private void FontColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            fontColor = (Color)e.NewValue;
            SolidColorBrush fontBrush = new SolidColorBrush(fontColor);
            Edit.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, fontBrush);
        }

        private void FontFamilyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FontFamilyComboBox.SelectedItem != null)
            {
                Edit.Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, FontFamilyComboBox.SelectedItem);
            }
        }

        private void FontSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FontSizeComboBox.SelectedItem != null)
            {
                Edit.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, FontSizeComboBox.SelectedItem);
            }
        }
    }
}
