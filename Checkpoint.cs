using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using static Terraria.ModLoader.ModContent;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace checkpoint
{
    [Serializable]
    public class Checkpoint : ModPlayer
    {
        private bool activated, firstTime;
        // private int playerID;
        private Dictionary<string, Vector2> positions;

        public Checkpoint()
        {
            this.activated = true; 
            this.firstTime = true;
            // this.playerID = 0;
            this.positions = new Dictionary<string, Vector2>();
        }

        public override void OnEnterWorld(Player player)
        {
            if(this.activated)
            {
                if(this.firstTime)
                {
                    positions[Main.worldID + "" + Main.player[Main.myPlayer].name] = Main.player[Main.myPlayer].position;
                    this.firstTime = false;
                }
                else
                {
                    Main.player[Main.myPlayer].position = this.positions[Main.worldID + "" + Main.player[Main.myPlayer].name];
                }
                Main.player[Main.myPlayer].noFallDmg = true;
                Main.NewText("Welcome back, " +  Main.player[Main.myPlayer].name + "!");
            }
        }

        private IFormatter formatter = new BinaryFormatter();

        public override TagCompound Save()
        {
            positions[Main.worldID + "" + Main.player[Main.myPlayer].name] = Main.player[Main.myPlayer].position; 
            
            Stream stream = new FileStream(@"checkpoint.txt",FileMode.OpenOrCreate,FileAccess.Write);
            this.formatter.Serialize(stream, this.positions);
            stream.Close();

            return new TagCompound {
                { "firstTime", this.firstTime }
            };
        }

        public override void Load(TagCompound tag) 
        {
            Stream loadStream = new FileStream(@"checkpoint.txt",FileMode.OpenOrCreate,FileAccess.Read);
            this.positions = (Dictionary<string, Vector2>) this.formatter.Deserialize(loadStream);
            loadStream.Close();
            this.firstTime = tag.GetBool("firstTime");
        }

        // public void setPlayerID(int id)
        // {
        //     this.playerID = id;
        // }
    }
}