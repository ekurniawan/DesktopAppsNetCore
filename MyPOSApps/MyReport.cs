using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPOSApps
{
    public class MyReport
    {
        public string KodeBarang { get; set; }
        public string NamaBarang { get; set; }
        public decimal HargaBeli { get; set; }
        public decimal HargaJual { get; set; }
        public int Stok { get; set; }
        public DateTime TanggalBeli { get; set; }
    }
}
