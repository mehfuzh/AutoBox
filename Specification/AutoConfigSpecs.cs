using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Should;
using AutoBox.Specification.Repository;
using AutoBox.Specification.Controllers;
using Microsoft.Practices.ServiceLocation;

namespace AutoBox.Specification
{
    [TestFixture]
    public class AutoConfigSpecs
    {
        [SetUp]
        public void BeforeEach()
        {
            AutoBox.Init();
        }
   
        [Test]
        public void ShouldBeAbleToCacheWhenTimeSpanIsSpecifiedInOtherThanSeconds()
        {
            string product = Guid.NewGuid().ToString();
            int quantity = new Random().Next(1, 100);

            AutoBox.Setup<ProductRepository>(x => x.Create(product, quantity)).Caches(TimeSpan.FromMinutes(1));

            var result = ServiceLocator.Current.GetInstance<ProductController>();

            var source = result.Create(product, quantity).Id;
            var expected = result.Create(product, quantity).Id;

            source.ShouldEqual(expected);
        }

    }
}
