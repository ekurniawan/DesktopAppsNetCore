using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPOSApps.Models
{
    public class TransaksiBeli
    {
        public string No { get; set; }
        public DateTime TanggalBeli { get; set; }
        public int KodeSupplier { get; set; }
    }
}
