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
    Priority VARCHAR(255) NOT NULL,
    DueDate DATETIME,
    TaskCategoryID INT NOT NULL,
    TaskStateID INT NOT NULL,
    IsTaskActive BIT NOT NULL DEFAULT 1,
    CONSTRAINT Task_Category_Id FOREIGN KEY (TaskCategoryID) REFERENCES Categories (CategoryID),
    CONSTRAINT Task_State_Id FOREIGN KEY (TaskStateID) REFERENCES States (StateID)
);
