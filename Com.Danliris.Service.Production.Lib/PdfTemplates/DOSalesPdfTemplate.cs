using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.DOSales;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Com.Danliris.Service.Finishing.Printing.Lib.PdfTemplates
{
    public class DOSalesPdfTemplate
    {
        public MemoryStream GeneratePdfTemplate(DOSalesViewModel viewModel, int clientTimeZoneOffset)
        {
            const int MARGIN = 15;

            Font header_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 18);
            Font normal_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);
            Font bold_font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);

            Document document = new Document(PageSize.A4, MARGIN, MARGIN, MARGIN, MARGIN);
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);
            document.Open();

            #region Header

            PdfPTable headerTable = new PdfPTable(2);
            headerTable.SetWidths(new float[] { 10f, 10f });
            headerTable.WidthPercentage = 100;
            PdfPTable headerTable1 = new PdfPTable(1);
            PdfPTable headerTable2 = new PdfPTable(1);

            PdfPCell cellHeader1 = new PdfPCell() { Border = Rectangle.NO_BORDER };
            PdfPCell cellHeader2 = new PdfPCell() { Border = Rectangle.NO_BORDER };
            PdfPCell cellHeaderBody = new PdfPCell() { Border = Rectangle.NO_BORDER };

            PdfPCell cellHeaderCS2 = new PdfPCell() { Border = Rectangle.NO_BORDER, Colspan = 2 };

            cellHeaderBody.Phrase = new Phrase("PT. DANLIRIS", header_font);
            headerTable1.AddCell(cellHeaderBody);

            cellHeaderBody.Phrase = new Phrase("", header_font);
            headerTable1.AddCell(cellHeaderBody);

            cellHeaderBody.Phrase = new Phrase("No. " + viewModel.DOSalesNo, bold_font);
            headerTable1.AddCell(cellHeaderBody);
            cellHeaderBody.Phrase = new Phrase("FM-PJ-00-03-005 / R2", bold_font);
            headerTable1.AddCell(cellHeaderBody);

            cellHeaderBody.Phrase = new Phrase("", header_font);
            headerTable1.AddCell(cellHeaderBody);

            cellHeaderBody.Phrase = new Phrase("Harap dikeluarkan barang tersebut di bawah ini : ", normal_font);
            headerTable1.AddCell(cellHeaderBody);

            cellHeader1.AddElement(headerTable1);
            headerTable.AddCell(cellHeader1);
            cellHeaderBody.Phrase = new Phrase("", normal_font);
            headerTable2.AddCell(cellHeaderBody);

            cellHeaderBody.HorizontalAlignment = Element.ALIGN_CENTER;
            cellHeaderBody.Phrase = new Phrase("Sukoharjo, " + viewModel.DOSalesDate?.AddHours(clientTimeZoneOffset).ToString("dd MMMM yyyy", new CultureInfo("id-ID")), normal_font);
            headerTable2.AddCell(cellHeaderBody);

            cellHeaderBody.HorizontalAlignment = Element.ALIGN_CENTER;
            cellHeaderBody.Phrase = new Phrase("Kepada", normal_font);
            headerTable2.AddCell(cellHeaderBody);

            cellHeaderBody.HorizontalAlignment = Element.ALIGN_CENTER;
            cellHeaderBody.Phrase = new Phrase("Yth. Bpk./Ibu. " + viewModel.HeadOfStorage, normal_font);
            headerTable2.AddCell(cellHeaderBody);

            cellHeaderBody.HorizontalAlignment = Element.ALIGN_CENTER;
            cellHeaderBody.Phrase = new Phrase("Bag. " + viewModel.StorageName + " / Div. " + viewModel.StorageDivision, normal_font);
            headerTable2.AddCell(cellHeaderBody);

            cellHeaderBody.HorizontalAlignment = Element.ALIGN_CENTER;
            cellHeaderBody.Phrase = new Phrase("D.O. PENJUALAN", bold_font);
            headerTable2.AddCell(cellHeaderBody);
            cellHeaderBody.Phrase = new Phrase("", normal_font);
            headerTable2.AddCell(cellHeaderBody);

            cellHeaderBody.HorizontalAlignment = Element.ALIGN_CENTER;
            cellHeaderBody.Phrase = new Phrase("Order dari " + viewModel.BuyerName, normal_font);
            headerTable2.AddCell(cellHeaderBody);
            cellHeaderBody.Phrase = new Phrase("", normal_font);
            headerTable2.AddCell(cellHeaderBody);

            cellHeader2.AddElement(headerTable2);
            headerTable.AddCell(cellHeader2);

            cellHeaderCS2.Phrase = new Phrase("", normal_font);
            headerTable.AddCell(cellHeaderCS2);

            document.Add(headerTable);

            #endregion Header

            Dictionary<string, double> units = new Dictionary<string, double>();
            Dictionary<string, double> percentageUnits = new Dictionary<string, double>();

            int index = 1;
            double totalPackingQuantity = 0;
            double totalImperialQuantity = 0;
            double totalMetricQuantity = 0;

            #region Body

            PdfPTable bodyTable = new PdfPTable(6);
            PdfPCell bodyCell = new PdfPCell();

            float[] widthsBody = new float[] { 3f, 10f, 7f, 7f, 7f, 7f };
            bodyTable.SetWidths(widthsBody);
            bodyTable.WidthPercentage = 100;

            bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
            bodyCell.Phrase = new Phrase("No.", bold_font);
            bodyTable.AddCell(bodyCell);

            bodyCell.Phrase = new Phrase("Nama Barang", bold_font);
            bodyTable.AddCell(bodyCell);

            bodyCell.Phrase = new Phrase("Kode Barang", bold_font);
            bodyTable.AddCell(bodyCell);

            bodyCell.Phrase = new Phrase("Total Packing\n(" + viewModel.PackingUom + ")", bold_font);
            bodyTable.AddCell(bodyCell);

            bodyCell.Phrase = new Phrase("Total Panjang\n(" + viewModel.LengthUom + ")", bold_font);
            bodyTable.AddCell(bodyCell);

            bodyCell.Phrase = new Phrase("Total Panjang\n( yard )", bold_font);
            bodyTable.AddCell(bodyCell);

            foreach (DOSalesDetailViewModel item in viewModel.DOSalesDetails)
            {
                bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
                bodyCell.VerticalAlignment = Element.ALIGN_TOP;
                bodyCell.Phrase = new Phrase((index++).ToString(), normal_font);
                bodyTable.AddCell(bodyCell);

                bodyCell.HorizontalAlignment = Element.ALIGN_LEFT;
                bodyCell.Phrase = new Phrase(item.UnitName + " " + item.UnitRemark, normal_font);
                bodyTable.AddCell(bodyCell);

                bodyCell.HorizontalAlignment = Element.ALIGN_LEFT;
                bodyCell.Phrase = new Phrase(item.UnitCode, normal_font);
                bodyTable.AddCell(bodyCell);

                bodyCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                bodyCell.Phrase = new Phrase(string.Format("{0:n2}", item.TotalPacking), normal_font);
                bodyTable.AddCell(bodyCell);

                bodyCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                bodyCell.Phrase = new Phrase(string.Format("{0:n2}", item.TotalLength), normal_font);
                bodyTable.AddCell(bodyCell);


                bodyCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                bodyCell.Phrase = new Phrase(string.Format("{0:n2}", item.TotalLengthConversion), normal_font);
                bodyTable.AddCell(bodyCell);          
            }

            foreach (DOSalesDetailViewModel total in viewModel.DOSalesDetails)
            {
                totalPackingQuantity += total.TotalPacking;
                totalImperialQuantity += total.TotalLength;
                totalMetricQuantity += total.TotalLengthConversion;
            }


            bodyCell.Colspan = 2;
            bodyCell.Border = Rectangle.NO_BORDER;
            bodyCell.Phrase = new Phrase("", normal_font);
            bodyTable.AddCell(bodyCell);

            bodyCell.Colspan = 1;
            bodyCell.Border = Rectangle.BOX;
            bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
            bodyCell.Phrase = new Phrase("Total", bold_font);
            bodyTable.AddCell(bodyCell);

            bodyCell.Colspan = 1;
            bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
            bodyCell.Phrase = new Phrase(string.Format("{0:n2}", totalPackingQuantity), bold_font);
            bodyTable.AddCell(bodyCell);

            bodyCell.Colspan = 1;
            bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
            bodyCell.Phrase = new Phrase(string.Format("{0:n2}", totalImperialQuantity), bold_font);
            bodyTable.AddCell(bodyCell);

            bodyCell.Colspan = 1;
            bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
            bodyCell.Phrase = new Phrase(string.Format("{0:n2}", totalMetricQuantity), bold_font);
            bodyTable.AddCell(bodyCell);

            document.Add(bodyTable);

            #endregion Body

            #region Footer

            PdfPTable footerTable = new PdfPTable(1);
            PdfPCell cellFooterLeft = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };

            float[] widthsFooter = new float[] { 10f };
            footerTable.SetWidths(widthsFooter);
            footerTable.WidthPercentage = 100;

            cellFooterLeft.Phrase = new Phrase("", normal_font);
            footerTable.AddCell(cellFooterLeft);
            cellFooterLeft.Phrase = new Phrase("", normal_font);
            footerTable.AddCell(cellFooterLeft);

            cellFooterLeft.Phrase = new Phrase("Disp : " + viewModel.Disp + "       Op : " + viewModel.Op + "       Sc : " + viewModel.Sc, normal_font);
            footerTable.AddCell(cellFooterLeft);

            cellFooterLeft.Colspan = 3;
            cellFooterLeft.Phrase = new Phrase("", bold_font);
            footerTable.AddCell(cellFooterLeft);

            cellFooterLeft.Colspan = 3;
            cellFooterLeft.Phrase = new Phrase("Dikirim Kepada : " + viewModel.DestinationBuyerName, bold_font);
            footerTable.AddCell(cellFooterLeft);

            cellFooterLeft.Colspan = 3;
            cellFooterLeft.Phrase = new Phrase("Keterangan : " + viewModel.Remark, bold_font);
            footerTable.AddCell(cellFooterLeft);

            cellFooterLeft.Colspan = 3;
            cellFooterLeft.Phrase = new Phrase("", bold_font);
            footerTable.AddCell(cellFooterLeft);

            PdfPTable signatureTable = new PdfPTable(3) { HorizontalAlignment = Element.ALIGN_CENTER };
            PdfPCell signatureCell = new PdfPCell() { HorizontalAlignment = Element.ALIGN_CENTER };

            float[] widthsSignanture = new float[] { 10f, 10f, 10f };
            signatureTable.SetWidths(widthsSignanture);
            signatureTable.WidthPercentage = 100;

            signatureCell.Phrase = new Phrase("Adm.Penjualan", normal_font);
            signatureTable.AddCell(signatureCell);
            signatureCell.Phrase = new Phrase("Gudang", normal_font);
            signatureTable.AddCell(signatureCell);
            signatureCell.Phrase = new Phrase("Terima kasih :\nBagian Penjualan", normal_font);
            signatureTable.AddCell(signatureCell);

            signatureTable.AddCell(new PdfPCell()
            {
                Phrase = new Phrase("--------------------------------", normal_font),
                FixedHeight = 40,
                VerticalAlignment = Element.ALIGN_BOTTOM,
                HorizontalAlignment = Element.ALIGN_CENTER
            }); signatureTable.AddCell(new PdfPCell()
            {
                Phrase = new Phrase("--------------------------------", normal_font),
                FixedHeight = 40,
                VerticalAlignment = Element.ALIGN_BOTTOM,
                HorizontalAlignment = Element.ALIGN_CENTER
            }); signatureTable.AddCell(new PdfPCell()
            {
                Phrase = new Phrase("--------------------------------", normal_font),
                FixedHeight = 40,
                VerticalAlignment = Element.ALIGN_BOTTOM,
                HorizontalAlignment = Element.ALIGN_CENTER
            });

            footerTable.AddCell(new PdfPCell(signatureTable));

            cellFooterLeft.Phrase = new Phrase("", normal_font);
            footerTable.AddCell(cellFooterLeft);
            document.Add(footerTable);

            #endregion Footer

            document.Close();
            byte[] byteInfo = stream.ToArray();
            stream.Write(byteInfo, 0, byteInfo.Length);
            stream.Position = 0;

            return stream;
        }
    }
}
