using System;

namespace AltMath
{
	public static partial class Htam
	{
		// https://en.wikipedia.org/wiki/Atan2
		public static double Atan2_1(Func<double,double> fatan, double y, double x)
		{
			if (x >  0.0)             { return fatan(y/x); }
			if (x <  0.0 && y >= 0.0) { return fatan(y/x) + Math.PI; }
			if (x <  0.0 && y <  0.0) { return fatan(y/x) - Math.PI; }
			if (x == 0.0 && y >  0.0) { return Const.MathPIo2; }
			if (x == 0.0 && y <  0.0) { return -Const.MathPIo2; }
			return 0.0;
		}

		public static double Atan2_2(Func<double,double> fatan, double y, double x)
		{
			if (x > 0.0) { return fatan(y/x); }
			if (y > 0.0) { return Const.MathPIo2 - fatan(x/y); }
			if (y < 0.0) { return -Const.MathPIo2 - fatan(x/y); }
			if (x < 0.0) { return fatan(y/x) + Math.PI; }
			return 0.0;
		}

		// http://www.netlib.org/fdlibm/e_atan2.c
		//TODO

		// https://stackoverflow.com/questions/11930594/calculate-atan2-without-std-functions-or-c99
		//normalized_atan(x) ~ ( C x + x^2 + x^3) / ( 1 + (C + 1) x + (C + 1) x^2 + x^3)
		//where C = (1 + sqrt(17)) / 8
		public static double AtanSO1(double r)
		{
			double a = Math.Abs(r);
			//if (a > pio2) { a -= Math.Floor(a/pio2)*pio2; }
			double a2 = a*a, a3 = a*a*a;
			double t1 = a3 + a2 + atanC*a;
			double t2 = a3 + atanCpp*a2 + atanCpp*a + 1;
			return Math.Sign(r) * Const.MathPIo2 * t1 / t2;
		}

		const double atanC = 0.64038820320220756872767623199676;
		const double atanCpp = 1.0 + atanC;

		//http://mathworld.wolfram.com/InverseTangent.html
		//Maclaurin series
		public static double AtanMac(double r, int accuracy = 8)
		{
			if (r < -1 || r > 1) {
				return Math.Sign(r) * Const.MathPIo2 - AtanMac(1.0/r);
			}
			double z = 0.0;
			for(int n=0; n<accuracy; n++) {
				double m1 = n % 2 == 0 ? 1 : -1;
				double tnp1 = 2 * n + 1;
				z += m1 * Math.Pow(r,tnp1) / tnp1;
			}
			return z;
		}

		//http://mathworld.wolfram.com/InverseTangent.html
		public static double AtanActon(double r, int accuracy = 8)
		{
			double a = 1.0 / Math.Sqrt(r*r + 1.0);
			double b = 1.0;

			for(int i=0; i<accuracy; i++) {
				double an = (a + b) / 2.0;
				double bn = Math.Sqrt(an * b);
				a = an; b = bn;
			}

			double t = r / (a * Math.Sqrt(1 + r*r));
			return t;
		}

		//https://www.ams.org/journals/mcom/1954-08-047/S0025-5718-1954-0063487-2/S0025-5718-1954-0063487-2.pdf
		public static double AtanAms(double r)
		{
			return NBSApplied.Atan(r);
		}
	}
}
