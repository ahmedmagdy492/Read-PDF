using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Read_PDF
{
    internal class XrefRecord
    {
        public string Offset { get; set; }
        public string GenerationNumber { get; set; }
        public bool IsFree { get; set; }
    }
}
