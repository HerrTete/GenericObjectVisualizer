using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericObjectVisualizer.Client.Console
{
    public class TestObject
    {
        public TestObject()
        {
            Name = "Klaus-Dieter HansWurst";
            Klasse = "80D";
            GeburtsDatum = new DateTime(1985, 10, 30, 7, 27, 00);
            Nummer = 6;
            Durchschnitt = 7.777;
            Alter = DateTime.Now - GeburtsDatum;
            Liste = new List<string>();
            Liste.Add("Hans");
            Liste.Add("im");
            Liste.Add("Glück");
        }

        public List<string> Liste { get; set; }
        public string Name { get; set; }
        public string Klasse { get; set; }
        public int Nummer { get; set; }
        public double Durchschnitt { get; set; }
        public DateTime GeburtsDatum { get; set; }
        public TimeSpan Alter { get; set; }
    }

}
