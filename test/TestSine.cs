using System;
using System.Diagnostics;
using AltMath;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace test
{
	[TestClass]
	public class TestSin
	{
		[TestMethod]
		public void TestSinSO()
		{
			TestCommon(Htam.SinSO,-Math.PI*2,Math.PI*2);
			Assert.IsTrue(true);
		}

		[TestMethod]
		public void TestSin3()
		{
			TestCommon(Htam.Sin3,-Math.PI*2,Math.PI*2);
			Assert.IsTrue(true);
		}

		[TestMethod]
		public void TestSinXupremZero()
		{
			TestCommon(Htam.SinXupremZero,-Math.PI*2,Math.PI*2);
			Assert.IsTrue(true);
		}

		[TestMethod]
		public void TestSin4()
		{
			TestCommon(Htam.Sin4,-Math.PI*2,Math.PI*2);
			Assert.IsTrue(true);
		}

		[TestMethod]
		public void TestSin5()
		{
			TestCommon((double a) => Htam.Sin5(a),-Math.PI*2,Math.PI*2);
			Assert.IsTrue(true);
		}

		[TestMethod]
		public void TestSin5_2()
		{
			TestCommon((double a) => Htam.Sin5(a,16),-Math.PI*2,Math.PI*2);
			Assert.IsTrue(true);
		}

		[TestMethod]
		public void TestSin5_3()
		{
			TestCommon((double a) => Htam.Sin5(a,32),-Math.PI*2,Math.PI*2);
			Assert.IsTrue(true);
		}

		static void TestCommon(Func<float,float> rep, double min, double max)
		{
			TestCommon((double a) => (double)rep((float)a),min,max,rep.Method.Name);
		}

		static void TestCommon(Func<double,double> rep, double min, double max, string name = null)
		{
			double tot = 0.0;
			if (name == null) { name = rep.Method.Name; }
			for(double a=min; a<max; a+=0.1)
			{
				double vrep = rep(a);
				double vchk = Math.Sin(a);
				double diff = Math.Abs(vrep - vchk);
				tot += diff;

				//Helpers.Log(name+"\ta="+a+"\tv="+vrep+"\tc="+vchk+"\td="+diff);
			}
			Helpers.Log(name+"\ttot="+tot);

			var sw = Stopwatch.StartNew();
			for(double tt=min; tt<max; tt+=0.00001)
			{
				double vrep = rep(tt);
			}
			Helpers.Log(name+"\ttime test="+sw.ElapsedMilliseconds);
		}
	}
}
