using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherCheck.Shared.Dto;

namespace WeatherCheck
{
    public class SentenceBuilder
    {
        private readonly CheckCurrentWeatherResultDto _checkCurrentWeatherResult;

        public SentenceBuilder(CheckCurrentWeatherResultDto checkCurrentWeatherResult)
        {
            _checkCurrentWeatherResult = checkCurrentWeatherResult;
        }

        public string Build()
        {
            StringBuilder stringBuilder = new StringBuilder($"The weather today is '{_checkCurrentWeatherResult.WeatherDescription}'. ");

            stringBuilder.Append(buildShouldGoOutSentence());
            stringBuilder.Append(buildCanFlyKiteSentence());
            stringBuilder.Append(buildShouldApplySunscreenSentence());

            return stringBuilder.ToString();
        }

        private string buildShouldApplySunscreenSentence()
        {
            return (_checkCurrentWeatherResult.ShouldApplySunscreen, _checkCurrentWeatherResult.ShouldGoOut) switch
            {
                (true, true) => "Just don't forget to apply a sunscreen.",
                (true, false) => "If you do go out, don't forget to apply a sunscreen.",
                _ => string.Empty,
            };
        }

        private string buildCanFlyKiteSentence()
        {
            string flyKiteSenetence = string.Empty;
            if (_checkCurrentWeatherResult.CanFlyKite)
            {
                flyKiteSenetence = "Maybe go fly a kite? ";
            }

            return flyKiteSenetence;
        }

        private string buildShouldGoOutSentence()
        {
            return _checkCurrentWeatherResult.ShouldGoOut switch
            {
                true => "It's a nice day to go out. ",
                false => "You should stay home. "
            };
        }
    }
}
