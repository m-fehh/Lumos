using AutoMapper;
using Lumos.Data.Models.Management;
using Microsoft.AspNetCore.Mvc;

namespace Lumos.Mvc
{
    public class LumosControllerBase : Controller
    {
        private readonly LumosSession _session;
        protected readonly IMapper _mapper;

        public LumosControllerBase(LumosSession session, IMapper mapper)
        {
            _session = session;
            _mapper = mapper;
        }

        protected void SetViewBagValues()
        {
            ViewBag.UserName = _session.UserName?.ToString().ToUpper();
        }
    }
}
