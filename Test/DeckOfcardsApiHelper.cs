using DeckofcardsApi.APIHelpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace DeckofcardsApi.Test
{
    class DeckOfcardsApiHelper
    {
     
        public ResponseModel<Deck> getResponseNewDeck(bool jokersEnabled)
        {
            return APIHelperCommon.Get<Deck>(ServiceUri:Constants.serviceUri,controller: Constants.Controler, query: Constants.jokers_enabled + jokersEnabled.ToString());
        }

        public ResponseModel<Deck> getResponsOfDrawCardsFromDeck(string deckid, int removeCardNumber = 1)
        {
            string CombineDeckData = deckid + Constants.drawAction + removeCardNumber;
            return APIHelperCommon.Get<Deck>(ServiceUri: Constants.serviceUri, controller: Constants.Controler,query: CombineDeckData);
        }

        public bool verifyAddingNewDeckResponse(ResponseModel<Deck> response, int remainingValue)
        {
            try
            {
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "Expected Response statues code " + HttpStatusCode.OK + " is not matching " + response.StatusCode);
                Assert.IsNotNull(response.Data, "Response model Values return as null");
                Assert.AreEqual(remainingValue, response.Data.remaining, "Expectd remaining cards count " + Constants.withJokerRemainingValue + " is not matching with actual" + response.Data.remaining);
                Assert.IsTrue(response.Data.success, "Expected is true but actual was false");
                Assert.IsNotNull(response.Data.deck_id, "Deck id value is null");
                Assert.IsFalse(response.Data.shuffled, "Expected is false but actual was true");
                return true;

            }
            catch (Exception e){
                Console.WriteLine(e.Message);
                return false;
            }
        }
        
        public int SubtractValue(int DeckCount, int RemovingValue)
        {
            return DeckCount - RemovingValue;
        }

    }


    public class Deck
    {
        public bool success { get; set; }
        public string deck_id { get; set; }
        public bool shuffled { get; set; }
        public int remaining { get; set; }
        public List<Card> cards { get; set; }

    }
    public class Card
    {
        public string image { get; set; }
        public string value { get; set; }
        public string suit { get; set; }
        public string code { get; set; }
    }

    
    
}
