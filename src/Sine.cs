using System;

namespace AltMath
{
	public static partial class Htam
	{
		// https://stackoverflow.com/questions/2284860/how-does-c-compute-sin-and-other-math-functions
		public static double SinSO(double a)
		{
			a %= (2.0 * Math.PI);

			int i = 0;
			double cur = a;
			double acc = 1.0;
			double fact= 1.0;
			double pow = a;
			while (Math.Abs(acc) > double.Epsilon && ++i < 100.0)
			{
				fact *= ((2*i)*(2*i+1));
				pow *= -1 * a*a;
				acc = pow / fact;
				cur += acc;
			}
			return cur;
		}

		// http://freestuff.grok.co.uk/rom-dis/page223.txt
		static double[] Sin3Coefs = new double[] {
			-.000000003,
			0.000000592,
			-.000068294,
			0.004559008,
			-.142630785,
			1.276278962
		};
		public static double Sin3(double ang)
		{
			ang %= (2.0 * Math.PI);
			double C = ang * 180.0 / Math.PI;

			double Y = C / 360.0 - Math.Floor(C / 360.0 + 0.5);
			double W = 4.0 * Y;
			if (W > 1.0) { W = 2.0 - W; }
			if (W < -1.0) { W = -W - 2.0; }
			double Z = 2.0 * W * W - 1.0;
			int BREG = 6;
			double T = Generator(Sin3Coefs,Z,BREG);
			double deg = T * W;

			return deg * Math.PI / 180.0;
		}

		// http://freestuff.grok.co.uk/rom-dis/page222.txt
		static double Generator(double[] A,double Z,int BREG)
		{
			double M0 = 2.0 * Z;
			double M2 = 0.0;
			double T = 0.0;
			double M1 = 0.0;
			for(int I=BREG; I >= 1; I--) {
				M1 = M2;
				double U = T * M0 - M2 + A[BREG - I];
				M2 = T;
				T = U;
			}
			T = T - M1;
			return T;
		}

		// https://gist.github.com/XupremZero/49f82c9e21b42ac67a1f4e085c00226c
		public static float SinXupremZero(float x) //x in radians
		{
			x %= 6.28318531f;

			float sinn;
			if (x < -3.14159265f) {
				x += 6.28318531f;
			}
			else if (x > 3.14159265f) {
				x -= 6.28318531f;
			}

			if (x < 0)
			{
				sinn = 1.27323954f * x + 0.405284735f * x * x;

				if (sinn < 0) {
					sinn = 0.225f * (sinn * -sinn - sinn) + sinn;
				} else {
					sinn = 0.225f * (sinn * sinn - sinn) + sinn;
				}
				return sinn;
			}
			else
			{
				sinn = 1.27323954f * x - 0.405284735f * x * x;

				if (sinn < 0) {
					sinn = 0.225f * (sinn * -sinn - sinn) + sinn;
				} else {
					sinn = 0.225f * (sinn * sinn - sinn) + sinn;
				}
				return sinn;
			}
		}
		//public static float Cos(float x) //x in radians
		//
		//return SinXupremZero(x + 1.5707963f);
		//}

		// https://www.ams.org/journals/mcom/1954-08-047/S0025-5718-1954-0063487-2/S0025-5718-1954-0063487-2.pdf
		public static double Sin4(double ang)
		{
			double abound = ang % (2.0 * Math.PI);

			//turns out sin(pi/2*x) [-1,1] is the same as sin(x) from [-pi/2,pi/2]
			//so map [-pi/2,pi/2] to [-1,1]
			double a = abound * 2 / Math.PI;

			//shift these over
			     if (a < -3 || a > 3) { a =  a - Math.Sign(a) * 4; }
			//reflect these
			else if (a < -1 || a > 1) { a = -a + Math.Sign(a) * 2; }

			//sin (pi/2*x)
			//sum(n=0,inf An*Tn(x^2) (-1 <= x <= 1)

			//T0 = 1
			//T1 = 2x - 1
			//T2 = 8x^2 - 8x + 1
			//T3 = 32x^3 - 48x^2 + 18x - 1
			//T4 = 128x^4 - 256x^3 + 160x^2 - 32x + 1
			//T5 = 512x^5 - 1280x^4 + 1120x^3 - 400x^2 + 50x - 1

			double a2 = a * a;
			double a4 = a2 * a2;
			double a6 = a4 * a2;
			double a8 = a6 * a2;
			double a10 = a8 * a2;

			double sum =
				   1.276278972 * 1
				+ -0.285261569 * (2 * a2 - 1)
				+  0.009118016 * (8 * a4 - 8 * a2 + 1)
				+ -0.000136587 * (32 * a6 - 48 * a4 + 18 * a2 - 1)
				+  0.000001185 * (128 * a8 - 256 * a6 + 160 * a4 - 32 * a2 + 1)
				+ -0.000000007 * (512 * a10 - 1280 * a8 + 1120 * a6 - 400 * a4 + 50 * a2 - 1)
			;

			return a * sum;
		}

