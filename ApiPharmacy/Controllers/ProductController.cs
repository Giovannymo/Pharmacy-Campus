using ApiPharmacy.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiPharmacy.Controllers;

    
public class ProductController : BaseApiController
{
        private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    //Listar Todos Los Productos
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ProductDto>>> Get()
    {
        var products = await _unitOfWork.Products.GetAllAsync();
        return _mapper.Map<List<ProductDto>>(products);
    }


    //Listar los proveedores con su información de contacto en medicamentos.
    [HttpGet("ContactSupplierInProduct")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ProductSupplierDto>>> Get5()
    {
        var products = await _unitOfWork.Products.GetAllInfoAsync();
        if(products == null)
        {
            return BadRequest();
        }
        return _mapper.Map<List<ProductSupplierDto>>(products);
    }


    //Total de medicamentos vendidos en el trimestre (X) del Año (X)
    [HttpGet("AllSalesQuarter/{year}/{trim}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<TotalProductYear>>> Get2(int year, int trim)
    {
        var products = await _unitOfWork.Products.AllSalesQuarter(year, trim);
        return _mapper.Map<List<TotalProductYear>>(products);
    }

    //Cantidad total de dinero recaudado por las ventas de medicamentos
    [HttpGet("TotalGain")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get3()
    {
        var total = await _unitOfWork.Products.GetTotalGain();

        var dto = new TotalProductsGainDto
        {
            TotalGain = total
        };

        return Ok(dto);
    }

    //Obtener todos los medicamentos con menos de (X) unidades en stock
    [HttpGet("GetLessThan/{amount}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetLessThan(int amount)
    {
        var products = await _unitOfWork.Products.GetLessThanStockAsync(amount);
        if(products == null)
        {
            return NotFound("No se encontraron productos menores a " + amount );
        }
        return _mapper.Map<List<ProductDto>>(products);

    }
    // Medicamentos que caducan antes del dia (X) del Mes (x) del Año (X).
    [HttpGet("ExpiredBefore/{expiryDate}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProductExpiredBefore(DateTime ExpiryDate)
    {
        var products = await _unitOfWork.Products.GetAllProductExpiredBeforeAsync(ExpiryDate);
        if(products == null)
        {
            return BadRequest("No se encontraron productos a expirar en la fecha " + ExpiryDate );
        }
        return _mapper.Map<List<ProductDto>>(products);

    }

    //Medicamentos con un precio mayor a (X) y un stock menor a (X).
    [HttpGet("GetHighPricedLowStock/{price}/{stock}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetHighPricedLowStock(decimal price, double stock)
    {
        var products = await _unitOfWork.Products.GetHighPricedLowStockAsync(price,stock);
        return _mapper.Map<List<ProductDto>>(products);

    }

    //Medicamentos que no han sido vendidos nunca.
    [HttpGet("GetAllProductsNeverSold")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProductsNeverSold()
    {
        var products = await _unitOfWork.Products.GetAllProductsNeverSold();
        return _mapper.Map<List<ProductDto>>(products);
    } 

    //Medicamentos que no han sido vendidos en el año (X).
    [HttpGet("GetAllNotSoldAtYear/{year}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ProductNameDto>>> GetAllProductsNotSoldAtYear(int year)
    {
        var products = await _unitOfWork.Products.GetAllProductsNotSoldInYearAsync(year);
        if(products == null)
        {
            return new ContentResult
            {
                StatusCode = 204, // Código de estado HTTP 204 (No Content)
                Content = $"El el año {year} todos los productos han tenido ventas :D",
                ContentType = "text/plain" // Tipo de contenido 
            };
        }
        return _mapper.Map<List<ProductNameDto>>(products);
    }


    //Medicamentos comprados al ‘Proveedor A’.
    [HttpGet("ProductsBySupplier/{supplier}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ProductNameDto>>> GetAllProductsBySupplier(string supplier)
    {
        var products = await _unitOfWork.Products.GetAllProductsBySupplierAsync(supplier);
        if(products == null)
        {
            return new ContentResult
            {
                StatusCode = 204, 
                Content = $"No hay productos relacionados con el proovedor {supplier}",
                ContentType= "text/Plain"
            };
        }
        return _mapper.Map<List<ProductNameDto>>(products);
    }


    //Medicamentos que han sido vendidos cada mes del año 2023
    [HttpGet("GetAllProductsSoldInMonth/{month}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ProductNameDto>>> GetAllProductsSoldInMonth(int month)
    {
        var products = await _unitOfWork.Products.GetAllProductsSoldInMonthAsync(month);
        if(products == null)
        {
            return new ContentResult
            {
                StatusCode = 204, 
                Content = $"No hay ventas de ningun producto este mes :C",
                ContentType= "text/Plain"
            };
        }
        return _mapper.Map<List<ProductNameDto>>(products);
    }

    //Obtener el medicamento menos vendido en 2023.
    [HttpGet("GetLowestSellingProduct/{year}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductDto>> GetLowestSellingProduct(int year)
    {
        var product = await _unitOfWork.Products.GetLowestSellingProductAsync(year);
        return _mapper.Map<ProductDto>(product);
    }

    //Obtener el medicamento más caro
    [HttpGet("GetMostExpensive")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductDto>> Get4()
    {
        var product = await _unitOfWork.Products.GetProductMostExpensive();
        return _mapper.Map<ProductDto>(product);
    }


    //Obtener el total de medicamentos vendidos en marzo de 2023.
    [HttpGet("GetTotalMonth/{month}/{year}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get4( int year, int month)
    {
        var count = await _unitOfWork.Products.GetTotalProductMonthAsync(month, year );

        var dto = new TotProdMonthDto
        {
            TotalProduct = count
        };

        return Ok(dto);
    }

    //Obtener todos los medicamentos que expiren en 2024
    [HttpGet("Expired/{year}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ProductDto>>> Get5(int year)
    {
        var products = await _unitOfWork.Products.GetAllProductExpiredAsync(year);
        return _mapper.Map<List<ProductDto>>(products);

    }

    //Total de medicamentos vendidos por mes en 2023
    [HttpGet("GetTotalProduct/{year}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IEnumerable<ProductMonthDto>>  Get6(int year)
    {
        var total = await _unitOfWork.Products.GetTotalProduct(year);
        return _mapper.Map<List<ProductMonthDto>>(total);
    }


    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get(int id)
    {
        var products = await _unitOfWork.Products.GetByIdAsync(id);
        return Ok(products);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Product>> Post(ProductDto productDto)
    {
        var product = _mapper.Map<Product>(productDto);
        _unitOfWork.Products.Add(product);
        await _unitOfWork.SaveAsync();
        if (productDto == null)
        {
            return BadRequest();
        }
        productDto.Id = product.Id;
        return CreatedAtAction(nameof(Post), new { id = productDto.Id }, productDto);
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<ProductDto>> Put(int id, [FromBody] ProductDto productDto)
    {
        if (productDto == null)
        {
            return NotFound();
        }
        var product = _mapper.Map<Product>(productDto);
        _unitOfWork.Products.Update(product);
        await _unitOfWork.SaveAsync();

        return productDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        _unitOfWork.Products.Remove(product);
        await _unitOfWork.SaveAsync();

        return NoContent();
    }



}
