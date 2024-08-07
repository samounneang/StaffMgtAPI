using OfficeOpenXml;
using StaffMgtBlazorApp.Models;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout;
namespace StaffMgtBlazorApp.Services
{
    public class ExportService
    {
        public ExportService()
        {
            // Ensure the EPPlus license context is set
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public async Task<byte[]> ExportToExcelAsync(IEnumerable<StaffDto> staffs)
        {
            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Staffs");

            worksheet.Cells[1, 1].Value = "Staff ID";
            worksheet.Cells[1, 2].Value = "Full Name";
            worksheet.Cells[1, 3].Value = "Birthday";
            worksheet.Cells[1, 4].Value = "Gender";

            var row = 2;
            foreach (var staff in staffs)
            {
                worksheet.Cells[row, 1].Value = staff.StaffId;
                worksheet.Cells[row, 2].Value = staff.FullName;
                worksheet.Cells[row, 3].Value = staff.Birthday.ToShortDateString();
                worksheet.Cells[row, 4].Value = staff.Gender == 1 ? "Male" : "Female";
                row++;
            }

            return await package.GetAsByteArrayAsync();
        }
        public async Task<byte[]> ExportToPdfAsync(List<StaffDto> staffList)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var writer = new PdfWriter(memoryStream))
                {
                    using (var pdf = new PdfDocument(writer))
                    {
                        var document = new Document(pdf);

                        // Add a title
                        document.Add(new Paragraph("Staff Report").SetFontSize(18));

                        // Add a table with staff information
                        var table = new Table(new float[] { 1, 2, 2, 2 }).UseAllAvailableWidth();
                        table.AddHeaderCell("ID");
                        table.AddHeaderCell("Full Name");
                        table.AddHeaderCell("Birthday");
                        table.AddHeaderCell("Gender");

                        foreach (var staff in staffList)
                        {
                            table.AddCell(staff.StaffId);
                            table.AddCell(staff.FullName);
                            table.AddCell(staff.Birthday.ToShortDateString());
                            table.AddCell(staff.Gender.ToString());
                        }

                        document.Add(table);
                    }
                }

                return memoryStream.ToArray();
            }
        }
    }
}
