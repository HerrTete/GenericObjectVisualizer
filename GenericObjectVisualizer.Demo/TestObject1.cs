using System;
using System.Collections.Generic;

namespace GenericObjectVisualizer.Demo
{
    public class TestObject1
    {
        public TestObject1()
        {
            Name = "Klaus-Dieter HansWurst";
            Klasse = "80D";
            GeburtsDatum = new DateTime(1985, 10, 30, 7, 27, 00);
            Nummer = 6;
            Durchschnitt = 7.777;
            Alter = DateTime.Now - GeburtsDatum;
            AnderesObjekt = new TestObject2();
            Child = new TestObject3();
            Liste = new List<string>();
            Liste.Add("Hans");
            Liste.Add("im");
            Liste.Add("Glück");
            KomplexeListe = new List<TestObject3>();
            KomplexeListe.Add(new TestObject3());
            KomplexeListe.Add(new TestObject3{Hans = "Branz"});
        }

        public List<string> Liste { get; set; }
        public string Name { get; set; }
        public string Klasse { get; set; }
        public int Nummer { get; set; }
        public double Durchschnitt { get; set; }
        public DateTime GeburtsDatum { get; set; }
        public TimeSpan Alter { get; set; }
        public TestObject2 AnderesObjekt { get; set; }
        public TestObject3 Child { get; set; }
        public List<TestObject3> KomplexeListe { get; set; } 
    }
}
