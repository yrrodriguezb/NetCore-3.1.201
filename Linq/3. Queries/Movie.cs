namespace Queries
{
    public class Movie
    {
        public string Title { get; set; }
        public float Rating { get; set; }

        int _year;
        public int Year 
        { 
            get 
            {
                System.Console.WriteLine($"Retornando {_year} para la pelicula {Title}");
                return _year;
            } 
            set 
            {
                _year = value;
            }
        }
    }
}