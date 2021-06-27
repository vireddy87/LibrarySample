using Library.Data;
using Library.Models;
using System.Collections.Generic;

namespace Library.Repository
{
    public class BooksRepository
    {
        private readonly string _location = @"Resources";
        FileProcessor FileProcessor;
        /// <summary>
        /// book repository constructor
        /// </summary>
        public BooksRepository()
        {
            FileProcessor = new FileProcessor(_location);
        }
        /// <summary>
        /// Parameterized Constructor
        /// </summary>
        /// <param name="filePath"></param> File path of the text files
        public BooksRepository(string filePath)
        {
            FileProcessor = new FileProcessor(filePath);
        }
        /// <summary>
        /// Method to get book names
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Books> GetBookNames()
        {
            return FileProcessor.GetBookNames();
        }
        /// <summary>
        /// get word counts per book
        /// </summary>
        /// <param name="Id"></param> ID of the Book
        /// <returns></returns>
        public IEnumerable<WordCount> GetWordCounts(int id)
        {
            return FileProcessor.GetWordCounts(id);
        }
        /// <summary>
        /// get words per book by search word
        /// </summary>
        /// <param name="id"></param> Book Id
        /// <param name="searchWord"></param> Word to search in the book
        /// <returns></returns>
        public IEnumerable<WordCount> SearchWordCounts(int id, string searchWord)
        {
            return FileProcessor.SearchWordCounts(id, searchWord);
        }
    }
}