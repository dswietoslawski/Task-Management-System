using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using TMS.API.Models;

namespace TMS.API.Infrastructure
{
    public class TmsInitializer
    {
        private readonly TmsContext _ctx;
        private readonly UserManager<TmsUser> _userMgr;

        public TmsInitializer(TmsContext ctx, UserManager<TmsUser> userMgr )
        {
            _ctx = ctx;
            _userMgr = userMgr;
        }

        public async Task SeedAsync() {
            if (await _userMgr.FindByNameAsync("dswietoslawski") == null) {
                var user = new TmsUser() {
                    Email = "dswietoslawski@gmail.com",
                    UserName = "dswietoslawski",
                    EmailConfirmed = true
                };

                var result = await _userMgr.CreateAsync(user, "Passw0rd!");
                if (!result.Succeeded) throw new InvalidProgramException("Failed to create seed user");
            }
            //_ctx.Tasks.Add(new UserTask() {
            //    Name = "What"
            //});
            _ctx.SaveChanges();
        }
    }
}
