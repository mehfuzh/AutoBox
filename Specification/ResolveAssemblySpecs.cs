using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Should;
using NUnit.Framework;
using System.Reflection;
using AutoBox.Specification.Repositories.Abstraction;
using Microsoft.Practices.ServiceLocation;

namespace AutoBox.Specification
{
    [TestFixture]
    public class ResolveAssemblySpecs
    {
        [Test]
        public void ShouldBeAbleToResolveFromSpecificAssembly()
        {
            Container.Init(Resolve.From(Assembly.GetExecutingAssembly()));
            ServiceLocator.Current.GetInstance<IProductRepository>().ShouldNotBeNull();
        }
    }
}
