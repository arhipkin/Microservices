using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Settings
{
    public class TokenSettings
    {
        public TimeSpan Expiration { get; set; }
        public string Issuer { get; set; }
        public string PublicCertPath { get; set; }
        public string PrivateCertPath { get; set; }
        public string PrivateCertPassword { get; set; }
    }
}
