using System;
using System.Linq;

class BigInteger
{
    private ulong[] data;
    public BigInteger(ulong[] data)
    {
        this.data = data;
    }

    public static BigInteger SetHex(string hexValue)
    {
        int arraySize = (hexValue.Length + 15) / 16;
        ulong[] dataArray = new ulong[arraySize];
        for (int i = 0; i < arraySize; i++)
        {
            int startIndex = hexValue.Length - (i + 1) * 16;
            if (startIndex < 0) startIndex = 0;
            string hexChunk = hexValue.Substring(startIndex, Math.Min(16, hexValue.Length - startIndex));
            dataArray[i] = ulong.Parse(hexChunk, System.Globalization.NumberStyles.HexNumber);
        }
        return new BigInteger(dataArray);
    }

    public static string GetHex(BigInteger bigInt)
    {
        string hexValue = "";
        for (int i = bigInt.data.Length - 1; i >= 0; i--)
        {
            hexValue += bigInt.data[i].ToString("X16");
        }
        return hexValue;
    }

    public static BigInteger INV(BigInteger bigInt)
    {
        ulong[] resultData = new ulong[bigInt.data.Length];
        for (int i = 0; i < bigInt.data.Length; i++)
        {
            resultData[i] = ~bigInt.data[i];
        }
        return new BigInteger(resultData);
    }

    public static BigInteger XOR(BigInteger bigInt1, BigInteger bigInt2)
    {
        int maxSize = Math.Max(bigInt1.data.Length, bigInt2.data.Length);
        ulong[] resultData = new ulong[maxSize];
        for (int i = 0; i < maxSize; i++)
        {
            ulong value1 = (i < bigInt1.data.Length) ? bigInt1.data[i] : 0;
            ulong value2 = (i < bigInt2.data.Length) ? bigInt2.data[i] : 0;
            resultData[i] = value1 ^ value2;
        }
        return new BigInteger(resultData);
    }

    public static BigInteger OR(BigInteger bigInt1, BigInteger bigInt2)
    {
        int maxSize = Math.Max(bigInt1.data.Length, bigInt2.data.Length);
        ulong[] resultData = new ulong[maxSize];
        for (int i = 0; i < maxSize; i++)
        {
            ulong value1 = (i < bigInt1.data.Length) ? bigInt1.data[i] : 0;
            ulong value2 = (i < bigInt2.data.Length) ? bigInt2.data[i] : 0;
            resultData[i] = value1 | value2;
        }
        return new BigInteger(resultData);
    }

    public static BigInteger AND(BigInteger bigInt1, BigInteger bigInt2)
    {
        int maxSize = Math.Max(bigInt1.data.Length, bigInt2.data.Length);
        ulong[] resultData = new ulong[maxSize];
        for (int i = 0; i < maxSize; i++)
        {
            ulong value1 = (i < bigInt1.data.Length) ? bigInt1.data[i] : 0;
            ulong value2 = (i < bigInt2.data.Length) ? bigInt2.data[i] : 0;
            resultData[i] = value1 & value2;
        }
        return new BigInteger(resultData);
    }

    public static BigInteger ShiftR(BigInteger bigInt, int shift)
    {
        int arrayShift = shift / 64;
        int bitShift = shift % 64;
        int dataSize = bigInt.data.Length;
        ulong[] resultData = new ulong[dataSize];
        for (int i = 0; i < dataSize; i++)
        {
            int prevIndex = i - arrayShift - 1;
            ulong value = 0;
            if (prevIndex >= 0)
            {
                ulong prevValue = bigInt.data[prevIndex];
                ulong shiftedBits = prevValue >> (64 - bitShift);
                value |= shiftedBits;
            }
            if (i - arrayShift >= 0)
            {
                ulong currentBits = bigInt.data[i - arrayShift] << bitShift;
                value |= currentBits;
            }
            resultData[i] = value;
        }
        return new BigInteger(resultData);
    }
    public static BigInteger ShiftL(BigInteger bigInt, int shift)
    {
        int arrayShift = shift / 64;
        int bitShift = shift % 64;
        int dataSize = bigInt.data.Length;
        ulong[] resultData = new ulong[dataSize];
        for (int i = dataSize - 1; i >= 0; i--)
        {
            int nextIndex = i + arrayShift + 1;
            ulong value = 0;
            if (nextIndex < dataSize)
            {
                ulong nextValue = bigInt.data[nextIndex];
                ulong shiftedBits = nextValue << (64 - bitShift);
                value |= shiftedBits;
            }
            if (i + arrayShift < dataSize)
            {
                ulong currentBits = bigInt.data[i + arrayShift] >> bitShift;
                value |= currentBits;
            }
            resultData[i] = value;
        }
        return new BigInteger(resultData);
    }

    public static void TestOperations()
    {
        string hexValue1 = "51bf608414ad5726a3c1bec098f77b1b54ffb2787f8d528a74c1d7fde6470ea4";
        string hexValue2 = "403db8ad88a3932a0b7e8189aed9eeffb8121dfac05c3512fdb396dd73f6331c";
        BigInteger num1 = SetHex(hexValue1);
        BigInteger num2 = SetHex(hexValue2);

        BigInteger invNum1 = INV(num1);
        BigInteger xorNums = XOR(num1, num2);
        BigInteger orNums = OR(num1, num2);
        BigInteger andNums = AND(num1, num2);
        BigInteger shiftRight = ShiftR(num1, 64);
        BigInteger shiftLeft = ShiftL(num1, 64);

        Console.WriteLine($"num1: {GetHex(num1)}");
        Console.WriteLine($"num2: {GetHex(num2)}");
        Console.WriteLine($"INV(num1): {GetHex(invNum1)}");
        Console.WriteLine($"XOR(num1, num2): {GetHex(xorNums)}");
        Console.WriteLine($"OR(num1, num2): {GetHex(orNums)}");
        Console.WriteLine($"AND(num1, num2): {GetHex(andNums)}");
        Console.WriteLine($"ShiftR(num1, 64): {GetHex(shiftRight)}");
        Console.WriteLine($"ShiftL(num1, 64): {GetHex(shiftLeft)}");
    }

    public static void Main()
    {
        TestOperations();
    }
}
