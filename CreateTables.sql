use Pssc;

create table LoginT(
Id bigint primary key identity,
UserName nvarchar(25) not null,
UserPassword nvarchar(25) not null,
UserRole bigint not null
);

create table Student(
Id bigint primary key identity,
StudentFirstName nvarchar(25) not null,
StudentLastName nvarchar(25) not null,
Faculty nvarchar(50) not null
);

create table StudentGrades(
Id bigint primary key identity,
StudentId bigint not null,
Grade int not null,
constraint StudGrades_Fk foreign key(StudentId) references Student(Id) on update cascade on delete cascade
);

create table DormList(
Id bigint primary key identity,
StudentId bigint not null,
Average int not null,
constraint DormList_FK foreign key(StudentId) references Student(Id) on update cascade on delete cascade
);

insert into LoginT(UserName,UserPassword,UserRole) values('profesor','profesor',1),('secretariat','secretariat',1),('administrator','administrator',1);