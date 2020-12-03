using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PUZZLE
{
    class Title:IScene
    {
        private int timer;
        private bool endFlag;
        private bool IsPressKey;
        public Title()
        {

        }
        
        public void Initialize()
        {
            endFlag = false;
            IsPressKey = true;
            timer = 0;
        }
        
        public void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if (IsPressKey == false)
                {
                    endFlag = true;
                    IsPressKey = true;
                }
            }
            else
            {
                IsPressKey = false;
            }
            timer++;
        }

        public void Draw(Renderer renderer)
        {
            renderer.DrawTexture("titlebg", new Vector2(0, 0));

            renderer.DrawTexture("titletext", new Vector2(0, 160), 0.6f);

            renderer.DrawTexture("info", new Vector2(0, 300), 1f);

            if (timer % 60 < 30)
            {
                renderer.DrawTexture("starttext", new Vector2(0, 660));
            }
            else
            {
                renderer.DrawTexture("starttext", new Vector2(0, 660), 0.3f);
            }
        }

        public bool IsEnd()
        {
            return endFlag;
        }

        public Scene Next()
        {
            return Scene.GamePlay;
        }
    }
}
