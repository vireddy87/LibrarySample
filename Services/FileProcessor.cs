using Library.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library.Data
{
    public class FileProcessor
    {
        private readonly string _fielPath = string.Empty;
        public FileProcessor(string filePath)
        {
            _fielPath = filePath;
        }

        /// <summary>
        /// Method which get all the books from given path
        /// </summary>
        /// <returns>list of bookd</returns>
        public IEnumerable<Books> GetBookNames()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _fielPath);
            int listIndex = 1;
            List<Books> files = new List<Books>();
            foreach (var file in Directory.EnumerateFiles(filePath))
            {
                files.Add(new Books { Id = listIndex, BookName = file });
                listIndex++;
            }
            return files;
        }

        /// <summary>
        /// Get the words and count per book Id
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public IEnumerable<WordCount> GetWordCounts(int bookId)
        {

            List<WordCount> wordsCount = new List<WordCount>();
            try
            {
                //get source array
                 string[] source = GetSourceArray(bookId);
              
                string filePath = GetBookNames().FirstOrDefault(file => file.Id == bookId).Location;
             
                var result = new ConcurrentDictionary<string, int>();
                Parallel.ForEach(File.ReadLines(filePath, Encoding.UTF8), fileLine =>
                {
                    fileLine = new Regex("[^a-zA-Z0-9]").Replace(fileLine, " ");
                    var words = fileLine.ToLowerInvariant().Split(new[] { ' ', }, StringSplitOptions.RemoveEmptyEntries);

                    //check if lenght =5 or more
                    foreach (var word in words.Where(str => str.Length >= 5))
                    {
                        result.AddOrUpdate(word, 1, (_, i) => i + 1);
                    }
                });

                //now diaply only 10
                foreach (var keyvalue in result.OrderByDescending(x => x.Value).Take(10))
                    wordsCount.Add(new WordCount(ConvertCase(keyvalue.Key.ToString()), keyvalue.Value));

            }
            catch (Exception ex)
            {
                
            }
            return wordsCount;
        }


        /// <summary>
        /// search string with count
        /// </summary>
        /// <param name="bookId"></param>
        /// <param name="searchString"></param>
        /// <returns>list word count</returns>
        public IEnumerable<WordCount> SearchWordCounts(int bookId, string searchString)
        {
            List<WordCount> words = new List<WordCount>();
            try
            {
                string[] source = GetSourceArray(bookId);
                var matchQuery = source.Where(r => r.StartsWith(searchString));
                List<string> filteredData = matchQuery.Distinct().Select(x => x).ToList();
                foreach (var data in filteredData)
                {
                    var match = from word in source
                                where word.ToLowerInvariant() == data.ToString().ToLowerInvariant()
                                select word;
                    //covert the first letter to caps and add to collection
                    words.Add(new WordCount(ConvertCase(data.ToString()),match.Count()));
                }
            }
            catch (Exception ex)
            {
                //Log Error
            }
            return words;
        }

        private string[] GetSourceArray(int bookId)
        {
            string filePath = GetBookNames().FirstOrDefault(file => file.Id == bookId).Location;
            string fileText = string.Join(" ", File.ReadLines(filePath).ToArray());
            //remove unwanted charaters
            Regex reg_exp = new Regex("[^a-zA-Z0-9]");
            fileText = reg_exp.Replace(fileText, " ");
            //remove empty entries
            string[] source = fileText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return source;
        }

        public string ConvertCase(string str)
        {
             if (!string.IsNullOrEmpty(str) && str.Length > 1)
            {
                return char.ToUpperInvariant(str[0]) + str.Substring(1).ToLowerInvariant();
            }
            return str;
        }
    }

  
}