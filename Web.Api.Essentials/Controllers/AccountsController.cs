using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Api.Essentials.Data;
using Web.Api.Essentials.Data.Models;
using Web.Api.Essentials.Helpers;
using Web.Api.Essentials.ViewModels;

namespace Web.Api.Essentials.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public AccountsController(UserManager<ApplicationUser> userManager, IMapper mapper, ApplicationDbContext context)
        {
            _userManager = userManager;
            _mapper = mapper;
            _db = context;
        }

        // POST api/accounts
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdentity = _mapper.Map<ApplicationUser>(model);

            var result = await _userManager.CreateAsync(userIdentity, model.Password);

            if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

            await _db.SaveChangesAsync();

            return new OkObjectResult("Account created");
        }
    }
}
