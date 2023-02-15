namespace SnjMathApp
{
    public class LineData
    {
        internal LineData() { }

        public LineData(List<decimal> digits)
        {
            DigitsList = digits;
        }

        public int LineNumber { get; set; }
        public string? LineText { get; set; }

        internal IList<decimal> DigitsList { get; set; } = new List<decimal>();
        public IEnumerable<decimal> Digits => DigitsList as IEnumerable<decimal>;

        public Exception? Error { get; set; }
        public bool IsValide { get => Error == null; }
        public bool IsBroken { get => !IsValide; }

        private decimal? _sum = null;
        public decimal? Sum
        {
            get
            {
                if (_sum == null)
                    _sum = Digits.Sum();
                return _sum;
            }
        }

        public override string ToString() => $"{LineNumber}: {LineText}";
    }
}
