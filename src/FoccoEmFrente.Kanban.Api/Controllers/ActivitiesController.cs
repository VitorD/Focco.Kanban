using FoccoEmFrente.Kanban.Api.Controllers.Attributes;
using FoccoEmFrente.Kanban.Application.Entities;
using FoccoEmFrente.Kanban.Application.Repositories;
using FoccoEmFrente.Kanban.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoccoEmFrente.Kanban.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ValidateModelState]
    [Authorize]
    public class ActivitiesController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;


        private readonly IActivityService _activityService;

        public ActivitiesController(IActivityService activityService, UserManager<IdentityUser> userManager)
        {
            _activityService = activityService;
            _userManager = userManager;
        }

        protected Guid UserId => Guid.Parse(_userManager.GetUserId(User));

        [HttpGet]
        public async Task<IActionResult>  Listar()
        {
            
            var atividades = await _activityService.GetAllAsync(UserId);
            _activityService.Dispose();
            return Ok(atividades);
        
        }

        [HttpPost]
        public async Task<IActionResult> Inserir(Activity activity) {
            
            activity.UserId = UserId;

            var newActivity = await _activityService.AddAsync(activity);
            return Ok(newActivity);
        
        }

        [HttpPut]
        public async Task<IActionResult> Alterar(Activity activity)
        {

            activity.UserId = UserId;

            var result = await _activityService.UpdateAsync(activity);
            return Ok(result);

        }

        [HttpDelete]
        public async Task<IActionResult> Deletar(Activity activity)
        {
            activity.UserId = UserId;

            var newActivity = await _activityService.RemoveAsync(activity);
            return Ok(newActivity);

        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarById(Guid id)
        {
            

            var newActivity = await _activityService.RemoveAsync(id,UserId);
            return Ok(newActivity);

        }



        [HttpGet("{Id}")]
        public async Task<IActionResult> SelecionarAsync(Guid Id)
        {
            var newActivity = await _activityService.GetByIdAsync(Id,UserId);
            if (newActivity == null)
                return NotFound();
            return Ok(newActivity);

        }

        [HttpPut("{Id}/todo")]
        public async Task<IActionResult> AtualizarStatusParaTodo(Guid Id)
        {

            var activity = await _activityService.UpdateToTodoAsync(Id, UserId);
            return Ok(activity);
        }

        [HttpPut("{Id}/doing")]
        public async Task<IActionResult> AtualizarStatusParaDoing(Guid Id)
        {

            var activity = await _activityService.UpdateToDoingAsync(Id, UserId);
            return Ok(activity);
        }

        [HttpPut("{Id}/done")]
        public async Task<IActionResult> AtualizarStatusParaDone(Guid Id)
        {

            var activity = await _activityService.UpdateToDoneAsync(Id, UserId);
            return Ok(activity);
        }


    }

}
