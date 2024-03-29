﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Data/EnemyLimit")]
public class EnemyLimit : ScriptableObject {
    public Enemy EnemyPrefab;
    public int Max;
}
     
public class EnemySpawnControl : MonoBehaviour
{
    [SerializeField] private Entity player;
    [SerializeField] private List<EnemyLimit> enemies;
    [SerializeField] private EvilPoints evilPoints;
    [SerializeField] private float spawnAtZDistanceFromPlayer = 100;
    [SerializeField] private Vector3 spawnVolume = new Vector3(100, 100, 10);
    [SerializeField] private float spawnEachPassedUnits = 10;

    private Transform playerTransform;
    private Vector3 playerStartPos;
    private float nextZPosSpawn;
    private int treeDepth = 3;

    private void Awake() {
        if (!player) {
            Debug.LogError("Player is null");
            return;
        }

        playerTransform = player.transform;
        playerStartPos = playerTransform.position;
        nextZPosSpawn = playerStartPos.z + spawnEachPassedUnits;
    }


    private void Update() {
        if (IsSpawnTime())
            SpawnEnemies(new Vector3(0, 0, playerTransform.position.z + spawnAtZDistanceFromPlayer), spawnVolume, GetEnemiesForSpawn());
    }
    
    bool IsSpawnTime() {
        if (playerTransform.position.z > nextZPosSpawn) {
            nextZPosSpawn += spawnEachPassedUnits;
            return true;
        }
        return false;
    }

    List<Enemy> GetEnemiesForSpawn() {
        if (!evilPoints) {
            Debug.LogError("EvilPoints is null");
            return null;
        }
        List<Enemy> result = new List<Enemy>();

        foreach (EnemyLimit el in enemies.OrderByDescending(t => t.EnemyPrefab.EnemyParams.WorthPrice).ThenBy(t => t.EnemyPrefab.EnemyParams.Price)) {
            int availableEnemyCount = el.Max;
            while (evilPoints.GetEvilPoints() >= el.EnemyPrefab.EnemyParams.Price && availableEnemyCount > 0) {
                result.Add(el.EnemyPrefab);
                availableEnemyCount--;
                evilPoints.SpendPoints(el.EnemyPrefab.EnemyParams.Price);
            }
        }

        return result;
    }

    QuadTree qTree;
    void SpawnEnemies(Vector3 pos, Vector3 size, List<Enemy> enemies) {
        //if (enemies.Count > 0)
        //    Debug.Log("Spawning " + enemies.Count + " enemies in " + pos + " in volume " + size);

        //делим пространство на ячейки
        qTree = new QuadTree(pos, size, treeDepth);
        
        //словарь, где ключ базовые 4 ноды, а значение все ее свободные листья
        Dictionary<QuadTree.QuadTreeNode, List<QuadTree.QuadTreeNode>> baseNodesLists = new Dictionary<QuadTree.QuadTreeNode, List<QuadTree.QuadTreeNode>>();
        for (int i = 0; i < qTree.RootNode.Nodes.Length; i++) {
            baseNodesLists.Add(qTree.RootNode.Nodes[i], qTree.RootNode.Nodes[i].GetAllFreeNodes());
        }
        int currentFreeNodesCount = baseNodesLists.Sum(t => t.Value.Count);

        for (int i = 0; i < baseNodesLists.Count;) {
            if (enemies.Count == 0 || currentFreeNodesCount == 0) 
                break;
            if (baseNodesLists.ElementAt(i).Value.Count == 0) {
                i++;
                continue;
            }

            int randomLeafID = Random.Range(0, baseNodesLists.ElementAt(i).Value.Count);
            SpawnEnemy(enemies[0], baseNodesLists.ElementAt(i).Value[randomLeafID].Position);
            enemies.RemoveAt(0);

            baseNodesLists.ElementAt(i).Value[randomLeafID].IsFree = false;
            currentFreeNodesCount--;
            baseNodesLists.ElementAt(i).Value.RemoveAt(randomLeafID);
            i = (i + 1) % baseNodesLists.Count;
        }


    }

    void SpawnEnemy(Enemy enemyPrefab, Vector3 pos) {
        Enemy enemy = Instantiate(enemyPrefab);
        enemy.transform.position = pos;
    }

    
    //void OnDrawGizmos() {
    //    if (qTree != null)
    //        DrawNode(qTree.RootNode);
    //}

    //private void DrawNode(QuadTree.QuadTreeNode node, int nodeDepth = 0) {
    //    if (!node.IsLeaf()) {
    //        foreach (var subnode in node.Nodes) {
    //            DrawNode(subnode, nodeDepth + 1);
    //        }
    //    }
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireCube(node.Position, node.Size);
    //}
}
