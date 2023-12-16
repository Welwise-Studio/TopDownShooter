using YG;

namespace Domain.SettingsSystem
{
    public class SettingsFactory
    {
        public Settings FromYG()
        {
            return new Settings(YandexGame.savesData.Sfx, YandexGame.savesData.Master, YandexGame.savesData.Music);
        }
    }
}
