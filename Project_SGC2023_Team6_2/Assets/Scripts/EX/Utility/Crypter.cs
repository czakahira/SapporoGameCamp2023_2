using System;
using System.Text;					//エンコーディング
using System.Security.Cryptography; //AES使用

/// <summary>
/// 暗号化・ 復号化の実施クラス
/// <para>
/// デフォルトプリセットが反映されたAESを持つ静的インスタンスが存在します <br/>
/// それを介することで都度インスタンスせずとも、暗号化・復号化の機能が利用できます
/// </para>
/// </summary>
public class Crypter : IDisposable
{
	#region Static

	//▼ 本プロジェクトでのAESのデフォルトプリセット ▼
	/* ◆ Using AES-256(CBC) ◆
	 * stringの１文字 = １byte = ８bit
	 * 
	 * １６文字 = 128 bit
	 * ３２文字 = 256 bit
	 * 
	 * */
	/// <summary> 
	/// 暗号化・復号化するブロックのサイズ（bit単位）
	/// </summary>
	/* ※ 注釈
	 * 既定値 及び利用可能な値は 128bit のみです
	 * 公式ドキュメント:https://learn.microsoft.com/ja-jp/dotnet/api/system.security.cryptography.aesmanaged.blocksize?view=net-7.0&viewFallbackFrom=netframework-4.7.2
	 */
	static readonly private int AES_BLOCK_SIZE = 128;
	static readonly private string AES_IV = @"VjbBRSafX5TwMSxL";
	/// <summary>
	/// 暗号化・復号化に使用するキーサイズ
	/// </summary>
	/* ※ 注釈
	 * 既定値 及び最大値は 256bit です
	 * 他に 128bit, 192bit も利用可能なようですが、動作確認はしていません
	 */
	static readonly private int AES_KEY_SIZE = 256;
	static readonly private string AES_KEY = @"qZupvfHmnHUa2gVEEmdfjVX6YF5CFXCs";
	static readonly private CipherMode AES_CIPHER_MODE = CipherMode.CBC;
	static readonly private PaddingMode AES_PADDING_MODE = PaddingMode.PKCS7;

	/// <summary>
	/// Return This Project's Default Settiinged AesManaged
	/// </summary>
	static public AesManaged GetDefaultManaged()
	{
		AesManaged aes = new AesManaged();

		aes.BlockSize = AES_BLOCK_SIZE;
		aes.KeySize = AES_KEY_SIZE;
		aes.Mode = AES_CIPHER_MODE;
		aes.Padding = AES_PADDING_MODE;
		aes.IV = Encoding.UTF8.GetBytes(AES_IV);
		aes.Key = Encoding.UTF8.GetBytes(AES_KEY);

		return aes;
	}

	/// <summary>
	/// デフォルト設定されたAESで暗号化・復号化する静的インスタンス
	/// </summary>
	static readonly private Crypter g_Crypter = new Crypter();
	/// <summary>
	/// デフォルト暗号化
	/// </summary> 
	/// <param name="_bytes"> 暗号化するバイト列 </param>
	/// <returns> 暗号化されたバイト列 </returns>
	static public byte[] DefaultEncrypt(byte[] _bytes)
	{
		return g_Crypter.Encrypt(_bytes);
	}
	/// <summary>
	/// デフォルト復号化
	/// </summary>
	/// <param name="_bytes"> 復号化するバイト列 </param>
	/// <returns> 復号化するバイト列 </returns>
	static public byte[] DefaultDecrypt(byte[] _bytes)
	{
		return g_Crypter.Decrypt(_bytes);
	}

	#endregion

	/// <summary>
	/// 暗号化に使用するマネージド
	/// </summary>
	private AesManaged m_Aes;

	#region Constructor & Destructor
	/// <summary>
	/// デフォルト設定されたマネージドを持つコンストラクタ
	/// </summary>
	public Crypter() { m_Aes = GetDefaultManaged();	}
	/// <summary>
	/// 外部で設定されたマネージドを参照するコンストラクタ
	/// </summary>
	public Crypter(AesManaged _aes) { m_Aes = _aes; }
	~Crypter() { }
	#endregion

	void IDisposable.Dispose()
	{
		//マネージドも破棄する
		m_Aes.Dispose();
	}

	/// <summary>
	/// 指定バイト列を暗号化して返す
	/// </summary>
	public byte[] Encrypt(byte[] _bytes)
	{
		return m_Aes.CreateEncryptor().TransformFinalBlock(_bytes, 0, _bytes.Length);
	}
	public byte[] Encrypt(string _str)
	{	
		return Encrypt(Encoding.UTF8.GetBytes(_str));
	}
	/// <summary>
	/// 指定バイト列を復号化して返す
	/// </summary>
	public byte[] Decrypt(byte[] _bytes)
	{
		return m_Aes.CreateDecryptor().TransformFinalBlock(_bytes, 0, _bytes.Length);
	}
	public void Decrypt(byte[] _bytes, out string _outPut)
	{
		_outPut = Encoding.UTF8.GetString(Decrypt(_bytes));
	}
}
