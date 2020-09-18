using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeckofcardsApi.TestData
{
    class TestDataHelper
    {
        public static IEnumerable<TestCaseData> UserTestFixtureData
        {
            get
            {
                yield return new TestCaseData(true, Constants.withJokerRemainingValue).SetArgDisplayNames("New Deck with Jocker");
                yield return new TestCaseData(false, Constants.withOutJokerRemainingValue).SetArgDisplayNames("New Deck without Jocker");
            }
        }
        public static IEnumerable<TestCaseData> DrawingTestFixtureData
        {
            get
            {
                yield return new TestCaseData(true, Constants.withJokerRemainingValue, 1);
                yield return new TestCaseData(false, Constants.withOutJokerRemainingValue, 1);
                yield return new TestCaseData(true, Constants.withJokerRemainingValue, 20);
                yield return new TestCaseData(false, Constants.withOutJokerRemainingValue, 30);
            }
        }
    }
}
