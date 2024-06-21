
# TaskManage - Blazor Server Application

## Descripción

TaskManager es una aplicación desarrollada con Blazor Server que se conecta a una API para administrar metas y tareas. Esta aplicación permite a los usuarios gestionar sus objetivos y las tareas relacionadas.

## Requisitos Previos

- .NET 6.0 SDK o superior
- SQL Server

## Configuración

### Paso 1: Cambiar el Puerto del Equipo Local para la URL de la API

1. Abre el archivo `AppService.cs` en el proyecto.
2. Encuentra la línea donde se establece la URL de la API. Debería verse similar a esto:

   ```csharp
   private readonly string _baseURL = "https://localhost:5001/api";

3. Cambia el puerto de la URL según sea necesario para que coincida con el puerto de tu equipo local.

### Paso 2: Crear la Base de Datos

1. Abre el archivo script.sql que se encuentra en el directorio raíz del proyecto.
2. Ejecuta este script en tu servidor SQL para crear la base de datos necesaria para la aplicación.

### Paso 3:  Actualizar la Cadena de Conexión

1. Una vez creada la base de datos, abre el archivo `appsettings.json` en el proyecto `TaskManager.API`.

2. Encuentra la sección `ConnectionStrings` y localiza la entrada `dbTaskManager`.

3. Actualiza la cadena de conexión para que apunte a la base de datos que acabas de crear. Debería verse similar a esto:

`"ConnectionStrings": {
    "dbTaskManager": "Server=YOUR_SERVER;Database=TaskManagerDb;User Id=YOUR_USER;Password=YOUR_PASSWORD;"
}`

NOTA: Asegúrate de reemplazar YOUR_SERVER, YOUR_USER y YOUR_PASSWORD con los valores correspondientes de tu configuración.


### CONTACTO
Si tienes alguna pregunta o problema, no dudes en ponerte en contacto a través de contact.jfo117@gmail.com

# ¡Gracias por usar TaskManager!

