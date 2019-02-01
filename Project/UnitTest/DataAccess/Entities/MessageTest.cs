﻿using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using DataAccess.Entities;
using DA = DataAccess.Context;

namespace UnitTest.DataAccess.Entities
{
    [TestClass]
    public class MessageTest
    {
        // FIELDS
        static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB; Integrated Security=True; Initial Catalog=MessageTestDB";
        static DA.AppContext dbContext;
        static Resources.Classes.DbFiller dbFiller;
        // PROPERTIES
        public TestContext TestContext { get; set; }
        // INITIALIZERS
        [ClassInitialize]
        public static void Constructor(TestContext context)
        {
            dbFiller = new Resources.Classes.DbFiller();
            dbContext = new DA.AppContext(connectionString);
        }
        [ClassCleanup]
        public static void Finalizer()
        {
            dbContext.Dispose();
            System.Data.Entity.Database.Delete(connectionString);
        }
        [TestInitialize]
        public void Filler()
        {
            dbFiller.Fill(dbContext);
        }
        [TestCleanup]
        public void Cleaner()
        {
            dbFiller.Purge(dbContext);
        }

        [TestMethod]
        public void GetAllColumnsOfMessage()
        {
            // Arrange
            Message expectedMessage = new Message
            {
                Date = DateTime.Now,
                Text = "Some test text",
                User = dbContext.Users.First(),
                Subject = dbContext.Subjects.First()
            };

            // Act
            dbContext.Messages.Add(expectedMessage);
            dbContext.SaveChanges();
            Message actualMessage = dbContext.Messages.Find(expectedMessage.Id);

            // Assert
            Assert.AreEqual(expectedMessage.Date, actualMessage.Date);
            Assert.AreEqual(expectedMessage.Text, actualMessage.Text);
            Assert.AreEqual(expectedMessage.User, actualMessage.User);
            Assert.AreEqual(expectedMessage.Subject, actualMessage.Subject);
        }
    }
}