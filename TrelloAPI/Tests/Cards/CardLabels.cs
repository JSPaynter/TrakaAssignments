using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TrelloAPI.Objects;

namespace TrelloAPI.Tests.Cards
{
    internal class CardLabels : SetupTeardown
    {
        [Test, TestCaseSource(nameof(DeleteCardLabelData))]
        public void DeleteCardLabel(Card card, string deleteLabelId, string[] idLabels, string cardId)
        {
            steps.GivenACardIsOnTheTrelloBoard(card);
            steps.WhenALabelIsDeletedFromACard(card, deleteLabelId);
            steps.ThenTheResponseShouldContainTheCorrectStatusCode(HttpStatusCode.OK);
            steps.ThenTheLabelsOnTheCardShouldBeCorrect(idLabels, cardId);
        }

        static readonly object[] DeleteCardLabelData =
        {
            new object[] { new Card("6675f9cb9c1f1e64d58197f8"),
                "6675f9d9ade9af2339d7e5a0", new string[] { "6675f9e07fbfac05c27fa42b" }, "6675f9cb9c1f1e64d58197f8"
            }
        };

        [Test, TestCaseSource(nameof(DeleteCardLabelErrorData))]
        public void DeleteCardLabelThatDoesntExist(Card card)
        {
            steps.GivenACardIsOnTheTrelloBoard(card);
            steps.WhenALabelIsDeletedFromACard(card, "ErrorLabelID");
            steps.ThenTheResponseShouldContainTheCorrectStatusCode(HttpStatusCode.BadRequest);
        }

        static readonly object[] DeleteCardLabelErrorData =
        {
            new object[] { new Card("6675f9cb9c1f1e64d58197f8")
            }
        };
    }
}
