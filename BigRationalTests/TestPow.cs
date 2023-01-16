namespace Galaxon.Numerics.BigRationalTests;

[TestClass]
public class TestPow
{
    [TestMethod]
    public void TestPowWithIntegerExponent()
    {
        BigRational f = new (2, 3);
        BigRational g = f ^ 2;
        Assert.AreEqual(4, g.Numerator);
        Assert.AreEqual(9, g.Denominator);
    }

    [TestMethod]
    public void TestPowWithNegativeOneExponent()
    {
        BigRational f = new (2, 3);
        BigRational g = f ^ -1;
        Assert.AreEqual(3, g.Numerator);
        Assert.AreEqual(2, g.Denominator);
    }

    [TestMethod]
    public void TestPowWithNegativeIntegerExponent()
    {
        BigRational f = new (2, 3);
        BigRational g = f ^ -2;
        Assert.AreEqual(9, g.Numerator);
        Assert.AreEqual(4, g.Denominator);
    }

    [TestMethod]
    public void TestPowWithBigRationalExponent()
    {
        BigRational f = new (4, 9);
        BigRational g = new (1, 2);
        BigRational h = f ^ g;
        Assert.AreEqual(2, h.Numerator);
        Assert.AreEqual(3, h.Denominator);
    }

    [TestMethod]
    public void TestPowWithDoubleExponent()
    {
        BigRational f = new (4, 9);
        double g = 0.5;
        BigRational h = f ^ g;
        Assert.AreEqual(2, h.Numerator);
        Assert.AreEqual(3, h.Denominator);
    }
}
