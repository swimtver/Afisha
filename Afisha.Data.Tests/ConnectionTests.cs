using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Afisha.Data;
using Should.Fluent;
using Afisha.Models;

namespace Afisha.Data.Tests
{
    [TestClass]
    public class ConnectionTests
    {

        [TestInitialize]
        public void TestInitialize()
        {
            DataConfiguration.Configure();
        }

        [TestCleanup]
        public void CleanUp()
        {
            IMovieRepository repo = new MovieRepository();
            foreach (var item in repo.GetList())
            {
                repo.Delete(item.Id);
            }
        }

        [TestMethod]
        public void CRUDTest()
        {
            IMovieRepository repo = new MovieRepository();

            var movie = new Movie
            {
                Title = "Fight Club"
            };
            var id=repo.Insert(movie).Id;

            repo.GetList().Should().Contain.One(m => m.Title == "Fight Club");

            Movie one = repo.GetItem(id);
            one.Should().Not.Be.Null();
            one.Title.Should().Equal("Fight Club");

            one.Title = "Forest Gump";

            repo.Update(one);
            repo.GetList().Should().Contain.One(m => m.Title == "Forest Gump");

            repo.Delete(id);
            repo.GetList().Should().Be.Empty();
        }
    }
}
