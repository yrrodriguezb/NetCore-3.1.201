using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace ToXML
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateXml();
            QueryXml();
        }

        private static void QueryXml()
        {
            var document = XDocument.Load("fuel.xml");

            var ns = (XNamespace)"https://pluralsight.com/cars/2020";
            var ex = (XNamespace)"https://pluralsight.com/cars/2020/ex";

            var query = 
                from element in document.Element(ns  +"Cars")?.Elements(ex + "Car") 
                                ?? Enumerable.Empty<XElement>() 
                
                // or document.Descendants("Car")
                // Pero existe la posibilidad de extraer otros elements en un nodo diferente a "Cars"

                where element.Attribute("Manufacturer")?.Value == "BMW"
                select element.Attribute("Name").Value;

            foreach (var Name in query)
            {
                Console.WriteLine(Name);
            }

        }

        private static void CreateXml()
        {
            var records = ProcessCar("fuel.csv");

            var ns = (XNamespace)"https://pluralsight.com/cars/2020";
            var ex = (XNamespace)"https://pluralsight.com/cars/2020/ex";

            var document = new XDocument();
            var cars = new XElement(ns + "Cars");

            // Con XML
            // var elements = WithXML(records);

            // Con Linq
            var elements = WithLinq(records);

            cars.Add(elements);
            cars.Add(new XAttribute(XNamespace.Xmlns + "ex", ex));

            document.Add(cars);
            document.Save("fuel.xml");
        }

        private static List<Car> ProcessCar(string path)
        {
            // Consulta, Por extension de método
            return File.ReadAllLines(path)
                .Skip(2) // Omite las dos primeras lineas
                .Where(line => line.Length > 1)
                .ToCar()
                .ToList();
        }

        private static List<Manufacturer> ProcessManufacturers(string path)
        {
            var query = 
                File.ReadAllLines(path)
                    .Where(l => l.Length > 1)
                    .ToManufacturer();

            return query.ToList();
        }

        private static XElement WithXML (List<Car> records)
        {
            var cars = new XElement("Cars");

            var ex = (XNamespace)"https://pluralsight.com/cars/2020/ex";

            foreach (var record in records)
            {
                /* 
                var name = new XAttribute("Name", record.Name);
                var combined = new XAttribute("Combined", record.Combined);
                var car = new XElement("Car", name, combined);
                */

                // or 
                var car = new XElement(ex + "Car", 
                    new XAttribute("Name", record.Name),
                    new XAttribute("Combined", record.Combined),
                    new XAttribute("Manufacturer", record.Manufacturer)
                );

                cars.Add(car);
            }

            return cars;
        }

        private static IEnumerable<XElement> WithLinq (List<Car> records)
        {
            var ex = (XNamespace)"https://pluralsight.com/cars/2020/ex";

            var elements =
                from record in records
                select new XElement(ex +"Car", 
                    new XAttribute("Name", record.Name),
                    new XAttribute("Combined", record.Combined),
                    new XAttribute("Manufacturer", record.Manufacturer)
                );

            return elements;
        }
    }
}
