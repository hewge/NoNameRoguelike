using System;
using ConsoleScreenGameHelper.Enum;
using ConsoleScreenGameHelper.Interface;

namespace ConsoleScreenGameHelper.Core.Entity.Components
{
	public class Equipment : Component
	{
        //skall detta verkligen vara såhär?
        public Item Head { get; set; }
        public Item Body { get; set; }
        public Item Legs { get; set; }
        public Item Feet { get; set; }
        public Item Hand { get; set; }


        public override ComponentType ComponentType { get { return ComponentType.Equipment; } }

		public Equipment()
		{
		}

        public bool Contains(Item item)
        {
            if(Body == item || Feet == item || Hand == item || Head == item || Legs == item)
            {
                return true;
            }
            return false;
        }

        public bool TryEquip(Item item)
        {
            switch(item.GetComponent<Statistic>(ComponentType.Stats).EquipmentSlot)
            {
                case EquipmentSlot.Body:
                    if(Body == null)
                    {
                        Body = item;
                        return true;
                    }
                    break;
                case EquipmentSlot.Feet:
                    if(Feet == null)
                    {
                        Feet = item;
                        return true;
                    }
                    break;
                  case EquipmentSlot.Hand:
                    if(Hand == null)
                    {
                        Hand = item;
                        return true;
                    }
                    break;
               case EquipmentSlot.Head:
                    if(Head == null)
                    {
                        Head = item;
                        return true;
                    }
                    break;
               case EquipmentSlot.Legs:
                    if(Legs == null)
                    {
                        Legs = item;
                        return true;
                    }
                    break;
            }
            return false;
        }

        public override void FireEvent(object sender, EventArgs e)
        {
        }

	}
}

