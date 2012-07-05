using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace DM_Uber_Tool
{
  public enum DmModuleType
  {
    DM_Guilde_Module,         // links to the PDFs and the HTML reference guide
    Web_Module,               // basic browser window, search via Google

    Player_Charater_Module,   // basic character stats, gear, feats, saves
    Npc_Generator_Module,     // Complete NPC generator, level, gear, desc, etc
    Town_Module,              // Random township generator - features, size, population
    Games_Module,             // Dice, roulette, cards
    Dungeon_Module,           // Dungeon maze generator
    Encounter_Module,         // Random encouter generator
    Items_Module,             // Mundane and magical item generator
  }

  interface IDmModule
  {
    void          SaveState( XmlWriter writer );
    void          LoadState( XmlReader reader );

    string        GetModuleName();
    DmModuleType  GetModuleType();
  }

}
