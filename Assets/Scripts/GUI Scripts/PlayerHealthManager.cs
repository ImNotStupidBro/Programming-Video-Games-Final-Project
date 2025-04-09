using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    public Player currPlayer;
    public PlayerHealthUnit playerHealthUnitPrefab;
    private List<PlayerHealthUnit> playerHPBar = new List<PlayerHealthUnit>();
    private bool isInitialized = false;

    private GameObject canvas;
    private GameObject game;
    void Awake()
    {
        game = GameObject.Find("Game");
        canvas = GameObject.Find("UICanvas");
    }

    void Update()
    {
        if(!isInitialized && game.GetComponent<Game>().isInitialized)
        {
            for (int i = 0; i < currPlayer.GetHealthPoints(); i++){
                float HPUnitX = this.transform.position.x + (i * 101);
                PlayerHealthUnit hpUnit = Instantiate(playerHealthUnitPrefab, new Vector3(HPUnitX, this.transform.position.y, 0.0f), Quaternion.identity);
                hpUnit.Init(currPlayer, i);
                hpUnit.transform.SetParent(canvas.transform, false);
                playerHPBar.Add(hpUnit);
            }
            isInitialized = true;
        }

        if(isInitialized)
        {
            foreach (PlayerHealthUnit PHU in playerHPBar)
            {
                if(currPlayer.GetHealthPoints() > PHU.index){
                    PHU.gameObject.SetActive(true);
                } else {
                    PHU.gameObject.SetActive(false);
                }
            }
        }
    }
}
