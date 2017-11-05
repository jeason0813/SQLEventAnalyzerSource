/*
Copyright (C) 2017 Lars Hove Christiansen
http://virtcore.com

This file is a part of SQL Event Analyzer

	SQL Event Analyzer is free software: you can redistribute it and/or modify
	it under the terms of the GNU General Public License as published by
	the Free Software Foundation, either version 3 of the License, or
	(at your option) any later version.

	SQL Event Analyzer is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
	GNU General Public License for more details.

	You should have received a copy of the GNU General Public License
	along with SQL Event Analyzer. If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public static class ConnectionStringSecurity
{
	private const string _saltKey = "85C99CEDFE4D4910";
	private const string _cipherKey = "26F5F8CD-0869-4358-8BA9-94E56C85BCC2";

	public static string EncodeConnectionString(string connectionString, string keyName)
	{
		if (connectionString == "")
		{
			return "";
		}

		SqlConnectionStringBuilder connBuilder = new SqlConnectionStringBuilder(connectionString);

		string regKeyName = string.Format("Password_{0}", keyName);

		if (!connBuilder.IntegratedSecurity)
		{
			string clearTextPassword = connBuilder.Password;
			string ecryptedPassword = Encode(clearTextPassword);
			byte[] protectedPassword = ProtectedData.Protect(Encoding.UTF8.GetBytes(ecryptedPassword), null, DataProtectionScope.LocalMachine);
			RegistryHandler.SaveByte(regKeyName, protectedPassword);
			connBuilder.Password = "";
		}
		else
		{
			RegistryHandler.Delete(regKeyName);
		}

		return connBuilder.ToString();
	}

	public static string DecodeConnectionString(string connectionString, string keyName)
	{
		if (connectionString == "")
		{
			return "";
		}

		SqlConnectionStringBuilder connBuilder = new SqlConnectionStringBuilder(connectionString);

		string regKeyName = string.Format("Password_{0}", keyName);

		if (!connBuilder.IntegratedSecurity)
		{
			byte[] protectedPassword = RegistryHandler.ReadByte(regKeyName);

			if (protectedPassword != null)
			{
				string unprotectedPassword = Encoding.UTF8.GetString(ProtectedData.Unprotect(protectedPassword, null, DataProtectionScope.LocalMachine));
				string clearTextPassword = Decode(unprotectedPassword);
				connBuilder.Password = clearTextPassword;
			}
		}

		return connBuilder.ToString();
	}

	public static string Encode(string plainText)
	{
		if (plainText == "")
		{
			return "";
		}

		byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

		RijndaelManaged symmetricKey = new RijndaelManaged();
		symmetricKey.Mode = CipherMode.CBC;

		byte[] salt = Encoding.ASCII.GetBytes(_saltKey);

		Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(_cipherKey, salt);
		byte[] keyBytes = key.GetBytes(symmetricKey.KeySize / 8);

		ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, salt);
		MemoryStream memoryStream = new MemoryStream();
		CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

		cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
		cryptoStream.FlushFinalBlock();
		byte[] cipherTextBytes = memoryStream.ToArray();

		memoryStream.Close();
		cryptoStream.Close();

		return Convert.ToBase64String(cipherTextBytes);
	}

	public static string Decode(string cipherText)
	{
		if (cipherText == "")
		{
			return "";
		}

		byte[] cipherTextBytes = Convert.FromBase64String(cipherText);

		RijndaelManaged symmetricKey = new RijndaelManaged();
		symmetricKey.Mode = CipherMode.CBC;

		byte[] salt = Encoding.ASCII.GetBytes(_saltKey);

		Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(_cipherKey, salt);
		byte[] keyBytes = key.GetBytes(symmetricKey.KeySize / 8);

		ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, salt);
		MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
		CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

		byte[] plainTextBytes = new byte[cipherTextBytes.Length];
		int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

		memoryStream.Close();
		cryptoStream.Close();

		return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
	}
}
