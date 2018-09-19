using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheBigScreen.Entities.Interfaces;
using TheBigScreen.Services.Interfaces;
using TheBigScreen.Services.ViewModels;
using TheBigScreen.WebApi.Models;
using TMDbLib.Objects.Movies;

namespace TheBigScreen.WebApi.Controllers
{
//    [Authorize]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private string userId = "109186138895177821151";
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUnitOfWork unitOfWork, IUserService userService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
            _mapper = mapper;
        }


        #region authedRoutesForUserCrud

        [Authorize("read:userdetails")]
        [HttpGet("user/me")]
        public async Task<IActionResult> GetUserOwnerDetails()
        {
            
            try
            {
                var user = await _userService.GetUserOwnerDetails(userId);
                if (user != null)
                {
                    //                await _unitOfWork.CompleteAsync();
                    return Ok(user);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [Authorize("read:userdetails")]
        [HttpPost("user/me")]
        public async Task<IActionResult> CreateOrUpdateUserDetails([FromBody] UserViewModel userModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                UserViewModel user;
                if (userModel.Id == userId)
                {
                    user = await _userService.CreateOrUpdateUserDetails(userId, userModel);
                    if (user != null)
                    {
                        await _unitOfWork.CompleteAsync(); // posible overwritting ( handle better ) 
                        return Ok(user);
                    }
                }
                return BadRequest($"User ids don't match!");
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }



        [Authorize("read:userdetails")]
        [HttpDelete("user/me")]
        public async Task<IActionResult> CreateOrUpdateUserDetails()
        {
            try
            {
                bool deleted;
                var exists = await _userService.GetUserOwnerDetails(userId);
                if (exists != null)
                {
                    deleted = await _userService.DeleteUserDetails(userId);
                }
                else
                {
                    return NotFound();
                }
                if (deleted)
                {
                    await _unitOfWork.CompleteAsync();
                    return Ok($"user account with id: {userId} was deleted deleted!");
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        //        TODO: for the next phase
        [Authorize("read:userdetails")]
        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetUserDetails(string id)
        {

            try
            {
                var user = await _userService.GetUserDetails(id);
                if (user != null)
                {
                    //                await _unitOfWork.CompleteAsync();
                    return Ok(user);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }


        #endregion
    }
}
