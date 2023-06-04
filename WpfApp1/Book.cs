using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Book {
        private String title;
        private String author;
        private String releaseDate;
        private double price;

        // konstruktor
        public Book(String title, String author, String releaseDate, double price) {
            this.title = title;
            this.author = author;
            this.releaseDate = releaseDate;
            this.price = price;
        }

        // Gettery i settery
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        public string Author
        {
            get { return author; }
            set { author = value; }
        }
        public string ReleaseDate
        {
            get { return releaseDate; }
            set { releaseDate = value; }
        }
        public double Price
        {
            get { return price; }
            set { price = value; }
        }
    }
}
