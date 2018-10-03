using Com.Danliris.Service.Finishing.Printing.Lib.Models.ReturToQC;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        private static readonly Font title_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 15);
        private static readonly Font iso_font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10);
        private static readonly Font header_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10);
        private static readonly Font bold_font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 9);
        private static readonly Font normal_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 9);
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
            List<string> headerLefts1 = new List<string> { HEADER_LEFT_1 };
            List<string> headerLefts2 = new List<string> { string.Format("{0} {1}", SPLITTER, model.Destination), model.Remark };
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
                null,
                returToQCItemDetail.Select(x => (x.Length / YARD_DIVIDER)).Sum().ToString("N2", CultureInfo.InvariantCulture),
                returToQCItemDetail.Select(x => x.Length).Sum().ToString("N2", CultureInfo.InvariantCulture),
                returToQCItemDetail.Select(x => x.Weight).Sum().ToString("N2", CultureInfo.InvariantCulture)
            };
            #endregion

            #region Body Footer
            List<string> bodyFooter = new List<string> { string.Format("[{0} {1} {2}]",KODE_BARANG, SPLITTER, model.Destination), model.Remark };
            #endregion


            //this.Title = this.GetTitle();
            //this.Header = this.GetHeader(headerLefts, headerRights1, headerRights2);
            //this.Body = this.GetBody(bodyColumn, bodyData, totalData);
            //this.BodyFooter = this.GetBodyFooter(footerHeaders, footerValues);
            //this.Footer = this.GetFooter(model.Date.AddHours(timeoffset), model.CreatedBy);
        }
    }
}
