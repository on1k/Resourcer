using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Resourcer.Controllers;
using Resourcer.Data.DB;
using Resourcer.Data.DB.Models;
using Resourcer.Data.DTO;
using Moq.EntityFrameworkCore;
using Resourcer.Test.Helpers;

namespace Resourcer.Test.Tests;

public class Requests_Tests
{
    [Fact]
    public async Task Get_Grant_Access()
    {
        //Arrange
        var repo = new Mock<DataContext>();
        repo.Setup<DbSet<Resource>>(t => t.Resources)
            .ReturnsDbSet(DataBaseProvider.GetResources());
        var controller = new ResourceController(repo.Object);
        
        //Act
        var result = await controller.GetResource(new Request { Resource = "res1" });
        var okResult = result as OkObjectResult;
        var resultObject = okResult.Value as Response;
        
        //Assert
        Assert.Equal(200, okResult.StatusCode);
        Assert.Equal("Grant", resultObject.Decision);
    }
}