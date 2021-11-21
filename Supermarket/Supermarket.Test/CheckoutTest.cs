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
        public void NoScan()
        {
            var price = _checkout.GetTotalPrice();

            Assert.That(price, Is.EqualTo(0));
        }

        [Test]
        public void ScanOnce()
        {
            _checkout.Scan("A");
            var price = _checkout.GetTotalPrice();

            Assert.That(price, Is.EqualTo(50));
        }

        [Test]
        public void ScanSameProductTwice()
        {
            _checkout.Scan("A");
            _checkout.Scan("A");
            var price = _checkout.GetTotalPrice();

            Assert.That(price, Is.EqualTo(100));
        }
    }
}