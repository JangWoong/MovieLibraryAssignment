// See https://aka.ms/new-console-template for more information
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Mvc;
using NLog;

string path = Directory.GetCurrentDirectory() + "\\nlog.config";
var logger = LogManager.Setup().LoadConfigurationFromFile(path).GetCurrentClassLogger();
string resp = "";

do
{
    // ask for input
    Console.WriteLine("Enter 1 to read a movies file.");
    Console.WriteLine("Enter 2 to input movies data.");
    Console.WriteLine("Enter anything else to quit.");
    // input response
    resp = Console.ReadLine();

    string file = "movies.csv";

    if (resp == "1")
    {
        // read data from file
        if (File.Exists(file))
        {
            //movieId,title,genres
            //1,Toy Story (1995),Adventure|Animation|Children|Comedy|Fantasy
            try 
            {
                // read data from file
                StreamReader sr = new StreamReader(file);
                //logger.Info($"Reader File open. ");
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string id = "";
                    string movieTitle = "";
                    string genres = "";

                    // convert string to array
                    string[] arr = line.Split(',');

                    if(arr.Length == 3)
                    {
                        // input array data
                        id = arr[0];
                        movieTitle = arr[1];
                        genres = arr[2];
                    }
                    else if(arr.Length > 3)
                    {
                        int alength = arr.Length;
                        id = arr[0];

                        // check " 
                        char[] charArray = arr[1].ToCharArray();
                        if(charArray[0] ==  '"')
                        {
                            for (int i = 1; i < alength -2; i++)
                            {
                                movieTitle = $"{arr[i]},";
                            }

                            movieTitle += $"{arr[alength-2]}";
                        }
                        genres = arr[alength-1];
                    }
                    else
                        logger.Info($"Array error data: {arr}");

                    Console.WriteLine($"ID: {id} \tTitle: {movieTitle} \nGenres: {genres}");
                }

                sr.Close();

                //logger.Info($"Reader File close. ");
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

        }
        else
        {
            logger.Info("File does not exist");
        }
        
    }
    else if (resp == "2")
    {
        // add movies
        if (File.Exists(file))
        {
            string endid = "", saveAnswer = "Y";
            string movieTitle = "", inputMovieTitle = "";
            string genres = "", inputMovieGenres = "";

            // ask for input
            Console.Write("Enter movie title: ");
            inputMovieTitle = Console.ReadLine();

            Console.Write("Enter movie genres: ");
            inputMovieGenres = Console.ReadLine();

            try 
            {
                // read data from file
                StreamReader sr = new StreamReader(file);
                //logger.Info($"Reader File open. ");
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    // convert string to array
                    string[] arr = line.Split(',');

                    if(arr.Length == 3)
                    {
                        // input array data
                        endid = arr[0];
                        movieTitle = arr[1];
                        genres = arr[2];
                    }
                    else if(arr.Length > 3)
                    {
                        int alength = arr.Length;
                        endid = arr[0];

                        // check " 
                        char[] charArray = arr[1].ToCharArray();
                        if(charArray[0] ==  '"')
                        {
                            for (int i = 1; i < alength -2; i++)
                            {
                                movieTitle = $"{arr[i]},";
                            }

                            movieTitle += $"{arr[alength-2]}";
                        }
                        genres = arr[alength-1];
                    }
                    else
                        logger.Info($"Array error data: {arr}");

                    if(movieTitle == inputMovieTitle)
                    {
                        Console.WriteLine($"ID: {endid} \tTitle: {movieTitle} \nGenres: {genres}");
                        Console.Write($"There is a same movies title. Save Yes(Y) or No(N or Other)? ");
                        saveAnswer = Console.ReadLine();

                        if(saveAnswer.ToUpper() != "Y")
                            break;
                    }
                }

                sr.Close();

                //logger.Info($"Reader File close. ");
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            try 
            {
                if(saveAnswer.ToUpper() == "Y")
                {
                    // write data from file
                    StreamWriter sw = new StreamWriter(file, append: true);
                    //logger.Info($"Writer File open. ");
                    int numId = Convert.ToInt32(endid) + 1;

                    sw.WriteLine($"{numId.ToString()}, {inputMovieTitle}, {inputMovieGenres}");
                    sw.Close();
                    //logger.Info($"Writer File close. ");
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
        else
        {
            logger.Info("File does not exist");
        }
    }
    else
    {
        logger.Info("Program exit.");
    }

} while(resp == "1" || resp == "2");
