using System;

namespace QualityLab
{
    public class OrderService
    {
        public decimal ApplyDiscount(decimal price, decimal discountPercent)
        {
            if (price < 0)
            {
                throw new ArgumentException("Price cannot be negative.");
            }

            if (discountPercent < 0 || discountPercent > 100)
            {
                throw new ArgumentException("Discount must be between 0 and 100.");
            }

            return price - (price * discountPercent / 100);
        }

        public bool IsEligibleForFreeShipping(decimal orderTotal)
        {
            return orderTotal >= 100;
        }

        public decimal CalculateFinalTotal(decimal price, decimal discountPercent, decimal shippingCost)
        {
            if (shippingCost < 0)
            {
                throw new ArgumentException("Shipping cost cannot be negative.");
            }

            var discountedPrice = ApplyDiscount(price, discountPercent);

            if (IsEligibleForFreeShipping(discountedPrice))
            {
                shippingCost = 0;
            }

            return discountedPrice + shippingCost;
        }
    }
}
