using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Read_PDF
{
    internal class PdfFile
    {
        public readonly ICollection<Xref> xrefObjects = new List<Xref>();
        public readonly List<Trailer> trailers = new List<Trailer>();
        private readonly PdfParser pdfParser;

        public PdfFile()
        {
            pdfParser = new PdfParser();
        }

        public void GetTrailers(string fileName)
        {
            using (StreamReader sr = new StreamReader(fileName))
            {
                string fileContent = sr.ReadToEnd();
                int lastIndexOfTrailer = 0;
                while (true)
                {
                    int trailerIndex = fileContent.IndexOf("trailer", lastIndexOfTrailer);

                    if (trailerIndex == -1)
                        break;

                    trailers.Add(pdfParser.GetTrailer(fileContent, trailerIndex));
                    lastIndexOfTrailer = trailerIndex + "trailer".Length;
                }
            }
        }

        public void GetXrefObjects(string fileName)
        {
            if (trailers.Count == 0)
                throw new InvalidOperationException("No Trailers Were Found");

            for (int i = 0; i < trailers.Count; i++)
            {
                xrefObjects.Add(pdfParser.GetXrefObject(fileName, new Xref
                {
                    StartAddress = trailers[i].XrefStart
                }));
            }
        }
    }
}
