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
    public class BlogManager : IBlogService
    {
        IBlogDal _blogDal;

        public IEnumerable<object> Writers { get; set; }

        public BlogManager(IBlogDal blogDal)
        {
            _blogDal = blogDal;
        }

        public void Add(Blog blog)
        {
            _blogDal.Insert(blog);
        }

        public void Delete(Blog blog)
        {
            _blogDal.Delete(blog);
        }

        public List<Blog> GetAll()
        {
            return _blogDal.GetAll();
        }

        public List<Blog> GetAllBlogswithCategory()
        {
            return _blogDal.GetAllWithCategory();
        }
        
        public List<Blog> GetBlogById(int id)
        {
            return _blogDal.GetListAll(b => b.Id== id);
        }

        public List<Blog> GetBlogListWithWriter(int id)
        {
            return _blogDal.GetListAll(b=>b.WriterId== id); 
        }

        public Blog GetById(int id)
        {
            return _blogDal.GetById(id);
        }

        public void Update(Blog blog)
        {
            _blogDal.Update(blog);
        }
        public List<Blog> GetLastThreeBlog()
        {
            return _blogDal.GetAll().Take(3).ToList();
        }
        public List<Blog> GetListWithCategoryByWriterBM(int id)
		{
            return _blogDal.GetListWithCategoryByWriter(id);
		}
    }
}
