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
    public class BannedListManager : IBannedListService
    {
        IBannedListDal _bannedListDal;

        public BannedListManager(IBannedListDal bannedListDal)
        {
            _bannedListDal = bannedListDal;
        }

        public List<string> GetBannedIpList()
        {
            return _bannedListDal.GetListAll().Select(x=>x.BannedIp).ToList();
        }

        public List<BannedList> GetList()
        {
            return _bannedListDal.GetListAll();
        }

        public List<string> GetSpamMessageList()
        {
            return _bannedListDal.GetListAll().Select(x => x.SpamMessage).ToList();
        }

        public void TAdd(BannedList t)
        {
            throw new NotImplementedException();
        }

        public void TDelete(BannedList t)
        {
            throw new NotImplementedException();
        }

        public BannedList TGetById(int id)
        {
            throw new NotImplementedException();
        }

        public void TUpdate(BannedList t)
        {
            throw new NotImplementedException();
        }
    }
}
