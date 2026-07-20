# .NET Todo List API

[![GitHub license](https://img.shields.io/github/license/KurosakiShun00/ToDoList-MVC)](https://github.com/KurosakiShun00/ToDoList-MVC/blob/Develop/LICENSE)
[![GitHub stars](https://img.shields.io/github/stars/KurosakiShun00/ToDoList-MVC)](https://github.com/KurosakiShun00/ToDoList-MVC/stargazers)

> Un'API RESTful per la gestione di una lista di attività (Todo List), sviluppata in .NET come progetto di esercizio.

Il progetto utilizza un **Database In-Memory**, il che significa che non è richiesta alcuna configurazione o installazione di database esterni (come SQL Server). I dati vengono salvati temporaneamente nella memoria RAM e si azzerano a ogni riavvio dell'applicazione.

---

## 📌 Indice

- [Caratteristiche principali](#-caratteristiche-principali)
- [Tecnologie utilizzate](#-tecnologie-utilizzate)
- [Installazione e Configurazione](#-installazione-e-configurazione)
- [Endpoint dell'API](#-endpoint-dellapi)
- [Come testare le API](#-come-testare-le-api)

---

## ✨ Caratteristiche principali

*   **Operazioni CRUD complete:** Creazione, lettura, aggiornamento e cancellazione dei task.
*   **Database In-Memory:** Avvio immediato senza configurazioni di stringhe di connessione.
*   **Documentazione Swagger:** Interfaccia grafica integrata per esplorare e testare gli endpoint direttamente dal browser.

---

## 🛠 Tecnologie utilizzate

*   **.NET / C#** (Core dell'applicazione)
*   **Entity Framework Core** (Per la gestione dei dati tramite ORM)
*   **EF Core In-Memory Database Provider** (Database temporaneo in RAM)
*   **Swagger / OpenAPI** (Per la documentazione degli endpoint)

---

## ⚙️ Installazione e Configurazione

### Prerequisiti
Per avviare questo progetto sul tuo computer, hai solo bisogno di:
*   **[.NET SDK](https://dotnet.microsoft.com/download)** (Versione 8.0 o successiva, a seconda di quella che hai usato)

Per verificare se lo hai già installato, apri il terminale e digita:
```bash
dotnet --version
