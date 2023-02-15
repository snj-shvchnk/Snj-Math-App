using System.Globalization;

namespace SnjMathAppTests
{
    public class IntegrationTests
    {
        private int digitsInLine = 100;
        private int linesInCollection = 1000000;
        private string filePath;

        [SetUp]
        public void Setup()
        {
            // Create random lines for processing
            filePath = Path.GetTempFileName();
            List<decimal> digits;
            Random random;

            for (int i = 0; i < linesInCollection; i++)
            {
                digits = new();
                for (int j = 0; j < digitsInLine; j++)
                {
                    random = new Random(digits.GetHashCode());
                    digits.Add(
                        Convert.ToDecimal(
                            random.NextDouble() * 
                            ((j + i - 0.676m) / (i - j + 0.334m)).GetHashCode()
                        )
                    );
                }

                var line = 
                    String.Join(
                        ',',
                        digits.Select(
                            s => s.ToString(CultureInfo.InvariantCulture)
                        )
                    );

                File.AppendAllText(filePath, $"{line}\n");
            }
        }

        [Test]
        public void IntegrationTest()
        {
            var p = new SnjMathApp.MathFileProcessor();
            var r = p.ProcessFile(filePath);

            Assert.IsNotNull(r);
            Assert.IsEmpty(r.BrokenLines);
            Assert.That(r.LinesCount, Is.EqualTo(linesInCollection));
            Assert.IsNotNull(r.MaxLine);

            File.Delete(filePath);
        }
    }
}