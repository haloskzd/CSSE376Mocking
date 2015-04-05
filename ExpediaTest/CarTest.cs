using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Expedia;
using System.Collections.Generic;
using Rhino.Mocks;

namespace ExpediaTest
{
	[TestClass]
	public class CarTest
	{	
		private Car targetCar;
		private MockRepository mocks;
		
		[TestInitialize]
		public void TestInitialize()
		{
			targetCar = new Car(5);
			mocks = new MockRepository();
		}
		
		[TestMethod]
		public void TestThatCarInitializes()
		{
			Assert.IsNotNull(targetCar);
		}	
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForFiveDays()
		{
			Assert.AreEqual(50, targetCar.getBasePrice()	);
		}
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForTenDays()
		{
            var target = new Car(10);
			Assert.AreEqual(80, target.getBasePrice());	
		}
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForSevenDays()
		{
			var target = new Car(7);
			Assert.AreEqual(10*7*.8, target.getBasePrice());
		}
		
		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestThatCarThrowsOnBadLength()
		{
			new Car(-5);
		}

        [TestMethod]
        public void TestThatCarDoesGetCarLocationFromDatabase()
        {
            IDatabase mockDB = mocks.StrictMock<IDatabase>();
            String carLocation = "Panama";
            String anotherCarLocation = "Terre Haute";
            Expect.Call(mockDB.getCarLocation(100)).Return(carLocation);
            Expect.Call(mockDB.getCarLocation(200)).Return(anotherCarLocation);
            mocks.ReplayAll();
            Car target = new Car(7);
            target.Database = mockDB;
            String result;
            result = target.getCarLocation(200);
            Assert.AreEqual(anotherCarLocation, result);
            result = target.getCarLocation(100);
            Assert.AreEqual(carLocation, result);
            mocks.VerifyAll();
        }

        [TestMethod]
        public void TestThatMileageDoesGetCarLocationFromDatabase()
        {
            IDatabase mockDatabase = mocks.StrictMock<IDatabase>();
            int Mileage = 100;
            Expect.Call(mockDatabase.Miles).PropertyBehavior();
            mocks.ReplayAll();
            mockDatabase.Miles = Mileage;
            var target = new Car(7);
            target.Database = mockDatabase;
            int Miles = target.Mileage;
            Assert.AreEqual(Miles, Mileage);
            mocks.VerifyAll();
        }

        [TestMethod]
        public void IKnowObjectMother()
        {
            var target = ObjectMother.BMW();
            Assert.AreEqual(target.Name, "BMW i8");
        }
	}
}
