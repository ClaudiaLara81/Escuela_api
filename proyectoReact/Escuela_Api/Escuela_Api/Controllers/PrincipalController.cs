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

        public PrincipalController(ILogger<PrincipalController> logger, AplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }
        [HttpGet]
        public ActionResult<IEnumerable<StudentDto>> GetStudents()
        {
            _logger.LogInformation("Obtener la lista de Estudiantes");
            return Ok(_db.students.ToList());
            //return Ok(StudentStore.StudentList);
        }

        [HttpGet("id:int", Name = "GetStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<StudentDto> GetStudents(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Error al buscar el Id" + id);
                return BadRequest();
            }
            //var StudentId = StudentStore.StudentList.FirstOrDefault(v => v.Id == id);
            var StudentId = _db.students.FirstOrDefault(v => v.Id == id);
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
        public ActionResult<StudentDto> AddStudent([FromBody] StudentDto studentDto) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_db.students.FirstOrDefault(v=>v.Correo.ToLower() == studentDto.Correo.ToLower()) !=null)
            {
                ModelState.AddModelError("CorreoExiste", "Este correo ya existe");
                    return BadRequest(ModelState);
            }
                if (studentDto == null)
            {
                return BadRequest(studentDto);
            }
            if (studentDto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError); 
            }
            Student modelo = new()
            {
                //Id = studentDto.Id,
                Apellido_Paterno = studentDto.Apellido_Paterno,
                Apellido_Materno = studentDto.Apellido_Materno,
                Nombres = studentDto.Nombres,
                Fecha_Nacimiento = studentDto.Fecha_Nacimiento,
                Lugar_Nacimiento = studentDto.Lugar_Nacimiento,
                Estado_Civil = studentDto.Estado_Civil,
                Genero = studentDto.Genero,
                Nacionalidad = studentDto.Nacionalidad,
                Celular = studentDto.Celular,
                Direccion = studentDto.Direccion,
                Telefono = studentDto.Telefono,
                Correo = studentDto.Correo,
                Especialidad = studentDto.Especialidad,
                Numero_Matricula = studentDto.Numero_Matricula,
                Fecha_Inicio = studentDto.Fecha_Inicio,
                Curp = studentDto.Curp,
                Estado = studentDto.Estado
            };
            _db.students.Add(modelo);
            _db.SaveChanges();
            //studentDto.Id = StudentStore.StudentList.OrderByDescending(v => v.Id).FirstOrDefault().Id + 1;
            //StudentStore.StudentList.Add(studentDto);

            return CreatedAtRoute("GetStudents", new { id = studentDto.Id }, studentDto);
        }
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteStudent(int id) 
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
            _db.SaveChanges();

            return NoContent();
        }
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateStudent(int id, [FromBody]StudentDto studentDto) 
        {
            if (studentDto==null || id != studentDto.Id)
            {
                return BadRequest();
            }
            //var Student = StudentStore.StudentList.FirstOrDefault(v => v.Id == id);
            //Student.Name = studentDto.Name;
            //Student.Correo = studentDto.Correo;
            //Student.celular = studentDto.celular;
            //Student.Direccion = studentDto.Direccion;

            Student modelo = new()
            {
                Id = studentDto.Id,
                Apellido_Paterno = studentDto.Apellido_Paterno,
                Apellido_Materno = studentDto.Apellido_Materno,
                Nombres = studentDto.Nombres,
                Fecha_Nacimiento = studentDto.Fecha_Nacimiento,
                Lugar_Nacimiento = studentDto.Lugar_Nacimiento,
                Estado_Civil = studentDto.Estado_Civil,
                Genero = studentDto.Genero,
                Nacionalidad = studentDto.Nacionalidad,
                Celular = studentDto.Celular,
                Direccion = studentDto.Direccion,
                Telefono = studentDto.Telefono,
                Correo = studentDto.Correo,
                Especialidad = studentDto.Especialidad,
                Numero_Matricula = studentDto.Numero_Matricula,
                Fecha_Inicio = studentDto.Fecha_Inicio,
                Curp = studentDto.Curp,
                Estado = studentDto.Estado
            };
            _db.students.Update(modelo);
            _db.SaveChanges();
            return NoContent();
        }

            
        

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult UpdatePartialStudent(int id, JsonPatchDocument<StudentDto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }
            var Student = _db.students.AsNoTracking().FirstOrDefault(v => v.Id == id);

            StudentDto studentDto = new()
            {
                Id = Student.Id,
                Apellido_Paterno = Student.Apellido_Paterno,
                Apellido_Materno = Student.Apellido_Materno,
                Nombres = Student.Nombres,
                Fecha_Nacimiento = Student.Fecha_Nacimiento,
                Lugar_Nacimiento = Student.Lugar_Nacimiento,
                Estado_Civil = Student.Estado_Civil,
                Genero = Student.Genero,
                Nacionalidad = Student.Nacionalidad,
                Celular = Student.Celular,
                Direccion = Student.Direccion,
                Telefono = Student.Telefono,
                Correo = Student.Correo,
                Especialidad = Student.Especialidad,
                Numero_Matricula = Student.Numero_Matricula,
                Fecha_Inicio = Student.Fecha_Inicio,
                Curp = Student.Curp,
                Estado = Student.Estado
            };

            if (Student == null)
            {
                return BadRequest();
            }

            patchDto.ApplyTo(studentDto, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Student modelo = new()
            {
                Id = studentDto.Id,
                Apellido_Paterno = studentDto.Apellido_Paterno,
                Apellido_Materno = studentDto.Apellido_Materno,
                Nombres = studentDto.Nombres,
                Fecha_Nacimiento = studentDto.Fecha_Nacimiento,
                Lugar_Nacimiento = studentDto.Lugar_Nacimiento,
                Estado_Civil = studentDto.Estado_Civil,
                Genero = studentDto.Genero,
                Nacionalidad = studentDto.Nacionalidad,
                Celular = studentDto.Celular,
                Direccion = studentDto.Direccion,
                Telefono = studentDto.Telefono,
                Correo = studentDto.Correo,
                Especialidad = studentDto.Especialidad,
                Numero_Matricula = studentDto.Numero_Matricula,
                Fecha_Inicio = studentDto.Fecha_Inicio,
                Curp = studentDto.Curp,
                Estado = studentDto.Estado
            };

            _db.students.Update(modelo);
            _db.SaveChanges();
            return NoContent();
        }
    }
}
