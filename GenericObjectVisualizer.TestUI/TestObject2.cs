using System;

namespace GenericObjectVisualizer.TestUI
{
    public class TestObject2
    {
        public TestObject2()
        {
            Id = Guid.NewGuid().ToString();
            Zauber = 777;
            ChildChild = new TestObject3();
        }
        public string Id { get; set; }
        public int Zauber { get; set; }
        public TestObject3 ChildChild { get; set; }
        public TestObject3 Leer { get; set; }
    }
}