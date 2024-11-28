using CyberMall.Data;
using CyberMall.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet("{id}")]
        public async Task<List<ItemListing>> Index(int? id)
        {
            if (id == null)
            {
                // To-Do: don't just leak everything and create a container object
                return await _context.ItemListings.ToListAsync();
            }    
            else
            {
                List<ItemListing> itemList = new List<ItemListing>();
                ItemListing listing = await _context.ItemListings.FindAsync(id);
                itemList.Add(listing);
                return itemList;
            }
        }

    }
}
