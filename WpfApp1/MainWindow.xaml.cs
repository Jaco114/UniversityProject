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
using System.IO;
using System.Globalization;

namespace WpfApp1
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BookService bookService;
        public MainWindow() {
            InitializeComponent();

            // Tworzenie obiektu i przypisanie go do zmiennej bookService
            bookService = new BookService(BookListBox,TotalPriceTextBlock,TotalBooksAmountTextBlock);
        }

        // Wykona się po kliknięciu w przycisk dodający książkę do pliku
        // Następuje w niej sprawdzenie czy tekst w text boxach jest poprawny
        // i wywołanie metody addBook w bookservice
        // Jeśli tekst nie będzie poprawny zostanie wyświetlony message box z informacją o błędzie
        private void addButtonClick(Object sender, EventArgs e) {
            Boolean proceed = true;

            if (TitleTextBox.Text.Equals("") || TitleTextBox.Text.Contains(";") || TitleTextBox.Text.Length > 20 || TitleTextBox.Text.Substring(0, 1).Equals(" "))
                proceed = false;
            if (AuthorTextBox.Text.Equals("") || AuthorTextBox.Text.Contains(";") || AuthorTextBox.Text.Length > 20 || AuthorTextBox.Text.Substring(0, 1).Equals(" "))
                proceed = false;
            if (ReleaseDateTextBox.Text.Equals("") || ReleaseDateTextBox.Text.Contains(";") || ReleaseDateTextBox.Text.Length > 20 || ReleaseDateTextBox.Text.Substring(0, 1).Equals(" "))
                proceed = false;
            if (PriceTextBox.Text.Equals("") || PriceTextBox.Text.Contains(";") || PriceTextBox.Text.Length > 20 || PriceTextBox.Text.Substring(0, 1).Equals(" "))
                proceed = false;

            try{
                DateTime.ParseExact(ReleaseDateTextBox.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch{
                MessageBox.Show("Prawidłowy format daty to dzień/miesiąc/rok");
                return;
            }

            try{
                Double.Parse(PriceTextBox.Text);
            }
            catch{
                MessageBox.Show("Niepoprawna cena książki. \n Poprawnie napisana cena: 12,32 lub 15");
                return;
            }
            if (proceed)
                bookService.addBook(TitleTextBox.Text, AuthorTextBox.Text, ReleaseDateTextBox.Text, PriceTextBox.Text);
            else
                MessageBox.Show("Pola tytuł, autor, data wydania lub cena nie są poprawnie wpisane. \n " +
                    "Pole te nie mogą być puste, nie mogą zawierać znaku ';', nie mogą być dłuższe niż 20 znaków \n" +
                    "pierwszym znakiem nie może być spacja. ");
        }

        // Wykona się po kliknięciu w przycisk usuwający książkę z pliku
        // Jeśli dowonly element z list boxa jest zaznaczony to zostanie wywołana metoda removeBook w bookService
        // W przypadku gdy nie został zaznaczony żaden element nie zostanie wykonane żadne działanie
        private void removeButtonClick(Object sender, EventArgs e) {
            int index = BookListBox.SelectedIndex;
            if (index!= -1) bookService.removeBook(index);
        }
    }
}
