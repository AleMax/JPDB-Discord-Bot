﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot
{
    struct ConfigJson
    {
        [JsonProperty("DiscordToken")]
        public string DiscordToken { get; private set; }
        [JsonProperty("Prefix")]
        public string Prefix { get; private set; }
        [JsonProperty("JPDBToken")]
        public string JPDBToken { get; private set; }
        [JsonProperty("LogLevel")]
        public string LogLevel { get; private set; }
        [JsonProperty("WelcomeMessages")]
        public string WelcomeMessages { get; private set; }
    }
}
