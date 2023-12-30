using AutoMapper;
using IMobi.School.BAL.Base;
using IMobi.School.DAL.Context;
using IMobi.School.DomainModal.v1;
using IMobi.School.ServiceModal.v1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMobi.School.BAL
{
    public class StudentProcess : BaseProcess
    {
        private readonly IRepository<StudentDM> _studentRepository;
        public StudentProcess(AppDbContext appDbContext, IMapper mapper,
            IRepository<StudentDM> studentRepository) : base(appDbContext, mapper)
        {
            _studentRepository = studentRepository;
        }

        public async Task<StudentSM?> GetStudents()
        {
            var students = await _studentRepository.GetAllAsync();
            if(students != null)
            {
                return _mapper.Map<StudentSM>(students);
            }
            return null;
        }

        public async Task<StudentSM> AddStudent(StudentSM studentSM)
        {
           return _mapper.Map<StudentSM>(
               await _studentRepository.AddAsync(
                   _mapper.Map<StudentDM>(studentSM)));
        }

        public async Task<StudentSM>? GetStudentById(int id)
        {
            return _mapper.Map<StudentSM>(await _studentRepository.GetByIdAsync(id));
        }
    }
}
