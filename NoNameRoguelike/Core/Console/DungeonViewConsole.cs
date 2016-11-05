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
            //FIXME: Had some success with <T> where T : IMap , now make it better. now we only need to know in the instansiating class which maptype to use.
            RogueSharp.MapCreation.IMapCreationStrategy<RogueSharp.Map> ms = (RogueSharp.MapCreation.IMapCreationStrategy<RogueSharp.Map>)new RogueSharp.MapCreation.RandomRoomsMapCreationStrategy<RogueSharp.Map>(mapWidth, mapHeight, 100, 13, 7);

            mapLevel = new MapLevel(mapWidth, mapHeight, ms, TextSurface);

            inputManager = new InputManager();
            inputManager.AddButton(Keys.Down, Input.Down);
            inputManager.AddButton(Keys.Up, Input.Up);
            inputManager.AddButton(Keys.Left, Input.Left);
            inputManager.AddButton(Keys.Right, Input.Right);
            TextSurface.RenderArea = new Rectangle(0, 0, viewWidth, viewHeight);
            playerEntity = new BaseEntity(GameWorld.MessageLog);
            playerEntity.NAME = "Player";
            playerEntity.AddComponent(new PlayerInput());
			playerEntity.AddComponent(new Actor(Color.Orange, Color.Black, mapLevel, 10, 10, 10,  '@'));
            playerEntity.AddComponent(new ViewPort(viewWidth, viewHeight, mapWidth, mapHeight));
            playerEntity.AddComponent(new FOV());
            mapLevel.CameraFollow = playerEntity;

            testMob = new BaseEntity(GameWorld.MessageLog);
            testMob.AddComponent(new Actor(mapLevel));
            mapLevel.EntityContainer.Add(testMob);
            mapLevel.EntityContainer.Add(playerEntity);
            //TODO: Maybe make an 'EntityList' Class that MoveInfo can have ersponibility for, or is updated in MoveInfo.
            MoveInfo.Entities = mapLevel.EntityContainer;

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

