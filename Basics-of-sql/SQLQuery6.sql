Create database assignment

use assignment

//Basic Task

CREATE TABLE Students2024 (ID INT, Name VARCHAR(50));
CREATE TABLE Students2025 (ID INT, Name VARCHAR(50));

INSERT INTO Students2024 VALUES
(1,'Ravi'),(2,'Asha'),(3,'John'),(4,'Meera'),(5,'Kiran'),
(6,'Divya'),(7,'Lokesh'),(8,'Anita'),(9,'Rahul'),(10,'Sneha');

INSERT INTO Students2025 VALUES
(2,'Asha'),(4,'Meera'),(5,'Kiran'),(11,'Prakash'),(12,'Vidya'),
(13,'Neha'),(14,'Manoj'),(15,'Ramesh'),(16,'Lata'),(17,'Akash');






CREATE TABLE Employees (EmpID INT, Name VARCHAR(50), Department VARCHAR(20));

INSERT INTO Employees VALUES 
(1,'Ananya','HR'),(2,'Ravi','IT'),(3,'Meera','Finance'),
(4,'John','IT'),(5,'Divya','Marketing'),(6,'Sneha','Finance'),
(7,'Lokesh','HR'),(8,'Asha','IT'),(9,'Kiran','Finance'),(10,'Rahul','Sales');





CREATE TABLE Projects (ProjectID INT, Name VARCHAR(50), StartDate DATE, EndDate DATE);
INSERT INTO Projects VALUES 
(1,'Bank App','2025-01-01','2025-03-15'),
(2,'E-Commerce','2025-02-10','2025-05-20');





CREATE TABLE Contacts (ID INT, Name VARCHAR(50), Email VARCHAR(50), Phone VARCHAR(20));
INSERT INTO Contacts VALUES
(1,'Ravi','ravi@gmail.com',NULL),
(2,'Asha',NULL,'9876543210'),
(3,'John',NULL,NULL);




/* Show a list of all student names (unique only).

Show a list of all student names (including duplicates). */


select Name from Students2024
union
select Name from Students2025
order by Name; 

select Name from Students2024
union all
select Name from Students2025
order by Name; 

/*
1. Display employee names in UPPERCASE and LOWERCASE.

2. Find the length of each employee’s name.

3. Show only the first 3 letters of each name.

4. Replace Finance department with Accounts.

Create a new column showing "Name - Department" using CONCAT.
*/


select Name, upper(Name) AS UppercaseName, lower(Name) as LowercaseName from Employees;


select Name, len(Name) AS Name_Length from Employees;

select NAME, substring(name, 1, 3) as first3 from employees;



select name, replace (Department, 'Finance', 'Accounts') as updated_dept from employees;



/* 
Date Functions (DATEDIFF, DATEADD, GETDATE, NOW)

Tasks:
Show today’s date using GETDATE().

Find the duration (in days) of each project using DATEDIFF.

Add 10 days to each project’s EndDate using DATEADD.

Find how many days are left until each project ends. (Hint: Use DATEDIFF with GETDATE())

*/


SELECT getdate() AS TodayDate;


select
ProjectID, Name, StartDate, EndDate, 
datediff(DAY, StartDate, EndDate) AS durationDays
from Projects;




select ProjectID, Name, EndDate,
dateadd(DAY, 10, EndDate) AS newEndDate
from Projects;



select  ProjectID, Name, EndDate,
datediff(DAY, GETDATE(), EndDate) AS daysLeft
FROM Projects;










/* 
CAST, CONVERT, CASE

Tasks:
Convert today’s date into DD/MM/YYYY format using CONVERT.

Convert a float 123.456 into an integer using CAST.

For employees (from Employees table above), categorize them:

If Dept = IT → show Tech Team

If Dept = HR → show Human Resources

Else → Other

(Hint: Use CASE expression)
*/




select convert(varchar, getdate(), 103) AS newDate;


select cast(123.456 as int) AS IntegerValue;