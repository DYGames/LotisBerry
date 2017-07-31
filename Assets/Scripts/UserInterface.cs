using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UserInterface : MonoBehaviour
{
    [SerializeField]
    private TowerContainer tower;

    [Header("UI Objects")]
    [SerializeField]
    public Text MoneyText;

    [SerializeField]
    private List<RectTransform> MenuTypes;

    [SerializeField]
    private GameObject Menu;
    [SerializeField]
    private GameObject Gun;

    [SerializeField]
    private Text towerNum;
    [SerializeField]
    private Text scoretext;
    [SerializeField]
    private Text waveTimetext;
    [SerializeField]
    private Text wavetext;
    [SerializeField]
    private Text gunAmounttext;
    [SerializeField]
    private GameObject reload;
    [SerializeField]
    public GameObject waveStart;
    [SerializeField]
    public GameObject waveClear;
    [SerializeField]
    private Text hptext;
    [SerializeField]
    public Button continueButton;
    [SerializeField]
    public GameObject Main;

    [SerializeField]
    public Image red;
    [SerializeField]
    public Text respawn;
    [SerializeField]
    public GameObject defeat;

    private int Score;
    private System.DateTime waveTime;

    private PlayerAttack playerAttack;

    private void Start()
    {
        Context.userInterface = this;
        waveTime = new System.DateTime(2017, 7, 27, 0, 5, 0);
    }

    private void Update()
    {
        if (playerAttack == null)
            playerAttack = Context.Player.GetComponent<PlayerAttack>();
        else
            gunAmounttext.text = playerAttack.magAmount.ToString();

        towerNum.text = Context.tileInput.towerNum.ToString();
        scoretext.text = Score.ToString();
        waveTimetext.text = waveTime.ToString("mm : ss");
        if (WaveMng.isEndlessMode)
            wavetext.text = "Endless Wave";
        else
            wavetext.text = (WaveMng.Wave + 1).ToString();
        reload.SetActive(playerAttack.isReload);
        hptext.text = playerAttack.HP.ToString();
    }

    public IEnumerator SubtractWaveTime()
    {
        yield return new WaitForSeconds(1);
        waveTime = waveTime.Subtract(new System.TimeSpan(0, 0, 1));
        if (waveTime.Minute == 0 && waveTime.Second == 0)
        {
            //time over
        }
        StartCoroutine(SubtractWaveTime());
    }

    public void BuyTower(int tn)
    {
        if (Context.tileInput.SelectedTowerNum == 0 && tower.Towers[tn - 1].price <= Context.gameData.Money)
        {
            Context.gameData.Money -= tower.Towers[tn - 1].price;
            Context.tileInput.SelectedTowerNum = tn;
        }
    }

    public void SelectMenuType(RectTransform MenuType)
    {
        for (int i = 0; i < MenuTypes.Count; i++)
            MenuTypes[i].gameObject.SetActive(MenuTypes[i].Equals(MenuType));
    }

    public void setProjection(bool toOrt)
    {
        if (toOrt)
        {
            Menu.SetActive(true);
            Gun.SetActive(false);
        }
        else
        {
            Menu.SetActive(false);
            Gun.SetActive(true);
        }
    }

    public IEnumerator Respawn(int i)
    {
        red.gameObject.SetActive(true);
        respawn.gameObject.SetActive(true);
        respawn.text = string.Format("Respawn In {0}...", i);
        for (int s = 0; s < i; s++)
        {
            yield return new WaitForSeconds(1);
            respawn.text = string.Format("Respawn In {0}...", i - s);
        }
        yield return new WaitForSeconds(1);
        red.gameObject.SetActive(false);
        respawn.gameObject.SetActive(false);

        Context.Player.GetComponent<PlayerAttack>().Respawn();
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

}
