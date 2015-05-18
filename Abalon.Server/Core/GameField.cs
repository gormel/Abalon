using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abalon.Server.Core
{
	public enum ElementState
	{
		Black,
		White,
		Free
	}

	internal class FieldElement
	{
		public ElementState State = ElementState.Free;
	}

	public class GameField
	{
		FieldElement[][] elements = new FieldElement[9][];
		private void Fill()
		{
			for (int i = 0; i < elements.Length; i++)
			{
				elements[i] = new FieldElement[9 - Math.Abs(4 - i)];
			}

			for (int i = 0; i < elements.Length; i++)
			{
				for (int j = 0; j < elements[i].Length; j++)
				{
					elements[i][j] = new FieldElement();
				}
			}
		}
	}
}
