using System;

namespace AltMath
{
	public static class MapAngles
	{
		const double Math2PI = 2.0 * Math.PI;
		const double MathPIo2 = Math.PI / 2.0;
		const double MathPIo4 = Math.PI / 4.0;

		//TODO these functions don't do anyting usefull..
		// they just return the same thing as a % (2*PI)

		/// <summary>
		/// Maps angles in radians to [-pi/4,pi/4]
		/// </summary>
		public static double MapToPio4(double ang)
		{
			double a = ang % MathPIo4;
			double m = Math.Floor(Math.Abs(ang/MathPIo4));
			if (ang < -MathPIo4) { return a - m * MathPIo4; }
			if (ang >  MathPIo4) { return a + m * MathPIo4; }
			return a;
		}

		/// <summary>
		/// Maps angles in radians to [-pi/2,pi/2]
		/// </summary>
		public static double MapToPio2(double ang)
		{
			double a = ang % MathPIo2;
			double m = Math.Floor(Math.Abs(ang/MathPIo2));
			if (ang < -MathPIo2) { return a - m * MathPIo2; }
			if (ang >  MathPIo2) { return a + m * MathPIo2; }
			return a;
		}

		/// <summary>
		/// Maps angles in radians to [-pi,pi]
		/// </summary>
		public static double MapToPi(double ang)
		{
			double a = ang % Math.PI;
			double m = Math.Floor(Math.Abs(ang/Math.PI));
			if (ang < -Math.PI) { return a - m * Math.PI; }
			if (ang >  Math.PI) { return a + m * Math.PI; }
			return a;
		}

		/// <summary>
		/// Maps angles in radians to [-2*pi,2*pi]
		/// </summary>
		public static double MapTo2Pi(double ang)
		{
			double a = ang % Math2PI;
			//no need to adjust since 2 * pi is the full range
			return a;
		}
	}
}
