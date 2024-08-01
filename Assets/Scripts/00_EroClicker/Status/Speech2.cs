using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Speech2 : MonoBehaviour
{
	// テキストを表示するテキスト
	Text wordtxt;
	// 表示する文字
	string word;

	// 文字送りの速度
	[SerializeField]
	float characterFeedSpeed = 0.3f;

	// 文字を更新する速度
	[SerializeField]
	float speechSpeed = 3;

	bool isInit = true;

	Vector3 textStartPos = Vector3.zero;
	Vector3 textMovePos = Vector3.zero;

	private void OnEnable()
	{
		isInit = true;
	}

	private void LateUpdate()
	{
		if (isInit && WordsManager.Instance != null)
		{
			isInit = false;
			// テキスト取得
			wordtxt = GetComponent<Text>();

			textStartPos = wordtxt.transform.position;

			// 位置調整
			textMovePos = (wordtxt.transform as RectTransform).position + new Vector3(0, 0.15f);

			Speech();
		}
	}

	int n = 0;

	/// <summary>
	/// 表示する文字取得
	/// </summary>
	void GetWord()
	{
		var key = FeverManager.Instance.IsFever ? GameData.feverSpriteIndex + 1 : GameData.treeIndex + 1;
		var words = WordsManager.Instance.GetCSV(key.ToString());
		//var number = 0;

		while (true)
		{
			var number = Random.Range(0, words.Count);
			if (word != words[number])
			{
				word = words[number];
				break;
			}
		}
		++n;
		n %= (words.Count - 2);
	}

	/// <summary>
	/// 文字設定
	/// </summary>
	/// <param name="dispWord">設定する文字</param>
	void SetWord(string dispWord)
	{
		// 文字設定
		// 他に必要な処理がある場合はなにか書く（そのための関数化）
		wordtxt.text = dispWord;
	}

	/// <summary>
	/// 文字更新
	/// </summary>
	void Speech()
	{
		// 表示する文字取得
		GetWord();
		if (GameData.language == 0)
		{
			// 文字送り開始
			StartCoroutine(CharacterFeed());
		}
		else if (GameData.language == 1)
		{
			// 英語の文字送り
			StartCoroutine(ICharacterFeed());
		}
	}

	/// <summary>
	/// 文字送り(日本語)
	/// </summary>
	IEnumerator CharacterFeed()
	{
		// 表示する文字数
		int wordNum = 0;

		// 文字初期化
		wordtxt.text = "";

		wordtxt.transform.position = textStartPos;

		// 文字が最大になるまで回す
		while (word.Length > wordNum)
		{
			// 表示されている文字取得
			string dispWord = wordtxt.text;

			// 改行をスペースで表現しているので、スペースを削除
			if (wordNum != 0 &&
				wordtxt.text[wordtxt.text.Length - 1] == '\n' &&
				(word[wordNum] == ' ' || word[wordNum] == '　'))
			{
				++wordNum;
			}
			// 表示する文字追加
			dispWord += word[wordNum];

			// ♡3つに対応
			if (word[wordNum] == '♡' &&
				word.Length > wordNum + 1 &&
				word[wordNum + 1] == '♡')
			{
				// 文字設定
				SetWord(dispWord);
				++wordNum;
				yield return new WaitForSeconds(characterFeedSpeed);
				continue;
			}

			// 改行する文字か判定(大文字小文字どちらにも対応する)
			if (word[wordNum] == '。' ||
				word[wordNum] == '?' ||
				word[wordNum] == '？' ||
				word[wordNum] == '!' ||
				word[wordNum] == '！' ||
				word[wordNum] == '♡' ||
				word[wordNum] == '★' ||
				word[wordNum] == ')' ||
				word[wordNum] == '）')
			{
				var lastWords = wordtxt.text.LastIndexOf('\n');
				if (lastWords == -1)
				{
					lastWords = 0;
				}

				var sp = wordtxt.text.Split("\n");
				if (sp[sp.Length - 1].Contains("."))
				{
					lastWords += 4;
				}
				var lastWordsLength = wordtxt.text.Length - lastWords;
				// 禁則文字対応、前の文字がドットの場合三点リーダーなので改行はなし
				if (lastWordsLength == 20/* && dispWord[dispWord.Length - 1] != '.'*/)
				{
					// 小さい文字も先頭にしない
					if (dispWord[dispWord.Length - 2] == 'ぁ' ||
						dispWord[dispWord.Length - 2] == 'ぃ' ||
						dispWord[dispWord.Length - 2] == 'ぅ' ||
						dispWord[dispWord.Length - 2] == 'ぇ' ||
						dispWord[dispWord.Length - 2] == 'ぉ' ||
						dispWord[dispWord.Length - 2] == 'っ' ||
						dispWord[dispWord.Length - 2] == '…')
					{
						// 最後の文字保存
						var s = dispWord[dispWord.Length - 3];
						var s1 = dispWord[dispWord.Length - 2];
						var s2 = dispWord[dispWord.Length - 1];
						// 最後の文字削除
						dispWord = dispWord.Remove(dispWord.Length - 2);
						// \nを代入
						dispWord += "\n" + s + s1 + s2;
					}
					else
					{
						// 最後の文字保存
						var s1 = dispWord[dispWord.Length - 2];
						var s2 = dispWord[dispWord.Length - 1];
						// 最後の文字削除
						dispWord = dispWord.Remove(dispWord.Length - 2);
						// \nを代入
						dispWord += "\n" + s1 + s2;
					}
				}

				if (word[wordNum] == '!' ||
					word[wordNum] == '！')
				{
					// !?かどうかを判定する
					if (word.Length > (wordNum + 1) &&
						(word[wordNum + 1] == '?' || word[wordNum + 1] == '？'))
					{
						++wordNum;
						// !が追加されているので、?だけ追加
						dispWord += word[wordNum];
					}
				}

				// 次の文字が)だった場合改行しない
				if (((word.Length - 1) > wordNum) && (word[wordNum + 1] == ')' || word[wordNum + 1] == '）'))
				{

				}
				else
				{
					dispWord += "\n";
					if (sp.Length == 4)
					{
						(wordtxt.transform as RectTransform).position = textMovePos;
					}
				}
			}
			else if (
				word[wordNum] == 'ぁ' ||
				word[wordNum] == 'ぃ' ||
				word[wordNum] == 'ぅ' ||
				word[wordNum] == 'ぇ' ||
				word[wordNum] == 'ぉ' ||
				word[wordNum] == 'っ' ||
				word[wordNum] == 'ゃ' ||
				word[wordNum] == '.' ||
				word[wordNum] == '…')
			{
				var lastWords = wordtxt.text.LastIndexOf('\n');
				if (lastWords == -1)
				{
					lastWords = 0;
				}

				var sp = wordtxt.text.Split("\n");
				if (sp[sp.Length - 1].Contains("."))
				{
					lastWords += 4;
				}
				var lastWordsLength = wordtxt.text.Length - lastWords;
				// 禁則文字対応
				if (lastWordsLength ==
					(word != "遠慮無く……私を…………滅茶苦茶にして……くださいぃ♡" ? 20 : 19))
				{
					// 小さい文字も先頭にしない
					if (dispWord[dispWord.Length - 2] == 'ぁ' ||
						dispWord[dispWord.Length - 2] == 'ぃ' ||
						dispWord[dispWord.Length - 2] == 'ぅ' ||
						dispWord[dispWord.Length - 2] == 'ぇ' ||
						dispWord[dispWord.Length - 2] == 'ぉ' ||
						dispWord[dispWord.Length - 2] == 'っ' ||
						dispWord[dispWord.Length - 2] == 'ゃ' ||
						dispWord[dispWord.Length - 4] == '.' ||
						dispWord[dispWord.Length - 2] == '…')
					{
						// 最後の文字保存
						var s = dispWord[dispWord.Length - 3];
						var s1 = dispWord[dispWord.Length - 2];
						var s2 = dispWord[dispWord.Length - 1];
						// 最後の文字削除
						dispWord = dispWord.Remove(dispWord.Length - 3);
						// \nを代入
						dispWord += "\n" + s + s1 + s2;
					}
					else
					{
						// 最後の文字保存
						var s1 = dispWord[dispWord.Length - 2];
						var s2 = dispWord[dispWord.Length - 1];
						// 最後の文字削除
						dispWord = dispWord.Remove(dispWord.Length - 2);
						// \nを代入
						dispWord += "\n" + s1 + s2;
					}
				}
			}

			// 三点リーダー対応
			if (word[wordNum] == '.')
			{
				for (int i = 0; i < 2; ++i)
				{
					++wordNum;
					dispWord += word[wordNum];
				}
			}

			// 文字設定
			SetWord(dispWord);

			//if (System.Text.RegularExpressions.Regex.IsMatch(dispWord, @"^[a-zA-Z0-9<>().,]+$"))
			//{
			//	// 新しく入れる文字
			//	var v = "";
			//	var length = dispWord.Length;
			//	for (int i = 0; i < length - 1; ++i)
			//	{
			//		v += dispWord[i];
			//	}
			//}

			// 表示する文字追加
			++wordNum;

			// 指定された秒数待つ
			yield return new WaitForSeconds(characterFeedSpeed);
		}
		// 次に文字を更新するまでの時間待つ
		yield return new WaitForSeconds(speechSpeed);
		Speech();
	}

	/// <summary>
	/// 文字送り(英語)
	/// </summary>
	/// <returns></returns>
	IEnumerator CharacterFeedEN()
	{
		// 表示している文字数
		int wordNum = 0;
		// スペースで区切って文字取得(単語ごとに区切る)
		var words = word.Split(' ');
		// 表示している単語数
		int wordValue = 0;
		// 表示している単語の文字数
		int nowWordValue = 0;

		// 文字初期化
		wordtxt.text = "";

		// true = 一度文字を飛ばした
		bool isSkiped = false;
		// 文字が最大になるまで回す
		while (word.Length > wordNum)
		{
			// 表示されている文字取得
			string dispWord = wordtxt.text;

			// true = 次の文字に進む
			bool isNextWord = true;

			// 先頭の文字でない場合スペースを入れる
			if (dispWord != "" && !isSkiped)
			{
				// 最後の文字が改行ではない場合スペースを入れる
				if (wordtxt.text[wordtxt.text.Length - 1] != '\n')
				{
					dispWord += " ";
					++wordNum;
				}
			}

			// 単語の文字数
			for (int i = nowWordValue; i < words[wordValue].Length; ++i)
			{
				if ((words[wordValue][i] == '.' ||
					words[wordValue][i] == '!' ||
					words[wordValue][i] == '！' ||
					words[wordValue][i] == '?' ||
					words[wordValue][i] == '？') &&
					!isSkiped)
				{
					isSkiped = true;
					isNextWord = false;
					break;
				}
				// 追加する文字を一文字づつ追加
				dispWord += words[wordValue][i];
				// 三点リーダー対応
				if (words[wordValue][i] == '.' &&        // 表示しようとしている文字がドットかどうか
					(i + 1) < words[wordValue].Length && // 配列外ではないか
					words[wordValue][i + 1] == '.')      // 次に表示する予定の文字がドットであれば三点リーダーとみなす
				{
					// .を2回追加
					dispWord += words[wordValue][i + 1];
					dispWord += words[wordValue][i + 2];
					// 表示している文字数を増やす
					wordNum += 3;
					// 表示している単語数を増やす
					nowWordValue += 3;

					// 次の文字に行かない
					isNextWord = false;
					break;
				}
				++nowWordValue;
				++wordNum;
				isSkiped = false;
			}

			// 次の文字に進む
			if (isNextWord)
			{
				// 追加する文字の初期化
				nowWordValue = 0;

				// 改行する文字対応
				var c = dispWord[dispWord.Length - 1];
				if (c == '.' ||
					c == '!' ||
					c == '！' ||
					c == '?' ||
					c == '？')
				{
					dispWord += "\n";
				}
				else
				{
					// 表示する文字を増やす
					++wordValue;
				}
			}

			// 文字設定
			SetWord(dispWord);

			// 指定された秒数待つ
			yield return new WaitForSeconds(characterFeedSpeed);
		}
		// 次に文字を更新するまでの時間待つ
		yield return new WaitForSeconds(speechSpeed);
		Speech();
	}

	IEnumerator ICharacterFeed()
	{
		// 表示している文字数
		int letterValue = 0;
		// 表示している単語数
		int wordValue = 0;
		// 表示している単語の文字数
		int numberOfCharactersInWord = 0;
		// スペースで区切って文字取得(単語ごとに区切る)
		var words = word.Split(' ');

		// 文字初期化
		wordtxt.text = "";

		// true = 三点リーダー
		var isThreePointLeader = false;
		// true = 二度目の三点リーダー(絶対に三点リーダーは二回ある)
		var isThreePointLeaderSeconds = false;

		// true = 改行する文字
		var isNewLine = false;

		// true = !?マーク
		var isBikkuriHatena = false;

		// 表示する文字
		var dispWord = "";

		// 全ての文字を表示するまでループ
		while (words.Length > wordValue)
		{
			// 表示する文字列
			var dispWords = wordtxt.text;

			// 1単語ずつ取得する
			for (int i = numberOfCharactersInWord; i < words[wordValue].Length; ++i)
			{
				// 三点リーダーの場合は三文字表示する
				if (isThreePointLeader)
				{
					// 三文字追加 (forとどっちが早いのかわからないのでベタ書き)
					dispWord += words[wordValue][i];
					dispWord += words[wordValue][i + 1];
					dispWord += words[wordValue][i + 2];

					// 三文字追加したので、表示している文字数を3追加
					letterValue += 3;
					// 三文字追加したので、表示している単語の文字数に3追加
					numberOfCharactersInWord += 3;

					// 二度表示する
					if (!isThreePointLeaderSeconds)
					{
						// 一度表示したので、次の三点リーダーを表示する
						isThreePointLeaderSeconds = true;
						// 次のタイミングで2つ目の三点リーダーを表示したいのでbreak
						break;
					}
					else
					{
						// 三点リーダーの表示が終わったのでfalseにする
						isThreePointLeader = false;

						// 次の三点リーダーで二回表示できるように初期化
						isThreePointLeaderSeconds = false;
						// 次の単語を表示し始めるのでbreak
						break;
					}
				}
				// 改行する文字の場合文字を表示して改行する(!,?,!?,.)
				else if (isNewLine)
				{
					// !?の場合二文字追加
					if (isBikkuriHatena)
					{
						dispWord += words[wordValue][i];
						dispWord += words[wordValue][i + 1];
						// 二文字追加したので、表示している文字数を2追加
						letterValue += 3;
						// 二文字追加したので、表示している単語の文字数に2追加
						numberOfCharactersInWord += 3;
					}
					// !?以外の場合一文字追加
					else
					{
						dispWord += words[wordValue][i];
						// 一文字追加したので、表示している文字数を1追加
						++letterValue;
						// 一文字追加したので、表示している単語の文字数に1追加
						++numberOfCharactersInWord;
					}

					// 改行する
					dispWord += "\n";

					// 改行する文字の表示が終わったのでfalseにする
					isNewLine = false;
					// !?ではない状態にする
					isBikkuriHatena = false;

					// 次の単語を表示し始めるのでbreak
					break;
				}
				// それ以外は一文字ずつ表示する
				else
				{
					// 一文字ずつ取得
					dispWord += words[wordValue][i];
					// 文字を追加したので、表示している文字、単語の文字数を追加
					++letterValue;
					++numberOfCharactersInWord;

					// 次の文字がある場合
					if (words[wordValue].Length > (i + 1))
					{
						// 三点リーダー対応
						// 次の文字がドットでかつ
						// その次の文字が存在していて、
						// その次の文字がドットの場合
						// 三点リーダーとみなす
						if (words[wordValue][i + 1] == '.' &&
							words[wordValue].Length > (i + 2) &&
							words[wordValue][i + 2] == '.')
						{
							// 三点リーダーは空白がないので次の文字に進まない
							isThreePointLeader = true;
							// 同じタイミングで表示したくないのでbreak
							break;
						}

						// 改行する文字の場合、次で文字を入れて改行する
						if (words[wordValue][i + 1] == '.' ||
							words[wordValue][i + 1] == '!' ||
							words[wordValue][i + 1] == '?' ||
							words[wordValue][i + 1] == '！' ||
							words[wordValue][i + 1] == '？' ||
							words[wordValue][i + 1] == '♡' ||
							words[wordValue][i + 1] == '★')
						{
							// ドットも間に空白がないので、次の文字に進まない
							isNewLine = true;

							// 文字数がオーバーしていないかつ
							// 次の文字が!かつ
							// その次の文字が?の場合
							// !?とみなす
							// (全角半角対応)
							if (words[wordValue].Length > (i + 2) &&
								(words[wordValue][i + 1] == '!' || words[wordValue][i + 1] == '！') &&
								(words[wordValue][i + 2] == '?' || words[wordValue][i + 1] == '？'))
							{
								isBikkuriHatena = true;
							}

							// 同じタイミングで表示したくないのでbreak
							break;
						}
					}
				}
			}

			// 取得した単語を表示する文字列に追加
			dispWords += dispWord;

			// 次の文字が三点リーダーでも改行する文字でもない場合次の文字に移動
			if (!isNewLine && !isThreePointLeader)
			{
				// 次の文字に進む
				++wordValue;

				// 改行した後はスペースを入れない
				if (dispWord[dispWord.Length - 1] != '\n')
				{
					// 次の単語を表示する前にスペースを入れる
					dispWord += " ";
				}

				// 文字数をリセット
				numberOfCharactersInWord = 0;
			}

			// 文字設定
			SetWord(dispWord);

			// 指定された秒数待つ
			yield return new WaitForSeconds(characterFeedSpeed);
		}
		// 次に文字を更新するまでの時間待つ
		yield return new WaitForSeconds(speechSpeed);
		Speech();
	}
}
