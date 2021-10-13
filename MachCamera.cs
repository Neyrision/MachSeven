using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace MachSeven
{
    class MachCamera
    {
        public float _fov;
        public float _near;
        public float _far;
        public int _width;
        public int _height;

        public Vector3 _direction;
        public Vector3 _position;
        public Vector3 _target = new Vector3(0,0,0);
        public Vector3 _cameraUp;
        public Vector3 _cameraRight;

        private Matrix4x4 _viewMatrix;
        private Matrix4x4 _projectionMatrix;




        public MachCamera(MachWindow mWindow)
        {
            _position = new Vector3(0f, -1f, 2f);
            _direction = Vector3.Normalize(_position - _target);

            _near = 0.1F;
            _far = 100F;

            _height = mWindow.window.Height;
            _width = mWindow.window.Width;

            var up = new Vector3(0,1,0);
            _cameraRight = Vector3.Cross(up, _direction);
            _cameraUp = Vector3.Cross(_direction, _cameraRight);

        }

        public static Matrix4x4 ScalingMatrix(Vector3 vector)
        {
            Matrix4x4 matrix;
            return matrix = Matrix4x4.CreateScale(vector);

        }
    }
}
