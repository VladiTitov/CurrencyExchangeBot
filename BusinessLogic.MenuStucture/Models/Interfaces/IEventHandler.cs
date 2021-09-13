namespace BusinessLogic.MenuStucture.Models.Interfaces
{
    public interface IEventHandler
    {
        void MessageProcess(string message);

        void CallbackProcess(string message);
    }
}
