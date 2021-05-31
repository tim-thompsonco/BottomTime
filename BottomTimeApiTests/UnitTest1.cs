using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BottomTimeApiTests {
	[TestClass]
	public class UnitTest1 {
		[TestMethod]
		public void TestMethod1() {
			const int testShouldPass = 2 + 2;

			Assert.AreEqual(4, testShouldPass);
		}
	}
}
