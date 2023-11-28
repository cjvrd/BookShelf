using System;

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