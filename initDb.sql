USE shop;

DELETE FROM Manufacturers;

INSERT INTO Manufacturers
VALUES 
('NVIDIA'),
('GIGABYTE'),
('INTEL'),
('RYZEN'),
('AMD'),
('DELL'),
('ASUS'),
('HP'),
('IKEA'),
('LOGITECH'),
('ALIENWARE'),
('CORSAIR'),
('SONY'),
('GALAXY');

DELETE FROM Categories;

INSERT INTO Categories
VALUES 
('CPU'),
('GPU'),
('HDD'),
('SSD'),
('MBD'),
('RAM'),
('FAN'),
('POW'),
('MOUSE'),
('KEYBOARD'),
('MONITOR'),
('LAPTOP'),
('SMARTPHONE'),
('CAM'),
('CHAR');

DELETE FROM OrderStatus;

INSERT INTO OrderStatus
VALUES 
('��������'),
('����������'),
('��������');

DELETE FROM Sellers;

INSERT INTO Sellers 
VALUES
('asd', 'asd', 'asd', 'asd'),
('seller', 'pass', '������', '��������');

DELETE FROM Customers;

INSERT INTO Customers 
VALUES
('as', 'as', 'as', 'as'),
('customer', 'pass', '����', '������');

DELETE FROM Products;

INSERT INTO Products
VALUES 
(1000.00, '������ Dell XPS 15', '������ ������� ��� ������ � �����������', 10, (select c.id from Categories c where c.name = 'LAPTOP'), (select m.id from Manufacturers m where m.name = 'DELL')),
(500.50, '������� ������� ASUS ROG Swift', '������� � ������� ����������� ��� ���', 5, (select c.id from Categories c where c.name = 'MONITOR'), (select m.id from Manufacturers m where m.name = 'ASUS')),
(1200.00, '����������� ���������� NVIDIA GeForce RTX 3080', '���������� ��� ���������������� ����������� ����������', 3, (select c.id from Categories c where c.name = 'GPU'), (select m.id from Manufacturers m where m.name = 'NVIDIA')),
(800.00, '������� HP Pavilion', '������ � ���������� ������� ��� ������������� �������������', 8, (select c.id from Categories c where c.name = 'LAPTOP'), (select m.id from Manufacturers m where m.name = 'HP')),
(350.99, '������� ������ IKEA Markus', '������� ������ ��� ���������� ������ �� �����������', 15, (select c.id from Categories c where c.name = 'CHAR'), (select m.id from Manufacturers m where m.name = 'IKEA')),
(150.00, '������������ ���� Logitech MX Master 3', '������������ ���� � ���������� �������', 20, (select c.id from Categories c where c.name = 'MOUSE'), (select m.id from Manufacturers m where m.name = 'LOGITECH')),
(1200.00, '���������� ������� Alienware m15', '������ ������� ��� ��� � ������� �����', 5, (select c.id from Categories c where c.name = 'LAPTOP'), (select m.id from Manufacturers m where m.name = 'ALIENWARE')),
(200.00, '������������ ���������� Corsair K70', '������� ���������� � ������������� ���������������', 10, (select c.id from Categories c where c.name = 'KEYBOARD'), (select m.id from Manufacturers m where m.name = 'CORSAIR')),
(600.00, '����������� Sony Alpha a6400', '���������� ������ ��� ���������������� ������', 7, (select c.id from Categories c where c.name = 'CAM'), (select m.id from Manufacturers m where m.name = 'SONY')),
(450.50, '�������� Samsung Galaxy S21', '������ �������� � ������������������ �������', 12, (select c.id from Categories c where c.name = 'SMARTPHONE'), (select m.id from Manufacturers m where m.name = 'GALAXY'));
