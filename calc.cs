using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Calculator
{
    class mainMain
    {
        static string Chis;
        static string Brackets;
        static void HowToUse()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t\t\tКАЛЬКУЛЯТОР");
            Console.WriteLine("Пример работы: введите выражение (2+2)*2-2/2 и нажмите Enter");
        }
        static bool NOP(int j)
        {
            return Brackets[j] != '+' && Brackets[j] != '-' && Brackets[j] != '*' && Brackets[j] != '/';
        }
        static double GLOP(int i)
        {
            string LOP = "";
            for (int j = i - 1; j >= 0; j--)
                if (NOP(j))
                    LOP = Brackets[j] + LOP;
                else
                    break;
            return double.Parse(LOP);
        }
        static double GROP(int i)
        {
            string ROP = "";
            for (int j = i + 1; j < Brackets.Length; j++)
                if (NOP(j))
                    ROP += Brackets[j];
                else
                    break;
            return double.Parse(ROP);
        }
        static void ReplaceChis(int i, double ToThis)
        {
            int FromI = 0;
            int ToI = Brackets.Length - 1;
            for (int j = i - 1; j >= 0; j--)
                if (NOP(j))
                    FromI = j;
                else
                    break;
            for (int j = i + 1; j < Brackets.Length; j++)
                if (NOP(j))
                    ToI = j;
                else
                    break;
            Brackets = Brackets.Replace(Brackets.Substring(FromI, ToI - FromI + 1), ToThis.ToString());
        }
        static void repA(int i)
        {
            double A;
            if (Brackets[i] == '*')
                A = GLOP(i) * GROP(i);
            else
                A = GLOP(i) / GROP(i);
            ReplaceChis(i, A);
            Calc();
        }
        static void repB(int i)
        {
            double B;
            if (Brackets[i] == '+')
                B = GLOP(i) + GROP(i);
            else
                B = GLOP(i) - GROP(i);
            ReplaceChis(i, B);
            Calc();
        }
        static void Calc()
        {
            int i;
            for (i = 0; i < Brackets.Length; i++)
                if (Brackets[i] == '*' || Brackets[i] == '/')
                {
                    repA(i);
                    return;
                }
            for (i = 0; i < Brackets.Length; i++)
                if (Brackets[i] == '+' || Brackets[i] == '-')
                {
                    repB(i);
                    return;
                }
        }
        static bool FindBrackets(out int o)
        {
            o = 0;
            if (Chis.IndexOf('(') != -1)
            {
                int ClosedBracket = Chis.IndexOf(')');
                int OpenBracket = 0;
                for (int i = ClosedBracket - 1; i >= 0; i--)
                    if (Chis[i] == '(')
                    {
                        OpenBracket = i;
                        Brackets = Chis.Substring(OpenBracket + 1, ClosedBracket - OpenBracket - 1);
                        o = OpenBracket;
                        Chis = Chis.Remove(OpenBracket, ClosedBracket - OpenBracket + 1);
                        break;
                    }
                return true;
            }
            return false;
        }        
        static void Main()
        {
            HowToUse();
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\nВведите выражение: ");
            Chis = '(' + Console.ReadLine().Replace(" ", "") + ')';
            int o;
            while (FindBrackets(out o))
            {
                Calc();
                Chis = Chis.Insert(o, Brackets);
            }
            Console.WriteLine("Ответ: " + Chis);
            Console.ReadKey();
        }
    }
}

