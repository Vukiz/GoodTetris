using System;
using UnityEngine;

namespace Player
{
    [Serializable]
    public struct PlayerActionButton
    {
        [SerializeField] private ActionType _actionType;
        [SerializeField] private KeyCode _keyCode;

        public ActionType ActionType => _actionType;
        public KeyCode KeyCode => _keyCode;
    }
}