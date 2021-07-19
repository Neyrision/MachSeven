using System;
using System.Collections.Generic;
using System.Text;
using Veldrid.Sdl2;
using Veldrid.StartupUtilities;

namespace MachSeven
{
    public class MachWindow
    {
        public Sdl2Window window;
        public WindowCreateInfo windowCI= new WindowCreateInfo()
        {
            X = 100,
            Y = 100,
            WindowWidth = 960,
            WindowHeight = 540,
            WindowTitle = "MachSeven"
        };
        public MachWindow()
        {
            window = VeldridStartup.CreateWindow(ref windowCI);
        }
    }
}
