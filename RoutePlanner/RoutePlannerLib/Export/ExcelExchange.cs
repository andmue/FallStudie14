using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Export
{
    class ExcelExchange
    {
        Application _excelApp;
        
        public ExcelExchange()
        {
            _excelApp = new Application();
        }

        public void WriteToFile(String fileName, City from, City to, List<Link> links)
        {
            var workbook = _excelApp.Workbooks.Add();
            var worksheet = workbook.Worksheets[1];

            worksheet.Cells[1, 1] = "From";
            worksheet.Cells[1, 2] = "To";
            worksheet.Cells[1, 3] = "Distance";
            worksheet.Cells[1, 4] = "Transport Mode";

            Range range;
            range = worksheet.get_Range("A1", "D1");
            range.Font.Size = 14;
            range.Font.Bold = true;
            range.BorderAround2(XlLineStyle.xlContinuous, XlBorderWeight.xlThin);

            int row = 2;
            foreach (var l in links)
            {
                worksheet.Cells[row, 1] = l.FromCity.Name;
                worksheet.Cells[row, 2] = l.ToCity.Name;
                worksheet.Cells[row, 3] = l.Distance;
                worksheet.Cells[row, 4] = l.TransportMode;
                ++row;
            }

            workbook.SaveAs(fileName, XlFileFormat.xlOpenXMLWorkbook);
            workbook.Close();
        }

        
    }
}
