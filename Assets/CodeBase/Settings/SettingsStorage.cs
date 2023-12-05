using CodeBase.DataSaver.Common;
using CodeBase.DataSaver.Interfaces;
using CodeBase.Settings.Common;

namespace CodeBase.Settings
{
    public class SettingsStorage
    {
        [Savable(nameof(IsFirstPlay), SaveMethod.PlayerPrefs)]
        public SettingsBoolVariable IsFirstPlay;
        [Savable(nameof(SoundEnabled), SaveMethod.PlayerPrefs)]
        public SettingsBoolVariable SoundEnabled;

        private readonly ILoader _loader;
        private readonly ISaver _saver;
        
        public SettingsStorage(ILoader loader, ISaver saver)
        {
            _loader = loader;
            _saver = saver;
        }

        public void LoadFromMemory()
        {
            if (!_loader.Load(out IsFirstPlay))
            {
                InitiateFirstPlay();
                return;
            }

            _loader.Load(out SoundEnabled);
        }

        public void Save()
        {
            _saver.Save(IsFirstPlay);
            _saver.Save(SoundEnabled);
        }

        private void InitiateFirstPlay()
        {
            IsFirstPlay = new SettingsBoolVariable(nameof(IsFirstPlay), true);
            SoundEnabled = new SettingsBoolVariable(nameof(SoundEnabled), true);
            Save();
        }
    }
}