using MachSeven.Models.Util;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Veldrid;

namespace MachSeven.Models
{
    public class Cube
    {
        public VertexPosition[] TransformedCube;
        public static VertexPosition[] GetCubeVertices()
        {
            VertexPosition[] vertices =
            {
                new VertexPosition(new Vector3(-0.5f, +0.5f, -0.5f), RgbaFloat.Red),
                new VertexPosition(new Vector3(+0.5f, +0.5f, -0.5f), RgbaFloat.Red),
                new VertexPosition(new Vector3(+0.5f, +0.5f, +0.5f), RgbaFloat.Red),
                new VertexPosition(new Vector3(-0.5f, +0.5f, +0.5f), RgbaFloat.Red),
                // Bottom                                                        
                new VertexPosition(new Vector3(-0.5f,-0.5f, +0.5f),  RgbaFloat.Green),
                new VertexPosition(new Vector3(+0.5f,-0.5f, +0.5f),  RgbaFloat.Green),
                new VertexPosition(new Vector3(+0.5f,-0.5f, -0.5f),  RgbaFloat.Green),
                new VertexPosition(new Vector3(-0.5f,-0.5f, -0.5f),  RgbaFloat.Green),
                // Left                                                          
                new VertexPosition(new Vector3(-0.5f, +0.5f, -0.5f), RgbaFloat.Blue),
                new VertexPosition(new Vector3(-0.5f, +0.5f, +0.5f), RgbaFloat.Blue),
                new VertexPosition(new Vector3(-0.5f, -0.5f, +0.5f), RgbaFloat.Blue),
                new VertexPosition(new Vector3(-0.5f, -0.5f, -0.5f), RgbaFloat.Blue),
                // Right                                                         
                new VertexPosition(new Vector3(+0.5f, +0.5f, +0.5f), RgbaFloat.Yellow),
                new VertexPosition(new Vector3(+0.5f, +0.5f, -0.5f), RgbaFloat.Yellow),
                new VertexPosition(new Vector3(+0.5f, -0.5f, -0.5f), RgbaFloat.Yellow),
                new VertexPosition(new Vector3(+0.5f, -0.5f, +0.5f), RgbaFloat.Yellow),
                // Back                                                          
                new VertexPosition(new Vector3(+0.5f, +0.5f, -0.5f), RgbaFloat.White),
                new VertexPosition(new Vector3(-0.5f, +0.5f, -0.5f), RgbaFloat.White),
                new VertexPosition(new Vector3(-0.5f, -0.5f, -0.5f), RgbaFloat.White),
                new VertexPosition(new Vector3(+0.5f, -0.5f, -0.5f), RgbaFloat.White),
                // Front                                                         
                new VertexPosition(new Vector3(-0.5f, +0.5f, +0.5f), RgbaFloat.Grey),
                new VertexPosition(new Vector3(+0.5f, +0.5f, +0.5f), RgbaFloat.Grey),
                new VertexPosition(new Vector3(+0.5f, -0.5f, +0.5f), RgbaFloat.Grey),
                new VertexPosition(new Vector3(-0.5f, -0.5f, +0.5f), RgbaFloat.Grey)
            };
            return vertices;
        }
        public static ushort[] GetCubeIndices()
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
    }

}
