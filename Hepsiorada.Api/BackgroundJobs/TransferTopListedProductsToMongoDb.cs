using Hangfire;
using Hepsiorada.Application.Products;
using Hepsiorada.Domain.Base;
using Hepsiorada.Domain.Products.MongoEntites;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hepsiorada.Api.BackgroundJobs
{
    public class TransferDataToMongo : ITransferDataToMongo
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransferDataToMongo(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void TransferTopListedProducts()
        {
            List<TopListedProduct> productDtos = new List<TopListedProduct>();
            var mostListedProducts = _unitOfWork.UserListRepository.GetMostListedProducts(10).Result;
            _unitOfWork.ProductMongoRepository.RemoveTopListedProducts();
            foreach (var item in mostListedProducts)
            {
                var product = _unitOfWork.ProductRepository.GetById(item.ProductId).Result;
                productDtos.Add(new TopListedProduct() { Product = product.Adapt<ProductMongo>(), Count = item.Count });
            }
            _unitOfWork.ProductMongoRepository.Add(productDtos);
        }

        public void TransferUserTopListedProducts()
        {
            List<UserTopListedProduct> productDtos = new List<UserTopListedProduct>();
            var mostListedProducts = _unitOfWork.UserListRepository.GetUsersMostListedProducts(10).Result;
            _unitOfWork.ProductMongoRepository.RemoveUserTopListedProducts();
            foreach (var item in mostListedProducts)
            {
                var product = _unitOfWork.ProductRepository.GetById(item.ProductId).Result;
                var user = _unitOfWork.UserRepository.GetById(item.UserId).Result;
                productDtos.Add(new UserTopListedProduct() { User = user.Adapt<UserMongo>(), Product = product.Adapt<ProductMongo>(), Count = item.Count });
            }
            _unitOfWork.ProductMongoRepository.Add(productDtos);
        }
    }
}
