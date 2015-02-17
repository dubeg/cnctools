using System;
using System.Collections.Generic;
using System.Text;

namespace ShpLib.Exceptions
{
    [Serializable]
    public class ShpCorruptedException : Exception
    {
        private const string DEFAULT_EX_MESSAGE = "The Shp file is possibly corrupted.";

        public ShpCorruptedException() : base(String.Format(DEFAULT_EX_MESSAGE)) { }

        public ShpCorruptedException(string message) : base(message) { }

        public ShpCorruptedException(string message, Exception inner) : base(message, inner) { }
        protected ShpCorruptedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
