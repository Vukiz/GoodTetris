using System;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
	public class PlayerInputController : MonoBehaviour
	{
		private class HoldKeyTimeData
		{
			public float StartTime { get; }
			public float CurrentTickTime { get; set; }

			public HoldKeyTimeData(float currentTime)
			{
				StartTime = currentTime;
			}
		}

		private const float HoldThreshold = 0.3f;
		private const float HoldTickTime = 0.15f;
		[SerializeField] private List<PlayerActionButton> _playerActionButtons;

		public Action<PlayerActionButton> ActionButtonDown;
		public Action<PlayerActionButton> ActionButtonUp;
		public Action<PlayerActionButton> ActionButtonHold;

		private readonly Dictionary<KeyCode, HoldKeyTimeData> _currentlyPressedKeys =
			new Dictionary<KeyCode, HoldKeyTimeData>();

		private void Update()
		{
			foreach (var playerActionButton in _playerActionButtons)
			{
				var keyCode = playerActionButton.KeyCode;
				if (Input.GetKeyDown(keyCode))
				{
					ActionButtonDown?.Invoke(playerActionButton);
				}

				if (Input.GetKeyUp(keyCode))
				{
					ActionButtonUp?.Invoke(playerActionButton);
				}

				if (Input.GetKey(keyCode))
				{
					var currentTime = Time.time;
					if (_currentlyPressedKeys.ContainsKey(keyCode))
					{
						var holdKeyTimeData = _currentlyPressedKeys[keyCode];
						if (currentTime - holdKeyTimeData.StartTime > HoldThreshold)
						{
							if (currentTime - holdKeyTimeData.CurrentTickTime >= HoldTickTime)
							{
								_currentlyPressedKeys[keyCode].CurrentTickTime -= HoldTickTime;
								ActionButtonHold?.Invoke(playerActionButton);
							}
						}
					}
					else
					{
						_currentlyPressedKeys.Add(keyCode, new HoldKeyTimeData(currentTime));
					}
				}
				else
				{
					_currentlyPressedKeys.Remove(keyCode);
				}
			}
		}
	}
}