CREATE TABLE Departments (
    DepartmentID INT PRIMARY KEY,
    DepartmentName VARCHAR(255) NOT NULL
);

CREATE TABLE Managers (
    ManagerID INT PRIMARY KEY,
    FullName VARCHAR(255) NOT NULL,
    DepartmentID INT,
    FOREIGN KEY (DepartmentID) REFERENCES Departments(DepartmentID)
);

CREATE TABLE Clients (
    ClientID INT PRIMARY KEY,
    FullName VARCHAR(255) NOT NULL,
    Phone VARCHAR(50)
);

CREATE TABLE Contracts (
    ContractNumber VARCHAR(20) PRIMARY KEY, -- Номер договора (например, '2025/01-185')
    ContractDate DATE NOT NULL,
    ClientID INT,
    ManagerID INT,
    ServiceType VARCHAR(100), -- Тип услуги (Консультация, Тренинг и т.д.)
    ServiceName VARCHAR(255), -- Название услуги
    Price DECIMAL(10, 2),     -- Цена
    PaymentMethod VARCHAR(50),-- Способ оплаты
    FOREIGN KEY (ClientID) REFERENCES Clients(ClientID),
    FOREIGN KEY (ManagerID) REFERENCES Managers(ManagerID)
);

INSERT INTO Departments (DepartmentID, DepartmentName) VALUES
(1, 'Отдел продаж для VIP'),
(2, 'Отдел продаж #2'),
(3, 'Министерство нестандартных решений'),
(4, 'Лаборатория хаоса и мелких пакостей');

INSERT INTO Managers (ManagerID, FullName, DepartmentID) VALUES
(1, 'Петров Павел Петрович', 1),
(2, 'Сидорова Светлана Сергеевна', 2),
(3, 'Кузнецов Кирилл Константинович', 3),
(4, 'Гурова Мария', 2),
(5, 'Сидорова Ульяна Павловна', 4);

INSERT INTO Clients (ClientID, FullName, Phone) VALUES
(1, 'Иванов Иван Иванович', '8-911-111-11-11'),
(2, 'Петрова Анна Сергеевна', '8-922-222-22-22'),
(3, 'Смирнов Виктор Павлович', '8-933-333-33-33, 8-944-444-44-44'),
(4, 'Козлов Дмитрий Алексеевич', '8-955-555-55-55'),
(5, 'Николаев Артём Петрович', '8-966-666-66-66'),
(6, 'Федорова Ольга Ивановна', '8-977-777-77-77');

INSERT INTO Contracts (ContractNumber, ContractDate, ClientID, ManagerID, ServiceType, ServiceName, Price, PaymentMethod) VALUES
('2025/01-185', '2025-01-30', 1, 1, 'Консультация', '«Как переговорить с катусом»', 2000, 'Карта'),
('2025/01-185', '2025-01-30', 1, 1, 'Тренинг', '«Тайм-менеджмент в ядерном апокалипсисе»', 5000, 'Карта'),
('2025/01-186', '2025-01-31', 2, 2, 'Розыгрыш', '«Коллега-призрак»', 10000, 'Наличные'),
('2025/01-187', '2025-01-31', 3, 3, 'Курс', '«Ораторское мастерство для интровертов»', 15000, 'Карта'),
('2025/02-001', '2025-02-01', 4, 4, 'Внедрение', '«Абсурдного регламента»', 12000, 'Перевод'),
('2025/02-002', '2025-02-01', 5, 5, 'Пранк', '«Экстренный розыгрыш»', 3000, 'Наличные'),
('2025/02-003', '2025-02-01', 6, 1, 'Тренинг', '«Курс молодого паникера»', 4000, 'Карта');

SELECT 
    c.ContractNumber, 
    c.ContractDate, 
    cl.FullName AS ClientName, 
    m.FullName AS ManagerName, 
    d.DepartmentName, 
    c.ServiceType, 
    c.ServiceName, 
    c.Price, 
    c.PaymentMethod
FROM Contracts c
JOIN Clients cl ON c.ClientID = cl.ClientID
JOIN Managers m ON c.ManagerID = m.ManagerID
JOIN Departments d ON m.DepartmentID = d.DepartmentID;

SELECT 
    m.FullName AS ManagerName,
    SUM(c.Price) AS TotalRevenue
FROM Contracts c
JOIN Managers m ON c.ManagerID = m.ManagerID
GROUP BY m.FullName
ORDER BY TotalRevenue DESC;
