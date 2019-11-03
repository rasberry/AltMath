using System;
using System.Diagnostics;

namespace AltMath
{
	class Program
	{
		static void Main(string[] args)
		{
			//NOTE: this is used as a playground.
			// See test project files for actual testing

			double asin = 0.0;

			Stopwatch sw = new Stopwatch();
			sw.Start();
			for(double a=-3.0; a<3.0; a+=0.1)
			{
				a = Math.Round(a,3);
				//double fsin = Htam.Sin(a);
				//double fsin = Htam.Sin((float)a);
				//double fsin = Htam.Sin2(a);
				double fsin = Htam.Sin3(a);

				double sbase = Math.Sin(a);
				asin += Math.Abs(sbase - fsin);

				//Console.WriteLine(string.Format("sbase={0} dsin={1} diff={2}",sbase,dsin,Math.Abs(sbase-dsin)));
				//Console.WriteLine(string.Format("sbase={0} fsin={1} diff={2}",sbase,fsin,Math.Abs(sbase-fsin)));
				//Console.WriteLine(string.Format("a={3} sbase={0} dsin2={1} diff={2}",sbase,dsin2,Math.Abs(sbase-dsin2),a));
			}

			//Console.WriteLine("adsin="+adsin);
			//Console.WriteLine("afsin="+afsin);
			Console.WriteLine("asin="+asin);
			Console.WriteLine("sw="+sw.ElapsedMilliseconds);
		}
	}
}
