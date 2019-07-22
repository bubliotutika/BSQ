using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bsq
{
    class AlgoBsq
    {
        static void Main(string[] args)
        {
            AlgoBsq Bsq = new AlgoBsq();
            Bsq.init();
        }

        private long time;
        private string result;
        private int maxValue = 0;
        private int bsqY;
        private int bsqX;
        private string mapStr;
        private List<List<int>> map = new List<List<int>>();

        public void init()
        {
            this.promptFile();

            var watchBsq = System.Diagnostics.Stopwatch.StartNew();

            this.parseMapStr();
            this.Bsq();
            this.printMap();

            watchBsq.Stop();
            this.time = watchBsq.ElapsedMilliseconds;

            //Console.WriteLine("Exec time: " + this.time + " ms");
            Console.Read();
        }

        public void promptFile()
        {
            Console.Write("File path: ");
            string filePath = Console.ReadLine();
            string[] mapArr = File.ReadAllLines(filePath);
            this.mapStr = String.Join("\n", mapArr);
        }

        public void parseMapStr()
        {
            int row = 0;
            bool start = false;
            this.map.Add(new List<int>());

            foreach (char str in this.mapStr)
            {
                if (start)
                {
                    if (str == '.')
                        this.map[row].Add(1);
                    if (str == 'o')
                        this.map[row].Add(0);
                    if (str == '\n')
                    {
                        ++row;
                        this.map.Add(new List<int>());
                    }
                }
                else
                {
                    if (str == '\n')
                    {
                        start = true;
                    }
                }
            }
        }

        public void printMap()
        {
            int Y = 0;
            int X = 0;
            string resultRow;
            List<string> resultArray = new List<string>();

            while (Y < this.map.Count)
            {
                X = 0;
                resultRow = "";
                while (X < this.map[Y].Count)
                {
                    if (Y >= this.bsqY && Y <= this.bsqY + this.maxValue - 1 && X >= this.bsqX && X <= this.bsqX + this.maxValue - 1 && this.maxValue != 0)
                        resultRow += "x";
                    else if (this.map[Y][X] == 0)
                        resultRow += "o";
                    else if (this.maxValue == 0 && this.map[Y][X] == 1)
                    {
                        resultRow += "x";
                        this.maxValue = -1;
                    }
                    else
                        resultRow += ".";
                    ++X;
                }
                resultArray.Add(resultRow);
                ++Y;
            }
            this.result = String.Join("\n", resultArray);
            Console.Write(this.result + "\n\n");
        }

        public int getMin(int a, int b, int c)
        {
            int min = Int32.MaxValue;

            if (a < min)
                min = a;
            if (b < min)
                min = b;
            if (c < min)
                min = c;

            return min;
        }

        public void Bsq()
        {
            int Y = 1;
            int X = 1;

            while (Y < this.map.Count)
            {
                X = 1;
                while (X < this.map[Y].Count)
                {
                    if (this.map[Y][X] != 0)
                        if (this.map[Y - 1][X] > 0 && this.map[Y][X - 1] > 0 && this.map[Y - 1][X - 1] > 0)
                            this.map[Y][X] = this.getMin(this.map[Y - 1][X], this.map[Y][X - 1], this.map[Y - 1][X - 1]) + 1;

                    if (this.map[Y][X] > this.maxValue)
                    {
                        this.maxValue = this.map[Y][X];
                        this.bsqY = Y - maxValue + 1;
                        this.bsqX = X - maxValue + 1;
                    }
                    ++X;
                }
                ++Y;
            }
        }
    }
}