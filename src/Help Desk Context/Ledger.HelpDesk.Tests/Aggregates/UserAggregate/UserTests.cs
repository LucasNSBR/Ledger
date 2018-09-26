using Ledger.HelpDesk.Domain.Aggregates.RoleAggregate;
using Ledger.HelpDesk.Domain.Aggregates.Roles;
using Ledger.HelpDesk.Domain.Aggregates.UserAggregate;
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
            Role supportRole = new Role(RoleTypes.Support);

            user.AddRole(supportRole);

            Assert.AreEqual(1, user.Roles.Count);
        }

        [TestMethod]
        public void UserShouldHaveRole()
        {
            Role supportRole = new Role(RoleTypes.Support);
            user.AddRole(supportRole);

            Assert.IsTrue(user.IsInRole(supportRole));
        }

        [TestMethod]
        public void UserShouldBeRemovedFromRole()
        {

            Role supportRole = new Role(RoleTypes.Support);
            user.AddRole(supportRole);

            Assert.IsTrue(user.IsInRole(supportRole));
            Assert.AreEqual(1, user.Roles.Count);

            user.RemoveRole(supportRole);

            Assert.AreEqual(0, user.Roles.Count);
        }

        [TestMethod]
        public void UserShouldBeAddedOnMultipleRoles()
        {
            Role supportRole = new Role(RoleTypes.Support);
            Role adminRole = new Role(RoleTypes.Admin);
            user.AddRole(supportRole);
            user.AddRole(adminRole);

            Assert.IsTrue(user.IsInRole(supportRole));
            Assert.AreEqual(2, user.Roles.Count);

            user.RemoveRole(supportRole);

            Assert.AreEqual(1, user.Roles.Count);

            user.RemoveRole(adminRole);
        }

        [TestMethod]
        public void UserShouldFailToBeAddedOnMultipleRoles()
        {
            Role supportRole = new Role(RoleTypes.Support);
            
            user.AddRole(supportRole);
            user.AddRole(supportRole);

            Assert.AreEqual("Já possui a permissão", user.GetNotifications().First().Title);
        }

        [TestMethod]
        public void UserShouldFailToBeRemovedFromRole()
        {
            Role supportRole = new Role(RoleTypes.Support);

            user.RemoveRole(supportRole);
            
            Assert.AreEqual("Não possui a permissão", user.GetNotifications().First().Title);
        }
    }
}
