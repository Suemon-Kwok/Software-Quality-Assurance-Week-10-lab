using Microsoft.VisualStudio.TestTools.UnitTesting;
using QualityLab;

namespace QualityLab.Tests
{
    [TestClass]
    public class OrderServiceTests
    {
        // ---------- Part 4: initial (weak) test ----------

        [TestMethod]
        public void ApplyDiscount_WhenPriceIs100AndDiscountIs20_Returns80()
        {
            // Arrange
            var service = new OrderService();

            // Act
            var result = service.ApplyDiscount(100m, 20m);

            // Assert
            Assert.AreEqual(80m, result);
        }

        // ---------- Part 5.1: test that exposes the discount defect ----------

        [TestMethod]
        public void ApplyDiscount_WhenPriceIs200AndDiscountIs20_Returns160()
        {
            // Arrange
            var service = new OrderService();

            // Act
            var result = service.ApplyDiscount(200m, 20m);

            // Assert
            Assert.AreEqual(160m, result);
        }

        // ---------- Part 5.2: discount boundary tests ----------

        [TestMethod]
        public void ApplyDiscount_WhenDiscountIsZero_ReturnsOriginalPrice()
        {
            var service = new OrderService();

            var result = service.ApplyDiscount(100m, 0m);

            Assert.AreEqual(100m, result);
        }

        [TestMethod]
        public void ApplyDiscount_WhenDiscountIsOneHundred_ReturnsZero()
        {
            var service = new OrderService();

            var result = service.ApplyDiscount(100m, 100m);

            Assert.AreEqual(0m, result);
        }

        // ---------- Part 5.3: invalid input tests ----------

        [TestMethod]
        public void ApplyDiscount_WhenPriceIsNegative_ThrowsArgumentException()
        {
            var service = new OrderService();

            Assert.ThrowsException<System.ArgumentException>(() =>
                service.ApplyDiscount(-1m, 10m));
        }

        [TestMethod]
        public void ApplyDiscount_WhenDiscountIsNegative_ThrowsArgumentException()
        {
            var service = new OrderService();

            Assert.ThrowsException<System.ArgumentException>(() =>
                service.ApplyDiscount(100m, -5m));
        }

        [TestMethod]
        public void ApplyDiscount_WhenDiscountIsGreaterThan100_ThrowsArgumentException()
        {
            var service = new OrderService();

            Assert.ThrowsException<System.ArgumentException>(() =>
                service.ApplyDiscount(100m, 101m));
        }

        // ---------- Part 5.4: free shipping boundary tests ----------
        // Business rule: free shipping applies when the discounted order total is $100 or more.

        [TestMethod]
        public void IsEligibleForFreeShipping_WhenOrderTotalIs100_ReturnsTrue()
        {
            var service = new OrderService();

            var result = service.IsEligibleForFreeShipping(100m);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsEligibleForFreeShipping_WhenOrderTotalIs99_ReturnsFalse()
        {
            var service = new OrderService();

            var result = service.IsEligibleForFreeShipping(99m);

            Assert.IsFalse(result);
        }

        // ---------- Part 5.5: final total tests ----------

        [TestMethod]
        public void CalculateFinalTotal_WhenDiscountedPriceIsEligibleForFreeShipping_ReturnsTotalWithoutShipping()
        {
            var service = new OrderService();

            var result = service.CalculateFinalTotal(200m, 10m, 15m);

            Assert.AreEqual(180m, result);
        }

        [TestMethod]
        public void CalculateFinalTotal_WhenDiscountedPriceIsNotEligibleForFreeShipping_AddsShipping()
        {
            var service = new OrderService();

            var result = service.CalculateFinalTotal(80m, 10m, 15m);

            Assert.AreEqual(87m, result);
        }

        // ---------- Optional extras (from the Part 3 review of Copilot ideas) ----------

        [TestMethod]
        public void IsEligibleForFreeShipping_WhenOrderTotalIsJustAbove100_ReturnsTrue()
        {
            var service = new OrderService();

            Assert.IsTrue(service.IsEligibleForFreeShipping(100.01m));
        }

        [TestMethod]
        public void CalculateFinalTotal_WhenDiscountedPriceIsExactly100_ShippingIsFree()
        {
            var service = new OrderService();

            // 125 - 20% = 100 exactly -> free shipping applies on the DISCOUNTED total
            var result = service.CalculateFinalTotal(125m, 20m, 15m);

            Assert.AreEqual(100m, result);
        }

        [TestMethod]
        public void CalculateFinalTotal_WhenShippingCostIsNegative_ThrowsArgumentException()
        {
            var service = new OrderService();

            Assert.ThrowsException<System.ArgumentException>(() =>
                service.CalculateFinalTotal(100m, 10m, -1m));
        }
    }
}
