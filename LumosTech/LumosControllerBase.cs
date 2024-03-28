using AutoMapper;
using Lumos.Data.Models.Management;
using Microsoft.AspNetCore.Mvc;

namespace Lumos.Mvc
{
    public class LumosControllerBase : Controller
    {
        protected readonly LumosSession _session;
        protected readonly IMapper _mapper;

        public LumosControllerBase(LumosSession session, IMapper mapper)
        {
            _session = session;
            _mapper = mapper;
        }
    }
}
