using System;
using System.Collections.Generic;

namespace Cars
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
                    Name = columns[0], 
                    MPG = double.Parse(columns[1]),
                    Cylinders = int.Parse(columns[2]),
                    Displacement = double.Parse(columns[3]),
                    Horsepower = double.Parse(columns[4]),
                    Weight = double.Parse(columns[5]),
                    Acceleration = double.Parse(columns[6]),
                    Model = int.Parse(columns[7]),
                    Origin = columns[8]
                };   
            }
        }
    }
}