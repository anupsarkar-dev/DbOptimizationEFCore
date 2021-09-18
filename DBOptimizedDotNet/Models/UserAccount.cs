using System;
using System.Collections.Generic;

#nullable disable

namespace DBOptimizedDotNet.Models
{
    public partial class UserAccount
    {
        public string Uid { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
