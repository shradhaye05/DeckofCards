using System;
using System.Collections.Generic;
using System.Text;

namespace DeckofcardsApi
{
    class Constants
    {
        public const string TimeOut = "00:40:00";

        public const string serviceUri = "https://deckofcardsapi.com";
        public const string Controler = "api/deck";
        public const string drawAction = "/draw/?count=";
        public const string jokers_enabled = "new?jokers_enabled=";
        public const int withJokerRemainingValue = 54;
        public const int withOutJokerRemainingValue = 52;
        public const string Attribue_UserTestFixtureData = "UserTestFixtureData";
        public const string Attribue_DrawingTestFixtureData = "DrawingTestFixtureData";

    }
}
