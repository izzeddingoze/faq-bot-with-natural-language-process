using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.Data.OleDb;

namespace StringSimilarity
{
    public class SentenceSimilarity
    {
        public string QUERIED;
        public string[] SENTENCES;
        public string[] ALL_SENTENCES;
        public string[] VECTOR_OF_DOCUMENT;        
        public double[] IDF_VECTOR_OF_DOCUMENT;
        public double MAX_SIM_RATE,MAX_TF=0;
        double[] TF_IDF_VECTOR_OF_QUERIED;
        double SUM_OF_DOCUMENT_VECTOR = 0;

        public SentenceSimilarity (string QUERIED_SENTENCE, string[] SENTENCES_ARRAY)
        {
            QUERIED = LEMMATIZATION_SENTENCE(CLEAN_STOP_WORDS(CLEAN_PUNCTUATION(CLEAN_SPACE(QUERIED_SENTENCE.Trim().ToLower()))));
            SENTENCES = new string[SENTENCES_ARRAY.Length];
           
            for (int k = 0; k < SENTENCES.Length; k++)
                SENTENCES[k] = LEMMATIZATION_SENTENCE(CLEAN_STOP_WORDS(CLEAN_PUNCTUATION(CLEAN_SPACE(SENTENCES_ARRAY[k].Trim().ToLower()))));

            ALL_SENTENCES = new string[SENTENCES.Length + 1];

            for (int i = 0; i < SENTENCES.Length; i++)
                ALL_SENTENCES[i] = SENTENCES[i];


            ALL_SENTENCES[SENTENCES.Length] = QUERIED;
            COMPUTE_VECTOR_OF_DOCUMENT();
            COMPUTE_MAX_TF();
            COMPUTE_IDF_VECTOR_OF_DOCUMENT();
            TF_IDF_VECTOR_OF_QUERIED = COMPUTE_TF_IDF_VECTOR(QUERIED);
        }
        public string CLEAN_STOP_WORDS(string sentence) // bir stringdeki noktalama işaretlerinini(-,;:) ve (ve , veya , ile) gibi kelimeleri siler
        {
            sentence = sentence.Trim();
            sentence = sentence.ToLower();
            sentence = CLEAN_SPACE(sentence); 
            
            string[] sentence_array;
            string tmp_sentence = "";
            sentence_array = sentence.Split(' ');

            int i;
            for (i = 0; i < sentence_array.Length; i++)
            {

                if (sentence_array[i] == "miyim" ||  sentence_array[i] == "miler" || sentence_array[i] == "miyiz" || sentence_array[i] == "misin" || sentence_array[i] == "mi" || sentence_array[i] == "mu" || sentence_array[i] == "ve" || sentence_array[i] == "veya" || sentence_array[i] == "ile" || sentence_array[i] == "gibi" || sentence_array[i] == "için")
                {

                }
                else
                {
                    tmp_sentence += " " + sentence_array[i];
                }

            }// ve veya ile gibi gibi kelimeleri almadan diziden stringe birleştirildi kelimeler
            return tmp_sentence;
        }
        public string CLEAN_PUNCTUATION(string sentence) // bir stringdeki noktalama işaretlerinini(-,;:) ve (ve , veya , ile) gibi kelimeleri siler
        {
            
            
            string a_char, tmp_sentence;
            tmp_sentence = "";
            int i;
            for (i = 0; i < sentence.Length; i++)
            {
                a_char = sentence.Substring(i, 1);
                if (a_char == "." || a_char == "," || a_char == "-" || a_char == ")" || a_char == "(" || a_char == ";" || a_char == ":" || a_char == "-" || a_char == "_" || a_char == "?" || a_char == "!")
                {
                    tmp_sentence += "";
                }
                else
                {
                    tmp_sentence += a_char;
                }
            }// noktalama işaretleri silindi yerine space konuldu           
            return tmp_sentence;
        }
        public string CLEAN_SPACE(string sentence) // bir stringdeki fazla boşlukları temizler
        {
            string a_char, space = "NO",  tmp_sentence= "";
            tmp_sentence.Trim();
            for (int i = 0; i<sentence.Length; i++)
            {
                a_char = sentence.Substring(i, 1);
                if (a_char != " " || space == "NO")
                    tmp_sentence += a_char;
                if (a_char == " ")
                    space = "YES";                
                else
                    space = "NO";
            }
            return tmp_sentence;
        }
        public int NUMBER_OF_WORD(string sentence) // bir stringdeki kelime sayısını bulur
        {                     
            sentence = sentence.Trim();
            sentence = CLEAN_SPACE(sentence);
            string[] array_of_words = sentence.Split(' ');
            return array_of_words.Length;
        }
        public string LEMMATIZATION_SENTENCE(string SENTENCE)
        {
            SENTENCE = SENTENCE.Trim();
            SENTENCE = SENTENCE.ToLower();
            int locate;

            SENTENCE = CLEAN_STOP_WORDS(CLEAN_SPACE(CLEAN_PUNCTUATION(SENTENCE)));

            string[] TMP_SENTENCE_ARRAY = SENTENCE.Trim().Split(' ').Distinct().ToArray();

            string TMP_SENTENCE = "";

            for (int m = 0; m < TMP_SENTENCE_ARRAY.Length; m++)
            {
                //ler lar
                locate = TMP_SENTENCE_ARRAY[m].IndexOf("ler");
                if (locate != -1)
                    TMP_SENTENCE_ARRAY[m] = TMP_SENTENCE_ARRAY[m].Substring(0, locate);
                locate = TMP_SENTENCE_ARRAY[m].IndexOf("lar");
                if (locate != -1)
                    TMP_SENTENCE_ARRAY[m] = TMP_SENTENCE_ARRAY[m].Substring(0, locate);


                //e bilir a bilir mek mak 
                locate = TMP_SENTENCE_ARRAY[m].IndexOf("ebilir");
                if (locate != -1)
                    TMP_SENTENCE_ARRAY[m] = TMP_SENTENCE_ARRAY[m].Substring(0, locate) + "mek";
                locate = TMP_SENTENCE_ARRAY[m].IndexOf("abilir");
                if (locate != -1)
                    TMP_SENTENCE_ARRAY[m] = TMP_SENTENCE_ARRAY[m].Substring(0, locate) + "mak";               

            }

            for (int m = 0; m < TMP_SENTENCE_ARRAY.Length; m++)
                TMP_SENTENCE += TMP_SENTENCE_ARRAY[m] + " ";

            

            TMP_SENTENCE = CLEAN_STOP_WORDS(CLEAN_SPACE(CLEAN_PUNCTUATION(TMP_SENTENCE)));

            return TMP_SENTENCE;
        }
        public double JAKARD_SIMILARITY_RATE(string sentence1,string sentence2) // jakard yöntemiyle iki stringin benzerlik oranını bulur
        {
            
            sentence1 = CLEAN_PUNCTUATION(sentence1);
            sentence1 = CLEAN_STOP_WORDS(sentence1);
            sentence1 = sentence1.Trim();
            sentence1 = CLEAN_SPACE(sentence1);

            sentence2 = CLEAN_PUNCTUATION(sentence2);
            sentence2 = CLEAN_STOP_WORDS(sentence2);            
            sentence2 = sentence2.Trim();
            sentence2 = CLEAN_SPACE(sentence2);

            string[] combine_array = ((sentence1 +" "+ sentence2).Split(' ')).Distinct().ToArray();
            string[] sentence1_array = sentence1.Split(' ').Distinct().ToArray();
            string[] sentence2_array = sentence2.Split(' ').Distinct().ToArray();

            double rate = (sentence1_array.Length + sentence2_array.Length) - combine_array.Length;
                       
            return rate/combine_array.Length;

        }
        private void COMPUTE_VECTOR_OF_DOCUMENT()
        {
            string TMP_SENTENCE = QUERIED.ToLower();
            int i;
            for(i=0;i<SENTENCES.Length;i++)
            {
                TMP_SENTENCE += " " + SENTENCES[i].ToLower();
            }

            TMP_SENTENCE=(LEMMATIZATION_SENTENCE(TMP_SENTENCE));
            

            TMP_SENTENCE = TMP_SENTENCE.Trim();
            TMP_SENTENCE = CLEAN_SPACE(TMP_SENTENCE);
            TMP_SENTENCE = CLEAN_PUNCTUATION(TMP_SENTENCE);
            TMP_SENTENCE = CLEAN_STOP_WORDS(TMP_SENTENCE);
            TMP_SENTENCE = TMP_SENTENCE.Trim();

            SUM_OF_DOCUMENT_VECTOR = NUMBER_OF_WORD(TMP_SENTENCE);

            VECTOR_OF_DOCUMENT = TMP_SENTENCE.Trim().Split(' ').Distinct().ToArray();
        }
        public void COMPUTE_IDF_VECTOR_OF_DOCUMENT()
        {

            IDF_VECTOR_OF_DOCUMENT = new double[VECTOR_OF_DOCUMENT.Length];

            int i, j, locate;
            double counter;

            for (j = 0; j < IDF_VECTOR_OF_DOCUMENT.Length;  j++)
            {
                counter = 0;
                for (i = 0; i < ALL_SENTENCES.Length; i++)
                {
                    
                    ALL_SENTENCES[i] = ALL_SENTENCES[i].Trim();
                    ALL_SENTENCES[i] = CLEAN_SPACE(ALL_SENTENCES[i]);
                    ALL_SENTENCES[i] = CLEAN_PUNCTUATION(ALL_SENTENCES[i]);
                    ALL_SENTENCES[i] = " " + ALL_SENTENCES[i].ToLower() + " ";

                    locate = ALL_SENTENCES[i].IndexOf(" " + VECTOR_OF_DOCUMENT[j] + " ");
                    if (locate != -1)
                    {
                        counter++;
                    }
                    ALL_SENTENCES[i] = ALL_SENTENCES[i].Trim();

                }
                    IDF_VECTOR_OF_DOCUMENT[j] = 1 + Math.Log((ALL_SENTENCES.Length) / counter);
                    //MessageBox.Show(counter.ToString());
            }
        }
        private double[] TF_VECTOR_OF_SENTENCE(string SENTENCE)
        {
            
            double[] TF_VECTOR = new double[VECTOR_OF_DOCUMENT.Length];

            int  j,locate;

            //vectorlere varsayılan sıfır değerlerini ata
            
                for (j = 0; j < VECTOR_OF_DOCUMENT.Length; j++)
                    TF_VECTOR[j] = 0;



            SENTENCE = SENTENCE.Trim();
            SENTENCE = CLEAN_SPACE(SENTENCE);
            SENTENCE = CLEAN_PUNCTUATION(SENTENCE);
            SENTENCE = " "+SENTENCE.ToLower() + " ";

            // sorgulanacak sentence ın tf vektorunu hesaplar

            for (j = 0; j <VECTOR_OF_DOCUMENT.Length; j++)
            {

                locate = SENTENCE.IndexOf(" "+VECTOR_OF_DOCUMENT[j] + " ");
                while (locate != -1)
                {
                    TF_VECTOR[j]++;
                    


                    if (TF_VECTOR[j] > MAX_TF)
                        MAX_TF = TF_VECTOR[j];

                    locate = SENTENCE.IndexOf(" " + VECTOR_OF_DOCUMENT[j] + " ", locate + 1);
                    
                }
                locate = 0;
                
            }

            return TF_VECTOR;

            
                
            
        }
        private void COMPUTE_MAX_TF()
        {
            for (int i = 0; i < ALL_SENTENCES.Length; i++) // normalize için en büyük tf bulma işlemi. tf vektor hesaplarken en büyük tf max_tfe atılıyor otomatik olarak
                TF_VECTOR_OF_SENTENCE(ALL_SENTENCES[i]);

        }
        public double[] NORMALIZE_TF_VECTOR(double[] TF_VECTOR)
        {
            int  j;

            for (j = 0; j < VECTOR_OF_DOCUMENT.Length; j++)
                TF_VECTOR[j] = TF_VECTOR[j] / MAX_TF;

            return TF_VECTOR;


        }
        public double[] COMPUTE_TF_IDF_VECTOR(string SENTENCE)
        {
           
            double[] TF_VECTOR = NORMALIZE_TF_VECTOR(TF_VECTOR_OF_SENTENCE(SENTENCE));

            double[] TF_IDF_VECTOR = new double[VECTOR_OF_DOCUMENT.Length];

            for (int j = 0; j < VECTOR_OF_DOCUMENT.Length; j++)
            {
                TF_IDF_VECTOR[j] = (TF_VECTOR[j] * IDF_VECTOR_OF_DOCUMENT[j]);
                
            }

            return TF_IDF_VECTOR;

        }
        public double ABS_OF_VECTOR( double[] VECTOR)
        {
            double RESULT = 0;
            int i;

            for (i = 0; i < VECTOR.Length; i++)            
                RESULT += (VECTOR[i] * VECTOR[i]);

            RESULT = Math.Sqrt(RESULT);

            return RESULT;
        }
        public double TF_IDF_COSINE_SIMILARITY_RATE( string SENTENCE)
        {
            


            double[] TF_IDF_VECTOR_OF_SENTENCE = COMPUTE_TF_IDF_VECTOR(SENTENCE);

            double PRODUCT_TF_IDF_VECTORS = 0;
            int i;
            for(i=0;i<VECTOR_OF_DOCUMENT.Length;i++)
            {
                PRODUCT_TF_IDF_VECTORS += TF_IDF_VECTOR_OF_SENTENCE[i] * TF_IDF_VECTOR_OF_QUERIED[i];
               

            }
            

            double RESULT = PRODUCT_TF_IDF_VECTORS / (ABS_OF_VECTOR(TF_IDF_VECTOR_OF_QUERIED) * ABS_OF_VECTOR(TF_IDF_VECTOR_OF_SENTENCE));




            return RESULT;
        }
        public double JENSEN_SHANNON_RATE(string SENTENCE_1, string SENTENCE_2)
        {

            double[] TF_VECTOR_1 = TF_VECTOR_OF_SENTENCE(CLEAN_STOP_WORDS(CLEAN_SPACE(CLEAN_PUNCTUATION(SENTENCE_1.Trim()))));
            double[] TF_VECTOR_2 = TF_VECTOR_OF_SENTENCE(CLEAN_STOP_WORDS(CLEAN_SPACE(CLEAN_PUNCTUATION(SENTENCE_2.Trim()))));
            
            double SUM_1 = 0, SUM_2 = 0,RATE=0,TMP1,TMP2;
            int i;

            for (i = 0; i < TF_VECTOR_1.Length; i++)
            {
                if (TF_VECTOR_1[i] == 0 && TF_VECTOR_2[i] == 0)
                {
                    continue;
                }
                else
                {
                    TF_VECTOR_1[i] += TF_VECTOR_1[i] + 1;
                    TF_VECTOR_2[i] += TF_VECTOR_2[i] + 1;
                }
                    
            }
                        

            for (i = 0; i < TF_VECTOR_1.Length; i++)
                SUM_1 += TF_VECTOR_1[i];
            for (i = 0; i < TF_VECTOR_2.Length; i++)
                SUM_2 += TF_VECTOR_1[i];

            for(i=0;i<TF_VECTOR_1.Length;i++)
            {
                TMP1 = (TF_VECTOR_1[i] / SUM_1);
                TMP2 = (TF_VECTOR_2[i] / SUM_2);

                if (TMP1 == 0 && TMP2 == 0)
                    continue;
                else
                    RATE += ((Math.Log(((TMP1) / ((TMP1 + TMP2) / 2))) * TMP1) + (Math.Log(((TMP2) / ((TMP1 + TMP2) / 2))) * TMP2)) / 2;
            }

            return RATE;    
        }
    }
}