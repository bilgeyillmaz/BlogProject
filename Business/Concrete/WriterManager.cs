﻿using Business.Abstract;
using DataAccess.Abstract;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class WriterManager : IWriterService
    {
        IWriterDal _writerDal;

        public WriterManager(IWriterDal writerDal)
        {
            _writerDal = writerDal;
        }

        public void Add(Writer writer)
        {
            _writerDal.Insert(writer);
        }

		public void Delete(Writer writer)
		{
			throw new NotImplementedException();
		}

		public List<Writer> GetAll()
		{
			throw new NotImplementedException();
		}

		public Writer GetById(int id)
		{
			return _writerDal.GetById(id);	
		}

		public List<Writer> GetWriterById(int id)
		{
			return _writerDal.GetListAll(w=>w.Id == id);	
		}

		public void Update(Writer writer)
		{
			 _writerDal.Update(writer);
		}
	}
}
