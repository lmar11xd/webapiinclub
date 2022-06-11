# ####################################
# #### LUIS MIGUEL ALVARADO ROMAN ####
# ####################################

# webapiinclub
Web Application with .Net 5, Mapper, Swagger, SQL Server and CQRS, Repository, Dedendency Injection Patterns



# 1. Base de Datos
	En la carpeta "db" se encuentran alojados los scripts;



# 2. Configurar Cadena de Conexión
	* Abrir la Solución del Proyecto con VS
	* Ubíquese en el Proyecto ApiInClub y en el archivo appsettings.json configurar la cadena de conexión

{
  "ConnectionStrings": {
    "DefaultConnection": "<Cadena de conexion>"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*"
}



# 3. La implementación se realizó bajo la arquitectura Onion


Gracias...