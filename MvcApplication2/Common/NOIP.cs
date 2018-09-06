using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Data;

namespace Common
{
    public static class NOIP
    {
        public static void GetSheet()
        {
            IWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.GetSheetAt(0);
            IRow row = sheet.GetRow(0);
            ICell cell = row.GetCell(0);
            string value = cell.ToString();
        }
        public static MemoryStream RenderToExcel(DataTable table,string sheetName)
        {
            MemoryStream ms = new MemoryStream();
            using (table)
            { 
                using(IWorkbook workbook=new HSSFWorkbook())
                {
                    using(ISheet sheet=workbook.CreateSheet(sheetName))
                    {
                        HSSFCellStyle headStyle = (HSSFCellStyle)workbook.CreateCellStyle();
                        headStyle.BorderBottom = CellBorderType.THIN;
                        headStyle.BorderLeft = CellBorderType.THIN;
                        headStyle.BorderRight = CellBorderType.THIN;
                        headStyle.BorderTop = CellBorderType.THIN;

                        IRow headerRow = sheet.CreateRow(0);
                        
                       
                        foreach(DataColumn column in table.Columns)//列 表头
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue(column.Caption);
                        }
                        int rowIndex = 1;
                      
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            IRow dataRow = sheet.CreateRow(rowIndex);
                           
                            foreach(DataColumn column in table.Columns)
                            {
                                dataRow.CreateCell(column.Ordinal).SetCellValue(table.Rows[i][column.Ordinal].ToString());
                                
                            }
                            rowIndex++;
                        }
                        workbook.Write(ms);
                        ms.Flush();
                        ms.Position = 0;
                    }
                }
            }
            return ms;
        }

        public static void SaveToFile(MemoryStream ms, string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
            {
                byte[] data = ms.ToArray();
                fs.Write(data,0,data.Length);
                fs.Flush();
                data = null;
            }
        }
    }
}
