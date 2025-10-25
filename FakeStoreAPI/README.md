Para ejecutar el proyecto realizar los siguientes pasos

Ejemplo:
```
{
    "ConnectionStrings": {
        "DefaultConnection": "Server=ANDY\\SQLEXPRESS;Database=FakeStorAPI;Trusted_Connection=True;TrustServerCertificate=True;"
    },
    "Logging": {
        "LogLevel": {
            "Default": "Information",
            "Microsoft.AspNetCore": "Warning"
        }
    },
    "AllowedHosts": "*",
    "JWT": {
        "Key": "42181DFB-5A31-4EA4-8903-BC7610E92829"
    },
    "Url": {
        "FakeStoreAPI": "https://fakestoreapi.com/products"
    }
}


```

1. Clonar el repositorio

2. Abrir el proyecto

3. Configurar la cadena de conexión en el archivo appsettings.json si es necesario

4. Ejecutar las migraciones para crear la base de datos y las tablas necesarias
	- Abrir la consola del administrador de paquetes (Package Manager Console)
	- Ejecutar el comando: `Add-Migration CrearBaseDatos` 
	- Ejecutar el comando: `Update-Database`

5. Registrarse en la web API y hacer Login
6. Copiar el pegar el token en la parte superior derecha donde dice 'Authorize'

7. En la /api/Currencys va ver que no hay ninguna moneda por ende tendra
que agregar algunas

{
  "moneda": "USD",
  "equivalente": 18
}

y

{
  "moneda": "MXN",
  "equivalente": 18
}

y

{
  "moneda": "EUR",
  "equivalente": 21
}

8. Despues de esto podra probar el demas funcionamiento de las apis

# Enlaces de codigo de referencia:
- https://github.com/Andy-Castle/WebAPIJWT.git
- https://github.com/Andy-Castle/SociedadesAPI.git