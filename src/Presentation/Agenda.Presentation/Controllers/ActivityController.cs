using Agenda.Domain.Entities;
using Agenda.Domain.Enuns.ActivityStatus;
using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Context;

namespace TrilhaApiDesafio.Controllers;

[ApiController]
[Route("[controller]")]
public class ActivityController : ControllerBase
{
    private readonly OrganizadorContext _context;

    public ActivityController(OrganizadorContext context)
    {
        _context = context;
    }

    [HttpGet("GetById/{id}")]
    public IActionResult GetById(long id)
    {
        // Activity: Buscar o Id no banco utilizando o EF
        // Activity: Validar o tipo de retorno. Se não encontrar a Activity, retornar NotFound,
        // caso contrário retornar OK com a Activity encontrada
        return Ok();
    }

    [HttpGet("GetAll")]
    public IActionResult GetAll()
    {
        // Activity: Buscar todas as Activitys no banco utilizando o EF
        return Ok();
    }

    [HttpGet("GetByTitle")]
    public IActionResult ObterPorTitulo(string titulo)
    {
        // Activity: Buscar  as Activitys no banco utilizando o EF, que contenha o titulo recebido por parâmetro
        // Dica: Usar como exemplo o endpoint ObterPorData
        return Ok();
    }

    [HttpGet("GetByDueDate")]
    public IActionResult ObterPorData(DateTime data)
    {
        var Activity = _context.Activitys.Where(x => x.DueDate.Date == data.Date);
        return Ok(Activity);
    }

    [HttpGet("GetByStatus")]
    public IActionResult ObterPorStatus(EnumActivityStatus status)
    {
        // Activity: Buscar  as Activitys no banco utilizando o EF, que contenha o status recebido por parâmetro
        // Dica: Usar como exemplo o endpoint ObterPorData
        var Activity = _context.Activitys.Where(x => x.Status == status);
        return Ok(Activity);
    }

    [HttpPost("Create")]
    public IActionResult Create(Activity activity)
    {
        if (activity.DueDate == DateTime.MinValue)
            return BadRequest(new { Erro = "A data da Activity não pode ser vazia" });

        // Activity: Adicionar a Activity recebida no EF e salvar as mudanças (save changes)
        //return CreatedAtAction(nameof(ObterPorId), new { id = Activity.Id }, Activity);
        return Ok();
    }

    [HttpPut("Update/{id}")]
    public IActionResult Update(long id, Activity Activity)
    {
        var ActivityBanco = _context.Activitys.Find(id);

        if (ActivityBanco == null)
            return NotFound();

        if (Activity.DueDate == DateTime.MinValue)
            return BadRequest(new { Erro = "A data da Activity não pode ser vazia" });

        // Activity: Atualizar as informações da variável ActivityBanco com a Activity recebida via parâmetro
        // Activity: Atualizar a variável ActivityBanco no EF e salvar as mudanças (save changes)
        return Ok();
    }

    [HttpDelete("Delete/{id}")]
    public IActionResult Delete(long id)
    {
        var ActivityBanco = _context.Activitys.Find(id);

        if (ActivityBanco == null)
            return NotFound();

        // Activity: Remover a Activity encontrada através do EF e salvar as mudanças (save changes)
        return NoContent();
    }
}