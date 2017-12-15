namespace LearningSystem.Services.Implementations
{
    using iTextSharp.text;
    using iTextSharp.text.html.simpleparser;
    using iTextSharp.text.pdf;
    using System.IO;

    public class PdfGenerator : IPdfGenerator
    {
        public byte[] GeneratePdfFromHtml(string html)
        {
            var pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            var htmlparser = new HtmlWorker(pdfDoc);

            using (var memoryStream = new MemoryStream())
            {
                var writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                pdfDoc.Open();

                using (var reader = new StringReader(html))
                {
                    htmlparser.Parse(reader);
                }

                pdfDoc.Close();

                return memoryStream.ToArray();
            }
        }
    }
}
