using System.Security.Cryptography;
using System.Text;

namespace webapp_boilerplate.Shared.Utils
{
    public static class FingerprintHelper
    {
        public static string GenerateDeviceFingerprint(string userAgent, string ipAddress)
        {
            var raw = $"{userAgent}_{ipAddress}";
            using (SHA256 sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(raw);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        public static bool ValidateFingerprint(string receivedFingerprint, string userAgent, string ipAddress)
        {
            var expected = GenerateDeviceFingerprint(userAgent, ipAddress);
            return receivedFingerprint == expected;
        }
    }
}
