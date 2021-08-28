using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPIDevam_0.Controllers
{
    using Models;
    using DesignPatterns.SingletonPattern;
    using DTOClasses;
    public class CategoryController : ApiController
    {
        NorthwindEntities _db;

        public CategoryController()
        {
            _db = DBTool.DBInstance;
        }

        [HttpGet]
        public List<CategoryDTO> ListCategories()
        {
            return _db.Categories.Select(x => new CategoryDTO
            {
                ID = x.CategoryID,
                Name = x.CategoryName,
                Description = x.Description
            }).ToList();
        }


        [HttpGet]
        public CategoryDTO BringCategory(int id)
        {
            return _db.Categories.Where(x => x.CategoryID == id).Select(x => new CategoryDTO
            {
                ID = x.CategoryID,
                Name = x.CategoryName,
                Description = x.Description
            }).FirstOrDefault();
        }

        [HttpGet]
        public List<CategoryDTO> SearchCategory(string item)
        {
            return _db.Categories.Where(x => x.CategoryName.Contains(item)).Select(x => new CategoryDTO
            {
                ID = x.CategoryID,
                Name = x.CategoryName,
                Description = x.Description

            }).ToList();
        }

        [HttpPost]
        public List<CategoryDTO> AddCategory(Category item)
        {
            _db.Categories.Add(item);
            _db.SaveChanges();
            return ListCategories();
        }

        [HttpPut]
        public List<CategoryDTO> UpdateCategory(Category item)
        {
            Category toBeUpdates = _db.Categories.Find(item.CategoryID);
            _db.Entry(toBeUpdates).CurrentValues.SetValues(item);
            _db.SaveChanges();
            return ListCategories();
        }

        [HttpDelete]
        public List<CategoryDTO> DeleteCategory(int id)
        {
            _db.Categories.Remove(_db.Categories.Find(id));
            _db.SaveChanges();
            return ListCategories();
        }
        
       


    }
}
