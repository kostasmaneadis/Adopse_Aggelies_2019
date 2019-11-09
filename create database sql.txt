create table Users (
	 id Integer not null ,
	 username varchar(50) not null,
	 password varchar(50) not null,
	 surname varchar (50) not null,
	 name varchar(50) not null,
	 fathersName varchar(50),
	 born date,
	 role varchar(5), 
	 PRIMARY KEY (id)
	);

create table Ads (
	 id Integer not null ,
	 dateAdded date not null,
	 description varchar(2000),
	 superAd  bit not null,
	 address varchar(50),
	 primary key (id)
	);
 
 create table Photos (
	 photoid Integer not null ,
	 adsId int not null, 
	 photo blob,
	 PRIMARY KEY (photoid),
	 foreign key (adsId) references Ads(id)
	 );
	 
create table Property(
	 propertyid int not null,
	 kind varchar(50) not null,
	 buy_rent varchar(10) not null,
	 area float,
	 dateBuil date,
	 price float,
	 new bit,
	 PRIMARY KEY (propertyid),
         FOREIGN KEY (propertyid) REFERENCES Ads(id)
	 );
	 
	 
create table Automoto(
	 automotoid  int not null,
	 kind varchar(50) not null,
	 make varchar(10) not null,
	 mode varchar(10) not null,
	 color varchar(10) not null,
	 damages varchar(10),
	 cubitCapacity int(50) ,
	 power int (50),
	 fuel varchar(10) not null,
	 firstRegistration date,
	 features varchar(50) not null,
	 price float,
	 new bit,
         PRIMARY KEY (automotoid),
         FOREIGN KEY (automotoid) REFERENCES ads(id)
	 );

create table jobs (
	 jobsid int not null,
	 category varchar (50) not null,
	 full_part_time varchar(10), 
   	 jobs_employers varchar (50) not null,
         PRIMARY KEY (jobsid),
         FOREIGN KEY (jobsid) REFERENCES Ads(id)
	 );
	 
create table health (
	 healthid int not null,
	 category varchar (20),
         PRIMARY KEY (healthid),
         FOREIGN KEY (healthid) REFERENCES ads(id) 
	 );

	 SELECT * FROM Ads, health;
	
	 

