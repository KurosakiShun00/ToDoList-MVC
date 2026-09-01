# .NET Todo List API

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/KurosakiShun00/ToDoList-MVC/blob/Develop/LICENSE)
[![GitHub stars](https://img.shields.io/github/stars/KurosakiShun00/ToDoList-MVC)](https://github.com/KurosakiShun00/ToDoList-MVC/stargazers)

> Un'API RESTful MVC per la gestione di una lista di attività (Todo List), sviluppata in .NET come esercizio.

---

## 📌 Indice

- [Caratteristiche principali](#-caratteristiche-principali)
- [Tecnologie utilizzate](#-tecnologie-utilizzate)
- [Installazione e Configurazione](#-installazione-e-configurazione)

---

## ✨ Caratteristiche principali

*   **Operazioni CRUD complete:** Creazione, lettura, aggiornamento e cancellazione di liste e dei task all'interno di liste.
*   **Gestione multi utente:** Possibilità per ogni utente registrato di eseguire CRUD sulle proprio liste.
*   **Collegamento Database :** Utilizzato per conservare i dati sulle liste e sugli utenti, utilizzo SqLite.
*   **Documentazione Swagger:** Interfaccia grafica integrata per esplorare e testare gli endpoint API direttamente dal browser.
*   **Email Delivery:** Servizio per la conferma dell'e-mail gestita da MailTrap configurando appsettings.json con i propri dati.
*   **Importazione Liste:** Caricando un file txt ben formattato è possibile creare una lista sul gestionale

---

## 🛠 Tecnologie utilizzate

*   **.NET / C#** (Core dell'applicazione)
*   **Entity Framework Core** (Per la gestione dei dati tramite ORM)
*   **DBeaver** Per il debug visuale del database
*   **Swagger / OpenAPI** (Per la documentazione degli endpoint)
*   **AspNetCore.Authentication** (Per il flusso di autenticazione)
*   **MailTrap** Servizio email delivery service (https://mailtrap.io)
*   **Bootstrap 5.0** HTML, CSS and JS library
  * **AI Tooling:** **Google Gemini** — UI refactoring e Debuggin

## ⚙️ Installazione e Configurazione

### Prerequisiti
Per avviare questo progetto sul tuo computer, hai bisogno di:
*   **[.NET SDK](https://dotnet.microsoft.com/download)** (Versione 10.0 per sicurezza)
*   Una IDE di sviluppo
*   Lanciare 'dotnet restore' nel caso i pacchetti risultino disallineati, per altre info al riguardo visitare:
*   https://learn.microsoft.com/en-us/nuget/consume-packages/package-restore
*   Avere un account MailTrap configurato correttamente, oppure utilizzare un qualunque servizio di Email Delivery

Per verificare se hai già installato un SDK .NET, apri il terminale e digita:
```bash
dotnet --version
```
*  Lanciare per allineare la struttura del Database il seguente comando su terminale:
```bash
dotnet database restore
``` 
*  Una volta che la build avrà successo, dovrebbe partire la web app senza problemi in HTTPS

