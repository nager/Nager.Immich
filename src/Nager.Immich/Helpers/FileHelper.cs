using System;
using System.IO;
using System.Security.Cryptography;

namespace Nager.Immich.Helpers
{
    public static class FileHelper
    {
        public static string ComputeSha1Checksum(string filePath)
        {
            using var sha1 = SHA1.Create();
            using var stream = File.OpenRead(filePath);
            var hash = sha1.ComputeHash(stream);
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }
    }
}
