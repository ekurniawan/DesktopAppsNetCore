using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPOSApps.Models
{
    public class ItemBeli
    {
        public int NoItemBeli { get; set; }
        public string No { get; set; }
        public string KodeBarang { get; set; }
        public int Jumlah { get; set; }
        public decimal HargaBeli { get; set; }
    }
}
