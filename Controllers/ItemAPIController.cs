using CyberMall.Data;
using CyberMall.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Data.SqlClient.DataClassification;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CyberMall.Controllers
{
    [Route("api/Item")]
    [ApiController]
    public class ItemAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ItemAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<List<ItemListingViewModel>> Index()
        {
            return await _context.ItemListings.Select(item => new ItemListingViewModel
            {
                Id = item.Id,
                SellerId = item.Seller.Id,
                ProductName = item.ProductName,
                Description = item.Description,
                Price = item.Price,
                Quantity = item.Quantity
            }).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Index(long? id)
        {
            List<ItemListingViewModel> itemList = new List<ItemListingViewModel>();
            ItemListing item = await _context.ItemListings.FindAsync(id);
            if (item == null)
            {
                return NotFound(itemList);
            }

            if (!item.Visible)
            {
                return Unauthorized(itemList);
            }

            itemList.Add(new ItemListingViewModel
            {
                Id = item.Id,
                SellerId = item.Seller.Id,
                ProductName = item.ProductName,
                Description = item.Description,
                Price = item.Price,
                Quantity = item.Quantity
            });
            
            return Ok(itemList);
        }

        public class ItemListingViewModel
        {
            public long Id { get; set; }

            public string SellerId { get; set; }

            public string ProductName { get; set; }
            public string Description { get; set; } // Description of the product
            public decimal Price { get; set; } // Price of the item

            public int Quantity { get; set; } // Available quantity
        }
    }
}
