using BusinessLayer.Interfaces;
using CommonLayer.OrderAddress;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class OrderAddressBusiness :IOrderAddressBusiness
    {
        private readonly IOrderAdressRepository _orderAdressRepository;
        public OrderAddressBusiness(IOrderAdressRepository orderAdressRepository)
        {
            _orderAdressRepository = orderAdressRepository;
        }
        public List<Orders> GetAllOrderes(int UId)
        {
            return _orderAdressRepository.GetAllOrderes(UId);
        }

        public bool AddOrder(int Uid, OrderToDB toDB)
        {
            return _orderAdressRepository.AddOrder(Uid, toDB);
        }

        public bool AddAddress(int UId, UpdateAddress address)
        {
            return _orderAdressRepository.AddAddress(UId, address);
        }

        public bool UpdateAddress(int UId, Address address)
        {
            return _orderAdressRepository.UpdateAddress(UId, address);
        }

        public List<Address> GetAddresses(int UId)
        {
            return _orderAdressRepository.GetAddresses(UId);
        }
    }
}
