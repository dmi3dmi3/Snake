// Статичный класс для создания случайных чисел
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Game_Snake.Classes
{
    static class MyRandom
    {
		//Статичный метод, возвращающий целое значение в диапазоне от 0 до а-1
        static public int RandomInt(int a)
        {
            RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
            byte[] randomNumber = new byte[1];
            new RNGCryptoServiceProvider().GetBytes(randomNumber);
            return randomNumber[0] % a;

        }
    }
}
