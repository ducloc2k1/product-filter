create database product_has_attribute

go 

use product_has_attribute

go

create table product
(
  id int identity primary key,
  name nvarchar(255),
  price int
)

go

insert into product values
('product1', 50),
('product2', 95.99)

go

create table attribute
(
  id int identity primary key,
  name varchar(10),
  attribute_value varchar(50)
)

go

insert into attribute values
('color', 'red'),
('color', 'yellow'),
('size', 'x-large'),
('size', 'x-medium')

go

create table product_attribute
(
  id int identity primary key,
  product_id int foreign key references product(id),
  attribute_id int foreign key references attribute(id)
)

go

insert into product_attribute values
(1,  1),
(1,  2),
(1,  3),
(2,  1),
(2,  2),
(2,  4);

go

create table variant(
	id int identity primary key,
	sku varchar(255),
	idProduct int foreign key references product(id)
)

go

insert into variant(sku, idProduct) values
('red-x-large', 1),
('yellow-x-medium',1)

go 

create table variant_attribute(
	id int identity primary key,
	variant_id int foreign key references variant(id),
	attribute_id int foreign key references attribute(id)
)

go

insert into variant_attribute values
(1, 1),
(1, 3),
(2, 2),
(2, 4)

go

create procedure getProductWithAttribute

as
begin 

select p.id, 
	   p.name,
	   a.name as attribute_name,
	   a.attribute_value
from product p join product_attribute pa on p.id = pa.product_id
			   join attribute a on a.id = pa.attribute_id
end

go

execute getProductWithAttribute

go

SELECT 
  p.id,
  p.name,
  p.price,
  a.attribute_value
from product p
left join product_attribute t
  on p.id = t.product_id
left join attribute a
  on t.attribute_id = a.id




