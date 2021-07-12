create database product_has_attribute

go 

use product_has_attribute

go

create table product
(
  id int identity primary key,
  name nvarchar(255),
  price decimal(18,0),
  sale_price decimal(18,0),
  gallery text,
  status tinyint
)

go

insert into product values
('Analogue Resin Strap', 30, 0, 
'<images>
	<image>/assets/images/first_image.jpg</image>
	<image>/assets/images/second_image.jpg</image>
</images>', 1
),
('La Bohème Rose Gold', 60, 40,
'<images>
	<image>/assets/images/first_watch_image.jpg</image>
	<image>/assets/images/second_watch_image.jpg</image>
</images>', 0
)

go

create table attribute
(
  id int identity primary key,
  name varchar(10),
  attribute_value varchar(50),
  code varchar(50)
)

go

insert into attribute values
('color', 'pink', '#fcc6de'),
('color', 'black', '#000'),
('color', 'blue', '#a8bcd4'),
('color', 'grey', '#ccc'),
('size', 'x', null),
('size', 'm', null)

go

create table product_attribute
(
  id int identity primary key,
  product_id int,
  attribute_id int
  --product_id int foreign key references product(id),
  --attribute_id int foreign key references attribute(id)
)

go

insert into product_attribute values
(1,  1),
(1,  2),
(1,  5),
(1,  6),
(2,  5),
(2,  8),
(2,  3),
(2,  4);

go

create table variant(
	id int identity primary key,
	sku varchar(255),
	idProduct int foreign key references product(id),
	imagePath varchar(max),
)

go

insert into variant(sku, idProduct, imagePath) values
('red-x', 1, '/assets/images/first_image.jpg'),
('yellow-x',1,'/assets/images/second_image.jpg')

go 

create table variant_attribute(
	id int identity primary key,
	variant_id int,
	attribute_id int
	--variant_id int foreign key references variant(id),
	--attribute_id int foreign key references attribute(id)
)

go

insert into variant_attribute values
(1, 5),
(1, 1),
(2, 6),
(2, 4)

go

create table [order](
	id int primary key identity(1,1),
	created_date datetime,
	ship_name nvarchar(50),
	ship_mobile nvarchar(50),
	ship_address nvarchar(50),
	ship_email nvarchar(50),
	status int
)

go

create table [order_detail] (
	variant_id int foreign key references variant(id),
	order_id int foreign key references [order](id),
	quantity int,
	price float
)

go


--product
create procedure getProductWithAttribute

as
begin 

select p.id, 
	   p.name,
	   p.gallery,
	   a.name as attribute_name,
	   a.attribute_value
from product p join product_attribute pa on p.id = pa.product_id
			   join attribute a on a.id = pa.attribute_id
end

go

execute getProductWithAttribute

go

create procedure getAttributeProduct

@idProduct int
as
begin
select 
	   a.name,
	   a.attribute_value,
	   a.code
from product p join product_attribute pa on p.id = pa.product_id
			   join attribute a on a.id = pa.attribute_id
where p.id = @idProduct

end

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

go

create procedure getProductById
@idProduct int
as
begin 
select * from product where product.id = @idProduct
end

go

--Variant

CREATE procedure getVariantBySku
@idProduct int,
@sku varchar(255)
as
begin
select * from variant where variant.sku = @sku and variant.idProduct = @idProduct
end

go

execute getVariantBySku 1, "red-x-large"






