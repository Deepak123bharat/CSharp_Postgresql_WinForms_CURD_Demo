--Create table students
create table Students 
(
	st_id serial primary key,
	st_firstname varchar(20),
	st_midname varchar(20),
	st_lastname varchar(20)
)

select * from students;

-- function to insert data
create or replace function st_insert(_firstname varchar, _midname varchar, _lastname varchar)
returns int as
$$ 
begin 
	insert into students(st_firstname, st_midname, st_lastname)
	values(_firstname, _midname, _lastname);
	if found then 
		return 1;
	else return 0;
	end if;
end
$$
language plpgsql

--test insert function 
select * from st_insert('Ravi','singh','Rajaput');
select * from st_insert('Baba','Jony','From heaven');

--create function to update student
create or replace function st_update(_id int, _firstname varchar, _midname varchar, _lastname varchar)
returns int as
$$
begin
	Update students
	Set 
		st_firstname = _firstname,
		st_midname = _midname,
		st_lastname = _lastname
	where 
		st_id = _id;
	if found then 
		return 1;
	else return 0;
	end if;
end
$$
language plpgsql
	
--test update function 
select * from st_update(2,'Jonny','baba','From hell');

--select function 
DROP FUNCTION st_select();
create or replace function st_select()
returns table
(
	id int,
	firstname varchar,
	midname varchar,
	lastname varchar
)as
$$
begin 
	return query 
	select st_id, st_firstname, st_midname, st_lastname from students Order by st_id;
end
$$
language plpgsql

--test select function 
select * from st_select();

create or replace function st_delete(_id int)
returns int as
$$
begin 
	Delete from students 
	where st_id = _id;
	if found then
		return 1;
	else return -1;
	end if;
end
$$
language plpgsql;

select * from st_delete(1);

