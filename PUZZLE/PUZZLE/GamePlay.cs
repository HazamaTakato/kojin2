using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PUZZLE
{
    class GamePlay:IScene
    {
        private bool endFlag;
        private Stage stage;
        private MoveBlock moveBlock;
        private StopBlock stopBlock;
        private Score score;
        private Cursor cursor;
        public GamePlay()
        {
            stage = new Stage();
            score = new Score();
            stopBlock = new StopBlock(score);//停止
            cursor = new Cursor(stopBlock);
            moveBlock = new MoveBlock(stopBlock);//移動ブロック
        }

        public void Initialize()
        {
            endFlag = false;
            moveBlock.Initialize();
            stopBlock.Initialize();
            score.Initialize();
            cursor.Initialize();
        }

        public void Update()
        {
            moveBlock.Update();
            stopBlock.Update();
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                endFlag = false;
            }
            // 上までブロックが積み上がったら
            if (stopBlock.GetStackY() == 0)
            {
                endFlag = true;// シーン終了に設定
            }
            cursor.Update();
        }

        public void Draw(Renderer renderer)
        {
            stage.Draw(renderer);
            stopBlock.Draw(renderer);
            moveBlock.Draw(renderer);
            score.Draw(renderer);
            cursor.Draw(renderer);
        }

        public bool IsEnd()
        {
            return endFlag;
        }

        public Scene Next()
        {
            return Scene.Ending;
        }
    }
}
