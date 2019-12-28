using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileStorage.Web.Infrastructure
{
    public static class ShortUrlService
    {
        private static char[] map = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();

        public static string GetUrl(Guid guid)
        {
            byte[] bytes = guid.ToByteArray();
            return Convert.ToBase64String(bytes).Trim('=');
        }

        public static Guid GetId(string url)
        {
            return Guid.NewGuid();
        }
    }
}
