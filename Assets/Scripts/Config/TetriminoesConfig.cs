using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Tetrimino.Data;

namespace Config
{
	[Serializable]
	public class TetriminoesConfig
	{
		public List<TetriminoConfig> Tetriminoes;

		public IEnumerable<CellPosition> GetPartsPositions(TetriminoType tetriminoType)
		{
			var tetriminoConfig = Tetriminoes.FirstOrDefault(t => t.TetriminoType == tetriminoType);
			if (tetriminoConfig != null)
			{
				return tetriminoConfig.TetriminoParts;
			}

			throw new KeyNotFoundException(
				$"Couldn't find parts for {Enum.GetName(typeof(TetriminoType), tetriminoType)}");
		}

		public TetriminoCalculationPoint GetRotationPoint(TetriminoType tetriminoType)
		{
			var tetriminoConfig = Tetriminoes.FirstOrDefault(t => t.TetriminoType == tetriminoType);
			if (tetriminoConfig != null)
			{
				return tetriminoConfig.RotationPoint;
			}

			throw new KeyNotFoundException(
				$"Couldn't find parts for {Enum.GetName(typeof(TetriminoType), tetriminoType)}");
		}
	}

	[Serializable]
	public class TetriminoConfig
	{
		public TetriminoType TetriminoType;
		public List<CellPosition> TetriminoParts;
		public TetriminoCalculationPoint RotationPoint;
	}
}