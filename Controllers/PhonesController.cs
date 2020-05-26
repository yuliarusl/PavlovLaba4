using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
using WebApplication3.Storage;

namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhonesController : ControllerBase
    {
        private static IStorage<PhonesData> _memCache = new MemCache();
        public PhonesController(IStorage<PhonesData> memCache)
        {
            _memCache = memCache;
        }


        [HttpGet]
        public ActionResult<IEnumerable<PhonesData>> Get()
        {
            return Ok(_memCache.All);
        }

        [HttpGet("{id}")]
        public ActionResult<PhonesData> Get(Guid id)
        {
            if (!_memCache.Has(id)) return NotFound("Не найдено!");

            return Ok(_memCache[id]);
        }

        [HttpPost]
        public IActionResult Post([FromBody] PhonesData value)
        {
            var validationResult = value.Validate();

            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            _memCache.Add(value);

            return Ok($"{value.ToString()} был добавлен!");
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] PhonesData value)
        {
            if (!_memCache.Has(id)) return NotFound("No such");

            var validationResult = value.Validate();

            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            var previousValue = _memCache[id];
            _memCache[id] = value;

            return Ok($"{previousValue.ToString()} обновлён до {value.ToString()}");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (!_memCache.Has(id)) return NotFound("Не найдено!");

            var valueToRemove = _memCache[id];
            _memCache.RemoveAt(id);

            return Ok($"{valueToRemove.ToString()} был удалён!");
        }
    }
}