namespace Pruebas.Blazor.Covid.Models
{
    public class CovidData
    {
        public string DateChecked { get; set; } = string.Empty;
        public int PositiveIncrease { get; set; }
        public int DeathIncrease { get; set; }
        // Agrega más propiedades según la API
    }

}
