namespace Com.Danliris.Service.Finishing.Printing.Lib.Models.FabricQualityControl
{
    public class CriteriaModel
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public int Group { get; set; }
        public int Name { get; set; }
        public double ScoreA { get; set; }
        public double ScoreB { get; set; }
        public double ScoreC { get; set; }
        public double ScoreD { get; set; }

        public int FabricGradeTestId { get; set; }
        public virtual FabricGradeTestModel FabricGradeTest { get; set; }
    }
}