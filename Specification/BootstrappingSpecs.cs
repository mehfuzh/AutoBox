using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Should;
using AutoBox.Specification.Abstraction;
using AutoBox.Specification.Controllers;
using Microsoft.Practices.ServiceLocation;

namespace AutoBox.Specification
{
    [TestFixture]
    public class BootstrappingSpecs
    {
        [SetUp]
        public void BeforeEach()
        {
            Box.Init(Resolve.FromCurrentAssembly);
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
        public void RemoveAllData()
        {
            Store.Clear();
        }
    }
}
