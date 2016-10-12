using System.Data.Entity;
using System.Linq;
using Moq;
using NUnit.Framework;
using POS.Domain.Entities;
using POS.Domain.Infrastructure;
using POS.Domain.Models;
using POS.Domain.Services;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace POS.Domain.Test
{
    [Binding]
    public class LoginShiftsSteps
    {
        Mock<PosContext> context;
        Mock<DbSet<Shift>> shiftsSet;
        //Mock<SignInManager<ApplicationUser, string>> SignInManager;
        ShiftsService sut;
        Result result;
        int count = 0;
        [Given(@"That I have the following shifts")]
        public void GivenThatIHaveTheFollowingShifts(Table table)
        {
            count = table.Rows.Count;
            context = TestHelper.GetPosContext();
            shiftsSet = TestHelper.GetQueryableSet<Shift>(table.CreateSet<Shift>(), context);
            sut = new ShiftsService();
            sut.Initialize(context.Object);
            //SignInManager.Setup(s => s.PasswordSignIn("mostafa", "123456", false, false)).Returns(SignInStatus.Success);
        }
        [When(@"I login as mostafa with Id ""(.*)"" on Machine with id ""(.*)""")]
        public void WhenILoginAsMostafaWithIdOnMachineWithId(string p0, int p1)
        {
            var userId = p0;
            var machineId = p1;
            result = sut.GetUserCurrentShift(userId, machineId).Result;
            if(result.Id == 0)
            {          
                sut.OpenShift(userId, machineId);
            }
            else if (result.Message != "")
            {
                sut.CloseShift(result.Id);
            }
        }

        [Then(@"I should continue with the opened shift")]
        public void ThenIShouldContinueWithTheOpenedShift()
        {
            Assert.IsTrue(count == shiftsSet.Object.ToList().Count);
        }

        [Then(@"A new shift is opened")]
        public void ThenANewShiftIsOpened()
        {
            context.Verify(c => c.SaveChanges(), Times.Once);
            shiftsSet.Verify(d => d.Add(It.IsAny<Shift>()), Times.Once);
            Assert.IsTrue(count + 1 == shiftsSet.Object.ToList().Count);
        }

        [Then(@"The opened shift is closed")]
        public void ThenTheOpenedShiftIsClosed()
        {
            context.Verify(c => c.SaveChanges(), Times.Once);
            shiftsSet.Verify(d => d.Attach(It.IsAny<Shift>()), Times.Once);

        }
    }
}
