using System;

namespace Forca
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Forca forca = new Forca();
            while(true)
            {
                IO.clear();
                IO.printl("1 - Jogar.");
                IO.printl("2 - Sair.");
                int entrada = IO.inputInt();
                if(entrada==1)
                {
                    forca.start();
                }
                else if(entrada==2)
                {
                    IO.printl("Tem certeza? [1-Sim|2-Não]");
                    while(true)
                    {
                        entrada = IO.inputInt();
                        if(entrada==1||entrada==2)
                        {
                            break;
                        } 
                        else
                        {
                            IO.printl("Entrada inválida, tente novamente: ");
                        }
                    }
                    if(entrada == 1)
                    {
                        break;
                    }
                }
                else
                {
                    IO.printl("Entrada inválida, tente novamente! ");
                    System.Console.ReadKey(true);
                }
            }
        }
    }

    static class IO
    {
        public static void printl(string arg)
        {
            System.Console.WriteLine(arg);
        }

        public static void print(char arg)
        {
            System.Console.Write(arg);
        }

        public static string inputStr()
        {
            string str;
            while(true)
            {
                str = System.Console.ReadLine();
                if(str.Length!=0){
                    break;
                }
                clear();
                printl("Entrada inválida!");
                printl("Tente Novamente:");
            }
            return str;
        }
        
        public static int inputInt()
        {
            while(true)
            {
                try
                {
                    string str = inputStr();
                    int res;
                    res = Int16.Parse(str);
                    return Int16.Parse(str);
                }
                catch (System.FormatException e)
                {
                    IO.printl($"Erro de Entrada: {e.GetType().Name}");
                    IO.printl("Por favor digite um número!");
                }
            }
            
        }

        public static void clear()
        {
            System.Console.Clear();
        }
    }

    class Forca
    {
        private char[,] background;
        private string letrasTentadas, secreta;
        private char[] amostra;
        bool ganhou;
        private int tentativas, acertos, pontuacao, palavrasAcertadas;
        private string[] words;
        public Forca()
        {
            string str;
            try
            {
                str = System.IO.File.ReadAllText(@"Background.txt");
                this.background = new char[14,40];
                for (int i = 0; i < 14; i++)
                {
                    for (int j = 0; j < 40; j++)
                    {
                        this.background[i,j] = str[i*40+j];
                    }
                }
                this.words = new String[5] {"Bola","Primavera","Carro","Limousine","Fiscal"};
                clearGame();
                reset();
            }
            catch(System.IO.IOException e)
            {
                IO.printl("O programa não pode ler o arquivo especificado: " + e.GetType().Name);
                System.Console.ReadKey(true);
            }
        }

        private void clearGame()
        {
            this.palavrasAcertadas = 0;
            this.pontuacao = 0;
        }

        private void reset()
        {
            this.ganhou = false;
            this.letrasTentadas = "";
            this.tentativas = 7;
            this.acertos = 0;
            this.secreta = words[palavrasAcertadas]; 
            this.amostra = new char[this.secreta.Length*2];
            for (int i = 0; i < secreta.Length*2; i++)
            {
                if(i%2==0)
                {
                    this.amostra[i] = '_';
                }
                else
                {
                    this.amostra[i] = ' ';
                }
            }
        }

        public void start()
        {
            bool jogando = true;
            while(jogando)
            {
                this.showBackground();
                this.hipotese();
                if(ganhou)
                {
                    this.showBackground();
                    System.Console.ReadKey(true);
                    IO.clear();
                    palavrasAcertadas++;
                    if(palavrasAcertadas==5)
                    {
                        IO.printl($"Parabéns! Você acertou todas as palavras!");
                        jogando = false;
                        clearGame();
                        reset();
                    }
                    else
                    {
                        IO.printl($"Você acertou a palavra {secreta}!");
                        IO.printl("Deseja continuar Jogando? [1-Sim|2-Não]");
                        int entrada;
                        while(true)
                        {
                            entrada = IO.inputInt();
                            if(entrada==1||entrada==2)
                            {
                                break;
                            } 
                            else
                            {
                                IO.printl("Entrada inválida, tente novamente: ");
                            }
                        }
                        if(entrada==1){
                            reset();
                        }
                        else
                        {
                            clearGame();
                            reset();
                            jogando = false;
                        }
                    }
                }
                else if(tentativas==0)
                {
                    this.showBackground();
                    System.Console.ReadKey(true);
                    IO.clear();
                    IO.printl($"Game Over! {Environment.NewLine}Pontuação:{pontuacao}");
                    IO.printl("Deseja continuar Jogando? [1-Sim|2-Não]");
                    int entrada;
                        while(true)
                        {
                            entrada = IO.inputInt();
                            if(entrada==1||entrada==2)
                            {
                                break;
                            } 
                            else
                            {
                                IO.printl("Entrada inválida, tente novamente: ");
                            }
                        }
                    if(entrada==1){
                        clearGame();
                        reset();
                    }
                    else
                    {
                        clearGame();
                        reset();
                        jogando = false;
                    }
                }
            }
            
            IO.printl("Pressione uma tecla para continuar...");
            System.Console.ReadKey(true);
            IO.clear();
        }

        public void showBackground()
        {
            IO.clear();
            for (int i = 0; i < 14; i++)
            {
                for (int j = 0; j < 40; j++)
                {
                    IO.print(this.background[i,j]);
                }
                IO.printl("");
            }
            System.Console.SetCursorPosition(18,9);
            IO.printl($"{new String(amostra)}");
            System.Console.SetCursorPosition(1,0);
            System.Console.Write($"Pontuação: {this.pontuacao}");
            System.Console.SetCursorPosition(1,1);
            System.Console.Write($"Palavras Acertadas: {this.palavrasAcertadas}");

            if(tentativas<7)
            {
                System.Console.SetCursorPosition(23,3);
                System.Console.Write("│");
            }
            if(tentativas<6)
            {
                System.Console.SetCursorPosition(23,4);
                System.Console.Write("o");
            }
            if(tentativas<5)
            {
                System.Console.SetCursorPosition(23,5);
                System.Console.Write("│");
            }
            if(tentativas<4)
            {
                System.Console.SetCursorPosition(22,5);
                System.Console.Write("/");
            }
            if(tentativas<3)
            {
                System.Console.SetCursorPosition(24,5);
                System.Console.Write("\\");
            }
            if(tentativas<2)
            {
                System.Console.SetCursorPosition(22,6);
                System.Console.Write("/");
            }
            if(tentativas<1)
            {
                System.Console.SetCursorPosition(24,6);
                System.Console.Write("\\");
            }
        }
        public void hipotese(){
            bool erro = true;
            System.Console.SetCursorPosition(1,11);
            System.Console.Write($"{this.letrasTentadas}");
            System.Console.SetCursorPosition(18,10);
            System.Console.Write("Hipotese: ");
            string hipotese = IO.inputStr();
            if(hipotese.Length==1)
            {
                char c = hipotese[0];
                foreach(char ch in letrasTentadas)
                {
                    if(ch == c){
                        IO.clear();
                        IO.printl("Letra já utilizada!");
                        System.Console.ReadKey(true);
                        return;
                    }
                }
                for (int i = 0; i < secreta.Length; i++)
                {
                    if(Char.ToLower(secreta[i])==Char.ToLower(c))
                    {
                        amostra[i*2] = i==0?Char.ToUpper(c):Char.ToLower(c);
                        erro = false;
                        acertos++;
                        if(acertos==secreta.Length)
                        {
                            ganhou = true;
                        }
                    }
                }
                letrasTentadas+=c;
                if(!erro){
                    pontuacao++;
                }
            }
            else
            {
                String str = new String(hipotese.ToCharArray());
                String scr = new String(secreta.ToCharArray());
                if(str.ToLower().CompareTo(scr.ToLower())==0)
                {
                   for (int i = 0; i < secreta.Length; i++)
                    {
                        amostra[i*2] = secreta[i];
                        erro = false;
                        ganhou = true;
                    }
                }
                if(!erro){
                    pontuacao+=10;
                }
            }
            if(erro)
            {
                this.tentativas--;
            }
        }

    }
}
