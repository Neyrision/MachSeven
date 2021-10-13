using MachSeven.Models;
using MachSeven.Models.Util;
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
        private DeviceBuffer _modelBuffer;
        private DeviceBuffer _viewBuffer;
        private DeviceBuffer _projectionBuffer;
        private VertexPosition[] _vertices;
        private ushort[] _indices;
        private MachShaderDescription shaderDescription;
        private MachPipelineDescription pipelineDescription;
        private MachWindow machWindow;
        private MachCamera machCamera;
        private float tick;

        public Game()
        {
            machWindow = new MachWindow();

            ModelReader mr = new ModelReader();

            GraphicsDeviceOptions options = new GraphicsDeviceOptions(
                debug: false,
                swapchainDepthFormat: PixelFormat.R16_UNorm,
                syncToVerticalBlank: false,
                resourceBindingModel: ResourceBindingModel.Improved,
                preferDepthRangeZeroToOne: true,
                preferStandardClipSpaceYDirection: false);

            _graphicsDevice = VeldridStartup.CreateGraphicsDevice(machWindow.window, options);
            machCamera = new MachCamera(machWindow);
            _indices = Cube.GetCubeIndices();
            _vertices = Cube.GetCubeVertices();

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


            _modelBuffer = factory.CreateBuffer(new BufferDescription(64, BufferUsage.UniformBuffer));
            _viewBuffer = factory.CreateBuffer(new BufferDescription(64, BufferUsage.UniformBuffer));
            _projectionBuffer = factory.CreateBuffer(new BufferDescription(64, BufferUsage.UniformBuffer));
            _vertexBuffer = factory.CreateBuffer(new BufferDescription((uint) _vertices.Length * VertexPosition.SizeInBytes, BufferUsage.VertexBuffer));
            _indexBuffer = factory.CreateBuffer(new BufferDescription((uint) _indices.Length * sizeof(ushort), BufferUsage.IndexBuffer));


            _graphicsDevice.UpdateBuffer(_vertexBuffer, 0, _vertices);
            _graphicsDevice.UpdateBuffer(_indexBuffer, 0, _indices);

            shaderDescription = new MachShaderDescription(factory, _modelBuffer, _viewBuffer, _projectionBuffer);

            pipelineDescription = new MachPipelineDescription(_graphicsDevice, shaderDescription, factory);
          

            //var test = factory.CreateTexture(new TextureDescription(26, 26, 1, 1, 1, PixelFormat.R8_G8_B8_A8_UNorm, TextureUsage.Staging, TextureType.Texture2D, _graphicsDevice.GetSampleCountLimit(PixelFormat.R8_G8_B8_A8_UNorm, false)));

            _cl = factory.CreateCommandList();
        }

        public void Draw()
        {
            tick += 0.0001F;
            _cl.Begin();


            Matrix4x4 modelMatrix = 
                Matrix4x4.CreateTranslation(tick, 0, -0.01f)
                * Matrix4x4.CreateRotationX(0f)
                * Matrix4x4.CreateRotationY(0f)
                * Matrix4x4.CreateScale(1.0f);

            
            Matrix4x4 lookAtMatrix = Matrix4x4.CreateLookAt(machCamera._position,  machCamera._position - machCamera._direction, machCamera._cameraUp);

            Matrix4x4 perspectiveMatrix = Matrix4x4.CreatePerspectiveFieldOfView(60.0f * (float)Math.PI / 180f, machCamera._width / (float)machCamera._height, machCamera._near, machCamera._far);


            _cl.UpdateBuffer(_modelBuffer, 0, ref modelMatrix);
            _cl.UpdateBuffer(_viewBuffer, 0, ref lookAtMatrix);
            _cl.UpdateBuffer(_projectionBuffer, 0, ref perspectiveMatrix);
            
            _cl.SetFramebuffer(_graphicsDevice.MainSwapchain.Framebuffer);
            
            _cl.ClearColorTarget(0, RgbaFloat.Black);
            _cl.ClearDepthStencil(1f);
            
            _cl.SetPipeline(pipelineDescription._pipeline);
            _cl.SetGraphicsResourceSet(0, shaderDescription.modelSet);
            _cl.SetGraphicsResourceSet(1, shaderDescription.vertexSet);
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
            _graphicsDevice.WaitForIdle();
        }

        public void DisposeResources()
        {
            pipelineDescription._pipeline.Dispose();
            _cl.Dispose();
            _vertexBuffer.Dispose();
            _indexBuffer.Dispose();
            _graphicsDevice.Dispose();
        }
    }
}
