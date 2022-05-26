using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalrChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalrChat.Controllers
{
    public class ProductController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IHubContext<SignalrServer> _signalrHub;

        public ProductController(AppDbContext context, IHubContext<SignalrServer> signalrHub)
        {
            _context = context;
            _signalrHub = signalrHub;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var res = _context.Products.ToList();
            return Ok(res);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create ([Bind("ProductId,ProductName,Category,UnitPrice,StockQuantity")] Product products)
        {
            if (ModelState.IsValid)
            {
                _context.Add(products);
                await _context.SaveChangesAsync();
                await _signalrHub.Clients.All.SendAsync("LoadProducts");
                return RedirectToAction(nameof(Index));
            }
            return View(products);
        }

    }
}
