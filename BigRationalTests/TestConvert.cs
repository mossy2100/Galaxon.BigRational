using System.Diagnostics;

namespace Galaxon.Numerics.BigRationalTests;

[TestClass]
public class TestConvert
{
    [TestMethod]
    public void TestCastFromInt()
    {
        int a = 5;
        BigRational f = a;
        Assert.AreEqual(5, f.Numerator);
        Assert.AreEqual(1, f.Denominator);
    }

    [TestMethod]
    public void TestCastFromDoubleExact()
    {
        double d = 5;
        BigRational f = d;

        Trace.WriteLine(d);
        Trace.WriteLine(f.Numerator);
        Trace.WriteLine(f.Denominator);
        Trace.WriteLine((double)f.Numerator / (double)f.Denominator);

        Assert.AreEqual(5, f.Numerator);
        Assert.AreEqual(1, f.Denominator);
    }

    /// <summary>
    /// This example shows how a double converts exactly to a BigRational, but also how the double
    /// only stores an approximation of the original decimal fraction.
    /// </summary>
    [TestMethod]
    public void TestCastFromDoubleFuzzy()
    {
        double d = 0.1;
        BigRational f = d;

        Trace.WriteLine(d);
        Trace.WriteLine(f.Numerator);
        Trace.WriteLine(f.Denominator);

        double d2 = (double)f.Numerator / (double)f.Denominator;
        Trace.WriteLine(d2);

        Assert.AreEqual(3602879701896397, f.Numerator);
        Assert.AreEqual(36028797018963968, f.Denominator);
        Assert.AreEqual(d, d2);
    }
}
