CREATE TABLE Categories (
    CategoryID INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    CategoryName VARCHAR(255) NOT NULL,
    IsCategoryActive BIT NOT NULL DEFAULT 1,
);


CREATE TABLE States (
    StateID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    StateName VARCHAR(255) NOT NULL,
    IsStateActive BIT NOT NULL DEFAULT 1
);

CREATE TABLE Tasks (
    TaskID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    Title VARCHAR(255) NOT NULL,
    Description VARCHAR(255),
    DueDate DATETIME,
    TaskCategoryID INT NOT NULL,
    TaskStateID INT NOT NULL,
    IsTaskActive BIT NOT NULL DEFAULT 1,
   
    LabelId int,
    UserId int,
    CONSTRAINT Task_Category_Id FOREIGN KEY (TaskCategoryID) REFERENCES Categories (CategoryID),
    CONSTRAINT Task_State_Id FOREIGN KEY (TaskStateID) REFERENCES States (StateID)
);

CREATE TABLE Labels(
    Id int not null identity(1,1) primary key,
    Name varchar(30),
    Description varchar(30),
    Color varchar(30)
);

CREATE TABLE Users(
    Id int not null identity(1,1) primary key,
    Name varchar(30),
    Email varchar(30),
    Password varchar(30)

);

ALTER TABLE Tasks
ADD StartDate DATETIME;

ALTER TABLE Tasks
ADD StoryPoints smallint;

alter table Tasks
add constraint vezaSaLabel
foreign key (LabelId) references Labels(Id);

alter table Tasks
add constraint vezaSaUser
foreign key (UserId) references Users(Id);


