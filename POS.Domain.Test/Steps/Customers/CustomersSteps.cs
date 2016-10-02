using System;
using TechTalk.SpecFlow;

namespace POS.Domain.Test.Steps.Customers
{
    [Binding]
    public class CustomersSteps
    {
        [Given(@"That I have the following Customer Data")]
        public void GivenThatIHaveTheFollowingCustomerData(Table table)
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I Add Customer with the previous data")]
        public void WhenIAddCustomerWithThePreviousData()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"I should Get the iserted id")]
        public void ThenIShouldGetTheIsertedId()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
