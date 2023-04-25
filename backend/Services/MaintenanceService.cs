using AutoMapper;
using BCryptNet = BCrypt.Net.BCrypt;
using System.Collections.Generic;
using System.Linq;
using WebApi.Authorization;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Maintenance;
using Microsoft.EntityFrameworkCore;
using System;

namespace WebApi.Services
{
    public interface IMaintenanceService
    {
        IEnumerable<Maintenance> GetAll();
        Maintenance GetById(int id);
        void Register(RegisterRequest model);
        void Update(int id, UpdateRequest model);
        void Delete(int id);
    }

    public class MaintenanceService : IMaintenanceService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;
        
        public MaintenanceService(
            DataContext context,
            IJwtUtils jwtUtils,
            IMapper mapper)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }
        public IEnumerable<Maintenance> GetAll()
        {
            return _context.Maintenances.Include(u => u.User)
                .Include(l => l.Location)
                .Include(m => m.Machine)
                .Include(q => q.TaskType)
                .Include(s => s.Status); 
        }

        public Maintenance GetById(int id)
        {
            return getMaintenance(id);
        }

        public void Register(RegisterRequest model)
        {
            // validate
            // хуй знает надо ли if (_context.Maintenances.Any(x => x.Id == model.Maintenancename))
            //    throw new AppException("Имя пользователя '" + model.Maintenancename + "' уже существует");

            //// map model to new Maintenance object

            //var Maintenance = _mapper.Map<Maintenance>(model);

            //// hash password
            //Maintenance.PasswordHash = BCryptNet.HashPassword(model.Password);

            //// save Maintenance
            //_context.Maintenances.Add(Maintenance);
            //_context.SaveChanges();

            User user = null;
            Location location = null;
            Machine machine = null;
            Status status = null;
            TaskType tasktype = null;

            if (model.User != null)
                user = _context.Users.Where(user => user.Id == int.Parse(model.User)).FirstOrDefault();

            if (model.Location != null)
                location = _context.Locations.Where(location => location.Id == int.Parse(model.Location)).FirstOrDefault();

            if (model.Status != null)
                status = _context.Statuses.Where(status => status.Id == int.Parse(model.Status)).FirstOrDefault();

            if (model.Machine != null)
                machine = _context.Machines.Where(machine => machine.Id == int.Parse(model.Machine)).FirstOrDefault();
            if (model.TaskType != null)
                tasktype = _context.TaskType.Where(tasktype => tasktype.Id == int.Parse(model.TaskType)).FirstOrDefault();

            Maintenance maintenance = new Maintenance
            {
                DateOfUpdate = DateTime.Now,
                Status = status,
                Machine = machine,
                Location = location,
                User = user,
                TaskType = tasktype,
                Comment = model.Comment
            };
            AppHistory apphistory = new AppHistory { 
                Maintenance = maintenance,
                DateOfStart = DateTime.Now,
                Status = status.Name,
                Machine = machine.InventoryNumber,
                Location = location.Plot,
                Author = user.Username,
                TaskType = tasktype.Name,
                CommentOfAuthor = model.Comment
            };
            IEnumerable<AppHistory> arr = _context.AppHistories;
            if (_context.AppHistories.Count() == 0) {
                apphistory.Id = 1;
            }
            else { 
                 apphistory.Id = arr.OrderBy(id => apphistory.Id).Last().Id + 1; 
            }
            _context.Maintenances.Add(maintenance);
            _context.AppHistories.Add(apphistory);
            _context.SaveChanges();
        }

        public void Update(int id, UpdateRequest model)
        {
            var maintenance = getMaintenance(id);

            // validate
            //if (model.Maintenancename != maintenance.Maintenancename && _context.Maintenances.Any(x => x.Maintenancename == model.Maintenancename))
            //    throw new AppException("Maintenancename '" + model.Maintenancename + "' is already taken");



            User user = null;
            user = _context.Users.Where(user => user.Id == int.Parse(model.User)).FirstOrDefault();

            Status status = new Status
            {
                Id = 2,
                Name = "В работе"
            };

            AppHistory apphistory = new AppHistory
            {
                Maintenance = maintenance,
                Machine = maintenance.Machine.InventoryNumber,
                Author = maintenance.User.Username, //тут из-за порядка кода может возникнуть залупа, хотя пока норм
                CommentOfAuthor = maintenance.Comment,
                Location = maintenance.Location.Plot,
                DateOfCreate = maintenance.DateOfUpdate,
                CommentOfDispatcher = model.Comment,
                DateOfEnd = model.DateOfEnd,
                TaskType = maintenance.TaskType.Name,
                Executor = user.Username,
                Status = "В работе"
            };
            //maintenance.Status = status;
            maintenance.User = user;
            maintenance.DateOfUpdate = model.DateOfEnd;
            maintenance.Comment = model.Comment;


            IEnumerable<AppHistory> arr = _context.AppHistories;
            if (_context.AppHistories.Count() == 0)
            {
                apphistory.Id = 1;
            }
            else
            {
                apphistory.Id = arr.OrderBy(id => apphistory.Id).Last().Id + 1;
            }
            //AppHistory apphistory1 = new AppHistory();
            //apphistory1 = _context.AppHistory.FirstOrDefault(u => u.Id == id);
            _context.Maintenances.Update(maintenance);
            _context.AppHistories.Add(apphistory);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var maintenance = getMaintenance(id);                     // как правильно удалять записи с внешними ключами? сейчас на этой функции 
            _context.Maintenances.Remove(maintenance);                // исключение из-за внешнего ключа
            _context.SaveChanges();                                           

        }

        // helper methods

        private Maintenance getMaintenance(int id)
        {
            //var maintenance = _context.Maintenances.Find(id);
            //var maintenance = _context.Maintenances.FirstOrDefault(u => u.Id == id); // .FirstOrDefault(u => u.Id == id);

            //maintenance.User = _context.Users.Include(ur => ur.Roles).FirstOrDefault(u => u.Id == maintenance.User.Id);
            //maintenance.Machine = _context.Machines.Include(b => b.MachineModel).ThenInclude(a => a.MachineType).
            //                Include(c => c.Room).ThenInclude(q => q.Build).FirstOrDefault(u => u.Id == maintenance.Machine.Id);
            //maintenance.Location = _context.Locations.Include(l => l.User).FirstOrDefault(p => p.Id == maintenance.Location.Id);
            //maintenance.TaskType = _context.TaskType.FirstOrDefault(t => t.Id == t.Id);
            //maintenance.Status = _context.Statuses.FirstOrDefault(f => f.Id == f.Id);
            //if (maintenance == null) throw new KeyNotFoundException("Maintenance not found");

            var maintenance = _context.Maintenances.FirstOrDefault(u => u.Id == id); // .FirstOrDefault(u => u.Id == id);
            
            maintenance.User = _context.Users.FirstOrDefault(u => u.Id == maintenance.userId);
            maintenance.Location = _context.Locations.FirstOrDefault(u => u.Id == maintenance.locationId);
            maintenance.Machine = _context.Machines.FirstOrDefault(u => u.Id == maintenance.machineId);
            maintenance.Status = _context.Statuses.FirstOrDefault(u => u.Id == maintenance.statusId);
            maintenance.TaskType = _context.TaskType.FirstOrDefault(u => u.Id == maintenance.tasktypeId);

            if (maintenance == null) throw new KeyNotFoundException("Местоположение не найдено");
            return maintenance;
        }
    }
}