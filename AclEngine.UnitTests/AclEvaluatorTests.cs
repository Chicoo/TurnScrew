using System;
using System.Collections.Generic;
using Xunit;

namespace TurnScrew.Wiki.AclEngine.UnitTests
{
    public class AclEvaluatorTests
    {
        [Fact]
        public void AuthorizeAction_InexistentResource()
        {
            List<AclEntry> entries = new List<AclEntry>();
            entries.Add(new AclEntry("Res", "Action", "U.User", Value.Grant));
            entries.Add(new AclEntry("Res", "Action", "U.User2", Value.Deny));
            entries.Add(new AclEntry("Res", "Action2", "U.User", Value.Deny));
            entries.Add(new AclEntry("Res", "*", "U.User3", Value.Grant));
            entries.Add(new AclEntry("Res2", "Action", "U.User", Value.Deny));

            Assert.Equal(Authorization.Unknown, AclEvaluator.AuthorizeAction("Res3", "Action", "U.User", new string[0], entries.ToArray()));
        }

        [Fact]
        public void AuthorizeAction_InexistentAction()
        {
            List<AclEntry> entries = new List<AclEntry>();
            entries.Add(new AclEntry("Res", "Action", "U.User", Value.Grant));
            entries.Add(new AclEntry("Res", "Action", "U.User2", Value.Deny));
            entries.Add(new AclEntry("Res", "Action2", "U.User", Value.Deny));
            entries.Add(new AclEntry("Res", "*", "U.User3", Value.Grant));
            entries.Add(new AclEntry("Res2", "Action", "U.User", Value.Deny));

            Assert.Equal(Authorization.Unknown, AclEvaluator.AuthorizeAction("Res", "Action3", "U.User", new string[0], entries.ToArray()));
        }

        [Fact]
        public void AuthorizeAction_GrantUserExplicit()
        {
            List<AclEntry> entries = new List<AclEntry>();
            entries.Add(new AclEntry("Res", "Action", "U.User", Value.Grant));
            entries.Add(new AclEntry("Res", "Action", "U.User2", Value.Deny));
            entries.Add(new AclEntry("Res", "Action2", "U.User", Value.Deny));
            entries.Add(new AclEntry("Res", "*", "U.User3", Value.Grant));
            entries.Add(new AclEntry("Res2", "Action", "U.User", Value.Deny));

            Assert.Equal(Authorization.Granted, AclEvaluator.AuthorizeAction("Res", "Action", "U.User", new string[0], entries.ToArray()));
        }

        [Fact]
        public void AuthorizeAction_GrantUserFullControl()
        {
            List<AclEntry> entries = new List<AclEntry>();
            entries.Add(new AclEntry("Res", "*", "U.User", Value.Grant));
            entries.Add(new AclEntry("Res", "Action", "U.User2", Value.Deny));
            entries.Add(new AclEntry("Res", "Action2", "U.User", Value.Deny));
            entries.Add(new AclEntry("Res", "*", "U.User3", Value.Grant));
            entries.Add(new AclEntry("Res2", "Action", "U.User", Value.Deny));

            Assert.Equal(Authorization.Granted, AclEvaluator.AuthorizeAction("Res", "Action", "U.User", new string[0], entries.ToArray()));
        }

        [Fact]
        public void AuthorizeAction_DenyUserExplicit()
        {
            List<AclEntry> entries = new List<AclEntry>();
            entries.Add(new AclEntry("Res", "Action", "U.User", Value.Grant));
            entries.Add(new AclEntry("Res", "Action", "U.User2", Value.Deny));
            entries.Add(new AclEntry("Res", "Action2", "U.User", Value.Deny));
            entries.Add(new AclEntry("Res", "*", "U.User3", Value.Grant));
            entries.Add(new AclEntry("Res2", "Action", "U.User", Value.Deny));

            Assert.Equal(Authorization.Denied, AclEvaluator.AuthorizeAction("Res", "Action", "U.User2", new string[0], entries.ToArray()));
        }

        [Fact]
        public void AuthorizeAction_DenyUserFullControl()
        {
            List<AclEntry> entries = new List<AclEntry>();
            entries.Add(new AclEntry("Res", "Action", "U.User", Value.Grant));
            entries.Add(new AclEntry("Res", "*", "U.User2", Value.Deny));
            entries.Add(new AclEntry("Res", "Action2", "U.User", Value.Deny));
            entries.Add(new AclEntry("Res", "*", "U.User3", Value.Grant));
            entries.Add(new AclEntry("Res2", "Action", "U.User", Value.Deny));

            Assert.Equal(Authorization.Denied, AclEvaluator.AuthorizeAction("Res", "Action", "U.User2", new string[0], entries.ToArray()));
        }

