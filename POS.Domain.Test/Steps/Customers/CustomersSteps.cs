using System;
using System.Data.Entity;
using NUnit.Framework;
using POS.Domain.Entities;
using POS.Domain.Infrastructure;
using POS.Domain.Interfaces;
using POS.Domain.Services;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace POS.Domain.Test.Steps.Customers
{
    [Binding]
    public class CustomersSteps
    {
        private PosContext _context;
        private Customer _customer;
        private ICustomersService _customersService;
        private int _insertedId;

        [Given(@"That I have the following Customer Data")]
        public void GivenThatIHaveTheFollowingCustomerData(Table table)
        {
            _context  =  new PosContext();
            _customersService =new CustomersService();
            _customer =  table.CreateInstance<Customer>();

            _customersService.Initialize(_context);
        }
        
        [When(@"I Add Customer with the previous data")]
        public void WhenIAddCustomerWithThePreviousData()
        {
            _insertedId = _customersService.AddCustomer(_customer);
            
        }
        
        [Then(@"I should Get the iserted id")]
        public void ThenIShouldGetTheIsertedId()
        {

            Customer findedCustomer = _customersService.FindCustomerById(_customer.Id);

            Assert.AreNotEqual(findedCustomer.Id, _customer.Id);
            Assert.AreNotEqual(findedCustomer.Address, _customer.Address);
            Assert.AreNotEqual(findedCustomer.Name, _customer.Name);
            Assert.AreNotEqual(findedCustomer.Phone, _customer.Phone);
            Assert.AreNotEqual(findedCustomer.Balance, _customer.Balance);
            Assert.AreNotEqual(findedCustomer.Email, _customer.Email);
            

        }
    }
}
