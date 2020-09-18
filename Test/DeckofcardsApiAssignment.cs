using DeckofcardsApi.TestData;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace DeckofcardsApi.Test
{
    [Parallelizable]
    class DeckofcardsApiAssignment
    {
        DeckOfcardsApiHelper DeckOfcardsApiHelper = new DeckOfcardsApiHelper();

        [Test]
        [TestCaseSource(typeof(TestDataHelper), Constants.Attribue_UserTestFixtureData)]   
        public void AddingNewDeck(bool jokers,int remainingValue)
        {
            var response = DeckOfcardsApiHelper.getResponseNewDeck(jokersEnabled: jokers);
            Assert.IsTrue(DeckOfcardsApiHelper.verifyAddingNewDeckResponse(response, remainingValue));

        }


        [Test]
        [TestCaseSource(typeof(TestDataHelper), Constants.Attribue_DrawingTestFixtureData)]
        public void DrawOneOrMoreCardsFromDeck(bool jokers, int remainingValue,int removingValue)
        {
            var response = DeckOfcardsApiHelper.getResponseNewDeck(jokersEnabled: jokers);
            Assert.IsTrue(DeckOfcardsApiHelper.verifyAddingNewDeckResponse(response, remainingValue));
            var getResponsOfDrawCardsFromDeckResponse = DeckOfcardsApiHelper.getResponsOfDrawCardsFromDeck(response.Data.deck_id, removeCardNumber: removingValue);
            Assert.AreEqual(HttpStatusCode.OK, getResponsOfDrawCardsFromDeckResponse.StatusCode, "Expected Response statues code " + HttpStatusCode.OK + " is not matching " + response.StatusCode);
            Assert.IsNotNull(getResponsOfDrawCardsFromDeckResponse.Data, "Response model Values return as null");
            int substractValue = DeckOfcardsApiHelper.SubtractValue(response.Data.remaining, removingValue);
            Assert.AreEqual(substractValue, getResponsOfDrawCardsFromDeckResponse.Data.remaining, "Expectd remaining cards count " + substractValue + " is not matching with actual" + getResponsOfDrawCardsFromDeckResponse.Data.remaining);
            Assert.IsTrue(response.Data.success, "Expected is true but actual was false");
            Assert.AreEqual(response.Data.deck_id, getResponsOfDrawCardsFromDeckResponse.Data.deck_id, response.Data.deck_id+ " id is not matching with "+ getResponsOfDrawCardsFromDeckResponse.Data.deck_id);
            Assert.IsNotNull(getResponsOfDrawCardsFromDeckResponse.Data.cards);
        }

    }
}
