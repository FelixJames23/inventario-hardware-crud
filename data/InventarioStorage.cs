using System.Text.Json;
using inventario_hardware_crud.Models;

namespace inventario_hardware_crud.Data
{
    public class InventarioStorage
    {
        private readonly string _rutaArchivo = "inventario.json";

        public List<Componente> Cargar()
        {
            if (!File.Exists(_rutaArchivo))
                return new List<Componente>();

            string json = File.ReadAllText(_rutaArchivo);
            if (string.IsNullOrWhiteSpace(json))
                return new List<Componente>();

            return JsonSerializer.Deserialize<List<Componente>>(json) ?? new List<Componente>();
        }

        public void Guardar(List<Componente> lista)
        {
            var opciones = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(lista, opciones);
            File.WriteAllText(_rutaArchivo, json);
        }
    }
}