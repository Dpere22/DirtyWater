using UnityEngine;

namespace Interactables
{
    public class PlasticBag : Trash
    {
        protected override string SetTrashId()
        {
            return "plasticBag";
        }

        protected override bool CheckCanCollect()
        {
            return false;
        }
    }
}
