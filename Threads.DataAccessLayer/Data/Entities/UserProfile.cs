﻿
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Threads.DataAccessLayer.Data.Entities
{
    public class UserProfile
    {
        public string UserId { get; set; } = string.Empty;  // Match JWT's NameIdentifier
        public string? DisplayName { get; set; }
        public string? ImgUrl { get; set; }

        public string? Email { get; set; }

        //public IEnumerable<Tweet> Tweets { get; set; } = new List<Tweet>();
        //public IEnumerable<Comment> comments { get; set; } = new List<Comment>();
    }
}
