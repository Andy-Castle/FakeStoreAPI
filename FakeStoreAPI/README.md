Para ejecutar el proyecto realizar los siguientes pasos

"DefaultConnection": "Server=ANDY\\SQLEXPRESS;Database=FakeStorAPI;Trusted_Connection=True;TrustServerCertificate=True;"

1. Clonar el repositorio

2. Abrir el proyecto

3. Configurar la cadena de conexión en el archivo appsettings.json si es necesario

4. Ejecutar las migraciones para crear la base de datos y las tablas necesarias
	- Abrir la consola del administrador de paquetes (Package Manager Console)
	- Ejecutar el comando: `Add-Migration CrearBaseDatos` 
	- Ejecutar el comando: `Update-Database`
