using Hepsiorada.Api.Helpers;
using Hepsiorada.Domain.Base;
using Hepsiorada.Domain.Products;
using Hepsiorada.Domain.Products.ProductMeta;
using Hepsiorada.Domain.Users;
using Hepsiorada.Domain.Users.Lists;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hepsiorada.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    public class TestsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public TestsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("addtestdata")]
        public async Task<IActionResult> TestData()
        {
            var anyData = await _unitOfWork.ProductRepository.GetCategories();
            if (anyData.Count() == 0)
            {
                var ct = new List<Category>()
                {
                    new Category(){Name="Dizüstü Bilgisayar",CreatedAt=DateTime.Now,IsDeleted=false},
                    new Category(){Name="Tablet",CreatedAt=DateTime.Now,IsDeleted=false},
                    new Category(){Name="Masaüstü Bilgisayar",CreatedAt=DateTime.Now,IsDeleted=false},
                    new Category(){Name="Oyuncu Özel",CreatedAt=DateTime.Now,IsDeleted=false},
                    new Category(){Name="Veri Depolama",CreatedAt=DateTime.Now,IsDeleted=false}
                };
                foreach (var item in ct)
                {
                    var rnd = new Random();
                    item.Name = item.Name + rnd.Next(0, 500);
                    await _unitOfWork.ProductRepository.CreateCategory(item);
                }
                await _unitOfWork.SaveChangesAsync();
                var categories = await _unitOfWork.ProductRepository.GetCategories();
                foreach (var item in categories)
                {
                    var rnd = new Random();
                    for (int i = 0; i < 100; i++)
                    {
                        var p = new Product()
                        {
                            CategoryId = item.Id,
                            Name = $"Product {i}",
                            Brand = $"Brand {rnd.Next(0, 5)}",
                            CreatedAt = DateTime.Now,
                            Price = rnd.Next(80, 150),
                            DiscountedPrice = rnd.Next(50, 80),
                            IsDeleted = false,
                            Stock = rnd.Next(50, 100)
                        };
                        await _unitOfWork.ProductRepository.Add(p);
                    }
                }
                await _unitOfWork.SaveChangesAsync();
            }
            return Ok(ApiResponseHelper.SuccessResponse());
        }

        [HttpGet("lists")]
        public async Task<IActionResult> TestDataList()
        {
            var userLists = await _unitOfWork.UserListRepository.GetAll();
            var products = await _unitOfWork.ProductRepository.GetAll();
            var users = await _unitOfWork.UserRepository.GetAll();
            if (users.Count() == 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    var user = new User()
                    {
                        Email = $"email{i}@domain.com",
                        Password = $"password{i}",
                        IsDeleted = false,
                        Name = $"Name{i}",
                        Surname = $"Surname{i}"
                    };
                    await _unitOfWork.UserRepository.Add(user);
                }
                await _unitOfWork.SaveChangesAsync();
                users = await _unitOfWork.UserRepository.GetAll();
            }
            foreach (var item in users)
            {
                var rnd = new Random();
                for (int i = 0; i < 5; i++)
                {
                    var userList = new UserList();
                    userList.UserId = item.Id;
                    userList.Name = $"{item.Name}'s List{i}";
                    userList.IsDeleted = false;
                    userList.CreatedAt = DateTime.Now;
                    userList.Items = new List<ListItem>();
                    for (int j = 0; j < 50; j++)
                    {
                        var p = products.ElementAt(rnd.Next(products.Count() - 1));
                        userList.Items.Add(new ListItem() { ProductId = p.Id, Sort = 0 });
                    }
                    await _unitOfWork.UserListRepository.Add(userList);
                }
            }
            await _unitOfWork.SaveChangesAsync();


            return Ok(ApiResponseHelper.SuccessResponse());
        }

        [HttpGet("MostListedProducts")]
        public async Task<IActionResult> GetMostListedProducts()
        {
            var res = await _unitOfWork.UserListRepository.GetMostListedProducts(10);
            return Ok(ApiResponseHelper.SuccessResponse(res));
        }
        [HttpGet("UsersMostListedProducts")]
        public async Task<IActionResult> GetUsersMostListedProducts()
        {
            var res = await _unitOfWork.UserListRepository.GetUsersMostListedProducts(10);
            return Ok(ApiResponseHelper.SuccessResponse(res));
        }
    }
}
