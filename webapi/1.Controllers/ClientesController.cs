using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly ClienteService _clienteService;

    public ClientesController(ClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    [HttpGet]
    public IActionResult GetClientes()
    {
        var clientes = _clienteService.GetClientes();
        return Ok(clientes);
    }

    [HttpGet("{id}")]
    public IActionResult GetCliente(int id)
    {
        var cliente = _clienteService.GetClienteById(id);
        if (cliente == null)
            return NotFound();
        return Ok(cliente);
    }

    [HttpPost]
    public IActionResult CreateCliente([FromBody] Cliente cliente)
    {
        _clienteService.AddCliente(cliente);
        return CreatedAtAction(nameof(GetCliente), new { id = cliente.Id }, cliente);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateCliente(int id, [FromBody] Cliente cliente)
    {
        var existingCliente = _clienteService.GetClienteById(id);
        if (existingCliente == null)
            return NotFound();

        cliente.Id = id;
        _clienteService.UpdateCliente(cliente);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCliente(int id)
    {
        var cliente = _clienteService.GetClienteById(id);
        if (cliente == null)
            return NotFound();

        _clienteService.DeleteCliente(id);
        return NoContent();
    }

    [HttpGet("search")]
    public IActionResult SearchClientes([FromQuery] string nombre, [FromQuery] string correoElectronico)
    {
        var clientes = _clienteService.SearchClientes(nombre, correoElectronico);
        return Ok(clientes);
    }
}
