using AutoMapper;
using SmartDelivery.Data.Entities;
using SmartDelivery.Data.Repositories;
using SmartDelivery.Infrastructure.Models;
using SmartDelivery.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SmartDelivery.Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task Create(Category category)
        {
            if (category is null)
            {
                throw new ArgumentNullException("Category doesn't exist");
            }

            await _categoryRepository.Create(category);
        }

        public async Task Delete(int? id)
        {
            var categoryEntity = await _categoryRepository.Get(id.Value);
            if (categoryEntity is null)
            {
                return;
            }

            await _categoryRepository.Delete(categoryEntity);
        }

        public async Task<Category> Get(int? id)
        {
            var categoryEntity = await _categoryRepository.Get(id.Value);
            if (categoryEntity is null)
            {
                throw new ArgumentNullException("Category doesn't exist");
            }

            return categoryEntity;
        }

        public async Task<IList<Category>> GetAll(Expression<Func<Category, bool>> filter = null)
        {
            if (filter is null)
            {
                return await _categoryRepository.GetAll(p => p.ParentId == null);
            }

            return await _categoryRepository.GetAll(filter);
        }

        public async Task<IList<CategoryRequest>> GetAllCategoryWithSubCategory()
        {
            var getAllCategories = await GetAll(x => x.ParentId == null);
            var listAllCategories = new List<CategoryRequest>();
            foreach (var v in getAllCategories)
            {
                var category = _mapper.Map<Category, CategoryRequest>(v);
                var subList = await GetWithParentId(v.Id);
                var SubCategoryList = new List<SubCategoryRequest>();
                foreach (var j in subList)
                {
                    var subCategory = _mapper.Map<Category, SubCategoryRequest>(j);
                    SubCategoryList.Add(subCategory);
                }
                category.Subs = SubCategoryList;
                listAllCategories.Add(category);
            }

            return listAllCategories;
        }

        public async Task<IList<Category>> GetWithParentId(int? id)
        {
            var categoryEntity = await _categoryRepository.GetWithParentId(id.Value);
            if (categoryEntity is null)
            {
                throw new ArgumentNullException("Subcategory doesn't exist");
            }

            return categoryEntity;
        }

        public async Task Update(Category category)
        {
            var categoryEntity = await _categoryRepository.Get(category.Id);
            if (categoryEntity is null)
            {
                throw new ArgumentNullException("Category doesn't exist");
            }

            await _categoryRepository.Update(category);
        }
    }
}

