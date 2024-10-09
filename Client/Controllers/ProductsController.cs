using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shared;

namespace Client.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly HttpClient _httpClient;
        public ProductsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("https://localhost:7003/api/Products");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<List<Product>>(result);
                return View(products);
            }
            return View("Error");
        }
    }
}
