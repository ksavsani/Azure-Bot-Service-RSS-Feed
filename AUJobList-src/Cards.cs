// Created By KASHYAP SAVSANI on 4th Aug,2020

using System.Collections.Generic;
using System.IO;
using System.ServiceModel.Syndication;
using AUJobListBot.RSSFeeds;
using Microsoft.Bot.Schema;
using Newtonsoft.Json;
using System.Linq;
using System;

namespace Microsoft.BotBuilderSamples
{
    public static class Cards
    {    

        public static List<HeroCard> GetResult(string cat,string loc)
        {
            List<HeroCard> CardList = new List<HeroCard>();

           CardList = GetResults("https://aiaustralia.zohorecruit.com/recruit/downloadrssfeed?digest=t2QVsZkenOE3Sd9UTpG@zRWMAsezGsogx5ji7Hc6ZVY-&embedsource=CareerSite",cat,loc);

            return CardList;
        }

        public static List<HeroCard> GetResults(string url,string cat,string loc)
        {
            List<HeroCard> CardList = new List<HeroCard>();

            RSS rss = new RSS(url);
            SyndicationFeed Feed = rss.Get();

            if (Feed!=null)
            {
				int cnt = 0;
                foreach (var item in Feed.Items)
                {	
					string s1 = item.Summary.Text.ToString();
					bool boolean = s1.StartsWith("Category: " + cat + " <br><br>Location: " + loc);
                    if (boolean == true)
					{
						cnt = cnt+1;
					    var heroCard = new HeroCard
                        {
                           Title = item.Title.Text,
                           Buttons = new List<CardAction> { new CardAction(ActionTypes.OpenUrl, "Link", value: item.Links.FirstOrDefault().Uri.ToString()) }
                        };
					CardList.Add(heroCard);
					}
                }
				
				if (cnt == 0)
				{
					var heroCard = new HeroCard
					{
						Title = "No jobs found for your selection"
					};
				CardList.Add(heroCard);
				}
            }
                       
            return CardList;
        }

    }
}
