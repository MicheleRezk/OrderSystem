# OrderSystem
OrderSystem is simple Application for making waffle orders, it should integrate with waffle suppliers restful services, 
but for now it get suppliers info from json file in Data Folder inside OrderSystem.Web Project. 
It also save the orders in "orders.Json" file in the Data Folder. 

**you can Edit Suppliers Info Or Add New Suppliers from Json file.**

**The app choose the cheapest Supplier to make the waffle Orders.**

##  The app consist of these projects:
1.  OrderSystem.Domain (For Models like Supplier, Order, ..etc)
2.  OrderSystem.Application (For All Business Logic)
3.  OrderSystem.Application.Tests (Unit Testing for Business Logic)
4.  OrderSystem.Web (MVC Web Application for showing orders and making new orders)
