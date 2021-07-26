
using System;
using System.Numerics;
using System.Text;
using Veldrid;
using Veldrid.Sdl2;
using Veldrid.SPIRV;
using Veldrid.StartupUtilities;

namespace MachSeven
{
    class Program
    {
        
        private static GraphicsDevice _graphicsDevice;

        private static CommandList _commandList;
        private static DeviceBuffer _vertexBuffer;
        private static DeviceBuffer _indexBuffer;
        private static Shader[] _shaders;
        private static Pipeline _pipeline;

        static void Main(string[] args)
        {
            WindowCreateInfo windowCI = new WindowCreateInfo()
            {
                X = 100,
                Y = 100,
                WindowWidth = 960,
                WindowHeight = 540,
                WindowTitle = "MachSeven"
            };

            
            GraphicsDeviceOptions options = new GraphicsDeviceOptions
            {
                
                PreferStandardClipSpaceYDirection = true,
                PreferDepthRangeZeroToOne = true
            };


            MachWindow machWindow = new MachWindow();
            _graphicsDevice = VeldridStartup.CreateGraphicsDevice(machWindow.window, options);

            CreateResources();

            while (machWindow.window.Exists) 
            {
                machWindow.window.PumpEvents();
                Draw();
            }

            DisposeResources();

        }

        public static void CreateResources()
        {
            ResourceFactory factory = _graphicsDevice.ResourceFactory;
            _vertexBuffer = factory.CreateBuffer(new BufferDescription(4 * VertexPositionColor.SizeInBytes, BufferUsage.VertexBuffer));
            _indexBuffer = factory.CreateBuffer(new BufferDescription(4 * sizeof(ushort), BufferUsage.IndexBuffer));
            VertexPositionColor[] quadVertices =
            {
                new VertexPositionColor(new Vector2(-0.75f, 0.75f), RgbaFloat.Red),
                new VertexPositionColor(new Vector2(0.75f, 0.75f), RgbaFloat.Green),
                new VertexPositionColor(new Vector2(-0.75f, -0.75f), RgbaFloat.Blue),
                new VertexPositionColor(new Vector2(0.75f, -0.75f), RgbaFloat.Yellow)
            };
            ushort[] quadIndices = { 0, 1, 2, 3 };
            _graphicsDevice.UpdateBuffer(_vertexBuffer, 0, quadVertices);
            _graphicsDevice.UpdateBuffer(_indexBuffer, 0, quadIndices);

            MachShaderDescription shaderDescription = new MachShaderDescription();
            _shaders = factory.CreateFromSpirv(shaderDescription.vertexShaderDesc, shaderDescription.fragmentShaderDesc);

            MachPipelineDescription pipelineDescription = new MachPipelineDescription(_graphicsDevice, _shaders);
            _pipeline = factory.CreateGraphicsPipeline(pipelineDescription.graphicsPipelineDesc);
            var test = factory.CreateTexture(new TextureDescription(26, 26, 1, 1, 1, PixelFormat.R8_G8_B8_A8_UNorm, TextureUsage.Staging, TextureType.Texture2D, _graphicsDevice.GetSampleCountLimit(PixelFormat.R8_G8_B8_A8_UNorm, false)));
            
            _commandList = factory.CreateCommandList();
        }

        public static void Draw()
        {
            _commandList.Begin();
            _commandList.SetFramebuffer(_graphicsDevice.SwapchainFramebuffer);
            _commandList.ClearColorTarget(0, RgbaFloat.Black);

            _commandList.SetVertexBuffer(0, _vertexBuffer);
            _commandList.SetIndexBuffer(_indexBuffer, IndexFormat.UInt16);
            _commandList.SetPipeline(_pipeline);
            _commandList.DrawIndexed(
                indexCount: 4,
                instanceCount: 1,
                indexStart: 0,
                vertexOffset: 0,
                instanceStart: 0);

            _commandList.End();
            _graphicsDevice.SubmitCommands(_commandList);
            _graphicsDevice.SwapBuffers();
        }

        public static void DisposeResources()
        {
            _pipeline.Dispose();
            _commandList.Dispose();
            _vertexBuffer.Dispose();
            _indexBuffer.Dispose();
            _graphicsDevice.Dispose();
        }

        struct VertexPositionColor
        {
            public Vector2 Position;
            public RgbaFloat Color;
            public VertexPositionColor(Vector2 position, RgbaFloat color)
            {
                Position = position;
                Color = color;
            }
            public const uint SizeInBytes = 24;
        }

    }
}