        [Fact]
        public void AuthorizeAction_GrantGroupExplicit()
        {
            List<AclEntry> entries = new List<AclEntry>();
            entries.Add(new AclEntry("Res", "Action", "G.Group", Value.Grant));
            entries.Add(new AclEntry("Res", "Action", "U.User2", Value.Deny));
            entries.Add(new AclEntry("Res", "Action2", "G.Group", Value.Deny));
            entries.Add(new AclEntry("Res2", "Action", "G.Group", Value.Deny));
            entries.Add(new AclEntry("Res", "*", "U.User3", Value.Grant));

            Assert.Equal(Authorization.Granted, AclEvaluator.AuthorizeAction("Res", "Action", "U.User", new string[] { "G.Group" }, entries.ToArray()));
        }

        [Fact]
        public void AuthorizeAction_GrantGroupFullControl()
        {
            List<AclEntry> entries = new List<AclEntry>();
            entries.Add(new AclEntry("Res", "*", "G.Group", Value.Grant));
            entries.Add(new AclEntry("Res", "Action", "U.User2", Value.Deny));
            entries.Add(new AclEntry("Res", "Action2", "G.Group", Value.Deny));
            entries.Add(new AclEntry("Res2", "Action", "G.Group", Value.Deny));
            entries.Add(new AclEntry("Res", "*", "U.User3", Value.Grant));

            Assert.Equal(Authorization.Granted, AclEvaluator.AuthorizeAction("Res", "Action", "U.User", new string[] { "G.Group" }, entries.ToArray()));
        }

        [Fact]
        public void AuthorizeAction_DenyGroupExplicit()
        {
            List<AclEntry> entries = new List<AclEntry>();
            entries.Add(new AclEntry("Res", "Action", "G.Group", Value.Deny));
            entries.Add(new AclEntry("Res", "Action", "U.User2", Value.Grant));
            entries.Add(new AclEntry("Res", "Action2", "G.Group", Value.Grant));
            entries.Add(new AclEntry("Res2", "Action", "G.Group", Value.Grant));
            entries.Add(new AclEntry("Res", "*", "U.User3", Value.Deny));

            Assert.Equal(Authorization.Denied, AclEvaluator.AuthorizeAction("Res", "Action", "U.User", new string[] { "G.Group" }, entries.ToArray()));
        }

        [Fact]
        public void AuthorizeAction_DenyGroupFullControl()
        {
            List<AclEntry> entries = new List<AclEntry>();
            entries.Add(new AclEntry("Res", "*", "G.Group", Value.Deny));
            entries.Add(new AclEntry("Res", "Action", "U.User2", Value.Grant));
            entries.Add(new AclEntry("Res", "Action2", "G.Group", Value.Grant));
            entries.Add(new AclEntry("Res2", "Action", "G.Group", Value.Grant));
            entries.Add(new AclEntry("Res", "*", "U.User3", Value.Deny));

            Assert.Equal(Authorization.Denied, AclEvaluator.AuthorizeAction("Res", "Action", "U.User", new string[] { "G.Group" }, entries.ToArray()));
        }

        [Fact]
        public void AuthorizeAction_GrantGroupFullControl_DenyUserFullControl()
        {
            List<AclEntry> entries = new List<AclEntry>();
            entries.Add(new AclEntry("Res", "*", "G.Group", Value.Grant));
            entries.Add(new AclEntry("Res", "*", "U.User", Value.Deny));

            Assert.Equal(Authorization.Denied, AclEvaluator.AuthorizeAction("Res", "Action", "U.User", new string[] { "G.Group" }, entries.ToArray()));
        }

        [Fact]
        public void AuthorizeAction_GrantGroupExplicit_DenyUserFullControl()
        {
            List<AclEntry> entries = new List<AclEntry>();
            entries.Add(new AclEntry("Res", "Action", "G.Group", Value.Grant));
            entries.Add(new AclEntry("Res", "*", "U.User", Value.Deny));

            Assert.Equal(Authorization.Denied, AclEvaluator.AuthorizeAction("Res", "Action", "U.User", new string[] { "G.Group" }, entries.ToArray()));
        }

