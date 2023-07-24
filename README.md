
# Nimap_Task  
Tasks which this project comprises are listed below.
1. Download File WebApplication3
2. Category Master Page with Crud Operation
3. Product Master Page with Crud Operation. A product Belongs to Category.
4. DashBoard Contains Product and Category.
5. Product List with Pagination.
# successfully completed all the tasks
  
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
 
