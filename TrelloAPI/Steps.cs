using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrelloAPI.APIs;
using Newtonsoft.Json;
using NUnit.Framework;
using TrelloAPI.Objects;
using System.Net;

namespace TrelloAPI
{
    internal class Steps
    {
        readonly CardAPI cardAPI = new();
        HttpResponseMessage? lastResponse;

        public Steps()
        {

        }

        #region Given Steps

        public void GivenACardIsOnTheTrelloBoard(Card card)
        {
            /* Ideally this is where we would directly add a card to Trello,
             * however, as we cant access Trello's databases, we will use a pre created card
             */
        }

        public void GivenThereIsATrelloBoard(string board)
        {
            //We would use backend services to create a board if there wasnt 1 setup.
        }

        #endregion
        #region When Steps

        public void WhenTheCardIsUpdated(Card card, Dictionary<string, object> updateDetails)
        {
            lastResponse = cardAPI.UpdateCard(card.Id, updateDetails);
            Console.WriteLine(lastResponse.Content.ReadAsStringAsync().Result);
        }

        public void WhenACardIsCopiedToANewBoard(Card card, string newBoardId, Dictionary<string, object>? updateDetails = null)
        {
            lastResponse = cardAPI.CopyCard(newBoardId, card.Id, updateDetails);
            Console.WriteLine(lastResponse.Content.ReadAsStringAsync().Result);
        }

        internal void WhenALabelIsDeletedFromACard(Card card, string deleteLabelId)
        {
            lastResponse = cardAPI.RemoveLabel(card.Id, deleteLabelId);
            Console.WriteLine(lastResponse.Content.ReadAsStringAsync().Result);
        }

        #endregion
        #region Then Steps

        public void ThenTheCorrectCardDetailsShouldBeReturned(Card expectedCard)
        {
            Assert.That(lastResponse.IsSuccessStatusCode, lastResponse.Content.ReadAsStringAsync().Result);
            Card returnedCard = JsonConvert.DeserializeObject<Card>(lastResponse.Content.ReadAsStringAsync().Result);
            Assert.That(expectedCard.Equals(returnedCard));
        }

        public void ThenTheLabelsOnTheCardShouldBeCorrect(Card card, string cardId)
        {
            ThenTheLabelsOnTheCardShouldBeCorrect(card.IdLabels.ToArray(), cardId);
        }

        public void ThenTheLabelsOnTheCardShouldBeCorrect(string[] idLabels, string cardId)
        {
            Assert.That(lastResponse.IsSuccessStatusCode, lastResponse.Content.ReadAsStringAsync().Result);
            /* Ideally, we would get the remaining label list from the database
             * But for this demo we will use a get request
             */
            lastResponse = cardAPI.GetCard(cardId);
            Assert.That(lastResponse.IsSuccessStatusCode, lastResponse.Content.ReadAsStringAsync().Result);
            Card returnedCard = JsonConvert.DeserializeObject<Card>(lastResponse.Content.ReadAsStringAsync().Result);
            Assert.That(idLabels.OrderBy(m => m).SequenceEqual(returnedCard.IdLabels.OrderBy(m => m)));
        }

        public void ThenTheResponseShouldContainTheCorrectStatusCode(HttpStatusCode code)
        {
            Assert.That(lastResponse.StatusCode, Is.EqualTo(code));
        }

        #endregion
    }
}
