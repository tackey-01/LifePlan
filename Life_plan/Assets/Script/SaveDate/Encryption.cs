using UnityEngine;
using System.Collections;
using System.Security.Cryptography;
using System.Text;

public static class Encryption
{

    //=================================================================================
    //Key暗号化
    //=================================================================================

    private const string PASS = "haaUIRphNOs7aDgCdhT2E8UaOZ40yXeV";

    /// <summary>
    /// 文字列を暗号化する
    /// </summary>
    /// <param name="sourceString">暗号化する文字列</param>
    /// <returns>暗号化された文字列</returns>
    public static string EncryptString(string sourceString)
    {
        //RijndaelManagedオブジェクトを作成
        RijndaelManaged rijndael = new RijndaelManaged();

        //パスワードから共有キーと初期化ベクタを作成
        byte[] key, iv;
        GenerateKeyFromPassword(
        rijndael.KeySize, out key, rijndael.BlockSize, out iv);
        rijndael.Key = key;
        rijndael.IV = iv;

        //文字列をバイト型配列に変換する
        byte[] strBytes = Encoding.UTF8.GetBytes(sourceString);

        //対称暗号化オブジェクトの作成
        ICryptoTransform encryptor = rijndael.CreateEncryptor();

        //バイト型配列を暗号化する
        byte[] encBytes = encryptor.TransformFinalBlock(strBytes, 0, strBytes.Length);

        //閉じる
        encryptor.Dispose();

        //バイト型配列を文字列に変換して返す
        return System.Convert.ToBase64String(encBytes);
    }

    /// <summary>
    /// 暗号化された文字列を復号化する
    /// </summary>
    /// <param name="sourceString">暗号化された文字列</param>
    /// <returns>復号化された文字列</returns>
    public static string DecryptString(string sourceString)
    {
        //RijndaelManagedオブジェクトを作成
        RijndaelManaged rijndael = new RijndaelManaged();

        //パスワードから共有キーと初期化ベクタを作成
        byte[] key, iv;
        GenerateKeyFromPassword(
          rijndael.KeySize, out key, rijndael.BlockSize, out iv);
        rijndael.Key = key;
        rijndael.IV = iv;

        //文字列をバイト型配列に戻す
        byte[] strBytes = System.Convert.FromBase64String(sourceString);

        //対称暗号化オブジェクトの作成
        ICryptoTransform decryptor = rijndael.CreateDecryptor();

        //バイト型配列を復号化する
        //復号化に失敗すると例外CryptographicExceptionが発生
        byte[] decBytes = decryptor.TransformFinalBlock(strBytes, 0, strBytes.Length);

        //閉じる
        decryptor.Dispose();
        Debug.Log("///////////////////////////" + Encoding.UTF8.GetString(decBytes));
        //バイト型配列を文字列に戻して返す
        return Encoding.UTF8.GetString(decBytes);
    }

    /// パスワードから共有キーと初期化ベクタを生成する
    private static void GenerateKeyFromPassword(int keySize, out byte[] key, int blockSize, out byte[] iv)
    {
        //パスワードから共有キーと初期化ベクタを作成する
        //saltを決める
        byte[] salt = Encoding.UTF8.GetBytes("eskiw2e$$P5B");

        //Rfc2898DeriveBytesオブジェクトを作成する
        Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(PASS, salt);

        //反復処理回数を指定する デフォルトで1000回
        deriveBytes.IterationCount = 1000;

        //共有キーと初期化ベクタを生成する
        key = deriveBytes.GetBytes(keySize / 8);
        iv = deriveBytes.GetBytes(blockSize / 8);
    }

}