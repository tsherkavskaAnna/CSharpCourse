using System.Globalization;
using DataAPI;
using DataAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{

    DataContextDapper _dapper;
    public UserController(IConfiguration config)
    {
        _dapper = new DataContextDapper(config);
    }

    [HttpGet("TestConnection")]
    public DateTime TestConnection()
    {
        return _dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
    }


    [HttpGet("GetUsers")]
    // public IEnumerable<User> GetUsers()
    public IEnumerable<User> GetUsers()
    {
        string sql = @"
        SELECT [UserId],
               [FirstName],
               [LastName],
               [Email],
               [Gender],
               [Active] 
        FROM TutorialAppSchema.Users";

        IEnumerable<User> users = _dapper.LoadData<User>(sql);
        return users;
    }


    [HttpGet("GetSingleUsers/{userId}")]
    public User GetSingleUsers( int userId)
    {
        string sql = @"
        SELECT [UserId],
               [FirstName],
               [LastName],
               [Email],
               [Gender],
               [Active] 
        FROM TutorialAppSchema.Users
        WHERE UserId = " + userId.ToString();

        User user = _dapper.LoadDataSingle<User>(sql);
        return user;
    }

    [HttpPut("EditUser")]
    public IActionResult EditUser(User user)
    {
        string sql = @"
        UPDATE TutorialAppSchema.Users
            SET [FirstName] = '" + user.FirstName + 
            "',[LastName] = '" + user.LastName + 
            "',[Email] = '" + user.Email + 
            "',[Gender] = '" + user.Gender + 
            "',[Active] = '" + user.Active + 
            "' WHERE UserId = " + user.UserId;

            if(_dapper.ExecuteSgl(sql))
            {
                return  Ok();
            }
            throw new Exception("Failed to Update user!");
        
    }

    [HttpPost("AddUser")]
    public IActionResult AddUser(UserToAddDto user)
    {
        string sql = @"INSERT INTO TutorialAppSchema.Users(
               [FirstName],
               [LastName],
               [Email],
               [Gender],
               [Active] 
    ) VALUES (" +
            "'" + user.FirstName + 
            "', '" + user.LastName + 
            "', '" + user.Email + 
            "', '" + user.Gender + 
            "', '" + user.Active + 
        "')";
Console.WriteLine(sql);

        if(_dapper.ExecuteSgl(sql))
            {
                return  Ok();
            }
            throw new Exception("Failed to Add user!");
    }


    [HttpDelete("DeleteUsers/{userId}")]
    public IActionResult DeleteUser( int userId)
    {
        string sql = @"
               DELETE FROM TutorialAppSchema.Users
                       WHERE UserId = " + userId.ToString();

        Console.WriteLine(sql);

        if(_dapper.ExecuteSgl(sql))
            {
                return  Ok();
            }
            throw new Exception("Failed to Delete user!");
    }
////////        SALARY  get

[HttpGet("GetUserSalary/{userId}")]
   public IEnumerable<UserSalary> GetUserSalary( int userId)
    {
        string sql = @"
        SELECT [UserId],
               [Salary] 
        FROM TutorialAppSchema.UserSalary
        WHERE UserId = " + userId;
        return _dapper.LoadData<UserSalary>(sql);
    }
/////////     SALARY EDIT
///
[HttpPut("EditSalary")]
    public IActionResult EditSalary(UserSalary userSalary)
    {
        string sql = @"
        UPDATE TutorialAppSchema.UserSalary
            SET Salary = " + userSalary.Salary.ToString("0.00", CultureInfo.InvariantCulture) + 
            " WHERE UserId =" + userSalary.UserId.ToString();
        Console.WriteLine(sql);
            if(_dapper.ExecuteSgl(sql))
            {
                return  Ok(userSalary);
            }
            throw new Exception("Failed to Update userSalary!");     
            
    }
////////      ADD new Salary
///
[HttpPost("PostUserSalary")]
    public IActionResult PostUserSalary(UserSalary userSalary)
    {
        string sql = @"
            INSERT INTO TutorialAppSchema.UserSalary(
               UserId,
               Salary
    ) VALUES (" + userSalary.UserId.ToString() + 
            ", " + userSalary.Salary.ToString("0.00", CultureInfo.InvariantCulture) + 
        ")";
    Console.WriteLine(sql);

        if(_dapper.ExecuteSqlWithRowsCount(sql) > 0 )
            {
                return  Ok(userSalary);
            }
            throw new Exception("Failed to Add UserSalary!");
    }
///////     /DELETE salary
///
[HttpDelete("DeleteSalary/{userId}")]
    public IActionResult DeleteSalary( int userId)
    {
        string sql = @"
               DELETE FROM TutorialAppSchema.UserSalary
                       WHERE UserId = " + userId.ToString();

        Console.WriteLine(sql);

        if(_dapper.ExecuteSgl(sql))
            {
                return  Ok();
            }
            throw new Exception("Failed to Delete salary!");
    }

////////////Information about job and user
///get all info
///


}



