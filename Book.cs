using System;

public class Book //book object parameters
{
    private static int LastAssignedID = 0;
    public int id {get; private set;}
    public string title {get; private set;}
    public string author {get; private set;}
    public int numberOfPages {get; private set;}
    public int currentPage {get; private set;}
    public bool read {get; private set;}
    public int rating {get; private set;}

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