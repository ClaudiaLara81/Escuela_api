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
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IStudentRepository _studentRepo;
        private readonly IMapper _mapping;
        protected ApiResponse _response;

        public StudentController(ILogger<StudentController> logger, IStudentRepository studentRepo, IMapper mapping)
        {
            _logger = logger;
            _studentRepo = studentRepo;
            _mapping = mapping;
            _response = new();
        }
        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetStudents()
        {
            try
            {
                _logger.LogInformation("Obtener la lista de Estudiantes");
                IEnumerable<Student> studentList = await _studentRepo.ObtainAll();
                _response.Result = _mapping.Map<IEnumerable<StudentDto>>(studentList);
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

        [HttpGet("id:int", Name = "GetStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> GetStudent(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al obtener el Id" + id);
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccessful = false;
                    return BadRequest(_response);
                }
                var studentId = await _studentRepo.Obtain(v => v.Id == id);

                if (studentId == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccessful = false;
                    return NotFound(_response);
                }
                _response.Result = _mapping.Map<StudentDto>(studentId);
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
        public async Task<ActionResult<ApiResponse>> AddStudent([FromBody] StudentCreatedDto CreateStudentDto) 
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _studentRepo.Obtain(v => v.Correo.ToLower() == CreateStudentDto.Correo.ToLower()) != null)
                {
                    ModelState.AddModelError("CorreoExiste", "Este correo ya existe");
                    return BadRequest(ModelState);
                }
                if (CreateStudentDto == null)
                {
                    return BadRequest(CreateStudentDto);
                }
                Student modelo = _mapping.Map<Student>(CreateStudentDto);

                await _studentRepo.Create(modelo);
                _response.Result = modelo;
                _response.StatusCode = HttpStatusCode.Created;
                
                return CreatedAtRoute("GetStudents", new { id = modelo.Id }, _response);
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
        public async Task<IActionResult> DeleteStudent(int id) 
        {
            try
            {
                if (id == 0)
                {
                    _response.IsSuccessful= false;
                    _response.StatusCode=HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var student = await _studentRepo.Obtain(v => v.Id == id);
                if (student == null)
                {
                    _response.IsSuccessful = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _studentRepo.Removed(student);

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
        public async Task<IActionResult> UpdateStudent(int id, [FromBody]StudentUpdateDto updateStudentDto) 
        {
            if (updateStudentDto == null || id != updateStudentDto.Id)
            {
                _response.IsSuccessful = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            
            Student modelo = _mapping.Map<Student>(updateStudentDto);

            await _studentRepo.UpdateStudent(modelo);
            _response.StatusCode = HttpStatusCode.NoContent ;
            return Ok(_response);
        }

            
        

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> UpdatePartialStudent(int id, JsonPatchDocument<StudentUpdateDto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }
            var student = await _studentRepo.Obtain(v => v.Id == id, tracked:false);

            StudentUpdateDto studentDto = _mapping.Map<StudentUpdateDto>(student);
            
            if (student == null)
            {
                return BadRequest();
            }

            patchDto.ApplyTo(studentDto, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Student modelo = _mapping.Map<Student>(studentDto);
            
            await _studentRepo.UpdateStudent(modelo);
            _response.StatusCode = HttpStatusCode.NoContent;
            return Ok(_response);
        }
    }
}
