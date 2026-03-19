-- Clean up existing objects in correct dependency order
IF OBJECT_ID('OrderItem', 'U') IS NOT NULL DROP TABLE OrderItem;
IF OBJECT_ID('Order', 'U') IS NOT NULL DROP TABLE [Order];
IF OBJECT_ID('Drink', 'U') IS NOT NULL DROP TABLE Drink;
IF OBJECT_ID('ActivitySupervisor', 'U') IS NOT NULL DROP TABLE ActivitySupervisor;
IF OBJECT_ID('ActivityParticipant', 'U') IS NOT NULL DROP TABLE ActivityParticipant;
IF OBJECT_ID('Activity', 'U') IS NOT NULL DROP TABLE Activity;
IF OBJECT_ID('RoomAssignment', 'U') IS NOT NULL DROP TABLE RoomAssignment;
IF OBJECT_ID('Room_base', 'U') IS NOT NULL DROP TABLE Room_base;
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

-- Table: RoomAssignment
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
CREATE TABLE Drink (
    drink_id INT IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    price DECIMAL(10,2) NOT NULL,
    vat_rate DECIMAL(4,2) NOT NULL,
    stock INT NOT NULL
);

-- Table: [Order]
CREATE TABLE [Order] (
    order_id INT IDENTITY(1,1) PRIMARY KEY,
    student_id INT NOT NULL,
    order_date DATETIME NOT NULL,
    CONSTRAINT FK_Order_Student FOREIGN KEY (student_id) REFERENCES Student(person_id)
);

-- Table: OrderItem
CREATE TABLE OrderItem (
    order_id INT NOT NULL,
    drink_id INT NOT NULL,
    quantity INT NOT NULL,
    PRIMARY KEY (order_id, drink_id),
    CONSTRAINT FK_OrderItem_Order FOREIGN KEY (order_id) REFERENCES [Order](order_id),
    CONSTRAINT FK_OrderItem_Drink FOREIGN KEY (drink_id) REFERENCES Drink(drink_id)
);
