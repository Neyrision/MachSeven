using System;
using System.Collections.Generic;
using System.Text;
using Veldrid;

namespace MachSeven.Textures
{
    public class MachTexture
    {
        public  Texture _texture;
        public TextureView _textureView;
        public DeviceBuffer _viewBuffer;
        public DeviceBuffer _worldBuffer;
        public DeviceBuffer _projectionBuffer;

        public void CreateResource()
        {

        }
    }
}
