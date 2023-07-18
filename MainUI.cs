using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.GameContent;
using Terraria.GameContent.UI.ResourceSets;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.Localization;
using Terraria.DataStructures;

namespace WingTimeUI
{
    public class MainUI : UIState
    {

        // For this bar we'll be using a frame texture and then a gradient inside bar, as it's one of the more simpler approaches while still looking decent.
        // Once this is all set up make sure to go and do the required stuff for most UI's in the ModSystem class.
        private UIText text;
        private static UIElement area;
        private UIImage barFrame;
        private Asset<Texture2D> barFill;
        private UIImage icon;
        private Color gradientA;
        private Color gradientB;

        private float fadeInOutTime = 15;
        private float timer = 0;

        private UIState state;

        //private Player player = Main.LocalPlayer;

        private enum UIState
        {
            FadeIn,
            Sustain,
            FadeOut,
        }
        public override void OnInitialize()
        {
            
            
            barFrame = new UIImage(ModContent.Request<Texture2D>("WingTimeUI/WingTimeBar")); // Frame of our resource bar
            barFrame.Left.Set(-122f, 0f);
            barFrame.Top.Set(0, 0f);
            barFrame.Width.Set(244, 0f);
            barFrame.Height.Set(24, 0f);

            barFill = ModContent.Request<Texture2D>("WingTimeUI/_wingBarFill");

            text = new UIText("0/0", 0.8f); // text to show stat
            text.Width.Set(244, 0f);
            text.Height.Set(24, 0f);
            text.Top.Set(40, 0f);
            text.Left.Set(-122f, 0f);

            icon = new UIImage(ModContent.Request<Texture2D>("WingTimeUI/wingIcon"));
            icon.Left.Set(-150f, 0f);
            icon.Top.Set(-10f, 0f);


            SetAreaConstraint();
        }

        public void SetAreaConstraint()
        {
            UIConfig config = ModContent.GetInstance<UIConfig>();
            area = new UIElement();
            area.Left.Set((0f), config.horizontalPercentage); 
            area.Top.Set(0f, config.verticalPercentage);
            area.Width.Set(244, 0f);
            area.Height.Set(60, 0f);

            area.Append(text);
            area.Append(barFrame);
            area.Append(icon);
            Append(area);
        }

        public static void ApplyConfigChanges(float horz, float ver)
        {
            if(area == null)
            {
                return;
            }
            area.Left.Set((0f), horz);
            area.Top.Set(0f, ver);
            area.Recalculate();
            area.RecalculateChildren();
        }

        public void ChangeUIResolutionPlacement(Vector2 res)
        {
            SetAreaConstraint();
        }

        public void SetUIAlpha(float alpha)
        {
            Vector4 barvec = Color.White.ToVector4();
            barvec *= alpha;
            Vector4 iconVec = Color.White.ToVector4();
            iconVec *= alpha;
            Vector4 textLight = Color.White.ToVector4();
            textLight *= alpha;
            Vector4 textShadow = Color.Black.ToVector4();
            barFrame.Color = new Color(barvec);
            icon.Color = new Color(iconVec);
            text.TextColor= new Color(textLight);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            // This prevents drawing unless we are using an ExampleCustomResourceWeapon
            Player player = Main.LocalPlayer;
            if (player.equippedWings == null  || player.velocity.Y == 0)
                state = UIState.FadeOut;
            else
                state = UIState.FadeIn; 

            if(state == UIState.FadeOut && timer <= 0)
            {
                timer = 0;
                return;
            }
            else if(state == UIState.FadeIn && timer >= fadeInOutTime)
            {
                state = UIState.Sustain;
                timer = fadeInOutTime;
            }

            float alpha = 1f;
            switch (state)
            {
                case UIState.Sustain:
                    alpha = 1f;
                    break;
                case UIState.FadeIn:
                    alpha = timer / fadeInOutTime;
                    timer++;
                    break;
                case UIState.FadeOut:
                    alpha = timer / fadeInOutTime;
                    timer--;
                    break;
            }

            SetUIAlpha(alpha);

            float denom = player.wingTimeMax;
            denom += player.rocketBoots == 0 ? 0f : (float)(player.rocketTimeMax * 6);
            float quotient = player.wingTime / denom;
            if(state == UIState.FadeOut || player.grappling[0]>-1)
            {
                quotient = 1f;
            }
            quotient = Utils.Clamp(quotient, 0f, 1f); // Clamping it to 0-1f so it doesn't go over that.

            // Here we get the screen dimensions of the barFrame element, then tweak the resulting rectangle to arrive at a rectangle within the barFrame texture that we will draw the gradient. These values were measured in a drawing program.
            Rectangle hitbox = barFrame.GetInnerDimensions().ToRectangle();
            hitbox.X += 6;
            hitbox.Y += 6;
            hitbox.Height -= 6;

            // Now, using this hitbox, we draw a gradient by drawing vertical lines while slowly interpolating between the 2 colors.


            base.Draw(spriteBatch);

            int left = hitbox.Left;
            int right = hitbox.Right;
            int steps = (int)((right - left) * quotient);
            Vector4 col = Color.White.ToVector4();
            col *= alpha;
                // float percent = (float)i / steps; // Alternate Gradient Approach
            spriteBatch.Draw(barFill.Value, new Vector2(left, hitbox.Y), new Rectangle(0, 0, (int)(barFill.Width() * quotient), barFill.Height()), new Color(col));
            //new Rectangle(left, hitbox.Y, barFill.Width(), barFill.Height())

            icon.Draw(spriteBatch);
        }

        // Here we draw our UI
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            Player player = Main.LocalPlayer;
            if (player.equippedWings == null || player.velocity.Y == 0)
                return;

            float secs = (float)player.wingTime / 60f;
            // Setting the text per tick to update and show our resource values.
            text.SetText($"{secs.ToString("0.00")}s");
            base.Update(gameTime);
        }

    }

    [Autoload(Side = ModSide.Client)]
    internal class WingUISystem : ModSystem
    {
        private UserInterface WingUserInterface;

        internal MainUI ui;

        public override void Load()
        {
            WingUserInterface = new();
            ui = new();
            WingUserInterface.SetState(ui);
        }

        public override void UpdateUI(GameTime gameTime)
        {
            WingUserInterface?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int resourceBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
            if (resourceBarIndex != -1)
            {
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "WingTimeUI: Main UI",
                    delegate {
                        WingUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