        [Fact]
        public void AuthorizeAction_GrantGroupFullControl_DenyUserExplicit()
        {
            List<AclEntry> entries = new List<AclEntry>();
            entries.Add(new AclEntry("Res", "*", "G.Group", Value.Grant));
            entries.Add(new AclEntry("Res", "Action", "U.User", Value.Deny));

            Assert.Equal(Authorization.Denied, AclEvaluator.AuthorizeAction("Res", "Action", "U.User", new string[] { "G.Group" }, entries.ToArray()));
        }

        [Fact]
        public void AuthorizeAction_DenyGroupFullControl_GrantUserFullControl()
        {
            List<AclEntry> entries = new List<AclEntry>();
            entries.Add(new AclEntry("Res", "*", "G.Group", Value.Deny));
            entries.Add(new AclEntry("Res", "*", "U.User", Value.Grant));

            Assert.Equal(Authorization.Granted, AclEvaluator.AuthorizeAction("Res", "Action", "U.User", new string[] { "G.Group" }, entries.ToArray()));
        }

        [Fact]
        public void AuthorizeAction_DenyGroupExplicit_GrantUserFullControl()
        {
            List<AclEntry> entries = new List<AclEntry>();
            entries.Add(new AclEntry("Res", "Action", "G.Group", Value.Deny));
            entries.Add(new AclEntry("Res", "*", "U.User", Value.Grant));

            Assert.Equal(Authorization.Granted, AclEvaluator.AuthorizeAction("Res", "Action", "U.User", new string[] { "G.Group" }, entries.ToArray()));
        }

        [Fact]
        public void AuthorizeAction_DenyGroupFullControl_GrantUserExplicit()
        {
            List<AclEntry> entries = new List<AclEntry>();
            entries.Add(new AclEntry("Res", "*", "G.Group", Value.Deny));
            entries.Add(new AclEntry("Res", "Action", "U.User", Value.Grant));

            Assert.Equal(Authorization.Granted, AclEvaluator.AuthorizeAction("Res", "Action", "U.User", new string[] { "G.Group" }, entries.ToArray()));
        }

        [Fact]
        public void AuthorizeAction_GrantGroupFullControl_GrantUserFullControl()
        {
            List<AclEntry> entries = new List<AclEntry>();
            entries.Add(new AclEntry("Res", "*", "G.Group", Value.Grant));
            entries.Add(new AclEntry("Res", "*", "U.User", Value.Grant));

            Assert.Equal(Authorization.Granted, AclEvaluator.AuthorizeAction("Res", "Action", "U.User", new string[] { "G.Group" }, entries.ToArray()));
        }

        [Fact]
        public void AuthorizeAction_GrantGroupExplicit_GrantUserExplicit()
        {
            List<AclEntry> entries = new List<AclEntry>();
            entries.Add(new AclEntry("Res", "Action", "G.Group", Value.Grant));
            entries.Add(new AclEntry("Res", "Action", "U.User", Value.Grant));

            Assert.Equal(Authorization.Granted, AclEvaluator.AuthorizeAction("Res", "Action", "U.User", new string[] { "G.Group" }, entries.ToArray()));
        }

        [Fact]
        public void AuthorizeAction_DenyGroupExplicit_GrantUserExplicit()
        {
            List<AclEntry> entries = new List<AclEntry>();
            entries.Add(new AclEntry("Res", "Action", "G.Group", Value.Deny));
            entries.Add(new AclEntry("Res", "Action", "U.User", Value.Grant));

            Assert.Equal(Authorization.Granted, AclEvaluator.AuthorizeAction("Res", "Action", "U.User", new string[] { "G.Group" }, entries.ToArray()));
        }

        [Fact]
        public void AuthorizeAction_GrantGroupFullControl_GrantUserExplicit()
        {
            List<AclEntry> entries = new List<AclEntry>();
            entries.Add(new AclEntry("Res", "*", "G.Group", Value.Grant));
            entries.Add(new AclEntry("Res", "Action", "U.User", Value.Grant));

            Assert.Equal(Authorization.Granted, AclEvaluator.AuthorizeAction("Res", "Action", "U.User", new string[] { "G.Group" }, entries.ToArray()));
        }

