﻿using Cosmetics.Enum;

namespace Cosmetics.DTO.Order
{
    public class OrderCreateDTO
    {
        public int? CustomerId { get; set; }
        public int? SalesStaffId { get; set; }
        public Guid? AffiliateProfileId { get; set; }
        public decimal? TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime? OrderDate { get; set; }
        public string PaymentMethod { get; set; }
    }

}