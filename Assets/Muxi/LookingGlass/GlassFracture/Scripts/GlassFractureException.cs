using System;

namespace GlassSystem
{
    [Serializable]
    public class GlassFractureException : Exception
    {
        public GlassFractureException() { }

        public GlassFractureException(string message)
            : base(message) { }

        public GlassFractureException(string message, Exception inner)
            : base(message, inner) { }
    }
}