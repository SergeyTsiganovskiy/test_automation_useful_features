using System;
using System.IO;
using System.Text;
using iTextSharp.text.pdf;

namespace test_automation_useful_features
{
   public class PdfUtility
    {
        /// <summary>
        /// Extract text from pdf File
        /// </summary>
        /// <param name="path"></param>
        /// <param name="createOutputTextFile"></param>
        /// <returns></returns>
        public string PdfExtractor(string path,bool createOutputTextFile = false)
        {
            string contents;

            StringBuilder text = new StringBuilder();
            using (PdfReader reader = new PdfReader(path))
            {
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    text.Append(iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(reader, i));
                }
            }
            contents = text.ToString();
            if (createOutputTextFile)
            {
                string testFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "pdfText.txt");
                File.WriteAllText(testFilePath , contents);
            }
            return contents;
        }
    }
}
