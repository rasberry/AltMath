using System;
using System.Diagnostics;
using AltMath;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace test
{
	[TestClass]
	public class TestAtan
	{
		[TestMethod]
		public void TestAtanSO1()
		{
			TestCommon(Htam.AtanSO1,-10.0,10.0);
			TestCommon(Htam.Atan2SO1,-10.0,10.0);
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
				double vchk = Math.Atan(a);
				double diff = Math.Abs(vrep - vchk);
				tot += diff;

				//string txt = string.Format("{0}\ta={1:E}\tv={2:E}\tc={3:E}\td={4:E}",
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
