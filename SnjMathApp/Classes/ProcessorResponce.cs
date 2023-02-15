namespace SnjMathApp
{
    public class ProcessorResponce
    {
        public string FilePath { get; set; }

        public int LinesCount { get; set; }

        public LineData MaxLine { get; set; }

        public IList<LineData> BrokenLines { get; set; } = new List<LineData>();
    }
}
