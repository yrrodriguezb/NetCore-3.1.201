using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace ToXML
{
    public static class CarExtensions
    {
        public static IEnumerable<Car> ToCar (this IEnumerable<string> source)
        {

            foreach (var line in source)
            {
                var columns = line.Split(';');

                yield return new Car 
                {
                    Year = int.Parse(columns[0]),
                    Manufacturer = columns[1],
                    Name = columns[2],
                    Displacement = double.Parse(columns[3]),
                    Cylinders = int.Parse(columns[4]),
                    City = int.Parse(columns[5]),
                    Highway = int.Parse(columns[6]),
                    Combined = int.Parse(columns[7])
                };   
            }
        }

        public static IEnumerable<XElement> ToXElement (this IEnumerable<Car> source)
        {
            foreach (var car in source)
            {
                yield return new XElement("Car", 
                    new XAttribute("Name", car?.Name),
                    new XAttribute("Combined", car?.Combined),
                    new XAttribute("Manufacturer", car?.Manufacturer)
                );;
            }
        }
    }
}