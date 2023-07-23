using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace WingTimeUI
{
    public class UIConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Header("Placement")]
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

        [Header("Visibility")]
        [Label("UI Visibility Behavior")]
        [Tooltip("Determines the UI visibility behavior\n" +
            "AlwaysDraw: Always show the UI when applicable\n" +
            "DrawDuringBoss: Only show UI during boss fights\n" +
            "NeverDraw: Never show the UI")]
        [DefaultValue(0)]
        [Range(0, 2)]
        public UIDrawRule visible;

        [Label("Draw while Soaring Insignia is equipped")]
        [Tooltip("In vanilla, the Soaring Insignia gives infinite whing flight, so it's pretty redundant to have this UI show up\n" +
            "Don't use this if a mod you have changes the Insignia to not have infinite flight time")]
        [DefaultValue(true)]
        public bool DrawInsignia;

        public override void OnChanged()
        {
            MainUI.ApplyConfigChanges(horizontalPercentage, verticalPercentage, (float)horizontalPixelOffset, (float)verticalPixelOffset, visible, DrawInsignia);
        }
    }

    public enum UIDrawRule
    {
        AlwaysDraw,
        DrawDuringBoss,
        NeverDraw,
    }
}
