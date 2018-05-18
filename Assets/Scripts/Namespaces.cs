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
