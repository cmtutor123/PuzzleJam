using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CombatManager), typeof(LootManager))]
public class MapManager : MonoBehaviour
{
    private CombatManager combatManager;
    private LootManager lootManager;

    private List<PuzzleRenderer> possiblePuzzlePieces;
    private List<PuzzleRenderer> puzzlePiecePath;

    [Header("Canvas")]
    [SerializeField] private GameObject mapCanvas;

    [Header("Sprites")]
    [SerializeField] private Sprite emptySprite;
    [SerializeField] private Sprite enemySprite;
    [SerializeField] private Sprite lootSprite;

    [Header("Weights")]
    [SerializeField] private List<int> encounterWeights;
    [SerializeField] private List<int> lootWeights;

    [Header("Puzzle Prefab")]
    [SerializeField] private GameObject puzzlePrefab;

    [Header("Encounters")]
    [SerializeField] private List<EnemyEncounter> enemyEncountersDifficulty0;
    [SerializeField] private List<EnemyEncounter> enemyEncountersDifficulty1;
    [SerializeField] private List<EnemyEncounter> enemyEncountersDifficulty2;
    [SerializeField] private List<EnemyEncounter> enemyEncountersDifficulty3;

    [Header("Puzzle Colors")]
    [SerializeField] private List<PuzzleColor> possiblePuzzleColors;

    [Header("Positions")]
    [SerializeField] private float possibilityStart;
    [SerializeField] private float possibilityIncrement;
    [SerializeField] private float positionCenter;
    [SerializeField] private float positionSpacing;
    [SerializeField] private float pathPositionStart;
    [SerializeField] private float pathPositionIncrement;

    private List<List<EncounterPuzzlePiece>> possibilities;
    private List<EncounterPuzzlePiece> currentPath;

    private int currentPosition;

    private bool initLoad = false;

    private List<List<EnemyEncounter>> enemyEncounters;

    void Start()
    {
        combatManager = GetComponent<CombatManager>();
        lootManager = GetComponent<LootManager>();
        enemyEncounters = new List<List<EnemyEncounter>>();
        enemyEncounters.Add(enemyEncountersDifficulty0);
        enemyEncounters.Add(enemyEncountersDifficulty1);
        enemyEncounters.Add(enemyEncountersDifficulty2);
        enemyEncounters.Add(enemyEncountersDifficulty3);
        GeneratePossiblePaths();
        GeneratePuzzleRenderers();
        currentPosition = -1;
        SetPuzzleRendererPositions();
        ShowPuzzlePossibilities();
        ShowPuzzlePath();
        initLoad = true;
        combatManager.DisableCanvas();
    }

    private void Update()
    {
        if (initLoad && possiblePuzzlePieces[0].GetImage() != GetImage(GetEncounterType(0)))
        {
            initLoad = false;
            ShowPuzzlePossibilities();
            ShowPuzzlePath();
        }
    }

