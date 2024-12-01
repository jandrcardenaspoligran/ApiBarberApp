namespace ApiBarberApp.Models
{
    public class RespuestaGeneral<T>
    {
        public string? mensaje {  get; set; }
        public EstadosRespuesta? estado { get; set; } = EstadosRespuesta.NONE;
        public string? error { get; set; }
        public string? token { get; set; }
        public T? objeto { get; set; }
    }

    public enum EstadosRespuesta
    {   
        /// <summary>
        /// Estado para uso en caso de que el proceso sea exitoso y retorne la posible respuesta esperada por el usuario.
        /// </summary>
        SUCCESS,
        /// <summary>
        /// Estadp para usar en caso de que el proceso tenga una ejecución exitosa, sin errores, pero no sea la posible respuesta esperada por el usuario debido a que no cumplió con alguna validación.
        /// </summary>
        UNSUCCESSFUL,
        /// <summary>
        /// Estado para usar en caso de que el proceso presente una excepción del tipo GeneralException, la cual es controlada y arrojada adrede.
        /// </summary>
        FAILURE,
        /// <summary>
        /// Estado para usar en caso de que el proceso tenga un error y arroje una excepción. 
        /// </summary>
        ERROR,
        /// <summary>
        /// Estado inicial de EstadosRespuestas. No tiene un estado definido. 
        /// </summary>
        NONE
    }
}
