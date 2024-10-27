using UnityEngine;

public class GlobalTyperVariables : MonoBehaviour
{
    [Tooltip("central stats that depend on player and environment")]
    [Header("Player Stats")]
    public float clarity;
    public float energy;
    public float comfort;

    [Header("Typing Mechanic")]
    [Tooltip("key info about where in script player is at")]
    public bool npcTalking;
    public int ScriptIndex;
    
    [Tooltip("classes with importance")]
    [Header("Key GameObjects")]
    public GameObject Typer;
    public GameObject NPC;
    public GameObject Player;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Debug.Log("Report all stats: ");
            Debug.Log("clarity: " + clarity);
            Debug.Log("energy: " + energy);
            Debug.Log("comfort: " + comfort);
            Debug.Log("npcTalking: " + npcTalking);
            Debug.Log("Script index: " + ScriptIndex);
        }
    }

}