		// sin(x*pi/2) [-1,1]
		static double[] Sin5Coefs = new double[] {
			 1.08801856413265E-16, 1.13364817781175    ,-1.33226762955019E-17,-0.138071776587192   ,
			 1.99840144432528E-16, 0.00449071424655495 ,-8.43769498715119E-17,-6.77012758421158E-05,
			-9.10382880192628E-17, 5.89129532919674E-07, 9.76996261670138E-17,-3.33805964203293E-09,
			 3.10862446895044E-17, 1.32970745525540E-11, 1.02140518265514E-16,-3.94373422807348E-14,
			 1.46549439250521E-16, 1.06581410364015E-16, 2.13162820728030E-16, 6.21724893790088E-17,
			 4.44089209850063E-18,-2.17603712826531E-16, 2.08721928629529E-16, 2.48689957516035E-16,
			 8.65973959207622E-17,-1.15463194561016E-16, 1.57651669496772E-16,-2.39808173319034E-16,
			-8.68194405256872E-16, 6.70574706873595E-16, 6.88338275267597E-16,-5.92859095149834E-16,
			 3.61932706027801E-16,-2.99760216648792E-16,-1.35225164399344E-15,-2.88657986402541E-17,
			 3.99680288865056E-16, 2.59792187762287E-16,-1.86517468137026E-16,-5.10702591327572E-17,
			 4.35207425653061E-16,-7.30526750203353E-16, 2.30926389122033E-16, 2.30926389122033E-16,
			 1.39888101102770E-16, 5.92859095149834E-16, 2.77555756156289E-16, 1.21902488103842E-15,
			 2.66453525910038E-16, 2.44249065417534E-17, 2.70894418008538E-16,-9.85878045867139E-16,
			-1.97619698383278E-16, 5.92859095149834E-16,-1.88737914186277E-16,-6.57252030578093E-16,
			-1.26565424807268E-16, 1.77635683940025E-17, 3.29958282918597E-15, 6.66133814775094E-18,
			-1.77635683940025E-16,-5.21804821573824E-16, 3.77475828372553E-17,-6.94999613415348E-16,
			-8.39328606616618E-16,-9.37028232783632E-16, 5.95079541199084E-16,-2.73114864057789E-16,
			-2.04281036531029E-15, 3.77475828372553E-17, 1.64313007644523E-15, 4.21884749357559E-17,
			-5.58442181386454E-16,-4.24105195406810E-16, 6.81676937119846E-16,-1.28785870856518E-15,
			 2.44915199232310E-15,-2.59792187762287E-16, 2.88657986402541E-17,-2.18713935851156E-16,
			-1.63202784619898E-16,-2.93098878501041E-16, 4.67403893367191E-16,-5.77315972805081E-16,
			 1.20847776230448E-15,-2.10942374678780E-17, 1.02307051719208E-15,-6.27831120425526E-16,
			 1.33226762955019E-16,-1.00419672577345E-15, 2.58126853225349E-16,-8.60422844084496E-17,
			 1.13409281965460E-15, 5.49837952945609E-16, 4.15306677936655E-15, 6.03961325396085E-16,
			 6.71684929898220E-16, 1.24275589818978E-15, 5.02653474399040E-16,-1.27627075574566E-15
		};

		public static double Sin5(double ang, int accuracy = 8)
		{
			double abound = ang % (2.0 * Math.PI);
			double a = abound * 2.0 / Math.PI;

			//shift these over
			     if (a < -3.0 || a > 3.0) { a =  a - Math.Sign(a) * 4.0; }
			//reflect these
			else if (a < -1.0 || a > 1.0) { a = -a + Math.Sign(a) * 2.0; }

			double min = -1.0, max = 1.0;

			//InvChebyshev2
			int depth = Math.Clamp(accuracy,3,Sin5Coefs.Length);
			double dip1 = 0.0;
			double di = 0.0;
			double y = (2.0 * a - min - max) / (max - min);
			for(int i=depth-1; 1 <= i; i--) {
				double dip2 = dip1;
				dip1 = di;
				di = 2.0 * y * dip1 - dip2 + Sin5Coefs[i];
			}
			return y * di - dip1 + 0.5 * Sin5Coefs[0];
		}
	}
}
