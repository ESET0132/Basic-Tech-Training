create database bookStore
use bookStore

create table users(
user_id int primary key,
email varchar(50),
name varchar(50),
);



create table books(
product_id int primary key,
title varchar(50),
price varchar(50),
);

insert into users values(1, 'ramu@ramu.com', 'ramu')
insert into users values(2, 'Gopal@go.com', 'Gopal')
insert into users values(3, 'jay@jay.com', 'jay')
insert into users values(4, 'ganesh@ganesh.com', 'ganesh')

insert into users values(5, 'manu', 'manu@gmail.com');
insert into users values(6, 'yess', 'yess@gmail.com');
insert into users values(7, 'AssW', 'AssW@gmail.com');

select * from users

insert into books (product_id, title, price) values
(10, 'ramayan', '1500'),
(9, 'Book', '150'),
(8, 'States', '690'),
(7, 'Kalyug', '300');

insert into books values(6, 'Tinkle', '50');
insert into books values(5, 'Book1', '89');
insert into books values(4, 'Book2', '123');

select * from books

create table orders(
order_no int primary key,
user_id int,
product_id int,
foreign key (user_id) references users(user_id),
foreign key (product_id) references books(product_id)
);

select * from orders
select * from books

insert into orders(order_no, user_id, product_id) values
(101, 1, 10),
(102, 1, 7),
(103, 2, 8),
(104, 3, 9),
(105, 4, 8);

insert into orders(order_no, user_id, product_id) values
(106, 5, 6),
(107, 6, 5),
(108, 7, 5);

select * from orders




select * from users inner join orders on users.user_id = orders.user_id; 


SELECT
  *
from users as u
inner join orders as o
  on u.user_id = o.user_id
inner join books as b
  on o.product_id = b.product_id ;

SELECT
  *
from books as b
left join orders as o
  on b.product_id = o.product_id;

SELECT
  *
from books as b
right join orders as o
  on b.product_id = o.product_id;


  
SELECT
  *
from books as b
full outer join orders as o
  on b.product_id = o.product_id;


 /*find all the users and books*/ 

SELECT
  *
from users as u
full outer join orders as o
  on u.user_id = o.user_id
full outer join books as b
  on o.product_id = b.product_id ;

    
SELECT
  *
from users as u
inner join orders as o
  on u.user_id = o.user_id
inner join books as b
  on o.product_id = b.product_id ;

/*find users who bought kalyug, states and inkle books*/

 select
  u.name as user_name,
  b.title as book_title,
  b.price
from users as u
inner join orders as o
  on u.user_id = o.user_id
inner join books as b
  on o.product_id = b.product_id
where b.title in('Kalyug', 'States', 'Tinkle');


select 
u.name as user_name,
b.title as book_title,
o.order_no as orderno
from users as u
inner join orders as o
  on u.user_id = o.user_id
inner join books as b
  on o.product_id = b.product_id


  
select 
u.name as user_name,
SUM(b.price) as prices
from users as u
inner join orders as o
  on u.user_id = o.user_id
inner join books as b
  on o.product_id = b.product_id

  GROUP BY u.name


SELECT
u.name AS user_name,
SUM(b.price) AS total_spent
FROM users AS u
INNER JOIN orders AS o
ON u.user_id = o.user_id
INNER JOIN books AS b
ON o.product_id = b.product_id
GROUP BY
u.name;



select u.name as username, count(o.order_no)  as no_of_books
from users as u inner join orders as o on u.user_id = o.user_id group by u.name





/* Assignment-----1*/




select u.name, b.title, b.price,
row_number() over(partition by u.user_id order by b.price desc) as row_num,
rank() over(partition by u.user_id order by b.price desc) as popularity_rank,
dense_rank() over(partition by u.user_id order by b.price desc)as popurarity_dense_rank
from users as u inner join orders as o on u.user_id = o.user_id inner join books as b on o.product_id = b.product_id order by price


/* Assignment-----2*/


select u.name, b.price, SUM(b.price) AS total_spent
rank() over(partition by u.user_id order by total_spent desc) as popularity_rank,
dense_rank() over(partition by u.user_id order by total_spent desc)as popurarity_dense_rank
from users as u inner join orders as o on u.user_id = o.user_id inner join books as b on o.product_id = b.product_id group by total_spent

