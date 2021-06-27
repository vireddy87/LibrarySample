using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class Books
    {
        private string _bookName = string.Empty;
        public int Id { get; set; }
        public string BookName
        {
            get
            {
                return _bookName;
            }
            set
            {
                Location = value;
                _bookName = Path.GetFileNameWithoutExtension(value);
            }
        }
        internal string Location
        {
            get;
            private set;
        }
    }

}