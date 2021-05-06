using FavoDeMel.Application.DTO;
using FavoDeMel.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PusherServer;
using System;

namespace FavoDeMel.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ComandaController : ControllerBase
    {
        private readonly IComandaAppService _comandaApp;
        private readonly PusherOptions options;
        private readonly Pusher pusher;

        public ComandaController(IComandaAppService comandaApp)
        {
            options = new PusherOptions
            {
                Cluster = "us2",
                Encrypted = true
            };

            pusher = new Pusher(
              "1199386",
              "8fc6d8ccb5e28030c852",
              "0b13e9fc989a7d43c80b",
              options
            );

            _comandaApp = comandaApp;
        }

        // GET: api/Comanda
        [HttpGet]
        public IActionResult GetComandas()
        {
            try
            {
                var result = _comandaApp.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Comanda/5
        [HttpGet("{id}")]
        public IActionResult GetComanda(long id)
        {
            try
            {
                var result = _comandaApp.GetById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Comanda/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutComanda(ComandaDTO comandaDTO)
        {
            if (ModelState.IsValid)
            {
                _comandaApp.Update(comandaDTO);
                pusher.TriggerAsync("favo_de_mel", "comanda", new { message = "" });
                return Ok(comandaDTO);
            }

            return BadRequest(comandaDTO);
        }

        // PUT: api/Comanda/Finalizar/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Finalizar/{id}")]
        public IActionResult PutFinalizarComanda(long id)
        {
            if (ModelState.IsValid)
            {
                var comanda = _comandaApp.Finalizar(id);
                pusher.TriggerAsync("favo_de_mel", "comanda", new { message = "Comanda Nº " + id + " fechada" });
                return Ok(comanda);
            }

            return BadRequest(id);
        }

        // POST: api/Comanda
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostComanda(ComandaCreateDTO comandaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(comandaDTO);
            }
            _comandaApp.Add(comandaDTO);
            pusher.TriggerAsync("favo_de_mel", "comanda", new { message = "Foi criada uma nova comanda" });
            return Ok(comandaDTO);
        }

        // DELETE: api/Comanda/5
        [HttpDelete("{id}")]
        public IActionResult DeleteComanda(long id)
        {
            try
            {
                _comandaApp.Remove(id);
                pusher.TriggerAsync("favo_de_mel", "comanda", new { message = "" });
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
