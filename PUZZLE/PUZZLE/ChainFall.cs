using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUZZLE
{
    class ChainFall
    {
        // メンバー変数の宣言
        private int[,] colorTable;       // ブロックの色（２次元配列）


        /// <summary>
        /// コンストラクタ（生成時に自動的に呼び出される）
        /// </summary>
        public ChainFall(int[,] colorTable)
        {
            // 情報の登録
            this.colorTable = colorTable;
        }
        public bool Update()
        {
            // まず連鎖で落ちるブロックはないと設定
            bool flag = false;

            // （縦横で２重ループ）
            // （Y方向は逆順にループ）
            for (int y = Block.YMax - 2; y >= 0; y--)
            {
                for (int x = 0; x < Block.XMax; x++)
                {
                    // ブロックがあれば
                    if (colorTable[y, x] != 0)
                    {
                        // 下が空いていれば
                        if (colorTable[y + 1, x] == 0)
                        {
                            // 下に位置にコピーして、今の場所に空白を入れることで、
                            // ブロックが下に移動
                            colorTable[y + 1, x] = colorTable[y, x];    // 下にコピー
                            colorTable[y, x] = 0;                      // 空白を入れる

                            // 連鎖で落ちるブロックがあることを登録
                            flag = true;
                        }
                    }
                }
            }

            // 結果を返す
            return flag;
        }


    }
}
