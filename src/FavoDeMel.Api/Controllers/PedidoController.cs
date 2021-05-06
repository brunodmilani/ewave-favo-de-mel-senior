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
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoAppService _pedidoApp;
        private readonly PusherOptions options;
        private readonly Pusher pusher;

        public PedidoController(IPedidoAppService pedidoApp)
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

            _pedidoApp = pedidoApp;
        }

        // GET: api/Pedido
        [HttpGet]
        public IActionResult GetPedidos()
        {
            try
            {
                var result = _pedidoApp.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Pedido/5
        [HttpGet("{id}")]
        public IActionResult GetPedido(long id)
        {
            try
            {
                var result = _pedidoApp.GetById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Pedido/5
        [HttpGet("Comanda/{comandaId}")]
        public IActionResult GetPedidosByComanda(long comandaId)
        {
            try
            {
                var result = _pedidoApp.GetAllByComanda(comandaId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Pedido
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public IActionResult PutPedido(PedidoDTO pedidoDTO)
        {
            if (ModelState.IsValid)
            {
                _pedidoApp.Update(pedidoDTO);
                pusher.TriggerAsync("favo_de_mel", "pedido", new { message = "" });
                if (pedidoDTO.Status == (int)Status.Finalizado)
                    pusher.TriggerAsync("favo_de_mel", "comanda", new { message = "Pedido Finalizado (Comanda Nº " + pedidoDTO.ComandaId + ")" });
                if (pedidoDTO.Status == (int)Status.Cancelado)
                    pusher.TriggerAsync("favo_de_mel", "comanda", new { message = "Pedido Cancelado (Comanda Nº " + pedidoDTO.ComandaId + ")" });

                return Ok(pedidoDTO);
            }

            return BadRequest(pedidoDTO);
        }

        // POST: api/Pedido
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostPedido(PedidoCreateDTO pedidoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(pedidoDTO);
            }
            _pedidoApp.Add(pedidoDTO);
            pusher.TriggerAsync("favo_de_mel", "pedido", new { message = "" });
            pusher.TriggerAsync("favo_de_mel", "comanda", new { message = "Novo Pedido (Comanda Nº " + pedidoDTO.ComandaId + ")" });
            return Ok(pedidoDTO);
        }

        // DELETE: api/Pedido/5
        [HttpDelete("{id}")]
        public IActionResult DeletePedido(long id)
        {
            try
            {
                _pedidoApp.Remove(id);
                pusher.TriggerAsync("favo_de_mel", "pedido", new { message = "" });
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
