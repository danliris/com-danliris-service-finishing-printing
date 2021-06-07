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

            table.Columns.Add(new DataColumn() { ColumnName = "No", DataType = typeof(String) });
            table.Columns.Add(new DataColumn() { ColumnName = "No Order", DataType = typeof(String) });
            table.Columns.Add(new DataColumn() { ColumnName = "No Kereta", DataType = typeof(String) });
            table.Columns.Add(new DataColumn() { ColumnName = "Reproses", DataType = typeof(String) });
            table.Columns.Add(new DataColumn() { ColumnName = "Mesin", DataType = typeof(String) });
            table.Columns.Add(new DataColumn() { ColumnName = "Step Proses", DataType = typeof(String) });
            table.Columns.Add(new DataColumn() { ColumnName = "Material", DataType = typeof(String) });
            table.Columns.Add(new DataColumn() { ColumnName = "Warna", DataType = typeof(String) });
            table.Columns.Add(new DataColumn() { ColumnName = "Lebar Kain (inch)", DataType = typeof(String) });
            table.Columns.Add(new DataColumn() { ColumnName = "Jenis Proses", DataType = typeof(String) });
            table.Columns.Add(new DataColumn() { ColumnName = "Tgl Input", DataType = typeof(String) });
            table.Columns.Add(new DataColumn() { ColumnName = "Jam Input", DataType = typeof(String) });
            table.Columns.Add(new DataColumn() { ColumnName = "Input", DataType = typeof(Double) });
            table.Columns.Add(new DataColumn() { ColumnName = "Tgl Output", DataType = typeof(String) });
            table.Columns.Add(new DataColumn() { ColumnName = "Jam Output", DataType = typeof(String) });
            table.Columns.Add(new DataColumn() { ColumnName = "BQ", DataType = typeof(Double) });
            table.Columns.Add(new DataColumn() { ColumnName = "BS", DataType = typeof(Double) });
            table.Columns.Add(new DataColumn() { ColumnName = "Keterangan BQ", DataType = typeof(String) });

            table.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", 0, "", "", 0, 0, "");

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
