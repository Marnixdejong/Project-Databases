# Opdracht 1 - Database ontwerp

## ERD

Het ERD is inbegrepen in het project in het bestand `docs/erd.mmd`.

De database bevat de volgende entiteiten:
- Person (basis voor studenten en docenten)
- Student (erft van Person)
- Lecturer (erft van Person)
- Building
- Room (behoort tot een Building)
- RoomAssignment (koppelt Person aan Room)
- Activity
- ActivityParticipant (koppelt Student aan Activity)
- ActivitySupervisor (koppelt Lecturer aan Activity)
- Drink
- Order (geplaatst door Student)
- OrderItem (koppelt Order aan Drink)

## Relationeel model

Person(**person_id**, first_name, last_name, phone_number)

Student(**person_id** [FK -> Person.person_id], student_number, class)

Lecturer(**person_id** [FK -> Person.person_id], age)

Building(**building_id**, name)

Room(**room_id**, building_id [FK -> Building.building_id], room_number, capacity, is_teacher_room)

RoomAssignment(**person_id** [FK -> Person.person_id], **room_id** [FK -> Room.room_id])

Activity(**activity_id**, name, start_time, end_time)

ActivityParticipant(**student_id** [FK -> Student.person_id], **activity_id** [FK -> Activity.activity_id])

ActivitySupervisor(**lecturer_id** [FK -> Lecturer.person_id], **activity_id** [FK -> Activity.activity_id])

Drink(**drink_id**, name, price, vat_rate, stock)

Order(**order_id**, student_id [FK -> Student.person_id], order_date)

OrderItem(**order_id** [FK -> Order.order_id], **drink_id** [FK -> Drink.drink_id], quantity)

### Legenda

- **vet** = primary key
- [FK -> ...] = foreign key verwijzing
- Alle primary keys met IDENTITY worden automatisch gegenereerd
- Student en Lecturer gebruiken het person_id als zowel PK als FK (is-a relatie met Person)
- phone_number in Person mag NULL zijn, alle andere velden zijn NOT NULL
