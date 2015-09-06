using System;

namespace WishlistApi.Model.DTO.MyListWish
{
    public class QueryMyListWishDTO : MyListWishDTO
    {
        public Uri Uri { get; set; }
        public Uri ListUri { get; set; }
    }
}