using CS_SDL_test.Lib.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API
{
    public class EntryPoint
    {
        static public void exec(API_Application app)
        {
            try
            {
                app._app.run();
            }
            catch (Exception e)
            {
                Debug.log_to_file(".\\logs", "crash-" + Debug.get_timestamp(), e.Message);
                Debug.log_to_file(".\\logs", "crash-" + Debug.get_timestamp(), Environment.NewLine);
                Debug.log_to_file(".\\logs", "crash-" + Debug.get_timestamp(), e.StackTrace);
            }
        }
    }
}
