using AutoMapper;
using BCryptNet = BCrypt.Net.BCrypt;
using System.Collections.Generic;
using System.Linq;
using WebApi.Authorization;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Machines;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Services
{
    public interface IMachineService
    {
        IEnumerable<Machine> GetAll();                              // смотри строки 52 - 56
        Machine GetById(int id);
        void Register(RegisterRequest model);
        void Update(int id, UpdateRequest model);
        void Delete(int id);
    }

    public class MachineService : IMachineService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;

        public MachineService(
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
        //    var Machine = _context.Machines.SingleOrDefault(x => x.Machinename == model.Machinename);

        //    // validate
        //    if (Machine == null || !BCryptNet.Verify(model.Password, Machine.PasswordHash))
        //        throw new AppException("Имя пользователя или пароль введены неверно");

        //    // authentication successful
        //    var response = _mapper.Map<AuthenticateResponse>(Machine);
        //    response.Token = _jwtUtils.GenerateToken(Machine);
        //    return response;
        //}

        public IEnumerable<Machine> GetAll()
        {
            return _context.Machines.ToList();
        }

        public Machine GetById(int id)
        {
            return getMachine(id);
        }

        public void Register(RegisterRequest model)
        {
            // validate
            if (_context.Machines.Any(x => x.InventoryNumber == model.InventoryNumber)) //  ТУТ СТОИТ ПОДУМАТЬ
                throw new AppException("Станок с инвентарным номером '" + model.InventoryNumber + "' уже существует");
            
            // map model to new user object
            var machine = _mapper.Map<Machine>(model);

            _context.Machines.Add(machine);
            _context.SaveChanges();
        }

        public void Update(int id, UpdateRequest model)
        {
            var machine = getMachine(id);

            //// validate                                                                                    Жена занимается сексом с 3 муужиками, тут
            if (model.InventoryNumber != machine.InventoryNumber && _context.Machines.Any(x => x.InventoryNumber == model.InventoryNumber))              // приходит муж. Жена достаёт черные мешки и пакует туда
                throw new AppException("Станок с инвентарным номером '" + model.InventoryNumber + "' is already taken");                    //любовников. 
            //                                                                                               Муж: что это за мешки?                                                                                                             
            //                                                                                               Жена: мама с дачи гостинцы привезла
            //                                                                                               Муж пнул один мешок и слышит "Беее"
            //                                                                                               Муж: О барашек, отлично жаркое сварим   
            //                                                                                               Пнул второй мешок и слышит "Хрюююю"   
            //                                                                                               Муж: свинья, отлично - сало замаринуем   
            //                                                                                               Пнул третий мешок - тишина, пнул ещё раз - тишина, пнул со всей силы
            // copy model to user and save                                                                   Мешок: ты чё блять тупой? картошка я!               
            _mapper.Map(model, machine);
            _context.Machines.Update(machine);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var machine = getMachine(id);
            _context.Machines.Remove(machine);                      // опять же удаление записи, где есть ограничение на удаление я хз как делать
            _context.SaveChanges();
        }

        // helper methods

        private Machine getMachine(int id)
        {
            var machine = _context.Machines.Find(id);
            if (machine == null) throw new KeyNotFoundException("Местоположение не найдено");
            return machine;
        }
    }
}