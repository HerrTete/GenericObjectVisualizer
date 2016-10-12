using System.Collections.Generic;

namespace GenericObjectVisualizer.Demo
{
    public class TestObject4
    {
        public TestObject4()
        {
            InnerList = new List<TestObject3>();
            InnerList.Add(new TestObject3());
            InnerList.Add(new TestObject3 { Hans = "Branz" });
        }
        public List<TestObject3> InnerList { get; set; } 
    }
}