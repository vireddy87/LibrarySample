using Library.Repository;
using Library.Service;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace Library.Controllers
{
    [System.Web.Mvc.RoutePrefix("api/books")]    
    public class BooksController : ApiController
    {
        BooksService booksService = new BooksService();

        public BooksController()
        { }
        /// <summary>
        /// Get all books
        /// </summary>
        /// <returns>json books list</returns>
        public IHttpActionResult Get()
        {            
            var books = booksService.GetBookNames(); 
            if(books == null)
            {
                return NotFound();
            }
            return Json(books.ToList());
        }
        /// <summary>
        /// Get Word counts per book 
        /// </summary>
        /// <param name="Id"></param> bookId 
        /// <returns></returns>
        public IHttpActionResult Get(int Id)
        {

           var wordCounts = booksService.GetWordCounts(Id);
            if (wordCounts != null)
            {
              return  Json(wordCounts); 
            }
            return  NotFound();
        }
        /// <summary>
        ///  search words get api
        /// </summary>
        /// <param name="bookId"></param> Book ID
        /// <param name="searchQuery"></param> Word to search in the book
        /// <returns></returns>
        public IHttpActionResult Get(int Id, [FromUri] string searchQuery)
        {
            if (searchQuery.Length >= 3)
            {
                var wordCounts = booksService.SearchWordCounts(Id, searchQuery);
                if (wordCounts == null || wordCounts.Count() == 0)
                {
                    return NotFound();
                }
                return Json(wordCounts);
            }
            else
                return StatusCode(HttpStatusCode.LengthRequired);                
            
        }
    }
}
