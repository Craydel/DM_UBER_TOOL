using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace DM_Uber_Tool
{
  public partial class DmModuleNPCGenerator : AbstractModule, IDmModule
  {
    List<DmNPC> npcList = new List<DmNPC>();

    public DmModuleNPCGenerator()
      : base()
    {
      InitializeComponent();

      moduleDisplayName = "NPC Generator Module";
      moduleType        = DmModuleType.Npc_Generator_Module;
    }

    #region IDmModule Members

    public void SaveState( XmlWriter writer )
    {
      return;
    }

    public void LoadState( XmlReader reader )
    {
      return;
    }

    public string GetModuleName()
    {
      return moduleDisplayName;
    }

    public DmModuleType GetModuleType()
    {
      return moduleType;
    }

    #endregion

    /// <summary>
    /// Generate a new, random NPC.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btnGenerateNPC_Click( object sender, EventArgs e )
    {
      // conjure up a new NPC
      npcList.Add( DmNpcFactory.GenerateNPC() );

      // add the newest NPC in the list to the TreeView control
      treeView1.Nodes.Add( npcList[npcList.Count-1].GetNPCNode() );
    }

    /// <summary>
    /// TreeView control selected node change event handler
    /// When an NPC is selected, the full description is reflected in the textbox.
    /// Sub nodes from theat NPC do not change the display text.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void NodeSelectionChanged_Handler( object sender, TreeViewEventArgs e )
    {
      // nothing selected?  do nothing.
      if( treeView1.SelectedNode == null )
        return;

      if( treeView1.SelectedNode.Parent == null )
      {
        // top-level node selected - use this desc.
        int idx = treeView1.SelectedNode.Index;
        txtDescription.Text = npcList[idx].GetDescription();
      }
    }



    #region Debug functions and utilities
    /// <summary>
    /// DEBUG - Creates a random name
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void button1_Click( object sender, EventArgs e )
    {
      txtDescription.Text = DmNameGenerator.GenerateName( (NPCRace)rand.Next( Enum.GetValues(typeof(NPCRace)).Length ), 
                                                          (NPCGender)rand.Next( Enum.GetValues( typeof(NPCGender)).Length) 
                                                        ) + "\r\n";
    }

    /// <summary>
    /// DEBUG
    /// Generates generic code for the string descriptions for the NPC characteristic enums
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void GenerateNpcEnumDescriptions_Click( object sender, EventArgs e )
    {
      string tmp = "";

      tmp += "// Age\r\n";
      for( int i=0; i<Enum.GetValues( typeof( NPCAge ) ).Length; i++ )
        tmp += string.Format( "{0,-60}{1}", "descNPCAge[(int)NPCAge." + ((NPCAge)i).ToString() + "]", " = \"" + ((NPCAge)i).ToString().ToLower() + "\";\r\n" );
      tmp += "\r\n";

      tmp += "// Body Build\r\n";
      for( int i=0; i<Enum.GetValues( typeof( NPCBodyBuild ) ).Length; i++ )
        tmp += string.Format( "{0,-60}{1}", "descNPCBodyBuild[(int)NPCBodyBuild." + ((NPCBodyBuild)i).ToString() + "]", " = \"" + ((NPCBodyBuild)i).ToString().ToLower() + "\";\r\n" );
      tmp += "\r\n";

      tmp += "// Body Size\r\n";
      for( int i=0; i<Enum.GetValues( typeof( NPCBodySize ) ).Length; i++ )
        tmp += string.Format( "{0,-60}{1}", "descNPCBodySize[(int)NPCBodySize." + ((NPCBodySize)i).ToString() + "]", " = \"" + ((NPCBodySize)i).ToString().ToLower() + "\";\r\n" );
      tmp += "\r\n";

      tmp += "// Eye Color\r\n";
      for( int i=0; i<Enum.GetValues( typeof( NPCEyeColor ) ).Length; i++ )
        tmp += string.Format( "{0,-60}{1}", "descNPCEyeColor[(int)NPCEyeColor." + ((NPCEyeColor)i).ToString() + "]", " = \"" + ((NPCEyeColor)i).ToString().ToLower() + "\";\r\n" );
      tmp += "\r\n";

      tmp += "// Facial Hair\r\n";
      for( int i=0; i<Enum.GetValues( typeof( NPCFacialHair ) ).Length; i++ )
        tmp += string.Format( "{0,-60}{1}", "descNPCFacialHair[(int)NPCFacialHair." + ((NPCFacialHair)i).ToString() + "]", " = \"" + ((NPCFacialHair)i).ToString().ToLower() + "\";\r\n" );
      tmp += "\r\n";

      tmp += "// Feats\r\n";
      for( int i=0; i<Enum.GetValues( typeof( NPCFeats ) ).Length; i++ )
        tmp += string.Format( "{0,-60}{1}", "descNPCFeats[(int)NPCFeats." + ((NPCFeats)i).ToString() + "]", " = \"" + ((NPCFeats)i).ToString().ToLower() + "\";\r\n" );
      tmp += "\r\n";

      tmp += "// Features\r\n";
      for( int i=0; i<Enum.GetValues( typeof( NPCFeatures ) ).Length; i++ )
        tmp += string.Format( "{0,-60}{1}", "descNPCFeatures[(int)NPCFeatures." + ((NPCFeatures)i).ToString() + "]", " = \"" + ((NPCFeatures)i).ToString().ToLower() + "\";\r\n" );
      tmp += "\r\n";

      tmp += "// Gender\r\n";
      for( int i=0; i<Enum.GetValues( typeof( NPCGender ) ).Length; i++ )
        tmp += string.Format( "{0,-60}{1}", "descNPCGender[(int)NPCGender." + ((NPCGender)i).ToString() + "]", " = \"" + ((NPCGender)i).ToString().ToLower() + "\";\r\n" );
      tmp += "\r\n";

      tmp += "// Hair Color\r\n";
      for( int i=0; i<Enum.GetValues( typeof( NPCHairColor ) ).Length; i++ )
        tmp += string.Format( "{0,-60}{1}", "descNPCHairColor[(int)NPCHairColor." + ((NPCHairColor)i).ToString() + "]", " = \"" + ((NPCHairColor)i).ToString().ToLower() + "\";\r\n" );
      tmp += "\r\n";

      tmp += "// Hair Length\r\n";
      for( int i=0; i<Enum.GetValues( typeof( NPCHairLength ) ).Length; i++ )
        tmp += string.Format( "{0,-60}{1}", "descNPCHairLength[(int)NPCHairLength." + ((NPCHairLength)i).ToString() + "]", " = \"" + ((NPCHairLength)i).ToString().ToLower() + "\";\r\n" );
      tmp += "\r\n";

      tmp += "// Hair Style\r\n";
      for( int i=0; i<Enum.GetValues( typeof( NPCHairStyle ) ).Length; i++ )
        tmp += string.Format( "{0,-60}{1}", "descNPCHairStyle[(int)NPCHairStyle." + ((NPCHairStyle)i).ToString() + "]", " = \"" + ((NPCHairStyle)i).ToString().ToLower() + "\";\r\n" );
      tmp += "\r\n";

      tmp += "// Mental State\r\n";
      for( int i=0; i<Enum.GetValues( typeof( NPCMentalState ) ).Length; i++ )
        tmp += string.Format( "{0,-60}{1}", "descNPCMentalState[(int)NPCMentalState." + ((NPCMentalState)i).ToString() + "]", " = \"" + ((NPCMentalState)i).ToString().ToLower() + "\";\r\n" );
      tmp += "\r\n";

      tmp += "// Mood\r\n";
      for( int i=0; i<Enum.GetValues( typeof( NPCMood ) ).Length; i++ )
        tmp += string.Format( "{0,-60}{1}", "descNPCMood[(int)NPCMood." + ((NPCMood)i).ToString() + "]", " = \"" + ((NPCMood)i).ToString().ToLower() + "\";\r\n" );
      tmp += "\r\n";

      tmp += "// Personality\r\n";
      for( int i=0; i<Enum.GetValues( typeof( NPCPersonaliy ) ).Length; i++ )
        tmp += string.Format( "{0,-60}{1}", "descNPCPersonaliy[(int)NPCPersonaliy." + ((NPCPersonaliy)i).ToString() + "]", " = \"" + ((NPCPersonaliy)i).ToString().ToLower() + "\";\r\n" );
      tmp += "\r\n";

      tmp += "// Race\r\n";
      for( int i=0; i<Enum.GetValues( typeof( NPCRace ) ).Length; i++ )
        tmp += string.Format( "{0,-60}{1}", "descNPCRace[(int)NPCRace." + ((NPCRace)i).ToString() + "]", " = \"" + ((NPCRace)i).ToString().ToLower() + "\";\r\n" );
      tmp += "\r\n";

      tmp += "// Type\r\n";
      for( int i=0; i<Enum.GetValues( typeof( NPCType ) ).Length; i++ )
        tmp += string.Format( "{0,-60}{1}", "descNPCType[(int)NPCType." + ((NPCType)i).ToString() + "]", " = \"" + ((NPCType)i).ToString().ToLower() + "\";\r\n" );
      tmp += "\r\n";

      tmp += "// Class\r\n";
      for( int i=0; i<Enum.GetValues( typeof( NPCTypeAdventurer ) ).Length; i++ )
        tmp += string.Format( "{0,-60}{1}", "descNPCTypeAdventurer[(int)NPCTypeAdventurer." + ((NPCTypeAdventurer)i).ToString() + "]", " = \"" + ((NPCTypeAdventurer)i).ToString().ToLower() + "\";\r\n" );
      tmp += "\r\n";

      tmp += "// Profession\r\n";
      for( int i=0; i<Enum.GetValues( typeof( NPCTypeProfession ) ).Length; i++ )
        tmp += string.Format( "{0,-60}{1}", "descNPCTypeNonAdventurer[(int)NPCTypeNonAdventurer." + ((NPCTypeProfession)i).ToString() + "]", " = \"" + ((NPCTypeProfession)i).ToString().ToLower() + "\";\r\n" );
      tmp += "\r\n";

      txtDescription.Text = tmp;
    }
    #endregion

  }
}
