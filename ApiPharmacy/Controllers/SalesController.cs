using ApiPharmacy.Dtos;
using AutoMapper;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiPharmacy.Controllers;

public class SalesController : BaseApiController
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IMapper _mapper;

    public SalesController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    //Total de ventas del medicamento (X)
    [HttpGet("{Product}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get(string product)
    {
        var count = await _unitOfWork.Sales.GetTotalSaleProduct(product);

        var dto = new TotalSaleProductDto
        {
            TotalSales = count
        };

        return Ok(dto);
    }
}
