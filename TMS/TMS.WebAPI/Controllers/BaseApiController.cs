using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TMS.WebAPI.Infrastructure;
using TMS.WebAPI.Models;

namespace TMS.WebAPI.Controllers
{
    public class BaseApiController : ApiController
    {
        private ModelFactory _ModelFactory;
        private ApplicationUserManager _AppUserManager = null;

        protected ApplicationUserManager AppUserManager {
            get {
                return _AppUserManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        public BaseApiController() {

        }

        protected ModelFactory TheModelFactory {
            get {
                if(_ModelFactory == null) {
                    _ModelFactory = new ModelFactory(this.Request, this.AppUserManager);
                }
                return _ModelFactory;
            }
        }

        protected IHttpActionResult GetErrorResult(IdentityResult result) {
            if(result == null) {
                return InternalServerError();
            }

            if (!result.Succeeded) {
                if(result.Errors != null) {
                    foreach(string error in result.Errors) {
                        ModelState.AddModelError("", error);
                    }
                }
                if (ModelState.IsValid) {
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }
            return null;
        }
    }
}
