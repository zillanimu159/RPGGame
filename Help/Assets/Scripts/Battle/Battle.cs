using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Character;

public enum State { START, PLAYER, ENEMY, BATTLE, WIN, LOSS}
public enum PlayerState { MENU, SELECT_MAGIC, SELECT_ENEMY, SELECT_ITEM, SLEEP }

public class BattleLoop : MonoBehaviour
{   
    //Variables for Start
    [SerializeField]
    public GameObject characterPrefab;
    [SerializeField]
    public Sprite enemyImage;
    private int playerNum;
    //Variables for Player
    [SerializeField]
    GameObject menu;
    InfoForUI info;
    //General Variables
    public State CurrentState { get; set; }
    public PlayerState CurrentPlayerState { get; set; }
    public List<GameObject> Party { get; set; }
    private List<GameObject> Enemies;

    public void Start()
    {
        CurrentState = State.START;
        info = menu.GetComponent<InfoForUI>();
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(1));
    }
    public void Update()
    {
        switch (CurrentState) {
            case State.START:
                Party = new List<GameObject>();
                Enemies = new List<GameObject>();
                for (int i = 0; i < 4; i++) {
                    GameObject tempChar = Instantiate(characterPrefab, transform, true);
                    tempChar.transform.Translate(0, -2 * i, 0);
                    Party.Add(tempChar);
                }
                for (int i = 0; i < 4; i++)
                {
                    GameObject tempChar = Instantiate(characterPrefab, transform, true);
                    tempChar.GetComponent<SpriteRenderer>().sprite = enemyImage;
                    tempChar.transform.Translate(9, -2 * i, 0);
                    Enemies.Add(tempChar);
                }
                playerNum = 0;
                info.CurrentMenuNames = Party[playerNum].GetComponent<PartyCharUIManager>().GetPlayerCore().actionList;
                info.UpdateButtons(Party[playerNum].GetComponent<PartyCharUIManager>().GetPlayerCore().actionList, 0);
                CurrentState = State.PLAYER;
                break;
            case State.PLAYER:
                if (playerNum < Party.Count)
                {
                    playerMenu(playerNum);
                }
                else
                {
                    CurrentPlayerState = PlayerState.SLEEP;
                    CurrentState = State.ENEMY;
                }
                if (Input.GetButtonDown("Cancel")) {
                    CurrentState = State.WIN;
                }
                break;
            case State.ENEMY:
                //Enemy damage and selected player returned, deal damage, check for player death
                foreach (GameObject player in Party) {
                    //check health = 0;
                }
                //if players all health 0, state = loss
                CurrentState = State.WIN;
                break;
            case State.WIN:
                foreach (GameObject someObject in SceneManager.GetSceneAt(0).GetRootGameObjects())
                {
                    someObject.SetActive(true);
                }
                SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(1));
                break;
            case State.LOSS:
                break;
        }
    }

    public bool TransitionMenus(List<string> changedMenuNames, PlayerState selectedState) {
        info.CurrentMenuNames = changedMenuNames;
        info.UpdateButtons(changedMenuNames, 0);
        info.IndexSelected = 0;
        info.ButtonSelected = 0;
        CurrentPlayerState = selectedState;
        info.isFinishedChoosing = false;
        return true;
    }

    /// <summary>
    /// allows 
    /// </summary>
    /// <returns></returns>
    public int playerMenu(int partyNum) {
        switch (CurrentPlayerState)
        {
            case PlayerState.MENU:
                if (info.isFinishedChoosing)
                {
                    switch (info.IndexSelected)
                    {
                        case 0:
                            Debug.Log(Party[partyNum].GetComponent<PartyCharUIManager>().GetPlayerCore().BasicAttack.Damage);
                            CurrentPlayerState = PlayerState.SELECT_ENEMY;
                            break;
                        case 1:
                            List<string> magicNames = new List<string>();
                            //load magic list
                            for (int i = 0; i < Party[0].GetComponent<PartyCharUIManager>().GetPlayerCore().magicList.Count; i++)
                            {
                                magicNames.Add(Party[0].GetComponent<PartyCharUIManager>().GetPlayerCore().magicList[i].Name);
                            }
                            TransitionMenus(magicNames, PlayerState.SELECT_MAGIC);
                            break;
                        case 2:


                            break;
                        case 3:

                            break;
                        case 4:

                            break;
                        default:

                            break;
                    }
                }
                break;
            case PlayerState.SELECT_MAGIC:
                if (info.isFinishedChoosing)
                {
                    Debug.Log(Party[partyNum].GetComponent<PartyCharUIManager>().GetPlayerCore().magicList[info.IndexSelected].Damage);
                }
                if (Input.GetButtonDown("Cancel")) {
                    TransitionMenus(Party[partyNum].GetComponent<PartyCharUIManager>().GetPlayerCore().actionList, PlayerState.MENU);
                }
                break;
            //bool select magic
            case PlayerState.SELECT_ENEMY:
                //select enemies
                //deal damage
                if (Input.GetButtonDown("Cancel"))
                {
                    TransitionMenus(Party[partyNum].GetComponent<PartyCharUIManager>().GetPlayerCore().actionList, PlayerState.MENU);
                }
                CurrentPlayerState = PlayerState.SLEEP;
                break;
            case PlayerState.SLEEP:
                break;
            default:
                break;
        }
        return 0;
    }
}
    