        [Fact]
        public void AuthorizeAction_GrantGroupExplicit_GrantUserFullControl()
        {
            List<AclEntry> entries = new List<AclEntry>();
            entries.Add(new AclEntry("Res", "Action", "G.Group", Value.Grant));
            entries.Add(new AclEntry("Res", "*", "U.User", Value.Grant));

            Assert.Equal(Authorization.Granted, AclEvaluator.AuthorizeAction("Res", "Action", "U.User", new string[] { "G.Group" }, entries.ToArray()));
        }

        [Fact]
        public void AuthorizeAction_GrantGroupExplicit_DenyUserExpicit()
        {
            List<AclEntry> entries = new List<AclEntry>();
            entries.Add(new AclEntry("Res", "Action", "G.Group", Value.Grant));
            entries.Add(new AclEntry("Res", "Action", "U.User", Value.Deny));

            Assert.Equal(Authorization.Denied, AclEvaluator.AuthorizeAction("Res", "Action", "U.User", new string[] { "G.Group" }, entries.ToArray()));
        }

        [Fact]
        public void AuthorizeAction_DenyGroupExplicit_DenyUserExplicit()
        {
            List<AclEntry> entries = new List<AclEntry>();
            entries.Add(new AclEntry("Res", "Action", "G.Group", Value.Deny));
            entries.Add(new AclEntry("Res", "Action", "U.User", Value.Deny));

            Assert.Equal(Authorization.Denied, AclEvaluator.AuthorizeAction("Res", "Action", "U.User", new string[] { "G.Group" }, entries.ToArray()));
        }

        [Fact]
        public void AuthorizeAction_DenyGroupFullControl_DenyUserFullControl()
        {
            List<AclEntry> entries = new List<AclEntry>();
            entries.Add(new AclEntry("Res", "*", "G.Group", Value.Deny));
            entries.Add(new AclEntry("Res", "*", "U.User", Value.Deny));

            Assert.Equal(Authorization.Denied, AclEvaluator.AuthorizeAction("Res", "Action", "U.User", new string[] { "G.Group" }, entries.ToArray()));
        }

        [Fact]
        public void AuthorizeAction_DenyGroupFullControl_DenyUserExplicit()
        {
            List<AclEntry> entries = new List<AclEntry>();
            entries.Add(new AclEntry("Res", "*", "G.Group", Value.Deny));
            entries.Add(new AclEntry("Res", "Action", "U.User", Value.Deny));

            Assert.Equal(Authorization.Denied, AclEvaluator.AuthorizeAction("Res", "Action", "U.User", new string[] { "G.Group" }, entries.ToArray()));
        }

        [Fact]
        public void AuthorizeAction_DenyGroupExplicit_DenyUserFullControl()
        {
            List<AclEntry> entries = new List<AclEntry>();
            entries.Add(new AclEntry("Res", "Action", "G.Group", Value.Deny));
            entries.Add(new AclEntry("Res", "*", "U.User", Value.Deny));

            Assert.Equal(Authorization.Denied, AclEvaluator.AuthorizeAction("Res", "Action", "U.User", new string[] { "G.Group" }, entries.ToArray()));
        }

        [Fact]
        public void AuthorizeAction_GrantOneGroupExplicit_GrantOtherGroupExplicit()
        {
            List<AclEntry> entries = new List<AclEntry>();
            entries.Add(new AclEntry("Res", "Action", "G.Group1", Value.Grant));
            entries.Add(new AclEntry("Res", "Action", "G.Group2", Value.Grant));

            Assert.Equal(Authorization.Granted, AclEvaluator.AuthorizeAction("Res", "Action", "U.User", new string[] { "G.Group1", "G.Group2" }, entries.ToArray()));
        }

        [Fact]
        public void AuthorizeAction_DenyOneGroupExplicit_GrantOtherGroupExplicit()
        {
            List<AclEntry> entries = new List<AclEntry>();
            entries.Add(new AclEntry("Res", "Action", "G.Group1", Value.Deny));
            entries.Add(new AclEntry("Res", "Action", "G.Group2", Value.Grant));

            Assert.Equal(Authorization.Denied, AclEvaluator.AuthorizeAction("Res", "Action", "U.User", new string[] { "G.Group1", "G.Group2" }, entries.ToArray()));
        }

        [Fact]
        public void AuthorizeAction_DenyOneGroupExplicit_DenyOtherGroupExplicit()
        {
            List<AclEntry> entries = new List<AclEntry>();
            entries.Add(new AclEntry("Res", "Action", "G.Group1", Value.Deny));
            entries.Add(new AclEntry("Res", "Action", "G.Group2", Value.Deny));

            Assert.Equal(Authorization.Denied, AclEvaluator.AuthorizeAction("Res", "Action", "U.User", new string[] { "G.Group1", "G.Group2" }, entries.ToArray()));
        }

