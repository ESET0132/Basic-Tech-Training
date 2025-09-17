public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    public Book(string title, string author, decimal price, int quantity)
    {
        Title = title;
        Author = author;
        Price = price;
        Quantity = quantity;
    }

    public override string ToString()
    {
        return $"{Title}\t{Author}\t${Price:F2}\t\t{Quantity}";
    }
}