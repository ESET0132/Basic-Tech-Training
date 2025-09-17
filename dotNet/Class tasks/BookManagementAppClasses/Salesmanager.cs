public class SalesManager
{
    private List<Sale> sales = new List<Sale>();

    public void RecordSale(string customerName, string bookTitle, int quantity, decimal amount)
    {
        var sale = new Sale(customerName, bookTitle, quantity, amount);
        sales.Add(sale);
    }

    public List<Sale> GetAllSales()
    {
        return new List<Sale>(sales);
    }

    public decimal GetTotalSalesAmount()
    {
        return sales.Sum(s => s.Amount);
    }

    public bool HasSales()
    {
        return sales.Count > 0;
    }
}