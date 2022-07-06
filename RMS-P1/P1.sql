create schema P1;

create table P1.users(
	userID int identity primary key,
	username varchar(50) unique not null,
	password varchar(20) not null,
	role varchar(8) default 'Employee'
);
select * from P1.users;
drop table P1.users; -- For fixing errors if they occur
insert into P1.users(username,password) values ('Kris','password');
insert into P1.users(username,password, role) values ('Juniper', 'password','Manager');

create table P1.tickets(
	ticketNum int identity primary key,
	status varchar(10) not null default 'Pending',
	author varchar(50) foreign key references P1.users(username),
	resolver varchar(50) foreign key references P1.users(username),
	description varchar(255) not null,
	amount money not null
);
select *from P1.tickets;
drop table P1.tickets;
insert into P1.tickets(author,resolver,description, amount) values ('Kris','Juniper','Testing Ticket table','18.32');
update P1.tickets set status ='Denied' where amount < 25;