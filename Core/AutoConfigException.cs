using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace AutoBox
{
    [Serializable]
    public class AutoConfigException : Exception
    {
        public AutoConfigException(string message) : base(message)
        {
            
        }

        public AutoConfigException()
        {
            
        }

        public AutoConfigException(string message, Exception innerException)
            : base(message, innerException)
        {
            
        }

        protected AutoConfigException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
            
        }
         
    }
}
