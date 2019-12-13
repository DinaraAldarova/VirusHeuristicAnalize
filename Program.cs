 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace VirusHeuristicAnalize
{
    class Program
    {
        static void Main(string[] args)
        {
            string dirName = "C:\\Users\\dinar\\Desktop\\WORK";

            if (Directory.Exists(dirName))
            {
                string[] files = Directory.GetFiles(dirName);
                foreach (string path in files)
                {
                    File file1 = new File(path);
                    Console.WriteLine("{0} {1}", path, file1.IsInfected() ? "ЗАРАЖЕН" : "не заражен");
                }
                Console.ReadLine();
            }

            //C: \Users\dinar\Desktop\WORK\BAD.ASM не заражен
            //C:\Users\dinar\Desktop\WORK\BAD.COM ЗАРАЖЕН           //первый зараженный COM
            //C: \Users\dinar\Desktop\WORK\BAD.LST не заражен
            //C:\Users\dinar\Desktop\WORK\BAD.OBJ не заражен
            //C:\Users\dinar\Desktop\WORK\DN.BAT не заражен
            //C:\Users\dinar\Desktop\WORK\EXEPROC.exe ЗАРАЖЕН       //работа коллеги
            //C: \Users\dinar\Desktop\WORK\GOOD.ASM не заражен
            //C:\Users\dinar\Desktop\WORK\GOOD.COM не заражен
            //C:\Users\dinar\Desktop\WORK\GOOD.EXE не заражен
            //C:\Users\dinar\Desktop\WORK\GOOD.LST не заражен
            //C:\Users\dinar\Desktop\WORK\GOOD.OBJ не заражен
            //C:\Users\dinar\Desktop\WORK\GOOD_PA.BAK не заражен
            //C:\Users\dinar\Desktop\WORK\GOOD_PA.EXE не заражен
            //C:\Users\dinar\Desktop\WORK\GOOD_PA.PAS не заражен
            //C:\Users\dinar\Desktop\WORK\MARAWI.COM ЗАРАЖЕН        //исследуемый вирус
            //C: \Users\dinar\Desktop\WORK\sick.ASM не заражен
            //C:\Users\dinar\Desktop\WORK\sick.COM ЗАРАЖЕН          //переписанный зараженный COM
            //C: \Users\dinar\Desktop\WORK\sick.EXE ЗАРАЖЕН         //работающий зараженный EXE
            //C: \Users\dinar\Desktop\WORK\sick.LST не заражен
            //C:\Users\dinar\Desktop\WORK\sick.OBJ не заражен
            //C:\Users\dinar\Desktop\WORK\sick_old.EXE ЗАРАЖЕН      //первая версия зараженного EXE
            //C: \Users\dinar\Desktop\WORK\TASM.BAT не заражен
            //C:\Users\dinar\Desktop\WORK\TD.BAT не заражен
            //C:\Users\dinar\Desktop\WORK\UHEX.BAT не заражен
        }
    }
}
