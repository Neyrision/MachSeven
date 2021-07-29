using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Veldrid;

namespace MachSeven.Models.Util
{
    public struct VertexPosition
    {

            public Vector3 Position;
            public RgbaFloat Color;
            public VertexPosition(Vector3 position, RgbaFloat color)
            {
                Position = position;
                Color = color;
            }
            public const uint SizeInBytes = 28;
    }
}
