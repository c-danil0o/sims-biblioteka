using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CirkulacijaBiblioteke.Models;

public class Copy
{
    public int InventoryNumber { get; set; }
    public float Price { get; set; }
    public InstanceState State { get; set; }
    public Title Title { get; set; }

    public enum InstanceState
    {
        Available,
        Taken,
        Damaged
    }
}