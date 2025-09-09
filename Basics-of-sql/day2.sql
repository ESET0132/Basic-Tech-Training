create database Google
use Google

CREATE TABLE Employee (
    id INT PRIMARY KEY,
    name VARCHAR(100),
    Dept Varchar(50),
	Salary float
);

INSERT INTO employee (id, name, Dept, Salary) VALUES
(1, 'Sarath', 'HR',  200000.00),
(2, 'Akash', 'IT', 100000.00),
(3,'Abhishek', 'Senior-HR', 250000.00),
(4, 'kunal', 'MARKETING', 400000.00),
(5, 'Sowmya', 'IT', 100000.00);

Update Employee
set Salary = Salary + Salary * 0.4
where employee.name = 'sowmya'

select * from employee


delete from employee where employee.name = 'Abhishek'

EXEC sp_rename 'Employee', 'friends';

select * from friends

ALTER TABLE friends
ADD address VARCHAR(255);

ALTER TABLE friends
ALTER COLUMN Salary VARCHAR(50);

ALTER TABLE friends
Add phone_no varchar(50);

UPDATE friends
SET address = 'Mangalore'
WHERE name = 'Akash';

Alter TABLE friends
drop column address

EXEC sp_rename 'friends.phone_no', 'contact', 'COLUMN';