        [Fact]
        public void AuthorizeAction_GrantOneGroupFullControl_GrantOtherGroupFullControl()
        {
            List<AclEntry> entries = new List<AclEntry>();
            entries.Add(new AclEntry("Res", "*", "G.Group1", Value.Grant));
            entries.Add(new AclEntry("Res", "*", "G.Group2", Value.Grant));

            Assert.Equal(Authorization.Granted, AclEvaluator.AuthorizeAction("Res", "Action", "U.User", new string[] { "G.Group1", "G.Group2" }, entries.ToArray()));
        }

        [Fact]
        public void AuthorizeAction_DenyOneGroupFullControl_DenyOtherGroupFullControl()
        {
            List<AclEntry> entries = new List<AclEntry>();
            entries.Add(new AclEntry("Res", "*", "G.Group1", Value.Deny));
            entries.Add(new AclEntry("Res", "*", "G.Group2", Value.Deny));

            Assert.Equal(Authorization.Denied, AclEvaluator.AuthorizeAction("Res", "Action", "U.User", new string[] { "G.Group1", "G.Group2" }, entries.ToArray()));
        }

        [Fact]
        public void AuthorizeAction_DenyOneGroupFullControl_GrantOtherGroupFullControl()
        {
            List<AclEntry> entries = new List<AclEntry>();
            entries.Add(new AclEntry("Res", "*", "G.Group1", Value.Grant));
            entries.Add(new AclEntry("Res", "*", "G.Group2", Value.Deny));

            Assert.Equal(Authorization.Denied, AclEvaluator.AuthorizeAction("Res", "Action", "U.User", new string[] { "G.Group1", "G.Group2" }, entries.ToArray()));
        }

        [Fact]
        public void AuthorizeAction_DenyOneGroupFullControl_GrantOtherGroupExplicit()
        {
            List<AclEntry> entries = new List<AclEntry>();
            entries.Add(new AclEntry("Res", "Action", "G.Group1", Value.Grant));
            entries.Add(new AclEntry("Res", "*", "G.Group2", Value.Deny));

            Assert.Equal(Authorization.Granted, AclEvaluator.AuthorizeAction("Res", "Action", "U.User", new string[] { "G.Group1", "G.Group2" }, entries.ToArray()));
        }

        [Fact]
        public void AuthorizeAction_DenyOneGroupExplicit_GrantOtherGroupFullControl()
        {
            List<AclEntry> entries = new List<AclEntry>();
            entries.Add(new AclEntry("Res", "*", "G.Group1", Value.Grant));
            entries.Add(new AclEntry("Res", "Action", "G.Group2", Value.Deny));

            Assert.Equal(Authorization.Denied, AclEvaluator.AuthorizeAction("Res", "Action", "U.User", new string[] { "G.Group1", "G.Group2" }, entries.ToArray()));
        }

        [Fact]
        public void AuthorizeAction_GrantOneGroupExplicit_GrantOtherGroupFullControl()
        {
            List<AclEntry> entries = new List<AclEntry>();
            entries.Add(new AclEntry("Res", "*", "G.Group1", Value.Grant));
            entries.Add(new AclEntry("Res", "Action", "G.Group2", Value.Grant));

            Assert.Equal(Authorization.Granted, AclEvaluator.AuthorizeAction("Res", "Action", "U.User", new string[] { "G.Group1", "G.Group2" }, entries.ToArray()));
        }

        [Fact]
        public void AuthorizeAction_DenyOneGroupExplicit_DenyOtherGroupFullControl()
        {
            List<AclEntry> entries = new List<AclEntry>();
            entries.Add(new AclEntry("Res", "*", "G.Group1", Value.Deny));
            entries.Add(new AclEntry("Res", "Action", "G.Group2", Value.Deny));

            Assert.Equal(Authorization.Denied, AclEvaluator.AuthorizeAction("Res", "Action", "U.User", new string[] { "G.Group1", "G.Group2" }, entries.ToArray()));
        }

