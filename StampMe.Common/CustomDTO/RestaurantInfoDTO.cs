using System;
namespace StampMe.Common.CustomDTO
{
    public class RestaurantInfoDTO
    {
        public object Id { get; set; }
        public string Phone { get; set; }
        public string WorkingDays { get; set; }
        public string PaymentTypes { get; set; }
        public string WorkingHours { get; set; }
    }
}
