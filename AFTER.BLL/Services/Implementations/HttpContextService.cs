using AFTER.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace AFTER.BLL.Services.Implementations
{
    public class HttpContextService : IHttpContextService
    {
        private readonly IHttpContextAccessor _context;

        public HttpContextService(IHttpContextAccessor context)
        {
            _context = context;
        }

        public int? GetUserId()
        {
            var idClaim = _context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id")?.Value;
            return Int32.TryParse(idClaim, out int ret) ? ret : (int?)null;
        }
    }
}