    public void GeneratePossiblePaths()
    {
        currentPath = new List<EncounterPuzzlePiece>();
        currentPath.Add(GetStartingPuzzle());
        possibilities = new List<List<EncounterPuzzlePiece>>();
        for (int i = 0; i < 20; i++)
        {
            List<EncounterPuzzlePiece> possibleEncounters = new List<EncounterPuzzlePiece>();
            if (i == 0)
            {
                for (int j = 0; j < 2; j++)
                {
                    possibleEncounters.Add(new EncounterPuzzlePiece(GetEncounterType(i), GetRandomPuzzleColor()));
                }
                possibleEncounters[0].SetEdges(PuzzleEdge.Key, PuzzleEdge.Key);
                possibleEncounters[1].SetEdges(PuzzleEdge.Key, PuzzleEdge.Socket);
            }
            else if (i == 18)
            {
                for (int j = 0; j < 2; j++)
                {
                    possibleEncounters.Add(new EncounterPuzzlePiece(GetEncounterType(i), GetRandomPuzzleColor()));
                }
                possibleEncounters[0].SetEdges(PuzzleEdge.Key, PuzzleEdge.Key);
                possibleEncounters[1].SetEdges(PuzzleEdge.Socket, PuzzleEdge.Key);
            }
            else if (i == 19)
            {
                possibleEncounters.Add(new EncounterPuzzlePiece(GetEncounterType(i), GetRandomPuzzleColor()));
                possibleEncounters[0].SetEdges(PuzzleEdge.Socket, PuzzleEdge.Blank);
            }
            else
            {
                for (int j = 0; j < 4; j++)
                {
                    possibleEncounters.Add(new EncounterPuzzlePiece(GetEncounterType(i), GetRandomPuzzleColor()));
                }
                possibleEncounters[0].SetEdges(PuzzleEdge.Socket, GetRandomEdge());
                possibleEncounters[1].SetEdges(PuzzleEdge.Key, GetRandomEdge());
                possibleEncounters[2].SetEdges(GetRandomEdge(), PuzzleEdge.Socket);
                possibleEncounters[3].SetEdges(GetRandomEdge(), PuzzleEdge.Key);
            }
            possibilities.Add(possibleEncounters);
        }
    }

    public EncounterType GetEncounterType(int progress)
    {
        if (encounterWeights[progress] == 0) return EncounterType.Loot;
        if (lootWeights[progress] == 0) return EncounterType.Enemy;
        int rand = Random.Range(0, encounterWeights[progress] + lootWeights[progress]);
        if (rand < lootWeights[progress]) return EncounterType.Loot;
        else return EncounterType.Enemy;
    }

    public PuzzleEdge GetRandomEdge()
    {
        if (Random.Range(0, 2) == 0) return PuzzleEdge.Key;
        else return PuzzleEdge.Socket;
    }

    public PuzzleColor GetRandomPuzzleColor()
    {
        return possiblePuzzleColors[Random.Range(0, possiblePuzzleColors.Count)];
    }

    public void SetPuzzleRendererPositions()
    {
        int counter = 0;
        for (int i = 0; i < possibilities.Count; i++)
        {
            float xPos;
            if (i < currentPosition + 1 || i > currentPosition + 2 + 1) xPos = -100;
            else xPos = possibilityStart + (i - currentPosition) * possibilityIncrement;
            for (int j = 0; j < possibilities[i].Count; j++)
            {
                float yPos = positionCenter;
                switch (possibilities[i].Count)
                {
                    case 2:
                        yPos = (positionCenter - 0.5f * positionSpacing) + positionSpacing * j;
                        break;
                    case 4:
                        yPos = (positionCenter - 1.5f * positionSpacing) + positionSpacing * j;
                        break;
                }
                possiblePuzzlePieces[counter++].SetPosition(xPos, yPos);
            }
        }
        counter = 0;
        for (int i = 0; i < puzzlePiecePath.Count; i++)
        {
            float xPos;
            if (i > currentPosition + 1) xPos = -100;
            else xPos = pathPositionStart + (currentPosition - i) * -pathPositionIncrement;
            puzzlePiecePath[counter++].SetPosition(xPos, 0);
        }
    }

    public void ShowPuzzlePossibilities()
    {
        int counter = 0;
        for (int i = 0; i < possibilities.Count; i++)
        {
            for (int j = 0; j < possibilities[i].Count; j++)
            {
                PuzzlePiece temp = new PuzzlePiece(possibilities[i][j], GetImage(possibilities[i][j].GetEncounterType()));
                possiblePuzzlePieces[counter++].UpdateSprites(temp);
            }
        }
    }

    public void ShowPuzzlePath()
    {
        for (int i = 0; i < currentPath.Count; i++)
        {
            puzzlePiecePath[i].UpdateSprites(new PuzzlePiece(currentPath[i], GetImage(currentPath[i].GetEncounterType())));
        }
    }

    public void HidePuzzlePossibilities()
    {
        foreach (PuzzleRenderer puz in possiblePuzzlePieces)
        {
            puz.UnloadSprites();
        }
    }

