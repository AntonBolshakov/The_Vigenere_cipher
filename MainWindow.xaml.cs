using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NewProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _Text, _Key, _NewText, _NewIter, _control;
        private char SymbolText, SymbolKey, SymbolResult;
        private int CodSymbolText, CodSymbolKey, Result, Pos, LengthKey, LengthText;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OutputText.Text = ""; _Text = ""; _Key = ""; SymbolText = ' '; SymbolKey = ' '; SymbolResult = ' '; _NewText = ""; _NewIter = ""; 
            CodSymbolText = 0; CodSymbolKey = 0; Result = 0; Pos = 0;

            if (!(String.IsNullOrEmpty(InputText.Text))) _Text = (InputText.Text).ToLower();
            if (!(String.IsNullOrEmpty(KeyText.Text))) _Key = (KeyText.Text).ToLower();

            LengthKey = _Key.Length;
            LengthText = _Text.Length;

            if (!(String.IsNullOrEmpty(InputText.Text)) && !(String.IsNullOrEmpty(KeyText.Text)))
            {
                if (Cod.IsChecked == true)
                {
                    Coding();
                }
                else
                {
                    Decoding();
                }
            }
            else
            { 
                if(String.IsNullOrEmpty(InputText.Text)) MessageBox.Show("Текстовое поле с исходным текстом пустое.\nПожалуйста, заполните его.","Ошибка",MessageBoxButton.OK,MessageBoxImage.Error);
                else if (String.IsNullOrEmpty(KeyText.Text)) MessageBox.Show("Текстовое поле с ключом пустое.\nПожалуйста, заполните его.","Ошибка",MessageBoxButton.OK,MessageBoxImage.Error);
            }

            if(_control == "Ответ в виде итераций") OutputText.Text = _NewIter;
            else OutputText.Text = _NewText;
        }

        private void Coding()
        {
            for (int i = 0; i < LengthText; i++)
            {
                SymbolText = _Text[i];
                SymbolKey = _Key[Pos % LengthKey];

                CodSymbolText = Convert.ToInt32(SymbolText);
                CodSymbolKey = Convert.ToInt32(SymbolKey);

                if (((CodSymbolText >= 1072 && CodSymbolText <= 1103) || CodSymbolText == 1105) && ((CodSymbolKey >= 1072 && CodSymbolKey <= 1103) || CodSymbolKey == 1105))
                {
                    if (CodSymbolText == 1105) CodSymbolText = 6;
                    else
                    {
                        CodSymbolText = CodSymbolText - 1072;
                        if (CodSymbolText >= 6)
                        {
                            CodSymbolText += 1;
                        }
                    }

                    if (CodSymbolKey == 1105) CodSymbolKey = 6;
                    else
                    {
                        CodSymbolKey = CodSymbolKey - 1072;
                        if (CodSymbolKey >= 6)
                        {
                            CodSymbolKey += 1;
                        }
                    }

                    Result = (CodSymbolText + CodSymbolKey) % 33;

                    if (Result > 6)
                    {
                        Result -= 1;
                        SymbolResult = Convert.ToChar(Result + 1072);
                    }
                    else if (Result == 6) SymbolResult = 'ё';
                    else SymbolResult = Convert.ToChar(Result + 1072);

                    _NewIter = _NewIter + "Шаг: " + (i + 1) + ". Буква текста: " + SymbolText + " (" + CodSymbolText + ") " + "+ Буква ключа: " + SymbolKey + " (" + CodSymbolKey + ") = " + Result + " (Остаток от деления на 33). Результат: " + SymbolResult + "\n";
                    _NewText = _NewText + SymbolResult;
                    
                    Pos += 1;
                }
                else
                {
                    _NewText = _NewText + _Text[i];
                }
            }
        }

        private void Decoding()
            {
            for (int i = 0; i < LengthText; i++)
            {
                SymbolText = _Text[i];
                SymbolKey = _Key[Pos % LengthKey];

                CodSymbolText = Convert.ToInt32(SymbolText);
                CodSymbolKey = Convert.ToInt32(SymbolKey);

                if (((CodSymbolText >= 1072 && CodSymbolText <= 1103) || CodSymbolText == 1105) && ((CodSymbolKey >= 1072 && CodSymbolKey <= 1103) || CodSymbolKey == 1105))
                {
                    if (CodSymbolText == 1105) CodSymbolText = 6;
                    else
                    {
                        CodSymbolText = CodSymbolText - 1072;
                        if (CodSymbolText >= 6)
                        {
                            CodSymbolText += 1;
                        }
                    }

                    if (CodSymbolKey == 1105) CodSymbolKey = 6;
                    else
                    {
                        CodSymbolKey = CodSymbolKey - 1072;
                        if (CodSymbolKey >= 6)
                        {
                            CodSymbolKey += 1;
                        }
                    }

                    Result = CodSymbolText - CodSymbolKey;
                    if (Result < 0) Result = Result + 33;

                    if (Result > 6)
                    {
                        Result -= 1;
                        SymbolResult = Convert.ToChar(Result + 1072);
                    }
                    else if (Result == 6) SymbolResult = 'ё';
                    else SymbolResult = Convert.ToChar(Result + 1072);

                    _NewIter = _NewIter + "Шаг: " + (i + 1) + ". Буква текста: " + SymbolText + " (" + CodSymbolText + ") " + "+ Буква ключа: " + SymbolKey + " (" + CodSymbolKey + ") = " + Result + " (Остаток от деления на 33). Результат: " + SymbolResult + "\n";
                    _NewText = _NewText + SymbolResult;
                    
                    Pos += 1;
                }
                else
                {
                    _NewText = _NewText + _Text[i];
                }
            }
        }

        private void ExitFromProgram(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult Res = MessageBox.Show("Вы действительно хотите выйти?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (Res == MessageBoxResult.No) e.Cancel = true;
        }

        private void ExitToProgram_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AboutProgram_Click(object sender, RoutedEventArgs e)
        {
            string Info = "Данное приложение было написано на языке C# с применением технологии WPF на платформе .NET, с целью выполнения задания по компьютерной алгебре.\n" +
                          "Данное приложение кодирует и раскодирует исходный текст с использованием ключевого слова (Шифр Виженера).";
            string Header = "О программе";
            MessageBox.Show(Info, Header, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void AboutAuthor_Click(object sender, RoutedEventArgs e)
        {
            string Info = "Данное приложение разработал:\n\n Большаков Антон Александрович — студент 4 курса ТГПУ имени Льва Николаевича Толстого, " +
                  "факультета математики, физики и информатики, группы 121131. \n\n" +
                  "Направление / Специальность: Фундаментальная информатика и информационные технологии \n\n" +
                  "Профиль / Специализация: Открытые информационные системы.";
            string Header = "О разработчике";
            MessageBox.Show(Info, Header, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Result_Click(object sender, RoutedEventArgs e)
        {
            MenuItem Choose = (MenuItem)sender;
            _control = Convert.ToString(Choose.Header);

            if (_control == "Ответ в виде итераций")
            {
                Choose.Header = "Ответ в виде текста";
                OutputText.Text = _NewIter;
            }
            else if (_control == "Ответ в виде текста")
            {
                Choose.Header = "Ответ в виде итераций";
                OutputText.Text = _NewText;
            }
        }

        
    }
}
