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

        public bool AgregarComponente(Componente componente)
        {
            if (string.IsNullOrWhiteSpace(componente.Nombre) || string.IsNullOrWhiteSpace(componente.Categoria))
                return false;

            if (componente.Precio <= 0 || componente.Stock < 0)
                return false;

            var lista = _storage.Cargar();
            componente.Id = lista.Count > 0 ? lista.Max(c => c.Id) + 1 : 1;
            lista.Add(componente);
            _storage.Guardar(lista);
            return true;
        }

        public bool ActualizarStockYPrecio(int id, decimal nuevoPrecio, int nuevoStock)
        {
            if (nuevoPrecio <= 0 || nuevoStock < 0)
                return false;

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

        public List<Componente> BuscarPorCategoria(string categoria)
        {
            var lista = _storage.Cargar();
            return lista
                .Where(c => c.Categoria.Equals(categoria.Trim(), StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}