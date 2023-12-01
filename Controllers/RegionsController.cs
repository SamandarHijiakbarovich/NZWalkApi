using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NzWalks.Api.Data;
using NzWalks.Api.Models.Domain;
using NzWalks.Api.Models.DTO;

namespace NzWalks.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegionsController : ControllerBase
{
    private readonly AppDbContext _context;
    public RegionsController(AppDbContext context)
    {
        _context = context;
        
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        // Get Data From Database -  Domain models 
        var regionsDomain =await _context.Regions.ToListAsync();

        // Map Domain Models to DTOs 
        var regionsDto = new List<RegionDto>();
        foreach(var regionDomain in regionsDomain)
        {
            regionsDto.Add(new RegionDto()
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl
            });
        }
        //return Region DTOs
        return Ok(regionsDto);
    }


    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        // get region Domain model from  database 
        //var regionDomain = _context.Regions.Find(id);

        var regionDomain =await  _context.Regions.FirstOrDefaultAsync(x => x.Id == id);

        if (regionDomain is null)
        {
            return NotFound("NotFaund");
        }

        var regionDto = new RegionDto()
        {
            Id = regionDomain.Id,
            Code = regionDomain.Code,
            Name = regionDomain.Name,
            RegionImageUrl = regionDomain.RegionImageUrl

        };
        return Ok(regionDto);


      
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRegionDto createRegionDto)
    {
        //Map or Convert DTO to Domain Model
        var regionDomainModel = new Region()
        {
            Code = createRegionDto.Code,
            Name = createRegionDto.Name,
            RegionImageUrl = createRegionDto.RegionImageUrl
        };
        await _context.Regions.AddAsync(regionDomainModel);
        await _context.SaveChangesAsync();

        var regionDto = new RegionDto
        {
            Id = regionDomainModel.Id,
            Name = regionDomainModel.Name,
            Code = regionDomainModel.Code,
            RegionImageUrl = regionDomainModel.RegionImageUrl
        };

        return  CreatedAtAction(nameof(GetById),new { id = regionDomainModel.Id },regionDto);
    }


    [HttpPut]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionDto updateRegionDto)
    {
        var regionDomainModel =await  _context.Regions.FirstOrDefaultAsync(x => x.Id == id);
        if(regionDomainModel is null )
        {
            return NotFound("Not Faund");
        }

        regionDomainModel.Code = updateRegionDto.Code;
        regionDomainModel.Name = updateRegionDto.Name;
        regionDomainModel.RegionImageUrl = updateRegionDto.RegionImageUrl;

        await _context.SaveChangesAsync();

        var regionDto = new RegionDto()
        {
            Id = regionDomainModel.Id,
            Code = regionDomainModel.Code,
            Name = regionDomainModel.Name,
            RegionImageUrl = regionDomainModel.RegionImageUrl
        };

        return Ok(regionDto);


    }


    [HttpDelete]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var region = await _context.Regions.FirstOrDefaultAsync(x => x.Id == id);

        if (region is null)
            return NotFound("Not Found");

         _context.Regions.Remove(region);
        await _context.SaveChangesAsync();

        var regionDto = new RegionDto()
        {
            Id = region.Id,
            Code = region.Code,
            Name = region.Name,
            RegionImageUrl = region.RegionImageUrl
        };

        return Ok(regionDto);
    }
}
