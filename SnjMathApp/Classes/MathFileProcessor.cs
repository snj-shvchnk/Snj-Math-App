using System.Globalization;

namespace SnjMathApp
{
    public class MathFileProcessor
    {
        public char DigitsSeparator = ',';

        public CultureInfo Culture = CultureInfo.InvariantCulture;

        public StringSplitOptions SplitOptions = 
            StringSplitOptions.TrimEntries | 
            StringSplitOptions.RemoveEmptyEntries;

        public ProcessorResponce ProcessFile(string filePath)
        {
            ProcessorResponce responce = new() { FilePath = filePath };
            int i = default;

            using (StreamReader reader = new(filePath))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    responce.LinesCount++;
                    var lineData = ProcessLine(++i, line);

                    if (lineData.IsBroken)
                    {
                        responce.BrokenLines.Add(lineData);
                        continue;
                    }

                    if (responce.MaxLine == null)
                        responce.MaxLine = lineData;

                    if (lineData.Sum > responce.MaxLine.Sum)
                        responce.MaxLine = lineData;
                }
            }

            return responce;
        }

        public LineData ProcessLine (int index, string line)
        {
            var lineData = new LineData()
            {
                LineNumber = index,
                LineText = line,
            };

            try
            {
                lineData.DigitsList = line
                    .Split(',', SplitOptions)
                    .Select(x => Decimal.Parse(x, Culture))
                    .ToList();
            }
            catch (Exception e)
            {
                lineData.Error = e;
            }

            return lineData;
        }
    }
}
