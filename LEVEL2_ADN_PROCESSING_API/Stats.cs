namespace LEVEL2_ADN_PROCESSING_API
{
    public class Stats
    {
        public int CountMutantDNA { get; set; }

        public int CountHumanDNA { get; set; }

        public float Ratio { get; set; }
    }

    public class StatsDB 
    {
        public int numberOf { get; set; }
        public int percentage { get; set; }
        public bool IsHuman { get; set; }

    }
}
