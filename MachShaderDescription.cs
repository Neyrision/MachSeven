using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Veldrid;

namespace MachSeven
{
    class MachShaderDescription
    {
        private string VertexCode;
        private string FragmentCode;
        public ShaderDescription vertexShaderDesc;
        public ShaderDescription fragmentShaderDesc;

        public MachShaderDescription()
        {
            VertexCode = File.ReadAllText(@"Shaders/BaseVertexShader.vert");
            FragmentCode = File.ReadAllText(@"Shaders/BaseFragmentShader.frag");

            vertexShaderDesc = new ShaderDescription(
                ShaderStages.Vertex,
                Encoding.UTF8.GetBytes(VertexCode),
                "main");

            fragmentShaderDesc = new ShaderDescription(
                ShaderStages.Fragment,
                Encoding.UTF8.GetBytes(FragmentCode),
                "main");
        }
    }
}
