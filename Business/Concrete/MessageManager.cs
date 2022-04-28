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
	public class MessageManager : IMessageService
	{
		IMessageDal _messageDal;

		public MessageManager(IMessageDal messageDal)
		{
			_messageDal = messageDal;
		}

		public void Add(Message t)
		{
			throw new NotImplementedException();
		}

		public void Delete(Message t)
		{
			throw new NotImplementedException();
		}

		public List<Message> GetAll()
		{
			return _messageDal.GetAll();	
		}

		public Message GetById(int id)
		{
			throw new NotImplementedException();
		}

        public List<Message> GetInboxListByWriter(string p)
        {
			return _messageDal.GetListAll(m=>m.Receiver == p);
		}

        public void Update(Message t)
		{
			throw new NotImplementedException();
		}
	}
}
