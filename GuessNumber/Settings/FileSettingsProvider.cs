namespace GuessNumber.Settings;

public sealed class FileSettingsProvider : ISettingsProvider
{
    private readonly string _path;
    public FileSettingsProvider(string path) => _path = path;

    public GameSettings Load() => throw new NotImplementedException();
    public void Save(GameSettings settings) => throw new NotImplementedException();
}