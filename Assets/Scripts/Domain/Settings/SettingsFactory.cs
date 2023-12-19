using YG;

namespace Domain.SettingsSystem
{
    public static class SettingsFactory
    {
        public static Settings Create() => new Settings(YandexGame.savesData.Sfx, YandexGame.savesData.Master, YandexGame.savesData.Music);
    }
}
