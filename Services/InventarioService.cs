using inventario_hardware_crud.Data;
using inventario_hardware_crud.Models;

namespace inventario_hardware_crud.Services
{
    public class InventarioService
    {
        private readonly InventarioStorage _storage;

        public InventarioService()
        {
            _storage = new InventarioStorage();
        }

        public List<Componente> ObtenerTodos()
        {
            return _storage.Cargar();
        }

        public void AgregarComponente(Componente componente)
        {
            var lista = _storage.Cargar();
            componente.Id = lista.Count > 0 ? lista.Max(c => c.Id) + 1 : 1;
            lista.Add(componente);
            _storage.Guardar(lista);
        }
    }
}