using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ModularMonolith.Frontend.Controllers
{
    public class AuthController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var client = _httpClientFactory.CreateClient("WebAPI");
            var response = await client.PostAsJsonAsync("api/auth/login", model);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var token = JsonDocument.Parse(content).RootElement.GetProperty("token").GetString();

                 
                HttpContext.Response.Cookies.Append("JwtToken", token, new CookieOptions { HttpOnly = true });
                return RedirectToAction("Index", "Home");
            }

            return View("Login");
        }

        
        public IActionResult Login() => View();

        public IActionResult Register() => View();
    }
}
