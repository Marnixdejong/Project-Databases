-- Clean up existing objects in correct dependency order
IF OBJECT_ID('DrinkOrder', 'U') IS NOT NULL DROP TABLE DrinkOrder;
IF OBJECT_ID('Drink', 'U') IS NOT NULL DROP TABLE Drink;
IF OBJECT_ID('ActivitySupervisor', 'U') IS NOT NULL DROP TABLE ActivitySupervisor;
IF OBJECT_ID('ActivityParticipant', 'U') IS NOT NULL DROP TABLE ActivityParticipant;
IF OBJECT_ID('Activity', 'U') IS NOT NULL DROP TABLE Activity;
IF OBJECT_ID('RoomAssignment', 'U') IS NOT NULL DROP TABLE RoomAssignment;
IF OBJECT_ID('Room', 'U') IS NOT NULL DROP TABLE Room;
IF OBJECT_ID('Building', 'U') IS NOT NULL DROP TABLE Building;
IF OBJECT_ID('Lecturer', 'U') IS NOT NULL DROP TABLE Lecturer;
IF OBJECT_ID('Student', 'U') IS NOT NULL DROP TABLE Student;
IF OBJECT_ID('Person', 'U') IS NOT NULL DROP TABLE Person;

-- SQL schema for Someren database project

-- Table: Person
CREATE TABLE Person (
    person_id INT IDENTITY(1,1) PRIMARY KEY,
    first_name VARCHAR(50) NOT NULL,
    last_name VARCHAR(50) NOT NULL,
    phone_number VARCHAR(20)
);

-- Table: Student
CREATE TABLE Student (
    person_id INT PRIMARY KEY,
    student_number VARCHAR(50) NOT NULL,
    class VARCHAR(50) NOT NULL,
    CONSTRAINT FK_Student_Person FOREIGN KEY (person_id) REFERENCES Person(person_id)
);

-- Table: Lecturer
CREATE TABLE Lecturer (
    person_id INT PRIMARY KEY,
    age INT NOT NULL,
    CONSTRAINT FK_Lecturer_Person FOREIGN KEY (person_id) REFERENCES Person(person_id)
);

-- Table: Building
CREATE TABLE Building (
    building_id INT IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR(100) NOT NULL
);

-- Table: Room
CREATE TABLE Room (
    room_id INT IDENTITY(1,1) PRIMARY KEY,
    building_id INT NOT NULL,
    room_number VARCHAR(10) NOT NULL,
    capacity INT NOT NULL,
    is_teacher_room BIT NOT NULL,
    CONSTRAINT FK_Room_Building FOREIGN KEY (building_id) REFERENCES Building(building_id)
);

-- Table: RoomAssignment (variant D - not implemented in web app)
CREATE TABLE RoomAssignment (
    person_id INT NOT NULL,
    room_id INT NOT NULL,
    PRIMARY KEY (person_id, room_id),
    CONSTRAINT FK_RoomAssignment_Person FOREIGN KEY (person_id) REFERENCES Person(person_id),
    CONSTRAINT FK_RoomAssignment_Room FOREIGN KEY (room_id) REFERENCES Room(room_id)
);

-- Table: Activity
CREATE TABLE Activity (
    activity_id INT IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    start_time DATETIME NOT NULL,
    end_time DATETIME NOT NULL
);

-- Table: ActivityParticipant
CREATE TABLE ActivityParticipant (
    student_id INT NOT NULL,
    activity_id INT NOT NULL,
    PRIMARY KEY (student_id, activity_id),
    CONSTRAINT FK_ActivityParticipant_Student FOREIGN KEY (student_id) REFERENCES Student(person_id),
    CONSTRAINT FK_ActivityParticipant_Activity FOREIGN KEY (activity_id) REFERENCES Activity(activity_id)
);

-- Table: ActivitySupervisor
CREATE TABLE ActivitySupervisor (
    lecturer_id INT NOT NULL,
    activity_id INT NOT NULL,
    PRIMARY KEY (lecturer_id, activity_id),
    CONSTRAINT FK_ActivitySupervisor_Lecturer FOREIGN KEY (lecturer_id) REFERENCES Lecturer(person_id),
    CONSTRAINT FK_ActivitySupervisor_Activity FOREIGN KEY (activity_id) REFERENCES Activity(activity_id)
);

