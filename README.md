# Desafío Backend .NET – API de Gestión de Usuarios, Recursos y Reservas para Medialityc

##  Descripción General

Este proyecto consiste en el desarrollo de una **API REST en .NET 9**, utilizando **FastEndpoints**, orientada a la gestión de **Usuarios**, **Recursos** y **Reservas**.

El objetivo principal no es únicamente que la solución funcione, sino demostrar la capacidad para **diseñar, estructurar y desarrollar un backend profesional**, aplicando buenas prácticas de arquitectura, separación de responsabilidades y reglas de negocio claras.

---

## Alcance del Proyecto

### ✔️ Se espera

* API REST desarrollada en .NET 9
* Uso obligatorio de **FastEndpoints**
* Persistencia con **Entity Framework Core**
* Base de datos relacional (PostgreSQL)
* Arquitectura clara y consistente
* Autenticación con JWT
* Autorización por roles (Admin / Usuario)
* Paginación y filtros tipados en todos los listados
* Swagger habilitado
* README con decisiones técnicas documentadas

### ❌ No se espera

* Frontend o interfaz gráfica
* Funcionalidades fuera del dominio solicitado
* Sobre–ingeniería innecesaria
* Borrado físico de entidades

---

##  Arquitectura

Se ha optado por la arquitectura **CQRS (Command Query Responsibility Segregation)**.

### Justificación

* Permite separar claramente las operaciones de **lectura (Queries)** de las de **escritura (Commands)**.
* Facilita el mantenimiento y la escalabilidad del sistema.
* Mejora la claridad del código al aislar reglas de negocio específicas.

### Capas principales

* **Endpoints**: manejo de HTTP, validaciones y autorización.
* **Commands / Queries**: lógica de negocio.
* **Repositorios**: acceso a datos mediante EF Core.
* **Entidades**: modelo de dominio.
* **DTOs**: contratos de entrada y salida.

---

## 🛠 Tecnologías Utilizadas

* **.NET 9**
* **FastEndpoints**
* **Entity Framework Core**
* **PostgreSQL** 
* **JWT Authentication**
* **Swagger**

---

## 👤 Gestión de Usuarios

### Funcionalidades

* Crear usuarios
* Consultar usuarios
* Actualizar usuarios
* Desactivar usuarios (Soft Delete)

### Reglas de negocio

* No se permite el borrado físico de usuarios.
* Un usuario puede tener múltiples correos electrónicos y teléfonos.
* Usuarios inactivos no pueden realizar reservas.

### Relaciones

* Usuario → Correos (1:N)
* Usuario → Teléfonos (1:N)

---

## Gestión de Recursos

### Funcionalidades

* Crear recursos
* Consultar recursos
* Actualizar recursos
* Desactivar recursos

Los recursos representan entidades reservables como salas, equipos u otros activos.

---

## Gestión de Reservas

### Funcionalidades

* Crear reservas
* Consultar reservas
* Cancelar reservas

### Reglas de negocio

* No se permiten reservas solapadas para un mismo recurso.
* Un usuario inactivo no puede realizar reservas.
* Las reservas pueden cancelarse, pero no eliminarse físicamente.

---

## 📄 Paginación y Filtros

Todos los endpoints de listado implementan:

### Request

* Page
* PageSize

### Response

* Items
* Page
* PageSize
* TotalItems
* TotalPages
* HasNext
* HasPrevious

La paginación se realiza **a nivel de base de datos** y los filtros son **tipados** (estado, fechas, tipo, etc.).

---

## 🔐 Seguridad

* Autenticación basada en JWT
* Autorización por roles:

  * **Admin**: gestión completa
  * **Usuario**: operaciones permitidas por dominio

---

## Documentación

* Swagger habilitado para pruebas y exploración de la API
* README con:

  * Alcance del proyecto
  * Decisiones técnicas
  * Instrucciones de ejecución

---

## Cómo ejecutar el proyecto

1. Clonar el repositorio
2. Configurar la cadena de conexión en `appsettings.json`
3. Ejecutar migraciones:

   ```bash
   dotnet ef database update
   ```
4. Ejecutar el proyecto:

   ```bash
   dotnet run
   ```
5. Acceder a Swagger:

   ```
   https://localhost:{puerto}/swagger
   ```

---

## 🧠 Decisiones Técnicas

* Se utiliza **Soft Delete** para mantener la integridad histórica de los datos.
* La lógica de negocio se mantiene fuera de los endpoints.
* Se prioriza claridad y mantenibilidad sobre complejidad innecesaria.

---

## Conclusión

Este proyecto demuestra la implementación de un backend lo mas robusto, mantenible y alineado que pude desarrollar con buenas prácticas profesionales, cumpliendo estrictamente con los requisitos del desafío.
