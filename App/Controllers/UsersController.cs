using App.DTOs;
using App.Extensions;
using App.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace App.Controllers;

//[Authorize]
public class UsersController : BaseApiController
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UsersController( IUserRepository userRepository
                           , IMapper mapper 
                          )
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }



    //////////////////////////////////////////
    /////////////////////////////////////////////
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    {// el está usando getMembers
        var users = await _userRepository.GetUsersAsync();
        var members = _mapper.Map<IEnumerable<MemberDto>>(users);

        return Ok(members);
    }

    // CON PAGINACION
    //[Authorize(Roles = "Admin")]
    //[HttpGet]
    //public async Task<ActionResult<AppUserPagedList>> GetUsers([FromQuery] UserParams userParams)
    //{// el está usando getMembers
    //    var currentUser = await _userRepository.GetUserByUserNameAsync(User.GetUsername());

    //    // p'q no me mande a mi en la lista de usuarios
    //    userParams.CurrentUsername = currentUser.UserName;

    //    // p'q xdefault me mande el sexo opuesto
    //    if (string.IsNullOrEmpty(userParams.Gender))
    //    {
    //        userParams.Gender = currentUser.Gender == "male" ? "female" : "male";
    //    }

    //    AppUserPagedList users = await _userRepository.GetPagedUsersAsync(userParams);

    //    var members = _mapper.Map<IEnumerable<MemberDto>>(users);

    //    Response.AddPaginationHeader(new PaginationHeader(users.CurrentPage, users.PageSize,
    //                                                      users.TotalCount, users.TotalPages));

    //    return Ok(members);
    //}


    //////////////////////////////////////////
    /////////////////////////////////////////////
    //[Authorize(Roles = "Member")]
    [HttpGet("{username}")]
    public async Task<ActionResult<MemberDto>> GetUser(string username)
    {// el está usando getMember
        var user = await _userRepository.GetUserByUserNameAsync(username); // trae todas las fotos

        // si no hay con este nombre tengo null
        if (user is null) return Ok("Este usuario no existe.");

        var member = _mapper.Map<MemberDto>(user);

        return Ok(member);
    }


    //////////////////////////////////////////////
    /////////////////////////////////////////////////
    // PUT api/Users
    [HttpPut]
    public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
    {
        //var username = User.FindFirst(ClaimTypes.Name)?.Value;
        var username = User.GetUsername();

        var user = await _userRepository.GetUserByUserNameAsync(username); // me trae todas las fotos
                                                                           // cambiar a GetUserByIdAsync
        if (user == null) return NotFound();

        // lo q esta em memberUpdateDto lo mete a user
        //                |---------->
        _mapper.Map(memberUpdateDto, user);

        // aùn y si no hay cambios me sobreescribe todo
        if (await _userRepository.UpdateUserAsync(user)) return NoContent();

        return BadRequest("Failed to update user.");
    }


    
}
