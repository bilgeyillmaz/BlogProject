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
	public class ContactManager : IContactService
	{
		IContactDal _contactDal;
		public ContactManager(IContactDal contactDal)
		{
			_contactDal = contactDal;
		}

		public void Add(Contact contact)
		{
			_contactDal.Insert(contact);
		}

		public void Delete(Contact contact)
		{
			throw new NotImplementedException();
		}

		public List<Contact> GetAll()
		{
			throw new NotImplementedException();
		}

		public Contact GetById(int id)
		{
			throw new NotImplementedException();
		}

		public void Update(Contact contact)
		{
			throw new NotImplementedException();
		}
	}
}
