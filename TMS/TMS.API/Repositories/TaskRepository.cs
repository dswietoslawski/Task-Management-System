using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TMS.API.Infrastructure;
using TMS.API.ViewModels;

namespace TMS.API.Repositories {
    internal class TaskRepository : ITaskRepository {
        private TmsContext _context;

        public TaskRepository(TmsContext context) {
            _context = context;
        }

        public ICollection<UserTaskViewModel> Get() {
            return Mapper.Map<ICollection<UserTaskViewModel>>(_context.Tasks);
        }
    }
}