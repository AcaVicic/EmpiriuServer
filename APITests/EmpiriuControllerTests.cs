using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using WebAPI.Controllers;

namespace APITests
{
    [TestClass]
    public class EmpiriuControllerTests
    {
        [TestMethod]
        public void GetJournalTest()
        {
            var data = new List<DailyJournal>
            {
                new DailyJournal()
                {
                Id = 1,
                Text = "Neki tekst",
                User = new User() { Id = 1 },
                Date = new System.DateTime(2022, 1, 1)
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<DailyJournal>>();
            mockSet.As<IQueryable<DailyJournal>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<DailyJournal>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<DailyJournal>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<DailyJournal>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<EmpiriuContext>();
            mockContext.Setup(m => m.DailyJournals).Returns(mockSet.Object);

            var controller = new EmpiriuController(mockContext.Object);
            var dailyJournal = controller.GetJournal(1, "1.1.2022.");

            Assert.AreEqual(1, dailyJournal.Id);
            Assert.AreEqual("Neki tekst", dailyJournal.Text);
            Assert.AreEqual(1, dailyJournal.User!.Id);
            Assert.AreEqual(1, dailyJournal.Date.Day);
            Assert.AreEqual(1, dailyJournal.Date.Month);
            Assert.AreEqual(2022, dailyJournal.Date.Year);
        }

        [TestMethod]
        public void GetQuoteTest()
        {
            var data = new List<Quote>
            {
                new Quote()
                {
                Id = 1,
                Text = "Neki tekst",
                AboutPhilosopher = "Neki opis",
                Book = "Neka knjiga",
                Explanation = "Neko objasnjenje",
                Image = "Neka slika",
                Philosopher = "Neki filozof"
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Quote>>();
            mockSet.As<IQueryable<Quote>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Quote>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Quote>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Quote>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<EmpiriuContext>();
            mockContext.Setup(m => m.Quotes).Returns(mockSet.Object);

            var controller = new EmpiriuController(mockContext.Object);
            var quote = controller.GetQuote(1);

            Assert.AreEqual(1, quote.Id);
            Assert.AreEqual("Neki tekst", quote.Text);
            Assert.AreEqual("Neki opis", quote.AboutPhilosopher);
            Assert.AreEqual("Neka knjiga", quote.Book);
            Assert.AreEqual("Neko objasnjenje", quote.Explanation);
            Assert.AreEqual("Neka slika", quote.Image);
            Assert.AreEqual("Neki filozof", quote.Philosopher);
        }

        [TestMethod]
        public void GetUserTest()
        {
            var data = new List<User>
            {
                new User()
                {
                Id = 1,
                Email = "Neki email",
                Username = "Neki username",
                Password = "Neki password"
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<EmpiriuContext>();
            mockContext.Setup(m => m.Users).Returns(mockSet.Object);

            var controller = new EmpiriuController(mockContext.Object);
            var user = controller.GetUser("Neki email");

            Assert.AreEqual(1, user.Id);
            Assert.AreEqual("Neki email", user.Email);
            Assert.AreEqual("Neki username", user.Username);
            Assert.AreEqual("Neki password", user.Password);
        }

        [TestMethod]
        public void PostDailyJournalTest()
        {
            var data = new List<User>
            {
                new User()
                {
                Id = 1,
                Email = "Neki email",
                Username = "Neki username",
                Password = "Neki password"
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<DailyJournal>>();
            var mockSetUsers = new Mock<DbSet<User>>();
            mockSetUsers.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSetUsers.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSetUsers.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSetUsers.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<EmpiriuContext>();
            mockContext.Setup(m => m.DailyJournals).Returns(mockSet.Object);
            mockContext.Setup(m => m.Users).Returns(mockSetUsers.Object);

            var controller = new EmpiriuController(mockContext.Object);
            controller.PostDailyJournal(new DailyJournal()
            {
                Id = 1,
                Text = "Neki tekst",
                User = new User()
                {
                    Id = 1,
                    Email = "Neki email",
                    Username = "Neki username",
                    Password = "Neki password"
                },
                Date = new System.DateTime(2022, 1, 1)
            });

            mockSet.Verify(m => m.Add(It.IsAny<DailyJournal>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void PutDailyJournalTest()
        {
            var data = new List<User>
            {
                new User()
                {
                Id = 1,
                Email = "Neki email",
                Username = "Neki username",
                Password = "Neki password"
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<DailyJournal>>();
            var mockSetUsers = new Mock<DbSet<User>>();
            mockSetUsers.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSetUsers.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSetUsers.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSetUsers.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<EmpiriuContext>();
            mockContext.Setup(m => m.DailyJournals).Returns(mockSet.Object);
            mockContext.Setup(m => m.Users).Returns(mockSetUsers.Object);

            var controller = new EmpiriuController(mockContext.Object);
            controller.PutDailyJournal(new DailyJournal()
            {
                Id = 1,
                Text = "Neki tekst",
                User = new User()
                {
                    Id = 1,
                    Email = "Neki email",
                    Username = "Neki username",
                    Password = "Neki password"
                },
                Date = new System.DateTime(2022, 1, 1)
            });

            mockSet.Verify(m => m.Update(It.IsAny<DailyJournal>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }


        [TestMethod]
        public void DeleteDailyJournalTest()
        {
            var data = new List<DailyJournal>
            {
                new DailyJournal()
                {
                Id = 1,
                Text = "Neki tekst",
                User = new User() { Id = 1 },
                Date = new System.DateTime(2022, 1, 1)
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<DailyJournal>>();
            mockSet.As<IQueryable<DailyJournal>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<DailyJournal>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<DailyJournal>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<DailyJournal>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<EmpiriuContext>();
            mockContext.Setup(m => m.DailyJournals).Returns(mockSet.Object);

            var controller = new EmpiriuController(mockContext.Object);
            controller.DeleteDailyJournal(1);

            mockSet.Verify(m => m.Remove(It.IsAny<DailyJournal>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }
    }
}