namespace ApiBarberApp.Utilities
{
    public class GeneralException : Exception
    {
        public int StatusCode { get; }

        public GeneralException(string mensaje, int codigo) : base(mensaje)
        {
            StatusCode = codigo;
        }
    }
}
