using System;
using Tetrimino.Data;
using TetriminoMoving.Data;

namespace Extensions
{
    public static class RotationExtension
    {
        public static TetriminoRotation Rotate(this TetriminoRotation tetriminoRotation,
            RotateDirection rotateDirection)
        {
            var rotationInc = rotateDirection == RotateDirection.Clockwise ? 1 : -1;
            return Rotate(tetriminoRotation, rotationInc);
        }

        private static TetriminoRotation Rotate(TetriminoRotation tetriminoRotation, int rotationInc)
        {
            var values = Enum.GetValues(typeof(TetriminoRotation));

            var newRotation = (int) tetriminoRotation + rotationInc;
            var valuesLength = values.Length;
            while (newRotation >= valuesLength)
            {
                newRotation %= valuesLength;
            }

            while (newRotation < 0)
            {
                newRotation += valuesLength;
            }
            
            return (TetriminoRotation) newRotation;
        }
    }
}