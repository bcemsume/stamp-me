using System;
using System.Collections.Generic;

namespace StampMe.Common.CustomDTO
{
    public class RestaurantQuickSaveDTO
    {
        public string Id
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public List<CategoriesDTO> Categories { get; set; }
        public List<PromotionDTO> Promotion { get; set; }
        public List<ProductDTO> Product { get; set; }
        public object ContractId { get; set; }

        public string Email
        {
            get;
            set;
        }
        public string Password
        {
            get;
            set;
        }
        public string UserName
        {
            get;
            set;
        }
        public string Adress
        {
            get;
            set;
        }
        public bool isPromo
        {
            get;
            set;
        }
        public bool isActive
        {
            get;
            set;
        }
    }
}
