using CS_SDL_test.Lib.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class CameraMoveScript : Script
    {
        public CameraMoveScript() : base() { }

        public override void on_key_pressed(Events.KeyEvent e)
        {
            switch (e.key)
            {
                case Input.KeyCode.SLASH_AND_QUESTION_MARK:
                    ViewManager.get_active_camera().FollowTarget = !ViewManager.get_active_camera().FollowTarget;
                    break;
                case Input.KeyCode.BACKSLASH_AND_PIPE:
                    ViewManager.get_active_camera().CenterOnTarget = !ViewManager.get_active_camera().CenterOnTarget;
                    break;
                case Input.KeyCode.LEFT_ARROW:
                    ViewManager.get_active_camera().move_x(-10);
                    break;
                case Input.KeyCode.RIGHT_ARROW:
                    ViewManager.get_active_camera().move_x(10);
                    break;
                case Input.KeyCode.UP_ARROW:
                    ViewManager.get_active_camera().move_y(-10);
                    break;
                case Input.KeyCode.DOWN_ARROW:
                    ViewManager.get_active_camera().move_y(10);
                    break;
                case Input.KeyCode.ESCAPE:
                    Game_EntryPoint.get_game().Application.request_close();
                    break;
            }
        }

        public override void on_create()
        {
            Debug.log("Arrow keys = free camera movement (up/left/down/right) NOTE: disable center & follow mode to enable free camera");
            Debug.log("/ = enable follow target mode (does not disable center mode)");
            Debug.log("\\ = enable center target mode (does not disable follow mode)");
            Debug.log("Esc = quit program");
        }
    }
}
