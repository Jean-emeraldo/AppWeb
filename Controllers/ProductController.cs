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
    }
}