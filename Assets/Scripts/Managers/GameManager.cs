using PlayerCodes;
using Utility;

namespace Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public PlayerController playerControl;

        private void Start()
        {
            // playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }
    }
}