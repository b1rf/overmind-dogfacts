using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using OvermindDogFacts.Model;

namespace OvermindDogFacts.Utils
{
    internal class ExcelUtils
    {
        /*
            References

            https://github.com/age-soft/npoi
            https://www.thecodebuzz.com/read-and-write-excel-file-in-net-core-using-npoi/
         */

        public static List<string> ReadExcel(string file)
        {
            List<string> rowsList = new List<string>();

            ISheet sheet;

            using (var stream = new FileStream(file, FileMode.Open))
            {
                stream.Position = 0;
                XSSFWorkbook xssWorkbook = new XSSFWorkbook(stream);

                sheet = xssWorkbook.GetSheetAt(0);
                
                for (int i = (sheet.FirstRowNum); i <= sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);

                    if (row == null) continue;
                    if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;

                    if (row.GetCell(0) != null)
                    {
                        if (!string.IsNullOrEmpty(row.GetCell(0).ToString()) && !string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))
                        {
                            rowsList.Add(row.GetCell(0).ToString());
                        }
                    }
                }
            }

            return rowsList;
        }

        internal static Task WriteExcel(List<DogFact> facts, string outputFile)
        {
            // Lets converts our object data to Datatable for a simplified logic.
            // Datatable is most easy way to deal with complex datatypes for easy reading and formatting.
            var memoryStream = new MemoryStream();

            using (var fs = new FileStream(outputFile, FileMode.CreateNew, FileAccess.Write))
            {
                IWorkbook workbook = new XSSFWorkbook();

                foreach (DogFact fact in facts) 
                {
                    ISheet excelSheet = workbook.CreateSheet(fact.dog.ToString());

                    int rowIndex = 0;

                    foreach (Fact item in fact.facts)
                    {
                        var row = excelSheet.CreateRow(rowIndex);
                        row.CreateCell(0).SetCellValue(item.fact);

                        rowIndex++;
                    }
                }

                workbook.Write(fs);
            }

            return Task.CompletedTask;
        }
    }
}
