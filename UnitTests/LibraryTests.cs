using Library.Repository;
using Library.Service;
using NUnit.Framework;
using System.Linq;

namespace Library.Tests
{
    [TestFixture]
    public class LibraryTests
    {
      private BooksService _service;
        /// <summary>
        /// Constructor
        /// </summary>
        public LibraryTests()
        {
            _service = new BooksService(@"UnitTests\\Resources");
        }
        /// <summary>
        /// SearchForBookName
        /// </summary>
        [Test]

        public void SearchForBookName()
        {
            var bookNames = _service.GetBookNames();
      
            Assert.AreEqual(1, bookNames.Count());
            Assert.AreEqual("UnitTest", bookNames.FirstOrDefault().BookName);
        }
        /// <summary>
        /// Invalid Book Id Test
        /// </summary>
        [Test]
        public void CheckForBookIdNotFound()
        {
            var results = _service.GetWordCounts(2);
            Assert.AreEqual(0, results.Count());
        }
        /// <summary>
        /// check for 10 words
        /// </summary>
        [Test]
        public void CheckServiceReturnTenWords()
        {
            var searchResult = _service.GetWordCounts(1);
            Assert.IsNotNull(searchResult);
            Assert.AreEqual(10, searchResult.Count());
        }
        /// <summary>
        /// seach with any of the keywords
        /// </summary>
        [Test]
        public void FindWordThatExistsAmdCheckForCount()
        {
            var words = _service.SearchWordCounts(1, "paria");
            Assert.IsNotNull(words);
            Assert.IsTrue(words.All(search => search.Word.Contains("Paria")));
            Assert.AreEqual(1, words.Count());
        }
  
        [Test]
        public void CheckForNoSearchWordFound()
        {
            var words = _service.SearchWordCounts(1, "unitestdata");
            Assert.IsNotNull(words);
            Assert.AreEqual(0, words.Count());
        }
      
    }
}