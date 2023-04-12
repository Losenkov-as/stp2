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
        //void Update(int id, UpdateRequest model);
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

            if (model.User != null)
                    user = _context.Users.Where(user => user.Id == int.Parse(model.User)).FirstOrDefault();

            if (model.Location != null)
                    location = _context.Locations.Where(location => location.Id == int.Parse(model.Location)).FirstOrDefault();

            if (model.Status != null)
                    status = _context.Statuses.Where(status => status.Id == int.Parse(model.Status)).FirstOrDefault();
            
            if (model.Machine != null)
                    machine = _context.Machines.Where(machine => machine.Id == int.Parse(model.Machine)).FirstOrDefault();

            Maintenance maintenance = new Maintenance
            {
                DateOfUpdate = DateTime.Now,
                Status = status,
                Machine = machine,
                Location = location,
                User = user
            };

            // save Maintenance
            _context.Maintenances.Add(maintenance);
            _context.SaveChanges();
        }

        //public void Update(string id, UpdateRequest model)
        //{
        //    var maintenance = getMaintenance(id);

        //    // validate
        //    if (model.Maintenancename != maintenance.Maintenancename && _context.Maintenances.Any(x => x.Maintenancename == model.Maintenancename))
        //        throw new AppException("Maintenancename '" + model.Maintenancename + "' is already taken");

        //    // hash password if it was entered
        //    if (!string.IsNullOrEmpty(model.Password))
        //        maintenance.PasswordHash = BCryptNet.HashPassword(model.Password);

        //    // copy model to Maintenance and save
        //    _mapper.Map(model, maintenance);
        //    _context.Maintenances.Update(maintenance);
        //    _context.SaveChanges();
        //}

        public void Delete(int id)
        {
            var maintenance = getMaintenance(id);                     // как правильно удалять записи с внешними ключами? сейчас на этой функции 
            _context.Maintenances.Remove(maintenance);                // исключение из-за внешнего ключа
            _context.SaveChanges();                                           

        }

        // helper methods

        private Maintenance getMaintenance(int id)
        {
            var maintenance = _context.Maintenances.Find(id);
            if (maintenance == null) throw new KeyNotFoundException("Maintenance not found");
            return maintenance;
        }
    }
}