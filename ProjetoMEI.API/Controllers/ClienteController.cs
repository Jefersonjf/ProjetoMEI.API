using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoMEI.API.Data;
using ProjetoMEI.API.Entities;

namespace ProjetoMEI.API.Controllers
{
    [Route("v1")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        [HttpGet]
        [Route("clientes")]
        public async Task<IActionResult> Get([FromServices] AppDbContext context)
        {
            var clientes = await context.Clientes.AsNoTracking().ToListAsync();
            return Ok(clientes);
        }


        [HttpGet]
        [Route("clientes/{CNPJ}")]
        public async Task<IActionResult> GetByCNPJ([FromServices] AppDbContext context, [FromRoute] int CNPJ)
        {
            var cliente = await context.Clientes.AsNoTracking().FirstOrDefaultAsync(x => x.CNPJ == CNPJ);
            return cliente == null
                ? NotFound()
                : Ok(cliente);
        }

        [HttpPost]
        [Route("clientes")]
        public async Task<IActionResult> Post([FromServices] AppDbContext context, Cliente model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var cliente = new Cliente
            {
                CNPJ = model.CNPJ,
                Name = model.Name
            };

            try
            {
                await context.Clientes.AddAsync(cliente);
                await context.SaveChangesAsync();
                return Created($"v1/clientes/{cliente.CNPJ}", cliente);
            }

            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        [HttpPut]
        [Route("clientes/{CNPJ}")]
        public async Task<IActionResult> Put([FromServices] AppDbContext context, [FromBody] Cliente model, [FromRoute] int CNPJ)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var cliente = await context.Clientes.FirstOrDefaultAsync(x => x.CNPJ == CNPJ);

            if (cliente == null)
                return NotFound();

            try
            {
                cliente.CNPJ = model.CNPJ;
                cliente.Name = model.Name;

                context.Clientes.Update(cliente);
                await context.SaveChangesAsync();
                return Ok(cliente);
            }

            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        [HttpDelete]
        [Route("clientes/{CNPJ}")]
        public async Task<IActionResult> Delete([FromServices] AppDbContext context, [FromRoute] int CNPJ)
        {
            var cliente = await context.Clientes.FirstOrDefaultAsync(x => x.CNPJ == CNPJ);

            try
            {
                context.Clientes.Remove(cliente);
                await context.SaveChangesAsync();

                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }

    }

}
