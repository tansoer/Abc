using System;
using Abc.Aids.Random;
using Abc.Aids.Reflection;
using Abc.Core.Rounding;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Core.Rounding {

    [TestClass]
    public class RoundingPolicyTests : SealedTests<RoundingPolicy, object> {


        [TestMethod]
        public void IsPositiveTest() {
            Assert.IsTrue(RoundingPolicy.isPositive(GetRandom.Double(0)));
            Assert.IsFalse(RoundingPolicy.isPositive(GetRandom.Double(max: 0)));
            Assert.IsTrue(RoundingPolicy.isPositive(123.456));
            Assert.IsFalse(RoundingPolicy.isPositive(-123.456));
            Assert.IsTrue(RoundingPolicy.isPositive(0));
        }

        [TestMethod]
        public void FloorTest() {
            Assert.AreEqual(123, RoundingPolicy.floor(123.456));
            Assert.AreEqual(-124, RoundingPolicy.floor(-123.456));
        }

        [TestMethod]
        public void CeilingTest() {
            Assert.AreEqual(124, RoundingPolicy.ceiling(123.456));
            Assert.AreEqual(-123, RoundingPolicy.ceiling(-123.456));
        }

        [TestMethod]
        public void CoeficientTest() {
            Assert.AreEqual(1E-10, RoundingPolicy.coeficient(-10));
            Assert.AreEqual(0.01, RoundingPolicy.coeficient(-2));
            Assert.AreEqual(0.1, RoundingPolicy.coeficient(-1));
            Assert.AreEqual(1, RoundingPolicy.coeficient(0));
            Assert.AreEqual(10, RoundingPolicy.coeficient(1));
            Assert.AreEqual(100, RoundingPolicy.coeficient(2));
            var d = GetRandom.Int8();
            Assert.AreEqual(Math.Pow(10, d), RoundingPolicy.coeficient(d));
        }

        [TestMethod]
        public void DigitTest() {
            Assert.AreEqual(6, RoundingPolicy.digit(123.456, 2));
            Assert.AreEqual(5, RoundingPolicy.digit(123.456, 1));
            Assert.AreEqual(4, RoundingPolicy.digit(123.456, 0));
            Assert.AreEqual(3, RoundingPolicy.digit(123.456, -1));
            Assert.AreEqual(2, RoundingPolicy.digit(123.456, -2));
            Assert.AreEqual(6, RoundingPolicy.digit(-123.456, 2));
            Assert.AreEqual(5, RoundingPolicy.digit(-123.456, 1));
            Assert.AreEqual(4, RoundingPolicy.digit(-123.456, 0));
            Assert.AreEqual(3, RoundingPolicy.digit(-123.456, -1));
            Assert.AreEqual(2, RoundingPolicy.digit(-123.456, -2));
        }

        [TestMethod]
        public void IsRoundByStepTest() {
            Assert.IsTrue(RoundingPolicy.isRoundByStep(RoundingStrategy.RoundDownByStep));
            Assert.IsTrue(RoundingPolicy.isRoundByStep(RoundingStrategy.RoundUpByStep));
            Assert.IsFalse(RoundingPolicy.isRoundByStep(RoundingStrategy.RoundDown));
            Assert.IsFalse(RoundingPolicy.isRoundByStep(RoundingStrategy.RoundUp));
            Assert.IsFalse(RoundingPolicy.isRoundByStep(RoundingStrategy.Round));
            Assert.IsFalse(RoundingPolicy.isRoundByStep(RoundingStrategy.RoundTowardsNegative));
            Assert.IsFalse(RoundingPolicy.isRoundByStep(RoundingStrategy.RoundTowardsPositive));
        }

        [TestMethod]
        public void RoundingPolicyDefaultTest() {
            testRoundingPolicy(obj);
        }

        private static void testRoundingPolicy(RoundingPolicy o,
            RoundingStrategy roundingStrategy = RoundingStrategy.Round,
            sbyte numberOfDigits = 2,
            byte roundingDigit = 5,
            double roundingStep = 0) {
            Assert.AreEqual(roundingStrategy, o.RoundingStrategy);
            Assert.AreEqual(numberOfDigits, o.NumberOfDigits);
            Assert.AreEqual(roundingDigit, o.RoundingDigit);
            Assert.AreEqual(roundingStep, o.RoundingStep);
        }

        [TestMethod]
        public void RoundingPolicyRoundingStepTest() {
            testRoundingPolicy(new RoundingPolicy(RoundingStrategy.Round, 0.25));
            testRoundingPolicy(new RoundingPolicy(RoundingStrategy.RoundUp, 0.25));
            testRoundingPolicy(new RoundingPolicy(RoundingStrategy.RoundDown, 0.25));
            testRoundingPolicy(new RoundingPolicy(RoundingStrategy.RoundTowardsNegative, 0.25));
            testRoundingPolicy(new RoundingPolicy(RoundingStrategy.RoundTowardsPositive, 0.25));
            testRoundingPolicy(new RoundingPolicy(RoundingStrategy.RoundUpByStep, 0.25), RoundingStrategy.RoundUpByStep, roundingStep: 0.25);
            testRoundingPolicy(new RoundingPolicy(RoundingStrategy.RoundDownByStep, 0.25), RoundingStrategy.RoundDownByStep, roundingStep: 0.25);

        }

        [TestMethod]
        public void RoundingPolicyNumberOfDigitsTest() {
            testRoundingPolicy(new RoundingPolicy(RoundingStrategy.Round, 4), numberOfDigits: 4);
            testRoundingPolicy(new RoundingPolicy(RoundingStrategy.RoundUp, 5), RoundingStrategy.RoundUp, 5);
            testRoundingPolicy(new RoundingPolicy(RoundingStrategy.RoundDown, 6), RoundingStrategy.RoundDown, 6);
            testRoundingPolicy(new RoundingPolicy(RoundingStrategy.RoundTowardsNegative, 7), RoundingStrategy.RoundTowardsNegative, 7);
            testRoundingPolicy(new RoundingPolicy(RoundingStrategy.RoundTowardsPositive, 8), RoundingStrategy.RoundTowardsPositive, 8);
            testRoundingPolicy(new RoundingPolicy(RoundingStrategy.RoundUpByStep, 9));
            testRoundingPolicy(new RoundingPolicy(RoundingStrategy.RoundDownByStep, 10));
        }

        [TestMethod]
        public void RoundingPolicyRoundingDigitTest() {
            testRoundingPolicy(new RoundingPolicy(RoundingStrategy.Round, 4, 1), numberOfDigits: 4, roundingDigit: 1);
            testRoundingPolicy(new RoundingPolicy(RoundingStrategy.RoundUp, 5, 2));
            testRoundingPolicy(new RoundingPolicy(RoundingStrategy.RoundDown, 6, 3));
            testRoundingPolicy(new RoundingPolicy(RoundingStrategy.RoundTowardsNegative, 7, 4));
            testRoundingPolicy(new RoundingPolicy(RoundingStrategy.RoundTowardsPositive, 8, 6));
            testRoundingPolicy(new RoundingPolicy(RoundingStrategy.RoundUpByStep, 9, 7));
            testRoundingPolicy(new RoundingPolicy(RoundingStrategy.RoundDownByStep, 10, 8));
        }

        [TestMethod]
        public void RoundingStrategyTest() {
            isReadOnly(obj, GetMember.Name<RoundingPolicy>(x => x.RoundingStrategy), RoundingStrategy.Round);
        }

        [TestMethod]
        public void NumberOfDigitsTest() {
            isReadOnly(obj, GetMember.Name<RoundingPolicy>(x => x.NumberOfDigits), (sbyte) 2);
        }

        [TestMethod]
        public void RoundingStepTest() {
            isReadOnly(obj, GetMember.Name<RoundingPolicy>(x => x.RoundingStep), (double) 0);
        }

        [TestMethod]
        public void RoundingDigitTest() {
            isReadOnly(obj, GetMember.Name<RoundingPolicy>(x => x.RoundingDigit), (byte) 5);
        }

        [TestMethod]
        public void RoundTest() {
            Assert.AreEqual(4.5, new RoundingPolicy(RoundingStrategy.RoundUp, 1).Round(4.45));
            Assert.AreEqual(4.46, new RoundingPolicy(RoundingStrategy.RoundUp, 2).Round(4.456));
            Assert.AreEqual(-4.5, new RoundingPolicy(RoundingStrategy.RoundUp, 1).Round(-4.45));
            Assert.AreEqual(-4.46, new RoundingPolicy(RoundingStrategy.RoundUp, 2).Round(-4.456));
            Assert.AreEqual(1400, new RoundingPolicy(RoundingStrategy.RoundUp, 2).Round(1400.00));
            Assert.AreEqual(4.4, new RoundingPolicy(RoundingStrategy.RoundDown, 1).Round(4.45));
            Assert.AreEqual(4.45, new RoundingPolicy(RoundingStrategy.RoundDown, 2).Round(4.456));
            Assert.AreEqual(-4.4, new RoundingPolicy(RoundingStrategy.RoundDown, 1).Round(-4.45));
            Assert.AreEqual(-4.45, new RoundingPolicy(RoundingStrategy.RoundDown, 2).Round(-4.456));
            Assert.AreEqual(1400, new RoundingPolicy(RoundingStrategy.RoundDown, 1).Round(1400.00));
            Assert.AreEqual(4.5, new RoundingPolicy(RoundingStrategy.Round, 1, 5).Round(4.45));
            Assert.AreEqual(4.45, new RoundingPolicy(RoundingStrategy.Round, 2, 7).Round(4.456));
            Assert.AreEqual(-4.5, new RoundingPolicy(RoundingStrategy.Round, 1, 5).Round(-4.45));
            Assert.AreEqual(-4.45, new RoundingPolicy(RoundingStrategy.Round, 2, 7).Round(-4.456));
            Assert.AreEqual(0.01, new RoundingPolicy(RoundingStrategy.Round, 5, 5).Round(0.01));
            Assert.AreEqual(0.01, new RoundingPolicy(RoundingStrategy.Round, 5, 5).Round(0.0100000000002));
            Assert.AreEqual(1, new RoundingPolicy(RoundingStrategy.Round, 0, 5).Round(0.99999999999999989));
            Assert.AreEqual(4.5, new RoundingPolicy(RoundingStrategy.RoundUpByStep, 0.25).Round(4.45));
            Assert.AreEqual(-4.5, new RoundingPolicy(RoundingStrategy.RoundUpByStep, 0.25).Round(-4.45));
            Assert.AreEqual(4.25, new RoundingPolicy(RoundingStrategy.RoundDownByStep, 0.25).Round(4.45));
            Assert.AreEqual(-4.25, new RoundingPolicy(RoundingStrategy.RoundDownByStep, 0.25).Round(-4.45));
            Assert.AreEqual(4.5,
                new RoundingPolicy(RoundingStrategy.RoundTowardsPositive, 1).Round(4.45));
            Assert.AreEqual(4.46,
                new RoundingPolicy(RoundingStrategy.RoundTowardsPositive, 2).Round(4.456));
            Assert.AreEqual(-4.4,
                new RoundingPolicy(RoundingStrategy.RoundTowardsPositive, 1).Round(-4.45));
            Assert.AreEqual(-4.45,
                new RoundingPolicy(RoundingStrategy.RoundTowardsPositive, 2).Round(-4.456));
            Assert.AreEqual(4.4,
                new RoundingPolicy(RoundingStrategy.RoundTowardsNegative, 1).Round(4.45));
            Assert.AreEqual(4.45,
                new RoundingPolicy(RoundingStrategy.RoundTowardsNegative, 2).Round(4.456));
            Assert.AreEqual(-4.5,
                new RoundingPolicy(RoundingStrategy.RoundTowardsNegative, 1).Round(-4.45));
            Assert.AreEqual(-4.46,
                new RoundingPolicy(RoundingStrategy.RoundTowardsNegative, 2).Round(-4.456));
        }

        [TestMethod]
        public void RoundUpTest() {
            Assert.AreEqual(4.5, new RoundingPolicy(RoundingStrategy.RoundUp, 1).roundUp(4.45));
            Assert.AreEqual(4.46, new RoundingPolicy(RoundingStrategy.RoundUp, 2).roundUp(4.456));
            Assert.AreEqual(-4.5, new RoundingPolicy(RoundingStrategy.RoundUp, 1).roundUp(-4.45));
            Assert.AreEqual(-4.46, new RoundingPolicy(RoundingStrategy.RoundUp, 2).roundUp(-4.456));
            Assert.AreEqual(1400, new RoundingPolicy(RoundingStrategy.RoundUp, 2).roundUp(1400.00));
        }

        [TestMethod]
        public void RoundDownTest() {
            Assert.AreEqual(4.4, new RoundingPolicy(RoundingStrategy.RoundDown, 1).roundDown(4.45));
            Assert.AreEqual(4.45, new RoundingPolicy(RoundingStrategy.RoundDown, 2).roundDown(4.456));
            Assert.AreEqual(-4.4, new RoundingPolicy(RoundingStrategy.RoundDown, 1).roundDown(-4.45));
            Assert.AreEqual(-4.45, new RoundingPolicy(RoundingStrategy.RoundDown, 2).roundDown(-4.456));
            Assert.AreEqual(1400, new RoundingPolicy(RoundingStrategy.RoundDown, 1).roundDown(1400.00));
        }

        [TestMethod]
        public void InternalRoundTest() {
            Assert.AreEqual(4.5, new RoundingPolicy(RoundingStrategy.Round, 1, 5).round(4.45));
            Assert.AreEqual(4.45, new RoundingPolicy(RoundingStrategy.Round, 2, 7).round(4.456));
            Assert.AreEqual(-4.5, new RoundingPolicy(RoundingStrategy.Round, 1, 5).round(-4.45));
            Assert.AreEqual(-4.45, new RoundingPolicy(RoundingStrategy.Round, 2, 7).round(-4.456));
            Assert.AreEqual(0.01, new RoundingPolicy(RoundingStrategy.Round, 5, 5).round(0.01));
            Assert.AreEqual(0.01, new RoundingPolicy(RoundingStrategy.Round, 5, 5).round(0.0100000000002));
            Assert.AreEqual(1, new RoundingPolicy(RoundingStrategy.Round, 0, 5).round(0.99999999999999989));
        }

        [TestMethod]
        public void RoundUpByStepTest() {
            Assert.AreEqual(4.5, new RoundingPolicy(RoundingStrategy.RoundUpByStep, 0.25).roundUpByStep(4.45));
            Assert.AreEqual(-4.5, new RoundingPolicy(RoundingStrategy.RoundUpByStep, 0.25).roundUpByStep(-4.45));
        }

        [TestMethod]
        public void RoundDownByStepTest() {
            Assert.AreEqual(4.25, new RoundingPolicy(RoundingStrategy.RoundDownByStep, 0.25).roundDownByStep(4.45));
            Assert.AreEqual(-4.25, new RoundingPolicy(RoundingStrategy.RoundDownByStep, 0.25).roundDownByStep(-4.45));
        }

        [TestMethod]
        public void RoundTowardsPositiveTest() {
            Assert.AreEqual(4.5,
                new RoundingPolicy(RoundingStrategy.RoundTowardsPositive, 1).roundTowardsPositive(4.45));
            Assert.AreEqual(4.46,
                new RoundingPolicy(RoundingStrategy.RoundTowardsPositive, 2).roundTowardsPositive(4.456));
            Assert.AreEqual(-4.4,
                new RoundingPolicy(RoundingStrategy.RoundTowardsPositive, 1).roundTowardsPositive(-4.45));
            Assert.AreEqual(-4.45,
                new RoundingPolicy(RoundingStrategy.RoundTowardsPositive, 2).roundTowardsPositive(-4.456));
        }

        [TestMethod]
        public void RoundTowardsNegativeTest() {
            Assert.AreEqual(4.4,
                new RoundingPolicy(RoundingStrategy.RoundTowardsNegative, 1).roundTowardsNegative(4.45));
            Assert.AreEqual(4.45,
                new RoundingPolicy(RoundingStrategy.RoundTowardsNegative, 2).roundTowardsNegative(4.456));
            Assert.AreEqual(-4.5,
                new RoundingPolicy(RoundingStrategy.RoundTowardsNegative, 1).roundTowardsNegative(-4.45));
            Assert.AreEqual(-4.46,
                new RoundingPolicy(RoundingStrategy.RoundTowardsNegative, 2).roundTowardsNegative(-4.456));
        }

    }

}
