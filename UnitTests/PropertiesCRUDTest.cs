using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CS586MVC.Controllers;
using CS586MVC.Models;
using Xunit;
using Moq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using CS586MVC.Services;
using Microsoft.AspNetCore.JsonPatch.Internal;
using Remotion.Linq.Parsing.ExpressionVisitors.Transformation.PredefinedTransformations;
using Assert = Xunit.Assert;

namespace CS586MVC.UnitTests
{
    [TestClass]
    public class PropertiesCRUDTest
    {
        [TestMethod]
        public async Task CreateApartmentComplex()
        {
            var mockDB = new Mock<IDatabaseService>();

            bool eagerLoading = false;

            var complexes = GetTestApartmentComplexes(false);
            mockDB.Setup(repo => repo.InsertApartmentComplex(It.IsAny<ApartmentComplex>())).Verifiable();

            var controller = new PropertyDataController(mockDB.Object);

            await controller.Properties(complexes[0]);

            mockDB.Verify();
        }

        [TestMethod]
        public async Task RetrieveAllApartmentComplexes()
        {
            var mockDB = new Mock<IDatabaseService>();

            bool eagerLoading = true;

            mockDB.Setup(repo =>
                repo.AllApartmentComplexes(It.IsAny<bool>()).Result).Returns(GetTestApartmentComplexes(eagerLoading));

            var propertyDataController = new PropertyDataController(mockDB.Object);

            var properties = await propertyDataController.Properties(null, eagerLoading);

            Assert.Equal(2, properties.Count());
        }

        [TestMethod]
        public async Task Retrieve_One_ApartmentComplex()
        {
            var mockDB = new Mock<IDatabaseService>();

            bool eagerLoading = true;

            var complexes = GetTestApartmentComplexes(eagerLoading);
            mockDB.Setup(repo => repo.ApartmentComplex(It.IsAny<int>(), It.IsAny<bool>()).Result).Returns(complexes[0]);

            var controller = new PropertyDataController(mockDB.Object);

            var result = await controller.Properties(complexes[0].Id, eagerLoading);
            Assert.Equal(complexes[0].Id, result.First().Id);
        }

        [TestMethod]
        public async Task UpdateApartmentComplex()
        {
            var complexes = GetTestApartmentComplexes(false);

            ApartmentComplex other = new ApartmentComplex()
            {
                Id = complexes[0].Id,
                Name = "NEW NAME",
                Address = complexes[0].Address,
                ApartmentComplexUnits = complexes[0].ApartmentComplexUnits
            };

            var mockDB = new Mock<IDatabaseService>();
            mockDB.Setup(repo => repo.UpdateApartmentComplex(It.IsAny<int>(), It.IsAny<ApartmentComplex>())).Verifiable();

            var controller = new PropertyDataController(mockDB.Object);

            await controller.Properties(other.Id, other);

            mockDB.Verify();
        }

        [TestMethod]
        public async Task DeleteApartmentComplex()
        {
            var complexes = GetTestApartmentComplexes(false);

            var mockDB = new Mock<IDatabaseService>();

            mockDB.Setup(repo => repo.RemoveApartmentComplex(It.IsAny<int>())).Verifiable();

            var controller = new PropertyDataController(mockDB.Object);

            await controller.Properties(complexes[0].Id);

            mockDB.Verify();
        }




        /// <summary>
        /// Helper function
        /// </summary>
        /// <param name="eagerLoading"></param>
        /// <returns></returns>

        private List<ApartmentComplex> GetTestApartmentComplexes(bool eagerLoading)
        {
            ApartmentComplex c1 = new ApartmentComplex()
            {
                Name = "Test1",
                Address = "12345 Test Street",
                Id = 1,
            };

            ApartmentComplex c2 = new ApartmentComplex()
            {
                Name = "Test 2",
                Address = "!@#$% Test2 Avenue",
                Id = 2,
            };

            if (eagerLoading)
            {
                c1.ApartmentComplexUnits = new List<ApartmentComplexUnit>()
                {
                    new ApartmentComplexUnit() {Id = 1, ApartmentComplexId = 1, BathRooms = 1, BedRooms = 1}
                };

                c2.ApartmentComplexUnits = new List<ApartmentComplexUnit>()
                {
                    new ApartmentComplexUnit() {Id = 1, ApartmentComplexId = 2, BathRooms = 2, BedRooms = 2}
                };
            }

            return new List<ApartmentComplex>(){c1, c2};
        }

    }
}
