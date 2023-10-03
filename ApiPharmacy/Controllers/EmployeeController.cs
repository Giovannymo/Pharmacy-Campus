using ApiPharmacy.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiPharmacy.Controllers;
[Authorize(Roles = "Gerente , Administrador")]

public class EmployeeController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    //Obterner Todos los Empleados
    [HttpGet("List")]
    [AllowAnonymous]
    [Authorize(Roles = "Cajero")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<EmployeeDto>>> Get()
    {
        var employees = await _unitOfWork.People.GetAllEmpoyeesAsync();
        return _mapper.Map<List<EmployeeDto>>(employees);
    }

    //Empleados que hayan hecho más de N ventas en total.
    [HttpGet("SalesGreaterThan/{quantity}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<EmployeeDto>>> Get1(int quantity)
    {
        if (quantity < 0)
        {
            return BadRequest("La cantidad de ventas debe ser un número positivo.");
        }
        var employees = await _unitOfWork.People.GetSaleEmployee(quantity);
        return _mapper.Map<List<EmployeeDto>>(employees);
    } 

    //Empleados que Realizaron menos de (N) Ventas el Año (N).
    [HttpGet("GetEmployeeSaleYear/{quantity}/{year}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<EmployeeDto>>> Get2(int quantity, int year)
    {
        if (quantity < 0)
        {
            return BadRequest("La cantidad de ventas debe ser un número positivo.");
        }
        var employees = await _unitOfWork.People.GetEmployeeSaleYear(quantity, year);
        return _mapper.Map<List<EmployeeDto>>(employees);
    } 


    //Empleados que no realizaron ventas en (N) Mes y (N) Año.
    [HttpGet("GetEmployeeNeverSale/{month}/{year}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<EmployeeDto>>> Get3(int month, int year)
    {
        var patients = await _unitOfWork.People.EmployeeNeverSaleMonthYear( month, year);
        return _mapper.Map<List<EmployeeDto>>(patients);
    } 


    //Empleados que no han realizado ninguna venta en el Año (N) 
    [HttpGet("EmployeeNeverSaleYear/{date}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<EmployeeDto>>> Get4(int date)
    {
        var employees = await _unitOfWork.People.EmployeeNeverSaleYear(date);
        return _mapper.Map<List<EmployeeDto>>(employees);
    }

    //Cantidad de ventas realizadas por cada empleado en el Año (X)
    [HttpGet("CountAllSalesEmployees/{year}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<SalesEmployeDto>>> Get5(int year)
    {
        var employees = await _unitOfWork.People.CountAllSalesEmployees(year);
        return _mapper.Map<List<SalesEmployeDto>>(employees);
    }

    //Empleado que ha vendido la mayor cantidad de medicamentos distintos en 2023
    [HttpGet("GetEmployeeDifferentProducts/{year}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EmployeeDto>> Get6(int year)
    {
        var employee = await _unitOfWork.People.GetEmployeeDifferentProducts(year);
        return _mapper.Map<EmployeeDto>(employee);
    }
    
    [HttpGet("Unique/{id}")]
    [Authorize(Roles = "Cajero")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EmployeeDto>> Get(string id)
    {
        var employee = await _unitOfWork.People.GetByIdAsync(id);
        return _mapper.Map<EmployeeDto>(employee);
    }


    [HttpPost]
    [Authorize(Roles = "Cajero")]
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
    [AllowAnonymous]
    [Authorize(Roles = "Cajero")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EmployeeDto>> Put([FromBody] EmployeeDto employeeDto)
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
    [AllowAnonymous]
    [Authorize(Roles = "Cajero")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(string id)
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
