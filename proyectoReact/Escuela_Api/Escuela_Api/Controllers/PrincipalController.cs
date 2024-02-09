using AutoMapper;
using Escuela_Api.Datos;
using Escuela_Api.Models;
using Escuela_Api.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Escuela_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrincipalController : ControllerBase
    {
        private readonly ILogger<PrincipalController> _logger;
        private readonly AplicationDbContext _db;
        private readonly IMapper _mapping;

        public PrincipalController(ILogger<PrincipalController> logger, AplicationDbContext db, IMapper mapping)
        {
            _logger = logger;
            _db = db;
            _mapping = mapping;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetStudents()
        {
            _logger.LogInformation("Obtener la lista de Estudiantes");
            IEnumerable<Student> studentList = await _db.students.ToListAsync();
            return Ok(_mapping.Map<IEnumerable<StudentDto>>(studentList));
            //return Ok(StudentStore.StudentList);
        }

        [HttpGet("id:int", Name = "GetStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StudentDto>> GetStudents(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Error al buscar el Id" + id);
                return BadRequest();
            }
            //var StudentId = StudentStore.StudentList.FirstOrDefault(v => v.Id == id);
            var StudentId = await _db.students.FirstOrDefaultAsync(v => v.Id == id);
            if (StudentId == null)
            {
                return NotFound();
            }
            return Ok(StudentId);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StudentDto>> AddStudent([FromBody] StudentCreatedDto CreateStudentDto) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _db.students.FirstOrDefaultAsync(v=>v.Correo.ToLower() == CreateStudentDto.Correo.ToLower()) !=null)
            {
                ModelState.AddModelError("CorreoExiste", "Este correo ya existe");
                    return BadRequest(ModelState);
            }
            if (CreateStudentDto == null)
            {
                return BadRequest(CreateStudentDto);
            }
            Student modelo = _mapping.Map<Student>(CreateStudentDto);
            //Student modelo = new()
            //{
            //    Apellido_Paterno = CreateDto.Apellido_Paterno,
            //    Apellido_Materno = CreateDto.Apellido_Materno,
            //    Nombres = CreateDto.Nombres,
            //    Fecha_Nacimiento = CreateDto.Fecha_Nacimiento,
            //    Lugar_Nacimiento = CreateDto.Lugar_Nacimiento,
            //    Estado_Civil = CreateDto.Estado_Civil,
            //    Genero = CreateDto.Genero,
            //    Nacionalidad = CreateDto.Nacionalidad,
            //    Celular = CreateDto.Celular,
            //    Direccion = CreateDto.Direccion,
            //    Telefono = CreateDto.Telefono,
            //    Correo = CreateDto.Correo,
            //    Especialidad = CreateDto.Especialidad,
            //    Numero_Matricula = CreateDto.Numero_Matricula,
            //    Fecha_Inicio = CreateDto.Fecha_Inicio,
            //    Curp = CreateDto.Curp,
            //    Estado = CreateDto.Estado
            //};
            await _db.students.AddAsync(modelo);
            await _db.SaveChangesAsync();
            //studentDto.Id = StudentStore.StudentList.OrderByDescending(v => v.Id).FirstOrDefault().Id + 1;
            //StudentStore.StudentList.Add(studentDto);

            return CreatedAtRoute("GetStudents", new { id = modelo.Id }, modelo);
        }
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteStudent(int id) 
        {
            if(id==0)
            {
                return BadRequest();
            }
            var Student = _db.students.FirstOrDefault(v => v.Id == id);
            if (Student == null)
            {
                return NotFound();
            }
            _db.students.Remove(Student);
            await _db.SaveChangesAsync();

            return NoContent();
        }
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody]StudentUpdateDto updateStudentDto) 
        {
            if (updateStudentDto == null || id != updateStudentDto.Id)
            {
                return BadRequest();
            }
            
            Student modelo = _mapping.Map<Student>(updateStudentDto);
       
            _db.students.Update(modelo);
            await _db.SaveChangesAsync();
            return NoContent();
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
            var Student = await _db.students.AsNoTracking().FirstOrDefaultAsync(v => v.Id == id);

            StudentUpdateDto studentDto = _mapping.Map<StudentUpdateDto>(patchDto);
            
            if (Student == null)
            {
                return BadRequest();
            }

            patchDto.ApplyTo(studentDto, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Student modelo = _mapping.Map<Student>(studentDto);
            
            _db.students.Update(modelo);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
