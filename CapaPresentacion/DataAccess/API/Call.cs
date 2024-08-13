using CapaPresentacion.DataAccess.API;
using System.Text;
using System.Text.Json;

public class ClienteService
{
    private readonly HttpClient _httpClient;

    public ClienteService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public void CrearCliente(Cliente cliente)
    {
        var json = JsonSerializer.Serialize(cliente);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            var response = _httpClient.PostAsync("https://localhost:44309/api/Clientes", content).Result;
            response.EnsureSuccessStatusCode();

            var responseData = response.Content.ReadAsStringAsync().Result;
            var createdCliente = JsonSerializer.Deserialize<Cliente>(responseData);
        }
        catch (HttpRequestException e)
        {
        }
    }

    public List<ClienteResponse> ObtenerClientes()
    {
        try
        {
            var response = _httpClient.GetAsync("https://localhost:44309/api/Clientes").Result;
            response.EnsureSuccessStatusCode();

            string responseData = response.Content.ReadAsStringAsync().Result;


            List<ClienteResponse> clientes = JsonSerializer.Deserialize<List<ClienteResponse>>(responseData);
            return clientes;
        }
        catch (HttpRequestException e)
        {
            return new List<ClienteResponse>();
        }
    }

    public Cliente ObtenerClientePorId(int id)
    {
        try
        {
            var response = _httpClient.GetAsync($"https://localhost:44309/api/Clientes/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                var responseData = response.Content.ReadAsStringAsync().Result;
                var cliente = JsonSerializer.Deserialize<Cliente>(responseData);
                return cliente;
            }
            else
            {
                return null;
            }
        }
        catch (HttpRequestException e)
        {
            return null;
        }
    }

    public void ActualizarCliente(int id, Cliente cliente)
    {
        cliente.Id = id;
        var json = JsonSerializer.Serialize(cliente);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            var response = _httpClient.PutAsync($"https://localhost:44309/api/Clientes/{id}", content).Result;
            if (response.IsSuccessStatusCode)
            {
                //Console.WriteLine("Cliente actualizado.");
            }
            else
            {
                //Console.WriteLine($"Error al actualizar cliente: {response.ReasonPhrase}");
            }
        }
        catch (HttpRequestException e)
        {
            //Console.WriteLine($"Error al actualizar cliente: {e.Message}");
        }
    }

    public void EliminarCliente(int id)
    {
        try
        {
            var response = _httpClient.DeleteAsync($"https://localhost:44309/api/Clientes/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
               // Console.WriteLine("Cliente eliminado.");
            }
            else
            {
               //  Console.WriteLine($"Error al eliminar cliente: {response.ReasonPhrase}");
            }
        }
        catch (HttpRequestException e)
        {
            // Console.WriteLine($"Error al eliminar cliente: {e.Message}");
        }
    }

    public List<Cliente> BuscarClientes(string nombre, string correoElectronico)
    {
        try
        {
            var url = $"https://localhost:44309/api/Clientes/search?nombre={nombre}&correoElectronico={correoElectronico}";
            var response = _httpClient.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();

            var responseData = response.Content.ReadAsStringAsync().Result;
            var clientes = JsonSerializer.Deserialize<List<Cliente>>(responseData);
            return clientes;
        }
        catch (HttpRequestException e)
        {
            // Console.WriteLine($"Error al buscar clientes: {e.Message}");
            return new List<Cliente>();
        }
    }
}
