using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMS.WebAPI.Infrastructure.Repositories.Task {
    public class TaskRepository : ITaskRepository {

        public void Add(TaskItem task) {
            throw new NotImplementedException();
        }

        public TaskItem Find(string key) {
            throw new NotImplementedException();
        }

        public IEnumerable<TaskItem> GetAll() {
            throw new NotImplementedException();
        }

        public TaskItem Remove(string key) {
            throw new NotImplementedException();
        }

        public void Update(TaskItem item) {
            throw new NotImplementedException();
        }
    }
}