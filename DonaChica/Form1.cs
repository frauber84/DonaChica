using Midi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace DonaChica
{
    public partial class Form1 : Form
    {
         
        String[][] Harmonias = 
        {
            new string[] { "C", "G", "G(#5)", "Ab7M", "C7M", "Em", "Em7", "Am7", "Bb°7", "Dø(9)", "Bø", "FM7(9)", "Dsus4", "Dm7(9)", "F6/9", "C#ø", "C#°7", "Db7(b5)",                },
            new string[] { "C",  "Dm", "Dm7(9)", "F7M", "Eø", "Bb°", "Bb°7", "Dø(9)", "Bø", "G#°7", "G7(13)", "Dsus4", "Am7",                },
            new string[] { "C", "C7M", "C6/9", "D7sus4", "Em", "Em7", "Eø", "E°7", "G°", "Bb°", "Am7", "Ab7M", "A7", "Gm", "F7M(9)", "F6/9",               },
            new string[] { "C7", "C",  "Em",  "A7", "Eø", "Eb", "Aø", "Gm7", "F7M(9)", "G7(b5)", "Cm7(7M)", "G(#5)", },
            new string[] { "Csus4", "F(#5)", "F", "Dm7", "G", "G7", "G7(b13)", "Dm", "Bø", "Db", "Fm", "Dø", "E7(b9)",                 },
            new string[] { "F7(b5)", "Csus4", "G#°7", "G", "G7", "G7(b13)", "G7(b9)", "E7(b9)", "A7(b13)", "Db", "Db7(b5)", "Dø", "Fm", "Db7", "Gb7M", "Bb", "Bb7M",                },
            new string[] { "C", "C7M", "Em7", "Em", "Am",  "F7M", "Dm7(9)", "D7(9)", "F7M(9)",                   },
            new string[] { "C",  "Am",  "F7M(9)", "C7M", "F7M", "F#ø", "Dm7(9)", "G7(13)", "Dø(9)", "Em7", "G°7", "Am7", "D7sus4", "E7", "E7(b13)", "G#(5)",                },
            new string[] { "F",  "Dm", "F#°7", "C#(#5)", "F#ø", "B7", "Am", "F7M", "B7(b9)", "F#°7", "F(#5)", "Bb7M", "Eb9(11)",                   },
            new string[] { "Dm", "Dm", "B7", "F", "F#ø", "Am", "F7M","B7", "B7(b9)", "A7(b13)", "G7(9)" ,},
            new string[] { "C",  "Em", "Em7", "Am7", "Dsus4", "Eø", "Bb°7", "E7(b9)", "E7sus4", "E°", "G7(9)",},
            new string[] { "C",  "C7M",  "Am7", "F7M(9)", "F7(9)", "Am", "Dsus4", "Gm", "Eø", "Bb°7", "B(#5)",                 },
            new string[] { "G",  "Em", "Em7", "A7", "F7M(9)", "Am7", "Db7(b5)", "C7", "C7(b9)", "Dsus4", "Bb°7", "Dø(9)", "F6/9",              },
            new string[] { "G", "G7", "G7(#5)", "Db7(b5)", "Ab7M", "Fm", "G7(13)", "G7(b5)", "F6/9",                },
            new string[] { "C",  "C7M", "Db7M       C7M", "Ab7M      C7M", "Fm6       C6/9"},
        };


        public Form1()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Harmonizar_Click(object sender, EventArgs e)
        {            
            if (Triade.Checked == false && Tetrade.Checked == false && Extensoes.Checked == false)
            {
                MessageBox.Show("Sem elementos para harmonização!", "Erro!");
                return;
            }

            LimpaTexto();                        
            RandomizaHarmonias();
            LimpaSuperfluos();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void RandomizaHarmonias()
        {
            Random rand = new Random();

            String[][] HarmoniasCopia = new string[Harmonias.Length][];

            for (int i =0; i < Harmonias.Length; i++)
            {
                HarmoniasCopia[i] = new string[Harmonias[i].Length];
                int j = 0;
                foreach (string s in Harmonias[i])
                {
                    
                    bool ignorar = false;
                    if (Extensoes.Checked == false)
                    {
                        if (s.Contains("9") || s.Contains("13") || s.Contains("4") || s.Contains("7(#5)") || s.Contains("(b5)"))
                        {
                            ignorar = true;
                            //goto ignora;
                        }
                    }
                    if (Tetrade.Checked == false)
                    {
                        if (s.Contains("7") || s.Contains("°7") || s.Contains("ø") )
                        {
                            if (! (s.Contains("9") || s.Contains("13") || s.Contains("4") || s.Contains("#5") || s.Contains("(b5)")))
                            {
                                ignorar = true;
                            }
                            
                            //goto ignora;
                        }
                    }
                    if (Triade.Checked == false)
                    {
                        if (!(s.Contains("7") || s.Contains("°7") || s.Contains("ø") || s.Contains("9") || s.Contains("13") || s.Contains("4") || s.Contains("5") || s.Contains("(b5)") ))
                        {
                            ignorar = true;
                        }
                    }

                    // elemento pode ser adicionado
                    if (ignorar == false)
                    {
                        HarmoniasCopia[i][j] = s;
                        j++;
                    }

                }

                while (j != HarmoniasCopia[i].Length)
                {
                    HarmoniasCopia[i][j] = "nulo";
                    j++;
                }
                
            }
           
            label0.Text = HarmoniasCopia[0][rand.Next(HarmoniasCopia[0].Length)];
            label1.Text = HarmoniasCopia[1][rand.Next(HarmoniasCopia[1].Length)];
            label2.Text = HarmoniasCopia[2][rand.Next(HarmoniasCopia[2].Length)];
            label3.Text = HarmoniasCopia[3][rand.Next(HarmoniasCopia[3].Length)];
            label4.Text = HarmoniasCopia[4][rand.Next(HarmoniasCopia[4].Length)];
            label5.Text = HarmoniasCopia[5][rand.Next(HarmoniasCopia[5].Length)];
            label6.Text = HarmoniasCopia[6][rand.Next(HarmoniasCopia[6].Length)];
            label7.Text = HarmoniasCopia[7][rand.Next(HarmoniasCopia[7].Length)];
            label8.Text = HarmoniasCopia[8][rand.Next(HarmoniasCopia[8].Length)];
            label9.Text = HarmoniasCopia[9][rand.Next(HarmoniasCopia[9].Length)];
            label10.Text = HarmoniasCopia[10][rand.Next(HarmoniasCopia[10].Length)];
            label11.Text = HarmoniasCopia[11][rand.Next(HarmoniasCopia[11].Length)];
            label12.Text = HarmoniasCopia[12][rand.Next(HarmoniasCopia[12].Length)];
            label13.Text = HarmoniasCopia[13][rand.Next(HarmoniasCopia[13].Length)];
            label14.Text = HarmoniasCopia[14][rand.Next(HarmoniasCopia[14].Length)];

            while (label0.Text == "nulo") label0.Text = HarmoniasCopia[0][rand.Next(HarmoniasCopia[0].Length)];
            while (label1.Text == "nulo") label1.Text = HarmoniasCopia[1][rand.Next(HarmoniasCopia[1].Length)];
            while (label2.Text == "nulo") label2.Text = HarmoniasCopia[2][rand.Next(HarmoniasCopia[2].Length)];
            while (label3.Text == "nulo") label3.Text = HarmoniasCopia[3][rand.Next(HarmoniasCopia[3].Length)];
            while (label4.Text == "nulo") label4.Text = HarmoniasCopia[4][rand.Next(HarmoniasCopia[4].Length)];
            while (label5.Text == "nulo") label5.Text = HarmoniasCopia[5][rand.Next(HarmoniasCopia[5].Length)];
            while (label6.Text == "nulo") label6.Text = HarmoniasCopia[6][rand.Next(HarmoniasCopia[6].Length)];
            while (label7.Text == "nulo") label7.Text = HarmoniasCopia[7][rand.Next(HarmoniasCopia[7].Length)];
            while (label8.Text == "nulo") label8.Text = HarmoniasCopia[8][rand.Next(HarmoniasCopia[8].Length)];
            while (label9.Text == "nulo") label9.Text = HarmoniasCopia[9][rand.Next(HarmoniasCopia[9].Length)];
            while (label10.Text == "nulo") label10.Text = HarmoniasCopia[10][rand.Next(HarmoniasCopia[10].Length)];
            while (label11.Text == "nulo") label11.Text = HarmoniasCopia[11][rand.Next(HarmoniasCopia[11].Length)];
            while (label12.Text == "nulo") label12.Text = HarmoniasCopia[12][rand.Next(HarmoniasCopia[12].Length)];
            while (label13.Text == "nulo") label13.Text = HarmoniasCopia[13][rand.Next(HarmoniasCopia[13].Length)];
            while (label14.Text == "nulo") label14.Text = HarmoniasCopia[14][rand.Next(HarmoniasCopia[14].Length)];

            if (Acordes.SelectedIndex == 0)
            {
                label1.Text = "";
                label3.Text = "";
                label5.Text = "";
                label7.Text = "";
                label9.Text = "";
                label11.Text = "";
                label13.Text = "";
            }
            else if (Acordes.SelectedIndex == 1)
            {
                //while (label1.Text == label0.Text && (label1.Text == "nulo") ) {  label1.Text = HarmoniasCopia[1][rand.Next(HarmoniasCopia[1].Length)]; }
                //while (label3.Text == label2.Text && (label3.Text == "nulo")) { label3.Text = HarmoniasCopia[3][rand.Next(HarmoniasCopia[3].Length)]; }
                //while (label5.Text == label4.Text && (label5.Text == "nulo")) { label5.Text = HarmoniasCopia[5][rand.Next(HarmoniasCopia[5].Length)]; }
                //while (label7.Text == label6.Text && (label7.Text == "nulo")) { label7.Text = HarmoniasCopia[7][rand.Next(HarmoniasCopia[7].Length)]; }
                //while (label9.Text == label8.Text && (label9.Text == "nulo")) { label9.Text = HarmoniasCopia[9][rand.Next(HarmoniasCopia[9].Length)]; }
                //while (label11.Text == label10.Text && (label11.Text == "nulo")) { label11.Text = HarmoniasCopia[11][rand.Next(HarmoniasCopia[11].Length)]; }
                //while (label13.Text == label12.Text && (label13.Text == "nulo")) { label13.Text = HarmoniasCopia[13][rand.Next(HarmoniasCopia[13].Length)]; }
            }


        }
        
        private int LimpaSuperfluos()
        {
            bool repetido = false;
            if (label1.Text == label0.Text) { label1.Text = ""; repetido = true; }
            if (label3.Text == label2.Text) { label3.Text = ""; repetido = true; }
            if (label5.Text == label4.Text) { label5.Text = ""; repetido = true; }
            if (label7.Text == label6.Text) { label7.Text = ""; repetido = true; }
            if (label9.Text == label8.Text) { label9.Text = ""; repetido = true; }
            if (label11.Text == label10.Text) { label11.Text = ""; repetido = true; }
            if (label13.Text == label12.Text) { label13.Text = ""; repetido = true; }

            if (repetido==true) return 1;
            else return 0;

        }

        private void LimpaTexto()
        {
            label0.Text = "";
            label1.Text = "";
            label2.Text = "";
            label3.Text = "";
            label4.Text = "";
            label5.Text = ""; 
            label6.Text = ""; 
            label7.Text = ""; 
            label8.Text = ""; 
            label9.Text = ""; 
            label10.Text = ""; 
            label11.Text = ""; 
            label12.Text = ""; 
            label13.Text = "";
            label14.Text = ""; 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Triade.Checked = true;
            Tetrade.Checked = true;
            Extensoes.Checked = true;
            LimpaTexto();
            label0.Text = "C";
            label4.Text = "G7";
            label6.Text = "C";
            label8.Text = "F";
            label10.Text = "C";
            label12.Text = "G";
            label13.Text = "G7";
            label14.Text = "C";
            Acordes.SelectedIndex = 1;


        }

        private void Escutar_Click(object sender, EventArgs e)
        {

            string arquivo1 = @"
(title Atirei o pau no gato)
(composer Dona Chica! v0.1)
(show )
(year 2012)
(comments )
(meter 4 4)
(key 0)
(tempo 100.0)
(volume 127)
(playback-transpose 0)
(chord-font-size 16)
(bass-instrument 33)
(bass-volume 112)
(drum-volume 105)
(chord-volume 110)
(breakpoint 54)
(layout)
(roadmap-layout 8)
(style ballad
    (swing 0.67)
    (comp-swing 0.67)
    (bass-high g-)
    (bass-low g---)
    (bass-base c--)
    (chord-high a)
    (chord-low c-)
    (chord-base c- e- g-)
)
(part
    (type chords)
    (title )
    (composer )
    (instrument 0)
    (volume 110)
    (key 0)
)


(section (style ballad)) 
";

    string arquivo2 = @"
(part
    (type melody)
    (title )
    (composer )
    (instrument 11)
    (volume 120)
    (key 0)
    (stave treble)
)
 g4+8 f8 e8 d8 e8 f8

 g4 g4 g4 a8 g8

 f4 f4 f4 g8 f8

 e4 e4 e4 c8 c8

 a4 a4 a4 b8 a8

 g4 g4 g4 e8 f8

 g4 e8 f8 g8 f8 e8 d8

 c1
";

            string Acordes = AdaptaFormato(label0.Text) + " " + AdaptaFormato(label1.Text) + " | " +
                     AdaptaFormato(label2.Text) + " " + AdaptaFormato(label3.Text) + " | " +
                     AdaptaFormato(label4.Text) + " " + AdaptaFormato(label5.Text) + " | " +
                     AdaptaFormato(label6.Text) + " " + AdaptaFormato(label7.Text) + " | " +
                     AdaptaFormato(label8.Text) + " " + AdaptaFormato(label9.Text) + " | " +
                     AdaptaFormato(label10.Text) + " " + AdaptaFormato(label11.Text) + " | " +
                     AdaptaFormato(label12.Text) + " " + AdaptaFormato(label13.Text) + " | " +
                     AdaptaFormato(label14.Text) + " | ";

            string arquivo = arquivo1 + Acordes + arquivo2;

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = "Exportar leadsheet para Impro-visor";
            dlg.Filter = "(.ls arquivo leadsheet)|*.ls";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                StreamWriter Settings = new StreamWriter(dlg.FileName);
                Settings.WriteLine(arquivo);
                Settings.Close();            
            }

        }

        private string AdaptaFormato(string entrada)
        {
            string saida = entrada;

            if (saida.Contains("9(11)") == true) saida = saida.Replace("9(11)", "11");
            if (saida.Contains("m7(7M)") == true) saida = saida.Replace("m7(7M)", "mMaj7");
            if (saida.Contains("7(9)") == true) saida = saida.Replace("7(9)", "9");
            if (saida.Contains("7(b9)") == true) saida = saida.Replace("7(b9)", "7b9");
            if (saida.Contains("7(b5)") == true) saida = saida.Replace("7(b5)", "7b5");
            if (saida.Contains("7(13)") == true) saida = saida.Replace("7(13)", "7add6");
            if (saida.Contains("7(b13)") == true) saida = saida.Replace("7(b13)", "7b6");            
            if (saida.Contains("7M(9)") == true) saida = saida.Replace("7M(9)", "M9");
            if (saida.Contains("m7(9)") == true) saida = saida.Replace("m7(9)", "m9");
            if (saida.Contains("7(#5)") == true) saida = saida.Replace("7(#5)", "7#5");
            if (saida.Contains("ø(9)") == true) saida = saida.Replace("ø(9)", "m9b5");            
            if (saida.Contains("(#5)") == true) saida = saida.Replace("(#5)", "M#5");            
            if (saida.Contains("7M") == true) saida = saida.Replace("7M", "Maj7");
            if (saida.Contains("°7") == true) saida = saida.Replace("°7", "o7");
            else if (saida.Contains("°") == true) saida = saida.Replace("°", "o");
            if (saida.Contains("ø") == true) saida = saida.Replace("ø", "m7b5");
            if (saida.Contains("6/9") == true) saida = saida.Replace("6/9", "69");

            return saida;
        }

        private void Triade_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void Tetrade_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void Extensoes_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Fator_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Miau!", "pprrrrrrrrrrrr...");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://www.facebook.com/pages/TDAH-Games/123997437708067");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problemas! Erro: \n", ex.ToString());
            }
            linkLabel1.LinkVisited = true;
        }
    }
}
