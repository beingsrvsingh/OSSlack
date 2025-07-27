using Identity.Application.Contracts;
using Identity.Application.Services.Interfaces;
using OfficeOpenXml;

namespace Identity.Infrastructure.Services
{
    public class ExcelService : IExcelService
    {
        public List<ExcelDTO> ReadLocationExcel(string path)
        {
            var list = new List<ExcelDTO>();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var package = new ExcelPackage(new FileInfo(path));
            var worksheet = package.Workbook.Worksheets[0];

            int colCount = worksheet.Dimension.End.Column;
            int rowCount = worksheet.Dimension.End.Row;

            var headers = new Dictionary<string, int>();
            for (int col = 1; col <= colCount; col++)
            {
                var header = worksheet.Cells[1, col].Text.Trim();
                if (!string.IsNullOrEmpty(header)) headers[header] = col;
            }

            for (int row = 2; row <= rowCount; row++)
            {
                var dto = new ExcelDTO
                {
                    Pincode = Convert.ToInt32(worksheet.Cells[row, headers["Pincode"]].Value),
                    DistrictName = worksheet.Cells[row, headers["District"]].Text.Trim(),
                    AreaName = worksheet.Cells[row, headers["OfficeName"]].Text.Trim(),
                    StateName = worksheet.Cells[row, headers["StateName"]].Text.Trim()
                };

                list.Add(dto);
            }

            return list;
        }

    }
}