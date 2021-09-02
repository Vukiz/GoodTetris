using System;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerInputController : MonoBehaviour
    {
        [SerializeField] private List<PlayerActionButton> _playerActionButtons;

        public Action<PlayerActionButton> ActionButtonDown;
        public Action<PlayerActionButton> ActionButtonUp;
        public Action<PlayerActionButton> ActionButtonHold;

        private void Update()
        {
            foreach (var playerActionButton in _playerActionButtons)
            {
                if (Input.GetKeyDown(playerActionButton.KeyCode))
                {
                    ActionButtonDown?.Invoke(playerActionButton);
                }

                if (Input.GetKeyUp(playerActionButton.KeyCode))
                {
                    ActionButtonUp?.Invoke(playerActionButton);
                }
            }
        }
    }
}