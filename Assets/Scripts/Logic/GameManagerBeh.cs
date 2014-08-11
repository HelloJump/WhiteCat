using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using whitecat.input;

public class GameManagerBeh : MonoBehaviour
{
    private Vector3 warriorInitPos = new Vector3(0f,0.55f,0f);
    private const float monsterRangeXMin = -10f;
    private const float monsterRangeXMax = 3f;
    private const float monsterRangeZMin = 5f;
    private const float monsterRangeZMax = 10f;
    private const float monsterRangeY = 0.33f;

    private const float monsterReduceBleed = 400f;
    private const float monsterMaxBleed = 1000f;

    private const string monsterMaxHp = "Hp";

    public GameObject Warrior;
    public GameObject Monster;
    public ScreenInputBeh inputGO;

    private static GameManagerBeh inst = null;
    public static GameManagerBeh Instance()
    {
        return inst;
    }

    void Awake()
    {
        inst = this;
    }

    void OnDestroy()
    {
        inst = null;
    }

    public GameObject warriorInst = null;
    public GameObject mainCamera;
    void Start()
    {
        if (this.warriorInst == null)
        {
            warriorInst = GameObject.Instantiate(Warrior) as GameObject;
            warriorInst.transform.localPosition = this.warriorInitPos;
            warriorInst.GetComponent<CameraFollowScript>().followCamera = this.mainCamera;
            inputGO.InputMessageReceiver = warriorInst;
            GetWarriorBeh().Bb.SetDataValue(monsterMaxHp, monsterMaxBleed);
        }
        InvokeRepeating("CreateMonster", 1f, Random.Range(2f,8f));
    }

    public Dictionary<string, GameObject> monsterDict = new Dictionary<string,GameObject>();

    private int tmpMonsterIdx = 0;
    private void CreateMonster()
    {
        GameObject monsterInst = GameObject.Instantiate(Monster) as GameObject;
        float x = Random.Range(monsterRangeXMin, monsterRangeXMax);
        float z = Random.Range(monsterRangeZMin, monsterRangeZMax);
        monsterInst.transform.localPosition = new Vector3(x, monsterRangeY, z);
        monsterDict.Add(tmpMonsterIdx.ToString(), monsterInst);
        GetMonsterBeh(monsterInst).Bb.SetDataValue("MonsterId", tmpMonsterIdx.ToString());
        this.tmpMonsterIdx++;
        if (this.tmpMonsterIdx == 1)
            CancelInvoke("CreateMonster");
    }

    public WarriorBeh GetWarriorBeh()
    {
        return this.warriorInst.GetComponent<WarriorBeh>();
    }

    public MonsterBeh GetMonsterBeh(GameObject monsterInst)
    {
        return monsterInst.GetComponent<MonsterBeh>();
    }

    public void WarriorAttack()
    {
        WarriorBeh warrior = this.GetWarriorBeh();
        Dictionary<string, float> monsterDistance = warrior.GetAttackTargetDistance();
        List<string> removeList = new List<string>();
        foreach (string id in monsterDistance.Keys)
        {
            if (monsterDistance[id] < warrior.warriorAttackDistance && this.monsterDict.ContainsKey(id))
            {
                MonsterBeh monster = GetMonsterBeh(this.monsterDict[id]);
                monster.Fsm.SendEvent("MonsterDamage");
                float hp = monster.Bb.GetDataValue<float>("Hp");
                monster.Bb.SetDataValue("Hp", hp - monsterReduceBleed);
                if (hp - monsterReduceBleed <= 0)
                    removeList.Add(id);

                GetMonsterBeh(this.monsterDict[id]).Fsm.SendEvent("MonsterDamage");
            }
        }

        for (int i = 0; i < removeList.Count; i++)
        {
            this.monsterDict.Remove(removeList[i]);
        }
    }
}
