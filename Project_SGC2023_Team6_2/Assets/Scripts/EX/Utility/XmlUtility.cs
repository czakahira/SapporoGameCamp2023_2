using System.IO;				//ストリーム
using System.Text;				//エンコーディング
using System.Xml.Serialization; //XML

/// <summary>
/// XMLの便利クラス
/// </summary>
static public class XmlUtility
{
	/// <summary>
	/// オブジェクトをXML文字列に変換します
	/// </summary>
	/// <param name="_obj"> 変換元のオブジェクト </param>
	static public string ToXml(object _obj)
	{
		using (MemoryStream stream = new MemoryStream()) {
			//メモリ上で、オブジェクトをXMLにシリアライズしバイトに変換する
			XmlSerializer serializer = new XmlSerializer(_obj.GetType());
			serializer.Serialize(stream, _obj);

			//バイト列を、XML文字列（UTF-8）に変換して返す
			return Encoding.UTF8.GetString(stream.ToArray());
		}
	}

	/// <summary>
	/// XML文字列をオブジェクトに変換します
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="_xml"> 変換元のXML文字列 </param>
	static public T FromXml<T>(string _xml)
	{
		//XML文字列(UTF-8)を、バイト列に変換
		byte[] bytes = Encoding.UTF8.GetBytes(_xml);

		using (MemoryStream stream = new MemoryStream(bytes)) {
			//メモリ上で、バイト列をXMLにデシリアライズし、オブジェクトに変換して返す
			XmlSerializer serializer = new XmlSerializer(typeof(T));
			return (T)serializer.Deserialize(stream);
		}
	}

}
