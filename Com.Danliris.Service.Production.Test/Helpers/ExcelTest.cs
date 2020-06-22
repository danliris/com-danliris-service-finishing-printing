using Com.Danliris.Service.Finishing.Printing.Lib.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.Helpers
{
   public  class ExcelTest
    {

        enum Align
        {
            horizontal = 1,
            vertical = 1,
        }

        [Fact]
        public void CreateExcel_Return_Success()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Dosage", typeof(int));
            table.Columns.Add("Drug", typeof(string));
            table.Columns.Add("Patient", typeof(string));
            table.Columns.Add("Date", typeof(DateTime));

            // Step 3: here we add 5 rows.
            table.Rows.Add(25, "Indocin", "David", DateTime.Now);
            table.Rows.Add(50, "Enebrel", "Sam", DateTime.Now);
            table.Rows.Add(10, "Hydralazine", "Christoff", DateTime.Now);
            table.Rows.Add(21, "Combivent", "Janet", DateTime.Now);
            table.Rows.Add(100, "Dilantin", "Melanie", DateTime.Now);

            var mergeCells = new List<(string cells, Enum hAlign, Enum vAlign)>
                          {
                              ("A1",Align.horizontal,Align.vertical)
                              
                          };
            var dtSourceList = new List<(DataTable table, string sheetName, List<(string cells, Enum hAlign, Enum vAlign)> mergeCells)>()
            {
                (table,"sheetName",mergeCells) 
            };

            var result = Excel.CreateExcel(dtSourceList, true);

        }
    }
}
