using Microsoft.AspNetCore.Mvc;
using GamingStore.Data;
using GamingStore.Models;
using System.Linq;

namespace GamingStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

       
        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            return View(products);
        }
        public IActionResult NewPage(){
            return View();
        }

        public IActionResult Apropos(){
            return View();
        }
       
        public IActionResult Buy(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound(); 
            }
            return View(product); 
        }

       
        [HttpPost]
        public IActionResult ConfirmBuy(int productId, string account, string password)
        {
            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Veuillez remplir tous les champs.");
            }

            if (!ModelState.IsValid)
            {
                var product = _context.Products.FirstOrDefault(p => p.Id == productId);
                return View("Buy", product); 
            }

          
            if (account == "test" && password == "1234")
            {
                TempData["Success"] = "Achat confirmé !";
                return RedirectToAction("Index");
            }

            TempData["Error"] = "Compte ou mot de passe incorrect.";
            var productError = _context.Products.FirstOrDefault(p => p.Id == productId);
            return View("Buy", productError); 
        }
    }
}
