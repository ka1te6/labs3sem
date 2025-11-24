using System.Security.Cryptography;

namespace Task3;

public sealed class SingleRandomizer
{
    private static readonly Lazy<SingleRandomizer> _lazy = new(() => new SingleRandomizer());

    public static SingleRandomizer Instance => _lazy.Value;

    private SingleRandomizer()
    {
    }

    public int NextInt(int minInclusive, int maxExclusive)
    {
        if (minInclusive >= maxExclusive)
        {
            throw new ArgumentException("min must be less than max");
        }

        return RandomNumberGenerator.GetInt32(minInclusive, maxExclusive);
    }

    public double NextDouble()
    {
        Span<byte> bytes = stackalloc byte[8];
        RandomNumberGenerator.Fill(bytes);
        var ul = BitConverter.ToUInt64(bytes);
        return ul / (double)ulong.MaxValue;
    }
}

