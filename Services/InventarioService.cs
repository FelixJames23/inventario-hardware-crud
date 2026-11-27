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

        public bool ActualizarStockYPrecio(int id, decimal nuevoPrecio, int nuevoStock)
        {
            var lista = _storage.Cargar();
            var item = lista.FirstOrDefault(c => c.Id == id);
            if (item == null)
                return false;

            item.Precio = nuevoPrecio;
            item.Stock = nuevoStock;
            _storage.Guardar(lista);
            return true;
        }

        public bool EliminarComponente(int id)
        {
            var lista = _storage.Cargar();
            var item = lista.FirstOrDefault(c => c.Id == id);
            if (item == null)
                return false;

            lista.Remove(item);
            _storage.Guardar(lista);
            return true;
        }
    }
}