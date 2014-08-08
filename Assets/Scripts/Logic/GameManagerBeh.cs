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
        }
        InvokeRepeating("CreateMonster", 1f, Random.Range(2f,8f));
    }

    public List<GameObject> monsterList = new List<GameObject>();

    private void CreateMonster()
    {
        GameObject monsterInst = GameObject.Instantiate(Monster) as GameObject;
        float x = Random.Range(monsterRangeXMin, monsterRangeXMax);
        float z = Random.Range(monsterRangeZMin, monsterRangeZMax);
        monsterInst.transform.localPosition = new Vector3(x, monsterRangeY, z);
        monsterList.Add(monsterInst);
    }

    public WarriorBeh GetWarriorBeh()
    {
        return this.warriorInst.GetComponent<WarriorBeh>();
    }

    public MonsterBeh GetMonsterBeh(GameObject monsterInst)
    {
        return monsterInst.GetComponent<MonsterBeh>();
    }
}
