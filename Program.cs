using System;

//TODO
//
// set up local database for books

namespace Bookshelf
{
    public class Program
    {
        public static void Main()
        {
            Shelf shelf = new Shelf();

            MenuOption userSelection;
            do
            {
                userSelection = ReadUserOption(); //input into this switch which calls whichever option user chose
                switch (userSelection)
                {
                    case MenuOption.NewBook:
                        NewBook(shelf);
                        break;
                    case MenuOption.RemoveBook:
                        RemoveBook(shelf);
                        break;
                    case MenuOption.SelectBook:
                        SelectBook(shelf);
                        break;
                    case MenuOption.PrintBookList:
                        PrintBookList(shelf);
                        break;
                    case MenuOption.Quit:
                        Console.WriteLine("I hope you enjoyed reading, goodbye!");
                        break;
                }
            } while (userSelection != MenuOption.Quit); //runs while quit option is not chosen, if it is, program ends
        }

        private static MenuOption ReadUserOption() //gives user a menu to choose from and reads in the input
        {
            int option;
            do
            {
                Console.WriteLine("");
                Console.WriteLine("Please choose option 1, 2, 3 or 4:");
                Console.WriteLine("1. Add a new book to the shelf.");
                Console.WriteLine("2. Remove a book from the shelf.");
                Console.WriteLine("3. Select a book from the shelf.");
                Console.WriteLine("4. Show all the books on the shelf.");
                Console.WriteLine("5. Quit.");
                try
                {
                    option = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("That's not a valid option");
                    option = -1;
                }
            } while (option < 1 || option > 5);
            return (MenuOption)(option - 1);
        }

        private static void NewBook(Shelf shelf) //adds new book to the shelf
        {
            Console.Write("Please enter the title of the book: ");
            string? title = Console.ReadLine();

            Console.Write("Please enter the author of the book: ");
            string? author = Console.ReadLine();

            Console.Write("Please enter the number of pages in the book: ");
            string? input = Console.ReadLine();
            Console.WriteLine("");

            int numberOfPages; //number of pages must be valid number > 0
            if (!int.TryParse(input, out numberOfPages))
            {
                Console.WriteLine("Please enter a valid number.");
            }
            else if (numberOfPages < 0)
            {
                Console.WriteLine("The book needs to have at least 1 page!");
            }
            else
            {
                Book book = new Book(title, numberOfPages, author);

                shelf.AddBook(book);
                Console.WriteLine($"You have added {title} by {author} to your bookshelf.");
            }
        }

