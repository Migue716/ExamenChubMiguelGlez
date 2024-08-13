using System.Text.Json.Serialization;

namespace CapaPresentacion.DataAccess.API
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Edad { get; set; }
        public string Direccion { get; set; }
        public string CorreoElectronico { get; set; }
    }


    public class ClienteResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; }

        [JsonPropertyName("edad")]
        public string Edad { get; set; }

        [JsonPropertyName("direccion")]
        public string Direccion { get; set; }

        [JsonPropertyName("correoElectronico")]
        public string CorreoElectronico { get; set; }
    }

}
