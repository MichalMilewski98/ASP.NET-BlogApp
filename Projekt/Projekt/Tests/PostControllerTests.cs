using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using NUnit.Framework;
using Projekt.Controllers;
using Projekt.Data;
using System.Web.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework.Constraints;
using ViewResult = Microsoft.AspNetCore.Mvc.ViewResult;

namespace Projekt.Tests
{
    public class PostControllerTests
    {
        [TestFixture]
        class RegisterTests
        {
            public DbContextOptions<ApplicationDbContext> dummyOptions { get; } = new DbContextOptionsBuilder<ApplicationDbContext>().Options;
            private readonly ApplicationDbContext _context;
            private UserManager<BlogUser> _userManager;
            

            [SetUp]
            public void Setup()
            {
                //var usersDbSetMock = dbContextMock.CreateDbSetMock(x => x.Post, initialEntities);

            }

            [Test]
            public async Task IsDetailsViewWorking()
            {


            }


        }
    }
}
