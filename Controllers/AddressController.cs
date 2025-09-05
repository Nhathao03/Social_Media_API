using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Social_Media.BAL;
using Social_Media.Models;
using Social_Media.Models.DTO;

namespace Social_Media.Controllers
{
    [Route("api/address")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        // Add new address (Admin only)
        [Authorize(Roles = "Admin")]
        [HttpPost("addAddress")]
        public async Task<IActionResult> addAddress([FromBody] AddressDTO addressDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _addressService.AddAddressAsync(addressDTO);
            return Ok("add success !");
        }

        //Get all addresses
        [HttpGet("getAllAddress")]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, NoStore = false)]
        public async Task<IActionResult> getAllAddress()
        {
            var address = await _addressService.GetAllAddressAsync();
            if (address == null) return NotFound();
            return Ok(address);
        }

        //Get address by ID
        [HttpGet("getAddressByID/{id}")]
        public async Task<IActionResult> getAddressByID(int id)
        {
            var address = await _addressService.GetAddressByIdAsync(id);
            if (address == null) return NotFound();
            return Ok(address);

        }

        //Update address by ID (Admin only)
        [Authorize(Roles = "Admin")]
        [HttpPut("updateAddress")]
        public async Task<IActionResult> updateAddress([FromBody] Address address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _addressService.UpdateAddressAsync(address);
            return Ok("Update address success");
        }

        //Delete address by ID (Admin only)
        [Authorize(Roles = "Admin")]
        [HttpDelete("deleteAddressByID/{id}")]
        public async Task<IActionResult> deleteAddressByID(int id)
        {
            var address = await _addressService.GetAddressByIdAsync(id);
            if (address == null) return NotFound();
            await _addressService.DeleteAddressAsync(id);
            return NoContent();
        }
    }
}
