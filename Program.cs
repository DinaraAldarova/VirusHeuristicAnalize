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
                    File file = new File(path);
                    Console.WriteLine("{0,-45} {1,-15} {2,-10}", path, file.IsInfected() ? "ЗАРАЖЕН" : "не заражен", file.TypeFile());
                }
                Console.ReadLine();
            }

            //C: \Users\dinar\Desktop\WORK\BAD.ASM      не заражен      undefined
            //C: \Users\dinar\Desktop\WORK\BAD.COM      ЗАРАЖЕН         undefined       //первый зараженный COM
            //C: \Users\dinar\Desktop\WORK\BAD.LST      не заражен      undefined
            //C: \Users\dinar\Desktop\WORK\BAD.OBJ      не заражен      undefined
            //C: \Users\dinar\Desktop\WORK\DN.BAT       не заражен      undefined
            //C: \Users\dinar\Desktop\WORK\EXEPROC.exe  ЗАРАЖЕН         EXE - файл      //работа коллеги
            //C: \Users\dinar\Desktop\WORK\GOOD.ASM     не заражен      undefined
            //C: \Users\dinar\Desktop\WORK\GOOD.COM     не заражен      undefined
            //C: \Users\dinar\Desktop\WORK\GOOD.EXE     не заражен      EXE-файл
            //C: \Users\dinar\Desktop\WORK\GOOD.LST     не заражен      undefined
            //C: \Users\dinar\Desktop\WORK\GOOD.OBJ     не заражен      undefined
            //C: \Users\dinar\Desktop\WORK\GOOD_PA.BAK  не заражен      undefined
            //C: \Users\dinar\Desktop\WORK\GOOD_PA.EXE  не заражен      EXE-файл
            //C: \Users\dinar\Desktop\WORK\GOOD_PA.PAS  не заражен      undefined
            //C: \Users\dinar\Desktop\WORK\MARAWI.COM   ЗАРАЖЕН         undefined       //исследуемый вирус
            //C: \Users\dinar\Desktop\WORK\sick.ASM     не заражен      undefined
            //C: \Users\dinar\Desktop\WORK\sick.COM     ЗАРАЖЕН         undefined       //переписанный зараженный COM
            //C: \Users\dinar\Desktop\WORK\sick.EXE     ЗАРАЖЕН         EXE - файл      //работающий зараженный EXE
            //C: \Users\dinar\Desktop\WORK\sick.LST     не заражен      undefined
            //C: \Users\dinar\Desktop\WORK\sick.OBJ     не заражен      undefined
            //C: \Users\dinar\Desktop\WORK\sick_old.EXE ЗАРАЖЕН         EXE - файл      //первая версия зараженного EXE
            //C: \Users\dinar\Desktop\WORK\TASM.BAT     не заражен      undefined
            //C: \Users\dinar\Desktop\WORK\TD.BAT       не заражен      undefined
            //C: \Users\dinar\Desktop\WORK\UHEX.BAT     не заражен      undefined
        }
    }
}
