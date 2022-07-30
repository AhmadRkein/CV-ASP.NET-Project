using I3332Proj.Models.DataModels;
using Microsoft.AspNetCore.Hosting;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace I3332Proj.Services
{
    public class ExportingServices
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly DatabaseServices databaseServices;

        public ExportingServices(IWebHostEnvironment webHostEnvironment, DatabaseServices dbServices)
        {
            this.webHostEnvironment = webHostEnvironment;
            databaseServices = dbServices;
        }

        public async Task<byte[]> CreatePDF(int id)
        {
            var cv = await databaseServices.GetCV(id);

            if (cv == null)
                return null;

            return CreatePDF(cv);
        }

        public byte[] CreatePDF(CV cv)
        {
            if (cv == null)
                return null;

            // Create a new PDF document
            var document = new PdfDocument();

            // Create an empty page
            var page = document.AddPage();

            // Get an XGraphics object for drawing
            var gfx = XGraphics.FromPdfPage(page);

            // Create a font
            var font = new XFont("Verdana", 16, XFontStyle.Regular);


            var tf = new XTextFormatter(gfx);
            var photoRect = new XRect(50, 50, 200, 200);
            gfx.DrawRectangle(XBrushes.White, photoRect);

            // Draw Photo
            var photoPath = Path.Combine(webHostEnvironment.WebRootPath, cv.PhotoPath);
            gfx.DrawImage(XImage.FromFile(photoPath), photoRect);

            // Draw the text
            var textRect = new XRect(50, 300, page.Width - 50, page.Height - 300);
            tf.DrawString(GetStringContent(cv), font, XBrushes.Black, textRect, XStringFormats.TopLeft);


            // Save the document...
            //string filename = "HelloWorld.pdf";
            //document.Save(filename);

            byte[] data;
            using(var stream = new MemoryStream())
            {
                document.Save(stream);
                data = stream.ToArray();
            }

            return data;
        }

        private string GetStringContent(CV cv)
        {
            return $"" +
                $"First Name: {cv.FirstName}" + "\n\n" +
                $"Last Name: {cv.LastName}" + "\n\n" +
                $"Date of Birth: {cv.BirthDate:dd-MMM-yyyy}" + "\n\n" +
                $"Nationality: {cv.Nationality}" + "\n\n" +
                $"Gender: {cv.Gender}" + "\n\n" +
                $"Email: {cv.Email}" + "\n\n" +
                $"Programing Skills: {cv.ProgSkills.Replace(";", ", ")}" + "\n\n" +
                $"Grade: {cv.Grade}";
            
        }
    }
}
