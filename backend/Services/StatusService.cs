using AutoMapper;
using BCryptNet = BCrypt.Net.BCrypt;
using System.Collections.Generic;
using System.Linq;
using WebApi.Authorization;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Statuses;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Services
{
    public interface IStatusService
    {
        IEnumerable<Status> GetAll();                              // ������ ������ 52 - 56
        Status GetById(int id);
        void Register(RegisterRequest model);
        void Update(int id, UpdateRequest model);
        void Delete(int id);
    }

    public class StatusService : IStatusService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;

        public StatusService(
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
        //    var Status = _context.Statuss.SingleOrDefault(x => x.Statusname == model.Statusname);

        //    // validate
        //    if (Status == null || !BCryptNet.Verify(model.Password, Status.PasswordHash))
        //        throw new AppException("��� ������������ ��� ������ ������� �������");

        //    // authentication successful
        //    var response = _mapper.Map<AuthenticateResponse>(Status);
        //    response.Token = _jwtUtils.GenerateToken(Status);
        //    return response;
        //}

        public IEnumerable<Status> GetAll()
        {
            return _context.Statuses.ToList();
        }

        public Status GetById(int id)
        {
            return getStatus(id);
        }

        public void Register(RegisterRequest model)
        {
            // validate
            if (_context.Statuses.Any(x => x.Name == model.Name)) //  ��� ����� ��������
                throw new AppException("����� ������ '" + model.Name + "' ��� ����������");
            
            // map model to new user object
            var status = _mapper.Map<Status>(model);

            _context.Statuses.Add(status);
            _context.SaveChanges();
        }

        public void Update(int id, UpdateRequest model)
        {
            var status = getStatus(id);

            //// validate                                                                                    ���� ���������� ������ � 3 ���������, ���
            if (model.Name != status.Name && _context.Statuses.Any(x => x.Name == model.Name))              // �������� ���. ���� ������ ������ ����� � ������ ����
                throw new AppException("����� ������'" + model.Name + "' is already taken");                    //����������. 
            //                                                                                               ���: ��� ��� �� �����?                                                                                                             
            //                                                                                               ����: ���� � ���� �������� ��������
            //                                                                                               ��� ���� ���� ����� � ������ "����"
            //                                                                                               ���: � �������, ������� ������ ������   
            //                                                                                               ���� ������ ����� � ������ "������"   
            //                                                                                               ���: ������, ������� - ���� ����������   
            //                                                                                               ���� ������ ����� - ������, ���� ��� ��� - ������, ���� �� ���� ����
            // copy model to user and save                                                                   �����: �� �� ����� �����? �������� �!               
            _mapper.Map(model, status);
            _context.Statuses.Update(status);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var status = getStatus(id);
            _context.Statuses.Remove(status);                      // ����� �� �������� ������, ��� ���� ����������� �� �������� � �� ��� ������
            _context.SaveChanges();
        }

        // helper methods

        private Status getStatus(int id)
        {
            var status = _context.Statuses.Find(id);
            if (status == null) throw new KeyNotFoundException("�������������� �� �������");
            return status;
        }
    }
}