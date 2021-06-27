using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class WordCount
    {
        public string Word { get; set; }
        public int Count { get; set; }
        public WordCount(string word, int count)
        {
            Word = word;
            Count = count;
        }
    }
}