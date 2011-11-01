using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Should;
using System.Threading;
using AutoBox.Specification.Repository;
using AutoBox.Specification.Controllers;
using Microsoft.Practices.ServiceLocation;

namespace AutoBox.Specification
{
    [TestFixture]
    public class CachingSpecs
    {
        private const string title = "Talisker";
        public int quantity = 1;

        [SetUp]
        public void BeforeEach()
        {
            Container.Init();
            Container.Setup<ProductRepository>(x => x.Create(title, quantity)).Caches(TimeSpan.FromSeconds(2));
        }
   
        [Test]
        public void ShouldReturnCachedItemWhenQueriedWithInTheSpecifiedCacheDuration()
        {
            var result = ServiceLocator.Current.GetInstance<ProductController>();

            var source = result.Create(title, quantity).Id;
            var expected = result.Create(title, quantity).Id;

            source.ShouldEqual(expected);
        }

        [Test]
        public void ShouldInvalidateWhenCachDurationExpires()
        {
            var result = ServiceLocator.Current.GetInstance<ProductController>();

            var source = result.Create(title, quantity).Id;
       
            Thread.Sleep(TimeSpan.FromSeconds(3));

            var expected = result.Create(title, quantity).Id;

            source.ShouldNotEqual(expected);
        }

        [TearDown]
        public void RemoveAllData()
        {
            Store.Clear();
        }
    }
}
