using I3332Proj.Models.DataModels;
using Microsoft.AspNetCore.Hosting;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using System.Diagnostics;
using System.IO;

namespace I3332Proj.Services
{
    public class ExportingServices
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public ExportingServices(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        public void CreatePDF(CV cv)
        {
            // Create a new PDF document
            PdfDocument document = new PdfDocument();

            // Create an empty page
            PdfPage page = document.AddPage();

            // Get an XGraphics object for drawing
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Create a font
            XFont font = new XFont("Verdana", 16, XFontStyle.Regular);


            XTextFormatter tf = new XTextFormatter(gfx);
            XRect photoRect = new XRect(50, 50, 200, 200);
            gfx.DrawRectangle(XBrushes.White, photoRect);

            // Draw Photo
            var photoPath = Path.Combine(webHostEnvironment.WebRootPath, cv.PhotoPath.Substring(1));
            gfx.DrawImage(XImage.FromFile(photoPath), photoRect);

            // Draw the text
            XRect textRect = new XRect(50, 300, page.Width - 50, page.Height - 300);
            tf.DrawString(getString(cv), font, XBrushes.Black, textRect, XStringFormats.TopLeft);


            // Save the document...
            string filename = "HelloWorld.pdf";
            document.Save(filename);
        }

        private string getString(CV cv)
        {
            var result = $"" +
                $"First Name: {cv.FirstName}" + "\n\n" +
                $"Last Name: {cv.LastName}" + "\n\n" +
                $"Date of Birth: {cv.BirthDate:dd-MMM-yyyy}" + "\n\n" +
                $"Nationality: {cv.Nationality}" + "\n\n" +
                $"Gender: {cv.Gender}" + "\n\n" +
                $"Email: {cv.Email}" + "\n\n" +
                $"Programing Skills: {cv.ProgSkills.Replace(";", ", ")}" + "\n\n" +
                $"Grade: {cv.Grade}";
            
            return result;
        }
    }
}
