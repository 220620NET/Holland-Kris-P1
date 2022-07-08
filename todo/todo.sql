create schema todoapp;
drop table todoapp.todos;
create table todoapp.todos(
	id int identity primary key,
	description varchar(200) not null,
	is_complete bit default 0
);
--CreateTodo()
insert into todoapp.todos(description) values ('Mop Floors');
--UpdateTodo()
update todoapp.todos set is_complete=1 where id=1;

--GetAllTodos()
select * from todoapp.todos;
--RemoveTodo()
delete from todoapp.todos where id=2;