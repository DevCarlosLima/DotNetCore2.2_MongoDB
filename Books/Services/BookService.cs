using System.Collections.Generic;
using Books.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Books.Services {
    public class BookService {
        private readonly IMongoCollection<BookModel> _books;

        public BookService (IConfiguration config) {
            var client = new MongoClient (config.GetConnectionString ("BookConnection"));
            var database = client.GetDatabase ("BookDb");
            _books = database.GetCollection<BookModel> ("Books");
        }

        public BookModel Add(BookModel book){
            _books.InsertOne(book);
            return book;
        }

        public IEnumerable<BookModel> Get () {
            return _books.Find (x => true).ToList ();
        }

        public BookModel Get(int id){
            return _books.Find<BookModel>(x=>x.Id == id).FirstOrDefault();
        }

        public void Update(int id, BookModel book){
            _books.ReplaceOne(x=>x.Id == id, book);
        }

        public void Remove(int id){
            _books.DeleteOne(x=>x.Id == id);
        }
    }
}