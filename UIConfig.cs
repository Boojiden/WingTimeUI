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

        [Label("Horizontal Screen Percentage")]
        [Tooltip("0% is to the left, 100% is to the right")]
        [Range(0f, 1f)]
        [Increment(0.01f)]
        [DefaultValue(0.5f)]
        public float horizontalPercentage;

        [Label("Vertical Screen Percentage")]
        [Tooltip("0% is at the top, 100% is at the bottom")]
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
