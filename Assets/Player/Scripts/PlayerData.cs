using Common.CommonScripts.States;
using UnityEngine;

namespace Player.Scripts
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Player/PlayerData")]
    public class PlayerData : ScriptableObject
    {
       public float jogSpeed = 5f;
       public float runSpeed = 8f;

       public float health = 100f;
       public float maxHealth = 100f;

       public float smoothSpeedTime = 10f;

       public State state;
    }
}
