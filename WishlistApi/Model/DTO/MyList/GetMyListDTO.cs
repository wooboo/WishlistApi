using System;

namespace WishlistApi.Model.DTO.MyList
{
    public class GetMyListDTO : MyListDTO
    {
        public Uri Uri { get; set; }
        public Uri WishesUri { get; set; }
    }
}