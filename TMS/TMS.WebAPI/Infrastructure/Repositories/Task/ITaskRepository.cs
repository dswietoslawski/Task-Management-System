using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.WebAPI.Infrastructure.Repositories.Task {
    interface ITaskRepository {
        void Add(TaskItem task);
        IEnumerable<TaskItem> GetAll();
        TaskItem Find(string key);
        TaskItem Remove(string key);
        void Update(TaskItem item);
    }
}
