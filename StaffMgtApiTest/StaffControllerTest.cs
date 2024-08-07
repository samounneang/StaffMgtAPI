using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StaffMgtApi.Controllers;
using StaffMgtApi.Data;
using StaffMgtApi.Models;
namespace StaffMgtApiTest
{
    public class StaffControllerTest
    {
        private readonly StaffController _controller;
        private readonly StaffMgtApiContext _context;
        public StaffControllerTest()
        {
            var options = new DbContextOptionsBuilder<StaffMgtApiContext>()
                .UseInMemoryDatabase(databaseName: "StaffMgtDatabase")
                .Options;

            _context = new StaffMgtApiContext(options);

            // Seed the database with test data
            SeedDatabase();

            _controller = new StaffController(_context);
        }

        private void SeedDatabase()
        {
            _context.StaffModel.RemoveRange(_context.StaffModel);
            _context.SaveChanges();
            _context.StaffModel.AddRange(new List<StaffModel>
            {
                new StaffModel { StaffId = "DCC001", FullName = "SOK HENG" },
                new StaffModel { StaffId = "DCC002", FullName = "Mey Mey" },
                new StaffModel { StaffId = "DCC003", FullName = "Sok Chan" },
                new StaffModel { StaffId = "DCC004", FullName = "Leng Heng" },
            });
            _context.SaveChanges();
        }

        [Fact]
        public async Task Get_ReturnsAllStaff()
        {
            // Act
            var result = await _controller.Get();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<StaffModel>>>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<StaffModel>>(actionResult.Value);
            Assert.Equal(4, model.Count());
        }

        // not use      
        public async Task GetStaff_ReturnsStaff_WhenStaffExists()
        {
            // Act
            var result = await _controller.GetStaff("DCC001");

            // Assert
            var actionResult = Assert.IsType<ActionResult<StaffModel>>(result);
            var model = Assert.IsType<StaffModel>(actionResult.Value);
            Assert.Equal("DCC001", model.StaffId);
        }

        [Fact]
        public async Task CreateStaff_ReturnsConflict_WhenStaffIdExists()
        {
            // Arrange
            var staff = new StaffModel { StaffId = "DCC001", FullName = "SOK HENG" };

            // Act
            var result = await _controller.CreateStaff(staff);



            // Assert
            var conflictResult = Assert.IsType<ConflictObjectResult>(result.Result);
            Assert.Equal("{ message = Staff ID already exists. }", conflictResult.Value.ToString());
        }

        [Fact]
        public async Task UpdateStaff_ReturnsNotFound_WhenStaffNotExists()
        {
            // Arrange
            var staff = new StaffModel { StaffId = "3", FullName = "Non Existent" };

            // Act
            var result = await _controller.UpdateStaff("3", staff);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task DeleteStaff_ReturnsNotFound_WhenStaffNotExists()
        {
            // Act
            var result = await _controller.DeleteStaff("3");

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteStaff_ReturnsNoContent_WhenStaffDeleted()
        {
            // Act
            var result = await _controller.DeleteStaff("DCC001");

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        //[Theory]
        //[InlineData(-1)]
        //[InlineData(0)]
        //[InlineData(1)]
        //public void IsPrime_ValuesLessThan2_ReturnFalse(int value)
        //{
        //    var result = value==1?true:false;

        //    Assert.False(result, $"{value} should not be prime");
        //}
    }
}