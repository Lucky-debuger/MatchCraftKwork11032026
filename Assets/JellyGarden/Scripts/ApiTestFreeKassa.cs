using UnityEngine;
using System.Security.Cryptography;
using System.Text;
using System.Globalization;
using TMPro;

public class SimpleFreeKassa : MonoBehaviour
{
    private string merchantId = "69475";
    private string secretWord = "0*XWe/fpd=s_D@t"; // Убедитесь, что это Секретное слово 1
    private string currency = "RUB";

    [SerializeField] private TMP_InputField inputFieldPlayerId;

    public void BuyCoins()
    {
        string playerId = inputFieldPlayerId.text;
        Debug.Log($"Player ID: {playerId}");
        float amount = 10f;
        string orderId = System.Guid.NewGuid().ToString();

        // Форматируем сумму с точкой
        string amountStr = amount.ToString(CultureInfo.InvariantCulture);

        // Строка для подписи
        string signatureString = $"{merchantId}:{amountStr}:{secretWord}:{currency}:{orderId}";
        Debug.Log("Строка для подписи: " + signatureString);

        string signature = Md5Hash(signatureString);
        Debug.Log("Вычисленная подпись: " + signature);

        // Ссылка
        string url = $"https://pay.fk.money/?m={merchantId}&oa={amountStr}&currency={currency}&o={orderId}&s={signature}&us_login={playerId}";
        Debug.Log("Полная ссылка: " + url);

        Application.OpenURL(url);
    }

    private string Md5Hash(string input)
    {
        using (MD5 md5 = MD5.Create())
        {
            byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
                sb.Append(hash[i].ToString("x2"));
            return sb.ToString();
        }
    }
}