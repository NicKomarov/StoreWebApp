SELECT Customers.name, Customers.lastname
FROM Customers
WHERE Customers.id IN
	(SELECT Orders.customerId
	 FROM Orders
	 WHERE Orders.productId IN
	 	(SELECT Products.id
		 FROM Products
		 WHERE Products.manufacturerId IN
		 	(SELECT Manufacturers.id
			 FROM Manufacturers
			 WHERE Manufacturers.name = X)));
