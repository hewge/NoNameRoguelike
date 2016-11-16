using System;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Xna.Framework;
using ConsoleScreenGameHelper.Core.Entity;
using ConsoleScreenGameHelper.Core.Entity.Components;

namespace ConsoleScreenGameHelper.Factory
{
	public static class GameObjectFactory
	{
        public static void LoadBluePrints()
        {
            /*
            using(StreamReader r = new StreamReader("World/Entity/Blueprint/test.json"))
            {
                string json = r.ReadToEnd();
                System.Console.WriteLine(json);
                var obj = JsonConvert.DeserializeObject<BaseEntity>(json, new JsonSerializerSettings()
                        {
                            TypeNameHandling = TypeNameHandling.Objects
                        });
                System.Console.WriteLine(string.Format("obj.ToString():{0}", obj.ToString()));
            }
            */
            using(StreamWriter w = new StreamWriter("World/Entity/Blueprint/test.json"))
            {
                var testMob = new BaseEntity();
                testMob.AddComponent(new SpriteAnimation('M', Color.Blue, Color.Black));
                testMob.AddComponent(new Actor());
                string s = JsonConvert.SerializeObject(testMob, Formatting.Indented, new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.Objects,
                            TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
                        });
                w.Write(s);

            }

        }
        static BaseEntity CreateEntity()
        {
            return new BaseEntity();
        }
	}
}

