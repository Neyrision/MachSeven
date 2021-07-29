using System;
using System.Collections.Generic;
using System.Text;
using Veldrid;

namespace MachSeven
{
    class MachPipelineDescription
    {
        public Pipeline _pipeline;
      

        public MachPipelineDescription(GraphicsDevice _graphicsDevice , MachShaderDescription _shaderDesc, ResourceFactory factory)
        {
            _pipeline = factory.CreateGraphicsPipeline(new GraphicsPipelineDescription(
                BlendStateDescription.SingleOverrideBlend,
                DepthStencilStateDescription.DepthOnlyLessEqual,
                new RasterizerStateDescription(
                    cullMode: FaceCullMode.None,
                    fillMode: PolygonFillMode.Wireframe,
                    frontFace: FrontFace.Clockwise,
                    depthClipEnabled: true,
                    scissorTestEnabled: false
                    ),

                PrimitiveTopology.TriangleList,
                _shaderDesc.shaderSet,
                new[] { _shaderDesc.worldLayout },
                _graphicsDevice.MainSwapchain.Framebuffer.OutputDescription
               
            ));;

            

        }
    }
}
