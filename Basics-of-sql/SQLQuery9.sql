create table student(
id int primary key,
name varchar(50),
);

select * from student


insert into student (id, name) values
(1, 'raj', 122),
(2, 'raju', 11);

insert into student values(2, 'raju', 123455)

alter table student
add ph_no int unique

UPDATE student
SET ph_no = 123453
WHERE name = 'raj';


create table teacher(
id int,
subject varchar(50),
foreign key (id) references student(id)
);

insert into teacher values(1, 'math')

select * from teacher

SELECT T1.name, T1.ph_no
FROM student AS T1
JOIN teacher AS T2
ON T1.id = T2.id
WHERE T1.id = 1 AND T2.subject = 'math';