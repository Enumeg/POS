Feature: LoginShifts
	I want to check login process for shifts

Scenario: Login to already opened shift for the same user on the same machine
	Given That I have the following shifts
	| Id | UserId | UserName | MachineId | MachineName | StartDate           | EndDate | Balance | IsClosed |
	| 1  | asdasd | mostafa  | 1         | pc1         | 01-01-2016 12:00:00 | NULL    | 500     | False    |
    When I login as mostafa with Id "asdasd" on Machine with id "1"
    Then I should continue with the opened shift

Scenario: Login with no opened shift for the same user on the same machine
    Given That I have the following shifts
	| Id | UserId | UserName | MachineId | MachineName | StartDate           | EndDate             | Balance | IsClosed |
	| 1  | asdasd | mostafa  | 1         | pc1         | 01-01-2016 12:00:00 | 01-01-2016 16:00:00 | 500     | True     |
    When I login as mostafa with Id "asdasd" on Machine with id "1"
    Then A new shift is opened
	
Scenario: Login to already opened shift for another user on the same machine
    Given That I have the following shifts
	| Id | UserId | UserName | MachineId | MachineName | StartDate           | EndDate | Balance | IsClosed |
	| 1  | wwwwww | Ahmed    | 1         | pc1         | 01-01-2016 12:00:00 | NULL    | 500     | False    |
    When I login as mostafa with Id "asdasd" on Machine with id "1"
    Then The opened shift is closed 
	And A new shift is opened

Scenario: Login to an opened shift for the same user on another machine
    Given That I have the following shifts
	| Id | UserId | UserName | MachineId | MachineName | StartDate           | EndDate | Balance | IsClosed |
	| 1  | asdasd | mostafa  | 1         | pc2         | 01-01-2016 12:00:00 | NULL    | 500     | False    |
    When I login as mostafa with Id "asdasd" on Machine with id "1"
    Then The opened shift is closed 
	And A new shift is opened
   