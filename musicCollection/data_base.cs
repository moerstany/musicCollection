using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace musicCollection
{
    public class Database
    {
        private MySqlConnection db;
        private MySqlCommand command;

        public Database()
        {
            var connectionString = ConnectionString.Init(@"db_connect.ini");
            db = new MySqlConnection(connectionString);
            command = new MySqlCommand {Connection = db};
        }

        public void Open() => db.Open();

        public void Close() => db.Close();

        public List<Singers> GetSingers()
        {
            Open();
            var list = new List<Singers>();

            var sql = "SELECT singer_id, singer  FROM tab_singers3;";
            command.CommandText = sql;
            var res = command.ExecuteReader();
            if (!res.HasRows) return null;

            while (res.Read())
            {
                var singer_id = res.GetUInt32("singer_id");
                var singer = res.GetString("singer");

                list.Add(new Singers {singer_id = singer_id, singer = singer});
            }

            Close();
            return list;
        }



        public List<Music> JsonMusics()
        {
            Open();
            var list = new List<Music>();
            var sql = @"SELECT id, song_name, song_time, music_style,singer,music_disk_name
                        FROM tab_musicCollection3
                        JOIN tab_music_disk3 
                            ON tab_musicCollection3.music_disk_id = tab_music_disk3.music_disk_id
                        JOIN tab_singers3 
                            ON tab_musicCollection3.singer_id = tab_singers3.singer_id;";
            command.CommandText = sql;
            var res = command.ExecuteReader();
            if (!res.HasRows) return null;

            while (res.Read())
            {
                var id = res.GetUInt32("id");
                var song_name = res.GetString("song_name");
                var song_time = res.GetUInt32("song_time");
                var music_style = res.GetString("music_style");
                var singer = res.GetString("singer");
                var music_disk_name = res.GetString("music_disk_name");
                list.Add(new Music
                {
                    id = id, song_name = song_name,
                    song_time = song_time, music_style = music_style,
                    singer = singer, music_disk_name = music_disk_name
                });
            }

            Close();

            return list;
        }

        public void ExportSingersToCSV(string path)
        {

            var singers = GetSingers();

            using var file = new StreamWriter(path, append: false);
            foreach (var singer in singers)
            {
                file.WriteLine($"{singer.singer_id}|{singer.singer}");
            }

        }

        public void ReadSingersFileCsv(string path)
        {
            var lines = File.ReadAllLines(path);
            var list = new List<Singers>();
            foreach (var line in lines)
            {
                var values = line.Split("|");
                var singers = new Singers {singer_id = uint.Parse(values[0]), singer = values[1]};
                list.Add(singers);
            }

            foreach (var singer in list)
            {
                Console.WriteLine($"{singer.singer_id} | {singer.singer}");
            }

        }

        public void ImportSingersFromCSV(string path)
        {
            Open();
            var singers_csv = new List<Singers>();

            using var file = new StreamReader(path);

            var line = string.Empty;
            while ((line = file.ReadLine()) != null)
            {
                var temp = line.Split('|');
                var singer = new Singers()
                {
                    singer_id = uint.Parse(temp[0]),
                    singer = temp[1]

                };
                singers_csv.Add(singer);
            }

            Close();
        }

        public  void SerialiseMusicToJson(string path)
        {
            // using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            var jsonMusics = JsonMusics();
            string JsonObject = JsonConvert.SerializeObject(jsonMusics);
            Console.WriteLine("Data has been saved to file");
            Console.WriteLine(JsonObject);
            File.WriteAllText(path, String.Empty);
            File.AppendAllText(path, JsonObject);


        }

       
        public static void DeSerialiseMusicToJson(string path)
        {
            
            var reader = File.ReadAllText(path);

            var getmusics = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Music>>(reader);

           // Console.WriteLine(getmusics.ToFormatedString());
           foreach (var music in getmusics)
           {
               Console.WriteLine(String.Format($"{music.id} | {music.song_name} | {music.song_time} | {music.music_style} | {music.singer} | {music.music_disk_name}"));
           }   
           
        }

        public List<Music_disk> GetSingerDisk()
        {
            Console.WriteLine("Выберете номер исполнителя из списка");
            var input1 = Convert.ToInt32(Console.ReadLine());
            Open();

            var list = new List<Music_disk>();

            var sql = string.Format($@"SELECT music_disk_id, singer, music_disk_name
                        FROM tab_musicCollection3
                        JOIN tab_music_disk3 
                            ON tab_musicCollection3.music_disk_id = tab_music_disk3.music_disk_id
                        JOIN tab_singers3 
                            ON tab_musicCollection3.singer_id = tab_singers3.singer_id
                        WHERE singer_id = '{input1 }' ");

        command.CommandText = sql;
            var res = command.ExecuteReader();
            if (!res.HasRows) return null;

            while (res.Read())
            {
                var music_disk_id = res.GetUInt32("music_disk_id");
                var singer = res.GetString("singer");
                var music_disk_name = res.GetString("music_disk_name");
                list.Add(new Music_disk()
                {
                    music_disk_id = music_disk_id, 
                    singer = singer, music_disk_name = music_disk_name
                });
            }

            Close();

            return list;
        }


        /*var getmusics = JsonMusics();
        using (var file_input = new FileStream(path, FileMode.Open))
        {
            
        }*/



    }
}



    
