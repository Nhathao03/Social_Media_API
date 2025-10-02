using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.Entities.DTO;
using SocialMedia.Core.Services;
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
            var newId = await _addressService.AddAddressAsync(addressDTO);
            return Ok(new { Message = "Add new address success", AddressId = newId });
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
        public async Task<IActionResult> updateAddress([FromBody] AddressDTO address)
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
