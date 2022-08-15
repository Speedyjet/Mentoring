using Mentoring.Models;

namespace Mentoring.BL
{
    public class UserService : IUserService
    {
        private readonly NorthwindContext _context;
        private readonly ILogger<CategoryService> _logger;

        public UserService(NorthwindContext context, ILogger<CategoryService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public IList<string> GetUsers()
        {
            _logger.LogInformation("Getting users list");
            return _context.Users.Select(x => x.UserName).ToList();
        }
    }
}
