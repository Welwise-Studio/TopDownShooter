
using System.Collections.Generic;
namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // Тестовые сохранения для демо сцены
        // Можно удалить этот код, но тогда удалите и демо (папка Example)
        public int money = 1;                       // Можно задать полям значения по умолчанию
        public string newPlayerName = "Hello!";
        public bool[] openLevels = new bool[3];

        // Ваши сохранения
        public Dictionary<string, string> Storage = new Dictionary<string, string>();
        public Dictionary<string, bool> openedWeapons = new Dictionary<string, bool>();
        // ...

        // Поля (сохранения) можно удалять и создавать новые. При обновлении игры сохранения ломаться не должны

        // REFACTORED
        public int Balance;
        public float Sfx = .5f;
        public float Master = .5f;
        public float Music = .5f;

        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
            // Допустим, задать значения по умолчанию для отдельных элементов массива

            openLevels[1] = true;
            openedWeapons["pistol"] = true;
            Balance = 0;
        }
    }
}
