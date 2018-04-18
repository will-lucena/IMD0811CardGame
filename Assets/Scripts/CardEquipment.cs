using UnityEngine;
using Enums;

[CreateAssetMenu(fileName = "Equip", menuName = "Cards/Equip")]
public class CardEquipment : CardAbstract
{
    [SerializeField] private Type equipType;

    // Use this for initialization
    void Start()
    {
        type = equipType;
    }
}
