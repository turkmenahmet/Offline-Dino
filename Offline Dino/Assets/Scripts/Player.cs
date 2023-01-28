using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController character;

    private void Awake() 
    {        
        character = GetComponent<CharacterController>();        
        
    }
}
