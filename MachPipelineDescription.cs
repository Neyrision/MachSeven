using System;
using System.Collections.Generic;
using System.Text;
using Veldrid;

namespace MachSeven
{
    class MachPipelineDescription
    {
        public GraphicsPipelineDescription graphicsPipelineDesc;

        public MachPipelineDescription(GraphicsDevice _graphicsDevice ,Shader[] _shaders)
        {
            VertexLayoutDescription vertexLayout = new VertexLayoutDescription(
                new VertexElementDescription("Position", VertexElementSemantic.TextureCoordinate, VertexElementFormat.Float2),
                new VertexElementDescription("Color", VertexElementSemantic.TextureCoordinate, VertexElementFormat.Float4));

            graphicsPipelineDesc = new GraphicsPipelineDescription();
            graphicsPipelineDesc.BlendState = BlendStateDescription.SingleOverrideBlend;
            graphicsPipelineDesc.DepthStencilState = new DepthStencilStateDescription(
                depthTestEnabled: true,
                depthWriteEnabled: true,
                comparisonKind: ComparisonKind.LessEqual);
            graphicsPipelineDesc.RasterizerState = new RasterizerStateDescription(
                cullMode: FaceCullMode.Back,
                fillMode: PolygonFillMode.Solid,
                frontFace: FrontFace.Clockwise,
                depthClipEnabled: true,
                scissorTestEnabled: false);
            graphicsPipelineDesc.PrimitiveTopology = PrimitiveTopology.TriangleStrip;
            graphicsPipelineDesc.ResourceLayouts = System.Array.Empty<ResourceLayout>();
            graphicsPipelineDesc.ShaderSet = new ShaderSetDescription(
                vertexLayouts: new VertexLayoutDescription[] { vertexLayout },
                shaders: _shaders);
            graphicsPipelineDesc.Outputs = _graphicsDevice.SwapchainFramebuffer.OutputDescription;
            
        }
    }
}
