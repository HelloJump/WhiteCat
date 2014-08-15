using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EntityManager : MonoBehaviour
{
    void Awake()
    {
        inst = this;
    }

    private Dictionary<int, AbsGameEntity> entityDict = new Dictionary<int, AbsGameEntity>();

    private static EntityManager inst = null;
    public static EntityManager Instance()
    {
        return inst;
    }

    public void RegisterEntity(AbsGameEntity gameEntity)
    {
        if (gameEntity == null)
        {
            Debug.LogError("Entity is null in entityDict from EntityManager.cs");
            return;
        }

        int gameEntityId = gameEntity.GetInstanceID();
        if (entityDict.ContainsKey(gameEntityId))
            Debug.LogWarning("entityDict has already contains key: " + gameEntityId);
        else
            entityDict.Add(gameEntityId, gameEntity);
    }

    public void RemoveEntity(int gameEntityId)
    {
        if (this.entityDict.ContainsKey(gameEntityId))
            this.entityDict.Remove(gameEntityId);
        else
            Debug.LogError("No Entity can remove with id : " + gameEntityId);
    }

    public void RemoveEntity(AbsGameEntity gameEntity)
    {
        if (gameEntity == null)
        {
            Debug.LogError("input gameEntity is null when remove it");
            return;
        }
        int gameEntityId = gameEntity.GetInstanceID();
        RemoveEntity(gameEntityId);
    }

    public AbsGameEntity GetEntityById(int gameEntityId)
    {
        AbsGameEntity gameEntity = null;
        if (!this.entityDict.TryGetValue(gameEntityId, out gameEntity))
        {
            Debug.LogError("No entity with id : " + gameEntityId);
            return null;
        }
        return gameEntity;
    }

    public List<int> GetAttackTarget(int attackSender)
    {
        foreach (int gameEntityId in this.entityDict.Keys)
        {

        }

        return null;
    }
}
