using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using MongoDB.Driver;
using Owin;
using WishlistApi.Infrastructure;
using WishlistApi.Model;
using WishlistApi.Model.DataAccess;
using WishlistApi.Model.Domain;
using WishlistApi.Model.DTO;
using WishlistApi.Model.DTO.MyListWish;

namespace WishlistApi.Endpoints
{
    //[Authorize]
    [InheritedRoute("api/MyList/{listId}/Wish/{wishId?}")]
    public class MyListWishController : ApiController
    {
        private readonly IRepository<WishList> _repository;
        private readonly IIdGenerator<Wish> _idGenerator;
        private readonly DTOMapper _dtoMapper;
        private readonly IUserIdProvider _userIdProvider;

        public MyListWishController(DTOMapper dtoMapper, IUserIdProvider userIdProvider, 
            IRepository<WishList> repository, IIdGenerator<Wish> idGenerator )
        {
            _dtoMapper = dtoMapper;
            _userIdProvider = userIdProvider;
            _repository = repository;
            _idGenerator = idGenerator;
        }
        public async Task<IHttpActionResult> Post(string listId, CreateMyListWishDTO myListWishDTO)
        {
            var list = await _repository.GetAsync(listId);
            var wish = _dtoMapper.Map(myListWishDTO).To<Wish>();
            _idGenerator.GenerateId(wish);
            list.Wishes.Add(wish);
            await _repository.UpdateAsync(listId, list);
            return Ok();
        }
        public async Task<IHttpActionResult> Put(string listId, string wishId, UpdateMyListWishDTO myListWishDTO)
        {
            var list = await _repository.GetAsync(listId);
            var wish = list.Wishes.Single(o => o.Id == wishId);
            _dtoMapper.Map(myListWishDTO).To<Wish>(wish);
            await _repository.UpdateAsync(wishId, list);
            return Ok();
        }
        public async Task<IHttpActionResult> Delete(string listId, string wishId)
        {
            var list = await _repository.GetAsync(listId);
            var wish = list.Wishes.Single(o => o.Id == wishId);
            list.Wishes.Remove(wish);
            await _repository.UpdateAsync(wishId, list);
            return Ok();
        }
        public async Task<IHttpActionResult> Get(string listId)
        {
            var list = await _repository.GetAsync(listId);

            var listDtos = list.Wishes.Select(item => _dtoMapper.Map(item).To<QueryMyListWishDTO>((s, d) =>
            {
                d.Uri = Url.GetResourceUri<MyListWishController>(new { listId = listId, wishId = s.Id });
                d.ListUri = Url.GetResourceUri<MyListController>(new { id = listId });
            })).ToList();
            return Ok(listDtos);
        }


        public async Task<IHttpActionResult> Get(string listId, string wishId)
        {
            var list = await _repository.GetAsync(listId);
            var wish = list.Wishes.Single(o => o.Id == wishId);
            await _repository.UpdateAsync(wishId, list);
            return Ok(_dtoMapper.Map(wish).To<GetMyListWishDTO>((s, d) =>
            {
                d.Uri = Url.GetResourceUri<MyListWishController>(new { listId = listId, wishId = s.Id });
                d.ListUri = Url.GetResourceUri<MyListController>(new { id = listId });
            }));
        }


    }
}