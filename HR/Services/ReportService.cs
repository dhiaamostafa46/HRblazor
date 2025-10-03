using HR.Models;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.AspNetCore.Mvc;


namespace HR.Services
{
    public interface IReportService
    {
        Task<List<Employee>> GetEmployeesAsync();
        Task<string> GeneratePdfReportAsync(List<Employee> reports);
    }

    public class ReportService : IReportService
    {
        private readonly IWebHostEnvironment _env;

        public ReportService(IWebHostEnvironment env)
        {
            _env = env;
        }

        // Simulating data retrieval
        public async Task<List<Employee>> GetEmployeesAsync()
        {
            // Implement data retrieval logic here
            return new List<Employee>(); // Replace with actual data
        }
        public async Task<string> GeneratePdfReportAsync(List<Employee> reports)
        {
            var folderPath = Path.Combine(_env.WebRootPath, "pdfs");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var fileName = $"employees_table_{DateTime.Now.Ticks}.pdf";
            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                var writer = new PdfWriter(stream);
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf);

                document.Add(new Paragraph("Employees with Departments and Companies")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetFontSize(18));

                var table = new Table(5);
                // Add headers
                table.AddHeaderCell("Employee");
                table.AddHeaderCell("Job Number");
                table.AddHeaderCell("Mobile Number");
                table.AddHeaderCell("Number of Leave Requests");
                table.AddHeaderCell("Last Leave Date");
                table.AddHeaderCell("Last Leave Type");

                foreach (var report in reports)
                {
                    table.AddCell(report.Name);
                    table.AddCell(report.JobNumber);
                    table.AddCell(report.MobileNumber);
                    table.AddCell(report.LeaveRequests.Count().ToString());
                    table.AddCell(report.LeaveRequests.Last().StartDate.ToShortDateString());
                    table.AddCell(report.LeaveRequests.Last().LeaveType.TypeName);
                }


                document.Add(table);
                document.Close();
            }

            var fileUrl = $"http://localhost:24158/pdfs/{fileName}";
            return fileUrl;
        }
    }
}