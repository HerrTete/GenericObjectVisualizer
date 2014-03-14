using GenericObjectVisualizer.TestUI;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenericObjectVisualizer.Test
{
    [TestClass]
    public class KeyValueConverterTest
    {
        [TestMethod]
        public void ConvertFromObjectTest()
        {
            var converter = new KeyValueConverter();
            var testObject = new TestObject1();
            var result = converter.ConvertFromObject(testObject);
            Assert.IsTrue(result.Count >= 25);
        }

        [TestMethod]
        public void ConvertToObjectTest()
        {
            var converter = new KeyValueConverter();
            var testObject = new TestObject1();
            var result = converter.ConvertFromObject(testObject);
            object newTestObject = converter.ConvertToObject(result, testObject);
            Assert.AreEqual(testObject, newTestObject);
        }

        [TestMethod]
        public void ConvertBackWithChangesObjectTest()
        {
            var converter = new KeyValueConverter();
            var testObject = new TestObject1();
            var result = converter.ConvertFromObject(testObject);
            foreach (var propertyVisualizerInformationse in result)
            {
                if (propertyVisualizerInformationse.Value == "Hans")
                {
                    propertyVisualizerInformationse.Value = "Hanz";
                }
            }
            object newTestObject = converter.ConvertToObject(result, testObject);
            Assert.AreEqual(testObject, newTestObject);
            Assert.AreEqual("Hanz", (newTestObject as TestObject1).Liste[0]);
        }
    }
}
