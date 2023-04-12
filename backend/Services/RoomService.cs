using AutoMapper;
using BCryptNet = BCrypt.Net.BCrypt;
using System.Collections.Generic;
using System.Linq;
using WebApi.Authorization;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Rooms;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Services
{
    public interface IRoomService
    {
        IEnumerable<Room> GetAll();                              // смотри строки 52 - 56
        Room GetById(int id);
        void Register(RegisterRequest model);
        void Update(int id, UpdateRequest model);
        void Delete(int id);
    }

    public class RoomService : IRoomService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;

        public RoomService(
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
        //    var Room = _context.Rooms.SingleOrDefault(x => x.Roomname == model.Roomname);

        //    // validate
        //    if (Room == null || !BCryptNet.Verify(model.Password, Room.PasswordHash))
        //        throw new AppException("Имя пользователя или пароль введены неверно");

        //    // authentication successful
        //    var response = _mapper.Map<AuthenticateResponse>(Room);
        //    response.Token = _jwtUtils.GenerateToken(Room);
        //    return response;
        //}

        public IEnumerable<Room> GetAll()
        {
            return _context.Rooms.Include(l => l.Build);
        }

        public Room GetById(int id)
        {
            return getRoom(id);
        }

        public void Register(RegisterRequest model)
        {
            // validate
            if (_context.Rooms.Any(x => x.Name == model.Name)) //  ТУТ СТОИТ ПОДУМАТЬ
                throw new AppException("Такой статус '" + model.Name + "' уже существует");

            Build build = null;

            build = _context.Builds.Where(build => build.Id == model.Build).FirstOrDefault();


            // map model to new user object
            //var user = _mapper.Map<User>(model);
            Room room = new Room
            {
                Name = model.Name,
                Build = build
            };

            _context.Rooms.Add(room);
            _context.SaveChanges();
        }

        public void Update(int id, UpdateRequest model)
        {
            var Room = getRoom(id);

            //// validate                                                                                    Жена занимается сексом с 3 муужиками, тут
            if (model.Name != Room.Name && _context.Rooms.Any(x => x.Name == model.Name))              // приходит муж. Жена достаёт черные мешки и пакует туда
                throw new AppException("Такой статус'" + model.Name + "' is already taken");                    //любовников. 
            //                                                                                               Муж: что это за мешки?                                                                                                             
            //                                                                                               Жена: мама с дачи гостинцы привезла
            //                                                                                               Муж пнул один мешок и слышит "Беее"
            //                                                                                               Муж: О барашек, отлично жаркое сварим   
            //                                                                                               Пнул второй мешок и слышит "Хрюююю"   
            //                                                                                               Муж: свинья, отлично - сало замаринуем   
            //                                                                                               Пнул третий мешок - тишина, пнул ещё раз - тишина, пнул со всей силы
            // copy model to user and save                                                                   Мешок: ты чё блять тупой? картошка я!               
            _mapper.Map(model, Room);
            _context.Rooms.Update(Room);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var Room = getRoom(id);
            _context.Rooms.Remove(Room);                      // опять же удаление записи, где есть ограничение на удаление я хз как делать
            _context.SaveChanges();
        }

        // helper methods

        private Room getRoom(int id)
        {
            var room = _context.Rooms.FirstOrDefault(u => u.Id == id); // .FirstOrDefault(u => u.Id == id);
            room.Build = _context.Builds.FirstOrDefault(u => u.Id == room.Build.Id);
            if (room == null) throw new KeyNotFoundException("Местоположение не найдено");
            return room;
        }
    }
}