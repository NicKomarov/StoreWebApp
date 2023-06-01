SELECT AVG(Products.price)
FROM Products
WHERE Products.ManufacturerId IN
	(SELECT Manufacturers.id
	 FROM Manufacturers
	 WHERE Manufacturers.name = X);