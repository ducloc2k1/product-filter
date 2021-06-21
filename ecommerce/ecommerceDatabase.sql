CREATE DATABASE ecommerce

GO

CREATE TABLE category (
	id INT PRIMARY KEY IDENTITY,
	name NVARCHAR(MAX) 
)

GO 

CREATE TABLE product(
	id INT  PRIMARY KEY IDENTITY,
	name NVARCHAR(MAX) ,
	idCategory INT FOREIGN KEY REFERENCES category(id)
)

GO

CREATE TABLE variant(
	id INT PRIMARY KEY IDENTITY,
	idProduct INT FOREIGN KEY REFERENCES product(id)
)

GO

CREATE TABLE [option](
	id INT PRIMARY KEY IDENTITY,
	name NVARCHAR(MAX),
)

GO

CREATE TABLE option_value(
	id INT PRIMARY KEY IDENTITY,
	name NVARCHAR(MAX),
	code NVARCHAR(MAX),
	idOption INT FOREIGN KEY REFERENCES [option](id)
)

GO

CREATE TABLE variant_option(
	id INT PRIMARY KEY IDENTITY,
	idVariant INT FOREIGN KEY REFERENCES variant(id),
	idOptionValue INT FOREIGN KEY REFERENCES option_value(id)
)

GO

INSERT INTO category VALUES 
	(N'clothes')

GO

INSERT INTO product VALUES
	('Black Cashmere V-neck Sweater',1),
	('Balloon-sleeve Turtleneck Sweater',1),
	('Band-collar Popover Tunic',1),
	('Ella Open-front Long Sweater-blazer',1),
	('Garment-dyed French Terry Hoodie',1)

GO

INSERT INTO variant VALUES
	(1),
	(1)

GO

INSERT INTO [option] VALUES
	('color'),
	('size')

GO

INSERT INTO option_value VALUES
	('surf blue', '#187a89',1),
	('cyan', '#1a9ccc',1),
	('black', '#000000',1),
	('brown', '#964f19',1),
	('light pink', '#ff9999',1),
	('pink', '#ff4f4f',1),
	('yellow', '#dbb20f',1),
	('x-small','',2),
	('small','',2),
	('medium','',2),
	('large','',2),
	('x-large','',2),
	('xx-large','',2),
	('3x-large','',2)

INSERT INTO variant_option VALUES	
	(1,1),
	(1,8),
	(2,3),
	(2,9)


