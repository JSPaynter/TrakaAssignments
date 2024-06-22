using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrelloAPI.Objects
{
    public class Card
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("desc")]
        public string Desc { get; set; }
        [JsonProperty("pos")]
        public string? Pos { get; set; }
        [JsonProperty("start")]
        public string? Start { get; set; }
        [JsonProperty("dueComplete")]
        public bool DueComplete { get; set; }
        [JsonProperty("idList")]
        public string IdList { get; set; }
        [JsonProperty("idLabels")]
        public List<string> IdLabels { get; set; }

        [JsonConstructor]
        public Card()
        {

        }

        public Card(string name, string desc, string? pos, string? start, bool dueComplete, string idList, List<string> idLabels, string id = "")
        {
            Id = id;
            Name = name;
            Desc = desc;
            Pos = pos;
            Start = start;
            DueComplete = dueComplete;
            IdList = idList;
            IdLabels = idLabels;
        }

        public Card(string id)
        {
            Id = id;
            Name = "";
            Desc = "";
            Pos = null;
            Start = "";
            DueComplete = false;
            IdList = "";
            IdLabels = [];
        }

        public override bool Equals(object? obj)
        {
            return obj is Card card &&
                   (string.IsNullOrEmpty(Id) || Id == card.Id) &&
                   Name == card.Name &&
                   Desc == card.Desc &&
                   (string.IsNullOrEmpty(Pos) || Pos == card.Pos) &&
                   Start == card.Start &&
                   DueComplete == card.DueComplete &&
                   IdList == card.IdList &&
                   IdLabels.OrderBy(m => m).SequenceEqual(card.IdLabels.OrderBy(m => m));
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Desc, Pos, Start, DueComplete, IdList, IdLabels);
        }
    }
}
