using AutoMapper;
using PredmetProjekat.Common.Dtos;
using PredmetProjekat.Common.Interfaces;
using PredmetProjekat.Common.Interfaces.IService;
using PredmetProjekat.Models.Models;

namespace PredmetProjekat.Services.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RegisterService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public Guid AddRegister(RegisterDto registerDto)
        {
            var id = Guid.NewGuid();
            _unitOfWork.RegisterRepository.CreateRegister(new Register
            {
                RegisterId = id,
                RegisterCode = registerDto.RegisterCode,
                Location = registerDto.Location
            });
            _unitOfWork.SaveChanges();

            return id;
        }

        public void DeleteRegister(Guid id)
        {
            var registerToBeDeleted = _unitOfWork.RegisterRepository.GetRegisterById(id);
            _unitOfWork.RegisterRepository.DeleteRegister(registerToBeDeleted);
            _unitOfWork.SaveChanges();
        }

        public IEnumerable<RegisterDtoId> GetRegisters()
        {
            var registers = _unitOfWork.RegisterRepository.GetAllRegisters();
            return _mapper.Map<IEnumerable<RegisterDtoId>>(registers);
        }
    }
}
