﻿using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Cosmetics.DTO.Product;
using Cosmetics.DTO.User;
using Cosmetics.Models;
using Cosmetics.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace Cosmetics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAllProduct")]
        public async Task<IActionResult> GetAll(string search = null, int? page = null, int? pageSize = null, string sortBy = null)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid data",
                    Data = ModelState
                });
            }

            var list = await _unitOfWork.Products.GetAllAsync();

            var totalCount = list.Count();

            page ??= 1;
            pageSize ??= 10;

            var products = await _unitOfWork.Products.GetAsync(
                filter: p => string.IsNullOrEmpty(search) || p.Name.ToLower().Contains(search.ToLower()),
                orderBy: sortBy switch
                {
                    "price" => q => q.OrderBy(p => p.Price),
                    _ => q => q.OrderBy(p => p.ProductId),
                },
                page: page,
                pageSize: pageSize,
                includes: [p => p.Brand, p => p.Category]
                );


            var productDTO = _mapper.Map<List<ProductDTO>>(products);
            var response = new
            {
                TotalCount = totalCount,
                ToTalPages = (int)Math.Ceiling(totalCount / (double)pageSize.Value),
                CurrentPage = page,
                PageSize = pageSize,
                Products = productDTO
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("GetProductBy/{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid data",
                    Data = ModelState
                });
            }

            var products = await _unitOfWork.Products.GetAsync(
                filter: p => p.ProductId == id,
                includes: [p => p.Brand, p => p.Category]
                );

            var product = products.FirstOrDefault();

            if(product == null)
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Product not found!"
                });
            }

            return Ok(new ApiResponse 
            { 
                Success = true,
                StatusCode = StatusCodes.Status200OK,
                Data = _mapper.Map<ProductDTO>(product)
            });
        }

        [HttpDelete]
        [Route("DeleteProduct/{id:guid}")]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid data",
                    Data = ModelState
                });
            }

            var product = await _unitOfWork.Products.GetByIdAsync(id);

            if(product == null)
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Product does not exist!",
                }); 
            }

             _unitOfWork.Products.Delete(product);
            await _unitOfWork.CompleteAsync();

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Product deleted successfully"
            });
        }

        [HttpPost]
        [Route("CreateProduct")]
        public async Task<IActionResult> Create(ProductCreateDTO productDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid data",
                    Data = ModelState
                });
            }

            if(!await _unitOfWork.Products.CategoryExist(productDTO.CategoryId.Value)) 
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    StatusCode= StatusCodes.Status404NotFound,
                    Message = "Category does not exist!",
                });
            }

            if(!await _unitOfWork.Products.BranchExist(productDTO.BrandId.Value))
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Branch does not exist!",
                });
            }

            var imageUrls = new List<string>();




            var productModel = new Product
            {
                ProductId = Guid.NewGuid(),
                Name = productDTO.Name,
                Description = productDTO.Description,
                Price = productDTO.Price,
                StockQuantity = productDTO.StockQuantity,
                ImageUrls = productDTO.imageUrls.ToArray(),
                CommissionRate = productDTO.CommissionRate,
                CategoryId = productDTO.CategoryId,
                BrandId = productDTO.BrandId,
                CreateAt = DateTime.Now,
                IsActive = true,
            };

            await _unitOfWork.Products.AddAsync(productModel);
            await _unitOfWork.CompleteAsync();

            return Ok(new ApiResponse
            {
                Success = true,
                StatusCode = StatusCodes.Status200OK,
                Message = "Created Product Successfully.",
                Data = _mapper.Map<ProductDTO>(productModel)
            });
        }

        [HttpPut]
        [Route("UpdateProduct/{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, ProductUpdateDTO productDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid data",
                    Data = ModelState
                });
            }

            var existingProduct = await _unitOfWork.Products.GetByIdAsync(id);
            if(existingProduct == null)
            {
                return NotFound(new ApiResponse
                {
                    Success = false,
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Product not found!",
                });
            }

        
       

            existingProduct.Name = productDTO.Name;
            existingProduct.Description = productDTO.Description;
            existingProduct.Price = productDTO.Price;
            existingProduct.StockQuantity = productDTO.StockQuantity;
            existingProduct.ImageUrls = productDTO.ImageUrls.ToArray();
            existingProduct.CommissionRate = productDTO.CommissionRate;
            existingProduct.IsActive = productDTO.IsActive;

             _unitOfWork.Products.UpdateAsync(existingProduct);
            await _unitOfWork.CompleteAsync();


            return Ok(new ApiResponse
            {
                Success = true,
                StatusCode = StatusCodes.Status200OK,
                Message = "Updated Product Successfully.",
                Data = _mapper.Map<ProductDTO>(existingProduct)
            });
        }
    }
}
