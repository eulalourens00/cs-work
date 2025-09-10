CREATE table Authors
(
Id INT Identity(1, 1) PRIMARY KEY,
FirstName NVARCHAR(50) NOT NULL,
LastName NVARCHAR(50) NOT NULL,
)

CREATE table Books
(
Id INT Identity(1,1) PRIMARY KEY,
AuthorId INT NOT NULL,
FOREIGN KEY (AuthorId) references Authors(Id),
Title NVARCHAR(100) NOT NULL,
Price INT, 
Pages INT
)
