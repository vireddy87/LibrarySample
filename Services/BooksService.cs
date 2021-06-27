using Library.Models;
using Library.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Service
{
    public class BooksService
    {
        BooksRepository booksRepository;
        public IEnumerable<Books> GetBookNames()
        {
         
            return booksRepository.GetBookNames();
        }
        public BooksService(string filePath)
        {
            booksRepository = new BooksRepository(filePath);
        }
        public BooksService()
        {
            booksRepository = new BooksRepository();
        }
        /// <summary>
        /// get word counts per book
        /// </summary>
        /// <param name="Id"></param> ID of the Book
        /// <returns></returns>
        public IEnumerable<WordCount> GetWordCounts(int bookId)
        {
            return booksRepository.GetWordCounts(bookId);
        }
        /// <summary>
        /// get words per book by search word
        /// </summary>
        /// <param name="bookId"></param> Book Id
        /// <param name="searchWord"></param> Word to search in the book
        /// <returns></returns>
        public IEnumerable<WordCount> SearchWordCounts(int bookId, string searchWord)
        {
            return booksRepository.SearchWordCounts(bookId, searchWord);
        }
    }
}