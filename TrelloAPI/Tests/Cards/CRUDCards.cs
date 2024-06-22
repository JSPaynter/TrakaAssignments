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
    [TestFixture]
    public class CRUDCards : SetupTeardown
    {
        [Test, TestCaseSource(nameof(UpdateCardData))]
        public void UpdateCard(Card card, Dictionary<string, object> updateDetails, Card expectedCard)
        {
            steps.GivenACardIsOnTheTrelloBoard(card);
            steps.WhenTheCardIsUpdated(card, updateDetails);
            steps.ThenTheResponseShouldContainTheCorrectStatusCode(HttpStatusCode.OK);
            steps.ThenTheCorrectCardDetailsShouldBeReturned(expectedCard);
        }

        static readonly object[] UpdateCardData =
        [
            new object[] { new Card("66742b44f7c05c5017039a8f"),
                new Dictionary<string, object>{ 
                    { "pos", "top" }
                }, new Card("Planning", "", "1", null, false, "66742b44b94752f8412a1c0c", [], "66742b44f7c05c5017039a8f") },
            new object[] { new Card("66742b44f7c05c5017039a8f"),
                new Dictionary<string, object>{
                    { "pos", "1" }
                }, new Card("Planning", "", "1", null, false, "66742b44b94752f8412a1c0c", [], "66742b44f7c05c5017039a8f") },
            new object[] { new Card("66742b44f7c05c5017039a8f"),
                new Dictionary<string, object>{
                    { "pos", "2" }
                }, new Card("Planning", "", "40962", null, false, "66742b44b94752f8412a1c0c", [], "66742b44f7c05c5017039a8f") },
            new object[] { new Card("66742b44f7c05c5017039a8f"),
                new Dictionary<string, object>{
                    { "pos", "bottom" }
                }, new Card("Planning", "", "245762", null, false, "66742b44b94752f8412a1c0c", [], "66742b44f7c05c5017039a8f") }
        ];

        [Test, TestCaseSource(nameof(UpdateCardDataCardDoesntExist))]
        public void UpdateCardCardDoesntExist(Card card, Dictionary<string, object> updateDetails)
        {
            steps.WhenTheCardIsUpdated(card, updateDetails);
            steps.ThenTheResponseShouldContainTheCorrectStatusCode(HttpStatusCode.BadRequest);
        }

        static readonly object[] UpdateCardDataCardDoesntExist =
        [
            new object[] { new Card("invalid"), new Dictionary<string, object>{ { "pos", "top" } }
            }
        ];

        [Test, TestCaseSource(nameof(CopyCardData))]
        public void CopyCardToNewBoard(Card card, string newBoardId, Card expectedCard)
        {
            steps.GivenACardIsOnTheTrelloBoard(card);
            steps.GivenThereIsATrelloBoard(newBoardId);
            steps.WhenACardIsCopiedToANewBoard(card, newBoardId);
            steps.ThenTheResponseShouldContainTheCorrectStatusCode(HttpStatusCode.OK);
            steps.ThenTheCorrectCardDetailsShouldBeReturned(expectedCard);
        }

        static readonly object[] CopyCardData =
        [
            new object[] { new Card("66742b44f7c05c5017039a8f"),
                "6675d9446c4c96da47399980",
                new Card("Planning", "", null, null, false, "6675d9446c4c96da47399980", []) }
        ];

        [Test, TestCaseSource(nameof(CopyCardOverWrittingData))]
        public void CopyCardToNewBoardOverwrittingDetails(Card card, string newBoardId,
            Dictionary<string, object> updateDetails, Card expectedCard)
        {
            steps.GivenACardIsOnTheTrelloBoard(card);
            steps.GivenThereIsATrelloBoard(newBoardId);
            steps.WhenACardIsCopiedToANewBoard(card, newBoardId, updateDetails);
            steps.ThenTheResponseShouldContainTheCorrectStatusCode(HttpStatusCode.OK);
            steps.ThenTheCorrectCardDetailsShouldBeReturned(expectedCard);
        }

        static readonly object[] CopyCardOverWrittingData =
        [
            new object[] { new Card("66742b44f7c05c5017039a8f"),
                "6675d9446c4c96da47399980",
                new Dictionary<string, object>{
                    { "name", "New Planning Card" }
                }, new Card("New Planning Card", "", null, null, false, "6675d9446c4c96da47399980", []) },
            new object[] { new Card("66742b44f7c05c5017039a8f"),
                "6675d9446c4c96da47399980",
                new Dictionary<string, object>{
                    { "desc", "Description goes here" }
                }, new Card("Planning", "Description goes here", null, null, false, "6675d9446c4c96da47399980", []) },
            new object[] { new Card("66742b44f7c05c5017039a8f"),
                "6675d9446c4c96da47399980",
                new Dictionary<string, object>{
                    { "name", "New Planning Card multi field" },
                    { "desc", "Description goes here" },
                    { "start", "2019-09-16" }
                }, new Card("New Planning Card multi field", "Description goes here", null, "2019-09-16T00:00:00.000Z", false, "6675d9446c4c96da47399980", []) }
        ];

        [Test, TestCaseSource(nameof(CopyCardCardDoesntExistData))]
        public void CopyCardToNewBoardCardDoesntExist(Card card, string newBoardId,
            Dictionary<string, object> updateDetails)
        {
            steps.GivenThereIsATrelloBoard(newBoardId);
            steps.WhenACardIsCopiedToANewBoard(card, newBoardId, updateDetails);
            steps.ThenTheResponseShouldContainTheCorrectStatusCode(HttpStatusCode.BadRequest);
        }

        static readonly object[] CopyCardCardDoesntExistData =
        [
            new object[] { new Card("InvalidCardId"),
                "6675d9446c4c96da47399980", new Dictionary<string, object>{ }
            }
        ];

        [Test, TestCaseSource(nameof(CopyCardBoardDoesntExistData))]
        public void CopyCardToNewBoardBoardDoesntExist(Card card, string newBoardId,
            Dictionary<string, object> updateDetails)
        {
            steps.GivenACardIsOnTheTrelloBoard(card);
            steps.WhenACardIsCopiedToANewBoard(card, newBoardId, updateDetails);
            steps.ThenTheResponseShouldContainTheCorrectStatusCode(HttpStatusCode.BadRequest);
        }

        static readonly object[] CopyCardBoardDoesntExistData =
        [
            new object[] { new Card("66742b44f7c05c5017039a8f"),
                "InvalidBoardID", new Dictionary<string, object>{ }
            }
        ];
    }
}
