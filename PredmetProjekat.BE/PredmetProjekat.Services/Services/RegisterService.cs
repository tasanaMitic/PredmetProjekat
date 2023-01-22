using PredmetProjekat.Common.Dtos;
using PredmetProjekat.Common.Interfaces;
using PredmetProjekat.Models.Models;

namespace PredmetProjekat.Services.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly IUnitOfWork _unitOfWork;
        public RegisterService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Guid AddRegister(RegisterDto registerDto)
        {
            var id = Guid.NewGuid();
            _unitOfWork.RegisterRepository.Add(new Register
            {
                RegisterId = id,
                RegisterCode = registerDto.RegisterCode,
                Location = registerDto.Location
            });

            return id;
        }

        public bool DeleteRegister(Guid id)
        {

            return _unitOfWork.RegisterRepository.Remove(id);
        }

        public IEnumerable<RegisterDto> GetRegisters()
        {
            return _unitOfWork.RegisterRepository.GetAll().Select(x => new RegisterDtoId
            {
                RegisterId = x.RegisterId,
                RegisterCode = x.RegisterCode,
                Location = x.Location
            });
        }
    }
}
