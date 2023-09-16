
#crud_operation
1. Category Master Page with Crud Operation
2. Product Master Page with Crud Operation. A product Belongs to Category.
3. DashBoard Contains Product and Category.
4. Product List with Pagination.

  
# Database code
6. CREATE TABLE Category (
   CategoryId INT PRIMARY KEY Identity,
   CategoryName VARCHAR(255)
   );

   CREATE TABLE Product (
   ProductId INT PRIMARY KEY Identity,
   ProductName VARCHAR(50),
   CategoryName VARCHAR(50) Not Null,
   CategoryId INT ,
   FOREIGN KEY (CategoryId) REFERENCES Category(CategoryId)
   );
 
