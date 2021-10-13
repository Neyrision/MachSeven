using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MachSeven.Models.Util
{
    class ModelReader
    {
        public ModelReader()
        {
            using (StreamReader sr = File.OpenText("Meshs/teapot.fbx"))
            {
                string s;
                while((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine(s);
                }
            }
        }
    }
}
