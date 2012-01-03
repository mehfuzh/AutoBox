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
    public class ConditionalCacheSpecs
    {
        [SetUp]
        public void BeforeEach()
        {
            Container.Init();
        }

        [Test]
        public void ShouldInvalidateCacheWhenArgumentsAreDifferent()
        {
            string product = Guid.NewGuid().ToString();

            Container.Setup<ProductRepository>(x => x.Create(product, 1)).Caches(TimeSpan.FromSeconds(5));

            var repository = ServiceLocator.Current.GetInstance<ProductController>();

            var expected =  repository.Create(product, 1).Id;

            repository.Create(product, 1).Id.ShouldEqual(expected);
            repository.Create(product, 2).Id.ShouldNotEqual(expected);
        }

        [Test]
        public void ShouldInvalidateCacheWhenTargetMethodWithSpecificArgumentsIsInvoked()
        {
            Container.Setup<ProductRepository>(x => x.GetAll()).Caches(TimeSpan.FromSeconds(10));
            Container.Setup<ProductRepository>(x => x.Create("x", 1)).Invalidates(x => x.GetAll());

            var controller = ServiceLocator.Current.GetInstance<ProductController>();

            int count = controller.GetAllProducts().Count;

            controller.GetAllProducts().Count.ShouldEqual(count);
         
            controller.Create("x", 1);

            controller.GetAllProducts().Count.ShouldNotEqual(count);
        }

        [Test]
        public void ShouldSkipInvalidaitonWhenTargetIsInvokedWithDifferentArgs()
        {
            Container.Setup<ProductRepository>(x => x.GetAll()).Caches(TimeSpan.FromSeconds(10));
            Container.Setup<ProductRepository>(x => x.Create("x", 1)).Invalidates(x => x.GetAll());

            var repository = ServiceLocator.Current.GetInstance<ProductController>();

            int count = repository.GetAllProducts().Count;

            repository.GetAllProducts().Count.ShouldEqual(count);

            repository.Create("y", 1);

            repository.GetAllProducts().Count.ShouldEqual(count);
        }

        [Test]
        public void ShouldInvalidateTargetWhenSourcetIsCalledWithSpecificArgument()
        {
            string product = Guid.NewGuid().ToString();

            Container.Setup<ProductRepository>(x => x.GetAll()).Caches(TimeSpan.FromSeconds(10));
            Container.Setup<ProductRepository>(x => x.Create(product, 1)).Invalidates(x => x.GetAll());

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

        [Test]
        public void ShouldInvalidateTargetWhenArgVariesIsSpecifiedInSouce()
        {
            string product = Guid.NewGuid().ToString();

            Container.Setup<ProductRepository>(x => x.GetAll()).Caches(TimeSpan.FromSeconds(10));
            Container.Setup<ProductRepository>(x => x.Create(Arg.Varies<string>(), Arg.Varies<int>())).Invalidates(x => x.GetAll());

            var repository = ServiceLocator.Current.GetInstance<ProductController>();

            int count = 0;

            repository.GetAllProducts().Count.ShouldEqual(count);

            repository.Create(product, 1);

            count = 1;

            repository.GetAllProducts().Count.ShouldEqual(count);

            Store.Add(new Product());

            // should still return cached item.
            repository.GetAllProducts().Count.ShouldEqual(count);
        }

        [Test]
        public void ShouldInvalidateTargetWhenArgVariesIsSpecifiedInSouceAndTarget()
        {
            string product = Guid.NewGuid().ToString();

            Container.Setup<DummyRepository>(x => x.GetProduct(Arg.Varies<string>())).Caches(TimeSpan.FromMinutes(1));
            Container.Setup<DummyRepository>(x => x.Create(Arg.Varies<Product>())).Invalidates(x => x.GetProduct(Arg.Varies<string>()));

            var repository = ServiceLocator.Current.GetInstance<IDummyRepository>();

            repository.GetProduct(product).ShouldBeNull();

            repository.Create(new Product { Title = product });

            repository.GetProduct(product).ShouldNotBeNull();
        }

        [Test]
        public void ShouldInvalidateOnlyTheTargetWhenSetWithSpecificValue()
        {
            string product = Guid.NewGuid().ToString();

            Container.Setup<DummyRepository>(x => x.GetProduct(Arg.Varies<string>())).Caches(TimeSpan.FromMinutes(1));
            Container.Setup<DummyRepository>(x => x.Create(Arg.Varies<Product>())).Invalidates(x => x.GetProduct(null));

            var repository = ServiceLocator.Current.GetInstance<IDummyRepository>();

            repository.GetProduct(product).ShouldBeNull();

            //it will only invalidate GetProduct("x")
            repository.Create(new Product { Title = product });

            repository.GetProduct(product).ShouldBeNull();
        }

        public interface IDummyRepository
        {
            void Create(Product product);
            Product GetProduct(string productTitle);
        }

        public class DummyRepository : IDummyRepository
        {
            public void Create(Product product)
            {
                Store.Add(product);
            }

            public Product GetProduct(string productTitle)
            {
                return Store.All.Where(x => x.Title == productTitle).FirstOrDefault();
            }
        }

        [TearDown]
        public void AfterEach()
        {
            Store.Clear();
        }
    }
}
