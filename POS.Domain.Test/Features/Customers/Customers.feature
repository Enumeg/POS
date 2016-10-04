Feature: Customers
	I want to Make CRUD Operations on Customer Table into DB


Scenario: Add New Customer to the Application
 Given That I have the following Customer Data
	| Name    | Phone | Email     | Address  | Balance |
	| M.hamed | 01144 | it.m.atef | 16 farid | 1000    |
    When I Add Customer with the previous data
    Then I should Get the iserted id


Scenario: Add New Customer to the Application with  the previous Name
 Given That I have the following Customer Data with  the previous Name
	| Name    | Phone | Email     | Address  | Balance |
	| M.hamed | 01144 | it.m.atef | 16 farid | 1000    |
    When I Add Customer with the Same data
    Then I should Get NULL


Scenario: Update Existing Customer
 Given That I have the New Customer Data with  Existing CustomerId
	| Id | Name   | Phone   | Email           | Address  | Balance |
	| 1  | M.Atef | 0121233 | m.hamed@ebc.net | 11 farid | 20      |
    When I Update Customer with this New Data
    Then I should Get Success With The New Data


Scenario: Update Existing Customer with another existing user name
 Given That I have the New Customer Data with  Existing CustomerId andd existing user name
	| Id | Name   | Phone   | Email           | Address  | Balance |
	| 3  | M.Atef | 0121233 | m.hamed@ebc.net | 11 farid | 20      |
    When I Update Customer with this New Data and user name exist
    Then I should Get Failed With The New Data


Scenario: Update Not Existing Customer
 Given That I have Not Existing Customer
	| Id    | Name   | Phone   | Email           | Address  | Balance |
	| 54335 | M.Atef | 0121233 | m.hamed@ebc.net | 11 farid | 20      |
    When I Update Not Existing Customer with this New Data
    Then I should Get Null Response


Scenario: Delete Existing Customer
 Given That I have Customer with  Existing CustomerId
	| Id | Name   | Phone   | Email           | Address  | Balance |
	| 2  | M.Atef | 0121233 | m.hamed@ebc.net | 11 farid | 20      |
    When I Delete this Customer 
    Then I should Get Success Delete


Scenario: Delete Not Existing Customer
 Given That I have Customer with  Not Existing CustomerId
	| Id | Name   | Phone   | Email           | Address  | Balance |
	| 6799  | M.Atef | 0121233 | m.hamed@ebc.net | 11 farid | 20      |
    When I Delete this Not Customer 
    Then I should Get Failed Delete


Scenario: Get All Existing Customer
 Given That I Want All  Customers 
    When I Call this Method Get All Customers 
    Then I should Get List Of All Customers





