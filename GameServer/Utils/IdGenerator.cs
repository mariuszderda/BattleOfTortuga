namespace GameServer.Utils;

using System;
using System.Text;

public static class IdGenerator
{
    private static readonly char[] chars =
        "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();

    private static readonly Random random = new Random();

    public static string GenerateId(int length = 9)
    {
        var sb = new StringBuilder(length);
        for (int i = 0; i < length; i++) {
            var idx = random.Next(chars.Length);
            sb.Append(chars[idx]);
        }

        return sb.ToString();
    }
}