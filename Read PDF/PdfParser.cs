using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Read_PDF
{
    internal class PdfParser
    {
        public Trailer GetTrailer(string fileContent, int startSearchIndex = 0)
        {
            // get the last trailer section
            int trailerIndex = fileContent.IndexOf("trailer", startSearchIndex);
            int startOfTrailer = fileContent.IndexOf("<<", trailerIndex);
            int endOfTrailer = fileContent.IndexOf(">>", trailerIndex);
            string trailerSection = fileContent.Substring(startOfTrailer + 2, endOfTrailer - (startOfTrailer + 2));

            string[] trailerLines = trailerSection.Split('/');
            int startIndexOfStartXref = fileContent.IndexOf("startxref", endOfTrailer + 2) + "startxref".Length;
            int endIndexOfStartXref = fileContent.IndexOf("%%EOF", endOfTrailer);

            return new Trailer
            {
                Info = trailerLines.FirstOrDefault(i => i.Contains("Info"))?.Replace("\n", ""),
                ID = trailerLines.FirstOrDefault(i => i.Contains("ID"))?.Replace("\n", ""),
                Size = trailerLines.FirstOrDefault(i => i.Contains("Size"))?.Replace("\n", ""),
                Root = trailerLines.FirstOrDefault(i => i.Contains("Root"))?.Replace("\n", ""),
                Prev = trailerLines.FirstOrDefault(i => i.Contains("Prev"))?.Replace("\n", ""),
                XrefStart = fileContent.Substring(startIndexOfStartXref, endIndexOfStartXref - startIndexOfStartXref)?.Replace("\n", "")
            };
        }

        public Xref GetXrefObject(string fileName, Xref xref)
        {
            using (StreamReader sr = new StreamReader(fileName))
            {
                sr.BaseStream.Seek(Convert.ToInt64(xref.StartAddress), SeekOrigin.Begin);
                string readLine = string.Empty;

                while ((readLine = sr.ReadLine()) != null)
                {
                    if (readLine.Contains("trailer"))
                        break;

                    string[] fields = readLine.Split(' ');
                    if(fields.Length == 2)
                    {
                        xref.sections.Add(new XrefSection
                        {
                            Index = Convert.ToInt32(fields[0]),
                            ObjectCounts = Convert.ToInt32(fields[1])
                        });
                    }
                    if(fields.Length >= 3)
                    {
                        XrefRecord xrefRecord = new XrefRecord
                        {
                            Offset = fields[0],
                            GenerationNumber = fields[1],
                            IsFree = fields[2].Contains('f') ? true : false
                        };
                        xref.sections[xref.sections.Count - 1].records.Add(xrefRecord);
                    }
                }
                return xref;
            }
        }
    }
}
