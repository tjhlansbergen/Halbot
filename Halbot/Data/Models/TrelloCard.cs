using Newtonsoft.Json;
using System;

namespace Halbot.Data.Models
{
    public partial class TrelloCard
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("badges")]
        public Badges Badges { get; set; }

        [JsonProperty("checkItemStates")]
        public object CheckItemStates { get; set; }

        [JsonProperty("closed")]
        public bool Closed { get; set; }

        [JsonProperty("dueComplete")]
        public bool DueComplete { get; set; }

        [JsonProperty("dateLastActivity")]
        public DateTimeOffset? DateLastActivity { get; set; }

        [JsonProperty("desc")]
        public string Desc { get; set; }

        [JsonProperty("descData")]
        public DescData DescData { get; set; }

        [JsonProperty("due")]
        public DateTimeOffset? Due { get; set; }

        [JsonProperty("dueReminder")]
        public long? DueReminder { get; set; }

        [JsonProperty("email")]
        public object Email { get; set; }

        [JsonProperty("idBoard")]
        public string IdBoard { get; set; }

        [JsonProperty("idChecklists")]
        public object[] IdChecklists { get; set; }

        [JsonProperty("idList")]
        public string IdList { get; set; }

        [JsonProperty("idMembers")]
        public object[] IdMembers { get; set; }

        [JsonProperty("idMembersVoted")]
        public object[] IdMembersVoted { get; set; }

        [JsonProperty("idShort")]
        public long IdShort { get; set; }

        [JsonProperty("idAttachmentCover")]
        public object IdAttachmentCover { get; set; }

        [JsonProperty("labels")]
        public Label[] Labels { get; set; }

        [JsonProperty("idLabels")]
        public string[] IdLabels { get; set; }

        [JsonProperty("manualCoverAttachment")]
        public bool ManualCoverAttachment { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("pos")]
        public long Pos { get; set; }

        [JsonProperty("shortLink")]
        public string ShortLink { get; set; }

        [JsonProperty("shortUrl")]
        public Uri ShortUrl { get; set; }

        [JsonProperty("start")]
        public object Start { get; set; }

        [JsonProperty("subscribed")]
        public bool Subscribed { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("cover")]
        public Cover Cover { get; set; }

        [JsonProperty("isTemplate")]
        public bool IsTemplate { get; set; }

        [JsonProperty("cardRole")]
        public object CardRole { get; set; }
    }

    public partial class Badges
    {
        [JsonProperty("attachmentsByType")]
        public AttachmentsByType AttachmentsByType { get; set; }

        [JsonProperty("location")]
        public bool Location { get; set; }

        [JsonProperty("votes")]
        public long Votes { get; set; }

        [JsonProperty("viewingMemberVoted")]
        public bool ViewingMemberVoted { get; set; }

        [JsonProperty("subscribed")]
        public bool Subscribed { get; set; }

        [JsonProperty("fogbugz")]
        public string Fogbugz { get; set; }

        [JsonProperty("checkItems")]
        public long CheckItems { get; set; }

        [JsonProperty("checkItemsChecked")]
        public long CheckItemsChecked { get; set; }

        [JsonProperty("checkItemsEarliestDue")]
        public object CheckItemsEarliestDue { get; set; }

        [JsonProperty("comments")]
        public long Comments { get; set; }

        [JsonProperty("attachments")]
        public long Attachments { get; set; }

        [JsonProperty("description")]
        public bool Description { get; set; }

        [JsonProperty("due")]
        public DateTimeOffset? Due { get; set; }

        [JsonProperty("dueComplete")]
        public bool DueComplete { get; set; }

        [JsonProperty("start")]
        public object Start { get; set; }
    }

    public partial class AttachmentsByType
    {
        [JsonProperty("trello")]
        public Trello Trello { get; set; }
    }

    public partial class Trello
    {
        [JsonProperty("board")]
        public long Board { get; set; }

        [JsonProperty("card")]
        public long Card { get; set; }
    }

    public partial class Cover
    {
        [JsonProperty("idAttachment")]
        public object IdAttachment { get; set; }

        [JsonProperty("color")]
        public object Color { get; set; }

        [JsonProperty("idUploadedBackground")]
        public object IdUploadedBackground { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("brightness")]
        public string Brightness { get; set; }

        [JsonProperty("idPlugin")]
        public object IdPlugin { get; set; }
    }

    public partial class DescData
    {
        [JsonProperty("emoji")]
        public Emoji Emoji { get; set; }
    }

    public partial class Emoji
    {
    }

    public partial class Label
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("idBoard")]
        public string IdBoard { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }
    }
}
