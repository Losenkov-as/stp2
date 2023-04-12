using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.MachineType;
using WebApi.Authorization;

namespace WebApi.Services
{
    public interface IMachineTypeService
    {
        IEnumerable<MachineType> GetAll();                              // смотри строки 52 - 56
        MachineType GetById(int id);
        void Register(RegisterRequest model);
        void Update(int id, UpdateRequest model);
        void Delete(int id);
    }

    public class MachineTypeService : IMachineTypeService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;

        public MachineTypeService(
            DataContext context,
            IJwtUtils jwtUtils,
            IMapper mapper)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }

        //public AuthenticateResponse Authenticate(AuthenticateRequest model)
        //{
        //    var MachineType = _context.MachineTypes.SingleOrDefault(x => x.MachineTypename == model.MachineTypename);

        //    // validate
        //    if (MachineType == null || !BCryptNet.Verify(model.Password, MachineType.PasswordHash))
        //        throw new AppException("Имя пользователя или пароль введены неверно");

        //    // authentication successful
        //    var response = _mapper.Map<AuthenticateResponse>(MachineType);
        //    response.Token = _jwtUtils.GenerateToken(MachineType);
        //    return response;
        //}

        public IEnumerable<MachineType> GetAll()
        {
            return _context.MachineTypes.ToList();
        }

        public MachineType GetById(int id)
        {
            return getMachineType(id);
        }

        public void Register(RegisterRequest model)
        {
            // validate
            if (_context.MachineTypes.Any(x => x.Name == model.Name)) //  ТУТ СТОИТ ПОДУМАТЬ
                throw new AppException("Такой статус '" + model.Name + "' уже существует");
            
            // map model to new user object
            var MachineType = _mapper.Map<MachineType>(model);
            //var arr = _context.MachineTypes.ToList();
        
            _context.MachineTypes.Add(MachineType);
            _context.SaveChanges();
        }

        public void Update(int id, UpdateRequest model)
        {
            var MachineType = getMachineType(id);

            //// validate                                                                                    Жена занимается сексом с 3 муужиками, тут
            if (model.Name != MachineType.Name && _context.MachineTypes.Any(x => x.Name == model.Name))              // приходит муж. Жена достаёт черные мешки и пакует туда
                throw new AppException("Такой статус'" + model.Name + "' is already taken");                    //любовников. 
            //                                                                                               Муж: что это за мешки?                                                                                                             
            //                                                                                               Жена: мама с дачи гостинцы привезла
            //                                                                                               Муж пнул один мешок и слышит "Беее"
            //                                                                                               Муж: О барашек, отлично жаркое сварим   
            //                                                                                               Пнул второй мешок и слышит "Хрюююю"   
            //                                                                                               Муж: свинья, отлично - сало замаринуем   
            //                                                                                               Пнул третий мешок - тишина, пнул ещё раз - тишина, пнул со всей силы
            // copy model to user and save                                                                   Мешок: ты чё блять тупой? картошка я!               
            _mapper.Map(model, MachineType);
            _context.MachineTypes.Update(MachineType);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var MachineType = getMachineType(id);
            _context.MachineTypes.Remove(MachineType);                      // опять же удаление записи, где есть ограничение на удаление я хз как делать
            _context.SaveChanges();
        }

        // helper methods

        private MachineType getMachineType(int id)
        {
            var MachineType = _context.MachineTypes.Find(id);
            if (MachineType == null) throw new KeyNotFoundException("Местоположение не найдено");
            return MachineType;
        }
    }
}