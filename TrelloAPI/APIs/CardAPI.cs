using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Core.BaseClasses;
using NUnit.Framework.Constraints;

namespace TrelloAPI.APIs
{

    internal class CardAPI : TrelloAPI
    {

        public CardAPI() : base("cards/")
        {

        }

        public HttpResponseMessage GetCard(string cardId)
        {
            ResetClient();
            Dictionary<string, object> parameters = GetSecurityHeaders();
            AddAcceptHeader("application/json");
            return Get(cardId, parameters);
        }

        public HttpResponseMessage CreateCard(dynamic data)
        {
            return CreateCard(data.name, data.desc, data.pos, data.due, data.start, data.dueComplete, data.idList);
        }

        public HttpResponseMessage CreateCard(string name, string desc, object pos, string due, string start, bool dueComplete, string idList)
        {
            ResetClient();
            Dictionary<string, object> parameters = GetSecurityHeaders();
            parameters.Add("name", name);
            parameters.Add("desc", desc);
            parameters.Add("pos", pos);
            parameters.Add("due", due);
            parameters.Add("start", start);
            parameters.Add("dueComplete", dueComplete);
            parameters.Add("idList", idList);
            AddAcceptHeader("application/json");
            return Post(parameters);
        }

        public HttpResponseMessage UpdateCard(string id, Dictionary<string, object> updateDetails)
        {
            ResetClient();
            Dictionary<string, object> parameters = GetSecurityHeaders();
            parameters = parameters.Union(updateDetails).ToDictionary();
            AddAcceptHeader("application/json");
            return Put(id, parameters);
        }

        public HttpResponseMessage CopyCard(string newBoardId, string id, Dictionary<string, object>? updateDetails = null)
        {
            ResetClient();
            Dictionary<string, object> parameters = GetSecurityHeaders();
            parameters.Add("idList", newBoardId);
            parameters.Add("idCardSource", id);
            if (updateDetails != null) parameters = parameters.Union(updateDetails).ToDictionary();
            AddAcceptHeader("application/json");
            return Post(parameters);
        }

        internal HttpResponseMessage RemoveLabel(string id, string deleteLabelId)
        {
            ResetClient();
            Dictionary<string, object> parameters = GetSecurityHeaders();
            string uri = $"{id}/idLabels/{deleteLabelId}";
            return Delete(uri, parameters);
        }
    }
}
