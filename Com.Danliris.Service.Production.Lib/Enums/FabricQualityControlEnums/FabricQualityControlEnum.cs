
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Enums.FabricQualityControlEnums
{
    public class FabricQualityControlEnums
    {
        public enum GeneralCriteriaName
        {
            [Display(Name = "Slubs")]
            Slubs = 1,
            [Display(Name = "Neps")]
            Neps,
            [Display(Name = "Kontaminasi Fiber")]
            KontaminasiFiber,
            [Display(Name = "Pakan Renggang")]
            PakanRenggang,
            [Display(Name = "Pakan Rapat")]
            PakanRapat,
            [Display(Name = "Pakan Double")]
            PakanDouble,
            [Display(Name = "Pakan Tebal Tipis")]
            PakanTebalTipis,
            [Display(Name = "Lusi Tebal Tipis")]
            LusiTebalTipis,
            [Display(Name = "Lusi Putus")]
            LusiPutus,
            [Display(Name = "Lusi Double")]
            LusiDouble,
            [Display(Name = "Madal Sumbi")]
            MadalSumbi,
            [Display(Name = "Salah Anyam / UP")]
            SalahAnyamUP,
            [Display(Name = "Reed Mark")]
            ReedMark,
            [Display(Name = "Temple Mark")]
            TempleMark,
            [Display(Name = "Snarl")]
            Snarl,
            [Display(Name = "Sobek Tepi")]
            SobekTepi,
            [Display(Name = "Kusut Mati")]
            KusutMati,
            [Display(Name = "Kusut / Krismak")]
            KusutKrismak,
            [Display(Name = "Belang Kondesat")]
            BelangKondesat,
            [Display(Name = "Belang Absorbsi")]
            BelangAbsorbsi,
            [Display(Name = "Flek Minyak / Dyest")]
            FlekMinyakDyest,
            [Display(Name = "Flek Oil Jarum")]
            FlekOilJarum,
            [Display(Name = "Bintik Htm, Mrh, Biru")]
            BintikHtmMrhBiru,
            [Display(Name = "Tepi Melipat")]
            TepiMelipat,
            [Display(Name = "Lebar Tak Sama")]
            LebarTakSama,
            [Display(Name = "Lubang / Pin Hole")]
            LubangPinHole,
            [Display(Name = "Bowing")]
            Bowing,
            [Display(Name = "Skewing")]
            Skewing,
            [Display(Name = "Meleset")]
            Meleset,
            [Display(Name = "Flek")]
            Flek,
            [Display(Name = "Print Kosong / Bundas")]
            PrintKosongBundas,
            [Display(Name = "Nyetrip")]
            Nyetrip,
            [Display(Name = "Kotor Tanah / Debu")]
            KotorTanahDebu,
            [Display(Name = "Kotor Hitam")]
            KotorHitam,
            [Display(Name = "Belang Kusut")]
            BelangKusut,
        }

        public enum GeneralCriteriaCode
        {
            B001 = 1,
            B002,
            B003,
            W001,
            W002,
            W003,
            W004,
            W005,
            W006,
            W007,
            W008,
            W009,
            W010,
            W011,
            W012,
            P001,
            P002,
            P003,
            P004,
            P005,
            P006,
            P007,
            P008,
            P009,
            P010,
            P011,
            P012,
            P013,
            P201,
            P202,
            P203,
            P204,
            P101,
            P102,
            P103
        }

        public enum GeneralCriteriaGroup
        {
            BENANG = 1,
            WEAVING,
            PRODUKSI
        }
    }
}
