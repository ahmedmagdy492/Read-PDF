using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Read_PDF
{
    internal class Xref
    {
        public List<XrefSection> sections;

        public Xref()
        {
            sections = new List<XrefSection>();
        }

        public string StartAddress { get; set; }
    }
}
