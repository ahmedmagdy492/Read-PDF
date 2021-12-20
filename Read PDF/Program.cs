using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Read_PDF
{
    internal class Program
    {
        //const string FileName = "TSQL querying.pdf";
        const string FileName = "el salat 3la el naby.pdf";
        static void Main(string[] args)
        {
            var root = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            PdfFile pdfFile = new PdfFile();
            pdfFile.GetTrailers(Path.Combine(root, FileName));
            pdfFile.GetXrefObjects(Path.Combine(root, FileName));

        }
    }
}
