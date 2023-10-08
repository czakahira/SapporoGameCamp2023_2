using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;				//StringBuilder使用
using System.IO;				//ファイルの読み書き
//using System.IO.Compression;
using UnityEngine;
using Cysharp.Threading.Tasks;	//UniTasks使用


/// <summary>
/// 文字列に変換できるデータ形式
/// </summary>
public enum EConvertTextType
{
	JSON,
	XML,
}

/// <summary>
/// ◆ FileSteamによる読み書きの便利クラス
/// <para>
/// １）このクラスは「オブジェクト >> 選択データ形式の文字列 >> バイト列」に変換しファイルへ書き込みます。 読み込みは逆順になります。
/// </para>
/// <para>
/// ２）読み書きは非同期で行われます。処理の完了は引数に渡すコールバックにより受け取ることが可能です。
/// </para>
/// <para>
/// ３）書き込み関数では暗号化、読み込み関数では復号の実施を、それぞれの引数によって選択できます。<br/>
/// ただし、暗号化されたファイルを復号せずに読み込む、もしくはその逆を行うと例外が発生しますのでご注意ください。
/// </para>
/// </summary>
public class FileStreamer : IDisposable
{
	void IDisposable.Dispose()
	{

	}

	#region Static
	// -------------------------
	// パスチェックに必要な文字
	// -------------------------
	/* １）ディレクトリセパレータ
	 * ◆補足
	 * プラットフォームによって種類が違いう事を考慮します。
	 * Wiindows は「\」MaxやLinuxは「/」です。
	 */
	static readonly private char PATH_BACKSLASH = char.Parse(@"\");
	static readonly private char PATH_SLASH		= char.Parse("/");
	/* ２）ドライブからの接続詞
	 * ◆補足
	 * プラットフォームによって種類が違う事を考慮します。
	 * Wiindows は「;」MaxやLinuxは「:」です。
	 */
	static readonly private char PATH_SEMICOLON = char.Parse(";");
	static readonly private char PATH_COLON		= char.Parse(":");
	// ３）ファイル拡張子のピリオド
	static readonly private char PATH_PERIOD	= char.Parse(".");

	// -------------------------
	// 静的メソッド
	// -------------------------
	/// <summary>
	/// 指定パスのファイルが存在するかチェックする <br/>
	/// ※ディレクトリのチェックには使用できません。\
	/// </summary>
	/// <param name="_path">
	/// チェックしたいファイルまでのパス <br/>
	/// 絶対パス・相対パスに対応しています
	/// </param>
	/// <param name="_outPut"> チェックの結果 </param>
	/// <returns> true = 在る / false = 無い </returns>
	static public bool CheckPathFileExist(string _path, out StreamExistCheckResultInfo _outPut)
	{
		//返り値となるフラグ
		//・途中のディレクトリが欠落した時点でfalseになる
		bool result = true;
		//出力先を初期化
		_outPut = new StreamExistCheckResultInfo();
		_outPut.result = StreamExistCheckResultInfo.EResult.Exist;

		//チェックした階層の履歴
		StringBuilder chechedHierarchy = new StringBuilder();

		//存在しないディレクトリの情報を更新するローカル関数
		void CheckExistDirectory(string _directryPath, ref bool _result, ref StreamExistCheckResultInfo _outPut2)
		{
			//ディレクトリが存在しなければ更新
			if (!Directory.Exists(_directryPath)) {
				_outPut2.notExistPath = _directryPath;
				_outPut2.result = StreamExistCheckResultInfo.EResult.NotExistDirectory;
				_result = false;
			}
		}

#if true
		//ディレクトリセパレータで分割
		string[] hierarchies = _path.Split(new char[] { PATH_SLASH, PATH_BACKSLASH });
		int length = hierarchies.Length;
		//最終階層の番号
		int lastHierarchyIndex = (length - 1);
		//初めの階層に接続詞があれば絶対パスとみなす
		bool isFullPath = (hierarchies[0].Contains(PATH_COLON) | hierarchies[0].Contains(PATH_SEMICOLON));

		//チェック処理
		for (int i = 0; i < length; i++) {
			chechedHierarchy.Append($"{hierarchies[i]}{PATH_BACKSLASH}");

			//絶対パスだったら最初の階層（ドライブ）は飛ばす
			if (isFullPath && (i <= 0)) { continue; }

			//途中まではディレクトリの有無チェック
			if (i < lastHierarchyIndex) {
				CheckExistDirectory(chechedHierarchy.ToString(), ref result, ref _outPut);
			}
			//最後の階層
			else {
				//途中のディレクトリが全て在るならファイルの有無をチェックする
				//※ 途中で欠落が有ったら下層のファイルはそもそも存在しない
				if (result && !File.Exists(_path)) {
					_outPut.notExistPath = _path;
					_outPut.result = StreamExistCheckResultInfo.EResult.NotExistFile;
					result = false;
				}
			}
		}
		chechedHierarchy.Clear();
#else
		//指定パスを１文字に変換
		char[] elements = _path.ToCharArray();
		//指定パス内で見つかったスラッシュ "/" の数
		int slashCnt = 0;

		//１文字ずつチェックしていく
		for (int i = 0; i < elements.Length; i++) {
			char element = elements[i];

			//▼ コロンが見つかったらその隣にある「ドライブとディレクトリ間のスラッシュ」を無視する
			//例）D:/Main...
			if (element.Equals(PATH_COLON) || element.Equals(PATH_SEMICOLON)) {
				slashCnt--;
			} else
			//▼ スラッシュが１つ見つかったら履歴のパスよりディレクトリの有無をチェックする
			if (element.Equals(PATH_SLASH) || element.Equals(PATH_BACKSLASH)) {
				slashCnt++;
				if (slashCnt >= 1) {
					CheckExistDirectory(chechedHierarchy.ToString(), ref result, ref _outPut);
					slashCnt = 0; //スラッシュ数リセット
				}
			}
			//▼ 途中階層のディレクトリが全て存在していれば、ファイルの有無をチェックする
			if (i >= (elements.Length - 1)) {
				if (result) {
					if (!File.Exists(_path)) {
						_outPut.notExistPath = _path;
						_outPut.result = FileAndDirectoryExistCheckResultInfo.EResult.NotExistFile;
						result = false;
					}
					break; // forを抜ける
				}
			}

			//チェック済み文字を履歴更新
			chechedHierarchy.Append(element);
		}
		//履歴削除
		chechedHierarchy.Clear();
#endif
		//返す
		return result;
	}

	/// <summary>
	/// オブジェクトを指定形式のテキストに変換します
	/// </summary>
	/// <param name="_obj"> 変換したいオブジェクト </param>
	/// <param name="_convertTextType"> 変換後のテキスト形式 既定値はJSONです </param>
	/// <returns> 変換されたテキスト </returns>
	static public string ConvertToText<T>(T _obj, EConvertTextType _convertTextType = EConvertTextType.JSON)
	{
		switch (_convertTextType) {
			case EConvertTextType.XML:
				return XmlUtility.ToXml(_obj);

			case EConvertTextType.JSON:
			default:
				return JsonUtility.ToJson(_obj);
		}
	}

	/// <summary>
	/// 指定形式のテキストをオブジェクトに変換します
	/// </summary>
	/// <param name="_text"> 変換元のテキスト <br/> これの形式は第２引数で指定する形式と同じでなければなりません </param>
	/// <param name="_convertTextType"> 変換元のテキストの形式 既定値はJSONです </param>
	/// <returns> 変換されたオブジェクト </returns>
	static public T ConvertFromText<T>(string _text, EConvertTextType _convertTextType = EConvertTextType.JSON)
	{
		switch (_convertTextType) {
			case EConvertTextType.XML:
				return XmlUtility.FromXml<T>(_text);

			case EConvertTextType.JSON:
			default:
				return JsonUtility.FromJson<T>(_text);
		}
	}

	#endregion

	#region Constructor & Destructor
	public FileStreamer()
	{

	}
	~FileStreamer() { }
	#endregion

	#region Method

	/// <summary>
	/// 指定オブジェクトを指定ファイルに非同期で書き込む
	/// <para> １）書き込む前に、指定パスのファイルや階層途中のディレクトリが存在するかチェックします。途中のディレクトリが存在しない場合は自動作成されます。 </para>
	/// <para> ２）指定パスに既に書き込み先となるファイルが存在した場合、既存ファイルをバックアップします。 </para>
	/// <para> ３）何らかの原因で書き込みに失敗した時、バックアップより復旧します。 </para>
	/// </summary>
	/// <typeparam name="T"> 書き込むオブジェクトの型（呼び出し時に省略可能） </typeparam>
	/// <param name="_obj"> 書き込むオブジェクト </param>
	/// <param name="_path"> 書き込み先となるファイルへのパス </param>
	/// <param name="_callback"> 書き込み終了時に呼ばれるコールバック </param>
	/// <param name="_convertType"> 書き込むデータ形式タイプ（標準はJSON） </param>
	/// <param name="_encrypt"> 暗号化実施フラグ </param>
	public async UniTask WriteAsync<T>(T _obj, string _path, Action<EGeneralResult> _callback = null, EConvertTextType _convertType = EConvertTextType.JSON, bool _encrypt = true)
	{
		EGeneralResult result = EGeneralResult.Failed;

		//バックアップのパス
		string backUpPath = null;

		//ファイルの有無をチェック
		bool isExisted = File.Exists(_path);
		//既存のファイルからバックアップを取る
		if (isExisted) {
			backUpPath = $"{_path}_backup";
			File.Move(_path, backUpPath);
		}
		//そもそも途中のディレクトリから無い場合、後述のファイル書き込みで例外になるのでディレクトリを作成
		else { 
			if (!CheckPathFileExist(_path, out StreamExistCheckResultInfo info)){
				if (info.result == StreamExistCheckResultInfo.EResult.NotExistDirectory) {
					Directory.CreateDirectory(info.notExistPath);
				}
			}
		}

		// オブジェクト >> 選択形式の文字列 >> バイトに変換
		byte[] bytes = Encoding.UTF8.GetBytes(ConvertToText(_obj, _convertType));
		if (_encrypt) { bytes = Crypter.DefaultEncrypt(bytes); } //暗号化

		//指定ファイルに書き込み >> 出力する
		//ファイルへのアクセスは「書き込み」
		//ここでのアクセスが終了するまで外部でできることは「読み込み」
		/* ◆補足
		 * ファイルの読み書きは、形式によって以下２種を使い分けます。
		 * １）FileStream	= バイト配列
		 * ２）StreamReader = 文字列（読み）
		 * 　　StreamWriter = 文字列（書き）
		 * 
		 * 参考サイト：https://light11.hatenadiary.com/entry/2019/06/29/152002
		 * 
		 * ココでは、暗号化・復号化の為に文字列をバイトに変換して読み書きするので FileStream を使用
		 */
		using (FileStream file = new FileStream(_path, FileMode.Create, FileAccess.Write, FileShare.Read)) { 
			try {
				await file.WriteAsync(bytes, 0, bytes.Length);
				result = EGeneralResult.Success;
				//バックアップを削除する
				if (isExisted) { File.Delete(backUpPath); }
			}
			catch (Exception error) {
				DebugEX.LogError(error);
				//失敗したファイルを閉じて削除する（削除前に閉じておかないとアクセス拒否のエラーが出ます）
				file.Close();
				File.Delete(_path);
				//バックアップから復旧させる
				if (isExisted) { File.Move(backUpPath, _path); }
			}
			finally {
				//最後にコールバック呼び出し
				_callback?.Invoke(result);
			}
		}
	}

	/// <summary>
	/// 指定パスのファイルを非同期で読み込み指定オブジェクトに変換して返す
	/// <para> １）ファイルが存在しない場合は何もしません </para>
	/// </summary>
	/// <typeparam name="T"> 変換先のオブジェクトの型（呼び出し時に省略可能） </typeparam>
	/// <param name="_path"> 読み込み元のパス </param>
	/// <param name="_callback"> 読み込み完了時のコールバック </param>
	/// <param name="_convertType"> 読み込むデータ形式タイプ（デフォルトはJSON） </param>
	/// <param name="_decrpt"> 復号化実施フラグ </param>
	public async UniTask<T> ReadAsync<T>(string _path, Action<EGeneralResult, T> _callback = null, EConvertTextType _convertType = EConvertTextType.JSON, bool _decrpt = true)
	{
		//返り値
		T obj = default;

		EGeneralResult result = EGeneralResult.Failed;

		//ファイルを開く
		if (File.Exists(_path)) {
			using (FileStream file = new FileStream(_path, FileMode.Open)) {
				try {
					//読み込み（同期）
					//byte[] bytes = File.ReadAllBytes(_path);
					//読み込み（非同期）
					//byte[] bytes = await File.ReadAllBytesAsync(_path, new System.Threading.CancellationToken());
					//読み込み（非同期＆バッファーを用意）
					byte[] bytes = new byte[file.Length];
					await file.ReadAsync(bytes, new System.Threading.CancellationToken());
					//復号化
					if (_decrpt) { bytes = Crypter.DefaultDecrypt(bytes); }
					// バイト >> 選択形式の文字列 >> オブジェクトに変換
					obj = ConvertFromText<T>(Encoding.UTF8.GetString(bytes), _convertType);
					result = EGeneralResult.Success;
				}
				catch (Exception error) {
					DebugEX.LogError(error);
				}
			}
		}
		//最後にコールバック呼び出し
		_callback?.Invoke(result, obj);

		return obj;
	}

	#endregion

}

/// <summary>
/// ファイルの存在チェックの結果構造体
/// </summary>
public struct StreamExistCheckResultInfo
{
	/// <summary>
	/// チェックするモード
	/// </summary>
	public enum ECheckMode
	{
		/// <summary>
		/// ファイル
		/// </summary>
		File,

		/// <summary>
		/// ディレクトリ
		/// </summary>
		Directory,
	}

	/// <summary>
	/// チェックの結果
	/// </summary>
	public enum EResult
	{
		//何もしていない（初期状態）
		None,

		/// <summary>
		/// ファイルが存在する
		/// </summary>
		Exist,

		/// <summary>
		/// ディレクトリが存在しない
		/// <para>
		/// 階層途中のディレクトリに欠落があった場合これが返ります
		/// </para>
		/// </summary>
		NotExistDirectory,

		/// <summary>
		/// ファイルが存在しない
		/// </summary>
		NotExistFile,
	}
	public EResult result;

	/// <summary>
	/// 存在しないファイルもしくはディレクトリへのパス
	/// <para> ※目的のファイルが存在する場合、Nullか、空の文字列が返ります </para>
	/// </summary>
	public string notExistPath;
}
