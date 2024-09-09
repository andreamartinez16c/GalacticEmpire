using GalacticEmpire.Controllers;
using GalacticEmpire.Data;
using GalacticEmpire.Models;
using GalacticEmpire.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GalacticEmpire.Tests
{
    public class HabitantRepositoryTests
    {
        // This test checks if GetAllHabitants() in the HabitantController returns an Ok response with a list of habitants
        [Fact]
        public async Task GetAllHabitants_ShouldReturnOkWithHabitants()
        {
            // Arrange: Set up the necessary components for the test
            // We create a mock (fake) implementation of the IHabitantRepository
            var mockRepo = new Mock<IHabitantRepository>();

            // Define some dummy data to simulate database data
            var dummyHabitants = new List<Habitant>
        {
            new Habitant { Id = 1, Name = "Luke Skywalker", IsRebel = true },
            new Habitant { Id = 2, Name = "Han Solo", IsRebel = true }
        };

            // Set up the mock repository to return the dummy data when GetAllHabitantsAsync() is called
            mockRepo.Setup(repo => repo.GetAllHabitantsAsync())
                    .ReturnsAsync(dummyHabitants);

            // Now we create the controller, passing the mocked repository as a dependency
            var controller = new HabitantController(mockRepo.Object);

            // Act: Call the method we are testing
            var result = await controller.GetAllHabitants();

            // Assert: Verify the results
            // Check that the result is of type OkObjectResult (HTTP 200 OK)
            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            // Check that the returned data is the list of habitants we defined earlier
            var returnedHabitants = Assert.IsType<List<Habitant>>(okResult.Value);

            // Finally, check that the returned list has the correct number of habitants (2 in this case)
            Assert.Equal(2, returnedHabitants.Count);
        }

        // This test checks if GetHabitantById() returns NotFound when an invalid ID is provided
        [Fact]
        public async Task GetHabitantById_InvalidId_ShouldReturnNotFound()
        {
            // Arrange: Set up the mock repository
            var mockRepo = new Mock<IHabitantRepository>();

            // Mock repository to return null when an invalid ID is passed
            mockRepo.Setup(repo => repo.GetHabitantByIdAsync(It.IsAny<int>()))
                    .ReturnsAsync((Habitant)null); // Simulates "not found" behavior

            // Create controller
            var controller = new HabitantController(mockRepo.Object);

            // Act: Call the method with an invalid ID (e.g., 999)
            var result = await controller.GetHabitantById(999);

            // Assert: Check that the result is a NotFoundResult (HTTP 404)
            Assert.IsType<NotFoundResult>(result.Result);
        }

        // This test checks if GetHabitantById() returns Ok when a valid ID is provided
        [Fact]
        public async Task GetHabitantById_ValidId_ShouldReturnOkWithHabitant()
        {
            // Arrange: Set up the mock repository
            var mockRepo = new Mock<IHabitantRepository>();

            // Define a dummy habitant that will be returned when a valid ID is passed
            var dummyHabitant = new Habitant { Id = 1, Name = "Luke Skywalker", IsRebel = true };

            // Set up the mock repository to return the dummy habitant when called with ID 1
            mockRepo.Setup(repo => repo.GetHabitantByIdAsync(1))
                    .ReturnsAsync(dummyHabitant);

            // Create controller
            var controller = new HabitantController(mockRepo.Object);

            // Act: Call the method with a valid ID (1 in this case)
            var result = await controller.GetHabitantById(1);

            // Assert: Check that the result is of type OkObjectResult (HTTP 200 OK)
            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            // Verify that the returned value is the habitant we defined earlier
            var returnedHabitant = Assert.IsType<Habitant>(okResult.Value);
            Assert.Equal("Luke Skywalker", returnedHabitant.Name);
        }

        // This test checks if AddHabitant() adds a new habitant and returns a CreatedAtAction result
        [Fact]
        public async Task AddHabitant_ShouldReturnCreatedAtAction()
        {
            // Arrange: Set up the mock repository
            var mockRepo = new Mock<IHabitantRepository>();

            // Define a dummy habitant to be added
            var newHabitant = new Habitant { Id = 3, Name = "Leia Organa", IsRebel = true };

            // Set up the mock repository so that AddHabitantAsync does nothing (since we are mocking)
            mockRepo.Setup(repo => repo.AddHabitantAsync(It.IsAny<Habitant>()))
                    .Returns(Task.CompletedTask); // Simulates a successful insert

            // Create controller
            var controller = new HabitantController(mockRepo.Object);

            // Act: Call the method to add the new habitant
            var result = await controller.AddHabitant(newHabitant);

            // Assert: Check that the result is a CreatedAtActionResult (HTTP 201)
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);

            // Verify that the newly added habitant's name is "Leia Organa"
            var addedHabitant = Assert.IsType<Habitant>(createdAtActionResult.Value);
            Assert.Equal("Leia Organa", addedHabitant.Name);

            // Ensure that the repository's AddHabitantAsync method was called once with the new habitant
            mockRepo.Verify(repo => repo.AddHabitantAsync(It.IsAny<Habitant>()), Times.Once);
        }

        // This test checks if GetRebels() returns only rebels
        [Fact]
        public async Task GetRebels_ShouldReturnOnlyRebels()
        {
            // Arrange: Set up the mock repository
            var mockRepo = new Mock<IHabitantRepository>();

            // Define a list of habitants, some rebels and some not
            var dummyHabitants = new List<Habitant>
        {
            new Habitant { Id = 1, Name = "Luke Skywalker", IsRebel = true },
            new Habitant { Id = 2, Name = "Darth Vader", IsRebel = false },
            new Habitant { Id = 3, Name = "Leia Organa", IsRebel = true }
        };

            // Mock the repository to return only rebels when GetRebelsAsync is called
            mockRepo.Setup(repo => repo.GetRebelsAsync())
                    .ReturnsAsync(dummyHabitants.Where(h => h.IsRebel).ToList());

            // Create controller
            var controller = new HabitantController(mockRepo.Object);

            // Act: Call the method to get rebels
            var result = await controller.GetRebels();

            // Assert: Check that the result is an OkObjectResult (HTTP 200)
            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            // Verify that only the rebels are returned
            var returnedRebels = Assert.IsType<List<Habitant>>(okResult.Value);
            Assert.Equal(2, returnedRebels.Count); // Should return 2 rebels
        }
    }
}
