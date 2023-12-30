using AutoMapper;
using IMobi.School.DAL.Context;

namespace IMobi.School.BAL.Base
{
    public class BaseProcess
    {
        protected readonly AppDbContext _appDbContext;
        protected readonly IMapper _mapper;
        public BaseProcess(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
    }
}
