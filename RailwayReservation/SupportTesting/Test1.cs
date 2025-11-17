using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RailwayReservation.Controllers;
using RailwayReservation.Interfaces;
using RailwayReservation.Models;
using RailwayReservation.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[TestClass]
public class SupportControllerTests
{
    private Mock<ISupport> _mockSupportRepository;
    private SupportController _controller;

    [TestInitialize]
    public void Setup()
    {
        _mockSupportRepository = new Mock<ISupport>();
        _controller = new SupportController(_mockSupportRepository.Object);
    }

    [TestMethod]
    public async Task GetAllSupports_ReturnsOkResult_WithSupportList()
    {
        // Arrange
        var mockSupports = new List<Support>
        {
            new Support { SupportId = "1", UserId = "100", QueryListId = "A1", QueryText = "Issue 1", Status = "Pending" },
            new Support { SupportId = "2", UserId = "101", QueryListId = "A2", QueryText = "Issue 2", Status = "Resolved" }
        };

        _mockSupportRepository.Setup(repo => repo.GetAllSupportsAsync()).ReturnsAsync(mockSupports);

        // Act
        var result = await _controller.GetAllSupports();

        // Assert
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        var returnedSupports = okResult.Value as List<Support>;
        Assert.AreEqual(2, returnedSupports.Count);
    }

    [TestMethod]
    public async Task GetAllSupports_ReturnsServerError_OnException()
    {
        // Arrange
        _mockSupportRepository.Setup(repo => repo.GetAllSupportsAsync()).ThrowsAsync(new Exception("Database error"));

        // Act
        var result = await _controller.GetAllSupports();

        // Assert
        var statusCodeResult = result as ObjectResult;
        Assert.IsNotNull(statusCodeResult);
        Assert.AreEqual(500, statusCodeResult.StatusCode);
    }

    [TestMethod]
    public async Task GetSupportById_ReturnsOkResult_WithSupportRecord()
    {
        // Arrange
        var support = new Support { SupportId = "1", UserId = "100", QueryListId = "A1", QueryText = "Issue", Status = "Pending" };

        _mockSupportRepository.Setup(repo => repo.GetSupportByIdAsync("1")).ReturnsAsync(support);

        // Act
        var result = await _controller.GetSupportById("1");

        // Assert
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        var returnedSupport = okResult.Value as Support;
        Assert.AreEqual("1", returnedSupport.SupportId);
    }

    [TestMethod]
    public async Task GetSupportById_ReturnsNotFound_WhenRecordDoesNotExist()
    {
        // Arrange
        _mockSupportRepository.Setup(repo => repo.GetSupportByIdAsync("1")).ReturnsAsync((Support)null);

        // Act
        var result = await _controller.GetSupportById("1");

        // Assert
        var notFoundResult = result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
    }

    [TestMethod]
    public async Task AddSupport_ReturnsCreatedAtAction_WhenSuccessful()
    {
        // Arrange
        var supportRequest = new SupportRequest { UserId = "100", QueryListId = "A1", QueryText = "Issue", Status = "Pending" };
        var support = new Support { SupportId = "1", UserId = "100", QueryListId = "A1", QueryText = "Issue", Status = "Pending" };

        _mockSupportRepository.Setup(repo => repo.AddSupportAsync(It.IsAny<Support>())).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.AddSupport(supportRequest);

        // Assert
        var createdResult = result as CreatedAtActionResult;
        Assert.IsNotNull(createdResult);
        Assert.IsInstanceOfType(createdResult.Value, typeof(Support));
    }

    [TestMethod]
    public async Task UpdateSupport_ReturnsNoContent_WhenSuccessful()
    {
        // Arrange
        var existingSupport = new Support { SupportId = "1", UserId = "100", QueryListId = "A1", QueryText = "Old Issue", Status = "Pending" };
        var updatedRequest = new SupportRequest { QueryListId = "A2", QueryText = "Updated Issue", Status = "Resolved" };

        _mockSupportRepository.Setup(repo => repo.GetSupportByIdAsync("1")).ReturnsAsync(existingSupport);
        _mockSupportRepository.Setup(repo => repo.UpdateSupportAsync(existingSupport)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.UpdateSupport("1", updatedRequest);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NoContentResult));
    }

    [TestMethod]
    public async Task UpdateSupport_ReturnsNotFound_WhenRecordDoesNotExist()
    {
        // Arrange
        _mockSupportRepository.Setup(repo => repo.GetSupportByIdAsync("1")).ReturnsAsync((Support)null);

        // Act
        var result = await _controller.UpdateSupport("1", new SupportRequest());

        // Assert
        var notFoundResult = result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
    }

    [TestMethod]
    public async Task DeleteSupport_ReturnsNoContent_WhenSuccessful()
    {
        // Arrange
        var support = new Support { SupportId = "1", UserId = "100", QueryListId = "A1", QueryText = "Issue", Status = "Pending" };

        _mockSupportRepository.Setup(repo => repo.GetSupportByIdAsync("1")).ReturnsAsync(support);
        _mockSupportRepository.Setup(repo => repo.DeleteSupportAsync("1")).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.DeleteSupport("1");

        // Assert
        Assert.IsInstanceOfType(result, typeof(NoContentResult));
    }

    [TestMethod]
    public async Task DeleteSupport_ReturnsNotFound_WhenRecordDoesNotExist()
    {
        // Arrange
        _mockSupportRepository.Setup(repo => repo.GetSupportByIdAsync("1")).ReturnsAsync((Support)null);

        // Act
        var result = await _controller.DeleteSupport("1");

        // Assert
        var notFoundResult = result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
    }
}
