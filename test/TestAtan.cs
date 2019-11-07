using System;
using System.Diagnostics;
using AltMath;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace test
{
	[TestClass]
	public class TestAtan : TestCommon
	{
		const double TestMin = -10.0;
		const double TestMax = 10.0;

		[TestMethod]
		public void TestAtanSO1()
		{
			TestAll(Htam.AtanSO1);
			Assert.IsTrue(true);
		}

		[TestMethod]
		public void TestAtanMac()
		{
			TestAll((double a) => Htam.AtanMac(a));
			Assert.IsTrue(true);
		}

		[TestMethod]
		public void TestAtanMac_2()
		{
			TestAll((double a) => Htam.AtanMac(a,16));
			Assert.IsTrue(true);
		}

		[TestMethod]
		public void TestAtanMac_3()
		{
			TestAll((double a) => Htam.AtanMac(a,32));
			Assert.IsTrue(true);
		}

		[TestMethod]
		public void TestAtanActon()
		{
			TestAll((double a) => Htam.AtanActon(a));
			Assert.IsTrue(true);
		}

		[TestMethod]
		public void TestAtanActon_2()
		{
			TestAll((double a) => Htam.AtanActon(a,16));
			Assert.IsTrue(true);
		}

		[TestMethod]
		public void TestAtanActon_3()
		{
			TestAll((double a) => Htam.AtanActon(a,32));
			Assert.IsTrue(true);
		}

		[TestMethod]
		public void TestAtanAms()
		{
			TestAll(Htam.AtanAms);
			Assert.IsTrue(true);
		}

		static void TestAll(Func<double,double> func)
		{
			string n = func.Method.Name;
			TestCommon(func,TestMin,TestMax,n);
			TestCommon((double y,double x) => Htam.Atan2_1(func,y,x),TestMin,TestMax,n+"-atan21");
			TestCommon((double y,double x) => Htam.Atan2_2(func,y,x),TestMin,TestMax,n+"-atan22");
		}

		static void TestCommon(Func<double,double> rep, double min, double max, string name = null)
		{
			double tot = 0.0;
			if (name == null) { name = rep.Method.Name; }
			for(double a=min; a<max; a+=0.1)
			{
				double vrep = rep(a);
				double vchk = Math.Atan(a);
				double diff = Math.Abs(vrep - vchk);
				tot += diff;

				//string txt = string.Format("{0}\ta={1:F6}\tv={2:F6}\tc={3:F6}\td={4:F6}",
				//	name,a,vrep,vchk,diff);
				//Helpers.Log(txt);
			}
			Helpers.Log(name+"\ttot="+tot);

			var sw = Stopwatch.StartNew();
			for(double tt=min; tt<max; tt+=0.00001)
			{
				double vrep = rep(tt);
			}
			Helpers.Log(name+"\ttime test="+sw.ElapsedMilliseconds);
		}

		static void TestCommon(Func<double,double,double> rep, double min, double max,string name = null)
		{
			double tot = 0.0;
			if (name == null) { name = rep.Method.Name; }
			for(double y=min; y<max; y+=0.1)
			for(double x=min; x<max; x+=0.1)
			{
				double vrep = rep(y,x);
				double vchk = Math.Atan2(y,x);
				double diff = Math.Abs(vrep - vchk);
				tot += diff;

				//string txt = string.Format("{0}\ta={1:E}\tv={2:E}\tc={3:E}\td={4:E}",
				//	name,a,vrep,vchk,diff);
				//Helpers.Log(txt);
			}
			Helpers.Log(name+"\ttot="+tot);

		}
	}
}
