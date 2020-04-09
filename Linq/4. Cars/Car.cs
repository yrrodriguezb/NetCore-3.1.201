using System;
using System.Linq;

namespace Cars
{
    public class Car
    {
        public string Name { get; set; }
        public double MPG { get; set; }
        public int Cylinders { get; set; }
        public double Displacement { get; set; }
        public double Horsepower { get; set; }
        public double Weight { get; set; }
        public double Acceleration { get; set; }
        public int Model { get; set; }
        public string Origin { get; set; }

        internal static Car ParseFromCsv(string line)
        {
            var columns = line.Split(';');

            return new Car 
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