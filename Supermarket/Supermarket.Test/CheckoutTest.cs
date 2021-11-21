using Moq;
using NUnit.Framework;
using Supermarket.Logic;
using Supermarket.Logic.Data;

namespace Supermarket.Test
{
    public class CheckoutTest
    {
        private ICheckout _checkout;

        [SetUp]
        public void Setup()
        {
            var mockRepo = new Mock<IDataRepository>();

            mockRepo.Setup(r => r.LookupProduct("A")).Returns(new Product("A", 50));

            _checkout = new Checkout(mockRepo.Object);
        }

        [Test]
        public void Test()
        {
            _checkout.Scan("A");
            var price = _checkout.GetTotalPrice();

            Assert.That(price, Is.EqualTo(50));
        }
    }
}