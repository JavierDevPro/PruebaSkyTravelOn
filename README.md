La aerolínea SkyTravel actualmente gestiona sus vuelos y reservas de manera manual en hojas de cálculo, lo cual ha generado múltiples problemas:

    Duplicidad o sobreventa de asientos.
    Errores en las fechas de vuelo.
    Dificultad para consultar la información de reservas o pasajeros.
    Pérdida de información al extraviar registros.

La dirección de SkyTravel ha decidido desarrollar un sistema interno en C# que permita organizar y controlar las reservas de vuelos, asegurando precisión, trazabilidad y disponibilidad de la información.

# Nugets:

    //>>> dotnet add package Microsoft.EntityFrameworkCore
    //>>> dotnet add package Microsoft.EntityFrameworkCore.Design
    //>>> dotnet add package Pomelo.EntityFrameworkCore.MySql

## Requisitos para montar la db:
    //>>> dotnet tool install --global dotnet-ef
    //>>> dotnet ef migrations add InitialCreate
    //>>> dotnet ef database update

## Configuracion en el Program:
    var connection = builder.Configuration.GetConnectionString("DefaultConnection");
    
    builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connection, ServerVersion.AutoDetect(connection))
    );

Tienen que ir antes de : var app = builder.Build();