# Someren Database Project – Assignment 1

## Context and Requirements

In mei gaan alle eerstejaars studenten en docenten Informatica op studiereis naar Someren. Van iedere student en docent worden voornaam, achternaam en telefoonnummer geregistreerd; studenten hebben daarnaast een studentnummer en klas, docenten een leeftijd. De groep verblijft in twee gebouwen met slaapzalen voor studenten (acht bedden) en eenpersoonskamers voor docenten. Tijdens het verblijf worden activiteiten georganiseerd zoals een puzzelspeurtocht, een voetbaltoernooi en een hindernisbaan. Studenten doen mee als deelnemers en docenten zijn begeleiders. Op de disco ‑avond kunnen studenten drankjes bestellen; voor ieder drankje worden naam, prijs, btw‑tarief (9 % of 21 %) en voorraad bijgehouden. Per bestelling wordt vastgelegd welke student welk drankje in welke hoeveelheid koopt.

## Wat je nu moet doen

Deze opdracht bestaat uit meerdere sprints. Voor de start van Sprint 1 en het eerste Living Lab met de docenten is het belangrijk om vooral de **basis** op orde te hebben. Concentreer je op de volgende punten:

### 1. Analyseer de casus en identificeer entiteiten

Lees de Someren‑beschrijving en lijst alle relevante gegevens op (personen, activiteiten, gebouwen, kamers, drankjes, bestellingen). Je hoeft nog niet alle details uit te werken, maar je moet wél weten welke entiteiten en relaties er in het systeem voorkomen. Dit vormt de basis voor het ERD dat je zelf maakt en uploadt.

### 2. Zet een Trello ‑board op en vul de backlog

Maak een Trello‑bord met de kolommen **Backlog**, **To Do**, **Doing**, **Review** en **Done**. Voeg alle user stories voor Assignment 1 uit Moodle toe aan de **Backlog**. Vul per kaart de user‑storytekst ("As a … I want … so that …") en checklist in. Dit is belangrijk zodat iedereen weet welke taken er zijn en wat er nog moet gebeuren.

### 3. Deel rollen en stem de werkwijze af

Spreek binnen je team af wie de rol van Scrum Master op zich neemt en wie contactpersoon (Product Owner) is. Maak afspraken over hoe je code deelt (bijv. altijd via pull requests, geen direct werk op `main`).

### 4. Maak en upload je eigen ERD

Iedereen maakt een eerste versie van het ERD met bijvoorbeeld draw.io. Je hoeft nu geen perfecte diagram te hebben, maar zorg dat alle entiteiten en relaties zichtbaar zijn. Nadat je ERD is goedgekeurd binnen de groep, upload je het naar de repository. In dit rapport wordt geen ERD ‑afbeelding meer opgenomen zodat je je eigen model kunt toevoegen.

### 5. Zet een GitHub ‑repository op en commit je bestanden

Initialiseer een privé ‑repository (bijvoorbeeld `Project ‑Databases`), voeg je teamgenoten toe als collaborators en maak een mapstructuur aan (`/docs` voor documentatie, `/sql` voor scripts). Upload in ieder geval dit rapport en je eigen ERD naar de map `docs`. Je kunt het SQL‑schema toevoegen zodra jullie dat hebben opgesteld.

## Wat je nu moet weten

Voor dit deel van de opdracht heb je vooral **basiskennis** nodig. Het is niet de bedoeling om al diep in de relationele theorie of SQL te duiken. Zorg dat je de volgende concepten en vaardigheden begrijpt:

* **ERD‑basisprincipes:** wat zijn entiteiten, attributen en relaties; hoe herken je een many‑to‑many relatie.
* **Primaire sleutels en foreign keys:** waarom zijn ze nodig en hoe herken je ze in je eigen model.
* **Git‑workflow:** klonen, committen, pushen en pullen. Begrijp waarom je niet op de `main` branch werkt zonder code review.
* **Scrum light:** de rollen (Scrum Master, Product Owner), sprintplanning en dagelijkse stand‑ups. Wees in staat om te vertellen wat je hebt gedaan, wat je gaat doen en waar je hulp bij nodig hebt.
* **Trello:** het aanmaken en beheren van user stories, het verschuiven van kaarten tussen kolommen en het afvinken van checklistitems.

Later in het project ga je het ERD omzetten naar een relationeel model en SQL‑tabellen, maar dat hoeft niet meteen voor het eerste Living Lab klaar te zijn. Focus nu op begrip van de casus, de planning en de samenwerking.

## User stories voor Sprint 1

Zorg dat de user stories uit Moodle op je Trello‑bord staan. De belangrijkste verhalen voor deze fase zijn:

- **Design an ERD** – Als developer wil je een overzichtelijk ERD zodat je de database goed begrijpt en kunt aanpassen. Checklist: casus analyseren, entiteiten en relaties identificeren, kardinaliteiten en totaliteiten benoemen en het diagram intern bespreken.
- **Convert the ERD into a relational model** – Zodra het ERD staat, wil je het omzetten naar tabellen met primaire sleutels en foreign keys. Checklist: unieke namen geven, n‑m relaties opdelen, juiste foreign keys toevoegen en verplichte participatie afdwingen. Dit verhaal hoeft pas tegen het einde van Sprint 1 volledig af te zijn.

## Bestanden

Op dit moment moeten de volgende bestanden in de repository staan:

- `docs/Assignment1_Report.md` – dit rapport met uitleg over de casus en de te nemen stappen.
- `docs/<jouw_ERD_bestand>` – jouw eigen ERD‑diagram, zodra het klaar is en geüpload wordt.
- `sql/someren_sql_schema.sql` – een optioneel SQL‑script voor de database (kan in een latere sprint worden aangevuld).
