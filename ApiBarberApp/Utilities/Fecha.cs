namespace ApiBarberApp.Utilities
{
    public static class Fecha
    {
        public static DateTime Actual()
        {
            /*** Zona horaria en Colombia -5 horas ***/
            return DateTime.UtcNow.AddHours(1);
        } 
    }
}
