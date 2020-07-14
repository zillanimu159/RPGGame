using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;
public class PartyCharUIManager : MonoBehaviour
{
    private Player playerCore;
    // add character animation core
    // Start is called before the first frame update
    void OnEnable()
    {
        playerCore = new Player("Wizard");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Player GetPlayerCore() {
        return playerCore;
    }
}
