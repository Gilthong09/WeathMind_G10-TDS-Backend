# WeathMind 

WeathMind es una aplicación desarrollada en ASP.NET Core MVC que permite a los usuarios gestionar sus finanzas personales, incluyendo ingresos, gastos y análisis estadístico por categoría y período de tiempo. Este proyecto forma parte de una iniciativa académica enfocada en buenas prácticas de desarrollo de software como la separación en capas, principios SOLID, y uso de Entity Framework.

## 🚀 Tecnologías Utilizadas

- ASP.NET Core MVC
- Entity Framework Core
- LINQ
- SQL Server
- AutoMapper
- IONIC (Frontend)
- C#

### Capas del proyecto:

- **Domain**: Contiene las entidades del dominio (como `Transaction`, `Category`, `User`, etc.).
- **Application**: Lógica del negocio, interfaces de servicios, DTOs y ViewModels.
- **Infrastructure**: Implementación de repositorios, acceso a datos (EF Core), y configuración de servicios.
- **WebAPI**: Capa de presentación (API RESTful), configuración de rutas, controladores y dependencias.

## ✨ Funcionalidades Principales

### ✅ Transacciones
- Registrar ingresos y gastos.
- Editar y eliminar transacciones.
- Ver historial por usuario, fecha o categoría.

### 📊 Estadísticas
- Análisis mensual:
  - Porcentaje de gastos por categoría.
  - Total mensual de ingresos y egresos.
- Análisis anual:
  - Distribución de gastos por mes.
  - Cantidad de transacciones al año.

### 🔎 Filtros y Consultas
- Buscar transacciones por rango de fecha.
- Obtener gastos más altos en un mes específico.
- Consultar ingresos y egresos por mes y año.

---

## 🧠 ViewModels Personalizados

Se utilizaron ViewModels para separar la lógica de negocio de la interfaz gráfica y facilitar el envío de datos a las vistas, como:
- `TransactionViewModel`
- `StatisticsViewModel`
- `CategoryViewModel`

---

## 🛠️ Configuración y Uso

1. Clona el repositorio:
   ```bash
   git clone https://github.com/Gilthong09/WeathMind_G10-TDS-Backend.git
   ```
2. Abre el proyecto en Visual Studio.
3. Configura la cadena de conexión en `appsettings.json`.
4. Ejecuta las migraciones:
   ```bash
   dotnet ef database update
   ```
5. Ejecuta el proyecto con `F5` o `dotnet run`.

---

## 👨‍💻 Desarrollador

**Gilthong Enmanuel Palin Garcia**  
Desarrollador de software en formación, apasionado por la inteligencia artificial, el aprendizaje continuo y el desarrollo de soluciones tecnológicas.  
📧 gilthong09@gmail.com

---

## 📄 Licencia

Este proyecto es de uso académico y personal. Puedes adaptarlo y expandirlo libremente para fines educativos.
---
