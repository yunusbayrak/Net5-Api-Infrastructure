using Hepsiorada.Application.Products.Commands;
using Hepsiorada.Application.Products.Queries;
using Hepsiorada.Domain.Base;
using Hepsiorada.Domain.Products;
using Hepsiorada.Domain.Products.MongoEntites;
using Hepsiorada.Domain.Products.ProductMeta;
using Mapster;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hepsiorada.Application.Products
{
    public class ProductHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductDto>>,
        IRequestHandler<GetProductQuery, ProductDto>, IRequestHandler<CreateProductCommand, Unit>, IRequestHandler<UpdateProductCommand, Unit>
        , IRequestHandler<DeleteProductCommand, Unit>, IRequestHandler<CreateCategoryCommand, Unit>
        , IRequestHandler<GetCategoriesQuery, IEnumerable<CategoryDto>>
        , IRequestHandler<GetTopListedProductsQuery, IEnumerable<TopListedProduct>>
        , IRequestHandler<GetUserTopListedProductsQuery, IEnumerable<UserTopListedProduct>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDistributedCache _distributedCache;

        public ProductHandler(IUnitOfWork unitOfWork, IDistributedCache distributedCache)
        {
            _unitOfWork = unitOfWork;
            _distributedCache = distributedCache;
        }
        public async Task<IEnumerable<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.ProductRepository.GetAll();
            return products.Adapt<IEnumerable<ProductDto>>();
        }

        public async Task<ProductDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.ProductRepository.GetById(request.Id);
            return product.Adapt<ProductDto>();
        }

        public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var p = request.Adapt<Product>();
            p.CreatedAt = System.DateTime.Now;
            p.IsDeleted = false;
            await _unitOfWork.ProductRepository.Add(p);
            await _unitOfWork.SaveChangesAsync();
            return Unit.Value;
        }

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.ProductRepository.Update(request.Adapt<Product>());
            await _unitOfWork.SaveChangesAsync();
            return Unit.Value;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.ProductRepository.Delete(request.Id);
            await _unitOfWork.SaveChangesAsync();
            return Unit.Value;
        }

        public async Task<Unit> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var cat = request.Adapt<Category>();
            cat.CreatedAt = System.DateTime.Now;
            cat.IsDeleted = false;
            await _unitOfWork.ProductRepository.CreateCategory(cat);
            await _unitOfWork.SaveChangesAsync();
            return Unit.Value;
        }

        public async Task<IEnumerable<CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.ProductRepository.GetCategories();
            return products.Adapt<IEnumerable<CategoryDto>>();
        }

        public async Task<IEnumerable<TopListedProduct>> Handle(GetTopListedProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.ProductMongoRepository.GetAllTopListedProducts();
            return products;
        }

        public async Task<IEnumerable<UserTopListedProduct>> Handle(GetUserTopListedProductsQuery request, CancellationToken cancellationToken)
        {
            var productsFromCache = await _distributedCache.GetAsync("UserTopListedProducts");
            if (productsFromCache != null)
            {
                var json = Encoding.UTF8.GetString(productsFromCache);
                return JsonConvert.DeserializeObject<List<UserTopListedProduct>>(json);
            }
            var products = await _unitOfWork.ProductMongoRepository.GetAllUserTopListedProducts();
            var productsjson = JsonConvert.SerializeObject(products);
            var productsCache = Encoding.UTF8.GetBytes(productsjson);
            var options = new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromDays(1))
                    .SetAbsoluteExpiration(DateTime.Now.AddDays(10));
            await _distributedCache.SetAsync("UserTopListedProducts", productsCache, options);
            return products;
        }
    }
}
