using System;
using UnityEngine;
using Zenject;

namespace Game
{
	public class GameTicker : ITickable
	{
		public Action GameTicked;

		private float _lastTimeTicked;
		private float _timePerTick = 1f;

		public void SetTimePerTick(float timePerTick)
		{
			_timePerTick = timePerTick;
		}

		public void Tick()
		{
			var time = Time.time;
			var timeToTick = _lastTimeTicked + _timePerTick;
			if (time >= timeToTick)
			{
				GameTicked?.Invoke();
				_lastTimeTicked = time;
			}
		}

		public void ResetTickTime()
		{
			_lastTimeTicked = Time.time - _timePerTick / 2;
		}
	}
}