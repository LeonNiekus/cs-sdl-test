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
                app.run();
            }
            catch (Exception e)
            {
                Debug.log_to_file(".\\logs", "crash-" + Debug.get_timestamp(), e.Message);
                Debug.log_to_file(".\\logs", "crash-" + Debug.get_timestamp(), Environment.NewLine);
                Debug.log_to_file(".\\logs", "crash-" + Debug.get_timestamp(), e.StackTrace);
/*                DateTime now = DateTime.Now;

                

                if (!Directory.Exists(".\\logs")) Directory.CreateDirectory(".\\logs");

                using (FileStream fs = System.IO.File.Create(".\\logs\\crash-" + now.ToString().Replace(':', '_') + ".txt"))
                {
                    byte[] errMsg = new UTF8Encoding(true).GetBytes(e.Message);
                    fs.Write(errMsg, 0, errMsg.Length);
                    byte[] newline = Encoding.ASCII.GetBytes(Environment.NewLine);
                    fs.Write(newline, 0, newline.Length);
                    byte[] errLog = new UTF8Encoding(true).GetBytes(e.StackTrace);
                    fs.Write(errLog, 0, errLog.Length);
                }*/
            }
        }
    }
}
