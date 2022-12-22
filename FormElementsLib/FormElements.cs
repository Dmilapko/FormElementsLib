using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace FormElementsLib
{
    public class FormElement:IDisposable
    {
        public bool pressed = false;
        public event EventHandler<ClickEventArgs> Click;
        public Vector2 Location;
        public System.Drawing.Size Size;
        public int mode;
        public const int initmode = 0;
        public const int onmode = 1;
        public const int pressmode = 2;
        public bool visible = true, active = true;
        public Color color;

        public virtual void Dispose()
        {

        }

        public virtual void Show()
        {
            visible = true;
        }

        public virtual void Hide()
        {
            visible = false;
        }
        public void CheckClick(MouseState state)
        {
            if (active&&visible)
            {
                if ((state.X >= Location.X) && (state.Y >= Location.Y) && (state.X <= (Size.Width + Location.X)) && (state.Y <= (Size.Height + Location.Y)))
                {
                    if (state.LeftButton == ButtonState.Pressed)
                    {
                        if (!pressed)
                        {
                            pressed = true;
                            MakePressed();
                        }
                    }
                    else
                    {
                        if (pressed)
                        {
                            Click?.Invoke(null, new ClickEventArgs(state));
                            OnElement();
                            pressed = false;
                        }
                        else
                        {
                            OnElement();
                        }
                    }
                }
                else
                {
                    if (state.LeftButton == ButtonState.Pressed) mode = initmode;
                    Release();
                }
            }
        }
        public virtual void OnElement()
        {

        }
        public virtual void MakePressed()
        {

        }

        public virtual void Release()
        {
            pressed = false;
        }
      
        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }

        public virtual void Check(MouseState mouse, KeyboardState keyboard)
        {
            if (visible)
            {
                CheckClick(mouse);
            }
        }
    }
    
    public class TextFE:FormElement
    {
        public string text;
        public Color textcolor;
        public bool outline = false;
        public float outlinelength = 1;
        public Color OutlineColor = Color.White;
    }

    public class ClickEventArgs : EventArgs
    {
        public MouseState mouseState;

        public ClickEventArgs(MouseState _mouseState)
        {
            mouseState = _mouseState;
        }
    }
}
