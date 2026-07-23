using ERP.API.Data;
using ERP.API.DTOs;
using ERP.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyMasterController : ControllerBase
    {
        private readonly ERPDbContext _context;
        public CompanyMasterController(ERPDbContext context) { _context = context; }

        [HttpGet("list")]
        public async Task<IActionResult> GetCompanies()
        {
            var companies = await _context.CompanyMasters
                .Select(c => new CompanyMasterDto
                {
                    CMPYID = c.CMPYID,
                    CMPYNAME = c.CMPYNAME,
                    CMPYADDR1 = c.CMPYADDR1,
                    CMPYADDR2 = c.CMPYADDR2,
                    CMPYADDR3 = c.CMPYADDR3,
                    CMPYCITY = c.CMPYCITY,
                    CMPYSTATE = c.CMPYSTATE,
                    CMPYPINCODE = c.CMPYPINCODE,
                    CMPYCONTACTPERSON = c.CMPYCONTACTPERSON,
                    CMPYPHONE1 = c.CMPYPHONE1,
                    CMPYPHONE2 = c.CMPYPHONE2,
                    CMPYEMAIL = c.CMPYEMAIL,
                    CMPYGSTNO = c.CMPYGSTNO,
                    CMPYPANNO = c.CMPYPANNO,
                    DISPSTATUS = c.DISPSTATUS
                }).ToListAsync();
            return Ok(companies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompany(int id)
        {
            var c = await _context.CompanyMasters.FindAsync(id);
            if (c == null) return NotFound();
            return Ok(new CompanyMasterDto
            {
                CMPYID = c.CMPYID,
                CMPYNAME = c.CMPYNAME,
                CMPYADDR1 = c.CMPYADDR1,
                CMPYADDR2 = c.CMPYADDR2,
                CMPYADDR3 = c.CMPYADDR3,
                CMPYCITY = c.CMPYCITY,
                CMPYSTATE = c.CMPYSTATE,
                CMPYPINCODE = c.CMPYPINCODE,
                CMPYCONTACTPERSON = c.CMPYCONTACTPERSON,
                CMPYPHONE1 = c.CMPYPHONE1,
                CMPYPHONE2 = c.CMPYPHONE2,
                CMPYEMAIL = c.CMPYEMAIL,
                CMPYGSTNO = c.CMPYGSTNO,
                CMPYPANNO = c.CMPYPANNO,
                DISPSTATUS = c.DISPSTATUS
            });
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCompany(CompanyMasterDto dto)
        {
            var c = new CompanyMaster
            {
                CMPYNAME = dto.CMPYNAME,
                CMPYADDR1 = dto.CMPYADDR1,
                CMPYADDR2 = dto.CMPYADDR2,
                CMPYADDR3 = dto.CMPYADDR3,
                CMPYCITY = dto.CMPYCITY,
                CMPYSTATE = dto.CMPYSTATE,
                CMPYPINCODE = dto.CMPYPINCODE,
                CMPYCONTACTPERSON = dto.CMPYCONTACTPERSON,
                CMPYPHONE1 = dto.CMPYPHONE1,
                CMPYPHONE2 = dto.CMPYPHONE2,
                CMPYEMAIL = dto.CMPYEMAIL,
                CMPYGSTNO = dto.CMPYGSTNO,
                CMPYPANNO = dto.CMPYPANNO,
                DISPSTATUS = dto.DISPSTATUS,
                CUSRID = "admin",
                PRCSDATE = DateTime.Now
            };
            _context.CompanyMasters.Add(c);
            await _context.SaveChangesAsync();
            return Ok(new { status = true, message = "Company Created Successfully" });
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCompany(int id, CompanyMasterDto dto)
        {
            var c = await _context.CompanyMasters.FindAsync(id);
            if (c == null) return NotFound();
            c.CMPYNAME = dto.CMPYNAME; c.CMPYADDR1 = dto.CMPYADDR1; c.CMPYADDR2 = dto.CMPYADDR2;
            c.CMPYADDR3 = dto.CMPYADDR3; c.CMPYCITY = dto.CMPYCITY; c.CMPYSTATE = dto.CMPYSTATE;
            c.CMPYPINCODE = dto.CMPYPINCODE; c.CMPYCONTACTPERSON = dto.CMPYCONTACTPERSON;
            c.CMPYPHONE1 = dto.CMPYPHONE1; c.CMPYPHONE2 = dto.CMPYPHONE2; c.CMPYEMAIL = dto.CMPYEMAIL;
            c.CMPYGSTNO = dto.CMPYGSTNO; c.CMPYPANNO = dto.CMPYPANNO; c.DISPSTATUS = dto.DISPSTATUS;
            await _context.SaveChangesAsync();
            return Ok(new { status = true, message = "Company Updated Successfully" });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            var c = await _context.CompanyMasters.FindAsync(id);
            if (c == null) return NotFound();
            _context.CompanyMasters.Remove(c);
            await _context.SaveChangesAsync();
            return Ok(new { status = true, message = "Company Deleted Successfully" });
        }
    }
}