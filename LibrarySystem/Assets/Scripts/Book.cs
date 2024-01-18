[System.Serializable]
public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public int CopyCount { get; set; }
    public int BorrowedCopyCount { get; set; }

    public Book(string title, string author, string isbn, int copyCount)
    {
        Title = title;
        Author = author;
        ISBN = isbn;
        CopyCount = copyCount;
        BorrowedCopyCount = 0;
    }
}
