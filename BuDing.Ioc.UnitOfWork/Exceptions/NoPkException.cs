using System;
using System.Collections.Generic;
using System.Text;

namespace BuDing.Ioc.UnitOfWork.Exceptions
{
   public  class NoPkException:Exception
    {
        public NoPkException()
        {
        }

        public NoPkException(string message)
            : base(message)
        {
        }

        public NoPkException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
