using UnityEngine;
using TMPro;
using System.Linq;
using System.Collections.Generic;

public class LibraryManager : MonoBehaviour
{
    public TMP_Text availableBooksText;
    public TMP_Text borrowedBooksText;

    private Library library;
    private List<Book> availableBooks;
    private List<Book> borrowedBooks;

    void Start()
    {
        library = new Library();
        // Test verileri ekle
        library.AddBook(new Book("Book 1", "Author 1", "ISBN1", 5));
        library.AddBook(new Book("Book 2", "Author 2", "ISBN2", 3));
        library.AddBook(new Book("Book 3", "Author 3", "ISBN3", 8));

        // Ödünç alınmamış kitapları listele
        ListAvailableBooks();
    }

    public void ListAvailableBooks()
    {
        availableBooks = library.GetAllBooks();
        UpdateAvailableBooksText("Available Books:", availableBooks);
    }

    public void ListBorrowedBooks()
    {
        borrowedBooks = library.GetAllBooks().Where(book => book.BorrowedCopyCount > 0).ToList();
        UpdateBorrowedBooksText("Borrowed Books:", borrowedBooks);
    }

    public void BorrowBook(string isbn)
    {
        var book = availableBooks.FirstOrDefault(b => b.ISBN == isbn);

        if (book != null && book.CopyCount > book.BorrowedCopyCount)
        {
            book.BorrowedCopyCount++;
            ListAvailableBooks(); // Ödünç alındıktan sonra ödünç alınmamış kitapları güncelle
            ListBorrowedBooks(); // Ödünç alındıktan sonra ödünç alınan kitapları güncelle
        }
    }

    private void UpdateAvailableBooksText(string header, List<Book> books)
    {
        availableBooksText.text = $"{header}\n";
        foreach (var book in books)
        {
            availableBooksText.text += $"{book.Title} - {book.Author} - {book.ISBN} - Copies: {book.CopyCount} - Borrowed: {book.BorrowedCopyCount}\n";
            // Eğer kitap tıklanabilir olacaksa, bir düğme ekleyebilir ve bu düğmeye bir tıklama işleyici (event handler) ekleyebilirsiniz.
            // Örneğin:
            // availableBooksText.text += $"<color=blue><u><b><size=12>{book.Title}</size></b></u></color> - {book.Author} - {book.ISBN} - Copies: {book.CopyCount} - Borrowed: {book.BorrowedCopyCount} <color=green><u><b><size=12>(Borrow)</size></b></u></color>\n";
            // Burada "Borrow" düğmesine bir tıklama işleyici ekleyerek ödünç alma işlemini gerçekleştirebilirsiniz.
        }
    }

    private void UpdateBorrowedBooksText(string header, List<Book> books)
    {
        borrowedBooksText.text = $"{header}\n";
        foreach (var book in books)
        {
            borrowedBooksText.text += $"{book.Title} - {book.Author} - {book.ISBN} - Copies: {book.CopyCount} - Borrowed: {book.BorrowedCopyCount}\n";
        }
    }
}
