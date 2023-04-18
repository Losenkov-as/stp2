using AutoMapper;
using BCryptNet = BCrypt.Net.BCrypt;
using System.Collections.Generic;
using System.Linq;
using WebApi.Authorization;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.AppHistory;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Services
{
    public interface IAppHistoryService
    {
        IEnumerable<AppHistory> GetAll();                              // смотри строки 52 - 56
        AppHistory GetById(int id);
        void Register(RegisterRequest model);
        //void Update(int id, UpdateRequest model);
        void Delete(int id);
    }

    public class AppHistoryService : IAppHistoryService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;

        public AppHistoryService(
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
        //    var AppHistory = _context.AppHistorys.SingleOrDefault(x => x.AppHistoryname == model.AppHistoryname);

        //    // validate
        //    if (AppHistory == null || !BCryptNet.Verify(model.Password, AppHistory.PasswordHash))
        //        throw new AppException("Имя пользователя или пароль введены неверно");

        //    // authentication successful
        //    var response = _mapper.Map<AuthenticateResponse>(AppHistory);
        //    response.Token = _jwtUtils.GenerateToken(AppHistory);
        //    return response;
        //}

        public IEnumerable<AppHistory> GetAll()
        {
            return _context.AppHistory.Include(l => l.maintenance);
        }

        public AppHistory GetById(int id)
        {
            return getAppHistory(id);
        }

        public void Register(RegisterRequest model)
        {
            // validate
            //if (_context.AppHistory.Any(x => x.maintenance == model.maintenance)) //  ТУТ СТОИТ ПОДУМАТЬ
            //    throw new AppException("Помещение '" + model.Plot + "' уже существует");
            
            // map model to new user object

            Maintenance maintenance = null;

            maintenance = _context.Maintenances.Where(maintenance => maintenance.Id == model.maintenance).FirstOrDefault();


            // map model to new user object
            //var user = _mapper.Map<User>(model);
            AppHistory AppHistory = new AppHistory
            {
                maintenance = maintenance
            };

            _context.AppHistory.Add(AppHistory);
            _context.SaveChanges();
        }

        //public void Update(int id, UpdateRequest model)
        //{
        //    var AppHistory = getAppHistory(id);

        //    //// validate                                                                                    Жена занимается сексом с 3 муужиками, тут
        //    if (model.Room != AppHistory.Plot && _context.AppHistorys.Any(x => x.Plot == model.Room))            // приходит муж. Жена достаёт черные мешки и пакует туда
        //        throw new AppException("Username '" + model.Room + "' is already taken");                    //любовников. 
        //    //                                                                                               Муж: что это за мешки?                                                                                                             
        //    //                                                                                               Жена: мама с дачи гостинцы привезла
        //    //                                                                                               Муж пнул один мешок и слышит "Беее"
        //    //                                                                                               Муж: О барашек, отлично жаркое сварим   
        //    //                                                                                               Пнул второй мешок и слышит "Хрюююю"   
        //    //                                                                                               Муж: свинья, отлично - сало замаринуем   
        //    //                                                                                               Пнул третий мешок - тишина, пнул ещё раз - тишина, пнул со всей силы
        //    // copy model to user and save                                                                   Мешок: ты чё блять тупой? картошка я!    
        //    User user = null;
        //    user = _context.Users.Where(user => user.Id == model.User).FirstOrDefault();

        //    AppHistory.Workshop = model.Build;
        //    AppHistory.Plot = model.Room;

        //    AppHistory.User = user;


        //    _context.AppHistorys.Update(AppHistory);
        //    _context.SaveChanges();
        //}

        public void Delete(int id)
        {
            var AppHistory = getAppHistory(id);
            _context.AppHistory.Remove(AppHistory);
            _context.SaveChanges();
        }

        // helper methods

        private AppHistory getAppHistory(int id)
        {
            var AppHistory = _context.AppHistory.FirstOrDefault(u => u.Id == id); // .FirstOrDefault(u => u.Id == id);
            AppHistory.maintenance = _context.Maintenances.FirstOrDefault(u => u.Id == AppHistory.Id);
            if (AppHistory == null) throw new KeyNotFoundException("Местоположение не найдено");
            return AppHistory;
        }
    }
}