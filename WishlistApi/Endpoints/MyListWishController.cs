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
    [InheritedRoute("api/MyList/{listId}/Wish/{name?}")]
    public class MyListWishController : ApiController
    {
        private readonly IRepository<WishList> _repository;
        private readonly ResourceUriHelper _resourceUriHelper;
        private readonly DTOMapper _dtoMapper;
        private readonly IUserIdProvider _userIdProvider;
        private readonly SlugGenerator _slugGenerator;

        public MyListWishController()
        {
            var client = new MongoClient();

            _repository = new MongoRepository<WishList>("List", client.GetDatabase("wishlist"));
            _resourceUriHelper = new ResourceUriHelper(this);
            _dtoMapper = new DTOMapper();
            _userIdProvider = new FakeUserIdProvider();
            _slugGenerator = new SlugGenerator();
        }
        public async Task<IHttpActionResult> Post(string listId, CreateMyListWishDTO myListWishDTO)
        {
            var list = await _repository.GetAsync(listId);
            var wish = _dtoMapper.Map(myListWishDTO).To<Wish>();
            wish.Id = _slugGenerator.ToUrlSlug(myListWishDTO.Name);
            list.Wishes.Add(wish);
            await _repository.UpdateAsync(listId, list);
            return Ok();
        }
        public async Task<IHttpActionResult> Put(string listId, string name, UpdateMyListWishDTO myListWishDTO)
        {
            var list = await _repository.GetAsync(listId);
            var wish = list.Wishes.Single(o => o.Id == name);
            _dtoMapper.Map(myListWishDTO).To<Wish>(wish);
            await _repository.UpdateAsync(name, list);
            return Ok();
        }
        public async Task<IHttpActionResult> Delete(string listId, string name)
        {
            var list = await _repository.GetAsync(listId);
            var wish = list.Wishes.Single(o => o.Id == name);
            list.Wishes.Remove(wish);
            await _repository.UpdateAsync(name, list);
            return Ok();
        }
        public async Task<IHttpActionResult> Get(string listId)
        {
            var list = await _repository.GetAsync(listId);

            var listDtos = list.Wishes.Select(item => _dtoMapper.Map(item).To<QueryMyListWishDTO>((s, d) =>
            {
                d.Uri = _resourceUriHelper.GetResourceUri<MyListWishController>(new { listId = listId, name = s.Id });
                d.ListUri = _resourceUriHelper.GetResourceUri<MyListController>(new { id = listId });
            })).ToList();
            return Ok(listDtos);
        }


        public async Task<IHttpActionResult> Get(string listId, string name)
        {
            var list = await _repository.GetAsync(listId);
            var wish = list.Wishes.Single(o => o.Id == name);
            await _repository.UpdateAsync(name, list);
            return Ok(_dtoMapper.Map(wish).To<GetMyListWishDTO>((s, d) =>
            {
                d.Uri = _resourceUriHelper.GetResourceUri<MyListWishController>(new { listId = listId, name = s.Id });
                d.ListUri = _resourceUriHelper.GetResourceUri<MyListController>(new { id = listId });
            }));
        }


    }
}