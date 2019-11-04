using System;

namespace AltMath
{
	public static partial class Htam
	{
		// http://www.netlib.org/fdlibm/e_atan2.c
		//TODO

		// https://stackoverflow.com/questions/11930594/calculate-atan2-without-std-functions-or-c99
		//normalized_atan(x) ~ ( C x + x^2 + x^3) / ( 1 + (C + 1) x + (C + 1) x^2 + x^3)
		//where C = (1 + sqrt(17)) / 8
		public static double AtanSO1(double ang)
		{
			double a = Math.Abs(ang);
			//if (a > pio2) { a -= Math.Floor(a/pio2)*pio2; }
			double a2 = a*a, a3 = a*a*a;
			double t1 = a3 + a2 + atanC*a;
			double t2 = a3 + atanCpp*a2 + atanCpp*a + 1;
			return Math.Sign(ang) * MathPIo2 * t1 / t2;
		}

		const double atanC = 0.64038820320220756872767623199676;
		const double atanCpp = 1.0 + atanC;

		public static double Atan2SO1(double y, double x)
		{
			double de = double.Epsilon;
			if (x > 0.0)             { return AtanSO1(y/x); }
			if (x < 0.0 && y >= 0.0) { return AtanSO1(y/x) + Math.PI; }
			if (x < 0.0 && y < 0.0)  { return AtanSO1(y/x) - Math.PI; }
			if (x < de && y > 0.0)   { return MathPIo2; }
			if (x < de && y < 0.0)   { return -1*MathPIo2; }
			return 0.0;
		}

	}
}
