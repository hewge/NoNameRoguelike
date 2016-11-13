using System;
using SadConsole;
using SadConsole.Input;
using ConsoleScreenGameHelper.Core.Entity;
using ConsoleScreenGameHelper.Core.Entity.Components;
using ConsoleScreenGameHelper.Core.Screen;
using ConsoleScreenGameHelper.GameComponent;
using NoNameRoguelike.Core.Console;
using NoNameRoguelike.Core.Systems;
using Microsoft.Xna.Framework;

namespace NoNameRoguelike.Core.Screen
{
	class DungeonScreen : BaseGameScreen
	{
        public DungeonViewConsole dungeonViewConsole;
        public CharacterStatusPanel characterStatusPanel;
        public MessageConsole messageConsole;
        public InventoryConsole inventoryConsole;

		public DungeonScreen(Game gameRef, GameStateManager manager) : base(gameRef, manager)
		{
            //TODO: Get these values dynamically.
            int width = 80;
            int height = 45;

            messageConsole = new MessageConsole((int)(width), (int)(height*0.30));
            Add(messageConsole);
            dungeonViewConsole = new DungeonViewConsole((int)(width*0.80), (int)(height*0.70), 30, 30);
            characterStatusPanel = new CharacterStatusPanel((int)(width*0.20), (int)(height*0.70));
            inventoryConsole = new InventoryConsole((int)(width*0.60), (int)(height*0.60));



            characterStatusPanel.Position = new Point((int)(width*0.80), 0);
            messageConsole.Position = new Point(0, (int)(height*0.70));
            inventoryConsole.Position = new Point((int)(width*0.30)/2, (int)(width*0.30)/2);

            Add(characterStatusPanel);
            Add(dungeonViewConsole);

            BaseEntity test_item = new BaseEntity();
            test_item.NAME = "Test Item";
            test_item.AddComponent(new Item());
            test_item.AddComponent(new SpriteAnimation('Y', Color.Red, Color.Yellow));

            inventoryConsole.Show();
            inventoryConsole.inventory = new Inventory();
            inventoryConsole.inventory.AddItem(test_item);

            inventoryConsole.inventory.AddItem(test_item);
            inventoryConsole.inventory.AddItem(test_item);
            inventoryConsole.inventory.AddItem(test_item);
            inventoryConsole.inventory.AddItem(test_item);
            inventoryConsole.inventory.AddItem(test_item);
            inventoryConsole.inventory.AddItem(test_item);
            inventoryConsole.inventory.AddItem(test_item);
            //TODO: Find a way to init those dynamically.

            dungeonViewConsole.Player.GetComponent<Actor>(ComponentType.Actor).Stats.StatsChanged += characterStatusPanel.StatusChanged;
            dungeonViewConsole.mapLevel.EntityContainer[0].GetComponent<Actor>(ComponentType.Actor).Stats.StatsChanged += characterStatusPanel.actor_StatusChanged;
            characterStatusPanel.CharacterName = dungeonViewConsole.Player.NAME;

		}

        public override bool ProcessKeyboard(KeyboardInfo info)
        {
            if(info.KeysPressed.Contains(AsciiKey.Get(Microsoft.Xna.Framework.Input.Keys.I)))
            {
                if(!inventoryConsole.IsVisible)
                {
                    inventoryConsole.Show();
                }
                else
                {
                    inventoryConsole.Hide();
                }
            }
            return dungeonViewConsole.ProcessKeyboard(info);
        }
        public override bool ProcessMouse(MouseInfo info)
        {
            if(!inventoryConsole.ProcessMouse(info))
            {
                return base.ProcessMouse(info);
            }
            return true;
        }
        public override void Update()
        {
            base.Update();
            inventoryConsole.Update();
        }
        public override void Render()
        {
            base.Render();
            inventoryConsole.Render();
        }
	}
}

