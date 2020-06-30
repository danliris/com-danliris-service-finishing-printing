using Com.Danliris.Service.Finishing.Printing.Lib.Models.DyestuffChemicalUsageReceipt;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.PdfTemplates
{
    public class DyestuffChemicalUsageReceiptPdfTemplate
    {
        public string DOCUMENT_TITLE = "RESEP PEMAKAIAN DYESTUFF & CHEMICAL UNTUK PASTA";
        public string ISO = "FM.FP-02-PR-06-09.1-009/R1";
        public Font HEADER_FONT_BOLD = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 14);
        public Font HEADER_FONT = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10);
        public Font SUBHEADER_FONT_BOLD_UNDERLINED = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10, Font.UNDERLINE);
        public Font TEXT_FONT_BOLD = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);
        public Font TEXT_FONT = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);

        private readonly PdfPTable Title;
        private readonly PdfPTable DOCUMENTISO;
        private readonly PdfPTable DocumentInfo;
        private readonly List<PdfPTable> DocumentItems;

        List<string> bodyTableColumns = new List<string> { "MACAM BARANG", "DESIGN", "S.P", "C.W", "SATUAN", "KUANTITI", "PANJANG TOTAL (m)", "BERAT TOTAL (kg)" };

        public DyestuffChemicalUsageReceiptPdfTemplate(DyestuffChemicalUsageReceiptModel model, int timeoffset)
        {
            DOCUMENTISO = GetISO();
            Title = GetTitle();
            DocumentInfo = GetDocumentInfo(model, timeoffset);
            DocumentItems = GetDocumentItems(model, timeoffset);
        }

        public MemoryStream GeneratePdfTemplate()
        {
            const int MARGIN = 25;

            Document document = new Document(PageSize.Flsa, MARGIN, MARGIN, MARGIN, MARGIN);
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);

            document.Open();

            #region Header
            document.Add(DOCUMENTISO);
            document.Add(new Paragraph(" "));
            document.Add(Title);
            document.Add(new Paragraph(" "));
            document.Add(DocumentInfo);
            document.Add(new Paragraph(" "));
            int index = 1;
            foreach (var item in DocumentItems)
            {
                document.Add(item);
                if (index++ != DocumentItems.Count)
                {
                    document.Add(new Paragraph(" "));
                }
            }
            document.Add(new Paragraph("Dibuat Oleh : ..................................... ", HEADER_FONT));
            #endregion Header

            document.Close();
            byte[] byteInfo = stream.ToArray();
            stream.Write(byteInfo, 0, byteInfo.Length);
            stream.Position = 0;

            return stream;
        }

        private PdfPTable GetISO()
        {
            PdfPTable table = new PdfPTable(1)
            {
                WidthPercentage = 100
            };
            PdfPCell cell = new PdfPCell()
            {
                Border = Rectangle.NO_BORDER,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                PaddingBottom = 1f
            };
            cell.Phrase = new Phrase(ISO, TEXT_FONT_BOLD);
            table.AddCell(cell);

            return table;
        }

        private PdfPTable GetTitle()
        {
            PdfPTable table = new PdfPTable(1)
            {
                WidthPercentage = 100
            };
            PdfPCell cell = new PdfPCell()
            {
                Border = Rectangle.NO_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                PaddingBottom = 5f
            };
            cell.Phrase = new Phrase(DOCUMENT_TITLE, HEADER_FONT_BOLD);
            table.AddCell(cell);

            return table;
        }

        private PdfPTable GetDocumentInfo(DyestuffChemicalUsageReceiptModel model, int offset)
        {
            PdfPTable table = new PdfPTable(5)
            {
                WidthPercentage = 100
            };
            float[] widths = new float[] { 1f, 3f, 1f, 1f, 3f };
            table.SetWidths(widths);
            PdfPCell cell = new PdfPCell()
            {
                Border = Rectangle.NO_BORDER,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_TOP,
                PaddingBottom = 2f
            };


            cell.Phrase = new Phrase("No SPP", HEADER_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase($": {model.ProductionOrderOrderNo}", HEADER_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase("", HEADER_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase("Motif", HEADER_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase($": {model.StrikeOffCode}", HEADER_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase("Jumlah Order", HEADER_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase($": {model.ProductionOrderOrderQuantity.ToString("N2", CultureInfo.InvariantCulture)}", HEADER_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase("", HEADER_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase("Proses", HEADER_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase($": {model.StrikeOffType}", HEADER_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase("Material", HEADER_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase($": {model.ProductionOrderMaterialName}/{model.ProductionOrderMaterialConstructionName}/{model.ProductionOrderMaterialWidth}", HEADER_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase("", HEADER_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase("Tanggal", HEADER_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase($": {model.Date.AddHours(offset).ToString("dd MMMM yyyy", new CultureInfo("id-ID"))}", HEADER_FONT);
            table.AddCell(cell);


            return table;
        }

        private List<PdfPTable> GetDocumentItems(DyestuffChemicalUsageReceiptModel model, int offset)
        {
            List<PdfPTable> items = new List<PdfPTable>();
            List<string> Adjss = new List<string>() { "Adjs 1", "Adjs 2", "Adjs 3", "Adjs 4" };
            foreach (var item in model.DyestuffChemicalUsageReceiptItems)
            {
                PdfPTable table = new PdfPTable(7)
                {
                    WidthPercentage = 100
                };
                float[] widths = new float[] { 2f, 1f, 1f, 1f, 1f, 1f, 1f };
                table.SetWidths(widths);
                PdfPCell cellCenter = new PdfPCell()
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                };

                PdfPCell cellColor = new PdfPCell()
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    Colspan = 7
                };

                PdfPCell cellSubHeader = new PdfPCell()
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    Rowspan = 2
                };

                PdfPCell cellDate = new PdfPCell()
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    MinimumHeight = 15f,
                };

                PdfPCell cellLeft = new PdfPCell()
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                };


                cellCenter.Phrase = new Phrase("Warna", TEXT_FONT_BOLD);
                table.AddCell(cellCenter);

                cellColor.Phrase = new Phrase(item.ColorCode, TEXT_FONT_BOLD);
                table.AddCell(cellColor);

                cellSubHeader.Phrase = new Phrase("Dyestuff & Chemical", TEXT_FONT_BOLD);
                table.AddCell(cellSubHeader);

                cellCenter.Phrase = new Phrase("Resep", TEXT_FONT_BOLD);
                table.AddCell(cellCenter);

                foreach (var adjsText in Adjss)
                {
                    cellCenter.Phrase = new Phrase(adjsText, TEXT_FONT_BOLD);
                    table.AddCell(cellCenter);
                }

                cellSubHeader.Phrase = new Phrase("Total", TEXT_FONT_BOLD);
                table.AddCell(cellSubHeader);

                cellDate.Phrase = new Phrase(item.ReceiptDate.HasValue ? item.ReceiptDate.Value.AddHours(offset).ToString("dd-MMM-yyyy", new CultureInfo("id-ID")) : "", TEXT_FONT_BOLD);
                table.AddCell(cellDate);

                cellDate.Phrase = new Phrase(item.Adjs1Date.HasValue ? item.Adjs1Date.Value.AddHours(offset).ToString("dd-MMM-yyyy", new CultureInfo("id-ID")) : "", TEXT_FONT_BOLD);
                table.AddCell(cellDate);

                cellDate.Phrase = new Phrase(item.Adjs2Date.HasValue ? item.Adjs2Date.Value.AddHours(offset).ToString("dd-MMM-yyyy", new CultureInfo("id-ID")) : "", TEXT_FONT_BOLD);
                table.AddCell(cellDate);

                cellDate.Phrase = new Phrase(item.Adjs3Date.HasValue ? item.Adjs3Date.Value.AddHours(offset).ToString("dd-MMM-yyyy", new CultureInfo("id-ID")) : "", TEXT_FONT_BOLD);
                table.AddCell(cellDate);

                cellDate.Phrase = new Phrase(item.Adjs4Date.HasValue ? item.Adjs4Date.Value.AddHours(offset).ToString("dd-MMM-yyyy", new CultureInfo("id-ID")) : "", TEXT_FONT_BOLD);
                table.AddCell(cellDate);

                foreach (var detail in item.DyestuffChemicalUsageReceiptItemDetails)
                {
                    cellLeft.Phrase = new Phrase(detail.Name, TEXT_FONT);
                    table.AddCell(cellLeft);

                    cellCenter.Phrase = new Phrase(detail.ReceiptQuantity.ToString("N2", CultureInfo.InvariantCulture), TEXT_FONT);
                    table.AddCell(cellCenter);

                    cellCenter.Phrase = new Phrase(detail.Adjs1Quantity == 0 ? "" : detail.Adjs1Quantity.ToString("N2", CultureInfo.InvariantCulture), TEXT_FONT);
                    table.AddCell(cellCenter);

                    cellCenter.Phrase = new Phrase(detail.Adjs2Quantity == 0 ? "" : detail.Adjs2Quantity.ToString("N2", CultureInfo.InvariantCulture), TEXT_FONT);
                    table.AddCell(cellCenter);

                    cellCenter.Phrase = new Phrase(detail.Adjs3Quantity == 0 ? "" : detail.Adjs3Quantity.ToString("N2", CultureInfo.InvariantCulture), TEXT_FONT);
                    table.AddCell(cellCenter);

                    cellCenter.Phrase = new Phrase(detail.Adjs4Quantity == 0 ? "" : detail.Adjs4Quantity.ToString("N2", CultureInfo.InvariantCulture), TEXT_FONT);
                    table.AddCell(cellCenter);

                    var total = detail.ReceiptQuantity + detail.Adjs1Quantity + detail.Adjs2Quantity + detail.Adjs3Quantity + detail.Adjs4Quantity;

                    if (detail.Name.ToLower() != "viscositas")
                    {

                        cellCenter.Phrase = new Phrase(total.ToString("N2", CultureInfo.InvariantCulture), TEXT_FONT);
                        table.AddCell(cellCenter);
                    }
                    else
                    {
                        cellCenter.Phrase = new Phrase("", TEXT_FONT);
                        table.AddCell(cellCenter);
                    }
                }

                cellLeft.Phrase = new Phrase("Pembuatan", TEXT_FONT);
                table.AddCell(cellLeft);

                cellLeft.Phrase = new Phrase("", TEXT_FONT);
                table.AddCell(cellLeft);

                cellLeft.Phrase = new Phrase("", TEXT_FONT);
                table.AddCell(cellLeft);

                cellLeft.Phrase = new Phrase("", TEXT_FONT);
                table.AddCell(cellLeft);

                cellLeft.Phrase = new Phrase("", TEXT_FONT);
                table.AddCell(cellLeft);

                cellLeft.Phrase = new Phrase("", TEXT_FONT);
                table.AddCell(cellLeft);

                cellLeft.Phrase = new Phrase("", TEXT_FONT);
                table.AddCell(cellLeft);

                items.Add(table);
            }

            return items;
        }
    }
}
