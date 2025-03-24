using UnityEngine;

namespace Upgrades
{
    public class SpeedUpgrade : Upgrade
    {
        public override void DoUpgrade()
        {
            Debug.Log("Upgrading Speed");
        }

        public override void EnableUpgrade()
        {
            throw new System.NotImplementedException();
        }
    }
}
