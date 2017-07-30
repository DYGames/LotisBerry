﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class WaveMng : MonoBehaviour
{
    public class MonsterAmount
    {
        public GameObject Monster;
        public int Amount;

        public MonsterAmount(GameObject m, int a) { Monster = m; Amount = a; }
    }

    [SerializeField]
    private Transform Spawn;

    [SerializeField]
    private List<GameObject> MonsterPrefabs;

    private List<List<MonsterAmount>> SpawnList;

    [SerializeField]
    private TextAsset waveXml;

    public static int Wave;

    public static Action WaveEndCallback;
    public static int waveState;
    private void Start()
    {
        SpawnList = new List<List<MonsterAmount>>();
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(waveXml.text);

        foreach (XmlNode item in doc)
        {
            int count = 0;
            foreach (XmlNode item0 in item)
            {
                Debug.Log(count);
                SpawnList.Add(new List<MonsterAmount>());
                foreach (XmlNode item1 in item0)
                {
                    GameObject m = null;
                    foreach (var mm in MonsterPrefabs)
                    {
                        if (mm.GetComponent(item1.Name))
                        {
                            m = mm;
                            break;
                        }
                    }
                    SpawnList[count].Add(new MonsterAmount(m, int.Parse(item1.InnerText)));
                }
                count++;
            }
        }

        WaveEndCallback += () =>
        {
            Wave += 1;
            if (Wave >= SpawnList.Count)
            {
                Wave = 0;
            }
            else
            {
                StartCoroutine(SpawnMob());
            }
        };

    }

    private IEnumerator SpawnMob()
    {
        int monsterIter = 0;
        int sideIter = 0;
        waveState = 1;
        if (Wave != 0)
            Context.userInterface.waveClear.GetComponent<Animator>().SetTrigger("Play");
        yield return new WaitForSeconds(5);
        waveState = 2;
        Context.userInterface.waveStart.GetComponent<Animator>().SetTrigger("Play");
        yield return new WaitForSeconds(5);
        waveState = 0;
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            if (SpawnList[Wave][monsterIter].Amount <= 0)
            {
                if (monsterIter + 1 < SpawnList[Wave].Count)
                    monsterIter += 1;
                else
                    yield break;
            }
            else
            {
                SpawnList[Wave][monsterIter].Amount -= 1;
                GameObject o = Instantiate(SpawnList[Wave][monsterIter].Monster);
                o.transform.SetParent(transform);
                o.transform.position = new Vector3(Spawn.GetChild(sideIter).position.x, o.transform.position.y, Spawn.GetChild(sideIter).position.z);
                sideIter += 1;
                if (sideIter > 3)
                    sideIter = 0;
            }
        }
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("Money", 500);
        StartCoroutine(SpawnMob());
        StartCoroutine(Context.userInterface.SubtractWaveTime());
        Context.userInterface.Main.SetActive(false);
    }

    public void EndGame()
    {
        Application.Quit();
    }
}