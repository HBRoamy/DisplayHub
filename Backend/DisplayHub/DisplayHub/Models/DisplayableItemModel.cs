using System;
using System.ComponentModel.DataAnnotations;

namespace DisplayHub.Models
{
    public class DisplayableItemModel
    {
        [Required]
        [StringLength(30, MinimumLength = 5)]
        public string Title { get; set; }

        [Required]
        public string Creater { get; set; }

        [Required]
        public DateTime CreationTime { get; set; } = DateTime.Now;

        [Required]
        [StringLength(500, MinimumLength = 10)]
        public string Description { get; set; }

        //users allowed should be a list though
        public string UsersAllowedToView { get; set; } // when its not set, it will be null so we can use that to show it to all

        public bool IsPrivate { get; set; } = false;

        public int LikesCounter { get; set; } = 0;
        public int dislikesCounter { get; set; } = 0;

        //[Required]
        [DataType(DataType.ImageUrl)]
        public string ItemImageLink { get; set; }

        public string ItemCategory { get; set; } // funny , info, passtime, hobby, ads

        public string ItemFlair { get; set; } //18+, children, PG-13, NSFW, Spoiler etc...
    }
}
