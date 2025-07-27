using Identity.Application.Contracts;

namespace Identity.Application.Services.Interfaces
{
    public interface IExcelService
    {
        List<ExcelDTO> ReadLocationExcel(string path);
    }
}