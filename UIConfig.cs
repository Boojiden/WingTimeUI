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

        [Label("Horizontal Pixel Offset")]
        [Tooltip("Negative is right, Positive is left. Try setting to 0 if the UI isn't visible")]
        [Range(-10000, 10000)]
        [Increment(1)]
        [DefaultValue(0)]
        public int horizontalPixelOffset;

        [Label("Vertical Pixel Offset")]
        [Tooltip("Negative is up, Positive is down. Try setting to 0 if the UI isn't visible")]
        [Range(-10000, 10000)]
        [Increment(1)]
        [DefaultValue(0)]
        public int verticalPixelOffset;

        [Label("UI Visibility")]
        [Tooltip("Makes the UI visible or invisible.")]
        [DefaultValue(true)]
        public bool visible;

        public override void OnChanged()
        {
            MainUI.ApplyConfigChanges(horizontalPercentage, verticalPercentage, (float)horizontalPixelOffset, (float)verticalPixelOffset, visible);
        }
    }
}
