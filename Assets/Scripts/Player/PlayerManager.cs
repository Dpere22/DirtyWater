using System.Collections.Generic;

namespace Player
{
    public class PlayerManager
    {

        public bool Swimming = false; //not the best place for this but ok for one off
        //Total Materials Collected
        public int TotalPlastic;
        public int TotalWood;
        public int TotalMetal;

        //public int TotalRefinedMetal?


        public int WaterBottle;
        public int PlasticTrash;
        public int WoodenPlank;
        public int WoodenCrate;



        public int DropOffCrates = 1;
        public int MaxDropOffCrates = 1;


        //Both Lists contains GarbageID, Plastic Value, Wood Value, Metal Value
        //Trash that is dropped off at the drop zone
        public List<List<string>> GarbageDroppedOff = new();

        //Trash currently in the inventory
        public List<List<string>> GarbageInInventory = new();

        public float MaxTime;
        public int MaxWeight;

        public int CurrentWeight;
        public int WalkingSpeed;
        public int SwimmingSpeed;

        public int WeightCost = 5;
        public int SpeedCost = 20;
    

        public PlayerManager()
        {
            var playerSettings = UnityEngine.Resources.Load<PlayerSettingsSO>("PlayerSettingsSO");
            MaxTime = playerSettings.maxTime;
            MaxWeight = playerSettings.maxWeight;
            WalkingSpeed = playerSettings.walkingSpeed;
            SwimmingSpeed = playerSettings.swimmingSpeed;
        }


        public void DroppedOffTrash()
        {
            //Puts Inventory trash in drop off
            foreach (List<string> item in GarbageInInventory)
            {
                GarbageDroppedOff.Add(item);

            }

            ResetCurrentInventory();
        }

        //Recycle Trash
        public void RecycleTrash()
        {
            //Moves everything into one list
            DroppedOffTrash();


            foreach (List<string> item in GarbageDroppedOff)
            {
                TotalPlastic += int.Parse(item[1]);
                TotalWood += int.Parse(item[2]);
                TotalMetal += int.Parse(item[3]);
            }



            ResetCurrentDayTrash();
        }

        private void ResetCurrentDayTrash()
        {
            GarbageDroppedOff = new();
            GarbageInInventory = new();

        }

        private void ResetCurrentInventory()
        {
            //Resets Inventory
            GarbageInInventory = new();
            WaterBottle = 0;
            PlasticTrash = 0;
            WoodenPlank = 0;
            WoodenCrate = 0;
            CurrentWeight = 0;
        }

    
    }
}
