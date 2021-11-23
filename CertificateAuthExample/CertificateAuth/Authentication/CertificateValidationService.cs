using System.Security.Cryptography.X509Certificates;

namespace CertificateAuth.Authentication
{
    public class CertificateValidationService
    {
        public bool ValidateCertificate(X509Certificate2 clientCertificate)
        {
            // In production code use key vault to read this.
            
            var cert = new X509Certificate2(Path.Combine("dev_cert.pfx"), "1234");

            if (clientCertificate.Thumbprint == cert.Thumbprint)
            {
                return true;
            }
	
            return false;
        }
    }
}
