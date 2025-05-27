# Stick Overflow
Sito in stile forum usando pagine Razor, dove gli utenti possono registrarsi, loggarsi, creare discussioni dentro a forum (suddivisi per topic) e rispondere alle discussioni tramite messaggi

![image](https://camo.githubusercontent.com/48e05e97e23f45d9f0081c8db50151ac41f6b36862e151e547a77ac8ef3ab9f8/68747470733a2f2f696d672e736869656c64732e696f2f62616467652f432532332d3233393132303f7374796c653d666f722d7468652d6261646765266c6f676f3d637368617270266c6f676f436f6c6f723d7768697465)  ![image](https://camo.githubusercontent.com/10c7a8fa2cf317cc7c4af6f13efac086a9f0ea010f0dfc746c94e5cde310b339/68747470733a2f2f696d672e736869656c64732e696f2f62616467652f48544d4c352d4533344632363f7374796c653d666f722d7468652d6261646765266c6f676f3d68746d6c35266c6f676f436f6c6f723d7768697465) ![JavaScript Badge](https://img.shields.io/badge/JavaScript-F7DF1E?logo=javascript&logoColor=000&style=flat)
## Tool usati
### Sviluppo
![image](https://img.shields.io/badge/Visual_Studio-5C2D91?style=for-the-badge&logo=visual%20studio&logoColor=white) 
### Librerie/framework
![.NET Badge](https://img.shields.io/badge/.NET-512BD4?logo=dotnet&logoColor=fff&style=flat) ![Bootstrap Badge](https://img.shields.io/badge/Bootstrap-7952B3?logo=bootstrap&logoColor=fff&style=flat-square)
### Gestione del progetto
![GitHub Badge](https://img.shields.io/badge/GitHub-181717?logo=github&logoColor=fff&style=flat-square) Projects

## Struttura logica della webapp
_stuff goes here_

## Funzioni della webapp
La homepage contiene tutti i form e una lista delle discussioni con più messaggi nell'ultimo periodo, accessibile da tutti, anche senza login. Inoltre si può visualizzare la pagina di ricerca e tutti i contenuti dei forum e delle discussioni. Infine, sempre senza login è possibile visualizzare i profili altrui.
Una volta effettuato il login è possibile modificare il proprio profilo, creare discussioni e rispondere ad esse tramite messaggi.
Se si è amministratore una pagina dove l'intero database è accessibile e si possono gestire gli utenti (sbannare/bannare), i messaggi (eliminare) e creare e modificare i form.

Le pagine menzionate sopra sono condivise tra utenti loggati e non, e la distinzione delle funzioni avviene tramite i session cookies:

    HttpContext.Session.SetString("user-id", Convert.ToString(ris))
oppure per salvare le credenziali oltre il tempo di scadenza della sessione tramite i cookie classici:

    CookieOptions cookie = new CookieOptions();
    cookie.Expires = DateTime.Now.AddDays(30);
    Response.Cookies.Append("logged-in-id", Convert.ToString(ris), cookie);
E poi per recuperarli:

    string usrId = HttpContext.Request.Cookies["logged-in-id"] ?? HttpContext.Session.GetString("user-id");

## Struttura del database
_stuff goes here_
_menzionare dbms etc_

## Hosting su Azure
_forse?_
