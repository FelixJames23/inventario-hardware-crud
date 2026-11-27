using inventario_hardware_crud.Models;
using inventario_hardware_crud.Services;

var servicio = new InventarioService();
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
        case "1":
            RegistrarNuevo(servicio);
            break;
        case "2":
            MostrarInventario(servicio);
            break;
        case "3":
            ActualizarComponente(servicio);
            break;
        case "4":
            EliminarItem(servicio);
            break;
        case "5":
            FiltrarPorCategoria(servicio);
            break;
        case "0":
            salir = true;
            break;
        default:
            Console.WriteLine("Opcion no valida.");
            break;
    }
}

static void RegistrarNuevo(InventarioService servicio)
{
    Console.Write("\nNombre del componente: ");
    string nombre = Console.ReadLine() ?? "";

    Console.Write("Categoria (CPU, GPU, RAM, etc.): ");
    string categoria = Console.ReadLine() ?? "";

    Console.Write("Marca: ");
    string marca = Console.ReadLine() ?? "";

    Console.Write("Precio: ");
    if (!decimal.TryParse(Console.ReadLine(), out decimal precio))
    {
        Console.WriteLine("Precio invalido.");
        return;
    }

    Console.Write("Stock inicial: ");
    if (!int.TryParse(Console.ReadLine(), out int stock))
    {
        Console.WriteLine("Cantidad invalida.");
        return;
    }

    var nuevo = new Componente
    {
        Nombre = nombre,
        Categoria = categoria,
        Marca = marca,
        Precio = precio,
        Stock = stock
    };

    servicio.AgregarComponente(nuevo);
    Console.WriteLine("Componente registrado correctamente.");
}

static void MostrarInventario(InventarioService servicio)
{
    var lista = servicio.ObtenerTodos();
    if (lista.Count == 0)
    {
        Console.WriteLine("\nNo hay componentes registrados.");
        return;
    }

    Console.WriteLine("\n--- LISTA DE COMPONENTES ---");
    foreach (var c in lista)
    {
        Console.WriteLine($"ID: {c.Id} | {c.Nombre} ({c.Marca}) - Cat: {c.Categoria} | Precio: ${c.Precio:N2} | Stock: {c.Stock}");
    }
}

static void ActualizarComponente(InventarioService servicio)
{
    Console.Write("\nID del componente a actualizar: ");
    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("ID invalido.");
        return;
    }

    Console.Write("Nuevo precio: ");
    if (!decimal.TryParse(Console.ReadLine(), out decimal nuevoPrecio))
    {
        Console.WriteLine("Precio invalido.");
        return;
    }

    Console.Write("Nuevo stock disponible: ");
    if (!int.TryParse(Console.ReadLine(), out int nuevoStock))
    {
        Console.WriteLine("Stock invalido.");
        return;
    }

    bool actualizado = servicio.ActualizarStockYPrecio(id, nuevoPrecio, nuevoStock);
    if (actualizado)
        Console.WriteLine("Componente actualizado correctamente.");
    else
        Console.WriteLine("No se encontro ningun componente con ese ID.");
}

static void EliminarItem(InventarioService servicio)
{
    Console.Write("\nID del componente a eliminar: ");
    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("ID invalido.");
        return;
    }

    Console.Write("Seguro que deseas eliminar este componente? (s/n): ");
    string? confirmar = Console.ReadLine();
    if (confirmar?.Trim().ToLower() != "s")
    {
        Console.WriteLine("Operacion cancelada.");
        return;
    }

    bool eliminado = servicio.EliminarComponente(id);
    if (eliminado)
        Console.WriteLine("Componente eliminado del inventario.");
    else
        Console.WriteLine("No se encontro ningun componente con ese ID.");
}

static void FiltrarPorCategoria(InventarioService servicio)
{
    Console.Write("\nIngresa la categoria a buscar (ej. GPU, CPU, RAM): ");
    string categoria = Console.ReadLine() ?? "";

    var resultados = servicio.BuscarPorCategoria(categoria);
    if (resultados.Count == 0)
    {
        Console.WriteLine("No se encontraron componentes en esa categoria.");
        return;
    }

    Console.WriteLine($"\n--- RESULTADOS PARA '{categoria.ToUpper()}' ---");
    foreach (var c in resultados)
    {
        Console.WriteLine($"ID: {c.Id} | {c.Nombre} ({c.Marca}) | Precio: ${c.Precio:N2} | Stock: {c.Stock}");
    }
}