using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Ateam
{
    public class Hotel : BaseBattleAISystem
    {
		readonly static float ATTACK_MIDDLE_LEN           = 8;
		readonly static float ATTACK_SHORT_LEN            = 2;
		readonly static float INVINCIBLE_LEN              = 3;
        //---------------------------------------------------
        // InitializeAI
        //---------------------------------------------------
        override public void InitializeAI()
        {
        }

        //---------------------------------------------------
        // UpdateAI
        //---------------------------------------------------
        override public void UpdateAI()
        {

			//互いのチームのデータを取得
			List<CharacterModel.Data> playerList = GetTeamCharacterDataList(TEAM_TYPE.PLAYER);
			List<CharacterModel.Data> enemyList = GetTeamCharacterDataList(TEAM_TYPE.ENEMY);

			//索敵機から順にロックオン
			//

			foreach (CharacterModel.Data playerData in playerList) {
				
				float minlen = 100;

				foreach (CharacterModel.Data enemyData in enemyList) {
					
					if (enemyData.Hp <= 0) {
						continue;
					}

					float len = (enemyData.BlockPos - playerData.BlockPos).magnitude;
					if (minlen > len)
						minlen = len;
				}
				if (minlen < ATTACK_SHORT_LEN)
					Action(playerData.ActorId, Define.Battle.ACTION_TYPE.ATTACK_SHORT);
				if (minlen < INVINCIBLE_LEN)
					Action(playerData.ActorId, Define.Battle.ACTION_TYPE.INVINCIBLE);
				if (minlen < ATTACK_MIDDLE_LEN)
					Action(playerData.ActorId, Define.Battle.ACTION_TYPE.ATTACK_MIDDLE);
				Action(playerData.ActorId, Define.Battle.ACTION_TYPE.ATTACK_LONG);
			}

			for (int i = 0; i < playerList.Count; i++) {
				
				CharacterModel.Data character = playerList[i];
				int id = character.ActorId;

				int move = UnityEngine.Random.Range(0, 4);
				switch (move)
				{
				case 0:
					//上移動
					Move(id, Common.MOVE_TYPE.UP);
					break;

				case 1:
					//下移動
					Move(id, Common.MOVE_TYPE.DOWN);
					break;

				case 2:
					//左移動
					Move(id, Common.MOVE_TYPE.LEFT);
					break;

				case 3:
					//右移動
					Move(id, Common.MOVE_TYPE.RIGHT);
					break;
				}

				//Action(id, Define.Battle.ACTION_TYPE.ATTACK_LONG);


			}




        }


        //---------------------------------------------------
        // ItemSpawnCallback
        //---------------------------------------------------
        override public void ItemSpawnCallback(ItemSpawnData itemData)
        {
        }
    }
}