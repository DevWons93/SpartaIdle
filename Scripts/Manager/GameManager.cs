using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GameManager : Singleton<GameManager>
{
    private bool IsBattleEnd = false;
    private bool IsNextStage = false;
    private int curStageNum = 1;
    private Player player;
    private List<Enemy> enemies;

    private void Start()
    {
        enemies = new List<Enemy>();

        StartGame();        
    }

    private void Update()
    {      
    }

    public bool CheckBattleEnd()
    {
        return IsBattleEnd;
    }

    public bool CheckNextStage()
    {
        return IsNextStage;
    }

    private void StartGame()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Player>();
        player.StartPos = GameObject.FindGameObjectWithTag("Stage").gameObject.transform.GetChild(1).position;
        InitStage();
    }

    private void InitStage()
    {   
        float startPos = curStageNum / 2f - 0.5f;
        GameObject parent = GameObject.FindGameObjectWithTag("Enemy");
        for (int i = 0; i < curStageNum; i++)
        {
            GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Enemy.prefab");
            GameObject enemy = Instantiate(obj, parent.transform);
            enemy.transform.position = new Vector3(startPos - i, 0, 0) + GameObject.FindGameObjectWithTag("Stage").gameObject.transform.GetChild(2).position;
            enemy.GetComponent<Enemy>().Target = player;
            enemies.Add(enemy.GetComponent<Enemy>());            
        }               
        
        player.Targets = enemies;
        IsBattleEnd = false;
        IsNextStage = false;
    }

    public void RemoveEnemy(Health enemy)
    {
        enemies.Remove(enemy.GetComponent<Enemy>());
        if (enemies.Count == 0) IsBattleEnd = true;
    }

    public void ReadyToNextStage()
    {
        curStageNum++;
        IsNextStage = true;
        InitStage();        
    }
}
