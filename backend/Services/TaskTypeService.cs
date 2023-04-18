using AutoMapper;
using BCryptNet = BCrypt.Net.BCrypt;
using System.Collections.Generic;
using System.Linq;
using WebApi.Authorization;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.TaskType;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Services
{
    public interface ITaskTypeService
    {
        IEnumerable<TaskType> GetAll();                              // смотри строки 52 - 56
        TaskType GetById(int id);
        void Register(RegisterRequest model);
        void Update(int id, UpdateRequest model);
        void Delete(int id);
    }

    public class TaskTypeService : ITaskTypeService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;

        public TaskTypeService(
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
        //    var TaskType = _context.TaskTypes.SingleOrDefault(x => x.TaskTypename == model.TaskTypename);

        //    // validate
        //    if (TaskType == null || !BCryptNet.Verify(model.Password, TaskType.PasswordHash))
        //        throw new AppException("Имя пользователя или пароль введены неверно");

        //    // authentication successful
        //    var response = _mapper.Map<AuthenticateResponse>(TaskType);
        //    response.Token = _jwtUtils.GenerateToken(TaskType);
        //    return response;
        //}

        public IEnumerable<TaskType> GetAll()
        {
            return _context.TaskType.ToList();
        }

        public TaskType GetById(int id)
        {
            return getTaskType(id);
        }

        public void Register(RegisterRequest model)
        {
            // validate
            if (_context.TaskType.Any(x => x.Name == model.Name)) //  ТУТ СТОИТ ПОДУМАТЬ
                throw new AppException("Такой статус '" + model.Name + "' уже существует");
            
            // map model to new user object
            var TaskType = _mapper.Map<TaskType>(model);

            _context.TaskType.Add(TaskType);
            _context.SaveChanges();
        }

        public void Update(int id, UpdateRequest model)
        {
            var TaskType = getTaskType(id);

            //// validate                                                                                    Жена занимается сексом с 3 муужиками, тут
            if (model.Name != TaskType.Name && _context.TaskType.Any(x => x.Name == model.Name))              // приходит муж. Жена достаёт черные мешки и пакует туда
                throw new AppException("Такой статус'" + model.Name + "' is already taken");                    //любовников. 
            //                                                                                               Муж: что это за мешки?                                                                                                             
            //                                                                                               Жена: мама с дачи гостинцы привезла
            //                                                                                               Муж пнул один мешок и слышит "Беее"
            //                                                                                               Муж: О барашек, отлично жаркое сварим   
            //                                                                                               Пнул второй мешок и слышит "Хрюююю"   
            //                                                                                               Муж: свинья, отлично - сало замаринуем   
            //                                                                                               Пнул третий мешок - тишина, пнул ещё раз - тишина, пнул со всей силы
            // copy model to user and save                                                                   Мешок: ты чё блять тупой? картошка я!               
            _mapper.Map(model, TaskType);
            _context.TaskType.Update(TaskType);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var TaskType = getTaskType(id);
            _context.TaskType.Remove(TaskType);                      // опять же удаление записи, где есть ограничение на удаление я хз как делать
            _context.SaveChanges();
        }

        // helper methods

        private TaskType getTaskType(int id)
        {
            var TaskType = _context.TaskType.Find(id);
            if (TaskType == null) throw new KeyNotFoundException("Местоположение не найдено");
            return TaskType;
        }
    }
}