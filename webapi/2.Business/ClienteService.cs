public class ClienteService
{
    private readonly IClienteRepository _clienteRepository;

    public ClienteService(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public void AddCliente(Cliente cliente)
    {
        // Aquí podrías añadir validaciones o lógica adicional antes de llamar al repositorio
        _clienteRepository.AddCliente(cliente);
    }

    public IEnumerable<Cliente> GetClientes()
    {
        return _clienteRepository.GetClientes();
    }

    public Cliente GetClienteById(int id)
    {
        return _clienteRepository.GetClienteById(id);
    }

    public void UpdateCliente(Cliente cliente)
    {
        // Aquí podrías añadir validaciones o lógica adicional antes de llamar al repositorio
        _clienteRepository.UpdateCliente(cliente);
    }

    public void DeleteCliente(int id)
    {
        _clienteRepository.DeleteCliente(id);
    }

    public IEnumerable<Cliente> SearchClientes(string nombre = null, string correoElectronico = null)
    {
        return _clienteRepository.SearchClientes(nombre, correoElectronico);
    }
}
