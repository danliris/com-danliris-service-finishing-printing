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
        public string DOCUMENT_TITLE_GROUND = "RESEP PEMAKAIAN DYESTUFF & CHEMICAL UNTUK GROUND";
        public string ISO = "FM.FP-02-PR-06-09.1-009/R1";
        public string ISOGROUND = "FM.DP-02-LB-09.1-001/R1";
        public Font HEADER_FONT_BOLD = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 14);
        public Font HEADER_FONT = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10);
        public Font SUBHEADER_FONT_BOLD_UNDERLINED = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10, Font.UNDERLINE);
        public Font TEXT_FONT_BOLD = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);
        public Font TEXT_FONT = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);

        private readonly PdfPTable Title;
        private readonly PdfPTable TitleGround;
        private readonly PdfPTable DOCUMENTISO;
        private readonly PdfPTable DOCUMENTISOGROUND;
        private readonly PdfPTable DocumentInfo;
        private readonly PdfPTable DocumentInfoGround;
        private readonly PdfPTable DocumentInfoGround2;
        private readonly List<PdfPTable> DocumentItems;
        private readonly PdfPTable DocumentItemsGround;
        private readonly PdfPTable DocumentItemsGround2;
        private readonly PdfPTable DocumentItemsGround3;
       // private readonly PdfPTable DocumentItemsSignature;

        private readonly string VISCOSITAS = "viscositas";

        List<string> bodyTableColumns = new List<string> { "MACAM BARANG", "DESIGN", "S.P", "C.W", "SATUAN", "KUANTITI", "PANJANG TOTAL (m)", "BERAT TOTAL (kg)" };

        public DyestuffChemicalUsageReceiptPdfTemplate(DyestuffChemicalUsageReceiptModel model, int timeoffset)
        {
            DOCUMENTISO = GetISO();
            DOCUMENTISOGROUND = GetISOGround();
            Title = GetTitle();
            TitleGround = GetTitleGround();
            DocumentInfo = GetDocumentInfo(model, timeoffset);
            DocumentInfoGround = GetDocumentInfoGround(model, timeoffset);
            DocumentInfoGround2 = GetDocumentInfoGround2(model, timeoffset);
            DocumentItems = GetDocumentItems(model, timeoffset);
            DocumentItemsGround = GetDocumentItemsGround(model, timeoffset);
            DocumentItemsGround2 = GetDocumentItemsGround2(model, timeoffset);
            DocumentItemsGround3 = GetDocumentItemsGround3(model, timeoffset);
          //  DocumentItemsSignature = GetDocumentItemsSignature(model, timeoffset);
        }

        public MemoryStream GeneratePdfTemplate()
        {
            const int MARGIN = 25;

            TwoColumnHeaderFooter pageEventHelper = new TwoColumnHeaderFooter(DOCUMENTISO, Title, DocumentInfo);
            var height = pageEventHelper.TableHeight;
            Document document = new Document(PageSize.Flsa, MARGIN, MARGIN, 50 + height, MARGIN);
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);
            writer.PageEvent = pageEventHelper;
            document.Open();

            #region Header
            //document.Add(DOCUMENTISO);
            //document.Add(new Paragraph(" "));
            //document.Add(Title);
            //document.Add(new Paragraph(" "));
            //document.Add(DocumentInfo);
            //document.Add(new Paragraph(" "));
            //for (int i = 1; i <= DocumentItems.Count; i++)
            //{
            //    int idx = 1;
            //    var docItem = DocumentItems[i - 1];
            //    document.Add(docItem);
            //    if (idx++ != DocumentItems.Count)
            //    {
            //        document.Add(new Paragraph(" "));
            //    }

            //    if (i % 4 == 0)
            //    {
            //        document.NewPage();
            //        document.Add(DOCUMENTISO);
            //        document.Add(new Paragraph(" "));
            //        document.Add(Title);
            //        document.Add(new Paragraph(" "));
            //        document.Add(DocumentInfo);
            //        document.Add(new Paragraph(" "));
            //    }
            //}


            //document.Add(DOCUMENTISO);
            //document.Add(new Paragraph(" "));
            //document.Add(Title);
            //document.Add(new Paragraph(" "));
            //document.Add(DocumentInfo);
            //document.Add(new Paragraph(" "));
            //int index = 1;
            foreach (var item in DocumentItems)
            {
                document.Add(item);
                //if (index++ != DocumentItems.Count)
                //{
                    
                //}
                document.Add(new Paragraph(" "));
            }
            document.Add(new Paragraph(" "));
            document.Add(new Paragraph("Dibuat Oleh : ..................................... ", HEADER_FONT));
            #endregion Header

            document.Close();
            byte[] byteInfo = stream.ToArray();
            stream.Write(byteInfo, 0, byteInfo.Length);
            stream.Position = 0;

            var ms2 = new MemoryStream();
            PdfReader reader = new PdfReader(stream);
            PdfStamper stamper = new PdfStamper(reader, ms2);
            int pages = reader.NumberOfPages;
            for (int i = 1; i <= pages; i++)
            {
                ColumnText.ShowTextAligned(stamper.GetUnderContent(i), Element.ALIGN_RIGHT, new Phrase($"Halaman {i} dari {pages}", TEXT_FONT), 580f, 15f, 0);
            }
            stamper.Close();
            byte[] byteInfo2 = ms2.ToArray();
            ms2.Write(byteInfo2, 0, byteInfo2.Length);
            ms2.Position = 0;
            return ms2;
        }

        public MemoryStream GeneratePdfTemplateGround()
        {
            const int MARGIN = 25;

            TwoColumnHeaderFooter pageEventHelper = new TwoColumnHeaderFooter(DOCUMENTISOGROUND, TitleGround, DocumentInfoGround);
            var height = pageEventHelper.TableHeight;
            Document document = new Document(PageSize.Flsa, MARGIN, MARGIN, 50 + height, MARGIN);
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);
            writer.PageEvent = pageEventHelper;
            document.Open();

            #region Content

            document.Add(DocumentItemsGround);

            document.Add(new Paragraph(" "));

            document.Add(DocumentInfoGround2);

            document.Add(new Paragraph(" "));

            document.Add(DocumentItemsGround2);

            document.Add(new Paragraph(" "));

            document.Add(DocumentItemsGround3);

            document.Add(new Paragraph(" "));

           // document.Add(DocumentItemsSignature);

            document.Add(new Paragraph(" "));

            //foreach (var item in DocumentItemsGround)
            //{
            //    document.Add(item);
            //    //if (index++ != DocumentItems.Count)
            //    //{

            //    //}
            //    document.Add(new Paragraph(" "));
            //}


            //document.Add(new Paragraph(" "));
            //document.Add(new Paragraph("Dibuat Oleh : ..................................... ", HEADER_FONT));
            #endregion Content

            document.Close();
            byte[] byteInfo = stream.ToArray();
            stream.Write(byteInfo, 0, byteInfo.Length);
            stream.Position = 0;

            var ms2 = new MemoryStream();
            PdfReader reader = new PdfReader(stream);
            PdfStamper stamper = new PdfStamper(reader, ms2);
            int pages = reader.NumberOfPages;
            for (int i = 1; i <= pages; i++)
            {
                ColumnText.ShowTextAligned(stamper.GetUnderContent(i), Element.ALIGN_RIGHT, new Phrase($"Halaman {i} dari {pages}", TEXT_FONT), 580f, 15f, 0);
            }
            stamper.Close();
            byte[] byteInfo2 = ms2.ToArray();
            ms2.Write(byteInfo2, 0, byteInfo2.Length);
            ms2.Position = 0;
            return ms2;
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

        private PdfPTable GetISOGround()
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
            cell.Phrase = new Phrase(ISOGROUND, TEXT_FONT_BOLD);
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

        private PdfPTable GetTitleGround()
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
            cell.Phrase = new Phrase(DOCUMENT_TITLE_GROUND, HEADER_FONT_BOLD);
            table.AddCell(cell);

            return table;
        }

        private PdfPTable GetDocumentInfo(DyestuffChemicalUsageReceiptModel model, int offset)
        {
            PdfPTable table = new PdfPTable(7)
            {
                WidthPercentage = 100
            };
            float[] widths = new float[] { 10f, 1f, 20f, 5f, 10f, 1f, 20f };
            table.SetWidths(widths);
            PdfPCell cell = new PdfPCell()
            {
                Border = Rectangle.NO_BORDER,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_TOP,
                PaddingBottom = 2f,
                PaddingLeft = 0f,
            };

            cell.Phrase = new Phrase("No SPP", HEADER_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase(":", HEADER_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase($"{model.ProductionOrderOrderNo}", HEADER_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase("", HEADER_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase("Motif", HEADER_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase(":", HEADER_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase($"{model.StrikeOffCode}", HEADER_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase("Jumlah Order", HEADER_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase(":", HEADER_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase($"{model.ProductionOrderOrderQuantity.ToString("N2", CultureInfo.InvariantCulture)}", HEADER_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase("", HEADER_FONT);
            table.AddCell(cell);

            if (string.IsNullOrEmpty(model.RepeatedProductionOrderNo))
            {


                cell.Phrase = new Phrase("Proses", HEADER_FONT);
                table.AddCell(cell);

                cell.Phrase = new Phrase(":", HEADER_FONT);
                table.AddCell(cell);

                cell.Phrase = new Phrase($"{model.StrikeOffType}", HEADER_FONT);
                table.AddCell(cell);

                cell.Phrase = new Phrase("Material", HEADER_FONT);
                table.AddCell(cell);

                cell.Phrase = new Phrase(": ", HEADER_FONT);
                table.AddCell(cell);

                cell.Phrase = new Phrase($"{model.ProductionOrderMaterialName}/{model.ProductionOrderMaterialConstructionName}/{model.ProductionOrderMaterialWidth}", HEADER_FONT);
                table.AddCell(cell);

                cell.Phrase = new Phrase("", HEADER_FONT);
                table.AddCell(cell);

                cell.Phrase = new Phrase("Tanggal", HEADER_FONT);
                table.AddCell(cell);

                cell.Phrase = new Phrase(":", HEADER_FONT);
                table.AddCell(cell);

                cell.Phrase = new Phrase($"{model.Date.AddHours(offset).ToString("dd MMMM yyyy", new CultureInfo("id-ID"))}", HEADER_FONT);
                table.AddCell(cell);

            }
            else
            {

                cell.Phrase = new Phrase("Repeat", HEADER_FONT);
                table.AddCell(cell);

                cell.Phrase = new Phrase(":", HEADER_FONT);
                table.AddCell(cell);

                cell.Phrase = new Phrase($"{model.RepeatedProductionOrderNo}", HEADER_FONT);
                table.AddCell(cell);

                cell.Phrase = new Phrase("Material", HEADER_FONT);
                table.AddCell(cell);

                cell.Phrase = new Phrase(":", HEADER_FONT);
                table.AddCell(cell);

                cell.Phrase = new Phrase($"{model.ProductionOrderMaterialName}/{model.ProductionOrderMaterialConstructionName}/{model.ProductionOrderMaterialWidth}", HEADER_FONT);
                table.AddCell(cell);

                cell.Phrase = new Phrase("", HEADER_FONT);
                table.AddCell(cell);

                cell.Phrase = new Phrase("Proses", HEADER_FONT);
                table.AddCell(cell);

                cell.Phrase = new Phrase(":", HEADER_FONT);
                table.AddCell(cell);

                cell.Phrase = new Phrase($"{model.StrikeOffType}", HEADER_FONT);
                table.AddCell(cell);

                cell.Phrase = new Phrase("", HEADER_FONT);
                table.AddCell(cell);

                cell.Phrase = new Phrase("", HEADER_FONT);
                table.AddCell(cell);

                cell.Phrase = new Phrase("", HEADER_FONT);
                table.AddCell(cell);

                cell.Phrase = new Phrase("", HEADER_FONT);
                table.AddCell(cell);


                cell.Phrase = new Phrase("Tanggal", HEADER_FONT);
                table.AddCell(cell);

                cell.Phrase = new Phrase(":", HEADER_FONT);
                table.AddCell(cell);
                cell.Phrase = new Phrase($"{model.Date.AddHours(offset).ToString("dd MMMM yyyy", new CultureInfo("id-ID"))}", HEADER_FONT);
                table.AddCell(cell);
            }




            return table;
        }

        private PdfPTable GetDocumentInfoGround(DyestuffChemicalUsageReceiptModel model, int offset)
        {
            PdfPTable table = new PdfPTable(7)
            {
                WidthPercentage = 100
            };
            float[] widths = new float[] { 10f, 1f, 20f, 5f, 10f, 1f, 20f };
            table.SetWidths(widths);
            PdfPCell cell = new PdfPCell()
            {
                Border = Rectangle.NO_BORDER,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_TOP,
                PaddingBottom = 2f,
                PaddingLeft = 0f,
            };

            cell.Phrase = new Phrase("No SPP", HEADER_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase(":", HEADER_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase($"{model.ProductionOrderOrderNo}", HEADER_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase("", HEADER_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase("Motif", HEADER_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase(":", HEADER_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase($"{model.StrikeOffCode}", HEADER_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase("Jumlah Order", HEADER_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase(":", HEADER_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase($"{model.ProductionOrderOrderQuantity.ToString("N2", CultureInfo.InvariantCulture)}", HEADER_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase("", HEADER_FONT);
            table.AddCell(cell);

            if (string.IsNullOrEmpty(model.RepeatedProductionOrderNo))
            {


                cell.Phrase = new Phrase("Proses", HEADER_FONT);
                table.AddCell(cell);

                cell.Phrase = new Phrase(":", HEADER_FONT);
                table.AddCell(cell);

                cell.Phrase = new Phrase($"{model.StrikeOffType}", HEADER_FONT);
                table.AddCell(cell);

                cell.Phrase = new Phrase("Material", HEADER_FONT);
                table.AddCell(cell);

                cell.Phrase = new Phrase(": ", HEADER_FONT);
                table.AddCell(cell);

                cell.Phrase = new Phrase($"{model.ProductionOrderMaterialName}/{model.ProductionOrderMaterialConstructionName}/{model.ProductionOrderMaterialWidth}", HEADER_FONT);
                table.AddCell(cell);

                cell.Phrase = new Phrase("", HEADER_FONT);
                table.AddCell(cell);

                cell.Phrase = new Phrase("Tanggal", HEADER_FONT);
                table.AddCell(cell);

                cell.Phrase = new Phrase(":", HEADER_FONT);
                table.AddCell(cell);

                cell.Phrase = new Phrase($"{model.Date.AddHours(offset).ToString("dd MMMM yyyy", new CultureInfo("id-ID"))}", HEADER_FONT);
                table.AddCell(cell);

                cell.Phrase = new Phrase("Mesin", HEADER_FONT);
                table.AddCell(cell);

                cell.Phrase = new Phrase(":", HEADER_FONT);
                table.AddCell(cell);

                cell.Phrase = new Phrase("", HEADER_FONT);
                table.AddCell(cell);

                cell.Phrase = new Phrase("", HEADER_FONT);
                table.AddCell(cell);


                cell.Phrase = new Phrase("", HEADER_FONT);
                table.AddCell(cell);

                cell.Phrase = new Phrase("", HEADER_FONT);
                table.AddCell(cell);
                cell.Phrase = new Phrase("",HEADER_FONT);
                table.AddCell(cell);
            }
            else
            {

                cell.Phrase = new Phrase("Repeat", HEADER_FONT);
                table.AddCell(cell);

                cell.Phrase = new Phrase(":", HEADER_FONT);
                table.AddCell(cell);

                cell.Phrase = new Phrase($"{model.RepeatedProductionOrderNo}", HEADER_FONT);
                table.AddCell(cell);

                cell.Phrase = new Phrase("Material", HEADER_FONT);
                table.AddCell(cell);

                cell.Phrase = new Phrase(":", HEADER_FONT);
                table.AddCell(cell);

                cell.Phrase = new Phrase($"{model.ProductionOrderMaterialName}/{model.ProductionOrderMaterialConstructionName}/{model.ProductionOrderMaterialWidth}", HEADER_FONT);
                table.AddCell(cell);

                cell.Phrase = new Phrase("", HEADER_FONT);
                table.AddCell(cell);

                cell.Phrase = new Phrase("Proses", HEADER_FONT);
                table.AddCell(cell);

                cell.Phrase = new Phrase(":", HEADER_FONT);
                table.AddCell(cell);

                cell.Phrase = new Phrase($"{model.StrikeOffType}", HEADER_FONT);
                table.AddCell(cell);

                cell.Phrase = new Phrase("Mesin", HEADER_FONT);
                table.AddCell(cell);

                cell.Phrase = new Phrase(":", HEADER_FONT);
                table.AddCell(cell);

                cell.Phrase = new Phrase(" ", HEADER_FONT);
                table.AddCell(cell);

                cell.Phrase = new Phrase("", HEADER_FONT);
                table.AddCell(cell);


                cell.Phrase = new Phrase("Tanggal", HEADER_FONT);
                table.AddCell(cell);

                cell.Phrase = new Phrase(":", HEADER_FONT);
                table.AddCell(cell);
                cell.Phrase = new Phrase($"{model.Date.AddHours(offset).ToString("dd MMMM yyyy", new CultureInfo("id-ID"))}", HEADER_FONT);
                table.AddCell(cell);
            }




            return table;
        }

        private PdfPTable GetDocumentInfoGround2(DyestuffChemicalUsageReceiptModel model, int offset)
        {
            PdfPTable table = new PdfPTable(7)
            {
                WidthPercentage = 100
            };
            float[] widths = new float[] { 10f, 1f, 20f, 5f, 10f, 1f, 20f };
            table.SetWidths(widths);
            PdfPCell cell = new PdfPCell()
            {
                Border = Rectangle.NO_BORDER,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_TOP,
                PaddingBottom = 2f,
                PaddingLeft = 0f,
            };

            cell.Phrase = new Phrase("Tanggal Produksi", HEADER_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase(":", HEADER_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase("", HEADER_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase("", HEADER_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase("Seri", HEADER_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase(":", HEADER_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase(" ", HEADER_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase("Shift", HEADER_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase(":", HEADER_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase(" ", HEADER_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase("", HEADER_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase("Shift", HEADER_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase(":", HEADER_FONT);
            table.AddCell(cell);

            cell.Phrase = new Phrase(" ", HEADER_FONT);
            table.AddCell(cell);






            return table;
        }
        //private PdfPTable GetDocumentInfo(DyestuffChemicalUsageReceiptModel model, int offset)
        //{
        //    PdfPTable table = new PdfPTable(5)
        //    {
        //        WidthPercentage = 100
        //    };
        //    float[] widths = new float[] { 2f, 3f, 1f, 2f, 3f };
        //    table.SetWidths(widths);
        //    PdfPCell cell = new PdfPCell()
        //    {
        //        Border = Rectangle.NO_BORDER,
        //        HorizontalAlignment = Element.ALIGN_LEFT,
        //        VerticalAlignment = Element.ALIGN_TOP,
        //        PaddingBottom = 2f
        //    };

        //    cell.Phrase = new Phrase("No SPP", HEADER_FONT);
        //    table.AddCell(cell);

        //    cell.Phrase = new Phrase($": {model.ProductionOrderOrderNo}", HEADER_FONT);
        //    table.AddCell(cell);

        //    cell.Phrase = new Phrase("", HEADER_FONT);
        //    table.AddCell(cell);

        //    cell.Phrase = new Phrase("Motif", HEADER_FONT);
        //    table.AddCell(cell);

        //    cell.Phrase = new Phrase($": {model.StrikeOffCode}", HEADER_FONT);
        //    table.AddCell(cell);

        //    cell.Phrase = new Phrase("Jumlah Order", HEADER_FONT);
        //    table.AddCell(cell);

        //    cell.Phrase = new Phrase($": {model.ProductionOrderOrderQuantity.ToString("N2", CultureInfo.InvariantCulture)}", HEADER_FONT);
        //    table.AddCell(cell);

        //    cell.Phrase = new Phrase("", HEADER_FONT);
        //    table.AddCell(cell);

        //    if (string.IsNullOrEmpty(model.RepeatedProductionOrderNo))
        //    {


        //        cell.Phrase = new Phrase("Proses", HEADER_FONT);
        //        table.AddCell(cell);

        //        cell.Phrase = new Phrase($": {model.StrikeOffType}", HEADER_FONT);
        //        table.AddCell(cell);

        //        cell.Phrase = new Phrase("Material", HEADER_FONT);
        //        table.AddCell(cell);

        //        cell.Phrase = new Phrase($": {model.ProductionOrderMaterialName}/{model.ProductionOrderMaterialConstructionName}/{model.ProductionOrderMaterialWidth}", HEADER_FONT);
        //        table.AddCell(cell);

        //        cell.Phrase = new Phrase("", HEADER_FONT);
        //        table.AddCell(cell);

        //        cell.Phrase = new Phrase("Tanggal", HEADER_FONT);
        //        table.AddCell(cell);

        //        cell.Phrase = new Phrase($": {model.Date.AddHours(offset).ToString("dd MMMM yyyy", new CultureInfo("id-ID"))}", HEADER_FONT);
        //        table.AddCell(cell);
        //    }
        //    else
        //    {

        //        cell.Phrase = new Phrase("Repeat", HEADER_FONT);
        //        table.AddCell(cell);

        //        cell.Phrase = new Phrase($": {model.RepeatedProductionOrderNo}", HEADER_FONT);
        //        table.AddCell(cell);

        //        cell.Phrase = new Phrase("Material", HEADER_FONT);
        //        table.AddCell(cell);

        //        cell.Phrase = new Phrase($": {model.ProductionOrderMaterialName}/{model.ProductionOrderMaterialConstructionName}/{model.ProductionOrderMaterialWidth}", HEADER_FONT);
        //        table.AddCell(cell);

        //        cell.Phrase = new Phrase("", HEADER_FONT);
        //        table.AddCell(cell);

        //        cell.Phrase = new Phrase("Proses", HEADER_FONT);
        //        table.AddCell(cell);

        //        cell.Phrase = new Phrase($": {model.StrikeOffType}", HEADER_FONT);
        //        table.AddCell(cell);

        //        cell.Phrase = new Phrase("", HEADER_FONT);
        //        table.AddCell(cell);

        //        cell.Phrase = new Phrase("", HEADER_FONT);
        //        table.AddCell(cell);

        //        cell.Phrase = new Phrase("", HEADER_FONT);
        //        table.AddCell(cell);


        //        cell.Phrase = new Phrase("Tanggal", HEADER_FONT);
        //        table.AddCell(cell);

        //        cell.Phrase = new Phrase($": {model.Date.AddHours(offset).ToString("dd MMMM yyyy", new CultureInfo("id-ID"))}", HEADER_FONT);
        //        table.AddCell(cell);
        //    }




        //    return table;
        //}

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

                float rowHeight = 16f;
                table.KeepTogether = true;
                float[] widths = new float[] { 2f, 1f, 1f, 1f, 1f, 1f, 1f };
                table.SetWidths(widths);
                PdfPCell cellCenter = new PdfPCell()
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    MinimumHeight = rowHeight
                };

                PdfPCell cellColor = new PdfPCell()
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    Colspan = 7,
                    //MinimumHeight = rowHeight,
                };

                PdfPCell cellColorName = new PdfPCell()
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    
                    //MinimumHeight = rowHeight,
                };

                PdfPCell cellSubHeader = new PdfPCell()
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    Rowspan = 2,
                    //MinimumHeight = rowHeight,
                };

                PdfPCell cellSubHeader2 = new PdfPCell()
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    
                    //MinimumHeight = rowHeight,
                };

                PdfPCell cellDate = new PdfPCell()
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    MinimumHeight = rowHeight,
                };

                PdfPCell cellLeft = new PdfPCell()
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    MinimumHeight = rowHeight,
                };
                PdfPCell cellRight = new PdfPCell()
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    MinimumHeight = rowHeight,
                };

                PdfPCell cellSample = new PdfPCell()
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_TOP,
                    FixedHeight = 45

                };

                cellColorName.Phrase = new Phrase("Warna", TEXT_FONT_BOLD);
                table.AddCell(cellColorName);

                cellColor.Phrase = new Phrase(item.ColorCode, TEXT_FONT_BOLD);
                table.AddCell(cellColor);

                cellSubHeader.Phrase = new Phrase("Dyestuff & Chemical", TEXT_FONT_BOLD);
                table.AddCell(cellSubHeader);

                cellSubHeader2.Phrase = new Phrase("Resep", TEXT_FONT_BOLD);
                table.AddCell(cellSubHeader2);

                foreach (var adjsText in Adjss)
                {
                    cellSubHeader2.Phrase = new Phrase(adjsText, TEXT_FONT_BOLD);
                    table.AddCell(cellSubHeader2);
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
                    if(detail.Name.ToLower() == VISCOSITAS)
                    {
                        cellRight.Phrase = new Phrase(detail.Name, TEXT_FONT);
                        table.AddCell(cellRight);
                    }
                    else
                    {
                        cellLeft.Phrase = new Phrase(detail.Name, TEXT_FONT);
                        table.AddCell(cellLeft);
                    }
                    

                    if (detail.Name.ToLower() == VISCOSITAS)
                    {
                        if (detail.ReceiptQuantity == 0)
                        {
                            cellCenter.Phrase = new Phrase("", TEXT_FONT);
                            table.AddCell(cellCenter);
                        }
                        else
                        {
                            cellCenter.Phrase = new Phrase(detail.ReceiptQuantity.ToString("N2", CultureInfo.InvariantCulture), TEXT_FONT);
                            table.AddCell(cellCenter);
                        }
                    }
                    else
                    {
                        cellCenter.Phrase = new Phrase(detail.ReceiptQuantity.ToString("N2", CultureInfo.InvariantCulture), TEXT_FONT);
                        table.AddCell(cellCenter);
                    }

                    cellCenter.Phrase = new Phrase(detail.Adjs1Quantity == 0 ? "" : detail.Adjs1Quantity.ToString("N2", CultureInfo.InvariantCulture), TEXT_FONT);
                    table.AddCell(cellCenter);

                    cellCenter.Phrase = new Phrase(detail.Adjs2Quantity == 0 ? "" : detail.Adjs2Quantity.ToString("N2", CultureInfo.InvariantCulture), TEXT_FONT);
                    table.AddCell(cellCenter);

                    cellCenter.Phrase = new Phrase(detail.Adjs3Quantity == 0 ? "" : detail.Adjs3Quantity.ToString("N2", CultureInfo.InvariantCulture), TEXT_FONT);
                    table.AddCell(cellCenter);

                    cellCenter.Phrase = new Phrase(detail.Adjs4Quantity == 0 ? "" : detail.Adjs4Quantity.ToString("N2", CultureInfo.InvariantCulture), TEXT_FONT);
                    table.AddCell(cellCenter);

                    cellCenter.Phrase = new Phrase("", TEXT_FONT);
                    table.AddCell(cellCenter);

                    //var total = detail.ReceiptQuantity + detail.Adjs1Quantity + detail.Adjs2Quantity + detail.Adjs3Quantity + detail.Adjs4Quantity;

                    //if (detail.Name.ToLower() != VISCOSITAS)
                    //{

                    //    cellCenter.Phrase = new Phrase(total.ToString("N2", CultureInfo.InvariantCulture), TEXT_FONT);
                    //    table.AddCell(cellCenter);
                    //}
                    //else
                    //{
                    //    cellCenter.Phrase = new Phrase("", TEXT_FONT);
                    //    table.AddCell(cellCenter);
                    //}
                }

                //cellRight.Phrase = new Phrase("Pembuatan", TEXT_FONT);
                //table.AddCell(cellRight);

                //cellLeft.Phrase = new Phrase("", TEXT_FONT);
                //table.AddCell(cellLeft);

                //cellLeft.Phrase = new Phrase("", TEXT_FONT);
                //table.AddCell(cellLeft);

                //cellLeft.Phrase = new Phrase("", TEXT_FONT);
                //table.AddCell(cellLeft);

                //cellLeft.Phrase = new Phrase("", TEXT_FONT);
                //table.AddCell(cellLeft);

                //cellLeft.Phrase = new Phrase("", TEXT_FONT);
                //table.AddCell(cellLeft);

                //cellLeft.Phrase = new Phrase("", TEXT_FONT);
                //table.AddCell(cellLeft);


                cellSample.Phrase = new Phrase("Pembuatan", TEXT_FONT);
                table.AddCell(cellSample);

                cellSample.Phrase = new Phrase("", TEXT_FONT);
                table.AddCell(cellSample);

                cellSample.Phrase = new Phrase("", TEXT_FONT);
                table.AddCell(cellSample);

                cellSample.Phrase = new Phrase("", TEXT_FONT);
                table.AddCell(cellSample);

                cellSample.Phrase = new Phrase("", TEXT_FONT);
                table.AddCell(cellSample);

                cellSample.Phrase = new Phrase("", TEXT_FONT);
                table.AddCell(cellSample);

                cellSample.Phrase = new Phrase("", TEXT_FONT);
                table.AddCell(cellSample);
                items.Add(table);
            }

            return items;
        }


        private PdfPTable GetDocumentItemsGround(DyestuffChemicalUsageReceiptModel model, int offset)
        {
            PdfPTable table = new PdfPTable(7)
            {
                WidthPercentage = 100
            };
            List<string> Adjss = new List<string>() { "Adjs 1", "Adjs 2", "Adjs 3", "Adjs 4" };
            foreach (var item in model.DyestuffChemicalUsageReceiptItems)
            {
                //PdfPTable table = new PdfPTable(7)
                //{
                //    WidthPercentage = 100
                //};
                table.KeepTogether = true;
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
                PdfPCell cellRight = new PdfPCell()
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                };

                PdfPCell cellSample = new PdfPCell()
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_TOP,

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
                    if (detail.Name.ToLower() == VISCOSITAS)
                    {
                        cellRight.Phrase = new Phrase(detail.Name, TEXT_FONT);
                        table.AddCell(cellRight);
                    }
                    else
                    {
                        cellLeft.Phrase = new Phrase(detail.Name, TEXT_FONT);
                        table.AddCell(cellLeft);
                    }


                    if (detail.Name.ToLower() == VISCOSITAS)
                    {
                        if (detail.ReceiptQuantity == 0)
                        {
                            cellCenter.Phrase = new Phrase("", TEXT_FONT);
                            table.AddCell(cellCenter);
                        }
                        else
                        {
                            cellCenter.Phrase = new Phrase(detail.ReceiptQuantity.ToString("N2", CultureInfo.InvariantCulture), TEXT_FONT);
                            table.AddCell(cellCenter);
                        }
                    }
                    else
                    {
                        cellCenter.Phrase = new Phrase(detail.ReceiptQuantity.ToString("N2", CultureInfo.InvariantCulture), TEXT_FONT);
                        table.AddCell(cellCenter);
                    }

                    cellCenter.Phrase = new Phrase(detail.Adjs1Quantity == 0 ? "" : detail.Adjs1Quantity.ToString("N2", CultureInfo.InvariantCulture), TEXT_FONT);
                    table.AddCell(cellCenter);

                    cellCenter.Phrase = new Phrase(detail.Adjs2Quantity == 0 ? "" : detail.Adjs2Quantity.ToString("N2", CultureInfo.InvariantCulture), TEXT_FONT);
                    table.AddCell(cellCenter);

                    cellCenter.Phrase = new Phrase(detail.Adjs3Quantity == 0 ? "" : detail.Adjs3Quantity.ToString("N2", CultureInfo.InvariantCulture), TEXT_FONT);
                    table.AddCell(cellCenter);

                    cellCenter.Phrase = new Phrase(detail.Adjs4Quantity == 0 ? "" : detail.Adjs4Quantity.ToString("N2", CultureInfo.InvariantCulture), TEXT_FONT);
                    table.AddCell(cellCenter);

                    cellCenter.Phrase = new Phrase("", TEXT_FONT);
                    table.AddCell(cellCenter);

                    //var total = detail.ReceiptQuantity + detail.Adjs1Quantity + detail.Adjs2Quantity + detail.Adjs3Quantity + detail.Adjs4Quantity;

                    //if (detail.Name.ToLower() != VISCOSITAS)
                    //{

                    //    cellCenter.Phrase = new Phrase(total.ToString("N2", CultureInfo.InvariantCulture), TEXT_FONT);
                    //    table.AddCell(cellCenter);
                    //}
                    //else
                    //{
                    //    cellCenter.Phrase = new Phrase("", TEXT_FONT);
                    //    table.AddCell(cellCenter);
                    //}
                }

                //cellRight.Phrase = new Phrase("Pembuatan", TEXT_FONT);
                //table.AddCell(cellRight);

                //cellLeft.Phrase = new Phrase("", TEXT_FONT);
                //table.AddCell(cellLeft);

                //cellLeft.Phrase = new Phrase("", TEXT_FONT);
                //table.AddCell(cellLeft);

                //cellLeft.Phrase = new Phrase("", TEXT_FONT);
                //table.AddCell(cellLeft);

                //cellLeft.Phrase = new Phrase("", TEXT_FONT);
                //table.AddCell(cellLeft);

                //cellLeft.Phrase = new Phrase("", TEXT_FONT);
                //table.AddCell(cellLeft);

                //cellLeft.Phrase = new Phrase("", TEXT_FONT);
                //table.AddCell(cellLeft);


                cellSample.Phrase = new Phrase("Total Pembuatan", TEXT_FONT);
                table.AddCell(cellSample);

                cellSample.Phrase = new Phrase("", TEXT_FONT);
                table.AddCell(cellSample);

                cellSample.Phrase = new Phrase("", TEXT_FONT);
                table.AddCell(cellSample);

                cellSample.Phrase = new Phrase("", TEXT_FONT);
                table.AddCell(cellSample);

                cellSample.Phrase = new Phrase("", TEXT_FONT);
                table.AddCell(cellSample);

                cellSample.Phrase = new Phrase("", TEXT_FONT);
                table.AddCell(cellSample);

                cellSample.Phrase = new Phrase("", TEXT_FONT);
                table.AddCell(cellSample);
               // items.Add(table);
            }

            return table;
        }

        private PdfPTable GetDocumentItemsGround2(DyestuffChemicalUsageReceiptModel model, int offset)
        {
            PdfPTable table = new PdfPTable(5)
            {
                WidthPercentage = 100
            };
            List<string> Adjss = new List<string>() { "Adjs 1", "Adjs 2", "Adjs 3"};
            
                //PdfPTable table = new PdfPTable(7)
                //{
                //    WidthPercentage = 100
                //};
                table.KeepTogether = true;
                float[] widths = new float[] { 1f, 1f, 1f, 1f, 1f };
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
                    MinimumHeight = 15f
                };
                PdfPCell cellRight = new PdfPCell()
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                };

            PdfPCell cellSample = new PdfPCell()
            {
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_TOP,
                MinimumHeight = 175f 

                };

                cellCenter.Phrase = new Phrase("Sample", TEXT_FONT_BOLD);
                table.AddCell(cellCenter);

                //cellColor.Phrase = new Phrase("", TEXT_FONT_BOLD);
                //table.AddCell(cellColor);

                cellCenter.Phrase = new Phrase("Resep Awal", TEXT_FONT_BOLD);
                table.AddCell(cellCenter);

               

                foreach (var adjsText in Adjss)
                {
                    cellCenter.Phrase = new Phrase(adjsText, TEXT_FONT_BOLD);
                    table.AddCell(cellCenter);
                }

            cellSample.Phrase = new Phrase("", TEXT_FONT);
            table.AddCell(cellSample);

            cellSample.Phrase = new Phrase("", TEXT_FONT);
            table.AddCell(cellSample);

            cellSample.Phrase = new Phrase("", TEXT_FONT);
            table.AddCell(cellSample);

            cellSample.Phrase = new Phrase("", TEXT_FONT);
            table.AddCell(cellSample);

            cellSample.Phrase = new Phrase("", TEXT_FONT);
            table.AddCell(cellSample);

            cellLeft.Phrase = new Phrase("Ket :", TEXT_FONT);
            table.AddCell(cellLeft);

            cellLeft.Phrase = new Phrase("Ket :", TEXT_FONT);
            table.AddCell(cellLeft);

            cellLeft.Phrase = new Phrase("Ket :", TEXT_FONT);
            table.AddCell(cellLeft);

            cellLeft.Phrase = new Phrase("Ket :", TEXT_FONT);
            table.AddCell(cellLeft);

            cellLeft.Phrase = new Phrase("Ket :", TEXT_FONT);
            table.AddCell(cellLeft);




            return table;
        }

        //Blank Space
        private PdfPTable GetDocumentItemsGround3(DyestuffChemicalUsageReceiptModel model, int offset)
        {
            PdfPTable table = new PdfPTable(6)
            {
                WidthPercentage = 100
            };
           // List<string> Adjss = new List<string>() { "Adjs 1", "Adjs 2", "Adjs 3" };

            //PdfPTable table = new PdfPTable(7)
            //{
            //    WidthPercentage = 100
            //};
            table.KeepTogether = true;
            
            float[] widths = new float[] { 1f, 2f, 1f, 1f, 1f,1f };
            table.SetWidths(widths);
            PdfPCell cellCenter = new PdfPCell()
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
            };

            

            PdfPCell cellLeft = new PdfPCell()
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                MinimumHeight = 15f
            };
            PdfPCell cellShadow = new PdfPCell()
            {
               
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Border = Rectangle.NO_BORDER,
                
            };

            PdfPCell cellSignature = new PdfPCell()
            {
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_TOP,
                MinimumHeight = 15f

            };


            PdfPCell cellMerge = new PdfPCell()
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                MinimumHeight = 15f,
                Colspan = 3

            };


            PdfPCell cellSample = new PdfPCell()
            {
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_TOP,
                MinimumHeight = 175f,
                //Rowspan = 2

            };

            #region Header 
            cellCenter.Phrase = new Phrase("Adj4", TEXT_FONT_BOLD);
            table.AddCell(cellCenter);

            //cellColor.Phrase = new Phrase("", TEXT_FONT_BOLD);
            //table.AddCell(cellColor);

            cellCenter.Phrase = new Phrase("Hasil Produksi", TEXT_FONT_BOLD);
            table.AddCell(cellCenter);

            cellShadow.Phrase = new Phrase("", TEXT_FONT_BOLD);
            table.AddCell(cellShadow);

            cellMerge.Phrase = new Phrase("Verifikasi", TEXT_FONT_BOLD);
            table.AddCell(cellMerge);

            //cellCenter.Phrase = new Phrase("", TEXT_FONT_BOLD);
            //table.AddCell(cellCenter);

            //cellCenter.Phrase = new Phrase("", TEXT_FONT_BOLD);
            //table.AddCell(cellCenter);

            

            #endregion





            //foreach (var adjsText in Adjss)
            //{
            //    cellCenter.Phrase = new Phrase(adjsText, TEXT_FONT_BOLD);
            //    table.AddCell(cellCenter);
            //}

            #region content
            cellSample.Phrase = new Phrase("", TEXT_FONT);
            table.AddCell(cellSample);

            cellSample.Phrase = new Phrase("", TEXT_FONT);
            table.AddCell(cellSample);

            cellSample.Phrase = new Phrase("", TEXT_FONT);
            table.AddCell(cellShadow);

            cellSample.Phrase = new Phrase("", TEXT_FONT);
            table.AddCell(cellSignature);

            cellSample.Phrase = new Phrase("", TEXT_FONT);
            table.AddCell(cellSignature);

            cellSample.Phrase = new Phrase("", TEXT_FONT);
            table.AddCell(cellSignature);


            #endregion

            




            #region footer
            cellLeft.Phrase = new Phrase("Ket :", TEXT_FONT);
            table.AddCell(cellLeft);

            cellLeft.Phrase = new Phrase("Ket :", TEXT_FONT);
            table.AddCell(cellLeft);

            cellLeft.Phrase = new Phrase("", TEXT_FONT);
            table.AddCell(cellShadow);

            cellLeft.Phrase = new Phrase("Operator", TEXT_FONT);
            table.AddCell(cellLeft);

            cellLeft.Phrase = new Phrase("Colorist", TEXT_FONT);
            table.AddCell(cellLeft);

            cellLeft.Phrase = new Phrase("Ka. Shift", TEXT_FONT);
            table.AddCell(cellLeft);

            
            #endregion




            return table;
        }

        //private PdfPTable GetDocumentItemsSignature(DyestuffChemicalUsageReceiptModel model, int offset)
        //{
        //    PdfPTable table = new PdfPTable(7)
        //    {
        //        WidthPercentage = 50
        //    };
        //    // List<string> Adjss = new List<string>() { "Adjs 1", "Adjs 2", "Adjs 3" };

        //    //PdfPTable table = new PdfPTable(7)
        //    //{
        //    //    WidthPercentage = 100
        //    //};
        //    table.KeepTogether = true;
        //    float[] widths = new float[] { 1f, 1f, 1f, 1f, 1f, 1f, 1f };
        //    table.SetWidths(widths);
        //    PdfPCell cellCenter = new PdfPCell()
        //    {
        //        HorizontalAlignment = Element.ALIGN_CENTER,
        //        VerticalAlignment = Element.ALIGN_MIDDLE,
        //    };

        //    PdfPCell cellColor = new PdfPCell()
        //    {
        //        HorizontalAlignment = Element.ALIGN_CENTER,
        //        VerticalAlignment = Element.ALIGN_MIDDLE,
        //        Colspan = 7
        //    };

        //    PdfPCell cellSubHeader = new PdfPCell()
        //    {
        //        HorizontalAlignment = Element.ALIGN_CENTER,
        //        VerticalAlignment = Element.ALIGN_MIDDLE,
        //        Rowspan = 2
        //    };

        //    PdfPCell cellDate = new PdfPCell()
        //    {
        //        HorizontalAlignment = Element.ALIGN_CENTER,
        //        VerticalAlignment = Element.ALIGN_MIDDLE,
        //        MinimumHeight = 15f,
        //    };

        //    PdfPCell cellLeft = new PdfPCell()
        //    {
        //        HorizontalAlignment = Element.ALIGN_LEFT,
        //        VerticalAlignment = Element.ALIGN_MIDDLE,
        //        MinimumHeight = 15f
        //    };
        //    PdfPCell cellRight = new PdfPCell()
        //    {
        //        HorizontalAlignment = Element.ALIGN_RIGHT,
        //        VerticalAlignment = Element.ALIGN_MIDDLE,
        //    };

        //    PdfPCell cellSample = new PdfPCell()
        //    {
        //        HorizontalAlignment = Element.ALIGN_RIGHT,
        //        VerticalAlignment = Element.ALIGN_TOP,
        //        MinimumHeight = 150f

        //    };

        //    #region Header 
        //    cellCenter.Phrase = new Phrase("Adj4", TEXT_FONT_BOLD);
        //    table.AddCell(cellCenter);

        //    //cellColor.Phrase = new Phrase("", TEXT_FONT_BOLD);
        //    //table.AddCell(cellColor);

        //    cellCenter.Phrase = new Phrase("Hasil Produksi", TEXT_FONT_BOLD);
        //    table.AddCell(cellCenter);

        //    cellCenter.Phrase = new Phrase("", TEXT_FONT_BOLD);
        //    table.AddCell(cellCenter);

        //    cellCenter.Phrase = new Phrase("", TEXT_FONT_BOLD);
        //    table.AddCell(cellCenter);

        //    cellCenter.Phrase = new Phrase("", TEXT_FONT_BOLD);
        //    table.AddCell(cellCenter);

        //    cellCenter.Phrase = new Phrase("", TEXT_FONT_BOLD);
        //    table.AddCell(cellCenter);

        //    cellCenter.Phrase = new Phrase("", TEXT_FONT_BOLD);
        //    table.AddCell(cellCenter);

        //    #endregion





        //    //foreach (var adjsText in Adjss)
        //    //{
        //    //    cellCenter.Phrase = new Phrase(adjsText, TEXT_FONT_BOLD);
        //    //    table.AddCell(cellCenter);
        //    //}

        //    #region content
        //    cellSample.Phrase = new Phrase("", TEXT_FONT);
        //    table.AddCell(cellSample);

        //    cellSample.Phrase = new Phrase("", TEXT_FONT);
        //    table.AddCell(cellSample);

        //    cellSample.Phrase = new Phrase("", TEXT_FONT);
        //    table.AddCell(cellSample);

        //    cellSample.Phrase = new Phrase("", TEXT_FONT);
        //    table.AddCell(cellSample);

        //    cellSample.Phrase = new Phrase("", TEXT_FONT);
        //    table.AddCell(cellSample);

        //    cellSample.Phrase = new Phrase("", TEXT_FONT);
        //    table.AddCell(cellSample);

        //    cellSample.Phrase = new Phrase("", TEXT_FONT);
        //    table.AddCell(cellSample);
        //    #endregion


        //    #region footer
        //    cellLeft.Phrase = new Phrase("Ket :", TEXT_FONT);
        //    table.AddCell(cellLeft);

        //    cellLeft.Phrase = new Phrase("Ket :", TEXT_FONT);
        //    table.AddCell(cellLeft);

        //    cellLeft.Phrase = new Phrase("", TEXT_FONT);
        //    table.AddCell(cellLeft);

        //    cellLeft.Phrase = new Phrase("", TEXT_FONT);
        //    table.AddCell(cellLeft);

        //    cellLeft.Phrase = new Phrase("", TEXT_FONT);
        //    table.AddCell(cellLeft);

        //    cellLeft.Phrase = new Phrase("", TEXT_FONT);
        //    table.AddCell(cellLeft);

        //    cellLeft.Phrase = new Phrase("", TEXT_FONT);
        //    table.AddCell(cellLeft);
        //    #endregion




        //    return table;
        //}
    }





    public class TwoColumnHeaderFooter : PdfPageEventHelper
    {
        public float TableHeight { get; set; }
        public PdfPTable Table { get; set; }

        public TwoColumnHeaderFooter(PdfPTable iso, PdfPTable title, PdfPTable info)
        {
            Table = new PdfPTable(1)
            {
                WidthPercentage = 100,
                TotalWidth = 700
            };
            PdfPCell cell = new PdfPCell()
            {
                Border = Rectangle.NO_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                PaddingBottom = 5f
            };

            cell.Table = iso;
            Table.AddCell(cell);

            cell.Table = title;
            Table.AddCell(cell);

            cell.Table = info;
            Table.AddCell(cell);

            TableHeight = CalculatePdfPTableHeight(Table);
        }

        public float CalculatePdfPTableHeight(PdfPTable table)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Document doc = new Document(PageSize.Flsa);
                PdfWriter w = PdfWriter.GetInstance(doc, ms);

                doc.Open();

                table.WriteSelectedRows(0, table.Rows.Count, 0, 0, w.DirectContent);

                w.Close();
                doc.Close();
                return table.TotalHeight;
            }
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            base.OnEndPage(writer, document);
            Table.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
            Table.WriteSelectedRows(0, -1, document.Left, document.Top + ((document.TopMargin + TableHeight) / 2), writer.DirectContent);

        }
    }
}
