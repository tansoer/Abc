using System;
using System.Net;
using System.Net.Http;
using Abc.Aids.Logging;

namespace Abc.Aids.Services {

    public static class WebService {

        public static string Load(string url) {
            var num = 0;

            while (num <= 3) {
                num++;
                using var client = new HttpClient();

                try { return client.GetStringAsync(url).GetAwaiter().GetResult(); }
                catch (Exception e) { Log.Exception(e); }
            }

            return string.Empty;
        }

    }

}