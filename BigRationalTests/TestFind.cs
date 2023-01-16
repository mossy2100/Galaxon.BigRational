using System.Diagnostics;

namespace Galaxon.Numerics.BigRationalTests;

[TestClass]
public class TestFind
{
    [TestMethod]
    public void TestFindSingleDigit()
    {
        for (int n = 0; n < 10; n++)
        {
            for (int d = 1; d < 10; d++)
            {
                BigRational f = new (n, d);
                double x = (double)n / d;
                BigRational f2 = BigRational.Find(x);
                Trace.WriteLine($"Testing that {f} == {x}");
                Assert.AreEqual(f, f2);
            }
        }
    }

    [TestMethod]
    public void TestFindHalf()
    {
        double x = 0.5;
        BigRational f = BigRational.Find(x);
        Assert.AreEqual(1, f.Numerator);
        Assert.AreEqual(2, f.Denominator);
    }

    [TestMethod]
    public void TestFindThird()
    {
        double x = 0.333333333333333;
        BigRational f = BigRational.Find(x);
        Assert.AreEqual(1, f.Numerator);
        Assert.AreEqual(3, f.Denominator);
    }

    [TestMethod]
    public void TestFindRandom()
    {
        Random rnd = new ();

        // Get a random numerator.
        int n = rnd.Next();

        // Get a random denominator but not 0.
        int d = 0;
        while (d == 0)
        {
            d = rnd.Next();
        }

        BigRational f = new (n, d);
        double x = (double)n / d;
        BigRational f2 = BigRational.Find(x);
        Trace.WriteLine($"f = {f}, x = {x}, f2 = {f2}");
        Assert.AreEqual(f, f2);
    }

    [TestMethod]
    public void TestFindPi()
    {
        double x = PI;
        BigRational f = BigRational.Find(x);
        double y = (double)f;
        Assert.AreEqual(x, y);
        Assert.AreEqual(245850922, f.Numerator);
        Assert.AreEqual(78256779, f.Denominator);
    }

    [TestMethod]
    public void TestFindPiLowerPrecision()
    {
        double x = PI;
        BigRational f = BigRational.Find(x, 1e-4);
        Assert.AreEqual(355, f.Numerator);
        Assert.AreEqual(113, f.Denominator);
    }
}
