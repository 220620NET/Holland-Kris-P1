create schema P1;

create table P1.users(
	userID int identity primary key,
	username varchar(50) unique not null,
	password varchar(20) not null,
	role varchar(8) default 'Employee'
);
-- GetAllUsers()
select * from P1.users;
drop table P1.users; -- For fixing errors if they occur
--CreateUser()
insert into P1.users(username,password) values ('Kris','password'); --employee
insert into P1.users(username,password, role) values ('Juniper', 'password','Manager');
-- GetUserByUsername()
select * from P1.users where username = 'Kris';
-- GetUserByID()
select * from P1.users where userID =1;
-- Creating columns after table creation
delete from P1.users is test;
alter table P1.users add test varchar(50);

select * from P1.users where userID  = 0; --checking a possible null response

create table P1.tickets(
	ticketNum int identity primary key,
	status varchar(10) not null default 'Pending',
	author int not null foreign key references P1.users(userID) default 1,
	resolver int foreign key references P1.users(userID) default 2,
	description varchar(255) not null,
	amount money not null
);
--GetAllTickets()
select * from P1.tickets;
-- For fixing errors
drop table P1.tickets;
-- CreateTicket()
insert into P1.tickets(author,description, amount) values (1,'Testing Ticket table',18.32);
-- UpdateTicket()
update P1.tickets set status ='Approved',resolver = 2 where ticketNum =1;
-- GetTicketByAuthor()
select * from P1.tickets where author = 1;
-- GetTicketByStatus()
select * from P1.tickets where status = 'Pending';
-- GetTicketByID()
select * from P1.tickets where ticketNum = 1;
