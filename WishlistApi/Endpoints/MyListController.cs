﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Routing;
using MongoDB.Driver;
using Owin;
using WishlistApi.Infrastructure;
using WishlistApi.Model;
using WishlistApi.Model.DataAccess;
using WishlistApi.Model.DTO;
using WishlistApi.Model.DTO.MyList;

namespace WishlistApi.Endpoints
{
    /// <summary>
    /// Managing lists where user is an owner
    /// </summary>
    //[Authorize]
    [InheritedRoute("api/MyList/{id?}")]
    public class MyListController : ApiController
    {
        private readonly IRepository<List> _repository;
        private readonly ResourceUriHelper _resourceUriHelper;
        private readonly DTOMapper _dtoMapper;
        private readonly IUserIdProvider _userIdProvider;

        /// <summary>
        /// Initializes controller
        /// </summary>
        public MyListController()
        {
            var client = new MongoClient();

            _repository = new MongoRepository<List>("List", client.GetDatabase("wishlist"));
            _resourceUriHelper = new ResourceUriHelper(this);
            _dtoMapper = new DTOMapper();
            _userIdProvider = new FakeUserIdProvider();
        }

        /// <summary>
        /// Creates a list
        /// </summary>
        /// <param name="myListDTO">Basig data of the list</param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Post(CreateMyListDTO myListDTO)
        {
            var list = _dtoMapper.Map(myListDTO).To<List>();
            list.Owners.Add(_userIdProvider.GetUserId());
            //list.Id = ToUrlSlug(myListDTO.Name);
            await _repository.AddAsync(list);
            return Ok();
        }

        public async Task<IHttpActionResult> Put(string id, UpdateMyListDTO myListDTO)
        {
            var item = await _repository.GetAsync(id);
            if (!item.Owners.Contains(_userIdProvider.GetUserId()))
            {
                return NotFound();
            }
            _dtoMapper.Map(myListDTO).To(item);
            await _repository.UpdateAsync(id, item);
            return Ok();
        }

        public async Task<IHttpActionResult> Delete(string id)
        {
            var item = await _repository.GetAsync(id);
            if (!item.Owners.Contains(_userIdProvider.GetUserId()))
            {
                return NotFound();
            }
            await _repository.DeleteAsync(id);
            return Ok();
        }

        public async Task<IHttpActionResult> Get()
        {
            var username = _userIdProvider.GetUserId();
            var result = await _repository.QueryAsync(o => o.Owners.Any(u => u == username));

            var listDtos = result.Select(item => _dtoMapper.Map(item).To<QueryMyListDTO>((s, d) =>
            {
                d.Uri = _resourceUriHelper.GetResourceUri<MyListController>(new {id = s.Id});
                d.WishesUri = _resourceUriHelper.GetResourceUri<MyListWishController>(new {listId = s.Id});
            })).ToList();
            return Ok(listDtos);
        }

        public async Task<IHttpActionResult> Get(string id)
        {
            var username = _userIdProvider.GetUserId();
            var item = await _repository.GetAsync(id);
            if (!item.Owners.Contains(_userIdProvider.GetUserId()))
            {
                return NotFound();
            }
            var listDTO = _dtoMapper.Map(item).To<GetMyListDTO>((s, d) =>
            {
                d.Uri = _resourceUriHelper.GetResourceUri<MyListController>(new { id = s.Id });
                d.WishesUri = _resourceUriHelper.GetResourceUri<MyListWishController>(new { listId = s.Id });
            });
            return Ok(listDTO);
        }

       
    }
}