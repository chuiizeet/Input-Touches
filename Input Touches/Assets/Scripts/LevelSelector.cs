using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSelector : MonoBehaviour {

    public GameObject levelHolder;
    public GameObject levelIcon;
    public GameObject thisCanvas;
    public Vector2 iconSpacing;
    public int numberOFLevels = 50;
    private Rect panelDimension;
    private Rect iconDimensions;
    private int amountPerPage;
    private int currentLevelCount;

    private void Start() {
        panelDimension = levelHolder.GetComponent<RectTransform>().rect;
        iconDimensions = levelIcon.GetComponent<RectTransform>().rect;
        int maxInARow = Mathf.FloorToInt((panelDimension.width + iconSpacing.x) / (iconDimensions.width + iconSpacing.x));
        int maxInACol = Mathf.FloorToInt((panelDimension.height + iconSpacing.y) / (iconDimensions.height + iconSpacing.y));
        amountPerPage = maxInARow * maxInACol;
        int totalPages = Mathf.CeilToInt((float)numberOFLevels / amountPerPage);
        LoadPanels(totalPages);
    }

    private void LoadPanels(int numberOfPanels) {
        GameObject panelClone = Instantiate(levelHolder) as GameObject;

        for (int i = 1; i <= numberOfPanels; i++) {
            GameObject panel = Instantiate(panelClone) as GameObject;
            panel.transform.SetParent(thisCanvas.transform, false);
            panel.transform.SetParent(levelHolder.transform);
            panel.name = "Page-" + i;
            panel.GetComponent<RectTransform>().localPosition = new Vector2(panelDimension.width * (i-1), 0);
            SetupGrid(panel);
            int numberOfIcons = i == numberOfPanels ? numberOFLevels - currentLevelCount : amountPerPage;
            LoadIcons(numberOfIcons,panel);
        }
        Destroy(panelClone);
    }

    private void SetupGrid(GameObject panel) {
        GridLayoutGroup grid = panel.AddComponent<GridLayoutGroup>();
        grid.cellSize = new Vector2(iconDimensions.width, iconDimensions.height);
        grid.childAlignment = TextAnchor.MiddleCenter;
        grid.spacing = iconSpacing;
    }

    private void LoadIcons(int numberOfIcons, GameObject parentObject) {
        for (int i = 1; i <= numberOfIcons; i++) {
            currentLevelCount++;
            GameObject icon = Instantiate(levelIcon) as GameObject;
            icon.transform.SetParent(thisCanvas.transform, false);
            icon.transform.SetParent(parentObject.transform);
            icon.name = "Level-" + i;
            icon.GetComponentInChildren<TextMeshProUGUI>().text = "Level-" + i;
            // icon.GetComponent<TextMeshProUGUI>().text = "Level-" + i;
        }
    }

    private void Update() {
        
    }

}
