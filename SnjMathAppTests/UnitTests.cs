using SnjMathApp;
using System.Globalization;

namespace SnjMathAppTests
{
    public class UnitTests
    {
        [Test]
        public void LineDataCtorTest()
        {
            var l = new LineData(new List<decimal>());
            Assert.IsNotNull(l);
            Assert.That(l.Error, Is.Null);
            Assert.That(l.LineNumber, Is.EqualTo(0));
            Assert.That(l.IsValide, Is.EqualTo(true));
            Assert.That(l.IsBroken, Is.EqualTo(false));
            Assert.That(l.Digits, Is.Not.Null);
            Assert.That(l.Digits, Is.Empty);
            Assert.That(l.LineText, Is.Null);

            var s = l.Sum;
            Assert.That(s, Is.EqualTo(0));
        }

        [Test]
        public void LineDataMathTest()
        {
            var l = 
                new LineData(new List<decimal>() {
                    1.2m, 3.0004m, -10000, 100000, 2.323223m
                });

            var s1 = l.Sum;
            Assert.That(s1, Is.EqualTo(90006.523623));
        }

        [Test]
        public void ParseLineValidTest ()
        {
            var t = "2432.3, 324.21,3,-1, 4.2";
            var p = new MathFileProcessor();
            var l = p.ProcessLine(11, t);

            Assert.IsNotNull(l);
            Assert.IsNull(l.Error);
            Assert.IsTrue(l.IsValide);
            Assert.IsFalse(l.IsBroken);
            Assert.That(l.LineNumber, Is.EqualTo(11));
            Assert.That(l.LineText, Is.EqualTo(t));
            Assert.That(l.Digits, Is.Not.Null);
            Assert.That(l.Digits, Is.Not.Empty);
            Assert.That(l.Digits.Count, Is.EqualTo(5));
            Assert.That(l.Sum, Is.EqualTo(2762.71));
        }

        [Test]
        public void ParseLineBrokenTest()
        {
            var t = "2432.3 Broken, 324.21,3,-1, 4.2";
            var p = new MathFileProcessor();

            try
            {
                var l = p.ProcessLine(13, t);

                Assert.IsNotNull(l);
                Assert.IsNotNull(l.Error);
                Assert.IsFalse(l.IsValide);
                Assert.IsTrue(l.IsBroken);
                Assert.That(l.LineNumber, Is.EqualTo(13));
                Assert.That(l.LineText, Is.EqualTo(t));
                Assert.That(l.Digits, Is.Not.Null);
                Assert.That(l.Digits, Is.Empty);
                Assert.That(l.Sum, Is.EqualTo(0));
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
    }
}