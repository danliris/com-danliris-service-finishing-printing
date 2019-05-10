using Com.Danliris.Service.Finishing.Printing.Lib.Models.Packing;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Kanban;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Com.Danliris.Service.Finishing.Printing.Lib.PdfTemplates
{
    public class PackingPdfTemplate
    {
        #region Static Data
        private const string TITLE = "BON PENYERAHAN PRODUKSI";
        private const string ISO = "FM.FP-GJ-15-003";
        private const string HEADER_LEFT_1 = "Kepada Yth. Bagian Penjualan";
        private const string HEADER_LEFT_2 = "Bersama ini kami kirimkan hasil produksi: Inspeksi";
        private const string HEADER_RIGHT_1 = "No";
        private const string HEADER_RIGHT_2 = "Sesuai No Order";
        private const string SOLID = "Solid";
        private const string PRINTING = "Printing";
        private const string FINISHING = "Finishing";
        private const string BUYER = "Buyer";
        private const string ORDER_TYPE = "Jenis Order";
        private const string CONSTRUCTION = "Konstruksi";
        private const string DESTINATION = "Tujuan";
        private const string COLOUR_TYPE = "Jenis Warna";
        private const string DESIGN_PATTERN = "Design/Motif";
        private const string SPLITTER = ":";
        private const string RECEIVED_BY = "Diterima oleh:";
        private const string NAME_PLACEHOLDER = "(                               )";
        private const string LOCATION = "Sukoharjo";
        private const string GIVEN_BY = "Diberikan oleh:";
        private const int MARGIN = 20;
        #endregion

        #region Font
        private static Font title_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 15);
        private static Font iso_font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10);
        private static Font header_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10);
        private static Font bold_font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 9);
        private static Font normal_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 9);
        #endregion

        #region Table
        private readonly PdfPTable Title;
        private readonly PdfPTable Header;
        private readonly PdfPTable Body;
        private readonly PdfPTable BodyFooter;
        private readonly PdfPTable Footer;
        #endregion

        public PackingPdfTemplate(PackingModel model, int timeoffset)
        {
            #region Data Preparation
            var OrderType = ((model.OrderTypeName ?? string.Empty) == PRINTING) ? PRINTING : FINISHING;
            #region Header
            IEnumerable<string> headerLefts = new List<string> { HEADER_LEFT_1, string.Format("{0} {1}", HEADER_LEFT_2, OrderType) };
            List<string> headerRights1 = new List<string> { HEADER_RIGHT_1, HEADER_RIGHT_2 };
            List<string> headerRights2 = new List<string> { string.Format("{0} {1}", SPLITTER, model.Code), string.Format("{0} {1}", SPLITTER, model.ProductionOrderNo) };
            #endregion

            #region Body
            IEnumerable<string> bodyColumn = new List<string> { "Barang", string.Format("Jumlah ({0})",model.PackingUom),
                "Panjang (Meter)", "Panjang Total (Meter)", "Berat Total (Kg)", "Keterangan" };
            List<List<string>> bodyData = new List<List<string>>
            { model.PackingDetails.Select(m => string.Format("{0} {1} {2} {3}", model.ColorName, m.Lot, m.Grade, (m.Grade.ToLower() == "a" ? "BQ" : "BS"))).ToList(),
              model.PackingDetails.Select(m => m.Quantity.ToString("#,#", CultureInfo.InvariantCulture)).ToList(),
              model.PackingDetails.Select(m => m.Length.ToString("#,#", CultureInfo.InvariantCulture)).ToList(),
              model.PackingDetails.Select(m => (m.Quantity * m.Length).ToString("#,#", CultureInfo.InvariantCulture)).ToList(),
              model.PackingDetails.Select(m => (m.Quantity * m.Weight).ToString("#,#", CultureInfo.InvariantCulture)).ToList(),
              model.PackingDetails.Select(m => m.Remark).ToList() };
            IEnumerable<string> totalData = new List<string>
            { model.PackingDetails.Select(m => m.Quantity).Sum().ToString("#,#", CultureInfo.InvariantCulture),
              model.PackingDetails.Select(m => m.Length).Sum().ToString("#,#", CultureInfo.InvariantCulture),
              model.PackingDetails.Select(m => (m.Quantity * m.Length)).Sum().ToString("#,#", CultureInfo.InvariantCulture),
              model.PackingDetails.Select(m => (m.Quantity * m.Weight)).Sum().ToString("#,#", CultureInfo.InvariantCulture) };
            #endregion

            #region Body Footer
            List<string> footerHeaders = new List<string>();
            List<string> footerValues = new List<string>();
            if (model.OrderTypeName.ToLowerInvariant() == SOLID.ToLower())
            {
                footerHeaders = new List<string> { BUYER, ORDER_TYPE, COLOUR_TYPE, CONSTRUCTION, DESTINATION };
                footerValues = new List<string> { model.BuyerName, OrderType, model.ColorType, model.Construction, model.BuyerAddress };
            }
            else if (model.OrderTypeName.ToLowerInvariant() == PRINTING.ToLower())
            {
                footerHeaders = new List<string> { BUYER, ORDER_TYPE, CONSTRUCTION, DESIGN_PATTERN, DESTINATION };
                footerValues = new List<string> { model.BuyerName, OrderType, model.Construction,
                    string.IsNullOrWhiteSpace(model.DesignNumber) && string.IsNullOrWhiteSpace(model.DesignCode) ?
                    string.Format("{0} - {1}",model.DesignNumber, model.DesignCode) : string.Empty, model.BuyerAddress };
            }
            else
            {
                footerHeaders = new List<string> { BUYER, ORDER_TYPE, CONSTRUCTION, DESTINATION };
                footerValues = new List<string> { model.BuyerName, OrderType, model.Construction, model.BuyerAddress };
            }
            #endregion
            #endregion
            
            this.Title = this.GetTitle();
            this.Header = this.GetHeader(headerLefts, headerRights1, headerRights2);
            this.Body = this.GetBody(bodyColumn, bodyData, totalData);
            this.BodyFooter = this.GetBodyFooter(footerHeaders, footerValues);
            this.Footer = this.GetFooter(model.Date.AddHours(timeoffset), model.CreatedBy);
        }

        private PdfPTable GetTitle()
        {
            PdfPTable title = new PdfPTable(1);
            title.WidthPercentage = 100;
            PdfPCell cellTitle = new PdfPCell() { Border = Rectangle.BOTTOM_BORDER, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE, PaddingBottom = 10f };
            cellTitle.Phrase = new Phrase(TITLE, title_font);
            title.AddCell(cellTitle);

            cellTitle = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE };
            cellTitle.Phrase = new Phrase(ISO, iso_font);
            title.AddCell(cellTitle);

            return title;
        }

        private PdfPTable GetHeader(IEnumerable<string> headerLefts, List<string> headerRights1, List<string> headerRights2)
        {
            PdfPTable header = new PdfPTable(2);
            header.SetWidths(new float[] { 6f, 4f });
            header.WidthPercentage = 100;
            PdfPCell cellHeader = new PdfPCell() { Border = Rectangle.NO_BORDER };

            PdfPCell subCellHeader = new PdfPCell() { Border = Rectangle.NO_BORDER };

            PdfPTable headerTable1 = new PdfPTable(1);
            headerTable1.WidthPercentage = 100;

            subCellHeader.HorizontalAlignment = Element.ALIGN_LEFT;
            foreach (var subHeaderLeft in headerLefts)
            {
                subCellHeader.Phrase = new Phrase(subHeaderLeft, header_font);
                headerTable1.AddCell(subCellHeader);
            }
            cellHeader.AddElement(headerTable1);
            header.AddCell(cellHeader);

            PdfPTable headerTable2 = new PdfPTable(2);
            headerTable2.SetWidths(new float[] { 30f, 40f });
            headerTable2.WidthPercentage = 100;
            
            for (int i = 0; i < headerRights1.Count; i++)
            {
                subCellHeader.Phrase = new Phrase(headerRights1[i], header_font);
                headerTable2.AddCell(subCellHeader);

                subCellHeader.Phrase = new Phrase(headerRights2[i], header_font);
                headerTable2.AddCell(subCellHeader);
            }
            cellHeader = new PdfPCell() { Border = Rectangle.NO_BORDER };
            cellHeader.AddElement(headerTable2);
            header.AddCell(cellHeader);

            return header;
        }

        private PdfPTable GetBody(IEnumerable<string> bodyColumn, List<List<string>> bodyData, IEnumerable<string> totalData)
        {
            PdfPTable bodyTable = new PdfPTable((bodyColumn.Count() + 1));
            bodyTable.HorizontalAlignment = Element.ALIGN_CENTER;
            bodyTable.SetWidths(new float[] { 5f, 35f, 10f, 10f, 10f, 10f, 20f });
            bodyTable.WidthPercentage = 100;
            PdfPCell bodyCell = new PdfPCell() { Border = Rectangle.BOX, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5f };
            PdfPCell emptyCell = new PdfPCell() { Border = Rectangle.BOX, FixedHeight = 15f };
            emptyCell.Phrase = new Phrase(string.Empty, normal_font);

            bodyCell.Phrase = new Phrase("No", bold_font);
            bodyTable.AddCell(bodyCell);
            foreach (var column in bodyColumn)
            {
                bodyCell.Phrase = new Phrase(column, bold_font);
                bodyTable.AddCell(bodyCell);
            }

            for (int rowNo = 0; rowNo < bodyData.FirstOrDefault().Count; rowNo++)
            {
                bodyCell.Phrase = new Phrase((rowNo + 1).ToString("#,#", CultureInfo.InvariantCulture), normal_font);
                bodyTable.AddCell(bodyCell);

                for (int colNo = 0; colNo < bodyData.Count; colNo++)
                {
                    bodyCell.Phrase = new Phrase(bodyData[colNo][rowNo], normal_font);
                    bodyTable.AddCell(bodyCell);
                }
            }
            
            bodyTable.AddCell(emptyCell);
            bodyCell.Phrase = new Phrase("Total", normal_font);
            bodyTable.AddCell(bodyCell);
            foreach (var total in totalData)
            {
                bodyCell.Phrase = new Phrase(total, normal_font);
                bodyTable.AddCell(bodyCell);
            }
            bodyTable.AddCell(emptyCell);

            return bodyTable;
        }

        private PdfPTable GetBodyFooter(List<string> footerHeaders, List<string> footerValues)
        {
            PdfPTable bodyTable = new PdfPTable(2);
            bodyTable.SetWidths(new float[] { 3f, 20f });
            bodyTable.WidthPercentage = 100;
            PdfPCell bodyCell = new PdfPCell() { Border = Rectangle.NO_BORDER };
            
            for (int i = 0; i < footerHeaders.Count; i++)
            {
                bodyCell.Phrase = new Phrase(footerHeaders[i], normal_font);
                bodyTable.AddCell(bodyCell);

                bodyCell.Phrase = new Phrase(string.Format("{0} {1}", SPLITTER, footerValues[i]), normal_font);
                bodyTable.AddCell(bodyCell);
            }

            return bodyTable;
        }

        private PdfPTable GetFooter(DateTimeOffset date, string name)
        {
            PdfPTable footerTable = new PdfPTable(4);
            footerTable.SetWidths(new float[] { 1f, 1f, 1f, 1f });
            footerTable.WidthPercentage = 100;
            PdfPCell footerCell = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };
            PdfPCell subFooterCell = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };
            PdfPCell emptyCell = new PdfPCell() { Border = Rectangle.NO_BORDER, FixedHeight = 15f };
            emptyCell.Phrase = new Phrase(string.Empty, normal_font);

            PdfPTable subFooterTable = new PdfPTable(1);
            subFooterTable.WidthPercentage = 100;
            subFooterTable.HorizontalAlignment = Element.ALIGN_CENTER;

            subFooterTable.AddCell(emptyCell);
            subFooterCell.Phrase = new Phrase(RECEIVED_BY, normal_font);
            subFooterTable.AddCell(subFooterCell);
            subFooterTable.AddCell(emptyCell);
            subFooterTable.AddCell(emptyCell);
            subFooterCell.Phrase = new Phrase(NAME_PLACEHOLDER, normal_font);
            subFooterTable.AddCell(subFooterCell);
            footerCell.AddElement(subFooterTable);

            footerTable.AddCell(footerCell);
            footerTable.AddCell(emptyCell);
            footerTable.AddCell(emptyCell);

            footerCell = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };
            subFooterTable = new PdfPTable(1);
            subFooterTable.WidthPercentage = 100;
            subFooterTable.HorizontalAlignment = Element.ALIGN_CENTER;

            subFooterCell.Phrase = new Phrase(string.Format("{0}, {1}", LOCATION, date.ToString("dd MMMM yyyy")), normal_font);
            subFooterTable.AddCell(subFooterCell);
            subFooterCell.Phrase = new Phrase(RECEIVED_BY, normal_font);
            subFooterTable.AddCell(subFooterCell);
            subFooterTable.AddCell(emptyCell);
            subFooterTable.AddCell(emptyCell);
            subFooterCell.Phrase = new Phrase(string.Format("({0})", name), normal_font);
            subFooterTable.AddCell(subFooterCell);
            footerCell.AddElement(subFooterTable);

            footerTable.AddCell(footerCell);

            return footerTable;
        }

        public MemoryStream GeneratePdfTemplate()
        {

            Document document = new Document(PageSize.A5.Rotate(), MARGIN, MARGIN, MARGIN, MARGIN);
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);
            document.Open();
            
            document.Add(Title);
            document.Add(Header);
            document.Add(Body);
            document.Add(BodyFooter);
            document.Add(Footer);

            document.Close();

            byte[] byteInfo = stream.ToArray();
            stream.Write(byteInfo, 0, byteInfo.Length);
            stream.Position = 0;

            return stream;
        }
    }
}
