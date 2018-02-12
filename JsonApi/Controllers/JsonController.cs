using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JsonApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JsonApi.Controllers
{
    [Route("api/[controller]")]
    public class JsonController : Controller
    {
        private readonly JsonContext _context;

        public JsonController(JsonContext context)
        {
            _context = context;

            if(_context.JsonItems.Count() == 0)
            {
                _context.JsonItems.Add(new JsonItem { Data = "[value1]" });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<JsonItem> GetAll()
        {
            return _context.JsonItems.ToList();
        }

        [HttpGet("{id}", Name = "GetJson")]
        public IActionResult GetById(long id)
        {
            var item = _context.JsonItems.FirstOrDefault(x => x.Id == id);
            if (item == null)
                return null;

            var data = JsonConvert.DeserializeObject(item.Data);            
            return new OkObjectResult(data);
        }

        [HttpPost]
        public IActionResult Create([FromBody] string data)
        {
            if (data == null)
                return BadRequest();
            var item = new JsonItem()
            {
                Data = data
            };
            _context.JsonItems.Add(item);
            _context.SaveChanges();

            return new ObjectResult(item.Id);
            
            //return CreatedAtRoute("GetJson", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] JsonItem item)
        {
            if (item == null || item.Id != id)
                return BadRequest();

            var jsonItem = _context.JsonItems.FirstOrDefault(x => x.Id == id);
            if (jsonItem == null)
                return NotFound();

            jsonItem.Data = item.Data;

            _context.JsonItems.Update(jsonItem);
            _context.SaveChanges();

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var item = _context.JsonItems.FirstOrDefault(x => x.Id == id);
            if (item == null)
                return NotFound();

            _context.JsonItems.Remove(item);
            _context.SaveChanges();

            return new NoContentResult();
        }
    }
}
