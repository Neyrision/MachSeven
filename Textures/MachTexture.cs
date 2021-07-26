using AssetPrimitives;
using SampleBase;
using System.Numerics;
using System.Text;
using Veldrid;
using Veldrid.SPIRV;

namespace MachSeven.Textures
{
    public class MachTexture
    {
        //private readonly ProcessedTexture _texData;
        public VertexPositionTexture[] _vertices;
        public readonly ushort[] _indices;
        public Texture _texture;
        public TextureView _textureView;
        public DeviceBuffer _viewBuffer;
        public DeviceBuffer _worldBuffer;
        public DeviceBuffer _projectionBuffer;
        public DeviceBuffer _indexBuffer;
        public DeviceBuffer _vertexBuffer;

        public MachTexture()
        {
            /*_texData = LoadEmbeddedAsset<ProcessedTexture>("spnza_bricks_a_diff.binary");
            _vertices = GetCubeVertices();
            _indices = GetCubeIndices();*/
        }

        public void CreateResource(ResourceFactory factory, GraphicsDevice graphicsDevice)
        {
            _projectionBuffer = factory.CreateBuffer(new BufferDescription(64, BufferUsage.UniformBuffer));
            _viewBuffer = factory.CreateBuffer(new BufferDescription(64, BufferUsage.UniformBuffer));
            _worldBuffer = factory.CreateBuffer(new BufferDescription(64, BufferUsage.UniformBuffer));

            _vertexBuffer = factory.CreateBuffer(new BufferDescription((uint)(VertexPositionTexture.SizeInBytes * _vertices.Length), BufferUsage.VertexBuffer));
            graphicsDevice.UpdateBuffer(_vertexBuffer, 0, _vertices);

            _indexBuffer = factory.CreateBuffer(new BufferDescription(sizeof(ushort) * (uint)_indices.Length, BufferUsage.IndexBuffer));
            graphicsDevice.UpdateBuffer(_indexBuffer, 0, _indices);

            
        }

       
    }
}
