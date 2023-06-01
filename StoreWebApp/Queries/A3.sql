SELECT C.lastname, C.email
FROM Customers C
WHERE C.name = Y
AND NOT EXISTS
	((SELECT Orders.productId
	  FROM Orders
	  WHERE Orders.customerId = C.id)
	 EXCEPT
	 (SELECT Products.id
	  FROM Products))
AND NOT EXISTS
	((SELECT Products.id
	  FROM Products)
	 EXCEPT
	 (SELECT Orders.productId
	  FROM Orders
	  WHERE Orders.customerId = C.id));
