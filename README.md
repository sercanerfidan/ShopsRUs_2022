# ShopsRUs_2022

ShopRUs Application

ShopsRUs is a .netcore wep application  that calculate a discount amount according to CustomerType, DiscountType and ProductType. Then creates an invoice according to discount amount.

Requirments to run application ;
* Net5.0 runtime
* Postman for calling api (recommended)
* SQLLITE for modifying and look up data and tables structure

build.ps1  -> To build project and run unit tests run this script in powershell in project root directory
run.ps1 -> To run application with command. Application will be start on 5001 port

Additional information;
* db file located in ShopsRUs/db/ShopsRUs.db
* About Discount table ;
    Discount service executes rules with data of this table.You can define diffrent discounts types. For example ;
    * If you define discount for CustomerType it sholuld be percent base and Discount type must be "Customer"
    * If you define amount base discount Discount Type must be "Amount"
    * If you define discount for category it must be percent base and Discount type must be "Product"
    * If you want to define discount for customer entry date you can select "Veteran" as Discount type in table.
  

