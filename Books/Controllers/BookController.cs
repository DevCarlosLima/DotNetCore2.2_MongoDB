using Books.Models;
using Books.Services;
using Microsoft.AspNetCore.Mvc;

namespace Books.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase {
        private readonly BookService _service;

        public BookController (BookService service) {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get () {
            return Ok (_service.Get ());
        }

        [HttpGet ("{id}")]
        public IActionResult Get (int id) {
            return Ok (_service.Get (id));
        }

        [HttpPost]
        public IActionResult Post (BookModel book) {
            _service.Add (book);

            return Ok ();
        }

        [HttpPut]
        public IActionResult Put (BookModel book) {
            var exist = _service.Get (book.Id);

            if (exist == null)
                return NotFound ();

            _service.Update (book.Id, book);

            return NoContent ();
        }

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