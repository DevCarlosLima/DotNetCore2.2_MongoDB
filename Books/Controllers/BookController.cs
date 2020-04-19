using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.Models;
using Books.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Books.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase {
        private readonly BookService _service;

        public BookController (BookService service) {
            _service = service;
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get () {
            return Ok (_service.Get ());
        }

        // GET api/values/5
        [HttpGet ("{id}")]
        public IActionResult Get (int id) {
            return Ok (_service.Get (id));
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post (BookModel book) {
            _service.Add (book);

            return Ok();
        }

        // PUT api/values/5
        [HttpPut]
        public IActionResult Put (BookModel book) {
            var exist = _service.Get (book.Id);

            if (exist == null)
                return NotFound ();

            _service.Update (book.Id, book);

            return NoContent ();
        }

        // DELETE api/values/5
        [HttpDelete ("{id}")]
        public IActionResult Delete (int id) { 
            var exist = _service.Get (id);

            if (exist == null)
                return NotFound ();

            _service.Remove (id);

            return NoContent ();
        }
    }
}