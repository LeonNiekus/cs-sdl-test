using API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class TestGame
    {
        private API_Application _application;

        public API_Application Application { get => _application; }

        public TestGame(API_Application app)
        {
            _application = app;
        }
    }
}
