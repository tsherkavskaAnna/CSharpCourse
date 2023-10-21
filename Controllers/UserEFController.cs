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
    IUserRepository _userRepository;
    IMapper _mapper;
    public UserEFController(IConfiguration config, IUserRepository userRepository)
    {

        _userRepository = userRepository;

        _mapper = new Mapper(new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<UserToAddDto, User>();
        }));
    }



    [HttpGet("GetUsers")]
    // public IEnumerable<User> GetUsers()
    public IEnumerable<User> GetUsers()
    {
        IEnumerable<User> users = _userRepository.GetUsers();
        return users;
    }


    [HttpGet("GetSingleUsers/{userId}")]
    public User GetSingleUsers(int userId)
    {
        return _userRepository.GetSingleUsers(userId);
    }
    // {
    //     User user = _entityFramework.Users
    //           .Where(u => u.UserId == userId)
    //           .FirstOrDefault<User>();

    //     if(user != null)
    //         {
    //             return  user;
    //         }
    //         throw new Exception("Failed to get user!");
    // }


    ///      UPDATE user
    [HttpPut("EditUser")]
    public IActionResult EditUser(User user)
    {
        User userDb = _userRepository.GetSingleUsers(user.UserId);

        if (userDb != null)
        {
            userDb.Active = user.Active;
            userDb.FirstName = user.FirstName;
            userDb.LastName = user.LastName;
            userDb.Gender = user.Gender;
            userDb.Email = user.Email;
            if (_userRepository.SaveChanges())
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

        _userRepository.AddEntity<User>(userDb);

        if (_userRepository.SaveChanges())
        {
            return Ok();
        }
        throw new Exception("Failed to Add user!");
    }
    ///     DELETE USER
    [HttpDelete("DeleteUsers/{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        User userDb = _userRepository.GetSingleUsers(userId);

        if (userDb != null)
        {
            _userRepository.RemoveEntity<User>(userDb);

            if (_userRepository.SaveChanges())
            {
                return Ok();
            }
            throw new Exception("Failed to Delete user!");
        }
        throw new Exception("Failed to Get User");
    }
    ////////////// get SALARY 
    ///
    [HttpGet("GetSalary/{userId}")]
    public UserSalary GetSalary(int userId)
    {
        return _userRepository.GetSingleUsersSalary(userId);
    }
    //////////////   ADD new SALARY
    ///
    [HttpPost("AddSalary")]
    public IActionResult PostSalary(UserSalary userSalaryAdd)
    {
        _userRepository.AddEntity<UserSalary>(userSalaryAdd);
        if (_userRepository.SaveChanges())
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
        UserSalary userSalaryToEdit = _userRepository.GetSingleUsersSalary(userSalaryEdit.UserId);

        if (userSalaryToEdit != null)
        {
            userSalaryToEdit.Salary = userSalaryEdit.Salary;
            if (_userRepository.SaveChanges())
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
    public IActionResult DeleteSalary(int userId)
    {
        UserSalary userDelete = _userRepository.GetSingleUsersSalary(userId);

        if (userDelete != null)
        {
            _userRepository.RemoveEntity<UserSalary>(userDelete);

            if (_userRepository.SaveChanges())
            {
                return Ok();
            }
            throw new Exception("Failed to Delete Salary!");
        }
        throw new Exception("Failed to find salary for delete!");
    }
    ///////////// get JOB INFO
    ///
    [HttpGet("GetJobInfo/{userId}")]
    public UserJobInfo GetJobInfo(int userId)
    {
        return _userRepository.GetSingleUsersJobInfo(userId);
        throw new Exception("Failed to get fob info!");
    }
    //////////////   ADD new JOB INFO
    ///
    [HttpPost("AddJobInfo")]
    public IActionResult PostJobInfo(UserJobInfo userJonInfoAdd)
    {
        _userRepository.AddEntity<UserJobInfo>(userJonInfoAdd);
        if (_userRepository.SaveChanges())
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
        UserJobInfo infoJob = _userRepository.GetSingleUsersJobInfo(editJobInfo.UserId);

        if (infoJob != null)
        {
            infoJob.Department = editJobInfo.Department;
            infoJob.JobTitle = editJobInfo.JobTitle;

            if (_userRepository.SaveChanges())
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
    public IActionResult DeleteJobInfo(int userId)
    {
        UserJobInfo jobInfoDelete = _userRepository.GetSingleUsersJobInfo(userId);

        if (jobInfoDelete != null)
        {
            _userRepository.RemoveEntity<UserJobInfo>(jobInfoDelete);

            if (_userRepository.SaveChanges())
            {
                return Ok();
            }
            throw new Exception("Failed to delete Job Information!");
        }
        throw new Exception("Failed to find job info for delete!");
    }
}
