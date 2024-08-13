using CapaPresentacion.DataAccess.API;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace CapaPresentacion
{
    public partial class Demo : Form
    {
        public Demo()
        {
            InitializeComponent();
        }

        private static readonly HttpClient _httpClient = new HttpClient();

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var clienteService = new ClienteService(_httpClient);
            var nuevoCliente = new Cliente
            {
                Nombre = txtNombre.Text,
                Edad = txtEdad.Text,
                Direccion = txtDireccion.Text,
                CorreoElectronico = txtCorreoElectronico.Text
            };

            clienteService.CrearCliente(nuevoCliente);
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            var clienteService = new ClienteService(_httpClient);
            clienteService.ObtenerClientes();

            dataGridCliente.DataSource = clienteService.ObtenerClientes();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var clienteService = new ClienteService(_httpClient);
            var nuevoCliente = new Cliente
            {
                Nombre = txtNombre.Text,
                Edad = txtEdad.Text,
                Direccion = txtDireccion.Text,
                CorreoElectronico = txtCorreoElectronico.Text
            };

            clienteService.ActualizarCliente(Convert.ToInt16(txtID.Text), nuevoCliente);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var clienteService = new ClienteService(_httpClient);
            clienteService.EliminarCliente(Convert.ToInt16(txtID.Text));
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var clienteService = new ClienteService(_httpClient);
            SearchClientesRequest search = ExtraerNombreOCorreo(txtSearch.Text);
            dataGridCliente.DataSource = clienteService.BuscarClientes(search.Nombre, search.CorreoElectronico);
        }

        public SearchClientesRequest ExtraerNombreOCorreo(string input)
        {
            string nombre = string.Empty;
            string correo = string.Empty;

            // Patrón para el correo electrónico
            string correoPattern = @"\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Z|a-z]{2,}\b";
            var correoMatch = Regex.Match(input, correoPattern);
            if (correoMatch.Success)
            {
                correo = correoMatch.Value;
            }

            // Patrón para el nombre (asumiendo que es cualquier cadena que no es un correo)
            string nombrePattern = @"\b(?![A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Z|a-z]{2,})\w+\b";
            var nombreMatch = Regex.Match(input, nombrePattern);
            if (nombreMatch.Success)
            {
                nombre = nombreMatch.Value;
            }

            return new SearchClientesRequest { Nombre = nombre, CorreoElectronico = correo};
        }
    }
}