-- Table: Drink
-- vat_rate: 0.09 = non-alcoholic (9% BTW), 0.21 = alcoholic (21% BTW)
CREATE TABLE Drink (
    drink_id INT IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    price DECIMAL(10,2) NOT NULL,
    vat_rate DECIMAL(4,2) NOT NULL,
    stock INT NOT NULL
);

-- Table: DrinkOrder
CREATE TABLE DrinkOrder (
    order_id INT IDENTITY(1,1) PRIMARY KEY,
    student_id INT NOT NULL,
    drink_id INT NOT NULL,
    quantity INT NOT NULL,
    order_date DATETIME NOT NULL,
    CONSTRAINT FK_DrinkOrder_Student FOREIGN KEY (student_id) REFERENCES Student(person_id),
    CONSTRAINT FK_DrinkOrder_Drink FOREIGN KEY (drink_id) REFERENCES Drink(drink_id)
);

-- Test data

-- Buildings
INSERT INTO Building (name) VALUES ('Hoofdgebouw'), ('Bijgebouw');

-- Persons (students)
INSERT INTO Person (first_name, last_name, phone_number) VALUES ('Jan', 'de Vries', '06-12345678');
INSERT INTO Person (first_name, last_name, phone_number) VALUES ('Lisa', 'Bakker', '06-23456789');
INSERT INTO Person (first_name, last_name, phone_number) VALUES ('Pieter', 'Jansen', '06-34567890');
INSERT INTO Person (first_name, last_name, phone_number) VALUES ('Sophie', 'Mulder', '06-45678901');
INSERT INTO Person (first_name, last_name, phone_number) VALUES ('Thomas', 'Visser', '06-56789012');

-- Students
INSERT INTO Student (person_id, student_number, class) VALUES (1, 'ST001', 'ICT-1A');
INSERT INTO Student (person_id, student_number, class) VALUES (2, 'ST002', 'ICT-1A');
INSERT INTO Student (person_id, student_number, class) VALUES (3, 'ST003', 'ICT-1B');
INSERT INTO Student (person_id, student_number, class) VALUES (4, 'ST004', 'ICT-1B');
INSERT INTO Student (person_id, student_number, class) VALUES (5, 'ST005', 'ICT-1A');

-- Persons (lecturers)
INSERT INTO Person (first_name, last_name, phone_number) VALUES ('Karin', 'de Boer', '06-11111111');
INSERT INTO Person (first_name, last_name, phone_number) VALUES ('Henk', 'Peters', '06-22222222');
INSERT INTO Person (first_name, last_name, phone_number) VALUES ('Maria', 'van Dijk', '06-33333333');

-- Lecturers
INSERT INTO Lecturer (person_id, age) VALUES (6, 42);
INSERT INTO Lecturer (person_id, age) VALUES (7, 55);
INSERT INTO Lecturer (person_id, age) VALUES (8, 38);

-- Rooms
INSERT INTO Room (building_id, room_number, capacity, is_teacher_room) VALUES (1, 'A101', 4, 0);
INSERT INTO Room (building_id, room_number, capacity, is_teacher_room) VALUES (1, 'A102', 4, 0);
INSERT INTO Room (building_id, room_number, capacity, is_teacher_room) VALUES (1, 'A201', 2, 1);
INSERT INTO Room (building_id, room_number, capacity, is_teacher_room) VALUES (2, 'B101', 6, 0);
INSERT INTO Room (building_id, room_number, capacity, is_teacher_room) VALUES (2, 'B102', 2, 1);

-- Activities
INSERT INTO Activity (name, start_time, end_time) VALUES ('Kayaking', '2026-06-15 10:00:00', '2026-06-15 12:00:00');
INSERT INTO Activity (name, start_time, end_time) VALUES ('Hiking', '2026-06-15 14:00:00', '2026-06-15 17:00:00');
INSERT INTO Activity (name, start_time, end_time) VALUES ('Campfire', '2026-06-15 20:00:00', '2026-06-15 22:00:00');

-- Drinks (vat_rate: 0.09 = non-alcoholic, 0.21 = alcoholic)
INSERT INTO Drink (name, price, vat_rate, stock) VALUES ('Coca Cola', 2.50, 0.09, 50);
INSERT INTO Drink (name, price, vat_rate, stock) VALUES ('Water', 1.00, 0.09, 100);
INSERT INTO Drink (name, price, vat_rate, stock) VALUES ('Heineken', 3.50, 0.21, 30);
INSERT INTO Drink (name, price, vat_rate, stock) VALUES ('Red Wine', 4.00, 0.21, 20);
