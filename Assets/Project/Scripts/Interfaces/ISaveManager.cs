namespace Military.Scripts.Interfaces
{
    public interface ISaveManager
    {
        public void Save(string mapName);
        public void Load(string mapName);
    }
}