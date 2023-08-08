using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BussinessLayer.Abstract
{
    public interface IBannedListService : IGenericService<BannedList>
    {
        List<string> GetBannedIpList();
        List<string> GetSpamMessageList();
    }
}
