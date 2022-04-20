namespace EFCore6Demo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DepartmentsController : ControllerBase
{
    private readonly ILogger<DepartmentsController> _logger;
    private readonly ContosoUniversityContext _db;

    public DepartmentsController(ILogger<DepartmentsController> logger, ContosoUniversityContext db)
    {
        _logger = logger;
        _db = db;
    }

    [HttpGet("")]
    public ActionResult<IEnumerable<Department>> GetDepartments()
    {
        return this._db.Departments.ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<Department> GetDepartmentById(int id)
    {
        return this._db.Departments.Find(id);
    }

}