        private static void RemoveBook(Shelf shelf)
        {
            shelf.Print();
            Console.WriteLine("");
            Console.WriteLine("Please enter the ID of the book you would like to remove: ");
            string? input = Console.ReadLine();
            Console.WriteLine("");
            int id;

            if (int.TryParse(input, out id))
            {
                bool found = false;

                foreach (Book book in shelf.books.ToList()) //have to use ToList() to create a new copy of the list, otherwise an error occurs
                {
                    if (book.id == id)
                    {
                        shelf.RemoveBook(book);
                        Console.WriteLine($"You have removed {book.title} by {book.author} from the shelf.");
                        found = true;
                    }
                }
                if (!found)
                {
                    Console.WriteLine("No book found with that ID.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }

        private static void SelectBook(Shelf shelf) //selects a book from the shelf and calls the open() method, if input doesnt match id, returns error
        {
            shelf.Print();
            Console.WriteLine("");
            Console.Write("Please enter the ID of the book you would like to open: ");
            string? input = Console.ReadLine();
            Console.WriteLine("");
            int id;

            if (int.TryParse(input, out id))
            {
                bool found = false;

                foreach (Book book in shelf.books)
                {
                    if (book.id == id)
                    {
                        Console.WriteLine($"You have opened {book.title} by {book.author}, you are currently on page {book.currentPage}.");
                        found = true;
                        book.Open();
                    }
                }
                if (!found)
                {
                    Console.WriteLine("No book found with that ID.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }

        private static void PrintBookList(Shelf shelf) //prints list of all books on the shelf
        {
            shelf.Print();
        }

        private enum MenuOption //enum for options
        {
            NewBook,
            RemoveBook,
            SelectBook,
            PrintBookList,
            Quit
        }
    }

    //BOOK CLASS

    public class Book //book object parameters
    {
        private static int LastAssignedID = 0;
        public int id { get; private set; }
        public string title { get; private set; }
        public string author { get; private set; }
        public int numberOfPages { get; private set; }
        public int currentPage { get; private set; }
        public bool read { get; private set; }
        public int rating { get; private set; }

        public Book(string title, int numberOfPages, string author) //constructor for book
        {
            id = ++LastAssignedID; //counter for id to automatically add unique id to each book added to shelf
            this.title = title;
            this.author = author;
            this.numberOfPages = numberOfPages;
            currentPage = 0;
            read = false;
        }

        private static BookOption ReadUserOption() //when open method is called, this menu is given to user, then reads in input
        {
            int option;
            do
            {
                Console.WriteLine("");
                Console.WriteLine("Please choose option 1, 2, 3, 4 or 5.");
                Console.WriteLine("1. Go to the next page.");
                Console.WriteLine("2. Go to the previous page.");
                Console.WriteLine("3. Skip to page.");
                Console.WriteLine("4. Finish and rate book.");
                Console.WriteLine("5. Close book.");
                try
                {
                    option = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("That's not a valid option.");
                    option = -1;
                }
            } while (option < 1 || option > 5);
            return (BookOption)(option - 1);
        }
        public void Open() //this method is called when a valid book is selected, gives user option to do methods below
        {
            BookOption userSelection;
            do
            {
                userSelection = ReadUserOption();
                switch (userSelection)
                {
                    case BookOption.NextPage:
                        NextPage();
                        break;
                    case BookOption.PreviousPage:
                        PreviousPage();
                        break;
                    case BookOption.SkipToPage:
                        SkipToPage();
                        break;
                    case BookOption.FinishBook:
                        FinishBook();
                        break;
                    case BookOption.Close:
                        Close();
                        break;
                }
            } while (userSelection != BookOption.Close); //once close book is called, then return to previous menu
        }

        private void Close() //closes book and shows current page and number of pages remaining
        {
            Console.WriteLine($"You have closed {title}.");
            Console.WriteLine($"You have read {currentPage} pages of {title}, you have {numberOfPages - currentPage} pages remaining.");
        }

        private void NextPage() //goes to next page
        {
            if (currentPage == numberOfPages)
            {
                Console.WriteLine("You are on the last page, don't forgot to use the finish book function to rate the book!");
            }
            else
            {
                currentPage += 1;
                Console.WriteLine($"You are now on page {currentPage}");
            }
        }

        private void PreviousPage() //goes to previous page
        {
            if (currentPage == 0)
            {
                Console.WriteLine("You are on the first page.");
            }
            else
            {
                currentPage -= 1;
                Console.WriteLine($"You are now on page {currentPage}");
            }
        }

        private void SkipToPage() //skips to inputted page number
        {
            int input;
            do
            {
                Console.Write("Please enter the number of the page you would like to skip to: ");
                try
                {
                    input = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine($"That's not a valid page number, the book has {numberOfPages} pages.");
                    input = -1;
                }
            } while (input < 0 || input > numberOfPages);

            currentPage = input;
            Console.WriteLine($"You are now on page {currentPage}");
        }

        private void FinishBook() //option to finish book, which sets "read" to true, and prompts user to rate the book
        {
            read = true;
            currentPage = numberOfPages;

            do
            {
                Console.Write("Please give the book a rating out of 5: ");
                try
                {
                    rating = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("That's not a valid rating.");
                    rating = -1;
                }
            } while (rating < 0 || rating > 5);
        }

        private enum BookOption //enum for options
        {
            NextPage,
            PreviousPage,
            SkipToPage,
            FinishBook,
            Close
        }
    }

    //SHELF CLASS
    
    public class Shelf
    {
        public List<Book> books = new List<Book>();

        public void AddBook(Book book) //adds book to list
        {
            books.Add(book);
        }

        public void RemoveBook(Book book) //removes book from list
        {
            books.Remove(book);
        }

        public void Print() //prints all books in list
        {
            Console.WriteLine("BOOKS:");
            foreach (Book book in books)
            {
                Console.WriteLine("");
                Console.WriteLine($"ID: {book.id}");
                Console.WriteLine($"Title: {book.title}");
                Console.WriteLine($"Author: {book.author}");
                if (book.read)
                {
                    Console.WriteLine("You have completed this book.");
                    Console.WriteLine($"Rating: {book.rating}/5");
                }
                else
                {
                    Console.WriteLine($"Current Page: {book.currentPage}");
                }
            }
        }
    }
}