Feature: Customers
	I want to Make CRUD Operations on Customer Table into DB


Scenario: Add New Customer to the Application
 Given That I have the following Customer Data
	| Name    | Phone | Email     | Address  | Balance |
	| Mohamed | 01144 | it.m.atef | 16 farid | 1000    |
    When I Add Customer with the previous data
    Then I should Get the iserted id
