USE MNLibrary;
GO

CREATE TABLE Roles (
    RoleID INT PRIMARY KEY IDENTITY(1,1),
    RoleName NVARCHAR(50) NOT NULL
);
GO

CREATE TABLE Accounts (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    UserName NVARCHAR(50) NOT NULL,
    Email NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(50) NOT NULL,
    RoleID INT NOT NULL,
    CONSTRAINT FK_Users_Roles FOREIGN KEY (RoleID) REFERENCES Roles(RoleID)
);
GO

CREATE TABLE Authors (
    AuthorID INT PRIMARY KEY IDENTITY(1,1),
    AuthorName NVARCHAR(100) NOT NULL
);
GO

CREATE TABLE Categories (
    CategoryID INT PRIMARY KEY IDENTITY(1,1),
    CategoryName NVARCHAR(100) NOT NULL
);
GO

CREATE TABLE Books (
    BookID INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(100) NOT NULL,
    ISBN NVARCHAR(13) NOT NULL UNIQUE,
    AuthorID INT NOT NULL,
    Image VARCHAR(MAX),
    CategoryID INT NOT NULL,
    CONSTRAINT FK_Books_Authors FOREIGN KEY (AuthorID) REFERENCES Authors(AuthorID),
    CONSTRAINT FK_Books_Categories FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID)
);
GO

ALTER TABLE Books
ADD 
    CreateDate DATETIME NOT NULL DEFAULT GETDATE(),
    UpdateDate DATETIME NULL,
    Quantity INT NOT NULL DEFAULT 0;
GO

CREATE TABLE BorrowRecords (
    BorrowID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    BookID INT NOT NULL,
    BorrowDate DATE NOT NULL,
    ReturnDate DATE NULL,
    CONSTRAINT FK_BorrowRecords_Accounts FOREIGN KEY (UserID) REFERENCES Accounts(UserID),
    CONSTRAINT FK_BorrowRecords_Books FOREIGN KEY (BookID) REFERENCES Books(BookID)
);
GO

CREATE TABLE Profiles (
    ProfileID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    FullName NVARCHAR(100) NOT NULL,
    DateOfBirth DATE NULL,
    Address NVARCHAR(255) NULL,
    PhoneNumber NVARCHAR(15) NULL,
    CONSTRAINT FK_Profiles_Accounts FOREIGN KEY (UserID) REFERENCES Accounts(UserID)
);
GO

CREATE TABLE BorrowDetails (
    BorrowDetailID INT PRIMARY KEY IDENTITY(1,1),
    BorrowID INT NOT NULL,
    BorrowDate DATETIME NOT NULL,
    DueDate DATETIME NOT NULL,
    ReturnDate DATETIME NULL,
    QuantityBorrowed INT NOT NULL,
    FineAmount DECIMAL(10, 2) NULL,
    Status NVARCHAR(50) NOT NULL DEFAULT 'Pending',
    Notes NVARCHAR(255) NULL,
    CONSTRAINT FK_BorrowDetails_BorrowRecords FOREIGN KEY (BorrowID) REFERENCES BorrowRecords(BorrowID)
);
GO


