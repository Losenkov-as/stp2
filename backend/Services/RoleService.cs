using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using WebApi.Authorization;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
    public interface IRoleService
    {
        IEnumerable<Role> GetAll();
        Role GetById(int id);
        void Create(Role model);
        void Update(int id, Role model);
        void Delete(int id);
    }

    public class RoleService : IRoleService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;

        public RoleService(
            DataContext context,
            IJwtUtils jwtUtils,
            IMapper mapper)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }


        public IEnumerable<Role> GetAll()
        {

            return _context.Roles.ToList();
        }

        public Role GetById(int id)
        {
            return getRole(id);
        }

        public void Create(Role model)
        {
            // validate
            if (_context.Roles.Any(x => x.Name == model.Name))
                throw new AppException("Наименование '" + model.Name + "' уже существует");

            // map model to new Role object
            var Role = _mapper.Map<Role>(model);


            // save Role
            _context.Roles.Add(Role);
            _context.SaveChanges();
        }

        public void Update(int id, Role model)
        {
            var Role = getRole(id);

            // validate
            if (_context.Roles.Any(x => x.Name == model.Name))
                throw new AppException("Наименование '" + model.Name + "' уже существует");


            // copy model to Role and save
            _mapper.Map(model, Role);
            _context.Roles.Update(Role);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var Role = getRole(id);
            _context.Roles.Remove(Role);
            _context.SaveChanges();
        }

        // helper methods

        private Role getRole(int id)
        {
            var Role = _context.Roles.Find(id);
            if (Role == null) throw new KeyNotFoundException("Role not found");
            return Role;
        }
    }
}