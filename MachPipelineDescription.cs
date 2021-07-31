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
                new DepthStencilStateDescription(
                    depthTestEnabled: true,
                    depthWriteEnabled: true,
                    comparisonKind: ComparisonKind.Always
                    ),
                new RasterizerStateDescription(
                    cullMode: FaceCullMode.Back,
                    fillMode: PolygonFillMode.Solid,
                    frontFace: FrontFace.Clockwise,
                    depthClipEnabled: false,
                    scissorTestEnabled: false
                    ),

                PrimitiveTopology.TriangleList,
                _shaderDesc.shaderSet,
                new[] { _shaderDesc.modelLayout, _shaderDesc.vertexLayout },
                _graphicsDevice.MainSwapchain.Framebuffer.OutputDescription));;

            

        }
    }
}
