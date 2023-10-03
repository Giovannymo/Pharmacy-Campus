using ApiPharmacy.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiPharmacy.Controllers;

public class PatientController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PatientController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    //Obtener todos los Pacientes 
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PatientDto>>> Get()
    {
        var suppliers = await _unitOfWork.People.GetAllPatientAsync();
        return _mapper.Map<List<PatientDto>>(suppliers);
    }

    //Pacientes que han comprado un producto especifico
    [HttpGet("PurchaseProduct/{product}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IEnumerable<PatientDto>> Get(string product)
    {
    var patients = await _unitOfWork.People.GetPurchasePatientProduct(product);
    return _mapper.Map<List<PatientDto>>(patients);
    }

    //Pacientes que compraron un producto (X) en el Año (X) 
    [HttpGet("PurchaseProductYear/{product}/{date}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IEnumerable<PatientDto>> Get2(string product, int date)
    {
        var patients = await _unitOfWork.People.GetPurchasePatientProductYear(product, date);
        return _mapper.Map<List<PatientDto>>(patients);
    }

    //Pacientes que no han comprado ningún medicamento en el año (X)
    [HttpGet("NeverBuy/{date}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PatientDto>>> Get2(int date)
    {
        var patients = await _unitOfWork.People.GetPatientsNeverBuy(date);
        return _mapper.Map<List<PatientDto>>(patients);
    }

    //Total gastado por cada paciente en el Año (X)
    [HttpGet("TotalSpentPatient/{year}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<SpentPatientDto>>> Get3(int year)
    {
        var patients = await _unitOfWork.People.TotalSpentPatient(year);
        return _mapper.Map<List<SpentPatientDto>>(patients);
    }
    
    [HttpGet("GetSpendMostMoneyInYear/{year}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PatientDto>> GetPatientSpendMostMoneyInYear(int year)
    {
        var patient = await _unitOfWork.People.GetPatientSpendMostMoneyInYear(year);
        return _mapper.Map<PatientDto>(patient);
    }


    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get(int id)
    {
        var patient = await _unitOfWork.People.GetByIdAsync(id);
        return Ok(patient);
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Person>> Post(PatientDto patientDto)
    {
        var patient = _mapper.Map<Person>(patientDto);
        _unitOfWork.People.Add(patient);
        await _unitOfWork.SaveAsync();
        if (patientDto == null)
        {
            return BadRequest();
        }
        patientDto.Id = patient.Id;
        return CreatedAtAction(nameof(Post), new { id = patientDto.Id }, patientDto);
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PatientDto>> Put([FromBody] PatientDto patientDto)
    {
        if (patientDto == null)
        {
            return NotFound();
        }
        var patient = _mapper.Map<Person>(patientDto);
        _unitOfWork.People.Update(patient);
        await _unitOfWork.SaveAsync();

        return patientDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var patient = await _unitOfWork.People.GetByIdAsync(id);
        if (patient == null)
        {
            return NotFound();
        }

        _unitOfWork.People.Remove(patient);
        await _unitOfWork.SaveAsync();

        return NoContent();
    }
}