using System.Collections.Generic;

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
            var testObject = new TestObject1();
            var result = KeyValueConverter.ConvertFromObject(testObject);
            Assert.IsTrue(result.Count >= 25);
        }

        [TestMethod]
        public void ConvertToObjectTest()
        {
            var testObject = new TestObject1();
            var result = KeyValueConverter.ConvertFromObject(testObject);
            object newTestObject = KeyValueConverter.ConvertToObject(result, testObject);
            Assert.AreEqual(testObject, newTestObject);
        }

        [TestMethod]
        public void ConvertBackWithChangesObjectTest()
        {
            var testObject = new TestObject1();
            var result = KeyValueConverter.ConvertFromObject(testObject);
            foreach (var propertyVisualizerInformationse in result)
            {
                if (propertyVisualizerInformationse.Value == "Hans")
                {
                    propertyVisualizerInformationse.Value = "Hanz";
                }
            }
            object newTestObject = KeyValueConverter.ConvertToObject(result, testObject);
            Assert.AreEqual(testObject, newTestObject);
            Assert.AreEqual("Hanz", (newTestObject as TestObject1).Liste[0]);
        }

        [TestMethod]
        public void ConvertFromObjectEnumerationTest1()
        {
            var testObject = new EnumTest1();
            var result = KeyValueConverter.ConvertFromObject(testObject);
            Assert.IsTrue(result.Count == 4);
            Assert.AreEqual("StringListe[0]", result[0].Name);
            Assert.AreEqual("StringListe[1]", result[1].Name);
            Assert.AreEqual("StringListe[2]", result[2].Name);
            Assert.AreEqual("StringListe[3]", result[3].Name);
            Assert.AreEqual("Das", result[0].Value);
            Assert.AreEqual("ist", result[1].Value);
            Assert.AreEqual("ein", result[2].Value);
            Assert.AreEqual("Test.", result[3].Value);

            //Änderungen machen
            foreach (var propertyVisualizerInformationse in result)
            {
                propertyVisualizerInformationse.Value += "TEST";
            }
            var reconvertedTestObject = KeyValueConverter.ConvertToObject(result, testObject) as EnumTest1;

            Assert.IsNotNull(reconvertedTestObject);
            foreach (var stringEintrag in reconvertedTestObject.StringListe)
            {
                Assert.IsTrue(stringEintrag.EndsWith("TEST"));
            }
        }

        [TestMethod]
        public void ConvertFromObjectEnumerationTest2()
        {
            var testObject = new EnumTest2();
            var result = KeyValueConverter.ConvertFromObject(testObject);
            Assert.IsTrue(result.Count == 6);
            Assert.AreEqual("Name", result[0].Name);
            Assert.AreEqual("Vorname", result[1].Name);
            Assert.AreEqual("Name", result[2].Name);
            Assert.AreEqual("Vorname", result[3].Name);
            Assert.AreEqual("Name", result[4].Name);
            Assert.AreEqual("Vorname", result[5].Name);

            Assert.AreEqual("ObjectListe[0]", result[0].Path);
            Assert.AreEqual("ObjectListe[0]", result[1].Path);
            Assert.AreEqual("ObjectListe[1]", result[2].Path);
            Assert.AreEqual("ObjectListe[1]", result[3].Path);
            Assert.AreEqual("ObjectListe[2]", result[4].Path);
            Assert.AreEqual("ObjectListe[2]", result[5].Path);

            Assert.AreEqual("Schmidt", result[0].Value);
            Assert.AreEqual("Klaus-Dieter", result[1].Value);
            Assert.AreEqual("Meier", result[2].Value);
            Assert.AreEqual("Karl", result[3].Value);
            Assert.AreEqual("Peter", result[4].Value);
            Assert.AreEqual("Hans", result[5].Value);

            //Änderungen machen
            foreach (var propertyVisualizerInformationse in result)
            {
                propertyVisualizerInformationse.Value += "TEST";
            }
            var reconvertedTestObject = KeyValueConverter.ConvertToObject(result, testObject) as EnumTest2;

            Assert.IsNotNull(reconvertedTestObject);
            foreach (var person in reconvertedTestObject.ObjectListe)
            {
                Assert.IsTrue(person.Name.EndsWith("TEST"));
                Assert.IsTrue(person.Vorname.EndsWith("TEST"));
            }
        }

        [TestMethod]
        public void ConvertFromObjectEnumerationTest3()
        {
            var testObject = new EnumTest3();
            var result = KeyValueConverter.ConvertFromObject(testObject);
            Assert.IsTrue(result.Count == 4);
            Assert.AreEqual("Name", result[0].Name);
            Assert.AreEqual("Vorname", result[1].Name);
            Assert.AreEqual("Name", result[2].Name);
            Assert.AreEqual("Vorname", result[3].Name);

            Assert.AreEqual("Gruppe\\Personen[0]", result[0].Path);
            Assert.AreEqual("Gruppe\\Personen[0]", result[1].Path);
            Assert.AreEqual("Gruppe\\Personen[1]", result[2].Path);
            Assert.AreEqual("Gruppe\\Personen[1]", result[3].Path);

            Assert.AreEqual("Müller", result[0].Value);
            Assert.AreEqual("Lieschen", result[1].Value);
            Assert.AreEqual("Peter", result[2].Value);
            Assert.AreEqual("Hans", result[3].Value);

            //Änderungen machen
            foreach (var propertyVisualizerInformationse in result)
            {
                propertyVisualizerInformationse.Value += "TEST";
            }
            var reconvertedTestObject = KeyValueConverter.ConvertToObject(result, testObject) as EnumTest3;

            Assert.IsNotNull(reconvertedTestObject);
            foreach (var person in reconvertedTestObject.Gruppe.Personen)
            {
                Assert.IsTrue(person.Name.EndsWith("TEST"));
                Assert.IsTrue(person.Vorname.EndsWith("TEST"));
            }
        }

        [TestMethod]
        public void ConvertFromObjectEnumerationTest4()
        {
            var testObject = new EnumTest4();
            var result = KeyValueConverter.ConvertFromObject(testObject);
            Assert.IsTrue(result.Count == 5);
            Assert.AreEqual("Klassenname", result[0].Name);
            Assert.AreEqual("Name", result[1].Name);
            Assert.AreEqual("Vorname", result[2].Name);
            Assert.AreEqual("Einträge[0]", result[3].Name);
            Assert.AreEqual("Einträge[1]", result[4].Name);

            Assert.AreEqual("Klassenbuch\\", result[0].Path);
            Assert.AreEqual("Klassenbuch\\Klassensprecher\\", result[1].Path);
            Assert.AreEqual("Klassenbuch\\Klassensprecher\\", result[2].Path);
            Assert.AreEqual("Klassenbuch\\", result[3].Path);
            Assert.AreEqual("Klassenbuch\\", result[4].Path);

            Assert.AreEqual("3c", result[0].Value);
            Assert.AreEqual("Streber", result[1].Value);
            Assert.AreEqual("Ober", result[2].Value);
            Assert.AreEqual("Alles in Bester Ordnung", result[3].Value);
            Assert.AreEqual("...oder auch nicht.", result[4].Value);

            //Änderungen machen
            foreach (var propertyVisualizerInformationse in result)
            {
                propertyVisualizerInformationse.Value += "TEST";
            }
            var reconvertedTestObject = KeyValueConverter.ConvertToObject(result, testObject) as EnumTest4;

            Assert.IsNotNull(reconvertedTestObject);
            foreach (var eintrag in reconvertedTestObject.Klassenbuch.Einträge)
            {
                Assert.IsTrue(eintrag.EndsWith("TEST"));
                Assert.IsTrue(eintrag.EndsWith("TEST"));
            }

            Assert.IsTrue(reconvertedTestObject.Klassenbuch.Klassenname.EndsWith("TEST"));

            Assert.IsTrue(reconvertedTestObject.Klassenbuch.Klassensprecher.Name.EndsWith("TEST"));
            Assert.IsTrue(reconvertedTestObject.Klassenbuch.Klassensprecher.Vorname.EndsWith("TEST"));
        }

        [TestMethod]
        public void ConvertStringTest()
        {
            var inputString = "Das ist ein Test";
            var newString = "Das war ein Test.";
            var result = KeyValueConverter.ConvertFromObject(inputString);
            Assert.IsTrue(result.Count == 1);
            Assert.AreEqual(inputString, result[0].Value);
            
            //Änderung machen
            result[0].Value = newString;

            var reconvertedString = KeyValueConverter.ConvertToObject(result, inputString);
            Assert.AreEqual(newString, reconvertedString);
        }
    }

    public class EnumTest4
    {
        public EnumTest4()
        {
            Klassenbuch = new Klassenbuch();
        }
        public Klassenbuch Klassenbuch { get; set; }
    }

    public class Klassenbuch
    {
        public Klassenbuch()
        {
            Klassensprecher = new Person { Name = "Streber", Vorname = "Ober" };
            Klassenname = "3c";
            Einträge = new List<string>();
            Einträge.Add("Alles in Bester Ordnung");
            Einträge.Add("...oder auch nicht.");
        }

        public string Klassenname { get; set; }

        public Person Klassensprecher { get; set; }

        public List<string> Einträge { get; set; }
    }

    public class EnumTest1
    {
        public EnumTest1()
        {
            StringListe = new List<string>();
            StringListe.Add("Das");
            StringListe.Add("ist");
            StringListe.Add("ein");
            StringListe.Add("Test.");
        }
        public List<string> StringListe { get; set; }
    }

    public class EnumTest2
    {
        public EnumTest2()
        {
            ObjectListe = new List<Person>();
            ObjectListe.Add(new Person { Name = "Schmidt", Vorname = "Klaus-Dieter" });
            ObjectListe.Add(new Person { Name = "Meier", Vorname = "Karl" });
            ObjectListe.Add(new Person());
        }
        public List<Person> ObjectListe { get; set; }
    }

    public class EnumTest3
    {
        public EnumTest3()
        {
            Gruppe = new Gruppe();
        }
        public Gruppe Gruppe { get; set; }
    }

    public class Person
    {
        public Person()
        {
            Name = "Peter";
            Vorname = "Hans";
        }

        public string Name { get; set; }
        public string Vorname { get; set; }
    }

    public class Gruppe
    {
        public Gruppe()
        {
            Personen = new List<Person>();
            Personen.Add(new Person{Name = "Müller", Vorname = "Lieschen"});
            Personen.Add(new Person{Name = "Peter", Vorname = "Hans"});
        }

        public List<Person> Personen { get; set; } 
    }
}
