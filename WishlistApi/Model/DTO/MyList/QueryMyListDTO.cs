using System;

namespace WishlistApi.Model.DTO.MyList
{
    public class QueryMyListDTO : MyListDTO
    {
        public Uri Uri { get; set; }
        public Uri WishesUri { get; set; }
    }
}