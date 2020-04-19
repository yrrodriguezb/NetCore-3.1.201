### Generics C#

1. **Introducción**
    1. El problema del búfer
    2. La solucion del objeto
    3. Parametros de tipo genérico
    4. Terminología genérica
    5. Pruebas de la solución

2. **Trabajando con colecciones genéricas**
    1. List  => Lista de cosas
    2. Queue => Cola de cosas
    3. Stack => Apilar cosas
    5. Set   => Conjunto de cosas
    6. Link  => Enlace de cosas
    7. Dictionary   => Mapa de cosas
    8. Sort  => Orden de cosas
    9. Pruebas

3. **Clases e Interfaces genéricas**
    1. IEnumerable<T>
    2. Interfaces de coleción
    3. Limpiar coleción, implmentado genéricos

4. **Métodos y Delegados genéricos**
    1. Métodos Genéricos
    2. Métodos de Extensión
    3. Delegados Genéricos
    4. Delegados Cotidianos
    5. Eventos y Genéricos

5. **Restricciones, Covarianza y Contravarianza**
    1. Modelo Objetos
    2. Repositorios y Restricciones
    3. Comprometerse y Consultar
    4. Una restricción de interfaz
    5. Covarianza
    6. Contravarianza

    ``` bash
        # Comandos de ayuda:

        # Agregar EF Core Tools
        dotnet tool install --global dotnet-ef
        
        # Agregar una migración
        dotnet ef migrations add "Migracion Inicial Empleados" -c Context

        # Actualizar la base de datos de acuerdo con la migración 
        dotnet ef database update -c Context
    ```

6. **Genéricos y Reflección**
    1. Instanciando tipos genéricos
    2. Invocación de tipos genéricos
    3. Construyendo tu propio contenedor
    4. Resolución de tipo simple
    5. Trabajando con constructores
    6. Trabajando con genéricos independientes

7. **Tips**
    1. Genéricos y Enums
    2. Usar Tipos Base (Base Types)
    3. Genéricos y Estático
    