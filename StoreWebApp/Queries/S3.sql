SELECT Products.name, Products.price
FROM Products
WHERE Products.manufacturerId IN
	(SELECT Manufacturers.id
	 FROM Manufacturers
	 WHERE Manufacturers.countryId IN
	 	(SELECT Countries.id
		 FROM Countries
		 WHERE Countries.name = X));
