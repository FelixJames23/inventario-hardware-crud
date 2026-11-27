using inventario_hardware_crud.Data;

var storage = new InventarioStorage();
bool salir = false;

while (!salir)
{
    Console.WriteLine("\n--- INVENTARIO DE HARDWARE ---");
    Console.WriteLine("1. Registrar componente");
    Console.WriteLine("2. Listar componentes");
    Console.WriteLine("3. Actualizar stock y precio");
    Console.WriteLine("4. Eliminar componente");
    Console.WriteLine("5. Buscar por categoria");
    Console.WriteLine("0. Salir");
    Console.Write("Selecciona una opcion: ");

    string? opcion = Console.ReadLine();

    switch (opcion)
    {
        case "0":
            salir = true;
            break;
        default:
            Console.WriteLine("Opcion aun no disponible.");
            break;
    }
}