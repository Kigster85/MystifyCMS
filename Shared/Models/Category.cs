﻿using System.ComponentModel.DataAnnotations;

namespace Shared.Models
{
    internal class Category
    {
        [Key]

        public int CatergoyId { get; set; }

        [Required]
        [MaxLength(256)]

        public string ThumbnailImagePath { get; set; }

        [Required]
        [MaxLength(128]

        public string Name { get; set; }

        [Required]
        [MaxLength(1024)]
        public string Description { get; set; }


    }
}
