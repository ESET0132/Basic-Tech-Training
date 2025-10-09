create database Advancetraining
use Advancetraining

create table studentadvanceT(
    studentid int primary key, 
    name varchar(50),
    subject1marks float,
    subject2marks float,
    subject3marks float,
    totalmarks as (subject1marks + subject2marks + subject3marks),
    averagemarks as ((subject1marks + subject2marks + subject3marks)/3)
);

insert into studentadvanceT values(1, 'shivansh', 56, 76, 76)
insert into studentadvanceT values(2, 'laksh', 28, 68, 45)
insert into studentadvanceT values(3, 'surya', 58, 28, 78)
insert into studentadvanceT values(4, 'manish', 86, 67, 56)
insert into studentadvanceT values(5, 'dhanno', 21, 87, 26,'2002-12-12','fail')


select * from studentadvanceT

alter table studentadvanceT add DOB date
update studentadvanceT set DOB =  '2001-11-06' where name = 'shivansh'
update studentadvanceT set DOB =  '2002-10-18' where name = 'laksh'
update studentadvanceT set DOB =  '2002-12-12' where name = 'surya'
update studentadvanceT set DOB =  '2003-02-22' where name = 'manish'

select * from studentadvanceT where subject1marks > 35 and subject2marks > 35 and subject3marks > 35
select * from studentadvanceT where DOB Between '2001-11-02' and '2002-11-08'
select * from studentadvanceT where subject1marks < 35 or subject2marks < 35 or subject3marks < 35
select * from studentadvanceT where name = 'shivansh'
select * from studentadvanceT where name = 'shivansh' or name = 'laksh'
select * from studentadvanceT where name in('shivansh', 'laksh', 'jassi', 'surya')
select * from studentadvanceT where month(DOB) = 12
select * from studentadvanceT where month(DOB) in(2, 12, 4, 10)
select * from studentadvanceT where month(DOB) between 6 and 12

select * from studentadvanceT where day(DOB) = 06
select * from studentadvanceT where year(DOB) = 2002


select 
    name,
    DOB,
    dateadd(day, 7, DOB) AS OneWeekLater,
    dateadd(day, 30, DOB) AS OneMonthLaterDays,
    dateadd(day, 365, DOB) AS OneYearLaterDays
FROM studentadvanceT

select 
name,
DOB,
    getdate() AS CurrentDate,
    datediff(year, DOB, getdate()) AS CurrentAge
FROM studentadvanceT



SELECT 
    name,
    DOB,
    DATEPART(YEAR, DOB) AS BirthYear,
    DATEPART(MONTH, DOB) AS BirthMonth,
    DATEPART(DAY, DOB) AS BirthDay
FROM studentadvanceT


SELECT 
    name,
    DOB,
    ISDATE(CONVERT(VARCHAR, DOB)) AS IsValidDate 
FROM studentadvanceT

SELECT 
    name,
    UPPER(name) AS UpperCase,
    LOWER(name) AS LowerCase
FROM studentadvanceT

SELECT 
    name,
    LTRIM(name) AS LeftTrimmed,
    RTRIM(name) AS RightTrimmed,
    TRIM(name) AS FullyTrimmed
FROM studentadvanceT

SELECT 
    name,
    LEFT(name, 5) AS First5Chars,
    RIGHT(name, 5) AS Last5Chars
FROM studentadvanceT


SELECT * FROM studentadvanceT 
WHERE name LIKE 's%'

SELECT * FROM studentadvanceT 
WHERE name LIKE '%s'

select name, len(name) as name_length from studentadvanceT

SELECT 
    name + ' has scored ' + CAST(averagemarks AS VARCHAR) + ' marks' AS Result
FROM studentadvanceT



SELECT 
    name,
    REPLICATE('*', 5) AS FiveStars,
    REPLICATE('-', 10) AS TenDashes,
    REPLICATE(name + ' ', 2) AS NameTwice
FROM studentadvanceT


SELECT 
    name,
    REPLACE(name, ' ', '_') AS NoSpaces,
    REPLICATE('*', LEN(REPLACE(name, ' ', ''))) AS PatternByLength,
    REPLACE(name, ' ', '') + REPLICATE('!', 3) AS NameWithExclamation
FROM studentadvanceT


SELECT 
    GETDATE() AS CurrentDate,
    FORMAT(GETDATE(), 'dd/MM/yyyy') AS UK_Format,
    FORMAT(GETDATE(), 'MM/dd/yyyy') AS US_Format,
    FORMAT(GETDATE(), 'yyyy-MM-dd') AS ISO_Format



CREATE TABLE studentarchiveT(
    studentid int primary key, 
    name varchar(50),
    subject1marks float,
    subject2marks float,
    subject3marks float,
    totalmarks float,
    averagemarks float,
    DOB date,
    archiveddate datetime DEFAULT GETDATE()
);

insert into studentarchiveT(studentid, name, subject1marks, subject2marks, subject3marks, totalmarks, averagemarks, DOB) 
select studentid, name, subject1marks, subject2marks, subject3marks, totalmarks, averagemarks, DOB from studentadvanceT

select * from studentarchiveT


SELECT * INTO studentcopy1 
FROM studentadvanceT;

SELECT * FROM studentcopy1;

ALTER TABLE studentadvanceT ADD result VARCHAR(10);
UPDATE studentadvanceT SET result = 'PASS' where  subject1marks >35 and subject2marks >35 and subject3marks >35 
UPDATE studentadvanceT SET result = 'PASS' where  subject1marks <35 or subject2marks <35 or subject3marks <35 

select * from studentadvanceT

SELECT TOP 3 * FROM studentadvanceT;

select name, subject1marks, subject2marks, subject3marks from studentadvanceT where result = 'PASS' group by name, subject1marks, subject2marks, subject3marks, averagemarks;
select name, result from studentadvanceT order by result, name


create table practiceConstraints (
    studentid int primary key,
    name varchar(50) not null,          
    subject1marks float not null,        
    DOB date not null,
	email varchar(50) unique
);

select * from practiceConstraints

alter table practiceConstraints add age int check (age >= 18 AND age <= 65)


CREATE TABLE Departments (
    deptid INT PRIMARY KEY,           
    deptname VARCHAR(50) NOT NULL,
    location VARCHAR(50)
);

CREATE TABLE StudentsAA (
    studentid INT PRIMARY KEY,       
    age INT CHECK (age >= 17),     
    deptid INT,                      
    FOREIGN KEY (deptid) REFERENCES Departments(deptid) 
);

INSERT INTO Departments VALUES 
(1, 'Computer Science', 'Building A'),
(2, 'Mathematics', 'Building B'),
(3, 'Physics', 'Building C');

SELECT * FROM Departments;


INSERT INTO StudentsAA VALUES 
(101 , 20, 1),   
(102, 19, 2),   
(103, 21, 1),   
(104, 18, 3);  

SELECT * FROM StudentsAA;