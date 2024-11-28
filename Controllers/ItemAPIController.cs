using CyberMall.Data;
using CyberMall.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Data.SqlClient.DataClassification;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberMall.Controllers
{
    [Route("api/Item")]
    [ApiController]
    public class ItemAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ItemAPIController(ApplicationDbContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<List<ItemListing>> Index()
        {
            return await _context.ItemListings.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Index(int? id)
        {
            List<ItemListing> itemList = new List<ItemListing>();
            ItemListing listing = await _context.ItemListings.FindAsync(id);

            itemList.Add(listing);
            if (listing == null)
            {
                return NotFound(itemList);
            }
            return Ok(itemList);
        }

    }
}
