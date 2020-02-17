using System;
using System.Collections.Generic;
using System.IO;
using NLog;

namespace MovieFone
{
    class MainClass
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public static void Main(string[] args)
        //method for listing movies - put in switch statement instead
        //string interpolation, remove quotes and extra commas
            //how to do?
        //add to 3 different lists arrays. 1 for id, one for title, one for genres(separate array for genres?)
        //method for adding movies... nope, put in switch
            // id is generated
            // name - check for duplicates (List.Contains)
                //
            // do while loop for genres

        {
            //initialize arrayLists and vars
            List<string> movieID = new List<string>();
            List<string> movieName = new List<string>();
            List<string> movieGenre = new List<string>();

            logger.Info("Program Started");

            try
            {
                string file = "movies.csv";
                StreamReader movies = new StreamReader(file);
                while (!movies.EndOfStream)
                {
                    string line = movies.ReadLine();
                    if (line.IndexOf('"') >= 0)
                    {
                        // need code
                        //pull ID out
                        //trim ID past quote
                        //pull name out
                        //
                        int quote = line.IndexOf('"');
                        movieID.Add(line.Substring(0, quote - 1));
                        //Console.WriteLine(line.Substring(0, quote - 1));
                        line = line.Substring(quote + 1);
                        quote = line.IndexOf('"');
                        //Console.WriteLine(line.Substring(0,quote));
                        movieName.Add(line.Substring(0, quote));
                        line = line.Substring(quote + 2);
                        movieGenre.Add(line);

                    }
                    else if(line.IndexOf('"') <=0)
                    {
                        try
                        {
                            //first line has no number for ID
                            string[] splitter = line.Split(',');
                            movieID.Add(splitter[0]);
                            movieName.Add(splitter[1]);
                            movieGenre.Add(splitter[2]);//need to split categories
                        }
                        catch(Exception e)
                        {
                            logger.Error("caught error {e}");
                            Console.WriteLine(e);
                        }
                        
                    }
                }
            }
            catch
            {
                Console.WriteLine("goes into catch");
                //throw new NotImplementedException();
            }
            string response = "1";
            while (response == "1" || response == "2")
            {
                Console.WriteLine("Press '1' to list all Movies");
                Console.WriteLine("Press '2' to add Movie");
                Console.WriteLine("Press anything else to exit");
                response = Console.ReadLine();
                logger.Info("users response is {response}");
                //switch for choices
                switch (response)
                {
                    case "1"://List all movies

                        for (int i = 0; i < movieID.Count; i++)
                        {
                            //remove duplicates
                            //if (movieName.Contains(movieName[i]))
                            //{
                            //    movieID.RemoveAt(i);
                            //    movieName.RemoveAt(i);
                            //    movieGenre.RemoveAt(i);
                            //}
                            Console.WriteLine(movieID[i]);
                            Console.WriteLine(movieName[i]);
                            //Console.WriteLine("{0} | {1}",i,movieID.Count);
                            Console.WriteLine(movieGenre[i] + "\n");
                        }
                        
                        break;
                    case "2"://Add Movie to List
                        // reset flag
                        string movName = "";

                        //create movie ID number
                        //get last id and ++
                        int newID = int.Parse(movieID[(movieID.Count) - 1]) + 1;
                        string IDST = newID.ToString();
                        movieID.Add(IDST);

                        //get movie title
                        Console.WriteLine("Name of movie:");
                        do
                        {
                            movName = Console.ReadLine();
                            if (movieName.Contains(movName))
                            {
                                Console.WriteLine("Movie Name is already in Database");
                            }
                        } while (movieName.Contains(movName));
                        movieName.Add(movName);
                        //loop for genres
                        string resp = "";
                        List<string> tempGenre = new List<string>();
                        Console.WriteLine("Add genres. Type none when all have been entered");
                        while (resp != "none")
                        {
                            resp = Console.ReadLine();
                            if (resp != "none")
                            {
                                tempGenre.Add(resp);
                            }
                        }
                        //convert to string
                        string genre = String.Join("|", tempGenre);
                        //string genre = tempGenre.ToString();
                        Console.WriteLine(genre);
                        //add to array
                        movieGenre.Add(genre);
                        //save to file - overwrite old file
                        break;
                    case null:
                        logger.Error("Not a valid response");
                        break;
                }
            }
        }
    }

}