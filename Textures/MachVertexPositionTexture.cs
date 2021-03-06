using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace MachSeven.Textures
{
    public struct VertexPositionTexture
    {
        public const uint SizeInBytes = 20;
    
        public float PosX;
        public float PosY;
        public float PosZ;
    
        public float TexU;
        public float TexV;
    
        public VertexPositionTexture(Vector3 pos, Vector2 uv)
        {
            PosX = pos.X;
            PosY = pos.Y;
            PosZ = pos.Z;
            TexU = uv.X;
            TexV = uv.Y;
        }
    }
}
