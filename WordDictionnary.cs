using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace WordBlitzPlayer
{
    public static class WordDictionnary
    {

        public static bool isLastActionAdd { get; private set; } = false;
        public static string LastWord { get; private set; } = string.Empty;

        public const string organizedDictBaseFolder = @"C:\Users\Jeremy\source\repos\WordBlitzPlayer\Dictionnairy";
        public const string rawDictionnaryPath = @"C:\Users\Jeremy\source\repos\WordBlitzPlayer\DictionnaireRaw.txt";

        public static string Dictionnary
        {
            get
            {
                if (!Directory.Exists(organizedDictBaseFolder))
                    CreateOrganizedDictionnary();

                return organizedDictBaseFolder;
            }
        }

        public static bool AddWord(String word)
        {
            if (string.IsNullOrEmpty(word)) return false;

            if(!Regex.IsMatch(word, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("Non-Letter character(s) in string", "Invalid word", MessageBoxButton.OK, MessageBoxImage.Error);
                return false ;
            }

            word = word.ToUpper();
            String path = organizedDictBaseFolder;
            foreach(char ltr in word)
            {
                path += $"\\{ltr}";
            }


            if(Directory.Exists(path) && Directory.GetFiles(path).Contains($"{path}\\{word.ToLower()}.word"))
            {
                MessageBox.Show($"'{word}' is already in the dictionnary", "Word already in dict!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }

            if (MessageBox.Show($"'{word}' is not in the dictionnairy, add it?", "You found a new word!", MessageBoxButton.YesNo, MessageBoxImage.Information) != MessageBoxResult.Yes)
            {
                MessageBox.Show($"'{word}' was not added", "Action cancelled by user", MessageBoxButton.OK);
                return false;
            }

            Directory.CreateDirectory(path);

            var file = File.Create(path + "\\" + word.ToLower() + ".word");
            file.Close();

            Logger.log($"'{word}' Added ");


            LastWord = word;
            isLastActionAdd = true;
            return true;

        }

        public static bool RemoveWord(string word)
        {
            word = word.ToUpper();
            String path = organizedDictBaseFolder;
            foreach (char ltr in word)
            {
                path += $"\\{ltr}";
            }

            if (!Directory.Exists(path)) return false;

            if (!Directory.GetFiles(path).Contains($"{path}\\{word.ToLower()}.word"))
            {
                MessageBox.Show($"'{word}' is not in the dictionnary", "Word not in dict!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }

            if (MessageBox.Show($"Are you sure you want to remove '{word}' from the dict?", "You sure?", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                return false;

            File.Delete($"{path}\\{word.ToLower()}.word");

        
            if(Directory.GetDirectories(path).Length == 0)
            {
                Directory.Delete(path, false);
                DirectoryInfo ParentDir = Directory.GetParent(path);

                while (ParentDir.GetDirectories().Length == 0 && ParentDir.GetFiles().Length == 0)
                {
                    ParentDir.Delete();
                    ParentDir = ParentDir.Parent;
                }
            }

            //if (MessageBox.Show($"'{word}' removed from dictionnary", "Word removed", MessageBoxButton.OKCancel, MessageBoxImage.Information) == MessageBoxResult.Cancel)
            //{
            //    AddWord(word);
            //    return false;
            //}
            //else
            //{
            LastWord = word;
            isLastActionAdd = false;
            return true;
            //}


        }
        
        public static bool Undo()
        {
            if(LastWord == string.Empty)
            {
                MessageBox.Show("No action to undo", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }
            if(isLastActionAdd)
            {
                return RemoveWord(LastWord);
            }
            else
            {
                return AddWord(LastWord);
            }
        }

        public static void CreateOrganizedDictionnary(string source = rawDictionnaryPath, string destinationDictionnaryName = organizedDictBaseFolder)
        {

            //TODO: Handle Œ

            if (!File.Exists(source))
                throw new Exception($"An error occured while trying to create an organized Dictionnary:\n\n{source} file does not exist");

            Directory.CreateDirectory(destinationDictionnaryName);

            int numberOfWord = 0;

            string rawDict;

            using (StreamReader r = new StreamReader(source, Encoding.GetEncoding("iso-8859-1")))
               rawDict = r.ReadToEnd();

            string[] dict = rawDict.RemoveDiacritics().Replace("\r", "").Split('\n'); 

            foreach (string word in dict)
            {

                //if (numberOfWord > 1000) return;

                string wordDirectory = destinationDictionnaryName;

                for(int letterIndex = 0; letterIndex < word.Length; letterIndex++)
                {
                    wordDirectory =  Directory.CreateDirectory(wordDirectory + $"\\{word[letterIndex].ToString().ToUpper()}").FullName;
                    
                    if(letterIndex == word.Length - 1)
                    {
                        string wordFile = wordDirectory + $@"\{word}.word";

                        if (!File.Exists(wordFile))
                        {
                            try
                            {
                                File.Create(wordFile);
                                numberOfWord++;
                            }
                            catch
                            {
                                wordFile = wordDirectory + $@"\_{word}.word";
                                File.Create(wordFile);
                                numberOfWord++;
                            }
                            
                        }

                        break;
                    }
                }
            }
            MessageBox.Show($"Dictionnary created with {numberOfWord} words");
        }

        public static String RemoveDiacritics(this String s)
        {
            string accentedStr;
            byte[] tempBytes;
            tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(s);
            string asciiStr = System.Text.Encoding.UTF8.GetString(tempBytes);

            return asciiStr;
            
        }

    }
}
