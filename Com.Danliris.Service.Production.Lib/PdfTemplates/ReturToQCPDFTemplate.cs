using Com.Danliris.Service.Finishing.Printing.Lib.Models.ReturToQC;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Com.Danliris.Service.Finishing.Printing.Lib.PdfTemplates
{
    public class ReturToQCPdfTemplate
    {
        #region Static Data
        private const string TITLE = "BON PENGANTAR";
        private const string ISO = "FM.FP-GJ-15-003";
        private const string HEADER_LEFT_1 = "Kepada Yth. Bagian";
        private const string HEADER_RIGHT_1 = "NO";
        private const string HEADER_RIGHT_2 = "DO";
        private const string KODE_BARANG = "KODE";
        private const string SPLITTER = ":";
        private const string RECEIVER = "Penerima";
        private const string RECEIVED_BY = "Dari Bagian: Gudang F.P";
        private const string NAME_PLACEHOLDER = "( ............................. )";
        private const string LOCATION = "Surakarata";
        private const string GIVEN_BY = "Diberikan oleh:";
        private const int MARGIN = 20;
        private const double YARD_DIVIDER = 0.9144;
        #endregion

        #region Font
        private static readonly Font title_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10);
        private static readonly Font kode_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);
        private static readonly Font header_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);
        private static readonly Font bold_font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 7);
        private static readonly Font normal_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);
        #endregion

        #region Table
        private readonly PdfPTable Title;
        private readonly PdfPTable Header;
        private readonly PdfPTable Body;
        private readonly PdfPTable BodyFooter;
        private readonly PdfPTable Footer;
        #endregion

        public ReturToQCPdfTemplate(ReturToQCModel model, int timeoffset)
        {
            #region Header
            List<string> headerLefts1 = new List<string> { HEADER_LEFT_1, "" };
            List<string> headerLefts2 = new List<string> { string.Format("{0} {1}", SPLITTER, model.Destination), string.Format("{0} {1}", " ", model.Remark) };
            List<string> headerRights1 = new List<string> { HEADER_RIGHT_1, HEADER_RIGHT_2 };
            List<string> headerRights2 = new List<string> { string.Format("{0} {1}", SPLITTER, model.ReturNo), string.Format("{0} {1}", SPLITTER, model.DeliveryOrderNo) };
            #endregion

            #region Body
            List<string> bodyColumn = new List<string> { "MACAM BARANG", "DESIGN",
                "KET", "S.P", "C.W", "JML", "SAT", "YARD", "METER", "KG" };

            var returToQCItemDetail = model.ReturToQCItems.SelectMany(x => x.ReturToQCItemDetails);
            List<List<string>> bodyData = new List<List<string>>
            {
                returToQCItemDetail.Select(x => x.ProductName).ToList(),
                returToQCItemDetail.Select(x => string.IsNullOrWhiteSpace(x.DesignCode) && x.DesignNumber == 0
                    ? "-" : string.Format("{0} - {1}", x.DesignCode, x.DesignNumber)).ToList(),
                returToQCItemDetail.Select(x => x.Remark).ToList(),
                returToQCItemDetail.Select(x => x.ReturToQCItem.ProductionOrderNo).ToList(),
                returToQCItemDetail.Select(x => x.ColorWay).ToList(),
                returToQCItemDetail.Select(x => x.ReturQuantity.ToString("N2", CultureInfo.InvariantCulture)).ToList(),
                returToQCItemDetail.Select(x => x.UOMUnit).ToList(),
                returToQCItemDetail.Select(x => (x.Length / YARD_DIVIDER).ToString("N2", CultureInfo.InvariantCulture)).ToList(),
                returToQCItemDetail.Select(x => x.Length.ToString("N2", CultureInfo.InvariantCulture)).ToList(),
                returToQCItemDetail.Select(x => x.Weight.ToString("N2", CultureInfo.InvariantCulture)).ToList()
            };
            List<string> totalData = new List<string>
            {
                returToQCItemDetail.Select(x => x.ReturQuantity).Sum().ToString("N2", CultureInfo.InvariantCulture),
                "",
                returToQCItemDetail.Select(x => (x.Length / YARD_DIVIDER)).Sum().ToString("N2", CultureInfo.InvariantCulture),
                returToQCItemDetail.Select(x => x.Length).Sum().ToString("N2", CultureInfo.InvariantCulture),
                returToQCItemDetail.Select(x => x.Weight).Sum().ToString("N2", CultureInfo.InvariantCulture)
            };
            #endregion

            #region Body Footer
            List<string> bodyFooter = new List<string> { string.Format("[{0} {1} {2}]", KODE_BARANG, SPLITTER, model.FinishedGoodCode) };
            #endregion


            Title = GetTitle();
            this.Header = this.GetHeader(headerLefts1, headerLefts2, headerRights1, headerRights2);
            this.Body = this.GetBody(bodyColumn, bodyData, totalData);
            this.BodyFooter = this.GetBodyFooter(bodyFooter);
            this.Footer = this.GetFooter(model.Date.AddHours(timeoffset), model.CreatedBy);
        }

        private PdfPTable GetTitle()
        {
            PdfPTable title = new PdfPTable(1)
            {
                WidthPercentage = 100
            };
            PdfPCell cellTitle = new PdfPCell()
            {
                Border = Rectangle.NO_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                PaddingBottom = 10f
            };
            cellTitle.Phrase = new Phrase(TITLE, title_font);
            title.AddCell(cellTitle);

            return title;
        }

        private PdfPTable GetHeader(List<string> headerLefts1, List<string> headerLefts2, List<string> headerRights1, List<string> headerRights2)
        {
            PdfPTable header = new PdfPTable(2);
            header.SetWidths(new float[] { 6f, 4f });
            header.WidthPercentage = 100;
            PdfPCell cellHeader = new PdfPCell() { Border = Rectangle.NO_BORDER };

            PdfPCell subCellHeader = new PdfPCell() { Border = Rectangle.NO_BORDER };

            PdfPTable headerTable1 = new PdfPTable(2);
            headerTable1.SetWidths(new float[] { 30f, 40f });
            headerTable1.WidthPercentage = 100;

            subCellHeader.HorizontalAlignment = Element.ALIGN_LEFT;
            for (int i = 0; i < headerLefts1.Count; i++)
            {
                subCellHeader.Phrase = new Phrase(headerLefts1[i], header_font);
                headerTable1.AddCell(subCellHeader);

                subCellHeader.Phrase = new Phrase(headerLefts2[i], header_font);
                headerTable1.AddCell(subCellHeader);
            }
            //foreach (var subHeaderLeft in headerLefts1)
            //{
            //    subCellHeader.Phrase = new Phrase(subHeaderLeft, header_font);
            //    headerTable1.AddCell(subCellHeader);
            //}
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

        private PdfPTable GetBody(List<string> bodyColumn, List<List<string>> bodyData, List<string> totalData)
        {
            PdfPTable bodyTable = new PdfPTable((bodyColumn.Count + 1));
            bodyTable.HorizontalAlignment = Element.ALIGN_CENTER;
            bodyTable.SetWidths(new float[] { 7f, 20f, 13f, 13f, 13f, 13f, 13f, 13f, 13f, 13f, 13f });
            bodyTable.WidthPercentage = 100;
            PdfPCell bodyCell = new PdfPCell() { Border = Rectangle.BOX, HorizontalAlignment = Element.ALIGN_CENTER };
            PdfPCell bodyTotal = new PdfPCell() { Border = Rectangle.BOX, HorizontalAlignment = Element.ALIGN_RIGHT, Colspan = 6 };
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
            
            bodyTotal.Phrase = new Phrase("Total", normal_font);
            bodyTable.AddCell(bodyTotal);
            foreach (var total in totalData)
            {
                bodyCell.Phrase = new Phrase(total, normal_font);
                bodyTable.AddCell(bodyCell);
            }
            bodyTable.AddCell(emptyCell);

            return bodyTable;
        }

        private PdfPTable GetBodyFooter(List<string> bodyFooters)
        {
            PdfPTable bodyTable = new PdfPTable(1);
            bodyTable.SetWidths(new float[] { 20f });
            bodyTable.WidthPercentage = 100;
            PdfPCell bodyCell = new PdfPCell() { Border = Rectangle.NO_BORDER };

            for (int i = 0; i < bodyFooters.Count; i++)
            {
                bodyCell.Phrase = new Phrase(bodyFooters[i], kode_font);
                bodyTable.AddCell(bodyCell);
            }

            return bodyTable;
        }

        private PdfPTable GetFooter(DateTimeOffset date, string name)
        {
            PdfPTable footerTable = new PdfPTable(2);
            footerTable.SetWidths(new float[] { 1f, 1f});
            footerTable.WidthPercentage = 100;
            PdfPCell footerCell = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };
            PdfPCell subFooterCell = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };
            PdfPCell emptyCell = new PdfPCell() { Border = Rectangle.NO_BORDER, FixedHeight = 15f };
            PdfPCell emptyCellReceiver = new PdfPCell() { Border = Rectangle.NO_BORDER, FixedHeight = 10f };
            emptyCell.Phrase = new Phrase(string.Empty, normal_font);

            PdfPTable subFooterTable = new PdfPTable(1);
            subFooterTable.WidthPercentage = 100;
            subFooterTable.HorizontalAlignment = Element.ALIGN_CENTER;

            subFooterTable.AddCell(emptyCellReceiver);
            subFooterCell.Phrase = new Phrase(RECEIVER, normal_font);
            subFooterTable.AddCell(subFooterCell);
            subFooterTable.AddCell(emptyCell);
            subFooterTable.AddCell(emptyCell);
            subFooterCell.Phrase = new Phrase(NAME_PLACEHOLDER, normal_font);
            subFooterTable.AddCell(subFooterCell);
            footerCell.AddElement(subFooterTable);

            footerTable.AddCell(footerCell);

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
            subFooterCell.Phrase = new Phrase(NAME_PLACEHOLDER, normal_font);
            subFooterTable.AddCell(subFooterCell);
            footerCell.AddElement(subFooterTable);

            footerTable.AddCell(footerCell);

            return footerTable;
        }

        public MemoryStream GeneratePdfTemplate()
        {

            Document document = new Document(PageSize.A6.Rotate(), MARGIN, MARGIN, MARGIN, MARGIN);
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
