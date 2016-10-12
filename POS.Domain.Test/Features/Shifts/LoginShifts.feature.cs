﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:2.1.0.0
//      SpecFlow Generator Version:2.0.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace POS.Domain.Test.Features.Shifts
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.1.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("LoginShifts")]
    public partial class LoginShiftsFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "LoginShifts.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "LoginShifts", "\tI want to check login process for shifts", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.TestFixtureTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Login to already opened shift for the same user on the same machine")]
        public virtual void LoginToAlreadyOpenedShiftForTheSameUserOnTheSameMachine()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Login to already opened shift for the same user on the same machine", ((string[])(null)));
#line 4
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "Id",
                        "UserId",
                        "UserName",
                        "MachineId",
                        "MachineName",
                        "StartDate",
                        "EndDate",
                        "Balance",
                        "IsClosed"});
            table1.AddRow(new string[] {
                        "1",
                        "asdasd",
                        "mostafa",
                        "1",
                        "pc1",
                        "01-01-2016 12:00:00",
                        "NULL",
                        "500",
                        "False"});
#line 5
 testRunner.Given("That I have the following shifts", ((string)(null)), table1, "Given ");
#line 8
    testRunner.When("I login as mostafa with Id \"asdasd\" on Machine with id \"1\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 9
    testRunner.Then("I should continue with the opened shift", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Login with no opened shift for the same user on the same machine")]
        public virtual void LoginWithNoOpenedShiftForTheSameUserOnTheSameMachine()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Login with no opened shift for the same user on the same machine", ((string[])(null)));
#line 11
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "Id",
                        "UserId",
                        "UserName",
                        "MachineId",
                        "MachineName",
                        "StartDate",
                        "EndDate",
                        "Balance",
                        "IsClosed"});
            table2.AddRow(new string[] {
                        "1",
                        "asdasd",
                        "mostafa",
                        "1",
                        "pc1",
                        "01-01-2016 12:00:00",
                        "01-01-2016 16:00:00",
                        "500",
                        "True"});
#line 12
    testRunner.Given("That I have the following shifts", ((string)(null)), table2, "Given ");
#line 15
    testRunner.When("I login as mostafa with Id \"asdasd\" on Machine with id \"1\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 16
    testRunner.Then("A new shift is opened", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Login to already opened shift for another user on the same machine")]
        public virtual void LoginToAlreadyOpenedShiftForAnotherUserOnTheSameMachine()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Login to already opened shift for another user on the same machine", ((string[])(null)));
#line 18
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                        "Id",
                        "UserId",
                        "UserName",
                        "MachineId",
                        "MachineName",
                        "StartDate",
                        "EndDate",
                        "Balance",
                        "IsClosed"});
            table3.AddRow(new string[] {
                        "1",
                        "wwwwww",
                        "Ahmed",
                        "1",
                        "pc1",
                        "01-01-2016 12:00:00",
                        "NULL",
                        "500",
                        "False"});
#line 19
    testRunner.Given("That I have the following shifts", ((string)(null)), table3, "Given ");
#line 22
    testRunner.When("I login as mostafa with Id \"asdasd\" on Machine with id \"1\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 23
    testRunner.Then("The opened shift is closed", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 24
 testRunner.And("A new shift is opened", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Login to an opened shift for the same user on another machine")]
        public virtual void LoginToAnOpenedShiftForTheSameUserOnAnotherMachine()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Login to an opened shift for the same user on another machine", ((string[])(null)));
#line 26
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                        "Id",
                        "UserId",
                        "UserName",
                        "MachineId",
                        "MachineName",
                        "StartDate",
                        "EndDate",
                        "Balance",
                        "IsClosed"});
            table4.AddRow(new string[] {
                        "1",
                        "asdasd",
                        "mostafa",
                        "1",
                        "pc2",
                        "01-01-2016 12:00:00",
                        "NULL",
                        "500",
                        "False"});
#line 27
    testRunner.Given("That I have the following shifts", ((string)(null)), table4, "Given ");
#line 30
    testRunner.When("I login as mostafa with Id \"asdasd\" on Machine with id \"1\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 31
    testRunner.Then("The opened shift is closed", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 32
 testRunner.And("A new shift is opened", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
