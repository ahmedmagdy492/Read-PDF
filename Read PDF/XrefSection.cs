using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Read_PDF
{
    internal class XrefSection
    {
        public readonly ICollection<XrefRecord> records;

        public XrefSection()
        {
            records = new List<XrefRecord>();
        }

        public int ObjectCounts { get; set; }
        public int Index { get; set; }
    }
}
