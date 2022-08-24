﻿using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;

namespace Rise.Models
{
    public partial class SyncedLyrics
    {
        [JsonProperty("message")]
        public SyncedMessage Message { get; set; }
    }

    public class SyncedMessage
    {
        [JsonProperty("header")]
        public SyncedHeader Header { get; set; }

        [JsonProperty("body", NullValueHandling = NullValueHandling.Ignore)]
        public SyncedBody Body { get; set; }
    }

    public class SyncedBody
    {
        [JsonProperty("subtitle")]
        public SyncedSubtitle Subtitle { get; set; }
    }

    public class SyncedSubtitle
    {
        [JsonProperty("subtitle_id")]
        public long SubtitleId { get; set; }

        [JsonProperty("restricted")]
        public long Restricted { get; set; }

        [JsonProperty("subtitle_body")]
        public string SubtitleBody { get; set; }

        [JsonProperty("subtitle_avg_count")]
        public long SubtitleAvgCount { get; set; }

        [JsonProperty("lyrics_copyright")]
        public string LyricsCopyright { get; set; }

        [JsonProperty("subtitle_length")]
        public long SubtitleLength { get; set; }

        [JsonProperty("subtitle_language")]
        public string SubtitleLanguage { get; set; }

        [JsonProperty("subtitle_language_description")]
        public string SubtitleLanguageDescription { get; set; }

        [JsonProperty("script_tracking_url")]
        public Uri ScriptTrackingUrl { get; set; }

        [JsonProperty("pixel_tracking_url")]
        public Uri PixelTrackingUrl { get; set; }

        [JsonProperty("html_tracking_url")]
        public Uri HtmlTrackingUrl { get; set; }

        [JsonProperty("writer_list")]
        public object[] WriterList { get; set; }

        [JsonProperty("publisher_list")]
        public object[] PublisherList { get; set; }

        [JsonProperty("updated_time")]
        public DateTimeOffset UpdatedTime { get; set; }
    }

    public class SyncedHeader
    {
        [JsonProperty("status_code")]
        public long StatusCode { get; set; }

        [JsonProperty("execute_time")]
        public double ExecuteTime { get; set; }

        [JsonProperty("hint", NullValueHandling = NullValueHandling.Ignore)]
        public string Hint { get; set; }
    }

    public partial class SyncedLyrics
    {
        public static SyncedLyrics FromJson(string json) => JsonConvert.DeserializeObject<SyncedLyrics>(json, SyncedConverter.Settings);
    }

    internal static class SyncedConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}