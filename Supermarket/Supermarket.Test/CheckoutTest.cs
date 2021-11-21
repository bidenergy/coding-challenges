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

            var productA = new Product("A", 50);
            var productB = new Product("B", 30);
            var productC = new Product("C", 20);
            var productD = new Product("D", 15);

            mockRepo.Setup(r => r.LookupProduct("A")).Returns(productA);
            mockRepo.Setup(r => r.LookupProduct("B")).Returns(productB);
            mockRepo.Setup(r => r.LookupProduct("C")).Returns(productC);
            mockRepo.Setup(r => r.LookupProduct("D")).Returns(productD);

            _checkout = new Checkout(mockRepo.Object);
        }

        [Test]
        public void NoScan()
        {
            var price = _checkout.GetTotalPrice();

            Assert.That(price, Is.EqualTo(0));
        }

        [Test]
        public void UpsupportedProduct_MakesNoDiff()
        {
            _checkout.Scan("Z");

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
        public void ScanAllExampleProducts()
        {
            _checkout.Scan("A");
            _checkout.Scan("B");
            _checkout.Scan("C");
            _checkout.Scan("D");

            var price = _checkout.GetTotalPrice();

            Assert.That(price, Is.EqualTo(115));
        }
    }
}
