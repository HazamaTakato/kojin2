using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace PUZZLE
{
    class Stage
    {
        public Stage() { }

        public void Draw(Renderer renderer)
        {
            renderer.DrawTexture("titlebg", new Vector2(0, 0));//ステージ
            //黒い壁紙
            renderer.DrawTexture("wall", new Vector2(Block.StartX, Block.StartY),200);
        }
    }
}
