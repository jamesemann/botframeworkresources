using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Newtonsoft.Json.Linq;
using AdaptiveCards;

namespace CardsDemoBot.Dialogs
{
    [Serializable]
    public class CardsDemoDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedStart);
        }

        public async Task MessageReceivedStart(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var activity = await argument;

            if (activity != null && activity.Text != null)
            {
                var replyMessage = context.MakeMessage();
                replyMessage.Attachments = new List<Attachment>();

                switch (activity.Text.ToLower())
                {
                    case "adaptive-card":
                        {
                            ShowAdaptiveCard(replyMessage);
                            break;
                        }
                    case "carousel":
                        {
                            ShowHeroCardCarousel(replyMessage);
                            break;
                        }
                    case "static-card":
                        {
                            ShowStaticCard(replyMessage);
                            break;
                        }
                    case "hero-card":
                        {
                            ShowHeroCard(replyMessage);
                            break;
                        }
                    case "thumbnail-card":
                        {
                            ShowThumbnailCard(replyMessage);
                            break;
                        }
                    case "receipt-card":
                        {
                            ShowReceiptCard(replyMessage);
                            break;
                        }
                    case "airline-checkin-card":
                        {
                            ShowFacebookMessengerAirlineCheckInCard(replyMessage);
                            break;
                        }
                    case "airline-update-card":
                        {
                            ShowFacebookMessengerAirlineFlightUpdateCard(replyMessage);
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }

                await context.PostAsync(replyMessage);
            }

            context.Wait(MessageReceivedStart);
        }

        private void ShowAdaptiveCard(IMessageActivity replyMessage)
        {
            var card = new AdaptiveCard();
            card.Body.Add(new TextBlock()
            {
                Text = "Adaptive Card Sample",
                Size = TextSize.ExtraLarge,
                Color = TextColor.Attention
            });
            card.Body.Add(new Image()
            {
                Url = "http://lorempixel.com/200/200/food"
            });
            Attachment attachment = new Attachment()
            {
                ContentType = AdaptiveCard.ContentType,
                Content = card
            };

            replyMessage.Attachments.Add(attachment);
        }

        private static void ShowSignInCard(IMessageActivity replyMessage)
        {
            var cardButtons = new List<CardAction>();
            var plButton = new CardAction
            {
                Value = "https://<OAuthSignInURL>",
                Type = "signin",
                Title = "Connect"
            };
            cardButtons.Add(plButton);
            var plCard = new SigninCard("You need to authorize me", cardButtons);
            var plAttachment = plCard.ToAttachment();
            replyMessage.Attachments.Add(plAttachment);
        }

        private static void ShowReceiptCard(IMessageActivity replyMessage)
        {
            var rnd = new Random();
            var cardButtons = new List<CardAction>();
            var plButton = new CardAction
            {
                Value = "https://en.wikipedia.org/wiki/Pig_Latin",
                Type = "openUrl",
                Title = "Wikipedia Page"
            };
            cardButtons.Add(plButton);
            var lineItem1 = new ReceiptItem
            {
                Title = "Pork Shoulder",
                Subtitle = "8 lbs",
                Text = null,
                Image = new CardImage($"http://lorempixel.com/200/200/food?{rnd.Next()}"),
                Price = "16.25",
                Quantity = "1",
                Tap = null
            };
            var lineItem2 = new ReceiptItem
            {
                Title = "Bacon",
                Subtitle = "5 lbs",
                Text = null,
                Image = new CardImage($"http://lorempixel.com/200/200/food?{rnd.Next()}"),
                Price = "34.50",
                Quantity = "2",
                Tap = null
            };
            var receiptList = new List<ReceiptItem>();
            receiptList.Add(lineItem1);
            receiptList.Add(lineItem2);
            var plCard = new ReceiptCard
            {
                Title = "I'm a receipt card, isn't this bacon expensive?",
                Buttons = cardButtons,
                Items = receiptList,
                Total = "275.25",
                Tax = "27.52"
            };
            var plAttachment = plCard.ToAttachment();
            replyMessage.Attachments.Add(plAttachment);
        }

        private static void ShowThumbnailCard(IMessageActivity replyMessage)
        {
            var cardImages = new List<CardImage>();
            cardImages.Add(new CardImage("http://lorempixel.com/200/200/food"));
            var cardButtons = new List<CardAction>();
            var plButton = new CardAction
            {
                Value = "https://en.wikipedia.org/wiki/Pig_Latin",
                Type = "openUrl",
                Title = "Wikipedia Page"
            };
            cardButtons.Add(plButton);
            var plCard = new ThumbnailCard
            {
                Title = "I'm a thumbnail card",
                Subtitle = "Wikipedia Page",
                Images = cardImages,
                Buttons = cardButtons
            };
            var plAttachment = plCard.ToAttachment();
            replyMessage.Attachments.Add(plAttachment);
        }

        private static void ShowHeroCard(IMessageActivity replyMessage)
        {
            var cardImages = new List<CardImage>();
            cardImages.Add(new CardImage("http://lorempixel.com/200/200/food"));
            cardImages.Add(new CardImage("http://lorempixel.com/200/200/food"));
            var cardButtons = new List<CardAction>();
            var plButton = new CardAction
            {
                Value = "https://en.wikipedia.org/wiki/Pig_Latin",
                Type = "openUrl",
                Title = "Wikipedia Page"
            };
            cardButtons.Add(plButton);
            var plCard = new HeroCard
            {
                Title = "I'm a hero card",
                Subtitle = "Wikipedia Page",
                Images = cardImages,
                Buttons = cardButtons
            };
            var plAttachment = plCard.ToAttachment();
            replyMessage.Attachments.Add(plAttachment);
        }

        private static void ShowHeroCardCarousel(IMessageActivity replyMessage)
        {
            var cardImages1 = new List<CardImage>();
            cardImages1.Add(new CardImage("http://lorempixel.com/600/600/food"));

            var cardImages2 = new List<CardImage>();
            cardImages2.Add(new CardImage("http://lorempixel.com/600/600/sports"));


            var cardButtons = new List<CardAction>();
            var plButton = new CardAction
            {
                Value = "https://en.wikipedia.org/wiki/Pig_Latin",
                Type = "openUrl",
                Title = "Wikipedia Page"
            };
            cardButtons.Add(plButton);
            var plCard1 = new HeroCard
            {
                Title = "I'm a hero card 1",
                Subtitle = "Wikipedia Page",
                Images = cardImages1,
                Buttons = cardButtons
            };

            var plCard2 = new HeroCard
            {
                Title = "I'm a hero card 2",
                Subtitle = "Wikipedia Page",
                Images = cardImages2,
                Buttons = cardButtons
            };


            replyMessage.AttachmentLayout = "carousel";
            replyMessage.Attachments.Add(plCard1.ToAttachment());
            replyMessage.Attachments.Add(plCard2.ToAttachment());
        }

        private static void ShowStaticCard(IMessageActivity replyMessage)
        {
            replyMessage.Attachments.Add(new Attachment
            {
                ContentUrl = "http://lorempixel.com/200/200/food",
                ContentType = "image/png",
                Name = "food.png"
            });
        }

        private static void ShowFacebookMessengerAirlineCheckInCard(IMessageActivity replyMessage)
        {
            var attachment = new
            {
                type = "template",
                payload = new
                {
                    template_type = "airline_checkin",
                    intro_message = "Check-in is available now.",
                    locale = "en_US",
                    pnr_number = "ABCDEF",
                    checkin_url = "https://www.airline.com/check-in",
                    flight_info = new[]
                    {
                        new
                        {
                            flight_number = "AL01",
                            departure_airport =
                            new
                            {
                                airport_code = "SFO",
                                city = "San Francisco",
                                terminal = "T4",
                                gate = "G8"
                            },
                            arrival_airport = new
                            {
                                airport_code = "SEA",
                                city = "Seattle",
                                terminal = "T4",
                                gate = "G8"
                            },
                            flight_schedule = new
                            {
                                boarding_time = "2016-01-05T15:05",
                                departure_time = "2016-01-05T15:45",
                                arrival_time = "2016-01-05T17:30"
                            }
                        }
                    }
                }
            };

            replyMessage.ChannelData = JObject.FromObject(new { attachment });
        }

        private static void ShowFacebookMessengerAirlineFlightUpdateCard(IMessageActivity replyMessage)
        {
            var attachment = new
            {
                type = "template",
                payload = new
                {
                    template_type = "airline_update",
                    intro_message = "Your flight is delayed.",
                    update_type = "delay",
                    locale = "en_US",
                    pnr_number = "ABCDEF",
                    update_flight_info =
                    new
                    {
                        flight_number = "AL01",
                        departure_airport =
                        new
                        {
                            airport_code = "SFO",
                            city = "San Francisco",
                            terminal = "T4",
                            gate = "G8"
                        },
                        arrival_airport = new
                        {
                            airport_code = "SEA",
                            city = "Seattle",
                            terminal = "T4",
                            gate = "G8"
                        },
                        flight_schedule = new
                        {
                            boarding_time = "2016-01-05T15:05",
                            departure_time = "2016-01-05T15:45",
                            arrival_time = "2016-01-05T17:30"
                        }
                    }
                }
            };

            replyMessage.ChannelData = JObject.FromObject(new { attachment });
        }
    }
}