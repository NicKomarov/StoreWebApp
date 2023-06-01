SELECT Manufacturers.name
FROM Manufacturers
WHERE Manufacturers.id IN
	(SELECT Products.manufacturerId
	 FROM Products
	 WHERE Products.price != X);