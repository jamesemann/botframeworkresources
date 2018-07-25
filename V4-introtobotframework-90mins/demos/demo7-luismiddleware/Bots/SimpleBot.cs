using Microsoft.Bot;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Ai.LUIS;
using Microsoft.Bot.Builder.Core.Extensions;
using Microsoft.Bot.Schema;
using Microsoft.Cognitive.LUIS.Models;
using System.Linq;
using System.Threading.Tasks;

namespace demo5luismiddleware.Bots
{
    public class SimpleBot : IBot
    {
        public async Task OnTurn(ITurnContext turnContext)
        {
            if (turnContext.Activity.Type == ActivityTypes.Message)
            {
                var result = turnContext.Services.Get<RecognizerResult>(LuisRecognizerMiddleware.LuisRecognizerResultKey);

                if (result.Properties.FirstOrDefault(x => x.Key == "luisResult").Value is LuisResult luisResult)
                {
                    switch (luisResult.TopScoringIntent.Intent)
                    {
                        case null:
                            await turnContext.SendActivity("Failed to get results from LUIS.");
                            break;
                        case "none":
                            await turnContext.SendActivity("Sorry, I don't understand.");
                            break;
                        case "adjustlights":
                            var lightsRoom = luisResult.Entities.FirstOrDefault(x => x.Type == "Room");
                            var onOff = luisResult.Entities.FirstOrDefault(x => x.Type == "onoroff");

                            if (lightsRoom == null)
                            {
                                await turnContext.SendActivity($"Adjusting the lights. Here we would disambiguate the room...");
                            }
                            else if (onOff == null)
                            {
                                await turnContext.SendActivity($"Adjusting the lights. Here we would disambiguate on/off...");
                            }
                            else
                            {
                                await turnContext.SendActivity($"Setting the lights to {onOff.Entity} in the {lightsRoom.Entity}");
                            }
                            break;
                        case "adjusttemperature":
                            var tempRoom = luisResult.Entities.FirstOrDefault(x => x.Type == "Room");
                            var temp = luisResult.Entities.FirstOrDefault(x => x.Type == "builtin.temperature");
                            var tempValue = temp?.Resolution?.FirstOrDefault(x => x.Key == "value").Value;
                            var tempUnit = temp?.Resolution?.FirstOrDefault(x => x.Key == "unit").Value;

                            if (tempRoom == null)
                            {
                                await turnContext.SendActivity($"Setting the temperature. Here we would disambiguate the room...");
                            }
                            else if (tempValue == null)
                            {
                                await turnContext.SendActivity($"Setting the temperature. Here we would disambiguate the value...");
                            }
                            else if (tempUnit == null)
                            {
                                await turnContext.SendActivity($"Setting the temperature. Here we would disambiguate the unit...");
                            }
                            else
                            {
                                await turnContext.SendActivity($"Setting the temperature to {tempValue} {tempUnit} in the {tempRoom.Entity}");
                            }
                            break;
                        default:
                            await turnContext.SendActivity($"Intent: {luisResult.TopScoringIntent.Intent} ({luisResult.TopScoringIntent.Score}).");
                            break;
                    }
                }
            }
        }
    }
}
