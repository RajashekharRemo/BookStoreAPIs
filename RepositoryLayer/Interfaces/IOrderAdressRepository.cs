using CommonLayer.OrderAddress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IOrderAdressRepository
    {
        public List<Orders> GetAllOrderes(int UId);
        public bool AddOrder(int Uid, OrderToDB toDB);
        public bool AddAddress(int UId, UpdateAddress address);
        public bool UpdateAddress(int UId, Address address);
        public List<Address> GetAddresses(int UId);
    }
}
