using Microsoft.AspNetCore.JsonPatch;
using SharedLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppDomain.IRepo_DAL
{
    public interface IDisplayItemsRepo
    {
        Task<DisplayableEntityDTO> CreateItem(DisplayableEntityDTO newItem);
        Task<DisplayableEntityDTO> GetItemById(int id);
        Task<IEnumerable<DisplayableEntityDTO>> GetAllPublicItems();
        Task<IEnumerable<DisplayableEntityDTO>> GetItemsByUsername(string username);
        Task<int> LikeItem(int id, JsonPatchDocument newLikesCount);
    }
}
