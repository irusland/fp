using System;
using System.Drawing;
using System.Text.RegularExpressions;
using TagCloud.Infrastructure.Settings.SettingsProviders;

namespace TagCloud.Infrastructure.Settings.UISettingsManagers
{
    public class LayoutCenterSettingManager : IInputManager
    {
        private readonly Func<ISpiralSettingsProvider> settingsProvider;
        private readonly Regex regex;

        public LayoutCenterSettingManager(Func<ISpiralSettingsProvider> settingsProvider)
        {
            this.settingsProvider = settingsProvider;
            regex = new Regex(@"^(?<x>\d+)\s+(?<y>\d+)$");
        }

        public string Title => "Layout Center";
        public string Help => "Choose where you want to see a layout. Point is counting from top left corner";

        public Result<string> TrySet(string input)
        {
            var match = regex.Match(input);
            if (!match.Success)
                return Result.Fail<string>("Incorrect input format ([x], [y])");
            settingsProvider().Center = new Point(
                int.Parse(match.Groups["x"].Value),
                int.Parse(match.Groups["y"].Value));
            return Get();
        }

        public string Get()
        {
            var settings = settingsProvider();
            return $"{settings.Center.X} {settings.Center.Y}";
        }
    }
}