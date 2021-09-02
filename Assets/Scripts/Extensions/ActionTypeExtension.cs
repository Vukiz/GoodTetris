using System;
using Player;
using TetriminoMoving.Data;

namespace Extensions
{
    public static class ActionTypeExtension
    {
        public static bool IsMoveAction(this ActionType actionType)
        {
            return actionType == ActionType.Down
                   || actionType == ActionType.Left
                   || actionType == ActionType.Right;
        }
        
        public static bool IsRotateAction(this ActionType actionType)
        {
            return actionType == ActionType.RotateClockwise
                   || actionType == ActionType.RotateCounterClockwise;
        }
        
        public static MoveDirection TransformInputToDirection(this ActionType actionType)
        {
            return actionType switch
            {
                ActionType.Left => MoveDirection.Left,
                ActionType.Right => MoveDirection.Right,
                ActionType.Down => MoveDirection.Down,
                _ => throw new ArgumentOutOfRangeException(nameof(actionType), actionType, null)
            };
        }
        
        public static RotateDirection TransformInputToRotation(this ActionType actionType)
        {
            return actionType switch
            {
                ActionType.RotateClockwise => RotateDirection.Clockwise,
                ActionType.RotateCounterClockwise => RotateDirection.Counterclockwise,
                _ => throw new ArgumentOutOfRangeException(nameof(actionType), actionType, null)
            };
        }
    }
}