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
        [Authorize(Roles = "Admin")]
        [HttpPost("addAddress")]
        public async Task<IActionResult> addAddress([FromBody] AddressDTO addressDTO)
        {
            if (addressDTO == null) return BadRequest();
            await _addressService.AddAddressAsync(addressDTO);
            return Ok("add success !");
        }

        [HttpGet("getAllAddress")]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, NoStore = false)]
        public async Task<IActionResult> getAllAddress()
        {
            var address = await _addressService.GetAllAddressAsync();
            if (address == null) return NotFound();
            return Ok(address);
        }

        [HttpGet("getAddressByID/{id}")]
        public async Task<IActionResult> getAddressByID(int id)
        {
            var address = await _addressService.GetAddressByIdAsync(id);
            if (address == null) return NotFound();
            return Ok(address);

        }
    }
}
