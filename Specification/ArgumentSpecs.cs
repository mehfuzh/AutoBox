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
    public class ArgumentSpecs
    {
        [SetUp]
        public void BeforeEach()
        {
            Container.Init();
        }

        [Test]
        public void ShouldReturnCachedItemForSimilarArgWhenVariableIsSpecified()
        {
            Container.Setup<ProductRepository>(x => x.Create(Arg.Varies<string>(), Arg.Varies<int>())).Caches(TimeSpan.FromSeconds(10));

            const string product = "x";

            var repository = ServiceLocator.Current.GetInstance<ProductController>();

            var expected = repository.Create(product, 1).Id;

            repository.Create(product, 1).Id.ShouldEqual(expected);
        }

        [Test]
        public void ShoudlInvalidateForDifferentArgWhenVariableIsSpecified()
        {
            Container.Setup<ProductRepository>(x => x.Create(Arg.Varies<string>(), Arg.Varies<int>())).Caches(TimeSpan.FromSeconds(10));

            const string product = "x";
            const string invalidatingProduct = "y";

            var repository = ServiceLocator.Current.GetInstance<ProductController>();

            var expected = repository.Create(product, 1).Id;

            repository.Create(invalidatingProduct, 1).Id.ShouldNotEqual(expected);
        }

        [Test]
        public void ShouldNotCacheWhenArgInInvoationDoesNotMatchSetup()
        {
            Container.Setup<ProductRepository>(x => x.Create(Arg.Varies<string>(), 1)).Caches(TimeSpan.FromSeconds(10));

            const string product = "x";

            var repository = ServiceLocator.Current.GetInstance<ProductController>();

            var expected = repository.Create(product, 2).Id;

            repository.Create(product, 2).Id.ShouldNotEqual(expected);
        }

        [TearDown]
        public void AfterEach()
        {
            Store.Clear();
        }
    }
}
