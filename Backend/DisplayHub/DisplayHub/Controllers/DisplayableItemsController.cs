using AppDomain.IRepo_DAL;
using AutoMapper;
using DisplayHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SharedLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisplayHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class DisplayableItemsController : ControllerBase
    {
        private readonly IDisplayItemsRepo itemRepo;

        public DisplayableItemsController(IDisplayItemsRepo itemRepo)
        {
            this.itemRepo = itemRepo;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateItem([FromBody]DisplayableItemModel newItem)
        {
            if(ModelState.IsValid)
            {
                if (newItem != null)
                {//trim the titl etc as people may pass empty strings too
                    var dtoToCreate = Mapper<DisplayableItemModel, DisplayableEntityDTO>(newItem);
                    var createdItem = await itemRepo.CreateItem(dtoToCreate);
                    return CreatedAtAction(nameof(GetItemById), new { id = createdItem.ItemId, controller = "DisplayableItems" }, createdItem.ItemId);
                }
                return BadRequest("An empty item was passed which is not allowed");
            }
            return BadRequest("Incorrect/Incomplete Item Details");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemById([FromRoute] int id)
        {
            var requestedItem = await itemRepo.GetItemById(id);
            if(requestedItem!=null)
            {
                return Ok(requestedItem);// we are sending a dto here so the Id also goes with it because we can use it on frontend
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPublicItems()
        {

            var itemDtos = await itemRepo.GetAllPublicItems();

            if(itemDtos!=null)
            {

            return Ok(itemDtos);
            }
            return NotFound();
        }

        [HttpGet("for")]
        public async Task<IActionResult> GetItemsByUserEmail([FromQuery]string username)
        {
            var dtoList = await itemRepo.GetItemsByUsername(username);
            if(dtoList!=null)
            {
                return Ok(dtoList);
            }
            return NotFound();
        }
        [HttpPatch("like/{id}")]
        public async Task<IActionResult> LikeItem([FromRoute]int id, [FromBody] JsonPatchDocument newLikesCount)
        {
            var updatedLikesCount = await itemRepo.LikeItem(id, newLikesCount);
            if(updatedLikesCount>=0)
            { 
            return Ok(updatedLikesCount);
            }
            return BadRequest();
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
