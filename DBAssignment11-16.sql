CREATE TABLE Products (
Id INT IDENTITY PRIMARY KEY NOT NULL,
Name NVARCHAR(80) NOT NULL,
Price Money NOT NULL
);

CREATE TABLE Customers (
ID INT IDENTITY PRIMARY KEY NOT NULL,
Firstname NVARCHAR(80) NOT NULL,
Lastname NVARCHAR(80) NOT NULL,
CardNumber NVARCHAR(16) NOT NULL CHECK(LEN(CardNumber) = 16)
);

CREATE TABLE Orders (
ID INT IDENTITY PRIMARY KEY NOT NULL,
ProductID INT
	FOREIGN KEY REFERENCES Products (ID),
CustomerID INT
	FOREIGN KEY REFERENCES Customers(ID)
);

INSERT INTO Products (Name, Price) VALUES
('Xbox', 300.00), 
('Playstation', 350.00),
('Nintendo Switch', 250.00);

INSERT INTO Customers (Firstname, Lastname, CardNumber) VALUES
('Richard', 'Hendrix', '1234123412341234'),
('Gavin', 'Belson', '2345234523452345'),
('Peter', 'Gregory', '3456345634563456');

INSERT INTO Orders (ProductID, CustomerID) VALUES
(1, 3),
(2, 2),
(3, 1);

INSERT INTO Products (Name, Price) VALUES ('iPhone', 200.00);

INSERT INTO Customers (FirstName, LastName, CardNumber) VALUES ('Tina', 'Smith', '4567456745674567');

INSERT INTO Orders (ProductID, CustomerID) VALUES (4, 4);

SELECT * FROM Orders WHERE CustomerID IN 
(SELECT ID FROM Customers WHERE Firstname = 'Tina' AND Lastname = 'Smith');

SELECT 'iPhone Sales', SUM(Price) FROM Products p INNER JOIN Orders o ON p.ID = o.ProductID 
WHERE p.ID IN (SELECT ID from Products WHERE Name = 'iPhone');


UPDATE Products SET Price = 250.00 WHERE Name = 'iPhone';