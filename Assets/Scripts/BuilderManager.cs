using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class BuilderManager : MonoBehaviour
{
    // Start is called before the first frame update
    private Grid grid;
    public Tilemap tilemap;
    public GameObject ButtonPrefab;
    public Transform Panel;
    
    void Start()
    {
        grid = GetComponent<Grid>();
        createButton();

    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetMouseButtonDown(0)){
        //     Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //     Debug.Log(worldPoint);
        //     Vector3Int position = grid.WorldToCell(worldPoint);
        //     Debug.Log(position);
        //     tilemap.SetTile(position, tile);
        // }
    }

    void createButton()
    {
        var buildings = GetComponentsInChildren<Building>();
        foreach (var building in buildings)
        {
            var Button = Instantiate(ButtonPrefab);
            Button.GetComponent<Image>().sprite = building.Sprite;
            Button.transform.SetParent(Panel);
        }
        
    }
}
