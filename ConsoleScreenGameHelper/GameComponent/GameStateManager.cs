using System;
using System.Collections;
using Microsoft.Xna.Framework;
using ConsoleScreenGameHelper.Interface;
using ConsoleScreenGameHelper.Core.Console;

namespace ConsoleScreenGameHelper.GameComponent
{
	public class GameStateManager : Microsoft.Xna.Framework.GameComponent
	{
        public event System.EventHandler OnStateChange;
        private ILog logger = null;
        Stack gameStates = new Stack();
        public GameState CurrentState
        {
            get {return (GameState)gameStates.Peek();}
        }
		public GameStateManager(Game gameRef) : base(gameRef)
		{
		}
        public GameStateManager(Game gameRef, ILog log) : this(gameRef)
        {
            logger = log;
        }
        public void PopState()
        {

            if(gameStates.Count > 0)
            {
                if(logger != null)
                {
                    logger.Debug(string.Format("Popping state {0}.", gameStates.Peek().ToString()));
                }

                RemoveState();
                if(OnStateChange != null)
                {
                    OnStateChange(this, null);
                }
            }
        }
        private void RemoveState()
        {
             gameStates.Pop();
        }
        public void PushState(GameState newState)
        {
            if(logger != null)
            {
                logger.Debug(string.Format("Pushing state {0}.", newState.ToString()));
            }
            AddState(newState);
            if(OnStateChange != null)
            {
                 OnStateChange(this, null);
            }
        }
        private void AddState(GameState newState)
        {
            gameStates.Push(newState);
            OnStateChange += newState.StateChange;
        }
        public void ChangeState(GameState newState)
        {
            if(logger != null)
            {
                logger.Debug(string.Format("Changing state to {0}.", newState.ToString()));
            }
             while(gameStates.Count > 0)
             {
                 RemoveState();
             }
             AddState(newState);
             if(OnStateChange != null)
             {
                  OnStateChange(this, null);
             }
        }
	}
}