        [Fact]
        public void AuthorizeAction_DenyUserExplicit_GrantUserFullControl()
        {
            List<AclEntry> entries = new List<AclEntry>();
            entries.Add(new AclEntry("Res", "*", "U.User", Value.Grant));
            entries.Add(new AclEntry("Res", "Action", "U.User", Value.Deny));

            Assert.Equal(Authorization.Denied, AclEvaluator.AuthorizeAction("Res", "Action", "U.User", new string[0], entries.ToArray()));
        }

        [Fact]
        public void AuthorizeAction_DenyGroupExplicit_GrantGroupFullControl()
        {
            List<AclEntry> entries = new List<AclEntry>();
            entries.Add(new AclEntry("Res", "*", "G.Group", Value.Grant));
            entries.Add(new AclEntry("Res", "Action", "G.Group", Value.Deny));

            Assert.Equal(Authorization.Denied, AclEvaluator.AuthorizeAction("Res", "Action", "U.User", new string[] { "G.Group" }, entries.ToArray()));
        }

        [Fact]
        public void AuthorizeAction_GrantUserExplicit_DenyUserFullControl()
        {
            List<AclEntry> entries = new List<AclEntry>();
            entries.Add(new AclEntry("Res", "*", "U.User", Value.Deny));
            entries.Add(new AclEntry("Res", "Action", "U.User", Value.Grant));

            Assert.Equal(Authorization.Granted, AclEvaluator.AuthorizeAction("Res", "Action", "U.User", new string[0], entries.ToArray()));
        }

        [Fact]
        public void AuthorizeAction_GrantGroupExplicit_DenyGroupFullControl()
        {
            List<AclEntry> entries = new List<AclEntry>();
            entries.Add(new AclEntry("Res", "*", "G.Group", Value.Deny));
            entries.Add(new AclEntry("Res", "Action", "G.Group", Value.Grant));

            Assert.Equal(Authorization.Granted, AclEvaluator.AuthorizeAction("Res", "Action", "U.User", new string[] { "G.Group" }, entries.ToArray()));
        }

        [Fact]
        public void Authorize_Resource_Null()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => AclEvaluator.AuthorizeAction(null, "Action", "U.User", new string[0], new AclEntry[0]));
            Assert.Equal("Value cannot be null.\r\nParameter name: resource", ex.Message);
        }

        [Fact]
        public void Authorize_Resource_Empty()
        {
            var ex = Assert.Throws<ArgumentException>(() => AclEvaluator.AuthorizeAction("", "Action", "U.User", new string[0], new AclEntry[0]));
            Assert.Equal("Resource cannot be empty.\r\nParameter name: resource", ex.Message);
        }

        [Fact]
        public void Authorize_Action_Null()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => AclEvaluator.AuthorizeAction("Res", null, "U.User", new string[0], new AclEntry[0]));
            Assert.Equal("Value cannot be null.\r\nParameter name: action", ex.Message);
        }

        [Fact]
        public void Authorize_Action_Empty()
        {
            var ex = Assert.Throws<ArgumentException>(() => AclEvaluator.AuthorizeAction("Res", "", "U.User", new string[0], new AclEntry[0]));
            Assert.Equal("Action cannot be empty.\r\nParameter name: action", ex.Message);
        }

        [Fact]
        public void Authorize_User_Null()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => AclEvaluator.AuthorizeAction("Res", "Action", null, new string[0], new AclEntry[0]));
            Assert.Equal("Value cannot be null.\r\nParameter name: user", ex.Message);
        }

        [Fact]
        public void Authorize_User_Empty()
        {
            var ex = Assert.Throws<ArgumentException>(() => AclEvaluator.AuthorizeAction("Res", "Action", "", new string[0], new AclEntry[0]));
            Assert.Equal("User cannot be empty.\r\nParameter name: user", ex.Message);
        }

        [Fact]
        public void Authorize_Action_Null_Groups()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => AclEvaluator.AuthorizeAction("Res", "Action", "U.User", null, new AclEntry[0]));
            Assert.Equal("Value cannot be null.\r\nParameter name: groups", ex.Message);
        }

        [Fact]
        public void Authorize_Action_Null_Entries()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => AclEvaluator.AuthorizeAction("Res", "Action", "U.User", new string[0], null));
            Assert.Equal("Value cannot be null.\r\nParameter name: entries", ex.Message);
        }

        [Fact]
        public void AuthorizeAction_EmptyEntries()
        {
            Assert.Equal(Authorization.Unknown, AclEvaluator.AuthorizeAction("Res", "Action", "U.User", new string[0], new AclEntry[0]));
        }
    }
}
