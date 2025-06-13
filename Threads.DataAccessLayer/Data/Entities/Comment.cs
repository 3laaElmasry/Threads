﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Threads.DataAccessLayer.Data.Entities
{

    public class Comment
    {
        public Guid CommentId { get; set; }

        public string Text { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public Guid AuthorId { get; set; }

        public Guid PostId { get; set; }

        public Guid? ParentId { get; set; }

        public int Replys { get; set; } = 0;
        public ApplicationUser? Author { get; set; }
        public Post? Post { get; set; }
        public Comment? Parent { get; set; }

    }
}
