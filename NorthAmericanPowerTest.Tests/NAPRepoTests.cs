using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthAmericanPower.Data;
using NorthAmericanPower.Domain;

namespace NorthAmericanPowerTest.Tests
{
    [TestClass]
    public class NAPRepoTests
    {
        [TestMethod]
        public void Only3AccountsInitially()
        {

            IAccountRepository repo = new AccountRepository();
            Assert.IsTrue(((List<UserAccount>)repo.GetAll()).Count == 3);

        }

        [TestMethod]
        public void JohnDoeIsSecondAccount()
        {

            IAccountRepository repo = new AccountRepository();
            Assert.IsTrue(repo.GetUserAccount(2).Name.ToUpper() == "JOHN DOE");

        }

        [TestMethod]
        public void Account999DoesNotExist()
        {

            IAccountRepository repo = new AccountRepository();

            var account = repo.GetUserAccount(999);
            
            Assert.IsTrue((account == null), "Account 999 does not exist.");

        }

        [TestMethod]
        public void SuccessfulAddHas4Accounts()
        {

            IAccountRepository repo = new AccountRepository();
            
            UserAccount ua = new UserAccount() {
                 Name ="Pete Rod",
                 Email = "p@p.com",
                 Address  = "111 Main Street",
                 Postal = "99999"                  
            };

            repo.Add(ua);

            Assert.IsTrue(((List<UserAccount>)repo.GetAll()).Count == 4);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "New NULL Account should throw an error.")]
        public void AddNullAccountCausesError()
        {

            IAccountRepository repo = new AccountRepository();

            UserAccount ua = null;

            repo.Add(ua);

        }

        [TestMethod]
        public void SuccessfulUpdateOfPostalCodeOnFirstAccount()
        {

            IAccountRepository repo = new AccountRepository();

            var account = repo.GetUserAccount(1);

            account.Postal = "11111";
            
            Assert.IsTrue((repo.Update(account) && repo.GetUserAccount(1).Postal == "11111"));

        }

        [TestMethod]
        public void UpdateFailsForNonExistentAccount()
        {

            IAccountRepository repo = new AccountRepository();

            var account = repo.GetUserAccount(1);
            repo.Remove(1);

            Assert.IsTrue((account != null) && (!repo.Update(account)));

        }

         [TestMethod]
         [ExpectedException(typeof(ArgumentNullException), "New NULL Account should throw an error.")]
         public void UpdateNullAccountCausesError()
         {

             IAccountRepository repo = new AccountRepository();

             UserAccount account = null;

             bool result = repo.Update(account);
             
         }

        [TestMethod]
        public void SuccessfulRemoveHas2Accounts()
        {

            IAccountRepository repo = new AccountRepository();

            repo.Remove(2);

            Assert.IsTrue(((List<UserAccount>)repo.GetAll()).Count == 2);

        }

        [TestMethod]
        public void RemoveAccountThatDoesNotExistsDoesNotCauseError()
        {

            IAccountRepository repo = new AccountRepository();

            repo.Remove(999);

            Assert.IsTrue(((List<UserAccount>)repo.GetAll()).Count == 3);

        }

    }
}
