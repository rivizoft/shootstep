using System;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace shootstep.view
{
    public class Camera
    {
        private Point _prevShift;
        private Point _shift;
        private IBaseGameObj _followObj;
        private int _width;
        private int _height;
        private int _vDelay;
        private int _hDelay;
        
        public Camera(IBaseGameObj followObj, int width, int height)
        {
            this._shift = new Point(width / 2 - followObj.Position.X, height / 2 - followObj.Position.Y);
            this._prevShift = _shift;
            SetNewParams(width,height,followObj);
            _followObj.Moved += Update;
        }

        public void SetSize(int newWidth, int newHeight)
        {
            _width = newWidth;
            _height = newHeight;
            this._vDelay = _height * 3 / 70;
            this._hDelay = _width * 3 / 70;
        }
        
        public void SetNewParams(int newWidth, int newHeight, IBaseGameObj newFollowObj)
        {
            SetSize(newWidth, newHeight);
            _followObj = newFollowObj;
        }

        public void SetNewFollowObj(IBaseGameObj newFollowObj) => _followObj = newFollowObj;

        public void Update()
        {
            _prevShift = _shift;
            var newX = _width / 2 - _followObj.Position.X;
            var newY = _height / 2 - _followObj.Position.Y;
            _shift.X += (((_shift.X + newX) / 2) - _shift.X) / _hDelay;
            _shift.Y += (((_shift.Y + newY) / 2) - _shift.Y) / _vDelay;
        }

        public Point GetViewPoint() => _shift;

        public event Action Moved;

    }
}