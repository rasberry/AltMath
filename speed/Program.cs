using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestPlatform.Common;
using Microsoft.VisualStudio.TestPlatform.Common.Telemetry;
using Microsoft.VisualStudio.TestPlatform.CrossPlatEngine.Discovery;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;
using Microsoft.VisualStudio.TestPlatform.MSTest.TestAdapter;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.MSTest.TestAdapter.ObjectModel;
using test;
using System.Diagnostics;

namespace speed
{
	class Program
	{
		static void Main(string[] args)
		{
			var tsw = Stopwatch.StartNew();
			foreach(var provider in GetAllITestItemProvider()) {
				Console.WriteLine("\n"+provider.GetType().FullName);
				Console.WriteLine("Name\tAccuracy\tSpeed");
				foreach(var tuple in provider.GetItems()) {
					DoTest(provider,tuple.Item1,tuple.Item2);
				}
			}
			Console.WriteLine("\nTotal Time: "+tsw.ElapsedMilliseconds);
		}

		static void DoTest(ITestItemProvider provider, TestItem item,Func<TestItem,double> test)
		{
			double acc = test.Invoke(item);

			var sw = Stopwatch.StartNew();
			provider.SpeedTest(item);
			var time = sw.Elapsed;

			Console.WriteLine(
				String.Format("{0}\t{1}\t{2}",item.Name,acc,time.TotalMilliseconds)
			);
		}

		static IEnumerable<ITestItemProvider> GetAllITestItemProvider()
		{
			var itipType = typeof(ITestItemProvider);
			foreach(var asem in GetAssemblies()) {
				foreach(Type t in asem.GetTypes()) {
					bool keep = t.IsClass && itipType.IsAssignableFrom(t);
					if (keep) {
						var inst = Activator.CreateInstance(t) as ITestItemProvider;
						yield return inst;
					}
				}
			}
		}

		static IEnumerable<Assembly> GetAssemblies()
		{
			var list = new List<string>();
			var stack = new Stack<Assembly>();
			stack.Push(Assembly.GetEntryAssembly());

			do {
				var asm = stack.Pop();
				yield return asm;

				foreach (var reference in asm.GetReferencedAssemblies()) {
					if (!list.Contains(reference.FullName)) {
						stack.Push(Assembly.Load(reference));
						list.Add(reference.FullName);
					}
				}
			}
			while (stack.Count > 0);
		}

		static void Main1(string[] args)
		{
			Console.WriteLine(GetTestsPath());
			var testStore = new List<TestCase>();
			var sink = new Sink(testStore);
			var log = new Logger();
			var dc = new DiscoveryContext();
			var mstd = new MSTestDiscoverer();
			mstd.DiscoverTests(new[] { GetTestsPath() },dc,log,sink);

			foreach(var test in testStore) {
				//var mste = new MSTestExecutor();
				//mste.RunTests(
				Log( "\n"
					+"\n"+test.CodeFilePath
					+"\n"+test.DisplayName
					+"\n"+test.FullyQualifiedName
					+"\n"+test.Id
					+"\n"+test.LineNumber
					+"\n"+test.Source
					//+"\n"+PropertiesToString(test.GetProperties())
					//+"\n"+TraitsToString(test.Traits)
				);
			}
		}

		static void Log(string m) {
			Console.WriteLine(m);
		}

		static string PropertiesToString(IEnumerable<KeyValuePair<TestProperty, object>> p)
		{
			var sb = new StringBuilder();
			bool isFirst = true;
			foreach(var kvp in p) {
				if (!isFirst) { sb.Append(' '); }
				sb.Append(kvp.Key.Label);
				isFirst = false;
			}
			return sb.ToString();
		}

		static string TraitsToString(TraitCollection tc)
		{
			var sb = new StringBuilder();
			bool isFirst = true;
			foreach(var trait in tc) {
				if (!isFirst) { sb.Append(' '); }
				sb.Append(trait.Name+"="+trait.Value);
				isFirst = false;
			}
			return sb.ToString();
		}

		static string GetTestsPath()
		{
			string top = Assembly.GetExecutingAssembly().Location;
			string loc = Path.GetDirectoryName(top);
			return Path.Combine(loc,"test.dll");

			//string last = "";
			//while(!String.IsNullOrEmpty(top) && top != last) {
			//	last = top;
			//	top = new Uri(Path.Combine(top,"..")).LocalPath;
			//	string dn = Path.GetDirectoryName(top) ?? "";
			//	if (dn.EndsWith("AltMath")) {
			//		return dn;
			//	}
			//}
			//return Path.Combine(top,"test");
		}

		//static UnitTestElement ToUnitTestElement(this TestCase testCase, string source)
		//{
		//	bool isAsync = (testCase.GetPropertyValue(Constants.AsyncTestProperty) as bool?) ?? false;
		//	string fullClassName = testCase.GetPropertyValue(Constants.TestClassNameProperty) as string;
		//	return new UnitTestElement(new TestMethod(testCase.get_DisplayName(), fullClassName, source, isAsync))
		//	{
		//		IsAsync = isAsync,
		//		TestCategory = (testCase.GetPropertyValue(Constants.TestCategoryProperty) as string[]),
		//		Priority = (testCase.GetPropertyValue(Constants.PriorityProperty) as int?)
		//	};
		//}
	}

	class Logger : IMessageLogger
	{
		public void SendMessage(TestMessageLevel level, string message)
		{
			string prefix = "";
			switch(level) {
				case TestMessageLevel.Error:
					prefix = "E: "; break;
				case TestMessageLevel.Informational:
					prefix = "I: "; break;
				case TestMessageLevel.Warning:
					prefix = "W: "; break;
			}

			Console.WriteLine(prefix + message);
		}
	}

	class Sink : ITestCaseDiscoverySink
	{
		public Sink(IList<TestCase> store)
		{
			Store = store;
		}
		IList<TestCase> Store;

		public void SendTestCase(TestCase discoveredTest)
		{
			Store.Add(discoveredTest);
		}
	}
}
