using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAppKeyVault.Models;
using Microsoft.Azure.KeyVault;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace WebAppKeyVault.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {            
            var kv = new KeyVaultClient(GetToken);
            var vaultUrl = "Vault URI";
            var secretName = "mysecret";
            var mysecret = "";
            try
            {
                mysecret = kv.GetSecretAsync(vaultUrl, secretName).Result.Value;
            }
            catch (Exception e)
            {
                throw;
            }
            return View();
        }
        public async Task<string> GetToken(string authority, string resource, string scope)
        {
            var clientId = "";
            var clientSecret = "";
            var authContext = new Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationContext(authority);
            ClientCredential clientCred = new ClientCredential(clientId, clientSecret);
            AuthenticationResult result = await authContext.AcquireTokenAsync(resource, clientCred);
            if (result == null)
                throw new InvalidOperationException();
            return result.AccessToken;
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
