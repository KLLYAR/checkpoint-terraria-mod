// using Microsoft.Xna.Framework;
// using Microsoft.Xna.Framework.Graphics;
// using System;
// using System.Collections.Generic;
// using System.IO;
// using System.Linq;
// using Terraria;
// using Terraria.DataStructures;
// using Terraria.GameInput;
// using Terraria.ID;
// using Terraria.ModLoader;
// using Terraria.ModLoader.IO;
// using static Terraria.ModLoader.ModContent;
// using System.Runtime.Serialization;
// using System.Runtime.Serialization.Formatters.Binary;
// using System.Text;
// using System.Threading.Tasks;

// namespace checkpoint
// {
//     public class IDGenerator : ModWorld
//     {
//         private List<Player> playersID;

//         public IDGenerator()
//         {
//             this.playersID = new List<Player>();
//         }

//         private IFormatter formatter = new BinaryFormatter();

//         public override TagCompound Save()
//         {
//             foreach(Player player in Main.player)
//             {
//                 bool exist = false;
//                 foreach(Player id in playersID)
//                 {   
//                     if(id == player)
//                     {
//                         exist = true;
//                     }
//                 }
//                 if(!exist)
//                 {
//                     this.playersID.Add(player);
//                     player.GetModPlayer<Checkpoint>().setPlayerID(playersID.IndexOf(player));
//                 }
//             }
            
//             Stream stream = new FileStream(@"" + Main.worldID + ".txt",FileMode.OpenOrCreate,FileAccess.Write);
//             this.formatter.Serialize(stream, this.playersID);
//             stream.Close();

//             return new TagCompound {
//             };
//         }

//         public override void Load(TagCompound tag) 
//         {
//             Stream loadStream = new FileStream(@"" + Main.worldID + ".txt",FileMode.OpenOrCreate,FileAccess.Read);
//             playersID = (List<Player>) this.formatter.Deserialize(loadStream);
//             loadStream.Close();
//         }

//         public void getPlayerID(Player player)
//         {
//             return this.playersID.IndexOf(player);
//         }
//     }
// }