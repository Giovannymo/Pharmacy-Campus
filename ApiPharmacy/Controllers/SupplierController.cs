using ApiPharmacy.Dtos;
using Application.UnitOfWork;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiPharmacy.Controllers;

public class SupplierController : BaseApiController
{

  private readonly IUnitOfWork _unitOfWork;
  private readonly IMapper _mapper;

  public SupplierController(IUnitOfWork unitOfWork, IMapper mapper)
  {
    _unitOfWork = unitOfWork;
    _mapper = mapper;
  }

  //Obterner Todos los Proveedores
  [HttpGet("List")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<IEnumerable<SupplierDto>>> Get()
  {
    var suppliers = await _unitOfWork.People.GetAllSupplierAsync();
    return _mapper.Map<List<SupplierDto>>(suppliers);
  }

  //Ganancia total por proveedor en el Año (X)
  [Authorize(Roles = "Administrador")]
 
  [HttpGet("TotalSupplierGain/{year}")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<IEnumerable<SupplierGainDto>>> Get2(int year)
  {
    var suppliers = await _unitOfWork.People.TotalSupplierGain(year);
    return _mapper.Map<List<SupplierGainDto>>(suppliers);
  }

  //Proveedores  que no han vendido ningún medicamento en Un año Especifico
  [HttpGet("NeverSell/{year}")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<IEnumerable<SupplierDto>>> Get3(int year)
  {
      var supplier = await _unitOfWork.People.GetSupplierNeverSell(year);
      return _mapper.Map<List<SupplierDto>>(supplier);
  }

  //Número total de proveedores que suministraron medicamentos en 2023
  [HttpGet("GetTotalYear/{year}")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> Get5(int year)
  {
      var amount = await _unitOfWork.People.GetTotalSuppliersYearAsync(year);

      var dto = new TotalSupplierYear
      {
          TotalSuppliers = amount
      };

      return Ok(dto);
  }

  //Proveedor que ha suministrado más medicamentos en 2023
  [HttpGet("MostSuminAsync/{year}")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<SupplierDto>> Get4(int year)
  {
      var supplier = await _unitOfWork.People.GetSupplierMostSuminAsync(year);
      return _mapper.Map<SupplierDto>(supplier);
  }

  //Número de medicamentos por proveedor.
  [HttpGet("TotalProductsSupplier")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<IEnumerable<SupplierGroupDto>>> Get5()
  {
      var suppliers = await _unitOfWork.People.GetTotalProductsSupplier();
      return _mapper.Map<List<SupplierGroupDto>>(suppliers);
  }

  //Proveedores que han suministrado al menos 5 medicamentos diferentes en 2023. 
  [HttpGet("WithAtLeastProducts/{amount}/{year}")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<IEnumerable<SupplierDto>>> Get5(int amount, int year)
  {
      var suppliers = await _unitOfWork.People.GetSuppliersWithAtLeastProductsAsync(amount, year);
      return _mapper.Map<List<SupplierDto>>(suppliers);
  }


  [HttpGet("Unique/{id}")]
  [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SupplierDto>> Get(string id)
    {
        var supplier = await _unitOfWork.People.GetByIdAsync(id);
        return _mapper.Map<SupplierDto>(supplier);
    }

  [HttpPost]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<Person>> Post(SupplierDto supplierDto)
  {
    var supplier = _mapper.Map<Person>(supplierDto);
    _unitOfWork.People.Add(supplier);
    await _unitOfWork.SaveAsync();
    if (supplierDto == null)
    {
      return BadRequest();
    }
    supplierDto.Id = supplier.Id;
    return CreatedAtAction(nameof(Post), new { id = supplierDto.Id }, supplierDto);
  }


  [HttpPut("{id}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<SupplierDto>> Put(int id, [FromBody] SupplierDto supplierDto)
  {
    if (supplierDto == null)
    {
      return NotFound();
    }
    var supplier = _mapper.Map<Person>(supplierDto);
    _unitOfWork.People.Update(supplier);
    await _unitOfWork.SaveAsync();

    return supplierDto;
  }

  [HttpDelete("{id}")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> Delete(string id)
  {
    var supplier = await _unitOfWork.People.GetByIdAsync(id);
    if (supplier == null)
    {
      return NotFound();
    }

    _unitOfWork.People.Remove(supplier);
    await _unitOfWork.SaveAsync();

    return NoContent();
  }

  //Total de medicamentos vendidos por cada proveedor
  [HttpGet("GetProductsSoldEachSupplier")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<IEnumerable<SupplierPurchasesDto>> GetProductsSoldEachSupplier()
  {
    var suppliers = await _unitOfWork.People.GetProductsSoldEachSupplierAsync();
    return _mapper.Map<List<SupplierPurchasesDto>>(suppliers);
  }

  //Proveedores de los medicamentos con menos de 50 unidades en stock
  [HttpGet("WithStock/{amount}")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<IEnumerable<SupplierProductDto>> Get6(int amount)
  {
    var suppliers = await _unitOfWork.People.GetSupplierWithStockAsync(amount);
    return _mapper.Map<List<SupplierProductDto>>(suppliers);
  }
}
