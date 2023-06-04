using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.IO;
using System.Windows;

namespace WpfApp1
{
    class BookService {


        private String fileName = "books.txt";
        private TextBlock totalPriceTextBlock;
        private TextBlock totalAmountOfBooksTextBlock;
        private ListBox bookListBox;
        private List<Book> books = new List<Book>();
        
        // Konstruktor który wykona się podczas tworzenia obiektu
        // Przypisze on dane do odpowienich zmiennych które zostały podane w parametrach konstruktora
        // Na końcu zostaną wywołane metody które służą do pobrania listy książek z pliku
        // i zaktualizowania text blocków i list boxa
        public BookService(ListBox bookListBox,TextBlock totalPriceTextBlock, TextBlock totalAmountOfBooksTextBlock) {
            this.totalPriceTextBlock = totalPriceTextBlock;
            this.totalAmountOfBooksTextBlock = totalAmountOfBooksTextBlock;
            this.bookListBox = bookListBox;

            createBooks(fileLinesToList(fileName));
            populateListBox();
            calculateCumulativePrice();
            totalAmountOfBooksTextBlock.Text = books.Count().ToString();
        }

        // Dodaje książkę do pliku i dodaje ją do listy books
        // na końcu wywoływane są metody które służą do
        // zaktualizowania text blocków i list boxa
        public void addBook(String title, String author, String date, String price) {
            String newLine = title + ";" + author + ";" + date + ";" + price;
            StreamWriter writer = null;
            try{
                writer = new StreamWriter(fileName,true);
                writer.WriteLine(newLine);
            }
            catch{
                MessageBox.Show("Wystąpił błąd podczas dodawania książki do pliku.");
                return;
            }
            finally {writer.Close();}

            books.Add(new Book(title, author, date, Double.Parse(price)));
            populateListBox();
            calculateCumulativePrice();
            totalAmountOfBooksTextBlock.Text = books.Count().ToString();
        }

        // Pobiera książki z pliku jako liste stringów,
        // usuwa cały tekst z pliku,
        // usuwa stringa z listy który jest na danym indexie
        // a następnie używa metody addBook żeby zapisać do pliku
        // książki które nie miały zostać usunięte
        public void removeBook(int index) {
            List<String> lines = fileLinesToList(fileName);
            StreamWriter writer = null;
            try{
                writer = new StreamWriter(fileName);
                writer.Write("");
                lines.RemoveAt(index);
                books.Clear();
            }
            catch{
                MessageBox.Show("Wystąpił błąd podczas usuwania książki.");
                return;
            }
            finally{writer.Close();}

            if (lines.Count() == 0) {
                populateListBox();
                calculateCumulativePrice();
                totalAmountOfBooksTextBlock.Text = books.Count().ToString();
            }
            else
            lines.ForEach(line =>{
                String[] bookInfo = line.Split(';');
                addBook(bookInfo[0], bookInfo[1], bookInfo[2], bookInfo[3]);
            });
        }

        //Oblicza całkowitą cenę wszystkich książek
        private void calculateCumulativePrice() {
            double totalPrice = 0;
            books.ForEach(book => {
                totalPrice = totalPrice + book.Price;
            });
            totalPriceTextBlock.Text = totalPrice.ToString();
        }

        // Wypełnia list boxa książkami
        private void populateListBox() {
            bookListBox.Items.Clear();
            books.ForEach(book => {
                bookListBox.Items.Add(
                    "Tytuł: " + book.Title +
                    "    Autor: " + book.Author +
                    "    Data wydania: " + book.ReleaseDate +
                    "    Cena: " + book.Price);
            });
        }

        // Tworzy listę obiektów book z listy stringów która
        // jest pobierana z pliku
        private void createBooks(List<String> lines) {
            if (lines == null) return;

            lines.ForEach(line =>{
                String[] bookInfo = line.Split(';');

                double price = 0;
                try{price = Double.Parse(bookInfo[3]);}
                catch{
                    MessageBox.Show("Błąd podczas wczytywania cen książek z pliku");
                    return;
                }
                books.Add(new Book(bookInfo[0], bookInfo[1], bookInfo[2], price));
            });
        }

        // Pobiera linie z pliku txt i dodaje je do listy stringów
        private List<String> fileLinesToList(String fileName) {
            List<String> lines = new List<string>();
            StreamReader reader = null;
            try{
                reader = new StreamReader(fileName);
                while (!reader.EndOfStream)
                    lines.Add(reader.ReadLine());
            }
            catch{
                MessageBox.Show("Nie można wczytać książek z pliku");
                return null;
            }
            finally{reader.Close();}
            return lines;
        }
    }
}
