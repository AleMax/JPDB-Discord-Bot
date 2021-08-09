﻿using Newtonsoft.Json;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordBot.Commands;
using System.Net;

namespace DiscordBot
{
    public class Bot
    {
        public DiscordClient Client { get; private set; }
        public CommandsNextExtension Commands { get; private set; }
        public async Task RunAsync()
        {
            Console.WriteLine("Would you like to enable debug logging (y/n)");
            bool debug = false;
            if (Console.ReadLine().Contains("y") == true)
            {
                debug = true;
            }
            Console.WriteLine("Reading config file...");
            var json = string.Empty;

            using (var fs = File.OpenRead("config.json"))
            {
                using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                {
                    json = await sr.ReadToEndAsync().ConfigureAwait(false);

                }
            }
            var configJson = JsonConvert.DeserializeObject<ConfigJson>(json);

            Console.WriteLine("Setting config...");
            DiscordConfiguration config;
            if (debug == false)
            {
                config = new DiscordConfiguration
                {
                    Token = configJson.Token,
                    TokenType = TokenType.Bot,
                    AutoReconnect = true,
                    MinimumLogLevel = Microsoft.Extensions.Logging.LogLevel.Warning,

                };
            } else
            {
                config = new DiscordConfiguration
                {
                    Token = configJson.Token,
                    TokenType = TokenType.Bot,
                    AutoReconnect = true,
                    MinimumLogLevel = Microsoft.Extensions.Logging.LogLevel.Debug,
                };
            }
            

            Client = new DiscordClient(config);
            Client.Ready += Client_Ready;
            //Client.MessageCreated

            var commandsConfig = new CommandsNextConfiguration
            {
                StringPrefixes = new string[] { configJson.Prefix },
                EnableMentionPrefix = true,
                EnableDms = false,
                DmHelp = false,
                EnableDefaultHelp = true,
                IgnoreExtraArguments = true,
            };
            Commands = Client.UseCommandsNext(commandsConfig);

            Commands.RegisterCommands<TestCommands>();
            await Client.ConnectAsync();
            await Task.Delay(-1);
        }
        private Task Client_Ready(DiscordClient sender, DSharpPlus.EventArgs.ReadyEventArgs e)
        {
            Console.WriteLine("JPDB Bot is ready.");
            return Task.CompletedTask;
        }

        public async Task ScanSubreddit(string inLast)
        {
            WebClient WebClient = new WebClient();
            string HTML = "";
            //int snipIndex = -1;
            WebClient.Encoding = System.Text.Encoding.UTF8;

            HTML = WebClient.DownloadString(new Uri("https://www.google.co.uk/search?q=%22jap%22+site:reddit.com/r/learnjapanese&source=lnt&tbs=qdr:m&sa=X&ved=2ahUKEwjRw9nf_qHyAhWsQEEAHa9cCEYQpwV6BAgBECM&biw=1536&bih=722"));
            HTML = HTML.Substring(HTML.IndexOf("div class=\"yuRUbf\""));

            //Client.GetChannelAsync(873903350771515414).SendMessageAsync("Message");

            await Task.Delay(0);
        }
    }
}
