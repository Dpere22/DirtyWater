namespace Interactables
{
    public class PlasticBottle : Trash
    {
        protected override string SetTrashId()
        {
            return "plasticBottle";
        }

        protected override bool CheckCanCollect()
        {
            return false;
        }
    }
}
