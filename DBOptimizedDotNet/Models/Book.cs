using System;
using System.Collections.Generic;

#nullable disable

namespace DBOptimizedDotNet.Models
{
    public partial class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Isbn { get; set; }
        public string AuthorName { get; set; }
    }
}
