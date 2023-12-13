using FluentResults;
using System.Net;
using System.Text;

namespace CustomerApp.Domain.Helpers;

public static class CustomExtensions
{
    public static string GetIpAddresss(this IPAddress? ipAdress)
    {
        if (ipAdress is null)
        {
            return string.Empty;
        }

        if (ipAdress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
        {
            ipAdress = Dns.GetHostEntry(ipAdress).AddressList
                .First(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
        }

        return ipAdress?.ToString() ?? string.Empty;
    }

    public static string ReasonsToString(this IEnumerable<IReason> reasons)
    {
        return
            string.Join(Environment.NewLine,
            reasons
            .Select(reason => (reasons.Count() > 1 ? "- " : "") + reason.Message));
    }

    public static string ToFirstLetterUp(this string str)
    {
        StringBuilder sb = new(str.Length);
        bool capitalize = true;
        foreach (char chr in str)
        {
#pragma warning disable S3358 // Ternary operators should not be nested
            sb.Append(capitalize ? (chr == 'i' ? 'İ' : Char.ToUpper(chr)) : Char.ToLower(chr));
#pragma warning restore S3358 // Ternary operators should not be nested
            capitalize = !Char.IsLetter(chr);
        }
        return sb.ToString();
    }
}
