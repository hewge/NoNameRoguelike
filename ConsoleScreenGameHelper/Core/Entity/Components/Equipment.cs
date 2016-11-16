using System;
using ConsoleScreenGameHelper.Core.DataContainer;
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
                        GetComponent<Statistic>(ComponentType.Stats).AddModifiers(item.GetComponent<Statistic>(ComponentType.Stats));
                        return true;
                    }
                    else if(Body == item)
                    {
                        GetComponent<Statistic>(ComponentType.Stats).RemoveModifiers(item.GetComponent<Statistic>(ComponentType.Stats));
                        Body = null;
                    }
                    break;
                case EquipmentSlot.Feet:
                    if(Feet == null)
                    {
                        Feet = item;
                        GetComponent<Statistic>(ComponentType.Stats).AddModifiers(item.GetComponent<Statistic>(ComponentType.Stats));
                        return true;
                    }
                     else if(Feet == item)
                    {
                        GetComponent<Statistic>(ComponentType.Stats).RemoveModifiers(item.GetComponent<Statistic>(ComponentType.Stats));
                        Feet = null;
                    }
                   break;
                  case EquipmentSlot.Hand:
                    if(Hand == null)
                    {
                        Hand = item;
                        GetComponent<Statistic>(ComponentType.Stats).AddModifiers(item.GetComponent<Statistic>(ComponentType.Stats));
                        return true;
                    }
                    else if(Hand == item)
                    {
                        GetComponent<Statistic>(ComponentType.Stats).RemoveModifiers(item.GetComponent<Statistic>(ComponentType.Stats));
                        Hand = null;
                    }
                    break;
               case EquipmentSlot.Head:
                    if(Head == null)
                    {
                        Head = item;
                        GetComponent<Statistic>(ComponentType.Stats).AddModifiers(item.GetComponent<Statistic>(ComponentType.Stats));
                        return true;
                    }
                    else if(Head == item)
                    {
                       GetComponent<Statistic>(ComponentType.Stats).RemoveModifiers(item.GetComponent<Statistic>(ComponentType.Stats));
                       Head = null;
                    }
                    break;
               case EquipmentSlot.Legs:
                    if(Legs == null)
                    {
                        Legs = item;
                        GetComponent<Statistic>(ComponentType.Stats).AddModifiers(item.GetComponent<Statistic>(ComponentType.Stats));
                        return true;
                    }
                    else if(Legs == item)
                    {
                        GetComponent<Statistic>(ComponentType.Stats).RemoveModifiers(item.GetComponent<Statistic>(ComponentType.Stats));
                        Legs = null;
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

