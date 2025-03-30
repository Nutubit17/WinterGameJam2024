using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntity
{
    EntityStatus Status { get; set; }
}

public interface IEntityComponent
{
    void Init(IEntity entity);
}

[System.Serializable]
public class EntityStatus
{
    [field: SerializeField] public int MaxHp { get; private set; } = 5;
    public int CurrentHp { get; private set; }

    [field: Space(10)]
    [field: SerializeField]
    public float Speed { get; private set; } = 5;

    [field: Space(10)]
    [field: SerializeField]
    public float MaxStamina { get; private set; } = 5;

    public float CurrentStamina { get; private set; }


    public void Init()
    {
        CurrentHp = MaxHp;
        CurrentStamina = MaxStamina;
    }

    public void AddHp(int amount)
    {
        CurrentHp = Mathf.Clamp(CurrentHp + amount, 0, MaxHp);
    }

    public void AddStamina(float amount)
    {
        CurrentStamina = Mathf.Clamp(CurrentStamina + amount, 0, MaxStamina);
    }

    public void ExtentStamina(float amount)
    {
        MaxStamina += amount;
    }

    public void AddSpeed(float speed)
    {
        Speed += speed;
    }
}