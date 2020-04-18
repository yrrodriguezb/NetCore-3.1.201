using System;

namespace Methods_Delegates
{
    class Program
    {
        static void Main(string[] args)
        {
            // bufferSample();

            bufferEventSample();
        }

        static void ConsoleWrite(double data)
        {
            Console.WriteLine(data);
        }

        private static void bufferEventSample()
        {
            var buffer = new CircularBuffer<double>(capacity: 3);

            buffer.ItemDiscarded += buffer_ItemDiscarded;

            ProcessInput(buffer);

            buffer.Dump(d => Console.WriteLine(d));

            ProcessBuffer(buffer);
        }

        private static void bufferSample()
        {
            var buffer = new Buffer<double>();

            ProcessInput(buffer);

            // Metodo de estensión para imprimir por pantalla los datos ingresados
            buffer.Dump(ConsoleWrite);

            Console.WriteLine("\nFunción con expresión lambda\n");

            buffer.Dump(d => Console.WriteLine(d));

            Console.WriteLine("\nConvertidor personalizado\n");

            // Usando un convertidor personalizado 
            Converter<double, DateTime> converter = d => DateTime.Now.AddDays(d);

            var aDates = buffer.Map(converter);
            foreach (var date in aDates)
            {
                Console.WriteLine(date);
            }

            Console.WriteLine("\nConvertidor personalizado con expresión lambda\n");

            var dates = buffer.Map(d => new DateTime().AddDays(d));
            foreach (var date in dates)
            {
                Console.WriteLine(date);
            }

            ProcessBuffer(buffer);
        }

        static void buffer_ItemDiscarded(object sender, ItemDiscardedEventArgs<double> e)
        {
            Console.WriteLine("Buffer full. Discarding {0}, new item is {1}", e.ItemDiscarded, e.NewItem);
        }

        private static void ProcessBuffer(IBuffer<double> buffer)
        {
            var sum = 0.0;

            Console.WriteLine("Buffer: ");

            while (!buffer.IsEmpty)
            {
                sum += buffer.Read();
            }

            Console.WriteLine(sum);
        }

        private static void ProcessInput(IBuffer<double> buffer)
        {
            while (true)
            {
                var value = 0.0;
                var input = Console.ReadLine();

                if (double.TryParse(input, out value))
                {
                    buffer.Write(value);
                    continue;
                }

                break;
            }
        }
    }
}
