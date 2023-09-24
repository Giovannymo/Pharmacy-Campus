using ApiPharmacy.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiPharmacy.Controllers;

public class EmployeeController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<EmployeeDto>>> Get()
    {
        var employees = await _unitOfWork.People.GetAllAsync();
        return _mapper.Map<List<EmployeeDto>>(employees);
    }

    [HttpGet("sales/{quantity}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IEnumerable<SalePatientProdDto>> Get2(int sales)
    {
    var patients = await _unitOfWork.People.GetSaleEmployee(sales);
    return _mapper.Map<List<SalePatientProdDto>>(patients);
    } 



    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get(int id)
    {
        var employee = await _unitOfWork.People.GetByIdAsync(id);
        return Ok(employee);
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Person>> Post(EmployeeDto employeeDto)
    {
        var employee = _mapper.Map<Person>(employeeDto);
        _unitOfWork.People.Add(employee);
        await _unitOfWork.SaveAsync();
        if (employeeDto == null)
        {
            return BadRequest();
        }
        employeeDto.Id = employee.Id;
        return CreatedAtAction(nameof(Post), new { id = employeeDto.Id }, employeeDto);
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EmployeeDto>> Put(int id, [FromBody] EmployeeDto employeeDto)
    {
        if (employeeDto == null)
        {
            return NotFound();
        }
        var employee = _mapper.Map<Person>(employeeDto);
        _unitOfWork.People.Update(employee);
        await _unitOfWork.SaveAsync();

        return employeeDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var employee = await _unitOfWork.People.GetByIdAsync(id);
        if (employee == null)
        {
            return NotFound();
        }

        _unitOfWork.People.Remove(employee);
        await _unitOfWork.SaveAsync();

        return NoContent();
    }
}
