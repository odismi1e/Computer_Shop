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
('Заказано'),
('Доставлено'),
('Отменено');

DELETE FROM Sellers;

INSERT INTO Sellers 
VALUES
('asd', 'asd', 'asd', 'asd'),
('seller', 'pass', 'Елисей', 'Михайлов');

DELETE FROM Customers;

INSERT INTO Customers 
VALUES
('as', 'as', 'as', 'as'),
('customer', 'pass', 'Иван', 'Иванов');

DELETE FROM Products;

INSERT INTO Products
VALUES 
(1000.00, 'Лэптоп Dell XPS 15', 'Мощный ноутбук для работы и развлечений', 10, (select c.id from Categories c where c.name = 'LAPTOP'), (select m.id from Manufacturers m where m.name = 'DELL')),
(500.50, 'Гейминг монитор ASUS ROG Swift', 'Монитор с высоким разрешением для игр', 5, (select c.id from Categories c where c.name = 'MONITOR'), (select m.id from Manufacturers m where m.name = 'ASUS')),
(1200.00, 'Графическая видеокарта NVIDIA GeForce RTX 3080', 'Видеокарта для профессиональных графических приложений', 3, (select c.id from Categories c where c.name = 'GPU'), (select m.id from Manufacturers m where m.name = 'NVIDIA')),
(800.00, 'Ноутбук HP Pavilion', 'Легкий и компактный ноутбук для повседневного использования', 8, (select c.id from Categories c where c.name = 'LAPTOP'), (select m.id from Manufacturers m where m.name = 'HP')),
(350.99, 'Офисное кресло IKEA Markus', 'Удобное кресло для длительной работы за компьютером', 15, (select c.id from Categories c where c.name = 'CHAR'), (select m.id from Manufacturers m where m.name = 'IKEA')),
(150.00, 'Беспроводная мышь Logitech MX Master 3', 'Эргономичная мышь с множеством функций', 20, (select c.id from Categories c where c.name = 'MOUSE'), (select m.id from Manufacturers m where m.name = 'LOGITECH')),
(1200.00, 'Геймерский ноутбук Alienware m15', 'Мощный ноутбук для игр и тяжелых задач', 5, (select c.id from Categories c where c.name = 'LAPTOP'), (select m.id from Manufacturers m where m.name = 'ALIENWARE')),
(200.00, 'Механическая клавиатура Corsair K70', 'Игровая клавиатура с механическими переключателями', 10, (select c.id from Categories c where c.name = 'KEYBOARD'), (select m.id from Manufacturers m where m.name = 'CORSAIR')),
(600.00, 'Видеокамера Sony Alpha a6400', 'Зеркальная камера для профессиональной съемки', 7, (select c.id from Categories c where c.name = 'CAM'), (select m.id from Manufacturers m where m.name = 'SONY')),
(450.50, 'Смартфон Samsung Galaxy S21', 'Мощный смартфон с высококачественной камерой', 12, (select c.id from Categories c where c.name = 'SMARTPHONE'), (select m.id from Manufacturers m where m.name = 'GALAXY'));
