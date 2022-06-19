using CS_SDL_test.Lib.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API
{
    public class API_Application
    {
        internal Application _app;

        public API_Application(bool is_3d)
        {
            _app = new Application(is_3d);
        }

        public void request_close()
        {
            _app.request_close();
        }
    }
}
