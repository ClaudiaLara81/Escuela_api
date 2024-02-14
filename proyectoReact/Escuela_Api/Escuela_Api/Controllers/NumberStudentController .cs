using AutoMapper;
using Escuela_Api.Datos;
using Escuela_Api.Models;
using Escuela_Api.Models.Dto;
using Escuela_Api.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Escuela_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumberStudentController : ControllerBase
    {
        private readonly ILogger<NumberStudentController> _logger;
        private readonly IStudentRepository _studentRepo;
        private readonly INumberStudentRepository _numberstudentRepo;
        private readonly IMapper _mapping;
        protected ApiResponse _response;

        public NumberStudentController(ILogger<NumberStudentController> logger, IStudentRepository studentRepo,INumberStudentRepository numberStudentRepo, IMapper mapping)
        {
            _logger = logger;
            _studentRepo = studentRepo;
            _numberstudentRepo = numberStudentRepo;
            _mapping = mapping;
            _response = new();

        }
        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetNumberStudents()
        {
            try
            {
                _logger.LogInformation("Obtener numero de Estudiantes");

                IEnumerable<NumberStudent> numberStudentList = await _numberstudentRepo.ObtainAll();
                
                _response.Result = _mapping.Map<IEnumerable<NumberStudentDto>>(numberStudentList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccessful = false;
                _response.ErrorMessages = new List<string>() { ex.ToString()};
            }
            return _response;
           

        }

        [HttpGet("id:int", Name = "GetNumberStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> GetNumberStudent(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al obtener el numero Id " + id);
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccessful = false;
                    return BadRequest(_response);
                }
                var numberStudentId = await _numberstudentRepo.Obtain(v => v.StudentNo == id);

                if (numberStudentId == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccessful=false;
                    return NotFound(_response);
                }
                _response.Result = _mapping.Map<NumberStudentDto>(numberStudentId);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsSuccessful = false;
                _response.ErrorMessages = new List<string>() {ex.ToString() };
            }
            return _response;
            
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> AddNumberStudent([FromBody] NumberStudentCreateDto CreateStudentDto) 
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _numberstudentRepo.Obtain(v => v.StudentNo == CreateStudentDto.StudentNo) != null)
                {
                    ModelState.AddModelError("NumeroExiste", "Este numero ya existe");
                    return BadRequest(ModelState);
                }
                if(await _studentRepo.Obtain(v=> v.Id==CreateStudentDto.StudentId) == null)
                {
                    ModelState.AddModelError("ClaveForanea", "El Id del Estudiante no existe!");
                    return BadRequest(ModelState);
                }
                if (CreateStudentDto == null)
                {
                    return BadRequest(CreateStudentDto);
                }
                NumberStudent modelo = _mapping.Map<NumberStudent>(CreateStudentDto);

                await _numberstudentRepo.Create(modelo);
                _response.Result = modelo;
                _response.StatusCode = HttpStatusCode.Created;
                
                return CreatedAtRoute("GetNumberStudents", new { id = modelo.StudentNo }, _response);
            }
            catch (Exception ex)
            {

                _response.IsSuccessful = false;
                _response.ErrorMessages = new List<string>() { ex.ToString()};
            }
            return _response;
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteNumberStudent(int id) 
        {
            try
            {
                if (id == 0)
                {
                    _response.IsSuccessful= false;
                    _response.StatusCode=HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var numberstudent = await _numberstudentRepo.Obtain(v => v.StudentNo == id);
                if (numberstudent == null)
                {
                    _response.IsSuccessful = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _numberstudentRepo.Removed(numberstudent);

                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsSuccessful = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return BadRequest(_response);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateNumberStudent(int id, [FromBody]NumberStudentUpdateDto updateStudentDto) 
        {
            if (updateStudentDto == null || id != updateStudentDto.StudentNo)
            {
                _response.IsSuccessful = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            if(await _studentRepo.Obtain(v=>v.Id == updateStudentDto.StudentId)==null)
            {
                ModelState.AddModelError("ClaveForanea", "El Id del estudiante no existe");
                return BadRequest(ModelState);
            }
            
            NumberStudent modelo = _mapping.Map<NumberStudent>(updateStudentDto);

            await _numberstudentRepo.UpdateStudent(modelo);
            _response.StatusCode = HttpStatusCode.NoContent ;
            return Ok(_response);
        }

            
        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> UpdatePartialNumberStudent(int id, JsonPatchDocument<NumberStudentUpdateDto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }
            var numberstudent = await _numberstudentRepo.Obtain(v => v.StudentNo == id, tracked:false);

            NumberStudentUpdateDto numberStudentDto = _mapping.Map<NumberStudentUpdateDto>(numberstudent);
            
            if (numberstudent == null)
            {
                return BadRequest();
            }

            patchDto.ApplyTo(numberStudentDto, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            NumberStudent modelo = _mapping.Map<NumberStudent>(numberStudentDto);
            
            await _numberstudentRepo.UpdateStudent(modelo);
            _response.StatusCode = HttpStatusCode.NoContent;
            return Ok(_response);
        }
    }
}
