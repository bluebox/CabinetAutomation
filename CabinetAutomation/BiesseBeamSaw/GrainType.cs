using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CabinetAutomation.BiesseBeamSaw
{
	public class GrainType
	{
		public const Int32 Automatic = 0;
		public const Int32 LongEdge = 1;
		public const Int32 None = 3;

		public static Int32 Default
		{
			get
			{
				return GrainType.Automatic;
			}
		}

		public static Int32 Parse(String s)
		{
			if (s.Equals("Automatic"))
				return Automatic;

			if (s.Equals("Long edge"))
				return LongEdge;

			if (s.Equals("None"))
				return None;

			return Default;
		}
	}
}
