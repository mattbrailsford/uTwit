using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Our.Umbraco.uTwit.Models
{
    [DataContract(Name = "Status")]
    public class Status
    {
        [DataMember]
        public long Id { get; internal set; }

        [DataMember]
        public User User { get; internal set; }

        [DataMember]
        public string Text { get; internal set; }

        [DataMember]
        public Entities Entities { get; internal set; }

        [DataMember]
        public long? InReplyToStatusId { get; internal set; }

        [DataMember]
        public string InReplyToStatusIdStr { get; internal set; }

        [DataMember]
        public long? InReplyToUserId { get; internal set; }

        [DataMember]
        public string InReplyToUserIdStr { get; internal set; }

        [DataMember]
        public string InReplyToScreenName { get; internal set; }

        [DataMember]
        public Status RetweetedStatus { get; set; }

        [DataMember]
        public bool Favorited { get; internal set; }

        [DataMember]
        public int? FavoriteCount { get; internal set; }

        [DataMember]
        public bool Retweeted { get; internal set; }

        [DataMember]
        public int? RetweetCount { get; internal set; }

        public IHtmlString Source { get; internal set; }

        [DataMember]
        public DateTime CreatedAt { get; internal set; }

        [DataMember(Name = "Source")]
        protected string SourceRaw
        {
            get
            {
                return Source.ToString();
            }
            set
            {
                // Don't do anything
            }
        }

        [DataMember]
        public bool IsRetweet
        {
            get
            {
                return RetweetedStatus != null;
            }
            internal set
            {
                // Don't do anything
            }
        }

        [DataMember]
        public string PermalinkUrl
        {
            get
            {
                return string.Format("https://twitter.com/{0}/status/{1}",
                    User.ScreenName,
                    Id);
            }
            internal set
            {
                // Don't do anything
            }
        }

        public IHtmlString LinkifiedText
        {
            get
            {
                var entities = new List<Entity>();
                entities.AddRange(Entities.UserMentions);
                entities.AddRange(Entities.Urls);
                entities.AddRange(Entities.Hashtags);

                entities.Sort(new StartIndexComparer());

                var linkifiedText = Text;
                var offset = 0;

                foreach (var entity in entities)
                {
                    var lengthBefore = linkifiedText.Length;

                    if (entity is UserMentionEntity)
                    {
                        var tEntity = (UserMentionEntity)entity;

                        linkifiedText = string.Format(
                            "{0}<a href=\"http://twitter.com/{1}\">@{1}</a>{2}",
                            linkifiedText.Substring(0, tEntity.Indices[0] + offset),
                            tEntity.ScreenName,
                            linkifiedText.Substring(tEntity.Indices[1] + offset));
                    }

                    if (entity is UrlEntity)
                    {
                        var tEntity = (UrlEntity)entity;

                        linkifiedText = string.Format(
                            "{0}<a href=\"{1}\">{2}</a>{3}",
                            linkifiedText.Substring(0, tEntity.Indices[0] + offset),
                            tEntity.Url,
                            tEntity.DisplayUrl ?? tEntity.Url,
                            linkifiedText.Substring(tEntity.Indices[1] + offset));
                    }

                    if (entity is HashtagEntity)
                    {
                        var tEntity = (HashtagEntity)entity;

                        linkifiedText = string.Format(
                            "{0}<a href=\"http://twitter.com/search?q=%23{1}\">#{1}</a>{2}",
                            linkifiedText.Substring(0, tEntity.Indices[0] + offset),
                            tEntity.Text,
                            linkifiedText.Substring(tEntity.Indices[1] + offset));
                    }

                    offset += linkifiedText.Length - lengthBefore;
                }

                return new HtmlString(linkifiedText);
            }
            internal set
            {
                // Don't do anything
            }
        }

        [DataMember(Name = "LinkifiedText")]
        protected string LinkifiedTextRaw
        {
            get
            {
                return LinkifiedText.ToString();
            }
            set
            {
                // Don't do anything
            }
        }
    }

    internal class StartIndexComparer : IComparer<Entity>
    {
        public int Compare(Entity x, Entity y)
        {
            if (x.Indices[0] > y.Indices[0])
                return 1;
            if (x.Indices[0] < y.Indices[0])
                return -1;
            
            return 0;
        }
    }
}