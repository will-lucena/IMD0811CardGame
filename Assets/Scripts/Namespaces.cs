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
        DEAD
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
        void onLeftClickAction();
        void onRightClickAction(UnityEngine.EventSystems.PointerEventData eventData);
    }
}
