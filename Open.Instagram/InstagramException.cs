using System;

namespace Open.Instagram
{
    public class InstagramException : Exception
    {
        public InstagramException()
        {
        }

        public InstagramException(Meta meta)
            : base(meta.ErrorMessage)
        {
            ErrorType = meta.ErrorType;
        }

        public InstagramException(Exception exc)
            : base(exc.Message, exc)
        {
        }

        public string ErrorType { get; private set; }
    }
}
