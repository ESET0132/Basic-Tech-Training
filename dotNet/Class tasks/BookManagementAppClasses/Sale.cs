public class Sale
{
    public string CustomerName { get; set; }
    public string BookTitle { get; set; }
    public int Quantity { get; set; }
    public decimal Amount { get; set; }
    public DateTime SaleDate { get; set; }

    public Sale(string customerName, string bookTitle, int quantity, decimal amount)
    {
        CustomerName = customerName;
        BookTitle = bookTitle;
        Quantity = quantity;
        Amount = amount;
        SaleDate = DateTime.Now;
    }

    public override string ToString()
    {
        return $"{CustomerName}\t\t{BookTitle}\t{Quantity}\t\t\t${Amount:F2}";
    }
}