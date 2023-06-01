SELECT Manufacturers.name
FROM Manufacturers
WHERE Manufacturers.id NOT IN
	(SELECT Products.manufacturerId
	 FROM Products
	 WHERE Products.name = X);