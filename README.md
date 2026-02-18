# Project-Databases

Overzicht Taken

Marnix
•	Scrum Master rol
•	Trello board opzetten en beheren
•	Sprint planning voorbereiden
•	GitHub repository aanmaken en structuur inrichten (/docs, /sql)
•	Rollen en afspraken vastleggen in documentatie

Jan
•	Casus analyseren en entiteiten identificeren
•	Relaties en kardinaliteiten bepalen
•	User stories uit Moodle toevoegen aan Trello
•	ERD reviewen en feedback geven
•	Documentatie controleren op volledigheid

Nout
•	Relationeel model voorbereiden (op basis van ERD)
•	Primary keys en foreign keys bepalen
•	Basis SQL-structuur opzetten (optioneel in Sprint 1)
•	GitHub commits controleren
•	Checklist per user story afvinken
•	Eerste versie ERD maken

Algemene Afspraken
•	Iedereen begrijpt het ERD en kan het uitleggen.
•	Geen directe commits naar main zonder review.
•	Trello kaarten pas naar 'Done' wanneer checklist compleet is.
•	Iedereen werkt aan minimaal één analyse-, modelleer- en documentatietaak.


Someren Database Project – Assignment 1
Context and Requirements
In mei gaan alle eerstejaars studenten en docenten Informatica op studiereis naar Someren. Van iedere student en docent worden voornaam, achternaam en telefoonnummer geregistreerd. Studenten hebben daarnaast een studentnummer en klas, docenten een leeftijd. De groep verblijft in twee gebouwen met slaapzalen voor studenten (acht bedden) en eenpersoonskamers voor docenten. Tijdens het verblijf worden activiteiten georganiseerd zoals een puzzelspeurtocht, een voetbaltoernooi en een hindernisbaan. Studenten doen mee als deelnemers en docenten zijn begeleiders. Op de disco‐avond kunnen studenten drankjes bestellen; voor ieder drankje wordt naam, prijs, btw‐tarief (9 % of 21 %) en voorraad bijgehouden. Per bestelling wordt vastgelegd welke student welk drankje in welke hoeveelheid koopt.
ERD‐diagram
Het ERD is weergegeven in de onderstaande afbeelding. Alle entiteiten (Person, Student, Lecturer, Building, Room, RoomAssignment, Activity, ActivityParticipant, ActivitySupervisor, Drink, Order, OrderItem) en relaties zijn opgenomen. Student en Lecturer zijn subtypes van Person. Kardinaliteiten en totaliteiten zijn aangegeven: bijvoorbeeld één Building heeft meerdere Rooms en één Student kan meerdere Orders plaatsen. Many‐to‐many relaties zijn opgelost met koppel¬tabel¬len.
Someren ERD

Relationeel Model
Voor het relationele model is iedere entiteit omgezet naar een tabel met een primaire sleutel en waar nodig foreign keys. Hieronder volgt per tabel een korte omschrijving.
•	Person – person_id (PK), first_name, last_name, phone_number.
•	Student – person_id (PK/FK naar Person), student_number, class.
•	Lecturer – person_id (PK/FK naar Person), age.
•	Building – building_id (PK), name.
•	Room – room_id (PK), building_id (FK), room_number, capacity, is_teacher_room.
•	RoomAssignment – samengestelde sleutel person_id, room_id (FK’s), beschrijft welke persoon in welke kamer slaapt.
•	Activity – activity_id (PK), name, start_time, end_time.
•	ActivityParticipant – samengestelde sleutel student_id, activity_id (FK’s), koppeling tussen studenten en activiteiten.
•	ActivitySupervisor – samengestelde sleutel lecturer_id, activity_id (FK’s), koppeling tussen docenten en activiteiten.
•	Drink – drink_id (PK), name, price, vat_rate, stock.
•	Order – order_id (PK), student_id (FK), order_date.
•	OrderItem – samengestelde sleutel order_id, drink_id (FK’s), quantity.
Het bijbehorende SQL‐schema staat in het bestand someren_sql_schema.sql. Dit script bevat alle CREATE TABLE‐statements met primaire sleutels, foreign keys en identity‐kolommen waar nodig.
SQL‐schema
Het SQL‐script is afzonderlijk opgeslagen als someren_sql_schema.sql. Importeer dit in SQL Server of een andere database om de tabellen aan te maken.
User stories voor Sprint 1
Om deze opdracht iteratief te ontwikkelen zijn de volgende user stories opgesteld:
User Story: Design an ERD
•	As a developer
•	I want to easily view the database structure via an ERD diagram
•	So that I can easily understand, modify and integrate with the database schema
Checklist
1.	Analyseer de Someren‐casus en identificeer alle entiteiten, relaties en attributen.
2.	Zorg dat het ERD alle entiteiten, relaties en attributen bevat.
3.	Noteer bij elke relatie zowel de functionaliteit (1, n, m) als de totaliteit (O, T) en beschrijf deze ook in tekst.
4.	Controleer en bespreek het ERD binnen de groep.
User Story: Convert the ERD into a relational model
•	As a developer
•	I want to have a relational model based on the ERD diagram
•	So that I can create and use a well‐structured database for my application
Checklist
1.	Geef alle entiteiten en relatie‐tabellen een unieke naam.
2.	Voorzie iedere entiteit van een primaire sleutel.
3.	Converteer alle n‐m relaties naar drie tabellen (twee entity‐tabellen en een koppel-tabel).
4.	Voeg voor n‐1 en 1‐n relaties de juiste foreign key toe aan de n‐kant.
5.	Implementeer voor 1‐1 relaties de attribuutkolommen en foreign key in één van de twee tabellen.
6.	Zorg dat verplichte participatie is afgedwongen met NOT NULL‐constraints en dat refererende waarden volledig voorkomen.
7.	Controleer en bespreek het relationele model in de groep.
Bestanden
De volgende bestanden moeten in de repository geplaatst worden:
•	docs/erd_someren.png – de ERD‐afbeelding.
•	docs/Assignment1_Report.md – dit rapport.
•	sql/someren_sql_schema.sql – het SQL‐schema.

