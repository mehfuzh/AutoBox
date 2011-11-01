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
    public class ConditionalCacheSpecs
    {
        [SetUp]
        public void BeforeEach()
        {
            AutoBox.Init();
        }

        [Test]
        public void ShouldInvalidateCacheWhenArgumentsAreDifferent()
        {
            var product = "x";

            AutoBox.Setup<ProductRepository>(x => x.Create(product, 1)).Caches(TimeSpan.FromSeconds(5));

            var repository = ServiceLocator.Current.GetInstance<ProductController>();

            var expected =  repository.Create(product, 1).Id;

            repository.Create(product, 1).Id.ShouldEqual(expected);
            repository.Create(product, 2).Id.ShouldNotEqual(expected);
        }

        [Test]
        public void ShouldInvalidateCacheWhenTargetMethodWithSpecificArgumentsIsInvoked()
        {
            AutoBox.Setup<ProductRepository>(x => x.GetAll()).Caches(TimeSpan.FromSeconds(10));
            AutoBox.Setup<ProductRepository>(x => x.Create("x", 1)).Invalidates(x => x.GetAll());

            var controller = ServiceLocator.Current.GetInstance<ProductController>();

            int count = controller.GetAllProducts().Count;

            controller.GetAllProducts().Count.ShouldEqual(count);
         
            controller.Create("x", 1);

            controller.GetAllProducts().Count.ShouldNotEqual(count);
        }

        [Test]
        public void ShouldSkipInvalidaitonWhenTargetIsInvokedWithDifferentArgs()
        {
            AutoBox.Setup<ProductRepository>(x => x.GetAll()).Caches(TimeSpan.FromSeconds(10));
            AutoBox.Setup<ProductRepository>(x => x.Create("x", 1)).Invalidates(x => x.GetAll());

            var repository = ServiceLocator.Current.GetInstance<ProductController>();

            int count = repository.GetAllProducts().Count;

            repository.GetAllProducts().Count.ShouldEqual(count);

            repository.Create("y", 1);

            repository.GetAllProducts().Count.ShouldEqual(count);
        }

        [Test]
        public void ShouldSetCacheFromPointWhenInvalidated()
        {
            string product = Guid.NewGuid().ToString();

            AutoBox.Setup<ProductRepository>(x => x.GetAll()).Caches(TimeSpan.FromSeconds(10));
            AutoBox.Setup<ProductRepository>(x => x.Create(product, 1)).Invalidates(x => x.GetAll());

            var repository = ServiceLocator.Current.GetInstance<ProductController>();

            int count = repository.GetAllProducts().Count;
            repository.GetAllProducts().Count.ShouldEqual(count);
            
            repository.Create(product, 1);

            count = 1;

            repository.GetAllProducts().Count.ShouldEqual(count);

            Store.Add(new Product());

            // should still return cached item.
            repository.GetAllProducts().Count.ShouldEqual(count);
        }

        [TearDown]
        public void RemoveAllData()
        {
            Store.Clear();
        }
    }
}
