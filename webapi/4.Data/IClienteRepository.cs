public interface IClienteRepository
{
    void AddCliente(Cliente cliente);
    IEnumerable<Cliente> GetClientes();
    Cliente GetClienteById(int id);
    void UpdateCliente(Cliente cliente);
    void DeleteCliente(int id);
    IEnumerable<Cliente> SearchClientes(string nombre = null, string correoElectronico = null);
}
