
CREATE TRIGGER trg_after_insert_student
ON students
AFTER INSERT
AS
BEGIN
    DECLARE @first_name VARCHAR(50), @last_name VARCHAR(50);


    SELECT @first_name = first_name, @last_name = last_name FROM inserted;
    

    PRINT 'New student inserted: ' + @first_name + ' ' + @last_name;
END;


INSERT INTO students (student_id, first_name, last_name, age, email, enrollment_date)
VALUES 
(6, 'shivansh', 'paliwal', 24, 'Sp@email.com', '2025-10-09'),

INSERT INTO students (student_id, first_name, last_name, age, email, enrollment_date)
VALUES 
(7, 'lakshay', 'saxena', 22, 'ls@gmail.com', '2025-10-01');



INSERT INTO students (student_id, first_name, last_name, age, email, enrollment_date)
VALUES 
(8, 'suraj', 'kumar', 22, 'sk@gmail.com', '2025-10-01'),
(9, 'manish', 'singh', 22, 'ms@gmail.com', '2025-10-01');

