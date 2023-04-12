using AutoMapper;
using BCryptNet = BCrypt.Net.BCrypt;
using System.Collections.Generic;
using System.Linq;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.MachineModel;
using Microsoft.EntityFrameworkCore;
using WebApi.Authorization;

namespace WebApi.Services
{
    public interface IMachineModelService
    {
        IEnumerable<MachineModel> GetAll();                              // ������ ������ 52 - 56
        MachineModel GetById(int id);
        void Register(RegisterRequest model);
        void Update(int id, UpdateRequest model);
        void Delete(int id);
    }

    public class MachineModelService : IMachineModelService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;

        public MachineModelService(
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
        //    var MachineModel = _context.MachineModels.SingleOrDefault(x => x.MachineModelname == model.MachineModelname);

        //    // validate
        //    if (MachineModel == null || !BCryptNet.Verify(model.Password, MachineModel.PasswordHash))
        //        throw new AppException("��� ������������ ��� ������ ������� �������");

        //    // authentication successful
        //    var response = _mapper.Map<AuthenticateResponse>(MachineModel);
        //    response.Token = _jwtUtils.GenerateToken(MachineModel);
        //    return response;
        //}

        public IEnumerable<MachineModel> GetAll()
        {
            return _context.MachineModels.Include(l => l.MachineType);
        }

        public MachineModel GetById(int id)
        {
            return getMachineModel(id);
        }

        public void Register(RegisterRequest model)
        {
            // validate
            if (_context.MachineModels.Any(x => x.Name == model.Name)) //  ��� ����� ��������
                throw new AppException("����� ������ '" + model.Name + "' ��� ����������");

            MachineType machinetype = null;

            machinetype = _context.MachineTypes.Where(machinetype => machinetype.Id == model.machinetype).FirstOrDefault();


            // map model to new user object
            //var user = _mapper.Map<User>(model);
            MachineModel machinemodel = new MachineModel
            {
                Name = model.Name,
                MachineType = machinetype
            };

            _context.MachineModels.Add(machinemodel);
            _context.SaveChanges();
        }

        public void Update(int id, UpdateRequest model)
        {
            var MachineModel = getMachineModel(id);

            //// validate                                                                                    ���� ���������� ������ � 3 ���������, ���
            if (model.Name != MachineModel.Name && _context.MachineModels.Any(x => x.Name == model.Name))              // �������� ���. ���� ������ ������ ����� � ������ ����
                throw new AppException("����� ������'" + model.Name + "' is already taken");                    //����������. 
            //                                                                                               ���: ��� ��� �� �����?                                                                                                             
            //                                                                                               ����: ���� � ���� �������� ��������
            //                                                                                               ��� ���� ���� ����� � ������ "����"
            //                                                                                               ���: � �������, ������� ������ ������   
            //                                                                                               ���� ������ ����� � ������ "������"   
            //                                                                                               ���: ������, ������� - ���� ����������   
            //                                                                                               ���� ������ ����� - ������, ���� ��� ��� - ������, ���� �� ���� ����
            // copy model to user and save                                                                   �����: �� �� ����� �����? �������� �!               
            _mapper.Map(model, MachineModel);
            _context.MachineModels.Update(MachineModel);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var MachineModel = getMachineModel(id);
            _context.MachineModels.Remove(MachineModel);                      // ����� �� �������� ������, ��� ���� ����������� �� �������� � �� ��� ������
            _context.SaveChanges();
        }

        // helper methods

        private MachineModel getMachineModel(int id)
        {
            var MachineModel = _context.MachineModels.Find(id);
            if (MachineModel == null) throw new KeyNotFoundException("�������������� �� �������");
            return MachineModel;
        }
    }
}