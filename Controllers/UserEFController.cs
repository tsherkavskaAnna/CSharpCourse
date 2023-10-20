using AutoMapper;
using DataAPI;
using DataAPI.Data;
using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserEFController : ControllerBase
{
    DataContextEF _entityFramework;
    IMapper _mapper;
    public UserEFController(IConfiguration config)
    {
        _entityFramework = new DataContextEF(config);
        _mapper = new Mapper(new MapperConfiguration(cfg => {
            cfg.CreateMap<UserToAddDto, User>();
        }));
    }

    

    [HttpGet("GetUsers")]
    // public IEnumerable<User> GetUsers()
    public IEnumerable<User> GetUsers()
    {
        IEnumerable<User> users = _entityFramework.Users.ToList<User>();
        return users;
    }


    [HttpGet("GetSingleUsers/{userId}")]
    public User GetSingleUsers( int userId)
    {
        User user = _entityFramework.Users
              .Where(u => u.UserId == userId)
              .FirstOrDefault<User>();

        if(user != null)
            {
                return  user;
            }
            throw new Exception("Failed to get user!");
        
    }
///Update user
    [HttpPut("EditUser")]
    public IActionResult EditUser(User user)
    {
        User userDb = _entityFramework.Users
              .Where(u => u.UserId == user.UserId)
              .FirstOrDefault<User>();

        if(userDb!= null)
            {
                userDb.Active = user.Active;
                userDb.FirstName = user.FirstName;
                userDb.LastName = user.LastName;
                userDb.Gender = user.Gender;
                userDb.Email = user.Email;
                if(_entityFramework.SaveChanges() > 0)
                {
                    return Ok();
                }
                throw new Exception("Failed to Update user!");
            }
            throw new Exception("Failed to get user!");         
    }
///Add new user
    [HttpPost("AddUser")]
    public IActionResult AddUser(UserToAddDto user)
    {
        User userDb = _mapper.Map<User>(user);

                _entityFramework.Add(userDb);

                if(_entityFramework.SaveChanges() > 0)
                {
                    return Ok();
                }
                throw new Exception("Failed to Add user!");
    }
///DELETE USER
    [HttpDelete("DeleteUsers/{userId}")]
    public IActionResult DeleteUser( int userId)
    {
        User userDb = _entityFramework.Users
              .Where(u => u.UserId == userId)
              .FirstOrDefault<User>();

        if(userDb != null)
        {
            _entityFramework.Users.Remove(userDb);

            if(_entityFramework.SaveChanges() > 0)
            {
                return  Ok();
            }
            throw new Exception("Failed to Delete user!");
        }
          throw new Exception("Failed to Get User");   
    }
}
