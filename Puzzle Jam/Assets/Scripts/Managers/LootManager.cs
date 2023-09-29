using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(MapManager), typeof(PlayerManager))]
public class LootManager : MonoBehaviour
{
    [SerializeField] private List<PuzzleData> possiblePieces;

    [SerializeField] private GameObject lootCanvas;

    [SerializeField] private List<PuzzleRenderer> renderers;

    [SerializeField] private TooltipManager tooltipManager;

    private List<PuzzlePiece> selections;

    private MapManager mapManager;
    private PlayerManager playerManager;

    public void Start()
    {
        mapManager = GetComponent<MapManager>();
        playerManager = GetComponent<PlayerManager>();
        for (int i = 0; i < renderers.Count; i++)
        {
            EventTrigger eventTrigger = renderers[i].gameObject.AddComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerEnter;
            int param = i;
            entry.callback.AddListener((eventData) => UpdateTooltip(param));
            eventTrigger.triggers.Add(entry);
            entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerExit;
            entry.callback.AddListener((eventData) => UnloadTooltip());
            eventTrigger.triggers.Add(entry);
            Button button = renderers[i].gameObject.AddComponent<Button>();
            button.onClick.AddListener(() => SelectLoot(param));
        }
        DisableCanvas();
    }

    public void UpdateTooltip(int selection)
    {
        PuzzlePiece piece = selections[selection];
        tooltipManager.SetSprite(piece.GetImage());
        tooltipManager.SetText(piece.GetName(), piece.GetDescription());
    }

    public void UnloadTooltip()
    {
        tooltipManager.UnloadSprites();
    }

    public List<PuzzlePiece> GetPuzzlePieces(int amount)
    {
        List<PuzzleData> temp = new List<PuzzleData>();
        temp.AddRange(possiblePieces);
        List<PuzzlePiece> selected = new List<PuzzlePiece>();
        for (int i = 0; i < amount && temp.Count > 0; i++)
        {
            int rand = Random.Range(0, temp.Count);
            selected.Add(new PuzzlePiece(temp[rand]));
            temp.RemoveAt(rand);
        }
        return selected;
    }

    public void DisableCanvas()
    {
        lootCanvas.SetActive(false);
    }

    public void EnableCanvas()
    {
        lootCanvas.SetActive(true);
    }

    public void StartLootSelection()
    {
        selections = GetPuzzlePieces(6);
        EnableCanvas();
        UpdatePuzzleRenderers();
    }

    public void SelectLoot(int index)
    {
        SelectLoot(selections[index]);
    }

    public void SelectLoot(PuzzlePiece piece)
    {
        DisableCanvas();
        playerManager.AddToDeck(piece);
        mapManager.EnableCanvas();
    }

    public void UpdatePuzzleRenderers()
    {
        HidePuzzleRenderers();
        for (int i = 0; i < renderers.Count; i++)
        {
            renderers[i].UpdateSprites(selections[i]);
        }
    }

    public void HidePuzzleRenderers()
    {
        foreach (PuzzleRenderer renderer in renderers)
        {
            renderer.UnloadSprites();
        }
    }
}
