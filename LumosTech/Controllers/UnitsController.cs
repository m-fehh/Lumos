using AutoMapper;
using Lumos.Application;
using Lumos.Application.Dtos.Management;
using Lumos.Data.Models.Management;

namespace Lumos.Mvc.Controllers
{
    public class UnitsController : LumosControllerBase<Units, UnitsDto, long>
    {

        public UnitsController(LumosSession session, IMapper mapper, LumosAppServiceBase<Units> Unitservice) : base(session, mapper, Unitservice) { }
    }
}
