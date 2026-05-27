# Sistema de Reservas de Cine (CinemaRes)

Aplicación de escritorio en C# diseñada para gestionar la disponibilidad de salas de cine, registrar clientes y procesar la reserva de asientos para diferentes funciones.

## Integrantes

Dominick Damiam Marengo De la rosa | Desarrollo del proyecto completo

## Descripción del problema

Un cine local necesita modernizar su sistema de asignación de asientos. Actualmente, las reservas se manejan de forma manual, lo que genera sobreventas y confusión en la disponibilidad de las salas. El sistema busca controlar qué asientos están disponibles, registrar a los clientes y emitir reservas formales.

## Objetivo del sistema

* Registrar entidades principales como Películas, Salas, Funciones, Empleados, Clientes y Reservas.
* Listar la cartelera y la disponibilidad de asientos por función.
* Buscar reservas activas por documento del cliente.
* Eliminar o cancelar reservas.
* Calcular el costo total de la reserva aplicando recargos por tipos de entrada (ej. VIP o Estándar).

## Posibles futuras integraciones
* Persistencia de datos con JSON
* Insertar peliculas.
* Crear y eliminar funciones.
* Desarrollo de área de alimentos y accesorios.

## Tecnologías utilizadas

* C# 14
* .NET 10.0x (10.0.300)
* Windows Forms (WinForms) 
* Visual Studio Code 
* Git y GitHub

## Requisitos previos

Antes de ejecutar el proyecto se necesita:
* Tener instalado el SDK de .NET 10.0 (o compatible).
* Tener instalado Visual Studio 2022 (recomendado) o Visual Studio Code.
* Tener Git instalado para clonar el repositorio.

## Instalación

```bash
# Clonar el repositorio
git clone https://github.com/DomiDelarosa/CinemaRes

# Navegar a la carpeta del proyecto
cd CinemaRes

|CinemaRes
|--Modelo
   |---
|--Servicios
|--Vista
|--Utilidades
