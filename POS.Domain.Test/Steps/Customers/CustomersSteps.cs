using System;
using System.Collections.Generic;
using System.Data.Entity;
using Castle.Components.DictionaryAdapter;
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
        private bool? _updateCustomer = false;
        private bool _deleteCustomer = false;
        private List<Customer> _customersLst;

        

        public CustomersSteps()
        {
            _context = new PosContext();
            _customersService = new CustomersService();
            _customersService.Initialize(_context);

            _customersLst =  new List<Customer>();
        }



        #region Add First Customer Should Success



        [Given(@"That I have the following Customer Data")]
        public void GivenThatIHaveTheFollowingCustomerData(Table table)
        {
           
            _customer =  table.CreateInstance<Customer>();

            
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

            Assert.AreNotEqual(null, findedCustomer);
            
           
            

        }

        #endregion

        #region Add Second Customer with the Same Name Should Fail


       
        [Given(@"That I have the following Customer Data with  the previous Name")]
        public void GivenThatIHaveTheFollowingCustomerDataWithThePreviousName(Table table)
        {
            
            _customer = table.CreateInstance<Customer>();

            
        }

        [When(@"I Add Customer with the Same data")]
        public void WhenIAddCustomerWithTheSameData()
        {
            _insertedId = _customersService.AddCustomer(_customer);

        }

        [Then(@"I should Get NULL")]
        public void ThenIShouldGetNull()
        {

            Customer findedCustomer = _customersService.FindCustomerById(_customer.Id);

            Assert.AreEqual(null, findedCustomer);




        }

        #endregion



        #region Update Existing Customer



        [Given(@"That I have the New Customer Data with  Existing CustomerId")]
        public void GivenThatIHaveNewCustomerDataExistingCustomerId(Table table)
        {
            
            _customer = table.CreateInstance<Customer>();

            
        }

        [When(@"I Update Customer with this New Data")]
        public void WhenIUpdateCustomerWithTheNewData()
        {
            _updateCustomer = _customersService.UpdateCustomer(_customer);

        }

        [Then(@"I should Get Success With The New Data")]
        public void ThenIShouldGetSuccessWithTheNewData()
        {

            Customer findedCustomer = _customersService.FindCustomerById(_customer.Id);

            Assert.AreNotEqual(null, findedCustomer);
            Assert.AreEqual(_customer.Id, findedCustomer.Id);
            Assert.AreEqual(_customer.Name, findedCustomer.Name);
            Assert.AreEqual(_customer.Address, findedCustomer.Address);
            Assert.AreEqual(_customer.Balance, findedCustomer.Balance);
            Assert.AreEqual(_customer.Phone, findedCustomer.Phone);
            Assert.AreEqual(_customer.Email, findedCustomer.Email);

        }

        #endregion

        #region Update Existing Customer with another existing user name

        
            
        [Given(@"That I have the New Customer Data with  Existing CustomerId andd existing user name")]
        public void GivenThatIHaveTheNewCustomerDataWithExistingCustomerIdAnddExistingUserName(Table table)
        {
            _customer = table.CreateInstance<Customer>();
        }

        [When(@"I Update Customer with this New Data and user name exist")]
        public void WhenIUpdateCustomerWithThisNewDataAndUserNameExist()
        {
            _updateCustomer = _customersService.UpdateCustomer(_customer);
        }

        [Then(@"I should Get Failed With The New Data")]
        public void ThenIShouldGetFailedWithTheNewData()
        {
            Assert.AreEqual(false, _updateCustomer);
        }

        #endregion

        #region Update Not Existing Customer


        [Given(@"That I have Not Existing Customer")]
        public void GivenThatIHaveNotExistingCustomer(Table table)
        {
            _customer = table.CreateInstance<Customer>();
        }

        [When(@"I Update Not Existing Customer with this New Data")]
        public void WhenIUpdateNotExistingCustomerWithThisNewData()
        {
            _updateCustomer = _customersService.UpdateCustomer(_customer);
        }

        [Then(@"I should Get Null Response")]
        public void ThenIShouldGetNullResponse()
        {
            Assert.AreEqual(null, _updateCustomer);
        }


        #endregion


        #region Delete Existing Customer



        [Given(@"That I have Customer with  Existing CustomerId")]
        public void GivenThatIHaveCustomerDataExistingCustomerId(Table table)
        {
           
            _customer = table.CreateInstance<Customer>();

            
        }

        [When(@"I Delete this Customer")]
        public void WhenIDeleteCustomer()
        {
            _deleteCustomer = _customersService.DeleteCustomer(_customer);

        }

        [Then(@"I should Get Success Delete")]
        public void ThenIShouldGetSuccessDelete()
        {

            Customer findedCustomer = _customersService.FindCustomerById(_customer.Id);

            Assert.AreEqual(null, findedCustomer);




        }

        #endregion

        #region Delete Not Exist Customer



        [Given(@"That I have Customer with  Not Existing CustomerId")]
        public void GivenThatIHaveCustomerWithNotExistingCustomerId(Table table)
        {
            _customer = table.CreateInstance<Customer>();
        }

        [When(@"I Delete this Not Customer")]
        public void WhenIDeleteThisNotCustomer()
        {
            _deleteCustomer = _customersService.DeleteCustomer(_customer);
        }

        [Then(@"I should Get Failed Delete")]
        public void ThenIShouldGetFailedDelete()
        {
            Assert.AreEqual(false, _deleteCustomer);
        }

        #endregion

        #region Get All Customers


        [Given(@"That I Want All  Customers")]
        public void GivenThatIWantAllCustomers()
        {
            
        }

        [When(@"I Call this Method Get All Customers")]
        public void WhenICallThisMethodGetAllCustomers()
        {
            _customersLst = _customersService.GetCustomers();
        }

        [Then(@"I should Get List Of All Customers")]
        public void ThenIShouldGetListOfAllCustomers()
        {
           Assert.AreNotEqual(0, _customersLst.Count);
        }

        #endregion


       


    }
}
