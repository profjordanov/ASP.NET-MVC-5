using System;
using System.Linq;
using MessageBoard.App_Start;
using MessageBoard.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MessageBoard.Tests.Fakes;
using System.Web.Mvc;
using System.Collections.Generic;
using MessageBoard.Data;
using MessageBoard.Services;

namespace MessageBoard.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        private FakeMessageBoardRepository _repo;
        private HomeController _ctrl;

        [TestInitialize]
        public void Init()
        {
            _repo = new FakeMessageBoardRepository();
            _ctrl = new HomeController(new MockMailService(), _repo);

        }

        [TestMethod]
        public void IndexCanRender()
        {
            var result = _ctrl.Index();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void IndexHasData()
        {
            var result = _ctrl.Index() as ViewResult;
            var topics = result.Model as IEnumerable<Topic>;

            Assert.IsNotNull(result.Model);
            Assert.IsNotNull(topics);
            Assert.IsTrue(topics.Count() > 0);

        }
    }
}
