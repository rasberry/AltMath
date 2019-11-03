using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;

namespace test
{
	[TestClass]
	public class TestConstants
	{
		[DataTestMethod]
		[DataRow(3.141592653589793238462643383279502884)]
		[DataRow(3.14159265358979323846)]
		[DataRow(3.1415926535897932384626433832795028841971693993751d)]
		[DataRow(Math.PI)]
		public void TestPI(double pi)
		{
			var bytes = BitConverter.GetBytes(pi);

			Assert.IsTrue(bytes != null);
			Assert.IsTrue(bytes.SequenceEqual(new byte[] {
				// https://en.wikipedia.org/wiki/Double-precision_floating-point_format
				0x18,0x2D,0x44,0x54,0xFB,0x21,0x09,0x40 //PI in IEEE 754
			}));
		}
	}
}
