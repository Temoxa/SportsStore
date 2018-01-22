using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using SportsStore.Controllers;
using SportsStore.Models;

namespace SportsStore.Tests
{
    public class ProductControllerTest
    {
        [Theory]
        [InlineData("Category1", 1)]
        public void List(string category, int page = 1)
        {
            ProductController controller = new ProductController(new FakeProductRepository());

            ViewResult resultView = controller.List(category, page);

            //Assert.Equal("List", resultView?.ViewName); //Проверка что названия представления совпадает
            Assert.NotNull(resultView); //Проверка что не пустоту вернуло
        }
    }
}
