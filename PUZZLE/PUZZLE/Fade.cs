using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace PUZZLE
{
    class Fade
    {
        private bool mode;
        private float timer;

        public Fade()
        {

        }

        public void Initialize()
        {
            mode = true;
            timer = 1;
        }
        public void Update()
        {
            if (mode == true)
            {
                timer -= 0.01f;
                if (timer <= 0)
                {
                    mode = false;
                }
            }
        }
        public void Draw(Renderer renderer)
        {
            if (mode == true)
            {
                renderer.DrawTexture("fade", new Vector2(0, 0), timer);
            }
        }
    }
}
