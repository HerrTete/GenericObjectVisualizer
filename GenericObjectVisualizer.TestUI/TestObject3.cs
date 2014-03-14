using System;
using System.Collections.Generic;

namespace GenericObjectVisualizer.TestUI
{
    public class TestObject3
    {
        public TestObject3()
        {
            Hans = "-Wurst";
            Klaus = "-Dieter";
            LiLaListe = new List<string>();
            LiLaListe.Add("Wie");
            LiLaListe.Add("geil");
            LiLaListe.Add("ist");
            LiLaListe.Add("das");
            LiLaListe.Add("denn?");
        }

        public string Hans { get; set; }
        public string Klaus { get; set; }
        public List<string> LiLaListe { get; set; } 
    }
}