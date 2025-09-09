create database EsyasoftHolding

use esyasoftHolding

create table Customers(
CustomerId int PRIMARY KEY,
Name varchar(50),
Address varchar(200),
Region varchar(50)
);


create table smarMeterReadings(
meterId int PRIMARY KEY,
CustomerId int,
location varchar(50),
installedDate date,
readingDateTime datetime,
EnergyConsumed float,
FOREIGN KEY(customerId) references Customers(customerId)
);

select * from smarMeterReadings

INSERT INTO Customers (CustomerId, Name, Address, Region)
VALUES
  (1, 'Shivansh', 'Jodhpur', 'North'),
  (2, 'Lakshay', 'Dwarka, Delhi', 'South'),
  (3, 'Piyush', 'Bokaro, jharkhand', 'East'),
  (4, 'mohit', 'Hydrabad, TS', 'South');




INSERT INTO smarMeterReadings (meterId, CustomerId, location, installedDate, readingDateTime, EnergyConsumed)
VALUES
  (101, 1, 'Main House', '2025-01-10', '2025-09-09 08:00:00', 50.75),
  (102, 2, 'Gate', '2025-02-10', '2025-09-09 01:50:00', 34.35),
  (103, 3, 'Rooftop', '2025-03-10', '2025-09-01 18:20:00', 90.15),
  (104, 4, ' Backyard', '2025-04-11', '2025-01-04 12:10:00', 80.45);

  INSERT INTO smarMeterReadings (meterId, CustomerId, location, installedDate, readingDateTime, EnergyConsumed)
VALUES
  (107, 1, 'Main House', '2025-01-10', '2025-09-01 01:00:00', 66.75),
  (108, 2, 'Gate', '2025-03-10', '2025-09-09 02:50:00', 322.35);

  /*Task - 1*/


  /*Energy Consumed is between 10 and 50 kw*/

 SELECT
    s.MeterId,
    s.ReadingDateTime,
    s.EnergyConsumed
FROM smarMeterReadings s WHERE s.EnergyConsumed BETWEEN 10 AND 50

/*Max energy consumed between date '2024-01-01' and '2024-12-31'*/

SELECT
    s.MeterId,
    s.ReadingDateTime,
    s.EnergyConsumed
from smarMeterReadings s where s.readingDateTime between '2024-01-01' and '2024-12-31'

/*Exclude meters installed after 2024-06-30*/

SELECT
    s.MeterId,
    s.ReadingDateTime,
    s.EnergyConsumed
from smarMeterReadings s where s.installedDate <= '2024-06-30'


/*Task - 2 */

/*AVG energy consumed per reading*/

select CustomerId, avg(EnergyConsumed) from smarMeterReadings group by customerId

/*Max energy consumed in single reading*/

select CustomerId, max(EnergyConsumed) from smarMeterReadings group by customerId


/*Only include reading from current year */

select CustomerId, readingDateTime from smarMeterReadings where YEAR(readingDateTime) = YEAR(getdate()) 



select count(CustomerId) from Customers

   
