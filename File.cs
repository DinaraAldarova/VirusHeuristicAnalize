using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace VirusHeuristicAnalize
{
    class File
    {
        private string[] keywords = {"Kahambugan", "Virus", "Warning", "Dangerous"};
        //Kahambugan - имя преподавателя, которого студенты благодарят за появление вируса MAWARI
        //Поэтому данная строка сильнее характеризует файл как зараженный, чем просто Virus
        //private byte[] commands = {0x0e, 0x1f};
        private byte command_jmp = 0xe8;

        string path = "";
        byte[] byte_buf;
        P[] probabilites = new P[10];

        public File(string path_str)
        {
            path = path_str;

            var stream = new FileStream(path, FileMode.Open);
            var reader = new BinaryReader(stream);
            byte_buf = reader.ReadBytes((int)stream.Length);
            
            reader.Close();
            stream.Close();
        }

        public bool IsInfected()
        {
            //Первая ступень анализатора. 
            //Из-за небольшого числа посылок и того факта, 
            //что основной файл MAWARI вирусом не является, 
            //распределение весов очень простое

            probabilites[0] = contain_keyword(0.8);             //Должен сработать MAWARI (других признаков вируса у него нет, это же не вирус) и другие вирусы (но на 0.5 * weight)
            probabilites[1] = contain_first_command_jmp(0.5);   //EXE- (если не переставил IP) и COM-вирусы сработают
            probabilites[2] = contain_not_round_IP(0.4);        //EXE-вирус может использовать не jmp, а другое значение IP
            
            P result = P.SumArray(probabilites);
            if (result.infected >= 0.8)
                return true;
            else
                return false;
        }

        //public string TypeFile()
        //{
        //    //Вторая ступень анализатора. 
        //    //MZ определит, но если этих букв в начале нет, это может быть и не COM-файл
        //    P prob = contain_MZ(1);
        //    if (prob.infected == 1.0)
        //        return "EXE-файл";
        //    else
        //        return "undefined";
        //}

        private P contain_keyword (double weight)
        {
            //Если содержатся разные ключевые слова, выбирается только одно самое серьезное
            P prob;
            if (FindSubarray(byte_buf, StringToByteArray(keywords[0])) > -1 ||
                FindSubarray(byte_buf, StringToByteArray(keywords[0].ToUpper())) > -1 ||
                FindSubarray(byte_buf, StringToByteArray(keywords[0].ToLower())) > -1 )
            {
                prob = new P(1.0, 0.0, weight);
            }
            else
                if (FindSubarray(byte_buf, StringToByteArray(keywords[1])) > -1 ||
                FindSubarray(byte_buf, StringToByteArray(keywords[1].ToUpper())) > -1 ||
                FindSubarray(byte_buf, StringToByteArray(keywords[1].ToLower())) > -1)
            {
                prob = new P(0.5, 0.5, weight);
            }
            else
                if (FindSubarray(byte_buf, StringToByteArray(keywords[2])) > -1 ||
                FindSubarray(byte_buf, StringToByteArray(keywords[2].ToUpper())) > -1 ||
                FindSubarray(byte_buf, StringToByteArray(keywords[2].ToLower())) > -1)
            {
                prob = new P(0.2, 0.8, weight);
            }
            else
                if (FindSubarray(byte_buf, StringToByteArray(keywords[3])) > -1 ||
                FindSubarray(byte_buf, StringToByteArray(keywords[3].ToUpper())) > -1 ||
                FindSubarray(byte_buf, StringToByteArray(keywords[3].ToLower())) > -1)
            {
                prob = new P(0.2, 0.8, weight);
            }
            else
                prob = new P();
            return prob;
        }

        private P contain_first_command_jmp(double weight)
        {
            P prob;
            int start_index = 0;
            if (byte_buf[0x00] == 'M' && byte_buf[0x01] == 'Z')
            {
                //Вычислили длину заголовка в параграфах
                start_index = byte_buf[0x09] * 0x100 + byte_buf[0x08];
                //Вычислили длину заголовка в байтах
                start_index *= 0x10;
                //Вычислили смещение IP от начала секции кода
                int offset_IP = byte_buf[0x15] * 0x100 + byte_buf[0x14];
                //Вычислили положение первой команды
                start_index += offset_IP;
            }

            if (byte_buf[start_index] == command_jmp)
            {
                prob = new P(1.0, 0.0, weight);
            }
            else
                prob = new P();
            return prob;
        }

        private P contain_not_round_IP(double weight)
        {
            P prob;
            if (byte_buf[0x00] == 'M' && byte_buf[0x01] == 'Z' && byte_buf[0x14] > 0)
                //сократила выражение IP % 0x100 > 0
                //(byte_buf[0x15] * 0x100 + byte_buf[0x14]) % 0x100 > 0
            {
                prob = new P(1.0, 0.0, weight);
            }
            else
                prob = new P();
            return prob;
        }

        //private P contain_MZ(double weight)
        //{
        //    P prob;
        //    if (byte_buf[0x00] == 'M' && byte_buf[0x01] == 'Z')
        //    {
        //        prob = new P(1.0, 0.0, weight);
        //    }
        //    else
        //        prob = new P(0.0, 1.0, weight);
        //    return prob;
        //}

        //private P contain_command()
        //{
        //    //Из разных последовательностей выберется самая серьезная
        //    //Поэтому последовательности лучше разбить на категории
        //    P prob;
        //    if (FindSubarray(byte_buf, commands[0]) > -1)
        //    {
        //        prob = new P(0.2, 0.0, 1);
        //    }
        //    else if (FindSubarray(byte_buf, commands[1]) > -1)
        //    {
        //        prob = new P(0.2, 0.0, 1);
        //    }
        //    else
        //        prob = new P();
        //    return prob;
        //}

        private byte[] StringToByteArray (string str)
        {
            byte[] arr = new byte[str.Length];
            for (int i = 0; i < str.Length; i++)
                arr[i] = Convert.ToByte(str[i]);
            return arr;
        }

        private static int FindSubarray(byte[] array, byte[] subArray, int startIndex = 0)
        {
            int index = -1;
            for (int i = startIndex; i < array.Length - subArray.Length + 1; i++)
            {
                index = i;
                for (int j = 0; j < subArray.Length; j++)
                {
                    if (array[i + j] != subArray[j])
                    {
                        index = -1;
                        break;
                    }
                }
                if (index >= 0)
                    return index;
            }
            return -1;
        }
        private static int FindSubarray(byte[] array, byte subArray, int startIndex = 0)
        {
            int index = -1;
            for (int i = startIndex; i < array.Length; i++)
            {
                if (array[i] == subArray)
                {
                    index = i;
                    return index;
                }
            }
            return -1;
        }
    }
}
