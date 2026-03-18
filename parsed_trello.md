# Trello Board: Project 1 - Databases

## List: Backlog

## List: to do

## List: doing

### As a user I want to view all lecturers
So that I know which lecturers are participating the Someren camp
**Checklist: Checklist**
  [ ] â€¢ Test data (lecturers) have been added to the database;
  [ ] â€¢ All lecturers are displayed in a structured way, ordered by last name;
  [ ] â€¢ For each lecturer the first name, last name, telephone number and age is displayed;

### As a user I want to view all rooms So that I know which rooms are used by students and which rooms are used by lecturers
**Checklist: Checklist**
  [x] â€¢ Test data (rooms) have been added to the database;
  [x] â€¢ All rooms are displayed in a structured way, ordered by room number;
  [ ] â€¢ For each room the number, size and type (student/lecturer) is displayed;

## List: Review

### Convert the ERD into a relational model
**Description:**
As a: developer

I want to: have a relational model based on the ERD diagram

So that: I can create and use a well structured database for my application

**Checklist: Checklist**
  [x] â€¢ (condition 1) All entities have a unique name;
  [x] (condition 2) All entities have a primary key;
  [x] (conversion rule 1) All n-to-m relations are represented by means of three tables: two for the entity types and one for the relationship type (containing reference keys to both entity types);
  [x] (conversion rule 2) For all n-to-1 and 1-to-n relations, the 1-sided entity type is converted into a table that contains a reference key to the n-side entity type;
  [x] (conversion rule 3) For all 1-to-1 relations, one of the entity types is converted into a table that contains all attribute types of the relationship as well as a reference key to the other entity type;
  [x] (conversion rule 4) For all tables with a reference key totally participating in a relationship, the reference key in that table is filled (not nullable);
  [x] (conversion rule 5) For all tables totally participating in a relationship, all key values occur at least once as the value of the reference key in the other table;
  [x] The relational model is checked and discussed in the group.

### Design an ERD
**Description:**
As a: developer

I want to: easily view the database structure via an ERD diagram

So that: I can easily understand, modify and integrate with the database schema

**Checklist: Checklist**
  [x] Analyze the Someren case and identify all entities, relationships and attributes.
  [x] The ERD contains all required entities.
  [x] The ERD contains all relationships between entities.
  [x] The ERD contains a complete attribute list.
  [x] The ERD contains the functionality (1, n, m) for all relationships.
  [x] Each functionality is also described in text to avoid confusion;
  [x] The ERD contains the totality (O, T) for all relationships;
  [x] Each totality is also described in text to avoid confusion;
  [x] The ERD is checked and discussed in the group.

### As a developer I want to create a database So that the Someren application can use it for storing/retrieving information
**Checklist: Checklist**
  [x] â€¢ An Azure database has been created (see manual SQL Server and Database creation manual);
  [x] â€¢ SQL Server Management Studio has been installed;
  [x] â€¢ All tables, columns and references (based on the relational model) has been created;

### As a developer I want to prepare the Visual Studio solution So that the functionalities for the Someren application can be implemented
**Checklist: Checklist**
  [x] â€¢ The Someren Visual Studio project (ASP.NET MVC) has been created;
  [x] â€¢ The database connection string has been configured in the project;
  [x] â€¢ A layout for all views of the Someren web application have been defined (including a top menu);
  [x] â€¢ A Git repository has been created for the Visual Studio project;

### As a user I want to view all students So that I know which students are participating the Someren camp
**Checklist: Checklist**
  [x] â€¢ Test data (students) have been added to the database;
  [x] â€¢ All students are displayed in a structured way, ordered by last name;
  [x] â€¢ For each student the student number, first name, last name, telephone number and class is displayed;

## List: done
