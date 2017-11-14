namespace Bot2048
{
    public interface IGameBot
    {
        void Initialize();
        Direction GetDirection(GameGrid grid);
        void Deinitialize();
    }
}