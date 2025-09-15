CREATE TABLE students (
    student_id INT PRIMARY KEY,
    first_name VARCHAR(50),
    last_name VARCHAR(50),
    age INT,
    email VARCHAR(100),
    enrollment_date DATE
);

INSERT INTO students (student_id, first_name, last_name, age, email, enrollment_date) VALUES
(1, 'John', 'Doe', 20, 'john.doe@email.com', '2023-09-01'),
(2, 'Jane', 'Smith', 22, 'jane.smith@email.com', '2023-08-15'),
(3, 'Mike', 'Johnson', 21, 'mike.johnson@email.com', '2023-09-05'),
(4, 'Sarah', 'Wilson', 19, 'sarah.wilson@email.com', '2023-08-20'),
(5, 'David', 'Brown', 23, 'david.brown@email.com', '2023-09-10');


CREATE PROCEDURE retrieve_data
AS
BEGIN
    SELECT * FROM students;
END;

EXEC retrieve_data;

drop procedure retrieve_data


create procedure retrive_data
@student_id int as begin select * from students where student_id = @student_id;

EXEC retrive_data @student_id=1;



CREATE PROCEDURE check_student_age
    @student_id INT
AS
BEGIN
    DECLARE @student_age INT;
	

	SELECT @student_age = age FROM students WHERE student_id = @student_id;


	IF @student_age IS NULL
 
        PRINT 'Student not found';

	else if @student_age < 18

		print 'underage';

	else

		print 'Student is above 18'

END;

exec check_student_age @student_id = 4
    
drop procedure  check_student_age