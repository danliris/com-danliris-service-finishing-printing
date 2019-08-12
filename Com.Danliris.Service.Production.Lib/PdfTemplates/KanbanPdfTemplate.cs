using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Kanban;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;

namespace Com.Danliris.Service.Finishing.Printing.Lib.PdfTemplates
{
    public class KanbanPdfTemplate
    {
        public MemoryStream GeneratePdfTemplate(KanbanViewModel viewModel, int timeoffset)
        {
            const int MARGIN = 20;

            Font header_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 14);
            Font normal_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);
            Font body_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);
            Font normal_font_underlined = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8, Font.UNDERLINE);
            Font bold_font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);
            Font body_bold_font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);

            Document document = new Document(PageSize.A4, MARGIN, MARGIN, MARGIN, MARGIN);
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);
            document.Open();

            #region Header
            PdfPTable headerTable = new PdfPTable(4);
            PdfPCell cellHeaderCS4 = new PdfPCell() { Border = Rectangle.NO_BORDER, Colspan = 4, PaddingTop = 1, PaddingBottom = 1  };
            PdfPCell cellHeader = new PdfPCell() { Border = Rectangle.NO_BORDER, PaddingTop = 1, PaddingBottom = 1 };
            float[] widths = new float[] { 6f, 10f, 5f, 10f };
            headerTable.SetWidths(widths);
            headerTable.WidthPercentage = 100;

            cellHeaderCS4.Phrase = new Phrase("PT DAN LIRIS", header_font);
            cellHeaderCS4.HorizontalAlignment = Element.ALIGN_CENTER;
            headerTable.AddCell(cellHeaderCS4);

            cellHeaderCS4.Phrase = new Phrase("KARTU PENGANTAR PROSES PRODUKSI", normal_font_underlined);
            cellHeaderCS4.HorizontalAlignment = Element.ALIGN_CENTER;
            headerTable.AddCell(cellHeaderCS4);

            cellHeaderCS4.Phrase = new Phrase($"Delivery: {DateTime.Now.ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID"))}", body_font);
            //cellHeader.Phrase = new Phrase($"Delivery: {viewModel.ProductionOrder.DeliveryDate.Value.ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID"))}", normal_font);
            cellHeaderCS4.HorizontalAlignment = Element.ALIGN_RIGHT;
            headerTable.AddCell(cellHeaderCS4);

            cellHeaderCS4.Phrase = new Phrase("--------------------------------", bold_font);
            cellHeaderCS4.HorizontalAlignment = Element.ALIGN_RIGHT;
            headerTable.AddCell(cellHeaderCS4);

            cellHeaderCS4.Phrase = new Phrase("FM-FP-00-PC-19-020", body_bold_font);
            cellHeaderCS4.HorizontalAlignment = Element.ALIGN_RIGHT;
            headerTable.AddCell(cellHeaderCS4);

            cellHeaderCS4.Phrase = new Phrase("", bold_font);
            cellHeaderCS4.HorizontalAlignment = Element.ALIGN_CENTER;
            headerTable.AddCell(cellHeaderCS4);

            cellHeader.Phrase = new Phrase("NO ORDER", body_font);
            cellHeader.HorizontalAlignment = Element.ALIGN_LEFT;
            headerTable.AddCell(cellHeader);

            cellHeader.Phrase = new Phrase($": {viewModel.ProductionOrder.OrderNo}", body_font);
            cellHeader.HorizontalAlignment = Element.ALIGN_LEFT;
            headerTable.AddCell(cellHeader);

            cellHeader.Phrase = new Phrase("MATERIAL", body_font);
            cellHeader.HorizontalAlignment = Element.ALIGN_LEFT;
            headerTable.AddCell(cellHeader);

            cellHeader.Phrase = new Phrase($": {viewModel.ProductionOrder.Material.Name}", body_font);
            cellHeader.HorizontalAlignment = Element.ALIGN_LEFT;
            headerTable.AddCell(cellHeader);

            cellHeader.Phrase = new Phrase("BUYER", body_font);
            cellHeader.HorizontalAlignment = Element.ALIGN_LEFT;
            headerTable.AddCell(cellHeader);

            cellHeader.Phrase = new Phrase($": {viewModel.ProductionOrder.Buyer.Name}", body_font);
            cellHeader.HorizontalAlignment = Element.ALIGN_LEFT;
            headerTable.AddCell(cellHeader);

            cellHeader.Phrase = new Phrase("NO BENANG", body_font);
            cellHeader.HorizontalAlignment = Element.ALIGN_LEFT;
            headerTable.AddCell(cellHeader);

            cellHeader.Phrase = new Phrase($": {viewModel.ProductionOrder.YarnMaterial.Name}", body_font);
            cellHeader.HorizontalAlignment = Element.ALIGN_LEFT;
            headerTable.AddCell(cellHeader);

            cellHeader.Phrase = new Phrase("WARNA", body_font);
            cellHeader.HorizontalAlignment = Element.ALIGN_LEFT;
            headerTable.AddCell(cellHeader);

            var color = viewModel.SelectedProductionOrderDetail.ColorType.Id.Equals(0) ? viewModel.SelectedProductionOrderDetail.ColorRequest : viewModel.SelectedProductionOrderDetail.ColorRequest + " - " + viewModel.SelectedProductionOrderDetail.ColorType.Name;
            cellHeader.Phrase = new Phrase($": {color}", body_font);
            cellHeader.HorizontalAlignment = Element.ALIGN_LEFT;
            headerTable.AddCell(cellHeader);

            cellHeader.Phrase = new Phrase("GRADE", body_font);
            cellHeader.HorizontalAlignment = Element.ALIGN_LEFT;
            headerTable.AddCell(cellHeader);

            cellHeader.Phrase = new Phrase($": {viewModel.Grade}", body_font);
            cellHeader.HorizontalAlignment = Element.ALIGN_LEFT;
            headerTable.AddCell(cellHeader);

            cellHeader.Phrase = new Phrase("STANDARD HANDFEEL", body_font);
            cellHeader.HorizontalAlignment = Element.ALIGN_LEFT;
            headerTable.AddCell(cellHeader);

            cellHeader.Phrase = new Phrase($": {viewModel.ProductionOrder.HandlingStandard}", body_font);
            cellHeader.HorizontalAlignment = Element.ALIGN_LEFT;
            headerTable.AddCell(cellHeader);

            cellHeader.Phrase = new Phrase("PANJANG", body_font);
            cellHeader.HorizontalAlignment = Element.ALIGN_LEFT;
            headerTable.AddCell(cellHeader);

            cellHeader.Phrase = new Phrase($": {viewModel.Cart.Qty}", body_font);
            cellHeader.HorizontalAlignment = Element.ALIGN_LEFT;
            headerTable.AddCell(cellHeader);

            cellHeader.Phrase = new Phrase("LEBAR FINISH", body_font);
            cellHeader.HorizontalAlignment = Element.ALIGN_LEFT;
            headerTable.AddCell(cellHeader);

            cellHeader.Phrase = new Phrase($": {viewModel.ProductionOrder.FinishWidth}", body_font);
            cellHeader.HorizontalAlignment = Element.ALIGN_LEFT;
            headerTable.AddCell(cellHeader);

            cellHeader.Phrase = new Phrase("NO KERETA", body_font);
            cellHeader.HorizontalAlignment = Element.ALIGN_LEFT;
            headerTable.AddCell(cellHeader);

            cellHeader.Phrase = new Phrase($": {viewModel.Cart.CartNumber}", body_font);
            cellHeader.HorizontalAlignment = Element.ALIGN_LEFT;
            headerTable.AddCell(cellHeader);

            cellHeaderCS4.Phrase = new Phrase("", bold_font);
            cellHeaderCS4.HorizontalAlignment = Element.ALIGN_CENTER;
            headerTable.AddCell(cellHeaderCS4);

            cellHeaderCS4.Phrase = new Phrase("", bold_font);
            cellHeaderCS4.HorizontalAlignment = Element.ALIGN_CENTER;
            headerTable.AddCell(cellHeaderCS4);

            document.Add(headerTable);
            #endregion Header

            #region Table 1
            PdfPTable bodyTable1 = new PdfPTable(2);
            bodyTable1.SplitLate = false;
            float[] bodyTable1Widths = new float[] { 50f, 50f };
            bodyTable1.SetWidths(bodyTable1Widths);
            bodyTable1.WidthPercentage = 100;

            PdfPTable bodyTable1Left = new PdfPTable(6);
            bodyTable1Left.SplitLate = false;
            float[] bodyTable1LeftWidths = new float[] { 10f, 10f, 10f, 50f, 10f, 10f };
            bodyTable1Left.SetWidths(bodyTable1LeftWidths);
            bodyTable1Left.WidthPercentage = 100;

            PdfPTable bodyTable1Right = new PdfPTable(4);
            bodyTable1Right.SplitLate = false;
            float[] bodyTable1RightWidths = new float[] { 30f, 20f, 30f, 20f };
            bodyTable1Right.SetWidths(bodyTable1RightWidths);
            bodyTable1Right.WidthPercentage = 100;

            #region Set Table 1 Header
            PdfPCell table1LeftCellHeader = new PdfPCell() { FixedHeight = 20 };
            PdfPCell table1RightCellHeader = new PdfPCell() { FixedHeight = 20, Colspan = 4 };

            table1LeftCellHeader.Phrase = new Phrase("TGL", body_bold_font);
            table1LeftCellHeader.HorizontalAlignment = Element.ALIGN_CENTER;
            table1LeftCellHeader.VerticalAlignment = Element.ALIGN_CENTER;
            bodyTable1Left.AddCell(table1LeftCellHeader);

            table1LeftCellHeader.Phrase = new Phrase("MESIN", body_bold_font);
            table1LeftCellHeader.HorizontalAlignment = Element.ALIGN_CENTER;
            table1LeftCellHeader.VerticalAlignment = Element.ALIGN_CENTER;
            bodyTable1Left.AddCell(table1LeftCellHeader);

            table1LeftCellHeader.Phrase = new Phrase("SHIFT", body_bold_font);
            table1LeftCellHeader.HorizontalAlignment = Element.ALIGN_CENTER;
            table1LeftCellHeader.VerticalAlignment = Element.ALIGN_CENTER;
            bodyTable1Left.AddCell(table1LeftCellHeader);

            table1LeftCellHeader.Phrase = new Phrase("KETERANGAN", body_bold_font);
            table1LeftCellHeader.HorizontalAlignment = Element.ALIGN_CENTER;
            table1LeftCellHeader.VerticalAlignment = Element.ALIGN_CENTER;
            bodyTable1Left.AddCell(table1LeftCellHeader);

            table1LeftCellHeader.Phrase = new Phrase("NAMA", body_bold_font);
            table1LeftCellHeader.HorizontalAlignment = Element.ALIGN_CENTER;
            table1LeftCellHeader.VerticalAlignment = Element.ALIGN_CENTER;
            bodyTable1Left.AddCell(table1LeftCellHeader);

            table1LeftCellHeader.Phrase = new Phrase("PARAF", body_bold_font);
            table1LeftCellHeader.HorizontalAlignment = Element.ALIGN_CENTER;
            table1LeftCellHeader.VerticalAlignment = Element.ALIGN_CENTER;
            bodyTable1Left.AddCell(table1LeftCellHeader);

            table1RightCellHeader.Phrase = new Phrase("FLOW PROSES", body_bold_font);
            table1RightCellHeader.HorizontalAlignment = Element.ALIGN_CENTER;
            table1RightCellHeader.VerticalAlignment = Element.ALIGN_CENTER;
            bodyTable1Right.AddCell(table1RightCellHeader);
            #endregion Set Table 1 Header

            #region Set Table 1 Value
            PdfPCell table1LeftCell = new PdfPCell() { FixedHeight = 15 };
            PdfPCell table1RightCellRS2CS4 = new PdfPCell() { Colspan = 4, FixedHeight = 30 };
            PdfPCell table1RightCellNoRightBorder = new PdfPCell() { FixedHeight = 15, BorderWidthRight = 0 };
            PdfPCell table1RightCellNoLeftBorder = new PdfPCell() { FixedHeight = 15, BorderWidthLeft = 0 };

            int totalRow = 0;
            foreach (var step in viewModel.Instruction.Steps)
            {
                table1RightCellRS2CS4.Phrase = new Phrase(step.Process, body_font);
                table1RightCellRS2CS4.HorizontalAlignment = Element.ALIGN_CENTER;
                table1RightCellRS2CS4.VerticalAlignment = Element.ALIGN_CENTER;
                bodyTable1Right.AddCell(table1RightCellRS2CS4);
                totalRow += 2;

                int countIndex = 0;
                foreach (var stepIndicator in step.StepIndicators)
                {
                    table1RightCellNoRightBorder.Phrase = new Phrase(stepIndicator.Name, body_font);
                    table1RightCellNoRightBorder.HorizontalAlignment = Element.ALIGN_LEFT;
                    bodyTable1Right.AddCell(table1RightCellNoRightBorder);

                    table1RightCellNoLeftBorder.Phrase = new Phrase($": {stepIndicator.Value} {stepIndicator.Uom}", body_font);
                    table1RightCellNoLeftBorder.HorizontalAlignment = Element.ALIGN_LEFT;
                    bodyTable1Right.AddCell(table1RightCellNoLeftBorder);

                    countIndex++;
                    if (countIndex == 2)
                    {
                        totalRow++;
                        countIndex = 0;
                    }
                }

                if (step.StepIndicators.Count % 2 != 0)
                {
                    table1RightCellNoRightBorder.Phrase = new Phrase("", body_font);
                    table1RightCellNoRightBorder.HorizontalAlignment = Element.ALIGN_LEFT;
                    bodyTable1Right.AddCell(table1RightCellNoRightBorder);

                    table1RightCellNoLeftBorder.Phrase = new Phrase("", body_font);
                    table1RightCellNoLeftBorder.HorizontalAlignment = Element.ALIGN_LEFT;
                    bodyTable1Right.AddCell(table1RightCellNoLeftBorder);
                    totalRow++;
                }
            }

            for (int i = 0; i < totalRow; i++)
            {
                AddSixCellTableRow(table1LeftCell, bodyTable1Left);
            }

            PdfPCell bodyTable1LeftCol = new PdfPCell() { Border = Rectangle.NO_BORDER, Padding = 0 };
            bodyTable1LeftCol.AddElement(bodyTable1Left);
            PdfPCell bodyTable1RightCol = new PdfPCell() { Border = Rectangle.NO_BORDER, Padding = 0 };
            bodyTable1RightCol.AddElement(bodyTable1Right);

            bodyTable1.AddCell(bodyTable1LeftCol);
            bodyTable1.AddCell(bodyTable1RightCol);

            //try
            //{
            //    document.Add(bodyTable1);
            //}
            //catch(Exception e)
            //{
            //    throw new Exception(e.Message);
            //}

            #endregion Set Table 1 Value

            document.Add(bodyTable1);
            #endregion Table 1

            document.Add(new Paragraph("\n"));

            #region Table 2
            int StepTotal = viewModel.Instruction.Steps.Count;
            int Columns = StepTotal + 5;
            PdfPTable bodyTable2 = new PdfPTable(Columns);
            float[] bodyTable2Widths = new float[Columns];

            for (int i = 0; i < Columns; i++)
            {
                if (i > 3 && i < Columns - 1)
                {
                    bodyTable2Widths[i] = 80 / StepTotal;
                }
                else
                {
                    bodyTable2Widths[i] = 5;
                }
            }
            bodyTable2.SetWidths(bodyTable2Widths);
            bodyTable2.WidthPercentage = 100;

            PdfPCell table2CellHeader = new PdfPCell();
            PdfPCell table2CellHeaderCS3 = new PdfPCell() { Colspan = 3 };
            PdfPCell table2CellHeaderRS2 = new PdfPCell() { Rowspan = 2 };

            table2CellHeaderRS2.Phrase = new Phrase("NO PCS", body_bold_font);
            table2CellHeaderRS2.HorizontalAlignment = Element.ALIGN_CENTER;
            table2CellHeaderRS2.VerticalAlignment = Element.ALIGN_CENTER;
            bodyTable2.AddCell(table2CellHeaderRS2);

            table2CellHeaderCS3.Phrase = new Phrase("PANJANG", body_bold_font);
            table2CellHeaderCS3.HorizontalAlignment = Element.ALIGN_CENTER;
            table2CellHeaderCS3.VerticalAlignment = Element.ALIGN_CENTER;
            bodyTable2.AddCell(table2CellHeaderCS3);

            foreach (var step in viewModel.Instruction.Steps)
            {
                table2CellHeaderRS2.Phrase = new Phrase(step.Alias, body_bold_font);
                table2CellHeaderRS2.HorizontalAlignment = Element.ALIGN_CENTER;
                table2CellHeaderRS2.VerticalAlignment = Element.ALIGN_CENTER;
                bodyTable2.AddCell(table2CellHeaderRS2);
            }

            table2CellHeaderRS2.Phrase = new Phrase("NO PCS", body_bold_font);
            table2CellHeaderRS2.HorizontalAlignment = Element.ALIGN_CENTER;
            table2CellHeaderRS2.VerticalAlignment = Element.ALIGN_CENTER;
            bodyTable2.AddCell(table2CellHeaderRS2);

            table2CellHeader.Phrase = new Phrase("ASLI", body_bold_font);
            table2CellHeader.HorizontalAlignment = Element.ALIGN_CENTER;
            table2CellHeader.VerticalAlignment = Element.ALIGN_CENTER;
            bodyTable2.AddCell(table2CellHeader);

            table2CellHeader.Phrase = new Phrase("GRADE", body_bold_font);
            table2CellHeader.HorizontalAlignment = Element.ALIGN_CENTER;
            table2CellHeader.VerticalAlignment = Element.ALIGN_CENTER;
            bodyTable2.AddCell(table2CellHeader);

            table2CellHeader.Phrase = new Phrase("CHECK", body_bold_font);
            table2CellHeader.HorizontalAlignment = Element.ALIGN_CENTER;
            table2CellHeader.VerticalAlignment = Element.ALIGN_CENTER;
            bodyTable2.AddCell(table2CellHeader);

            for (int i = 1; i <= 30; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    string index = "";
                    if (j == 0)
                        index = i.ToString();
                    table2CellHeader.Phrase = new Phrase(index, body_font);
                    table2CellHeader.HorizontalAlignment = Element.ALIGN_CENTER;
                    table2CellHeader.VerticalAlignment = Element.ALIGN_CENTER;
                    bodyTable2.AddCell(table2CellHeader);
                }
            }

            PdfPCell signatureCell = new PdfPCell() { FixedHeight = 30, Colspan = 3 };
            PdfPCell signatureBlankCell = new PdfPCell() { FixedHeight = 30 };

            signatureCell.Phrase = new Phrase("PARAF CHECK", body_bold_font);
            signatureCell.HorizontalAlignment = Element.ALIGN_CENTER;
            signatureCell.VerticalAlignment = Element.ALIGN_CENTER;
            bodyTable2.AddCell(signatureCell);


            for (int j = 3; j < Columns; j++)
            {
                signatureBlankCell.Phrase = new Phrase("", body_font);
                signatureBlankCell.HorizontalAlignment = Element.ALIGN_CENTER;
                signatureBlankCell.VerticalAlignment = Element.ALIGN_CENTER;
                bodyTable2.AddCell(signatureBlankCell);
            }

            PdfPCell bottomCell = new PdfPCell() { FixedHeight = 40, Colspan = 3 };
            PdfPCell bottomBlankCell = new PdfPCell() { FixedHeight = 40 };

            bottomCell.Phrase = new Phrase("KETERANGAN", body_bold_font);
            bottomCell.HorizontalAlignment = Element.ALIGN_CENTER;
            bottomCell.VerticalAlignment = Element.ALIGN_CENTER;
            bodyTable2.AddCell(bottomCell);

            for (int j = 3; j < Columns; j++)
            {
                bottomBlankCell.Phrase = new Phrase("", body_font);
                bottomBlankCell.HorizontalAlignment = Element.ALIGN_CENTER;
                bottomBlankCell.VerticalAlignment = Element.ALIGN_CENTER;
                bodyTable2.AddCell(bottomBlankCell);
            }

            document.Add(bodyTable2);
            #endregion Table 2

            document.Close();
            byte[] byteInfo = stream.ToArray();
            stream.Write(byteInfo, 0, byteInfo.Length);
            stream.Position = 0;

            return stream;
        }

        private void AddSixCellTableRow(PdfPCell table1LeftCell, PdfPTable bodyTable1Left)
        {
            Font body_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 9);
            for (var i = 0; i < 6; i++)
            {
                table1LeftCell.Phrase = new Phrase("", body_font);
                table1LeftCell.HorizontalAlignment = Element.ALIGN_CENTER;
                bodyTable1Left.AddCell(table1LeftCell);
            }
        }
    }
}
