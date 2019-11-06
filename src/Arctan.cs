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
			if (r < -1 || r > 1) {
				return Math.Sign(r) * Const.MathPIo2 - AtanAms(1.0/r);
			}

			double r2 = r * r;
			double r4 = r2 * r2;
			double r6 = r4 * r2;
			double r8 = r4 * r4;
			double r10 = r6 * r4;
			double r12 = r6 * r6;
			double r14 = r8 * r6;
			double r16 = r8 * r8;
			double r18 = r10 * r8;
			double r20 = r10 * r10;

			//double sum =
			//	   0.881373587 * 1
			//	+ -0.105892925 * (2*r2 -1)
			//	+  0.011135843 * (8*r4 -8*r2 +1)
			//	+ -0.001381195 * (32*r6 -48*r4 +18*r2 -1)
			//	+  0.000185743 * (128*r8 -256*r6 +160*r4 -32*r2 +1)
			//	+ -0.000026215 * (512*r10 -1280*r8 +1120*r6 -400*r4 +50*r2 -1)
			//	+  0.000003821 * (2048*r12 -6144*r10 +6912*r8 -3584*r6 +840*r4 -72*r2 +1)
			//	+ -0.000000570 * (8192*r14 -28672*r12 +39424*r10 -26880*r8 +9408*r6 -1568*r4 +98*r2 -1)
			//	+  0.000000086 * (32768*r16 -131072*r14 +212992*r12 -180224*r10 +84480*r8 -21504*r6 +2688*r4 -128*r2 +1)
			//	+ -0.000000013 * (131072*r18 -589824*r16 +1105920*r14 -1118208*r12 +658944*r10 -228096*r8 +44352*r6 -4320*r4 +162*r2 -1)
			//	+  0.000000002 * (524288*r20 -2621440*r18 +5570560*r16 -6553600*r14 +4659200*r12 -2050048*r10 +549120*r8 -84480*r6 +6600*r4 -200*r2 +1)
			//;

			double sum =
				+0.001048576*r20
				-0.006946816*r18
				+0.021626880*r16
				-0.043425792*r14
				+0.066340864*r12
				-0.087535616*r10
				+0.110391424*r8
				-0.142761152*r6
				+0.199992912*r4
				-0.333333116*r2
				+1.0
			;
			return sum * r;
		}
	}
}
