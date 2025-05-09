﻿using CONSUMOS_ENERGETICOS_CS_REST_NoSQL_API.Exceptions;
using CONSUMOS_ENERGETICOS_CS_REST_NoSQL_API.Models;
using CONSUMOS_ENERGETICOS_CS_REST_NoSQL_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CONSUMOS_ENERGETICOS_CS_REST_NoSQL_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiciosController(ServicioService servicioService) : Controller
    {
        private readonly ServicioService _servicioService = servicioService;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var losServicios = await _servicioService
                .GetAllAsync();

            return Ok(losServicios);
        }

        [HttpGet("{servicio_id:length(24)}")]
        public async Task<IActionResult> GetByIdAsync(string servicio_id)
        {
            try
            {
                var unServicio = await _servicioService
                    .GetByIdAsync(servicio_id);

                return Ok(unServicio);
            }
            catch (AppValidationException error)
            {
                return NotFound(error.Message);
            }
        }

        [HttpGet("{servicio_id:length(24)}/Componentes")]
        public async Task<IActionResult> GetAssociatedComponentsAsync(string servicio_id)
        {
            try
            {
                var losComponentesAsociados = await _servicioService
                    .GetAssociatedComponentsAsync(servicio_id);

                return Ok(losComponentesAsociados);
            }
            catch (AppValidationException error)
            {
                return NotFound(error.Message);
            }
        }

        [HttpGet("{servicio_id:length(24)}/Consumos")]
        public async Task<IActionResult> GetAssociatedConsumptionAsync(string servicio_id)
        {
            try
            {
                var losConsumosAsociados = await _servicioService
                    .GetAssociatedConsumptionAsync(servicio_id);

                return Ok(losConsumosAsociados);
            }
            catch (AppValidationException error)
            {
                return NotFound(error.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Servicio unServicio)
        {
            try
            {
                var servicioCreado = await _servicioService
                    .CreateAsync(unServicio);

                return Ok(servicioCreado);
            }
            catch (AppValidationException error)
            {
                return BadRequest($"Error de validación: {error.Message}");
            }
            catch (DbOperationException error)
            {
                return BadRequest($"Error de operacion en DB: {error.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(Servicio unServicio)
        {
            try
            {
                var servicioActualizado = await _servicioService
                    .UpdateAsync(unServicio);

                return Ok(servicioActualizado);
            }
            catch (AppValidationException error)
            {
                return BadRequest($"Error de validación: {error.Message}");
            }
            catch (DbOperationException error)
            {
                return BadRequest($"Error de operacion en DB: {error.Message}");
            }
        }

        [HttpDelete("{servicio_id:length(24)}")]
        public async Task<IActionResult> RemoveAsync(string servicio_id)
        {
            try
            {
                var nombreServicioBorrado = await _servicioService
                    .RemoveAsync(servicio_id);

                return Ok($"El servicio {nombreServicioBorrado} fue eliminado correctamente!");
            }
            catch (AppValidationException error)
            {
                return BadRequest($"Error de validación: {error.Message}");
            }
            catch (DbOperationException error)
            {
                return BadRequest($"Error de operacion en DB: {error.Message}");
            }
        }
    }
}
