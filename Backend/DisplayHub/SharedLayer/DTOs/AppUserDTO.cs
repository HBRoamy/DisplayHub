using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedLayer.DTOs
{
    public class AppUserDTO
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public string CategoriesLiked { get; set; } // when its null show all
        public string CategoriesCreated { get; set; }
        public string SavedPostsIds { get; set; }
    }
}
