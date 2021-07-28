using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Veldrid;
using Veldrid.SPIRV;
using Veldrid.StartupUtilities;

namespace MachSeven
{
    class Game
    {
        private GraphicsDevice _graphicsDevice;
        private CommandList _cl;
        private DeviceBuffer _vertexBuffer;
        private DeviceBuffer _indexBuffer;
        private DeviceBuffer _projectionBuffer;
        private DeviceBuffer _viewBuffer;
        private DeviceBuffer _worldBuffer;
        private VertexPositionColor[] _vertices;
        private ushort[] _indices;
        private MachShaderDescription shaderDescription;
        private MachPipelineDescription pipelineDescription;
        private float _ticks;

        private MachWindow machWindow;

        public Game()
        {
            machWindow = new MachWindow();

            GraphicsDeviceOptions options = new GraphicsDeviceOptions
            {
                PreferStandardClipSpaceYDirection = true,
                PreferDepthRangeZeroToOne = true
            };
            _graphicsDevice = VeldridStartup.CreateGraphicsDevice(machWindow.window, options);
            _indices = GetCubeIndices();
            _vertices = GetCubeVertices();

            CreateResources();

            while (machWindow.window.Exists)
            {
                machWindow.window.PumpEvents();
                Draw();
            }

            DisposeResources();
        }


        public void CreateResources()
        {
            ResourceFactory factory = _graphicsDevice.ResourceFactory;


            _vertexBuffer = factory.CreateBuffer(new BufferDescription((uint) _vertices.Length * VertexPositionColor.SizeInBytes, BufferUsage.VertexBuffer));
            _indexBuffer = factory.CreateBuffer(new BufferDescription((uint) _indices.Length * sizeof(ushort), BufferUsage.IndexBuffer));

            _graphicsDevice.UpdateBuffer(_vertexBuffer, 0, _vertices);
            _graphicsDevice.UpdateBuffer(_indexBuffer, 0, _indices);

            shaderDescription = new MachShaderDescription(factory);

            pipelineDescription = new MachPipelineDescription(_graphicsDevice, shaderDescription, factory);
          

            //var test = factory.CreateTexture(new TextureDescription(26, 26, 1, 1, 1, PixelFormat.R8_G8_B8_A8_UNorm, TextureUsage.Staging, TextureType.Texture2D, _graphicsDevice.GetSampleCountLimit(PixelFormat.R8_G8_B8_A8_UNorm, false)));

            _cl = factory.CreateCommandList();
        }

        public void Draw()
        {
            _cl.Begin();

            _cl.SetFramebuffer(_graphicsDevice.SwapchainFramebuffer);
            _cl.ClearColorTarget(0, RgbaFloat.Black);
            //_cl.ClearDepthStencil(0F);
            _cl.SetPipeline(pipelineDescription._pipeline);
            _cl.SetVertexBuffer(0, _vertexBuffer);
            _cl.SetIndexBuffer(_indexBuffer, IndexFormat.UInt16);
            _cl.DrawIndexed(
                indexCount: 36,
                instanceCount: 1,
                indexStart: 0,
                vertexOffset: 0,
                instanceStart: 0);

            _cl.End();
            _graphicsDevice.SubmitCommands(_cl);
            _graphicsDevice.SwapBuffers(_graphicsDevice.MainSwapchain);
        }

        public void DisposeResources()
        {
            pipelineDescription._pipeline.Dispose();
            _cl.Dispose();
            _vertexBuffer.Dispose();
            _indexBuffer.Dispose();
            _graphicsDevice.Dispose();
        }

        private static ushort[] GetCubeIndices()
        {
            ushort[] indices = {
                0,1,2, 0,2,3,
                4,5,6, 4,6,7,
                8,9,10, 8,10,11,
                12,13,14, 12,14,15,
                16,17,18, 16,18,19,
                20,21,22, 20,22,23,
            };

            return indices;
        }

        private static VertexPositionColor[] GetCubeVertices()
        {
            VertexPositionColor[] vertices =
            {
                new VertexPositionColor(new Vector3(-0.5f, +0.5f, -0.5f), RgbaFloat.Red),
                new VertexPositionColor(new Vector3(+0.5f, +0.5f, -0.5f), RgbaFloat.Green),
                new VertexPositionColor(new Vector3(+0.5f, +0.5f, +0.5f), RgbaFloat.Blue),
                new VertexPositionColor(new Vector3(-0.5f, +0.5f, +0.5f), RgbaFloat.Yellow),
                // Bottom                                                             
                new VertexPositionColor(new Vector3(-0.5f,-0.5f, +0.5f),  RgbaFloat.Red),
                new VertexPositionColor(new Vector3(+0.5f,-0.5f, +0.5f),  RgbaFloat.Green),
                new VertexPositionColor(new Vector3(+0.5f,-0.5f, -0.5f),  RgbaFloat.Blue),
                new VertexPositionColor(new Vector3(-0.5f,-0.5f, -0.5f),  RgbaFloat.Yellow),
                // Left                                                               
                new VertexPositionColor(new Vector3(-0.5f, +0.5f, -0.5f), RgbaFloat.Red),
                new VertexPositionColor(new Vector3(-0.5f, +0.5f, +0.5f), RgbaFloat.Green),
                new VertexPositionColor(new Vector3(-0.5f, -0.5f, +0.5f), RgbaFloat.Blue),
                new VertexPositionColor(new Vector3(-0.5f, -0.5f, -0.5f), RgbaFloat.Yellow),
                // Right                                                              
                new VertexPositionColor(new Vector3(+0.5f, +0.5f, +0.5f), RgbaFloat.Red),
                new VertexPositionColor(new Vector3(+0.5f, +0.5f, -0.5f), RgbaFloat.Green),
                new VertexPositionColor(new Vector3(+0.5f, -0.5f, -0.5f), RgbaFloat.Blue),
                new VertexPositionColor(new Vector3(+0.5f, -0.5f, +0.5f), RgbaFloat.Yellow),
                // Back                                                               
                new VertexPositionColor(new Vector3(+0.5f, +0.75f, -0.75f), RgbaFloat.White),
                new VertexPositionColor(new Vector3(-0.5f, +0.75f, -0.75f), RgbaFloat.White),
                new VertexPositionColor(new Vector3(-0.5f, -0.75f, -0.75f), RgbaFloat.White),
                new VertexPositionColor(new Vector3(+0.5f, -0.75f, -0.75f), RgbaFloat.White),
                // Front                                                              
                new VertexPositionColor(new Vector3(-0.5f, +0.5f, +0.5f), RgbaFloat.Red),
                new VertexPositionColor(new Vector3(+0.5f, +0.5f, +0.5f), RgbaFloat.Green),
                new VertexPositionColor(new Vector3(+0.5f, -0.5f, +0.5f), RgbaFloat.Blue),
                new VertexPositionColor(new Vector3(-0.5f, -0.5f, +0.5f), RgbaFloat.Yellow)
            };
            return vertices;
        }

        struct VertexPositionColor
        {
            public Vector3 Position;
            public RgbaFloat Color;
            public VertexPositionColor(Vector3 position, RgbaFloat color)
            {
                Position = position;
                Color = color;
            }
            public const uint SizeInBytes = 28;
        }
    }
}
