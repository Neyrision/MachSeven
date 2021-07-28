﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Veldrid;
using Veldrid.SPIRV;

namespace MachSeven
{
    class MachShaderDescription
    {
        private string VertexCode;
        private string FragmentCode;
        public ShaderSetDescription shaderSet;
        public ResourceLayout projViewLayout;
        public ResourceLayout worldTextureLayout;

        public MachShaderDescription(ResourceFactory factory)
        {
            VertexCode = File.ReadAllText(@"Shaders/BaseVertexShader.vert");
            FragmentCode = File.ReadAllText(@"Shaders/BaseFragmentShader.frag");
            
            shaderSet = new ShaderSetDescription(
                new[]
                {
                    new VertexLayoutDescription(
                        new VertexElementDescription("Position", VertexElementSemantic.TextureCoordinate, VertexElementFormat.Float3),
                        new VertexElementDescription("Color", VertexElementSemantic.TextureCoordinate, VertexElementFormat.Float4))
                },
                factory.CreateFromSpirv(
                    new ShaderDescription(ShaderStages.Vertex, Encoding.UTF8.GetBytes(VertexCode), "main"),
                    new ShaderDescription(ShaderStages.Fragment, Encoding.UTF8.GetBytes(FragmentCode), "main")));

        }
    }
}
