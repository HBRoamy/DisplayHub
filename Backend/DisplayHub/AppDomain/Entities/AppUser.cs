using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AppDomain.Entities
{
    public class AppUser : IdentityUser
    {

        [Required]
        public string DisplayName { get; set; }
        public string CategoriesLiked { get; set; } // when its null show all
        public string CategoriesCreated { get; set; }

        //[ForeignKey("ItemId")]
        //public ICollection<DisplayableItemEntity> SavedPosts { get; set; }

        // Add Age also FOR NSFW TAGS
        public string SavedPostsIds { get; set; }

        //[ForeignKey("Id")]
        //public ICollection<AppUser> ListOfFollowers { get; set; }
        //public ICollection<AppUser> PeopleYouFollow { get; set; }

        //public int TotalItemsCreated { get; set; } = 0;

        //public int Rank { get; set; } = 0;

        //public int TotalItemsValue { get; set; } = 0; //its like reddit's karma

        //public bool IsVerified { get; set; } = false;

        //public int UserStatus { get; set; } = 0; //rookie user is 0, 1 is bronze user, 2 is silver, 3 is gold, 4 plat, 5 diamond user 

        //totalFollowers

    }
}
