using System;
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
        public ResourceLayout modelLayout;
        public ResourceLayout vertexLayout;
        public ResourceSet modelSet;
        public ResourceSet vertexSet;


        public MachShaderDescription(ResourceFactory factory, DeviceBuffer _modelBuffer, DeviceBuffer _viewBuffer, DeviceBuffer _projectionBuffer)
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

            modelLayout = factory.CreateResourceLayout(
                new ResourceLayoutDescription(
                        new ResourceLayoutElementDescription("ModelBuffer", ResourceKind.UniformBuffer, ShaderStages.Vertex)));

            vertexLayout = factory.CreateResourceLayout(
                new ResourceLayoutDescription(
                        new ResourceLayoutElementDescription("ViewBuffer", ResourceKind.UniformBuffer, ShaderStages.Vertex)
                        //new ResourceLayoutElementDescription("ProjectionBuffer", ResourceKind.UniformBuffer, ShaderStages.Vertex)
                        ));

            modelSet = factory.CreateResourceSet(
                new ResourceSetDescription(
                    modelLayout,
                    _modelBuffer));

            vertexSet = factory.CreateResourceSet(
                new ResourceSetDescription(
                    vertexLayout,
                    _viewBuffer
                    //_projectionBuffer
                    ));
        }
    }
}
