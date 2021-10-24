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
        public Category GetById(int id)
        {
            return _cagoryDal.GetById(id);
        }
        public List<Category> GetList()
        {
            return _cagoryDal.GetListAll();
        }
        public void TAdd(Category t)
        {
            _cagoryDal.Insert(t);
        }
        public void TDelete(Category t)
        {
            _cagoryDal.Delete(t);
        }
        public void TUpdate(Category t)
        {
            _cagoryDal.Update(t);
        }
    }
}
