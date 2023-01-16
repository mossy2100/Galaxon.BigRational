using System.Numerics;
using Galaxon.Core.Numbers;

namespace Galaxon.Numerics;

public partial struct BigRational
{
    #region Cast operators to BigRational

    /// <summary>
    /// Implicitly cast an sbyte to a BigRational.
    /// </summary>
    public static implicit operator BigRational(sbyte num) =>
        new (num);

    /// <summary>
    /// Implicitly cast a byte to a BigRational.
    /// </summary>
    public static implicit operator BigRational(byte num) =>
        new (num);

    /// <summary>
    /// Implicitly cast an short to a BigRational.
    /// </summary>
    public static implicit operator BigRational(short num) =>
        new (num);

    /// <summary>
    /// Implicitly cast a ushort to a BigRational.
    /// </summary>
    public static implicit operator BigRational(ushort num) =>
        new (num);

    /// <summary>
    /// Implicitly cast an int to a BigRational.
    /// </summary>
    public static implicit operator BigRational(int num) =>
        new (num);

    /// <summary>
    /// Implicitly cast a uint to a BigRational.
    /// </summary>
    public static implicit operator BigRational(uint num) =>
        new (num);

    /// <summary>
    /// Implicitly cast an long to a BigRational.
    /// </summary>
    public static implicit operator BigRational(long num) =>
        new (num);

    /// <summary>
    /// Implicitly cast a ulong to a BigRational.
    /// </summary>
    public static implicit operator BigRational(ulong num) =>
        new (num);

    /// <summary>
    /// Implicitly cast an Int128 to a BigRational.
    /// </summary>
    public static implicit operator BigRational(Int128 num) =>
        new (num);

    /// <summary>
    /// Implicitly cast a UInt128 to a BigRational.
    /// </summary>
    public static implicit operator BigRational(UInt128 num) =>
        new (num);

    /// <summary>
    /// Implicitly cast an BigInteger to a rational.
    /// </summary>
    public static implicit operator BigRational(BigInteger num) =>
        new (num);

    /// <summary>
    /// Implicitly cast a floating point value to a BigRational.
    /// This can be done exactly.
    /// </summary>
    public static BigRational ConvertFromFloatingPoint<T>(T x) where T : IFloatingPoint<T>
    {
        // Check the value can be converted.
        if (!T.IsFinite(x))
        {
            throw new InvalidCastException("Cannot cast NaN or ±∞ to a BigRational.");
        }

        // Handle zero.
        if (x == T.Zero)
        {
            return Zero;
        }

        // Get the parts of the floating point value.
        (byte signBit, ushort expBits, ulong fracBits) = x.Disassemble();

        // Convert the fraction bits to a denominator.
        byte nFracBits = XFloatingPoint.GetNumFracBits<T>();
        if (T.IsNormal(x))
        {
            // Set the top bit.
            fracBits |= 1uL << nFracBits;
        }
        BigInteger num = fracBits;

        // Apply the sign.
        if (signBit == 1)
        {
            num = -num;
        }

        // Apply the exponent.
        BigInteger den = 1;
        short maxExp = XFloatingPoint.GetMaxExp<T>();
        short exp = (short)(expBits - maxExp - nFracBits);
        BigInteger pow = BigInteger.Pow(2, XShort.Abs(exp));
        if (exp < 0)
        {
            den = pow;
        }
        else
        {
            num *= pow;
        }

        // Construct and return the new value.
        return new BigRational(num, den);
    }

    /// <summary>
    /// Implicitly cast a Half to a BigRational.
    /// This can be done exactly.
    /// </summary>
    public static implicit operator BigRational(Half x) =>
        ConvertFromFloatingPoint(x);

    /// <summary>
    /// Implicitly cast a float to a BigRational.
    /// This can be done exactly.
    /// </summary>
    public static implicit operator BigRational(float x) =>
        ConvertFromFloatingPoint(x);

    /// <summary>
    /// Implicitly cast a double to a BigRational.
    /// This can be done exactly.
    /// </summary>
    public static implicit operator BigRational(double x) =>
        ConvertFromFloatingPoint(x);

    /// <summary>
    /// Implicitly cast a decimal to a BigRational.
    /// This can be done exactly.
    /// </summary>
    public static implicit operator BigRational(decimal x)
    {
        // Handle zero.
        if (x == 0m)
        {
            return Zero;
        }

        // Get the parts of the floating point value.
        (byte signBit, ushort scaleBits, UInt128 intBits) = x.Disassemble();

        // Get the numerator.
        BigInteger num = (signBit == 1 ? -1 : 1) * (BigInteger)intBits;

        // Get the denominator.
        BigInteger den = BigInteger.Pow(10, scaleBits);

        // Construct and return the new value.
        return new BigRational(num, den);
    }

