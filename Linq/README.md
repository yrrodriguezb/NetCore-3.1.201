### Curso - LINQ Fundamentals - Pluralsight

1. **Introudccion**
2. **Caracteristicas de C#**
    1. Métodos de Extensión
    2. Expresiones lambda
    3. Sintaxis de consulta versus sintaxis de método
3. **Consultas**
    1. Crear un operador de filtro personalizado
    2. Crear un operador de filtro con rendimiento
    3. Ejecución diferidad explicita
    4. Evitar trampas de ejecución diferidas
    5. Excepciones y consultas diferidas
    6. Operadores de transmisión
    7. Consultando infinito
4. **Filtrar, Ordenar y Proyectar**
    1. Procesar archivos con Linq
    2. Filtrar con Where y Take
    3. Cuantificacion de datos con Any, All y Contains
    4. Proyecccion de datos con Select
    5. Acoplar datos con SelectMany
5. **Union, Agrupar y Agregar**
    1. Agregar una segunda fuente de datos
    2. Unir datos con sintaxis de consulta
    3. Unir datos con sintaxis de método
    4. Crear una union con una clave compuesta
    5. Agrupación de datos 
    6. Uso de GroupJoin para datos jerárquicos
    7. Agrupacion eficiente con método de extensión
6. **Linq a XML**
    1. System.Xml.Linq
    2. Creación de XML orientado a objectos
    3. Construcción funcional con menos código
    4. Cargar y consultar XML con Linq
    5. Trabajar con espacios de nombres XML
7. **Linq y Entity Framework**
    1. Configurar Entity Framework
    2. Insertar datos en una nueva base de datos
    3. Escribr una consulta básica en Linq
    4. Trabajar con IQueryables y Arboles de Expresión (Expression Trees)
    5. Advertencias y trampas de Linq remoto
    6. Consulta avanzada en Linq

    ``` bash
        # Comandos de ayuda:

        # Agregar EF Core Tools
        dotnet tool install --global dotnet-ef
        
        # Agregar una migración
        dotnet ef migrations add "Migracion Modelo de Carro" -c Context

        # Actualizar la base de datos de acuerdo con la migración 
        dotnet ef database update -c Context
    ```