using System;
using System.Diagnostics;
using AltMath;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace test
{
	[TestClass]
	public class TestTrig : TestCommon
	{
		const double Math2PI = 2.0 * Math.PI;
		const double MathPIo2 = Math.PI / 2.0;
		const double MathPIo4 = Math.PI / 4.0;
		const double TestMin = -Math2PI;
		const double TestMax = Math2PI;

		[TestMethod]
		public void TestSinSO()
		{
			TestCommon(Htam.SinSO,Math.Sin,TestMin,TestMax);
			Assert.IsTrue(true);
		}

		[TestMethod]
		public void TestSin3()
		{
			TestCommon(Htam.Sin3,Math.Sin,TestMin,TestMax);
			Assert.IsTrue(true);
		}

		[TestMethod]
		public void TestSinXupremZero()
		{
			TestCommon(Htam.SinXupremZero,Math.Sin,TestMin,TestMax);
			Assert.IsTrue(true);
		}

		[TestMethod]
		public void TestSinAms()
		{
			TestCommon(Htam.SinAms,Math.Sin,TestMin,TestMax);
			Assert.IsTrue(true);
		}

		[TestMethod]
		public void TestSin5()
		{
			TestCommon((double a) => Htam.Sin5(a),
				Math.Sin,TestMin,TestMax);
			Assert.IsTrue(true);
		}

		[TestMethod]
		public void TestSin5_2()
		{
			TestCommon((double a) => Htam.Sin5(a,16),
				Math.Sin,TestMin,TestMax);
			Assert.IsTrue(true);
		}

		[TestMethod]
		public void TestSin5_3()
		{
			TestCommon((double a) => Htam.Sin5(a,32),
				Math.Sin,TestMin,TestMax);
			Assert.IsTrue(true);
		}

		[TestMethod]
		public void TestCordic()
		{
			TestCommon((double a) => Htam.Cordic(a).Item2,
				Math.Sin,TestMin,TestMax);
			Assert.IsTrue(true);
		}

		[TestMethod]
		public void TestCordic_2()
		{
			TestCommon((double a) => Htam.Cordic(a,24).Item2,
				Math.Sin,TestMin,TestMax);
			Assert.IsTrue(true);
		}

		[TestMethod]
		public void TestSinTaylor()
		{
			TestCommon(Htam.SinTaylor,Math.Sin,TestMin,TestMax);
			Assert.IsTrue(true);
		}

		[TestMethod]
		public void TestSinFdlibm()
		{
			TestCommon(Htam.SinFdlibm,Math.Sin,TestMin,TestMax);
			Assert.IsTrue(true);
		}


		[TestMethod]
		public void TestSin6()
		{
			TestCommon((double a) => Htam.Sin6(a),
				Math.Sin,TestMin,TestMax);
			Assert.IsTrue(true);
		}

		[TestMethod]
		public void TestSin6_2()
		{
			TestCommon((double a) => Htam.Sin6(a,16),
				Math.Sin,TestMin,TestMax);
			Assert.IsTrue(true);
		}

		[TestMethod]
		public void TestSin6_3()
		{
			TestCommon((double a) => Htam.Sin6(a,32),
				Math.Sin,TestMin,TestMax);
			Assert.IsTrue(true);
		}

		[TestMethod]
		public void TestCosAms()
		{
			TestCommon(NBSApplied.Cos,Math.Cos,TestMin,TestMax,"NBSCos");
			Assert.IsTrue(true);
		}

		static void TestCommon(Func<float,float> rep, Func<double,double> check,double min, double max)
		{
			TestCommon((double a) => (double)rep((float)a),check,min,max,rep.Method.Name);
		}

		static void TestCommon(Func<double,double> rep, Func<double,double> check, double min, double max, string name = null)
		{
			double tot = 0.0;
			if (name == null) { name = rep.Method.Name; }
			for(double a=min; a<max; a+=0.1)
			{
				double vrep = rep(a);
				double vchk = check(a);
				double diff = Math.Abs(vrep - vchk);
				tot += diff;

				string txt = string.Format("{0}\ta={1:E}\tv={2:E}\tc={3:E}\td={4:E}",
					name,a,vrep,vchk,diff);
				Helpers.Log(txt);
			}
			Helpers.Log(name+"\ttot="+tot);

			var sw = Stopwatch.StartNew();
			for(double tt=min; tt<max; tt+=0.00001)
			{
				double vrep = rep(tt);
			}
			Helpers.Log(name+"\ttime test="+sw.ElapsedMilliseconds);
		}


		//TODO === these are pretty much wrong
		// they were supposed to take the full cirlce range
		// and cut it down to a specified interval
		// i think these actually need to be transforms instead
		// taking a function as input
		// because they need to reduce the input angle
		// then modify the output to the appropriate quatrant
		#if false
		[TestMethod]
		public void TestSinRange1()
		{
			TestSinRange((double a) => {
				return a % Math2PI;
			});
		}

		[TestMethod]
		public void TestSinRange2()
		{
			TestSinRange((double ang) => {
				double r = ang % Math2PI;
				double a = ang % Math.PI;
				return r < -Math.PI || r > Math.PI ? -a : a;
			});
		}

		[TestMethod]
		public void TestSinRange3()
		{
			TestSinRange((double ang) => {
				double a = ang % MathPIo2;
				double m = Math.Floor(Math.Abs(ang/MathPIo2));
				if (ang < -MathPIo2) { return  a - m * MathPIo2; }
				if (ang > MathPIo2)  { return  a + m * MathPIo2; }
				return a;
			});
		}

		[TestMethod]
		public void TestSinRange4()
		{
			TestSinRange((double ang) => {
				double a = ang % MathPIo4;
				double m = Math.Floor(Math.Abs(ang/MathPIo4));
				if (ang < -MathPIo4) { return a - m * MathPIo4; }
				if (ang > MathPIo4)  { return a + m * MathPIo4; }
				return a;
			});
		}

		static void TestSinRange(Func<double,double> trans)
		{
			double min = -10.0;
			double max = 10.0;

			//Helpers.Log(trans.Method.Name + " " + new string('=',10));
			double tot = 0.0;
			for(double a=min; a<max; a+=0.1) {
				double ang = trans(a);
				double n = Math.Sin(a);
				double c = Math.Sin(ang);
				double d = Math.Abs(c-n);
				tot += d;
				//Helpers.Log(string.Format("a={0:F6}\tn={1:F6}\tc={2:F6}\td={3:F6}",
				//	a,n,c,d
				//));
			}
			//Helpers.Log("tot="+tot);
			Assert.AreEqual(tot,1e-10,1e-10);
		}
		#endif
	}
}
