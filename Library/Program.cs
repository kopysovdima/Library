using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class Program
    {
        static void Main(string[] args)
        {
            const string CommandAddBook = "1";
            const string CommandDeletBook = "2";
            const string CommandSearchBook = "3";
            const string CommandExit = "4";
            Library library = new Library();
            bool isActive = true;

            while (isActive)
            {
                Console.Clear();
                Console.WriteLine("Библиотека\n");
                library.ShowAllBooks();
                Console.WriteLine($"{CommandAddBook} - Добавить книгу");
                Console.WriteLine($"{CommandDeletBook} - Убрать книгу");
                Console.WriteLine($"{CommandSearchBook} - Найти по параметру");
                Console.WriteLine($"{CommandExit} - Выход\n");

                switch (Console.ReadLine())
                {
                    case CommandAddBook:
                        library.AddBook();
                        break;
                    case CommandDeletBook:
                        library.RemoveBook();
                        break;
                    case CommandSearchBook:
                        library.FindBooks();
                        break;
                    case CommandExit:
                        isActive = false;
                        Console.WriteLine("\nПока! Ещё увидимся!\n");
                        break;
                    default:
                        Console.WriteLine("Такой команды нет, попробуй ещё раз.\n");
                        break;
                }

                Console.ReadKey(true);
            }
        }
    }

    class Book
    {
        public string Title { get; private set; }
        public string Author { get; private set; }
        public int Year { get; private set; }
        public string Genre { get; private set; }

        public Book(string title, string author, int year, string genre)
        {
            Title = title;
            Author = author;
            Year = year;
            Genre = genre;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Название: {Title}, Автор: {Author}, Год издания: {Year}, Жанр: {Genre}\n");
        }
    }

    class Library
    {
        private List<Book> _books = new List<Book>();

        public Library()
        {
            _books.Add(new Book("The Hobbit", "J.R.R. Tolkien", 1937, "Adventure"));
            _books.Add(new Book("Sherlock Holmes", "Arthur Conan Doyle", 1887, "Detective"));
            _books.Add(new Book("1984", "George Orwell", 1949, "Classic"));
            _books.Add(new Book("Romeo and Juliet", "William Shakespeare", 1595, "Classic"));
            _books.Add(new Book("The Three Musketeers", "Alexandre Dumas", 1844, "Adventure"));
            _books.Add(new Book("The Lord of The Rings", "J.R.R. Tolkien", 1954, "Fantasy"));
            _books.Add(new Book("The Shining", "Stephen King", 1977, "Horror"));
            _books.Add(new Book("Hamlet", "William Shakespeare", 1603, "Drama"));
            _books.Add(new Book("It", "Stephen King", 1986, "Horror"));
        }

        public void ShowAllBooks()
        {
            Console.WriteLine("Полный список книг: ");

            for (int i = 0; i < _books.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_books[i].Author}: \"{_books[i].Title}\", {_books[i].Year}, {_books[i].Genre}");
            }

            Console.WriteLine();
        }

        public void AddBook()
        {
            string title;
            string author;
            int year;
            string genre;

            Console.Write("Введите название книги: ");
            title = Console.ReadLine();
            Console.Write("Введите автора книги: ");
            author = Console.ReadLine();
            Console.Write("Введите год издания книги: ");
            year = ReadInt();
            Console.Write("Введите жанр: ");
            genre = Console.ReadLine();
            _books.Add(new Book(title, author, year, genre));
            Console.Write("Книга добавлена.");
        }

        public void RemoveBook()
        {
            int index;

            Console.Write("Введите иномер книги для удаления: ");
            index = ReadInt();

            if (0 > index || index > _books.Count)
            {
                Console.WriteLine("Такого номера нет в списке.");
            }
            else
            {
                _books.RemoveAt(index - 1);
                Console.WriteLine("Книга удалена.");
            }
        }

        public void FindBooks()
        {
            const string CommandFindBooksByAuthor = "1";
            const string CommandFindByTitleBooks = "2";
            const string CommandFindBooksByYearOfPublication = "3";
            const string CommandFindByGenreBooks = "4";
            const string CommandExit = "5";
            bool isActive = true;

            while (isActive)
            {
                Console.WriteLine(
                    $"{CommandFindBooksByAuthor} Поиск по автору\n" +
                    $"{CommandFindByTitleBooks} Поиск по названию\n" +
                    $"{CommandFindBooksByYearOfPublication} Поиск по году издания\n" +
                    $"{CommandFindByGenreBooks} Поиск по жанру\n" +
                    $"{CommandExit} Выход из поиска\n"
                    );

                switch (Console.ReadLine())
                {
                    case CommandFindBooksByAuthor:
                        FindBooksByAuthor();
                        break;
                    case CommandFindByTitleBooks:
                        FindByTitleBooks();
                        break;
                    case CommandFindBooksByYearOfPublication:
                        FindBooksByYearOfPublication();
                        break;
                    case CommandFindByGenreBooks:
                        FindByGenreBooks();
                        break;
                    case CommandExit:
                        isActive = false;
                        Console.WriteLine("Выход из поиска.\n");
                        break;
                    default:
                        Console.WriteLine("Вы ввели не правильную команду, пожалуйста повторите попытку!");
                        break;
                }
            }
        }

        private static int ReadInt()
        {
            int result;

            while (int.TryParse(Console.ReadLine(), out result) == false)
            {
                Console.WriteLine("Неверный ввод числа!\nНеобходимо ввести целое число.");
                Console.Write("Введите целое число: ");
            }

            return result;
        }

        private void FindBooksByAuthor()
        {
            string author;
            bool isNotFound = true;

            Console.Write("Введите автора книги для поиска: ");
            author = Console.ReadLine();
            Console.WriteLine();

            foreach (var book in _books)
            {
                if (book.Author.ToLower() == author.ToLower())
                {
                    book.ShowInfo();
                    isNotFound = false;
                }
            }

            if (isNotFound)
            {
                ShowMassageBooksNotFound();
            }
        }

        private void FindByTitleBooks()
        {
            string title;
            bool _isNotFound = true;

            Console.Write("Введите название книги для поиска: ");
            title = Console.ReadLine();

            foreach (var book in _books)
            {
                if (book.Title.ToLower() == title.ToLower())
                {
                    book.ShowInfo();
                    _isNotFound = false;
                }
            }

            if (_isNotFound)
            {
                ShowMassageBooksNotFound();
            }
        }

        private void FindBooksByYearOfPublication()
        {
            int year;
            bool _isNotFound = true;

            Console.Write("Введите год издания книги для поиска: ");
            year = ReadInt();

            foreach (var book in _books)
            {
                if (book.Year == year)
                {
                    book.ShowInfo();
                    _isNotFound = false;
                }
            }

            if (_isNotFound)
            {
                ShowMassageBooksNotFound();
            }
        }

        private void FindByGenreBooks()
        {
            string genre;
            bool _isNotFound = true;

            Console.Write("Введите жанр книги для поиска: ");
            genre = Console.ReadLine();

            foreach (var book in _books)
            {
                if (book.Genre.ToLower() == genre.ToLower())
                {
                    book.ShowInfo();
                    _isNotFound = false;
                }
            }

            if (_isNotFound)
            {
                ShowMassageBooksNotFound();
            }
        }

        private void ShowMassageBooksNotFound()
        {
            Console.WriteLine("Ни одной книги не найдено\n");
        }
    }
}
