using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Should;
using AutoBox.Specification.Controllers;
using Microsoft.Practices.ServiceLocation;
using AutoBox.Specification.Repositories.Abstraction;

namespace AutoBox.Specification
{
    [TestFixture]
    public class ResolveTypeSpecs
    {
        [SetUp]
        public void BeforeEach()
        {
            Container.Init(Resolve.FromCurrentAssembly);
        }

        [Test]
        public void ShoudlResolveTypeWithDependencies()
        {
            var result = ServiceLocator.Current.GetInstance<ProductController>();
            result.Initalized().ShouldBeTrue();
        }

        [Test]
        public void ShouldInvokeOriginalMethodWhenNothingIsSpecified()
        {
            var result = ServiceLocator.Current.GetInstance<IProductRepository>();
            result.Create("ipad", 1).ShouldNotBeNull();
        }

        [Test]
        public void ShouldNotPersistsTheTopLevelContainer()
        {
            var repository = ServiceLocator.Current.GetInstance<ProductController>();
            ServiceLocator.Current.GetInstance<ProductController>().ShouldNotEqual(repository);
        }

        [TearDown]
        public void AfterEach()
        {
            Store.Clear();
        }
    }
}
