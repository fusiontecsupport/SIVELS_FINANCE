using ERP.API.Data;
using ERP.API.DTOs;
using ERP.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerMasterController : ControllerBase
    {
        private readonly ERPDbContext _context;

        public CustomerMasterController(ERPDbContext context)
        {
            _context = context;
        }

        // GET: api/CustomerMaster/dropdowns
        [HttpGet("dropdowns")]
        public async Task<IActionResult> GetDropdowns()
        {
            var states = await _context.StateMasters
                .Where(s => s.DISPSTATUS == 1)
                .Select(s => new { id = s.STATEID, name = s.STATENAME })
                .ToListAsync();

            var locations = await _context.LocationMasters
                .Where(l => l.DISPSTATUS == 1)
                .Select(l => new { id = l.LOCTID, name = l.LOCTNAME, stateId = l.STATEID })
                .ToListAsync();

            return Ok(new { states, locations });
        }

        // GET: api/CustomerMaster/list
        [HttpGet("list")]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await (
                from c in _context.CustomerMasters
                join cld in _context.CustomerLocationDetails on c.CATEID equals cld.CATEID into locGroup
                from cld in locGroup.OrderBy(x => x.CATEAID).Take(1).DefaultIfEmpty()
                join l in _context.LocationMasters on cld.LOCTID equals l.LOCTID into locJoin
                from l in locJoin.DefaultIfEmpty()
                join s in _context.StateMasters on cld.STATEID equals s.STATEID into stateJoin
                from s in stateJoin.DefaultIfEmpty()
                select new CustomerMasterDto
                {
                    CATEID = c.CATEID,
                    CATECODE = c.CATECODE,
                    CATENAME = c.CATENAME,
                    CATEDNAME = c.CATEDNAME,
                    CATE_GST_NO = c.CATE_GST_NO,
                    CATE_PAN_NO = c.CATE_PAN_NO,
                    CATE_TAN_NO = c.CATE_TAN_NO,
                    CATEBANKNAME = c.CATEBANKNAME,
                    CATEBANKBRNCHNAME = c.CATEBANKBRNCHNAME,
                    CATEBANKADDR = c.CATEBANKADDR,
                    CATEBANK_ACTYPE = c.CATEBANK_ACTYPE,
                    CATEBANK_ACNO = c.CATEBANK_ACNO,
                    CATEBANK_IFCS_CODE = c.CATEBANK_IFCS_CODE,
                    CATEBANK_IBAN_CODE = c.CATEBANK_IBAN_CODE,
                    CATEBANK_SWIFT_CODE = c.CATEBANK_SWIFT_CODE,
                    CATE_TALLY_NAME = c.CATE_TALLY_NAME,
                    DISPSTATUS = c.DISPSTATUS,

                    CATEAADDR = cld != null ? cld.CATEAADDR : null,
                    CATEAADDR1 = cld != null ? cld.CATEAADDR1 : null,
                    CATEAADDR2 = cld != null ? cld.CATEAADDR2 : null,
                    CATEA_CITY = cld != null ? cld.CATEA_CITY : null,
                    CATEA_COUNTRY = cld != null ? cld.CATEA_COUNTRY : null,
                    CATEA_PINCODE = cld != null ? cld.CATEA_PINCODE : null,
                    LOCTID = cld != null ? cld.LOCTID : (int?)null,
                    STATEID = cld != null ? cld.STATEID : (int?)null,
                    LocationName = l != null ? l.LOCTNAME : null,
                    StateName = s != null ? s.STATENAME : null
                }).ToListAsync();

            return Ok(customers);
        }

        // GET: api/CustomerMaster/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var c = await _context.CustomerMasters.FindAsync(id);
            if (c == null) return NotFound();

            var addresses = await _context.CustomerLocationDetails
                .Where(a => a.CATEID == id)
                .OrderBy(a => a.CATEAID)
                .Select(a => new
                {
                    cateaAddr = a.CATEAADDR,
                    cateaAddr1 = a.CATEAADDR1,
                    cateaAddr2 = a.CATEAADDR2,
                    cateA_CITY = a.CATEA_CITY,
                    cateA_COUNTRY = a.CATEA_COUNTRY,
                    cateA_PINCODE = a.CATEA_PINCODE,
                    loctid = a.LOCTID,
                    stateid = a.STATEID
                })
                .ToListAsync();

            return Ok(new
            {
                catecode = c.CATECODE,
                catename = c.CATENAME,
                catedname = c.CATEDNAME,
                cate_GST_NO = c.CATE_GST_NO,
                cate_PAN_NO = c.CATE_PAN_NO,
                cate_TAN_NO = c.CATE_TAN_NO,
                catebankname = c.CATEBANKNAME,
                catebankbrnchname = c.CATEBANKBRNCHNAME,
                catebankaddr = c.CATEBANKADDR,
                catebank_ACTYPE = c.CATEBANK_ACTYPE,
                catebank_ACNO = c.CATEBANK_ACNO,
                catebank_IFCS_CODE = c.CATEBANK_IFCS_CODE,
                catebank_IBAN_CODE = c.CATEBANK_IBAN_CODE,
                catebank_SWIFT_CODE = c.CATEBANK_SWIFT_CODE,
                cate_TALLY_NAME = c.CATE_TALLY_NAME,
                dispstatus = c.DISPSTATUS,
                addresses
            });
        }

        // POST: api/CustomerMaster/create
        [HttpPost("create")]
        public async Task<IActionResult> CreateCustomer(CreateCustomerDto dto)
        {
            // ===== STEP 1: Create the Customer row =====
            var customer = new CustomerMaster
            {
                CATECODE = dto.CATECODE,
                CATENAME = dto.CATENAME,
                CATEDNAME = dto.CATEDNAME,
                CATE_GST_NO = dto.CATE_GST_NO,
                CATE_PAN_NO = dto.CATE_PAN_NO,
                CATE_TAN_NO = dto.CATE_TAN_NO,
                CATEBANKNAME = dto.CATEBANKNAME,
                CATEBANKBRNCHNAME = dto.CATEBANKBRNCHNAME,
                CATEBANKADDR = dto.CATEBANKADDR,
                CATEBANK_ACTYPE = dto.CATEBANK_ACTYPE,
                CATEBANK_ACNO = dto.CATEBANK_ACNO,
                CATEBANK_IFCS_CODE = dto.CATEBANK_IFCS_CODE,
                CATEBANK_IBAN_CODE = dto.CATEBANK_IBAN_CODE,
                CATEBANK_SWIFT_CODE = dto.CATEBANK_SWIFT_CODE,
                CATE_TALLY_NAME = dto.CATE_TALLY_NAME,
                DISPSTATUS = dto.DISPSTATUS,

                // System-managed fields
                CUSRID = "admin", // TODO: replace with actual logged-in username later
                LMUSRID = 0,
                PRCSDATE = DateTime.Now,
                CATENO = 1,
                CATETID = 2,
                ACHEADID = 1
            };

            _context.CustomerMasters.Add(customer);
            await _context.SaveChangesAsync();
            // customer.CATEID is now auto-populated with the new ID

            // ===== STEP 2: Create one Address row per entry in dto.Addresses, using that same CATEID =====
            if (dto.Addresses != null)
            {
                foreach (var addr in dto.Addresses)
                {
                    var address = new CustomerLocationDetail
                    {
                        CATEID = customer.CATEID,
                        CATEAADDR = addr.CATEAADDR,
                        CATEAADDR1 = addr.CATEAADDR1,
                        CATEAADDR2 = addr.CATEAADDR2,
                        CATEA_CITY = addr.CATEA_CITY,
                        CATEA_COUNTRY = addr.CATEA_COUNTRY,
                        CATEA_PINCODE = addr.CATEA_PINCODE,
                        LOCTID = addr.LOCTID,
                        STATEID = addr.STATEID
                    };
                    _context.CustomerLocationDetails.Add(address);
                }
                await _context.SaveChangesAsync();
            }

            return Ok(new
            {
                status = true,
                message = "Customer Created Successfully",
                customerId = customer.CATEID
            });
        }

        // PUT: api/CustomerMaster/update/5
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, CreateCustomerDto dto)
        {
            var customer = await _context.CustomerMasters.FindAsync(id);
            if (customer == null) return NotFound();

            customer.CATECODE = dto.CATECODE;
            customer.CATENAME = dto.CATENAME;
            customer.CATEDNAME = dto.CATEDNAME;
            customer.CATE_GST_NO = dto.CATE_GST_NO;
            customer.CATE_PAN_NO = dto.CATE_PAN_NO;
            customer.CATE_TAN_NO = dto.CATE_TAN_NO;
            customer.CATEBANKNAME = dto.CATEBANKNAME;
            customer.CATEBANKBRNCHNAME = dto.CATEBANKBRNCHNAME;
            customer.CATEBANKADDR = dto.CATEBANKADDR;
            customer.CATEBANK_ACTYPE = dto.CATEBANK_ACTYPE;
            customer.CATEBANK_ACNO = dto.CATEBANK_ACNO;
            customer.CATEBANK_IFCS_CODE = dto.CATEBANK_IFCS_CODE;
            customer.CATEBANK_IBAN_CODE = dto.CATEBANK_IBAN_CODE;
            customer.CATEBANK_SWIFT_CODE = dto.CATEBANK_SWIFT_CODE;
            customer.CATE_TALLY_NAME = dto.CATE_TALLY_NAME;
            customer.DISPSTATUS = dto.DISPSTATUS;
            customer.LMUSRID = 0; // TODO: replace with actual logged-in userId later

            // Replace all existing address rows for this customer with the incoming set.
            // This naturally handles additions, edits, and deletions from the Location Details tab
            // without needing to track which CATEAID belongs to which row on the frontend.
            var existingAddresses = await _context.CustomerLocationDetails
                .Where(a => a.CATEID == id)
                .ToListAsync();
            _context.CustomerLocationDetails.RemoveRange(existingAddresses);

            if (dto.Addresses != null)
            {
                foreach (var addr in dto.Addresses)
                {
                    _context.CustomerLocationDetails.Add(new CustomerLocationDetail
                    {
                        CATEID = id,
                        CATEAADDR = addr.CATEAADDR,
                        CATEAADDR1 = addr.CATEAADDR1,
                        CATEAADDR2 = addr.CATEAADDR2,
                        CATEA_CITY = addr.CATEA_CITY,
                        CATEA_COUNTRY = addr.CATEA_COUNTRY,
                        CATEA_PINCODE = addr.CATEA_PINCODE,
                        LOCTID = addr.LOCTID,
                        STATEID = addr.STATEID
                    });
                }
            }

            await _context.SaveChangesAsync();

            return Ok(new { status = true, message = "Customer Updated Successfully" });
        }

        // DELETE: api/CustomerMaster/delete/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.CustomerMasters.FindAsync(id);
            if (customer == null) return NotFound();

            // Address rows are auto-deleted via ON DELETE CASCADE at the database level
            _context.CustomerMasters.Remove(customer);
            await _context.SaveChangesAsync();

            return Ok(new { status = true, message = "Customer Deleted Successfully" });
        }
    }
}