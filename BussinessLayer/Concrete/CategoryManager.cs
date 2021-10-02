using BussinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Concrete
{
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _cagoryDal;

        public CategoryManager(ICategoryDal cagoryDal)
        {
            _cagoryDal = cagoryDal;
        }

        public void CategoryAdd(Category category)
        {
            _cagoryDal.Insert(category);
        }

        public void CategoryDelete(Category category)
        {
            _cagoryDal.Delete(category);
        }

        public void CategoryUpdate(Category category)
        {
            _cagoryDal.Update(category);
        }

        public Category GetById(int id)
        {
            return _cagoryDal.GetById(id);
        }

        public List<Category> GetList()
        {
            return _cagoryDal.GetListAll();
        }
    }
}
