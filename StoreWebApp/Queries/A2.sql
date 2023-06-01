SELECT C.lastname
FROM Customers C
WHERE C.lastname != Y
AND NOT EXISTS
    ((SELECT Orders.productId
      FROM Orders
      WHERE Orders.customerId = C.id)
     EXCEPT
     (SELECT Orders.productId
      FROM Orders
      WHERE Orders.customerId IN
          (SELECT Customers.id
           FROM Customers
           WHERE Customers.lastname = Y)))
AND NOT EXISTS
    ((SELECT Orders.productId
      FROM Orders
      WHERE Orders.customerId IN
  	      (SELECT Customers.id
           FROM Customers
           WHERE Customers.lastname = Y))
     EXCEPT
     (SELECT Orders.productId
      FROM Orders
      WHERE Orders.customerId = C.id));