    public void HidePuzzlePath()
    {
        foreach (PuzzleRenderer puz in puzzlePiecePath)
        {
            puz.UnloadSprites();
        }
    }

    public void GeneratePuzzleRenderers()
    {
        puzzlePiecePath = new List<PuzzleRenderer>();
        possiblePuzzlePieces = new List<PuzzleRenderer>();
        int pathCount = 21;
        int possibleCount = 73;
        for (int i = 0; i < pathCount; i++)
        {
            puzzlePiecePath.Add(Instantiate(puzzlePrefab, mapCanvas.transform).GetComponent<PuzzleRenderer>());
        }
        for (int i = 0; i < possibleCount; i++)
        {
            possiblePuzzlePieces.Add(Instantiate(puzzlePrefab, mapCanvas.transform).GetComponent<PuzzleRenderer>());
        }
        int counter = 0;
        for (int i = 0; i < possibilities.Count; i++)
        {
            for (int j = 0; j < possibilities[i].Count; j++)
            {
                PuzzleRenderer renderer = possiblePuzzlePieces[counter++];
                Button button = renderer.gameObject.AddComponent<Button>();
                MapPuzzleLocation location = button.gameObject.AddComponent<MapPuzzleLocation>();
                location.SetDistance(i);
                location.SetNumber(j);
                button.onClick.AddListener(() => OnMapPuzzleClick(location.GetDistance(), location.GetNumber()));
            }
        }
    }

    public EncounterPuzzlePiece GetStartingPuzzle()
    {
        EncounterPuzzlePiece temp = new EncounterPuzzlePiece(EncounterType.None, possiblePuzzleColors[0]);
        temp.SetEdges(PuzzleEdge.Blank, PuzzleEdge.Socket);
        return temp;
    }

    public Sprite GetImage(EncounterType encounterType)
    {
        switch (encounterType)
        {
            case EncounterType.None:
                return emptySprite;
            case EncounterType.Enemy:
                return enemySprite;
            case EncounterType.Loot:
                return lootSprite;
        }
        return emptySprite;
    }

    public void OnMapPuzzleClick(int distance, int number)
    {
        if (TestValidNextPuzzle(distance, number)) PickNextPuzzle(distance, number);
    }

    public void DisableCanvas()
    {
        mapCanvas.SetActive(false);
    }

    public void EnableCanvas()
    {
        mapCanvas.SetActive(true);
        if (currentPosition == 19)
        {
            WinScreen();
        }
    }

    public bool TestValidNextPuzzle(int distance, int number)
    {
        return distance == currentPosition + 1 && PuzzleBoard.EdgesDoConnect(possibilities[distance][number].GetLeft(), currentPath[currentPosition + 1].GetRight());
    }

    public void PickNextPuzzle(int distance, int number)
    {
        currentPath.Add(possibilities[distance][number]);
        currentPosition++;
        SetPuzzleRendererPositions();
        ShowPuzzlePath();
        EncounterPuzzlePiece currentPiece = currentPath[currentPath.Count - 1];
        switch (currentPiece.GetEncounterType())
        {
            case EncounterType.Enemy:
                StartEncounter();
                break;
            case EncounterType.Loot:
                StartLoot();
                break;
        }
    }

    public void StartEncounter()
    {
        int encounterDifficulty = Mathf.FloorToInt(currentPosition / 4);
        EnemyEncounter currentEncounter = GetEncounter(encounterDifficulty);
        combatManager.StartEncounter(currentEncounter);
        DisableCanvas();
    }

    public void StartLoot()
    {
        lootManager.StartLootSelection();
        DisableCanvas();
    }

    public EnemyEncounter GetEncounter(int difficulty)
    {
        return enemyEncounters[difficulty][Mathf.Clamp(Random.Range(0, enemyEncounters[difficulty].Count), 0, 3)];
    }

    public void GameOver()
    {
        SceneManager.LoadScene("LoseGame");
    }

    public void WinScreen()
    {
        SceneManager.LoadScene("WinGame");
    }
}
