using AutoMapper;
using BCryptNet = BCrypt.Net.BCrypt;
using System.Collections.Generic;
using System.Linq;
using WebApi.Authorization;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Builds;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Services
{
    public interface IBuildService
    {
        IEnumerable<Build> GetAll();                              // смотри строки 52 - 56
        Build GetById(int id);
        void Register(RegisterRequest model);
        void Update(int id, UpdateRequest model);
        void Delete(int id);
    }

    public class BuildService : IBuildService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;

        public BuildService(
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
        //    var Build = _context.Builds.SingleOrDefault(x => x.Buildname == model.Buildname);

        //    // validate
        //    if (Build == null || !BCryptNet.Verify(model.Password, Build.PasswordHash))
        //        throw new AppException("Имя пользователя или пароль введены неверно");

        //    // authentication successful
        //    var response = _mapper.Map<AuthenticateResponse>(Build);
        //    response.Token = _jwtUtils.GenerateToken(Build);
        //    return response;
        //}

        public IEnumerable<Build> GetAll()
        {
            return _context.Builds.ToList();
        }

        public Build GetById(int id)
        {
            return getBuild(id);
        }

        public void Register(RegisterRequest model)
        {
            // validate
            if (_context.Builds.Any(x => x.Name == model.Name)) //  ТУТ СТОИТ ПОДУМАТЬ
                throw new AppException("Такой статус '" + model.Name + "' уже существует");
            
            // map model to new user object
            var Build = _mapper.Map<Build>(model);

            _context.Builds.Add(Build);
            _context.SaveChanges();
        }

        public void Update(int id, UpdateRequest model)
        {
            var Build = getBuild(id);

            //// validate                                                                                    Жена занимается сексом с 3 муужиками, тут
            if (model.Name != Build.Name && _context.Builds.Any(x => x.Name == model.Name))              // приходит муж. Жена достаёт черные мешки и пакует туда
                throw new AppException("Такой статус'" + model.Name + "' is already taken");                    //любовников. 
            //                                                                                               Муж: что это за мешки?                                                                                                             
            //                                                                                               Жена: мама с дачи гостинцы привезла
            //                                                                                               Муж пнул один мешок и слышит "Беее"
            //                                                                                               Муж: О барашек, отлично жаркое сварим   
            //                                                                                               Пнул второй мешок и слышит "Хрюююю"   
            //                                                                                               Муж: свинья, отлично - сало замаринуем   
            //                                                                                               Пнул третий мешок - тишина, пнул ещё раз - тишина, пнул со всей силы
            // copy model to user and save                                                                   Мешок: ты чё блять тупой? картошка я!               
            _mapper.Map(model, Build);
            _context.Builds.Update(Build);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var Build = getBuild(id);
            _context.Builds.Remove(Build);                      // опять же удаление записи, где есть ограничение на удаление я хз как делать
            _context.SaveChanges();
        }

        // helper methods

        private Build getBuild(int id)
        {
            var Build = _context.Builds.Find(id);
            if (Build == null) throw new KeyNotFoundException("Местоположение не найдено");
            return Build;
        }
    }
}