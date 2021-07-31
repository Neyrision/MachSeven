using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace MachSeven
{
    class MachCamera
    {
        private float _fov;
        private float _near;
        private float _far;

        private Vector3 _relation;
        private Vector3 _position;
        private Vector3 _target = new Vector3(0,0,0);
        private float _up;
        private float _right;

        private Matrix4x4 _viewMatrix;
        private Matrix4x4 _projectionMatrix;

        private int windowHeight;
        private int windowWidth;


        public MachCamera(MachWindow mWindow)
        {
            _position = new Vector3(0, 0, 3);
            _relation = _target - _position;

            windowHeight = mWindow.window.Height;
            windowWidth = mWindow.window.Width;
        }
    }
}
