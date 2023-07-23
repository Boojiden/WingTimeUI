using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace WingTimeUI
{
    public class UIConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Header("Placement")]
        [Range(0f, 1f)]
        [Increment(0.01f)]
        [DefaultValue(0.5f)]
        public float horizontalPercentage;

        [Range(0f, 1f)]
        [Increment(0.01f)]
        [DefaultValue(0.7f)]
        public float verticalPercentage;

        [Range(-10000, 10000)]
        [Increment(1)]
        [DefaultValue(0)]
        public int horizontalPixelOffset;

        [Range(-10000, 10000)]
        [Increment(1)]
        [DefaultValue(0)]
        public int verticalPixelOffset;

        [Header("Visibility")]
        [DefaultValue(0)]
        [Range(0, 2)]
        public UIDrawRule visible;

        [Label("Draw while Soaring Insignia is equipped")]
        [Tooltip("In vanilla, the Soaring Insignia gives infinite whing flight, so it's pretty redundant to have this UI show up\n" +
            "Don't use this if a mod you have changes the Insignia to not have infinite flight time")]
        [DefaultValue(true)]
        public bool drawInsignia;

        public override void OnChanged()
        {
            MainUI.ApplyConfigChanges(horizontalPercentage, verticalPercentage, (float)horizontalPixelOffset, (float)verticalPixelOffset, visible, drawInsignia);
        }
    }

    public enum UIDrawRule
    {
        AlwaysDraw,
        DrawDuringBoss,
        NeverDraw,
    }
}
