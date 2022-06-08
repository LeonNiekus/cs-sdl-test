using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_SDL_test.Lib
{
    public class NoEventsFoundException : Exception
    {
        public NoEventsFoundException()
        {
        }

        public NoEventsFoundException(string message)
            : base(message)
        {
        }

        public NoEventsFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