    #endregion Cast operators to BigRational

    #region Cast operators from BigRational

    /// <summary>
    /// Explicitly cast a BigRational to a sbyte.
    /// </summary>
    /// <exception cref="OverflowException">
    /// If the numerator or denominator is outside the range of double, or if the result is outside
    /// the range of sbyte.
    /// </exception>
    public static explicit operator sbyte(BigRational br) =>
        (sbyte)(double)br;

    /// <summary>
    /// Explicitly cast a BigRational to a byte.
    /// </summary>
    /// <exception cref="OverflowException">
    /// If the numerator or denominator is outside the range of double, or if the result is outside
    /// the range of byte.
    /// </exception>
    public static explicit operator byte(BigRational br) =>
        (byte)(double)br;

    /// <summary>
    /// Explicitly cast a BigRational to a short.
    /// </summary>
    /// <exception cref="OverflowException">
    /// If the numerator or denominator is outside the range of double, or if the result is outside
    /// the range of short.
    /// </exception>
    public static explicit operator short(BigRational br) =>
        (short)(double)br;

    /// <summary>
    /// Explicitly cast a BigRational to a ushort.
    /// </summary>
    /// <exception cref="OverflowException">
    /// If the numerator or denominator is outside the range of double, or if the result is outside
    /// the range of ushort.
    /// </exception>
    public static explicit operator ushort(BigRational br) =>
        (ushort)(double)br;

    /// <summary>
    /// Explicitly cast a BigRational to an int.
    /// </summary>
    /// <exception cref="OverflowException">
    /// If the numerator or denominator is outside the range of double, or if the result is outside
    /// the range of int.
    /// </exception>
    public static explicit operator int(BigRational br) =>
        (int)(double)br;

    /// <summary>
    /// Explicitly cast a BigRational to a uint.
    /// </summary>
    /// <exception cref="OverflowException">
    /// If the numerator or denominator is outside the range of double, or if the result is outside
    /// the range of uint.
    /// </exception>
    public static explicit operator uint(BigRational br) =>
        (uint)(double)br;

    /// <summary>
    /// Explicitly cast a BigRational to a long.
    /// </summary>
    /// <exception cref="OverflowException">
    /// If the numerator or denominator is outside the range of double, or if the result is outside
    /// the range of long.
    /// </exception>
    public static explicit operator long(BigRational br) =>
        (long)(double)br;

    /// <summary>
    /// Explicitly cast a BigRational to a ulong.
    /// </summary>
    /// <exception cref="OverflowException">
    /// If the numerator or denominator is outside the range of double, or if the result is outside
    /// the range of ulong.
    /// </exception>
    public static explicit operator ulong(BigRational br) =>
        (ulong)(double)br;

    /// <summary>
    /// Explicitly cast a BigRational to an Int128.
    /// </summary>
    /// <exception cref="OverflowException">
    /// If the numerator or denominator is outside the range of double, or if the result is outside
    /// the range of Int128.
    /// </exception>
    public static explicit operator Int128(BigRational br) =>
        (Int128)(double)br;

    /// <summary>
    /// Explicitly cast a BigRational to a UInt128.
    /// </summary>
    /// <exception cref="OverflowException">
    /// If the numerator or denominator is outside the range of double, or if the result is outside
    /// the range of UInt128.
    /// </exception>
    public static explicit operator UInt128(BigRational br) =>
        (UInt128)(double)br;

    /// <summary>
    /// Explicitly cast a BigRational to a Half.
    /// </summary>
    /// <exception cref="OverflowException">
    /// If the numerator or denominator is outside the range of double, or if the result is outside
    /// the range of Half.
    /// </exception>
    public static explicit operator Half(BigRational br) =>
        (Half)(double)br;

    /// <summary>
    /// Explicitly cast a BigRational to a float.
    /// </summary>
    /// <exception cref="OverflowException">
    /// If the numerator or denominator is outside the range of double, or if the result is outside
    /// the range of float.
    /// </exception>
    public static explicit operator float(BigRational br) =>
        (float)(double)br;

    /// <summary>
    /// Explicitly cast a BigRational to a double.
    /// </summary>
    /// <exception cref="OverflowException">
    /// If the numerator, denominator, or result is outside the range of double.
    /// </exception>
    public static explicit operator double(BigRational br) =>
        (double)br.Numerator / (double)br.Denominator;

    /// <summary>
    /// Explicitly cast a BigRational to a decimal.
    /// </summary>
    /// <exception cref="OverflowException">
    /// If the numerator, denominator, or result is outside the range of decimal.
    /// </exception>
    public static explicit operator decimal(BigRational br) =>
        (decimal)br.Numerator / (decimal)br.Denominator;

    #endregion Cast operators from BigRational
}
