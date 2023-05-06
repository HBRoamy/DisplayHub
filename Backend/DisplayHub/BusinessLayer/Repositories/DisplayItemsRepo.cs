using AppDomain.DB_Context;
using AppDomain.Entities;
using AppDomain.IRepo_DAL;
using AppDomain.Repo_DAL;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using SharedLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Repositories
{
    public class DisplayItemsRepo : IDisplayItemsRepo
    {
        private readonly IAppGenericRepo<DisplayableItemEntity> itemRepo;

        public DisplayItemsRepo(AppDbContext context)
        {
            this.itemRepo = new AppGenericRepo<DisplayableItemEntity>(context);
        }

        public async Task<DisplayableEntityDTO> CreateItem(DisplayableEntityDTO newItem)
        {
            DisplayableEntityDTO sendableDTO = null;
            if (newItem != null)
            {
                var itemEntity = Mapper<DisplayableEntityDTO, DisplayableItemEntity>(newItem);
                var receivedItem = await itemRepo.CreateAsync(itemEntity);
                sendableDTO = Mapper<DisplayableItemEntity, DisplayableEntityDTO>(receivedItem);
            }
            return sendableDTO;

        }

        public async Task<IEnumerable<DisplayableEntityDTO>> GetAllPublicItems()
        {
            var list = await itemRepo.FilterAsync(x => x.IsPrivate == false);
            IEnumerable<DisplayableEntityDTO> dtoList = null;
            if (list != null)
            {
                dtoList = MapperEnumerable<DisplayableItemEntity, DisplayableEntityDTO>(list);
            }
            return dtoList;
        }

        public async Task<DisplayableEntityDTO> GetItemById(int id)
        {
            var entity = await itemRepo.GetByIdAsync(id);
            DisplayableEntityDTO dto = null;
            if(entity!=null)
            {
                dto = Mapper<DisplayableItemEntity,DisplayableEntityDTO>(entity);
            }
           return dto;
        }

        public async Task<int> LikeItem( int id, JsonPatchDocument newLikesCount)
        {
            var itemToLike = await itemRepo.GetByIdAsync(id);
            if(itemToLike!=null)
            {
               newLikesCount.ApplyTo(itemToLike);
                await itemRepo.SaveAsync();
                return itemToLike.LikesCounter;
            }
            return -1;
        }
        public async Task<IEnumerable<DisplayableEntityDTO>> GetItemsByUsername(string username)
        {
            var receivedData = await itemRepo.FilterAsync(x => x.Creater == username.ToLower());
            IEnumerable<DisplayableEntityDTO> dtoList = null;
            if (receivedData!=null)
            {
                dtoList = MapperEnumerable<DisplayableItemEntity, DisplayableEntityDTO>(receivedData);
            }
            return dtoList;
        }

        TDestination Mapper<TSource, TDestination>(TSource model)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>());
            var mapper = config.CreateMapper();
            var eventEntity = mapper.Map<TSource, TDestination>(model);
            return eventEntity;
        }
        IEnumerable<TDestination> MapperEnumerable<TSource, TDestination>(IEnumerable<TSource> model)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>());
            var mapper = config.CreateMapper();
            var eventEntities = mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(model);
            return eventEntities;
        }

    }
}
