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
<<<<<<< Updated upstream
        
=======

>>>>>>> Stashed changes
        [Range(0f, 1f)]
        [Increment(0.01f)]
        [DefaultValue(0.5f)]
        public float horizontalPercentage;

<<<<<<< Updated upstream
        
=======
>>>>>>> Stashed changes
        [Range(0f, 1f)]
        [Increment(0.01f)]
        [DefaultValue(0.7f)]
        public float verticalPercentage;
<<<<<<< Updated upstream
=======

        [Range(-10000, 10000)]
        [Increment(1)]
        [DefaultValue(0)]
        public int horizontalPixelOffset;

        [Range(-10000, 10000)]
        [Increment(1)]
        [DefaultValue(0)]
        public int verticalPixelOffset;

        [DefaultValue(true)]
        public bool visible;

>>>>>>> Stashed changes
        public override void OnChanged()
        {
            MainUI.ApplyConfigChanges(horizontalPercentage, verticalPercentage);
        }
    }
}
