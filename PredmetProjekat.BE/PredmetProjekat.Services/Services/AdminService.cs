using PredmetProjekat.Common.Dtos;
using PredmetProjekat.Common.Interfaces;
using PredmetProjekat.Models.Models;

namespace PredmetProjekat.Services.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AdminService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public string AddAdmin(AccountDto accountDto)
        {
            _unitOfWork.AdminRepository.Add(new Admin
            {
                Lastname = accountDto.LastName,
                FirstName = accountDto.FirstName,
                UserName = accountDto.Username

            });

            return accountDto.Username;
        }

        public bool DeleteAdmin(string username)
        {
            return _unitOfWork.AdminRepository.RemoveByUsername(username);
        }

        public IEnumerable<AccountDto> GetAdmins()
        {
            return _unitOfWork.AdminRepository.GetAll().Select(x => new AccountDto
            {
                LastName = x.Lastname,
                FirstName = x.FirstName,
                Username = x.UserName
            });
        }
    }
}
