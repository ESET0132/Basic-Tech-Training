CREATE TABLE Employee (
    id INT PRIMARY KEY,
    name VARCHAR(100),
    Dept Varchar(50),
	Salary float
);

INSERT INTO employee (id, name, Dept, Salary) VALUES
(1, 'shubham', 'HR',  200000.00),
(2, 'Akash', 'IT', 100000.00),
(3,'prateek', 'Senior-HR', 250000.00),
(4, 'kunal', 'MARKETING', 400000.00),
(5, 'surya', 'IT', 100000.00);

select * from Employee
select * from Employee where id  !=  1
select * from Employee where salary is null

select * from Employee where name like '%k'

select MAX(salary) from employee

SELECT *FROM Employee
WHERE salary = (SELECT MAX(salary) FROM employee);


SELECT *FROM Employee
WHERE salary  between 150000 and 400000 

select * from Employee
where salary in( select salary from Employee where salary  between 150000 and 400000 )


SELECT Salary
FROM Employee
ORDER BY Salary DESC
OFFSET 1 ROWS
FETCH NEXT 1 ROWS ONLY;
