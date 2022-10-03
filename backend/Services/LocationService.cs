using AutoMapper;
using BCryptNet = BCrypt.Net.BCrypt;
using System.Collections.Generic;
using System.Linq;
using WebApi.Authorization;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Locations;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Services
{
    public interface ILocationService
    {
        IEnumerable<Location> GetAll();                              // ������ ������ 52 - 56
        Location GetById(int id);
        void Register(RegisterRequest model);
        void Update(int id, UpdateRequest model);
        void Delete(int id);
    }

    public class LocationService : ILocationService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;

        public LocationService(
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
        //    var Location = _context.Locations.SingleOrDefault(x => x.Locationname == model.Locationname);

        //    // validate
        //    if (Location == null || !BCryptNet.Verify(model.Password, Location.PasswordHash))
        //        throw new AppException("��� ������������ ��� ������ ������� �������");

        //    // authentication successful
        //    var response = _mapper.Map<AuthenticateResponse>(Location);
        //    response.Token = _jwtUtils.GenerateToken(Location);
        //    return response;
        //}

        public IEnumerable<Location> GetAll()
        {
            return _context.Locations.Include(l => l.user);
        }

        public Location GetById(int id)
        {
            return getLocation(id);
        }

        public void Register(RegisterRequest model)
        {
            // validate
            if ((_context.Locations.Any(x => x.Room == model.Room)) || (_context.Locations.Any(x => x.Build == model.Build))) //  ��� ����� ��������
                throw new AppException("��������� '" + model.Room + "' ��� ����������");
            
            // map model to new user object

            User user = null;

            user = _context.Users.Where(user => user.Id == model.User).FirstOrDefault();


            // map model to new user object
            //var user = _mapper.Map<User>(model);
            Location location = new Location
            {
                Room = model.Room,
                Build = model.Build,
                user = user
            };

            _context.Locations.Add(location);
            _context.SaveChanges();
        }

        public void Update(int id, UpdateRequest model)
        {
            var location = getLocation(id);

            //// validate                                                                                    ���� ���������� ������ � 3 ���������, ���
            if (model.Room != location.Room && _context.Locations.Any(x => x.Room == model.Room))            // �������� ���. ���� ������ ������ ����� � ������ ����
                throw new AppException("Username '" + model.Room + "' is already taken");                    //����������. 
            //                                                                                               ���: ��� ��� �� �����?                                                                                                             
            //                                                                                               ����: ���� � ���� �������� ��������
            //                                                                                               ��� ���� ���� ����� � ������ "����"
            //                                                                                               ���: � �������, ������� ������ ������   
            //                                                                                               ���� ������ ����� � ������ "������"   
            //                                                                                               ���: ������, ������� - ���� ����������   
            //                                                                                               ���� ������ ����� - ������, ���� ��� ��� - ������, ���� �� ���� ����
            // copy model to user and save                                                                   �����: �� �� ����� �����? �������� �!               
            _mapper.Map(model, location);
            _context.Locations.Update(location);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var location = getLocation(id);
            _context.Locations.Remove(location);
            _context.SaveChanges();
        }

        // helper methods

        private Location getLocation(int id)
        {
            var location = _context.Locations.Find(id);
            if (location == null) throw new KeyNotFoundException("�������������� �� �������");
            return location;
        }
    }
}