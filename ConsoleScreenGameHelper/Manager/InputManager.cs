using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ConsoleScreenGameHelper.Enum;
using ConsoleScreenGameHelper.EventHandler;

namespace ConsoleScreenGameHelper.Manager
{
	public class InputManager
	{
        private static event EventHandler<NewInputEventArgs> _FireNewInput;
        private Dictionary<Keys, Input> ButtonConfiguration = new Dictionary<Keys, Input>();
        public static event EventHandler<NewInputEventArgs> FireNewInput
        {
            add { _FireNewInput += value; }
            remove { _FireNewInput -= value; }
        }

        public InputManager()
        {

        }

        public void AddButton(Keys key, Input input)
        {
            ButtonConfiguration.Add(key, input);
        }

        private Input GetButton(Keys key)
        {
            return ButtonConfiguration[key];
        }

        public bool ProcessKeyboard(SadConsole.Input.KeyboardInfo info)
        {
            foreach (var k in ButtonConfiguration.Keys)
            {
                if (info.KeysPressed.Contains(SadConsole.Input.AsciiKey.Get(k)))
                {
                    Fire(GetButton(k));
                    return true;
                }
            }
            return false;
        }

        private void Fire(Input input)
        {
            if (_FireNewInput != null)
            {
                _FireNewInput(this, new NewInputEventArgs(input));
            }
        }

	}
}

