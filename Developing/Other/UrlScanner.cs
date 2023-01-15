using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Developing.Other;

public static class UrlScraper
{
    public static string Scrap(string url)
    {
        var awaiter = CallUrl(url);

        return awaiter.Result == string.Empty ? string.Empty : awaiter.Result;
    }

    public static async Task<string> CallUrl(string url)
    {
        using var client = new HttpClient();
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

        client.DefaultRequestHeaders.Accept.Clear();

        return await client.GetStringAsync(url);
    }

    public class OperatingSystemsScraper
    {
        private string _baseAddress = "https://sop.mini.pw.edu.pl/pl/sop2/lab/l3/";

        public void PerformScrape()
        {
            using var handler = new HttpClientHandler()
            {
                CookieContainer = new CookieContainer()
            };
            using var client = new HttpClient(handler);

        }
    }
}