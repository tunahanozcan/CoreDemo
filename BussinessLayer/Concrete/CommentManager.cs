using BussinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Concrete
{
    public class CommentManager : ICommentService
    {
        ICommentDal _commentDal;

        public CommentManager(ICommentDal commentDal)
        {
            _commentDal = commentDal;
        }

        public void CommentAdd(Comment comment)
        {
            _commentDal.Insert(comment);
        }

        public List<Comment> GetCommentWithBlog()
        {
            return _commentDal.GetListWithBlog();
        }

        public List<Comment> GetList(int id)
        {
           return _commentDal.GetListAll(x => x.BlogId == id);
        }
        public void TDelete(Comment t)
        {
            _commentDal.Delete(t);
        }
        public Comment GetCommentById(int id)
        {
            return _commentDal.GetById(id);
        }
        public void CommentEdit(Comment comment)
        {
            _commentDal.Update(comment);
        }
    }
}
