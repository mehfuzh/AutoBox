using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace AutoBox
{
    [Serializable]
    public class AutoBoxException : Exception
    {
        /// <summary>
        /// Initializes the new instance of <see cref="AutoBoxException"/> class.
        /// </summary>
        public AutoBoxException()
        {
        }

        /// <summary>
        /// Initializes the new instance of <see cref="AutoBoxException"/> class.
        /// </summary>
        public AutoBoxException(string message) : base(message)
        {
        }
      
        /// <summary>
        /// Initializes the new instance of <see cref="AutoBoxException"/> class.
        /// </summary>
        public AutoBoxException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes the new instance of <see cref="AutoBoxException"/> class.
        /// </summary>
        protected AutoBoxException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
         
    }
}
