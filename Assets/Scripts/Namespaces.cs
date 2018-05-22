namespace Enums
{
    public enum Type
    {
        HERO,
        WEAPON,
        ARMOR,
        FIELD,
        MAGIC
    }

    public enum State
    {
        ACTIVE, 
        SLEEPING,
    }
}

namespace Interfaces
{
    public interface ITargetable
    {
        void subscribeToClickable();
    }

    public interface IClickableAction
    {
        void onClickAction();
    }
}
