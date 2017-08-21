using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TileInput : MonoBehaviour
{
    [SerializeField]
    private GameObject Cursor;
    [SerializeField]
    private TowerContainer tower;
    [SerializeField]
    private AudioClip build;

    private List<Vector2> towerCoordinates;

    public int towerNum;

    private int _SelectedTowerNum;
    public int SelectedTowerNum
    {
        get
        {
            return _SelectedTowerNum;
        }
        set
        {
            if (_SelectedTowerNum != value)
            {
                if (value == 0)
                {
                    SelectedTowerGameObject.GetComponent<TowerInfo>().isActive = true;
                    SelectedTowerGameObject = null;
                }
                else
                {
                    foreach (var item in GameObject.FindGameObjectsWithTag("Tower"))
                    {
                        if (!item.Equals(SelectedTowerGameObject))
                        {
                            foreach (var item1 in item.GetComponentsInChildren<Renderer>())
                            {
                                foreach (var item2 in item1.materials)
                                {
                                    item2.color = new Color(item2.color.r, item2.color.g, item2.color.b, 0.2f);
                                }
                            }
                        }
                    }

                    SelectedTowerGameObject = Instantiate(tower.Towers[value - 1].towerPrefab);
                    SelectedTowerGameObject.name = tower.Towers[value - 1].towerName;
                    SelectedTowerGameObject.transform.SetParent(transform.parent);
                    TowerInfo ti = SelectedTowerGameObject.GetComponent<TowerInfo>();
                    ti.towerName = tower.Towers[value - 1].towerName;
                    ti.towerType = tower.Towers[value - 1].towerType;
                    ti.damage = tower.Towers[value - 1].damage;
                    ti.price = tower.Towers[value - 1].price;
                    ti.destoryCallback = () => 
                    {
                        towerCoordinates.Remove(ti.TowerCoordinate);
                        Context.Towers.Remove(ti.transform);
                        if (!ti.towerName.Equals("Trap"))
                        {
                            GameObject bb = Instantiate(Context.prefabLoader.TowerDestroyEffect);
                            bb.transform.position = ti.transform.position;
                        }
                    };
                    ti.isActive = false;
                    towerNum++;
                    Context.Towers.Add(SelectedTowerGameObject.transform);
                }
                _SelectedTowerNum = value;
            }
        }
    }

    private GameObject SelectedTowerGameObject;

    static public Action<bool> CursorActive;

    private void Start()
    {
        Context.tileInput = this;
        CursorActive = (bool a) => { Cursor.SetActive(a); };
        towerCoordinates = new List<Vector2>();
    }

    private void Update()
    {
        if (PlayMode.mode != PlayMode.Mode.EDIT)
            return;

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).localScale = Vector3.Lerp(transform.GetChild(i).localScale, Vector3.one, 0.1f);
        }
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(r, out hit))
        {
            if (hit.transform.gameObject.CompareTag("Tile"))
            {
                var crd = hit.transform.gameObject.name.Split(' ');
                var ttp = new Vector2(int.Parse(crd[0]), int.Parse(crd[1]));
                hit.transform.localScale = Vector3.Lerp(hit.transform.localScale, Vector3.one * 1.2f, 0.1f);
                if (SelectedTowerGameObject != null && !towerCoordinates.Contains(ttp))
                {
                    Cursor.SetActive(false);
                    SelectedTowerGameObject.SetActive(true);
                    SelectedTowerGameObject.transform.position = new Vector3(hit.transform.position.x, SelectedTowerGameObject.transform.position.y, hit.transform.position.z);
                }
                else
                {
                    Cursor.SetActive(true);
                    Cursor.transform.position = new Vector3(hit.transform.position.x, hit.transform.gameObject.GetComponent<BoxCollider>().size.z, hit.transform.position.z);
                }
                if (Input.GetMouseButtonDown(0))
                {
                    if (SelectedTowerGameObject != null)
                    {
                        foreach (var item in GameObject.FindGameObjectsWithTag("Tower"))
                        {
                            if (!item.Equals(SelectedTowerGameObject))
                            {
                                foreach (var item1 in item.GetComponentsInChildren<Renderer>())
                                {
                                    foreach (var item2 in item1.materials)
                                    {
                                        item2.color = new Color(item2.color.r, item2.color.g, item2.color.b, 1);
                                    }
                                }
                            }
                        }
                        SelectedTowerGameObject.GetComponent<TowerInfo>().TowerCoordinate = ttp;
                        towerCoordinates.Add(ttp);
                        GetComponent<AudioSource>().clip = build;
                        GetComponent<AudioSource>().Play();
                    }
                    SelectedTowerNum = 0;
                }
            }
        }
        else
        {
            Cursor.SetActive(false);
            if (SelectedTowerGameObject != null && SelectedTowerGameObject.activeSelf)
            {
                SelectedTowerGameObject.SetActive(false);
            }
        }
    }

}
