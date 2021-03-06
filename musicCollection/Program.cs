using System;
using MySql.Data.MySqlClient;


namespace musicCollection
{
    class Program
    {
        static void Main()
        {
           
            var exit = false;
           
            do
            {
                Show.Menu();

                var select = Console.ReadLine();
                switch (select)
                {
                    case "1":
                    
                       ExportSingers();
                       ImportSingers();
                       Console.WriteLine("Из базы данных загрузили в csv файл и прочитали в консоли записанный файл :)");
                       ReadSingers();
                        break;
                    case "2":
                        ExportMusicToJson();
                        Console.WriteLine("Из базы данных загрузили в json файл и прочитали в консоли записанный файл :)");
                        Database.DeSerialiseMusicToJson("music.json");
                        break;
                    case "3":
                        Console.WriteLine("выберете из списка номер исполнителя");
                        ReadSingers();
                        ExportMusicDiskToXml();
                        Database.DeSerialiseMusicToXml("music.xml");
                        break;
                    case "4":
                        PrintPopularSinger();
                        
                        break;
                    case "5":
                        PrintLongerSong();
                        break;
                   

                    case "0":
                        Console.WriteLine("До свидания");
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Неверный ввод");
                        break;
                }
            } while (!exit);

            static void ExportSingers()
            {
                var db = new Database();
                db.ExportSingersToCSV("singers.csv");
            }

            static void ReadSingers()
            {
                var db = new Database();
                db.ReadSingersFileCsv("singers.csv");
                Console.ReadLine();;
            }
            static void ImportSingers()
            {
                var db = new Database();
                db.ImportSingersFromCSV("singers.csv");
            }

            static void ExportMusicToJson()
            {
                var db = new Database();
                
                db.SerialiseMusicToJson("music.json");
            }
            
            static void ExportMusicDiskToXml()
            {
                var db = new Database();
                
                db.SerialiseMusicToXml("music.xml");
            }
            static void PrintPopularSinger()
            {
                var db = new Database();
                
                db.PrintPopularsinger();
            }
            
            static void PrintLongerSong()
            {
                var db = new Database();

                db.PrintLongerSong();
            }
        }

       
    }

}
        
    
            
    

     