SELECT Countries.name
FROM Countries
WHERE Countries.id IN
	(SELECT Manufacturers.countryId
	 FROM Manufacturers
	 WHERE Manufacturers.id IN
		(SELECT D.id
		 FROM Manufacturers D
		 WHERE NOT EXISTS
	 		((SELECT Products.price
			  FROM Products
		      WHERE Products.manufacturerId = Q)
		     EXCEPT
		     (SELECT Products.price
		      FROM Products
		      WHERE Products.manufacturerId = D.id AND Products.manufacturerId != Q))));