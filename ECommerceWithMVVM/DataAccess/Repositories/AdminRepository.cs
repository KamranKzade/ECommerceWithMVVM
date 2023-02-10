using System.Linq;
using ECommerceWithMVVM.Domain.Abstractions;
using System.Collections.ObjectModel;

namespace ECommerceWithMVVM.DataAccess.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ECommerceDataClassesDataContext _dataContext;

        public AdminRepository()
        {
            _dataContext = new ECommerceDataClassesDataContext();
        }

        public void AddData(Admin data)
        {
            _dataContext.Admins.InsertOnSubmit(data);
            _dataContext.SubmitChanges();
        }

        public void DeleteData(Admin data)
        {
            _dataContext.Admins.DeleteOnSubmit(data);
            _dataContext.SubmitChanges();
        }

        public ObservableCollection<Admin> GetAllData()
        {
            var admins = from a in _dataContext.Admins
                         select a;

            return new ObservableCollection<Admin>(admins);
        }

        public Admin GetData(int id)
        {
            return _dataContext.Admins.SingleOrDefault(a => a.Id == id);
        }

        public void UpdateData(Admin data)
        {
            var item = _dataContext.Admins.SingleOrDefault(a => a.Id == data.Id);

            item = new Admin
            {
                Id = data.Id,
                Username = data.Username,
                Password = data.Password,
            };

            _dataContext.SubmitChanges();
        }
    }
}