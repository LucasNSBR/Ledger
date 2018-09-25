using Ledger.HelpDesk.Domain.Aggregates.UserAggregate;
using Ledger.HelpDesk.Domain.Aggregates.UserAggregate.Roles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Ledger.HelpDesk.Tests.Aggregates.UserAggregate
{
    [TestClass]
    public class UserTests
    {
        User user;
        Guid userId = Guid.NewGuid();

        public UserTests()
        {
            user = new User(userId, "contoso@contoso.com");
        }

        [TestMethod]
        public void ShouldAddRoleToUser()
        {
            SupportRole supportRole = new SupportRole(userId);

            user.AddRole(supportRole);

            Assert.AreEqual(1, user.Roles.Count);
        }

        [TestMethod]
        public void UserShouldHaveRole()
        {
            SupportRole supportRole = new SupportRole(userId);
            user.AddRole(supportRole);

            Assert.IsTrue(user.IsInRole(supportRole.RoleName));
        }

        [TestMethod]
        public void UserShouldBeRemovedFromRole()
        {
            SupportRole supportRole = new SupportRole(userId);
            user.AddRole(supportRole);

            Assert.IsTrue(user.IsInRole(supportRole.RoleName));
            Assert.AreEqual(1, user.Roles.Count);

            user.RemoveRole(supportRole);

            Assert.AreEqual(0, user.Roles.Count);
        }

        [TestMethod]
        public void UserShouldBeAddedOnMultipleRoles()
        {
            SupportRole supportRole = new SupportRole(userId);
            AdminRole adminRole = new AdminRole(userId);
            user.AddRole(supportRole);
            user.AddRole(adminRole);

            Assert.IsTrue(user.IsInRole(supportRole.RoleName));
            Assert.AreEqual(2, user.Roles.Count);

            user.RemoveRole(supportRole);

            Assert.AreEqual(1, user.Roles.Count);

            user.RemoveRole(adminRole);
        }

        [TestMethod]
        public void UserShouldFailToBeAddedOnMultipleRoles()
        {
            SupportRole supportRole = new SupportRole(userId);
            SupportRole supportRole2 = new SupportRole(userId);

            user.AddRole(supportRole);
            user.AddRole(supportRole2);

            Assert.AreEqual("Já possui a permissão", user.GetNotifications().First().Title);
        }

        [TestMethod]
        public void UserShouldFailToBeRemovedFromRole()
        {
            SupportRole supportRole = new SupportRole(userId);
            
            user.RemoveRole(supportRole);
            
            Assert.AreEqual("Não possui a permissão", user.GetNotifications().First().Title);
        }
    }
}
