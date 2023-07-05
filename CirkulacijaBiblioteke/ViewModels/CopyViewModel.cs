namespace CirkulacijaBiblioteke.ViewModels;

public class CopyViewModel
{
    public string Title { get; set; }
    public string Authors { get; set; }
    public int InventoryNumber { get; set; }
    public string State { get; set; }
    public float Price { get; set; }
    public string Isbn { get; set; }

    public CopyViewModel(string title, string authors, int inventoryNumber, string state, float price, string isbn)
    {
        Title = title;
        Authors = authors;
        InventoryNumber = inventoryNumber;
        State = state;
        Price = price;
        Isbn = isbn;
    }

    public override string ToString()
    {
        return $"Title: {Title}, Authors: {Authors}, InventoryNumber: {InventoryNumber}, State: {State}, Price: {Price}, Isbn: {Isbn}";
    }
}