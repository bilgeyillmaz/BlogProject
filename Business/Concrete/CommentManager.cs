using Business.Abstract;
using DataAccess.Abstract;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CommentManager : ICommentService
    {
        ICommentDal _commentDal;

        public CommentManager(ICommentDal commentDal)
        {
            _commentDal = commentDal;
        }

        public void Add(Comment comment)
        {
           _commentDal.Insert(comment); 
        }

		public void Delete(Comment comment)
		{
			throw new NotImplementedException();
		}

		public List<Comment> GetAll()
		{
			throw new NotImplementedException();
		}

		public Comment GetById(int id)
		{
			throw new NotImplementedException();
		}

		public List<Comment> GetList(int id)
        {
            return _commentDal.GetListAll(c => c.BlogId == id);
        }

		public void Update(Comment comment)
		{
			throw new NotImplementedException();
		}
	}
}
