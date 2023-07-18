using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.Config;

namespace WingTimeUI
{
    public class UIConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;
        
        [Range(0f, 1f)]
        [Increment(0.01f)]
        [DefaultValue(0.5f)]
        public float horizontalPercentage;

        
        [Range(0f, 1f)]
        [Increment(0.01f)]
        [DefaultValue(0.7f)]
        public float verticalPercentage;
        public override void OnChanged()
        {
            MainUI.ApplyConfigChanges(horizontalPercentage, verticalPercentage);
        }
    }
}
