using ApiPharmacy.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiPharmacy.Controllers;

public class RecipeController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RecipeController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> Get()
    {
        var recipes = await _unitOfWork.Recipes.GetAllAsync();
        return _mapper.Map<List<RecipeDto>>(recipes);
    }


    [HttpGet("RecordsByDate/{date}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IEnumerable<RecipeDto>> Get2(DateTime date)
    {
        var recipes = await _unitOfWork.Recipes.GetRecordsByDate(date);
        return _mapper.Map<List<RecipeDto>>(recipes);
    }

}