using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using UnitOfWorkPattern.Application.Dtos;
using UnitOfWorkPattern.Application.Exceptions;
using UnitOfWorkPattern.Application.Interfaces.Repositories;
using UnitOfWorkPattern.Application.Wrappers;
using UnitOfWorkPattern.Domain.Entities;
using System.Net;
using UnitOfWorkPattern.Application.Interfaces.UnitOfWork;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UnitOfWorkPattern.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public ProductController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _uow.Products.GetByIdAsync(id);

            if (result is null) throw new ApiException($"Product Not Found.");

            var resulViewModel = _mapper.Map<GetProductByIdResponse>(result);

            return Ok(new ApiResponse<GetProductByIdResponse>(resulViewModel));
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            var result = await _uow.Products.GetAllAsync();

            var resulViewModelList = _mapper.Map<IEnumerable<GetAllProductsResponse>>(result);

            return Ok(new ApiResponse<IEnumerable<GetAllProductsResponse>>(resulViewModelList));
        }

        [HttpGet("GetPagedAll")]
        public async Task<IActionResult> GetPagedAll([FromQuery] GetAllProductsRequest request)
        {
            var result = await _uow.Products.GetAllPagedAsync(request.PageNumber, request.PageSize);

            var resulViewModelList = _mapper.Map<IEnumerable<GetAllProductsResponse>>(result);

            return Ok(new PagedResponse<IEnumerable<GetAllProductsResponse>>(resulViewModelList, request.PageNumber, request.PageSize));
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateRequest request)
        {
            var product = _mapper.Map<Product>(request);

            await _uow.Products.AddAsync(product);

            await _uow.Complete();

            return Ok(new ApiResponse<string>(string.Empty, "Success"));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, ProductCreateRequest request)
        {
            var product = await _uow.Products.GetByIdAsync(id);

            if (product is null) throw new ApiException($"Product Not Found.");

            product.Name = request.Name;
            product.Price = request.Price;
            product.Quantity = request.Quantity;

            await _uow.Products.UpdateAsync(product);

            await _uow.Complete();

            return Ok(new ApiResponse<string>(string.Empty, "Success"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await _uow.Products.GetByIdAsync(id);

            if (product is null) throw new ApiException($"Product Not Found.");

            await _uow.Products.DeleteAsync(product);

            await _uow.Complete();

            return Ok(new ApiResponse<string>(string.Empty, "Success"));
        }
    }
}
