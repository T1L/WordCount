using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WordCounts
{   
    public class Program
    {
        public static Program Me = new Program();
        List<Word> Words = new List<Word>();//存放单词
        List<Word> Phrase = new List<Word>();//存放指定长度的单词
        List<string> Lines = new List<string>();     
        List<string> Str = new List<string>();
        public int lines = 0;
        public int characters = 0;
        public static int number;
        public static int Num;
        public static string path1;
        public static string path2;
        static void Main(string[] args)
        {          
            Console.WriteLine("输入读取文本路径：");
            path1 = Console.ReadLine();
            Console.WriteLine("输入想要查找的词组长度：");
            number = int.Parse(Console.ReadLine());
            Console.WriteLine("想要输出的单词数量：");
            Num = int.Parse(Console.ReadLine());
            StreamReader a = new StreamReader(path1, Encoding.Default);
            string line;
            while ((line = a.ReadLine()) != null)
            {
                Me.Lines.Add(line);//一行一行读入
            }
            Me.WordsCount();
            Me.Print1();//输出字符 单词 行的数量
            Me.Print2();//输出指定长度的单词及次数
            path2 = "C:\\Users\\RAIse\\Desktop\\Result.txt";
            System.IO.File.WriteAllLines(path2, Me.Str);
            Console.ReadKey();
        } 
        /// <summary>
        /// 输出字符 单词 行的数量
        /// </summary>
        public void Print1()
        {
           
            Console.WriteLine("字符个数:     " + Me.characters + "个");
            Str.Add("字符个数:" + Me.characters);
            Console.WriteLine("单词:     " + Me.Words.Count+"个");
            Str.Add("单词个数: " + Me.Words.Count);
            Console.WriteLine("行数:     " + Me.lines + "行");
            Str.Add("行:" + Me.lines+"行");
        }
        /// <summary>
        /// 输出查找的指定长度的单词的数量
        /// </summary>
       public void Print2()
        {
           
            List<string> words = Me.WordSort();
            int i = 0;
            if (Words.Count < Num)
            {
                for (; i < Words.Count; i++)
                {
                    foreach (var a in Words)
                    {
                        if (words[i] == a.txt)
                        {
                            Console.WriteLine(a.txt + "     " + a.num+"次");
                            Str.Add(a.txt + "   " + a.num + "次");
                            break;
                        }
                    }
                }
            }
            else
            {
                for (; i < Num; i++)
                {
                    foreach (var a in Me.Words)
                    {
                        if (words[i] == a.txt)
                        {
                            Console.WriteLine(a.txt + "     " + a.num+"次");
                            Str.Add(a.txt + "    " + a.num + "次");
                            break;
                        }
                    }
                }
            }
            foreach (var word in Phrase)
            {
                Console.WriteLine(word.txt + " " + word.num+"次");
                Str.Add(word.txt + "   " + word.num + "次");
            }
        }             
        public void WordsCount()
        {

            int words1 = 0;
            int words2 = 0;
            int Phrases = 0;
            bool isWord = true;
            string article = "";
            foreach (var str in Lines)
            {
                int IsLine = 0;
                foreach (var word in str)
                {
                    if (word != ' ')
                    {
                        IsLine++;
                        characters++;
                        if ((isWord == true) && (((word >= 65) && (word <= 90)) || ((word >= 97) && (word <= 122))))
                        {
                            words1++;
                            article = article + word;
                        }
                        else
                        {
                            if (words1 == 0)
                            {
                                isWord = false;
                                words2++;
                            }
                            else
                            {
                                if ((words1 >= 4) && ((word >= 48) && (word <= 57)))
                                {
                                    article = article + word;
                                }
                                else
                                {
                                    if (words1 >= 4)
                                    {
                                        WordAdd(article);
                                        words1 = 0;
                                        article = "";
                                    }
                                    else
                                    {
                                        words1 = 0;
                                        article = "";
                                        isWord = true;
                                        words2 = 0;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        Phrases++;
                        isWord = true;
                        words2 = 0;
                        if (words1 >= 4)
                        {
                            WordAdd(article);
                            words1 = 0;
                            article = "";
                        }
                        else
                        {
                            words2++;
                            words1 = 0;
                            article = "";
                            Phrases = 0;
                        }
                        if ((words2 == 0) && (Phrases == number))
                        {
                            PhraseAdd();
                            Phrases = 0;
                        }
                    }
                }
                if (words1 >= 4)
                {
                    WordAdd(article);
                    words1 = 0;
                    article = "";
                    if ((words2 == 0) && (Phrases == number - 1))
                    {
                        PhraseAdd();
                        Phrases = 0;
                    }
                }
                if (IsLine != 0)
                {
                    lines++;
                }
            }
        }
        void WordAdd(string article)
        {
            int flag = 0;
            foreach (var word in Words)
            {
                if (word.txt == article)
                {
                    word.num++;
                    flag++;
                    break;
                }
            }
            if (flag == 0)
            {
                Word a = new Word(article);
                Words.Add(a);
            }
        }
        void PhraseAdd()
        {
            int flag = 0;
            int i = 0;
            int j = Words.Count;
            string text = "";
            for (; i < number; i++)
            {
                text = text + Words[j - number + i].txt + " ";
            }
            foreach (var word in Phrase)
            {
                if (word.txt == text)
                {
                    word.num++;
                    flag++;
                    break;
                }
            }
            if (flag == 0)
            {
                Word mutiWord = new Word(text);
                Phrase.Add(mutiWord);
            }
        }
        List<string> WordSort()
        {
            List<Word> Aword
                = new List<Word>();
            int i = 0;
            for (; i < Words.Count - 1; i++)
            {
                int k = 0;
                for (; k < Words.Count - 1; k++)
                {
                    if (Words[i].num > Words[k].num)
                    {
                        Word m = Words[i];
                        Words[i] = Words[k];
                        Words[k] = m;
                    }
                }
            }
            i = 0;
            if (Words.Count < Num)
            {
                for (; i < Words.Count; i++)
                {
                    Aword.Add(Words[i]);
                }
            }
            else
            {
                for (; i < Num; i++)
                {
                    Aword.Add(Words[i]);
                }
            }
            List<string> words = new List<string>();
            foreach (var word in Aword)
            {
                words.Add(word.txt);
            }
            words.Sort();
            return words;
        }               
    }
    class Word
    {
        public string txt;
        public int num;
        public Word(string txt)
        {
            this.txt = txt;
            num = 1;
        }
    }
}