using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class MapGenerator : MonoBehaviour
{
    private int[] floorPlan;
    private int floorPlanCount;
    private int minRooms;
    private int maxRooms;
    private List<int> endRooms;
    private int bossRoomIndex;
    private int shopRoomIndex;
    private int itemRoomIndex;
    public Cell cellPrefab;
    private float cellWidth;
    private float cellHeight;   
    private Queue<int> cellQueue;
    private List<Cell> spawnedCells;

    [Header("Sprite References")]
    [SerializeField] private GameObject dmgitem;
    [SerializeField] private GameObject stmitem;
    [SerializeField] private GameObject spditem;
    [SerializeField] private GameObject abiitem;
    [SerializeField] private GameObject atkitem;
    [SerializeField] private GameObject dshitem;
    private GameObject[] itemlist = new GameObject[6];
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject enemyPrefab;

    void Start()
    {
        itemlist[0] = dmgitem;
        itemlist[1] = stmitem;
        itemlist[2] = spditem;
        itemlist[3] = abiitem;
        itemlist[4] = atkitem;
        itemlist[5] = dshitem;
        minRooms = 7;
        maxRooms = 15;
        cellHeight = 12f;
        cellWidth = cellHeight * (16f / 9f);
        spawnedCells = new();
        
        SetupDungeon();
    }

    void SetupDungeon()
    {
        for(int i = 0; i < spawnedCells.Count; i++)
        {
            Destroy(spawnedCells[i].gameObject);
        }
        spawnedCells.Clear();
        floorPlan = new int[100];
        floorPlanCount = default;
        cellQueue = new Queue<int>();
        endRooms = new List<int>();
        VisitCell(45);
        GenerateDungeon();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            int startIndex = 45;
            int x = startIndex % 10;
            int y = startIndex / 10;

            Vector2 startPos = new Vector2(x * cellWidth, y * cellHeight);
            player.transform.position = startPos;
        }
    }

    void GenerateDungeon()
    {
        while(cellQueue.Count > 0)
        {
            int index = cellQueue.Dequeue();
            int x = index % 10;
            bool created = false;
            if (x > 1) created |= VisitCell(index - 1);
            if (x < 9) created |= VisitCell(index + 1);
            if (index >= 10) created |= VisitCell(index - 10);
            if (index < 90) created |= VisitCell(index + 10);
            if (created == false)
                endRooms.Add(index);
        }
        if(floorPlanCount < minRooms)
        {
            SetupDungeon();
            return;
        }
        SetupSpecialRooms();
        foreach (var cell in spawnedCells)
        {
            cell.SetupDoors(this);
        }
    }
    void SetupSpecialRooms()
    {
        bossRoomIndex = endRooms.Count > 0 ? endRooms[endRooms.Count - 1] : -1;

        if(bossRoomIndex != -1)
        {
            endRooms.RemoveAt(endRooms.Count - 1);
        }

        itemRoomIndex = RandomEndRoom();
        shopRoomIndex = RandomEndRoom();

        if (itemRoomIndex == -1 || shopRoomIndex == -1 || bossRoomIndex == -1)
        {
            SetupDungeon();
            return;
        }
        UpdateSpecialRoomVisuals();
    }
    void UpdateSpecialRoomVisuals()
    {
        foreach(var cell in spawnedCells)
        {
            if(cell.index == itemRoomIndex)
            {
                Vector2 pos = cell.transform.position;
                int itemval = Random.Range(0,5);
                cell.SpawnItem(itemlist[itemval], pos);
            }
            if(cell.index == shopRoomIndex)
            //FIX IT
            {
                //cell.SetSpecialRoomSprite(shop);
            }
            if(cell.index == bossRoomIndex)
            {
                Vector2 pos = cell.transform.position;
                cell.SpawnBoss(boss, pos);
            }
        }
    }

    int RandomEndRoom()
    {
        if (endRooms.Count == 0) return -1;
        int randomRoom = UnityEngine.Random.Range(0, endRooms.Count);
        int index = endRooms[randomRoom];

        endRooms.RemoveAt(randomRoom);

        return index;
    }

    private int GetNeighbourCount(int index)
    {
        int count = 0;

        if (index - 10 >= 0) count += floorPlan[index - 10];
        if (index + 10 < floorPlan.Length) count += floorPlan[index + 10];
        if (index % 10 != 0) count += floorPlan[index - 1];
        if (index % 10 != 9) count += floorPlan[index + 1];

        return count;
    }
    private bool VisitCell(int index)
    {
        if (floorPlan[index] != 0 || GetNeighbourCount(index) > 1 || floorPlanCount > maxRooms || UnityEngine.Random.value < 0.5f)
            return false;
        cellQueue.Enqueue(index);
        floorPlan[index] = 1;
        floorPlanCount++;
        SpawnRoom(index);
        return true;
    }
    private void SpawnRoom(int index)
    {
        int x = index % 10;
        int y = index / 10;
        Vector2 position = new Vector2(x * cellWidth, y * cellHeight);

        Cell newCell = Instantiate(cellPrefab, position, Quaternion.identity);
        newCell.value = 1;
        newCell.index = index;

        spawnedCells.Add(newCell);

        SpawnEnemies(newCell);
    }

    private void SpawnEnemies(Cell room)
    {
        for(int i = 0; i < 3; i++)
        {
            float randX = UnityEngine.Random.Range(-5f, 5f);
            float randY = UnityEngine.Random.Range(-5f, 5f);

            Vector2 spawnPos = (Vector2)room.transform.position + new Vector2(randX, randY);

            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        }
    }
    public bool RoomExists(int index)
    {
        if (index < 0 || index >= floorPlan.Length)
            return false;
        return floorPlan[index] == 1;
    }
}
