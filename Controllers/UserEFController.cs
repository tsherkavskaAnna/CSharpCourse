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
///      UPDATE user
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
///      ADD new user
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
///     DELETE USER
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
////////////// get SALARY 
///
 [HttpGet("GetSalary/{userId}")]
    public IEnumerable<UserSalary> GetSalary( int userId)
    {
        return _entityFramework.UserSalary
               .Where(u => u.UserId == userId)
               .ToList();
        throw new Exception("Failed to get salary!");  
    }
//////////////   ADD new SALARY
///
[HttpPost("AddSalary")]
    public IActionResult PostSalary(UserSalary userSalaryAdd)
    {
        _entityFramework.Add(userSalaryAdd);
            if(_entityFramework.SaveChanges() > 0)
                {
                    return Ok();
                }
            throw new Exception("Failed to Add Salary!");
    }
///////////      UPDATE Salary
///
[HttpPut("EditSalary")]
    public IActionResult EditSalary(UserSalary userSalaryEdit)
    {
        UserSalary userDb = _entityFramework.UserSalary
              .Where(u => u.UserId == userSalaryEdit.UserId)
              .FirstOrDefault();

        if(userDb!= null)
            {
                userDb.Salary = userSalaryEdit.Salary;
                if(_entityFramework.SaveChanges() > 0)
                {
                    return Ok();
                }
                throw new Exception("Failed to Update Salary!");
            }
            throw new Exception("Failed to find USerSalary to Update!");         
    }
/////////     DELETE SALARY
///
[HttpDelete("DeleteSalary/{userId}")]
    public IActionResult DeleteSalary( int userId)
    {
        UserSalary userDelete = _entityFramework.UserSalary
              .Where(u => u.UserId == userId)
              .FirstOrDefault();

        if(userDelete != null)
        {
            _entityFramework.UserSalary.Remove(userDelete);

            if(_entityFramework.SaveChanges() > 0)
            {
                return  Ok();
            }
            throw new Exception("Failed to Delete Salary!");
        }
          throw new Exception("Failed to find salary for delete!");   
    }
///////////// get JOB INFO
///
 [HttpGet("GetJobInfo/{userId}")]
    public IEnumerable<UserJobInfo> GetJobInfo( int userId)
    {
        return _entityFramework.UserJobInfo
               .Where(u => u.UserId == userId)
               .ToList();
        throw new Exception("Failed to get fob info!");  
    }
//////////////   ADD new JOB INFO
///
[HttpPost("AddJobInfo")]
    public IActionResult PostJobInfo(UserJobInfo userJonInfoAdd)
    {
        _entityFramework.Add(userJonInfoAdd);
            if(_entityFramework.SaveChanges() > 0)
                {
                    return Ok();
                }
            throw new Exception("Failed to Add Job Information!");
    } 
//////////      UPDATE Salary
///
[HttpPut("EditJobInfo")]
    public IActionResult EditJobInfo(UserJobInfo editJobInfo)
    {
        UserJobInfo infoJob = _entityFramework.UserJobInfo
              .Where(u => u.UserId == editJobInfo.UserId)
              .FirstOrDefault();

        if(infoJob!= null)
            {
                infoJob.Department = editJobInfo.Department;
                infoJob.JobTitle = editJobInfo.JobTitle;
                
                if(_entityFramework.SaveChanges() > 0)
                {
                    return Ok();
                }
                throw new Exception("Failed to Update Job Information!");
            }
            throw new Exception("Failed to find job information!");         
    } 
/////////     DELETE JOB INFO
///
[HttpDelete("DeleteJobInfo/{userId}")]
    public IActionResult DeleteJobInfo( int userId)
    {
        UserJobInfo jobInfoDelete = _entityFramework.UserJobInfo
              .Where(u => u.UserId == userId)
              .FirstOrDefault();

        if(jobInfoDelete != null)
        {
            _entityFramework.UserJobInfo.Remove(jobInfoDelete);

            if(_entityFramework.SaveChanges() > 0)
            {
                return  Ok();
            }
            throw new Exception("Failed to Delete Job Information!");
        }
          throw new Exception("Failed to find job info for delete!");   
    }          
}
