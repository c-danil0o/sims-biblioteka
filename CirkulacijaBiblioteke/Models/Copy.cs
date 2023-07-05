using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CirkulacijaBiblioteke.Models;

public class Copy
{
    public string TitleIsbn { get; set; }
    public int InventoryNumber { get; set; }
    public float Price { get; set; }
    public InstanceState State { get; set; }

    public Copy(int inventoryNumber, float price, InstanceState state, string titleIsbn)
    {
        InventoryNumber = inventoryNumber;
        Price = price;
        State = state;
        TitleIsbn = titleIsbn;
    }

    public enum InstanceState
    {
        Available,
        Taken,
        Damaged
    }
}