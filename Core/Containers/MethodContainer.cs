using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoBox.Abstraction;
using AutoBox.Attributes;

namespace AutoBox.Containers
{
    /// <summary>
    /// Defines method metadata.
    /// </summary>
    [NoIntercept]
    public sealed class MethodContainer : IMethodContainer
    {
        /// <summary>
        /// Initialize the instance of <see cref="MethodContainer"/> class.
        /// </summary>
        public MethodContainer()
        {
            methods = new Dictionary<MethodHash, IList<IMethodMetaData>>();
        }

        public IMethodMetaData CreateMethodMetaData(System.Reflection.MethodInfo methodInfo, Argument[] arguments)
        {
            var methodHash = new MethodHash(methodInfo);

            if (!methods.ContainsKey(methodHash))
                methods.Add(methodHash, new List<IMethodMetaData>());

            var methodMetaData = new MethodMetaData(methodHash, arguments);

            methods[methodHash].Add(methodMetaData);

            return methodMetaData;
        }

        public IMethodMetaData GetMethodMetaData(System.Reflection.MethodInfo methodInfo, object[] arguments)
        {
            var methodHash = new MethodHash(methodInfo);

            if (methods.ContainsKey(methodHash))
            {
                foreach (var metaData in methods[methodHash])
                {
                    if (metaData.ValidateArguments(arguments))
                        return metaData;
                }
            }
            return null;
        }

        private readonly IDictionary<MethodHash, IList<IMethodMetaData>> methods;

    }
}
