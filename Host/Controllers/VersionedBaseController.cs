using Host.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    public class VersionedBaseController : BaseController
    {
    }


}
