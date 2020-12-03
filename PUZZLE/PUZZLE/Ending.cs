using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUZZLE
{
    class Ending:IScene
    {
        private bool endFlag;
        private bool isPressKey;
        private int timer;
        public Ending()
        {

        }

        public void Initialize()
        {
            endFlag = false;
            isPressKey = true;
            timer = 0;
        }
        public void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if (isPressKey == false)
                {
                    if (timer>=2)
                    {
                        endFlag = true;
                        isPressKey = true;
                    }
                }
            }
            else
            {
                isPressKey = false;
            }
            timer++;
        }

        public void Draw(Renderer renderer)
        {
            renderer.DrawTexture("titlebg", new Vector2());
            renderer.DrawTexture("ending", new Vector2(0, 160), 0.8f);

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
            return Scene.Title;
        }
    }
}
