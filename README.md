# Stick Overflow
Sito in stile forum usando pagine Razor, dove gli utenti possono registrarsi, loggarsi, creare discussioni dentro a forum (suddivisi per topic) e rispondere alle discussioni tramite messaggi

![image](https://camo.githubusercontent.com/48e05e97e23f45d9f0081c8db50151ac41f6b36862e151e547a77ac8ef3ab9f8/68747470733a2f2f696d672e736869656c64732e696f2f62616467652f432532332d3233393132303f7374796c653d666f722d7468652d6261646765266c6f676f3d637368617270266c6f676f436f6c6f723d7768697465)  ![image](https://camo.githubusercontent.com/10c7a8fa2cf317cc7c4af6f13efac086a9f0ea010f0dfc746c94e5cde310b339/68747470733a2f2f696d672e736869656c64732e696f2f62616467652f48544d4c352d4533344632363f7374796c653d666f722d7468652d6261646765266c6f676f3d68746d6c35266c6f676f436f6c6f723d7768697465) ![JavaScript Badge](https://img.shields.io/badge/JavaScript-F7DF1E?logo=javascript&logoColor=000&style=flat)
## Tool usati
### Sviluppo
![image](https://img.shields.io/badge/Visual_Studio-5C2D91?style=for-the-badge&logo=visual%20studio&logoColor=white) 
### Librerie/framework
![.NET Badge](https://img.shields.io/badge/.NET-512BD4?logo=dotnet&logoColor=fff&style=flat) ![Bootstrap Badge](https://img.shields.io/badge/Bootstrap-7952B3?logo=bootstrap&logoColor=fff&style=flat-square)  [Microsoft Data SqlClient (aka OleDB)](https://www.nuget.org/packages/Microsoft.Data.SqlClient/6.1.0-preview1.25120.4)
### Gestione del progetto
![GitHub Badge](https://img.shields.io/badge/GitHub-181717?logo=github&logoColor=fff&style=flat-square) Projects

## Struttura logica della webapp
- Homepage
- Ricerca forum
- Visualizzazione forum
- Visualizzazione discussione
- Login / Registrazione
     - Creazione discussioni
     - Invio messaggi
     - Admin Panel (solo se utente è admin)
          - Creazione forum
          - Ban/unban utenti
          - Rimozione messaggi

## Funzioni della webapp
La homepage contiene tutti i form e una lista delle discussioni con più messaggi nell'ultimo periodo, accessibile da tutti, anche senza login. Inoltre si può visualizzare la pagina di ricerca e tutti i contenuti dei forum e delle discussioni. Infine, sempre senza login è possibile visualizzare i profili altrui.
Una volta effettuato il login è possibile modificare il proprio profilo, creare discussioni e rispondere ad esse tramite messaggi.
Se si è amministratore una pagina dove l'intero database è accessibile e si possono gestire gli utenti (sbannare/bannare), i messaggi (eliminare) e creare e modificare i form.

Le pagine menzionate sopra sono condivise tra utenti loggati e non, e la distinzione delle funzioni avviene tramite i session cookies:
``` cs
    HttpContext.Session.SetString("user-id", Convert.ToString(ris))
```
oppure per salvare le credenziali oltre il tempo di scadenza della sessione tramite i cookie classici:
``` cs
    CookieOptions cookie = new CookieOptions();
    cookie.Expires = DateTime.Now.AddDays(30);
    Response.Cookies.Append("logged-in-id", Convert.ToString(ris), cookie);
``` 
E poi per recuperarli:
``` cs
    string usrId = HttpContext.Request.Cookies["logged-in-id"] ?? HttpContext.Session.GetString("user-id");
```
## Struttura del database
Il database è tipo relazionale, usa SQL ed ha la seguente struttura:
![drawSQL-image-export-2025-06-05](https://github.com/user-attachments/assets/accee88a-0890-4348-8d9b-a7a8c86e4e07)
Il DBMS utilizzato di default è SQL Server (di Microsoft) ma in verità per cambiare DBMS basta hostare il DB su un sistema separato e cambiare la stringa di connessione

## Hosting
Non essendo a disposizione delle risorse necessarie per l'hosting non è possibile per me tenere il sito in rete, detto questo, ci sono alcune opzioni:
1. Hosting gratuito su Azure:
   Basta fare il deployment del servizio hosting ASP.NET di Azure e eseguire il Publish da Visual Studio
   Per il DB, basta fare il deployment del servizio DB SQL di azure e caricare il i file necessari tramite CLI/web GUI
2. Hosting con Docker:
   Si scrive il Dockerfile contenete i container per web server e DB, puntando alle directory giuste sulla macchina dove si eseguirà il container
3. Hosting su Windows, utilizzando IIS (Internet Information Services)
