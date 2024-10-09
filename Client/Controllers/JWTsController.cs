using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shared;
using System.Net.Http;
using System.Text;

namespace Client.Controllers
{
    public class JWTsController : Controller
    {
        private readonly HttpClient _httpClient;
        public JWTsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            var content = new StringContent(JsonConvert.SerializeObject(loginViewModel),
                Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:7003/api/JWTs/login", content);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var token = JsonConvert.DeserializeObject<TokenResponse>(result).Token;

                HttpContext.Session.SetString("JWT", token);
                return View();
            }
            return View("Error");
        }
    }
}
