using System.Collections.Generic;
using System.Collections;
using Newtonsoft.Json;
using SadConsole.Input;
using Microsoft.Xna.Framework.Input;
using ConsoleScreenGameHelper.Enum;
using ConsoleScreenGameHelper.Manager;
using ConsoleScreenGameHelper.Core.Entity;
using ConsoleScreenGameHelper.Core.Entity.Components;
using Microsoft.Xna.Framework;
using ConsoleScreenGameHelper.Core.Map;

namespace NoNameRoguelike.Core.Console
{
	class DungeonViewConsole : SadConsole.Consoles.Console
	{
        BaseEntity playerEntity;
        InputManager inputManager;
        BaseEntity testMob;
        public MapLevel mapLevel;
        public BaseEntity Player { get { return playerEntity; } }

		public DungeonViewConsole(int viewWidth, int viewHeight, int mapWidth, int mapHeight) : base(mapWidth, mapHeight)
		{
            RogueSharp.MapCreation.IMapCreationStrategy<RogueSharp.Map> ms = (RogueSharp.MapCreation.IMapCreationStrategy<RogueSharp.Map>)new RogueSharp.MapCreation.RandomRoomsMapCreationStrategy<RogueSharp.Map>(mapWidth, mapHeight, 100, 13, 7);

            mapLevel = new MapLevel(mapWidth, mapHeight, ms, TextSurface);

            inputManager = new InputManager();
            inputManager.AddButton(Keys.Down, Input.South);
            inputManager.AddButton(Keys.Up, Input.North);
            inputManager.AddButton(Keys.Left, Input.West);
            inputManager.AddButton(Keys.Right, Input.East);
            inputManager.AddButton(Keys.OemPeriod, Input.None);
            inputManager.AddButton(Keys.OemComma, Input.Pickup);

            inputManager.AddButton(Keys.NumPad2, Input.South);
            inputManager.AddButton(Keys.NumPad8, Input.North);
            inputManager.AddButton(Keys.NumPad4, Input.West);
            inputManager.AddButton(Keys.NumPad6, Input.East);
            inputManager.AddButton(Keys.NumPad9, Input.NorthEast);
            inputManager.AddButton(Keys.NumPad7, Input.NorthWest);
            inputManager.AddButton(Keys.NumPad3, Input.SouthEast);
            inputManager.AddButton(Keys.NumPad1, Input.SouthWest);
            inputManager.AddButton(Keys.NumPad5, Input.None);

            TextSurface.RenderArea = new Rectangle(0, 0, viewWidth, viewHeight);
            playerEntity = new BaseEntity(GameWorld.MessageLog);
            playerEntity.NAME = "Player";
            playerEntity.AddComponent(new PlayerInput());
            playerEntity.AddComponent(new Inventory());
			playerEntity.AddComponent(new Actor(Color.Orange, Color.Black, mapLevel, 10, 10, 10, 10, '@'));
            playerEntity.AddComponent(new FOV());
            mapLevel.CameraFollow = playerEntity;

            testMob = new BaseEntity(GameWorld.MessageLog);
            testMob.AddComponent(new Actor(mapLevel));
            testMob.AddComponent(new AI());
            mapLevel.EntityContainer.Add(testMob);
            mapLevel.EntityContainer.Add(playerEntity);

            testMob.GetComponent<SpriteAnimation>(ComponentType.SpriteAnimation).Position = mapLevel.MapData.GetWalkablePosition();
            playerEntity.GetComponent<SpriteAnimation>(ComponentType.SpriteAnimation).Position = mapLevel.MapData.GetWalkablePosition();
		}

        public override bool ProcessKeyboard(KeyboardInfo info)
        {
            return inputManager.ProcessKeyboard(info);
        }

        public override void Update()
        {
            base.Update();
            mapLevel.Update();
        }

        public override void Render()
        {
            base.Render();
            mapLevel.Render();
        }
	}
}

