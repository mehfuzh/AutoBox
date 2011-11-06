using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Should;
using AutoBox.Specification.Repositories;
using AutoBox.Specification.Controllers;
using Microsoft.Practices.ServiceLocation;

namespace AutoBox.Specification
{
    [TestFixture]
    public class AutoBoxSpecs
    {
        [SetUp]
        public void BeforeEach()
        {
            Container.Init();
        }
   
        [Test]
        public void ShouldBeAbleToCacheWhenTimeSpanIsSpecifiedInOtherThanSeconds()
        {
            string product = Guid.NewGuid().ToString();
            int quantity = new Random().Next(1, 100);

            Container.Setup<ProductRepository>(x => x.Create(product, quantity)).Caches(TimeSpan.FromMinutes(1));

            var result = ServiceLocator.Current.GetInstance<ProductController>();

            var source = result.Create(product, quantity).Id;
            var expected = result.Create(product, quantity).Id;

            source.ShouldEqual(expected);
        }

        [Test]
        public void ShouldThrowProperExceptionWhenTargetTypeForInterfaceNotFound()
        {
           string expectedMessage = string.Format("Failed to resolve the corresponding type for {0}", typeof(IOrphanService).Name);
           string actual = Assert.Throws<ActivationException>(() => ServiceLocator.Current.GetInstance<IOrphanService>()).Message;
           actual.ShouldEqual(expectedMessage);
        }

        public interface IOrphanService
        {
        }

        [Test]
        public void ShouldCacheWhenNullValuedArgumentIsSpecified()
        {
            Container.Setup<RandomService>(x => x.Next(null, null)).Caches(TimeSpan.FromMinutes(1));

            var service = ServiceLocator.Current.GetInstance<IRandomService>();
            
            service.Next(null, null).ShouldEqual(service.Next(null, null));
        }

        public interface IRandomService
        {
            int Next(int? minValue, int? maxValue);
        }

        public class RandomService : IRandomService
        {
            public int Next(int? minValue, int? maxValue)
            {
                if (minValue == null) minValue = 0;
                if (maxValue == null) maxValue = int.MaxValue;
                return new Random().Next(minValue.Value, maxValue.Value);
            }
        }
    }
}
