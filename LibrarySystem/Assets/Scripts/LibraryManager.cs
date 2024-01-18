using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

public class LibraryManager : MonoBehaviour
{
    public Text resultText;
    private Library library;

    private void Start()
    {
        library = new Library();
        // Add test data
        library.AddBook(new Book("Book 1", "Author 1", "ISBN1", 5));
        library.AddBook(new Book("Book 2", "Author 2", "ISBN2", 3));
        library.AddBook(new Book("Book 3", "Author 3", "ISBN3", 8));

        // Perform sample operations
        ListAllBooks();
        SearchBooks("Author 2");
        BorrowBook("ISBN1");
        ReturnBook("ISBN1");
        ListOverdueBooks();
    }

    public void ListAllBooks()
    {
        var books = library.GetAllBooks();
        DisplayResult("All Books:", books);
    }

    public void SearchBooks(string keyword)
    {
        var searchResults = library.GetAllBooks().Where(book => book.Author.Contains(keyword) || book.Title.Contains(keyword)).ToList();
        DisplayResult($"Search Results ({keyword}):", searchResults);
    }

    public void BorrowBook(string isbn)
    {
        var book = library.GetAllBooks().FirstOrDefault(b => b.ISBN == isbn);

        if (book != null && book.CopyCount > book.BorrowedCopyCount)
        {
            book.BorrowedCopyCount++;
            DisplayResult($"{book.Title} borrowed successfully.", new List<Book> { book }); // Tek bir kitap listesi ile çağrıldı.
        }
        else
        {
            DisplayResult("Book could not be borrowed. Either there is not enough stock or the book is not found.", new List<Book>());
        }
    }

    public void ReturnBook(string isbn)
    {
        var book = library.GetAllBooks().FirstOrDefault(b => b.ISBN == isbn);

        if (book != null && book.BorrowedCopyCount > 0)
        {
            book.BorrowedCopyCount--;
            DisplayResult($"{book.Title} returned successfully.", new List<Book> { book }); // Tek bir kitap listesi ile çağrıldı.
        }
        else
        {
            DisplayResult("Book could not be returned. No borrowed copies found.", new List<Book>());
        }
    }

    public void ListOverdueBooks()
    {
        var overdueBooks = library.GetAllBooks().Where(book => book.BorrowedCopyCount > 0).ToList();
        DisplayResult("Overdue Books:", overdueBooks);
    }

    private void DisplayResult(string header, List<Book> books)
    {
        resultText.text = $"{header}\n";
        foreach (var book in books)
        {
            resultText.text += $"{book.Title} - {book.Author} - {book.ISBN} - Copies: {book.CopyCount} - Borrowed: {book.BorrowedCopyCount}\n";
        }
    }
}
