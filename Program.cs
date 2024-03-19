// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");
// Console.WriteLine("Hello No World");

// Import statement
using HelloWorld.Models;
using HelloWorld.Data;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using AutoMapper;

namespace HelloWorld
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Connecting to database
            // It has the meta data of our connection
            // string connectionString = "Server=localhost;Database=DotNetCourseDatabase;TrustServerCertificate=true;Trusted_Connection=true;";
            // // DB connection object
            // IDbConnection dbConnection = new SqlConnection(connectionString);
            
            // This line has access to the json file
            IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

            DataContextDapper dapper = new DataContextDapper(config);

            // JSON
            // Read File
            string computersJson = File.ReadAllText("ComputersSnake.json");
            // Console.WriteLine(computersJson);

            // Mapper
            Mapper mapper = new Mapper(new MapperConfiguration((cfg)=>
            {
                cfg.CreateMap<ComputerSnake,Computer>()
                .ForMember(dest => dest.ComputerId, options => options.MapFrom(source => source.computer_id))
                .ForMember(dest => dest.CPUCores, options => options.MapFrom(source => source.cpu_cores))
                .ForMember(dest => dest.HasLTE, options => options.MapFrom(source => source.has_lte))
                .ForMember(dest => dest.HasWifi, options => options.MapFrom(source => source.has_wifi))
                .ForMember(dest => dest.Motherboard, options => options.MapFrom(source => source.motherboard))
                .ForMember(dest => dest.VideoCard, options => options.MapFrom(source => source.video_card))
                .ForMember(dest => dest.ReleaseDate, options => options.MapFrom(source => source.release_date))
                .ForMember(dest => dest.Price, options => options.MapFrom(source => source.price));
            }));

            // If using direct Computer Model instead of Mapping use JsonPropertyName attr in model
            IEnumerable<Computer>? computersSystem = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Computer>>(computersJson);
            
            if(computersSystem != null)
            {
            
                foreach(Computer singleComputer in computersSystem)
                {
                    Console.WriteLine(singleComputer.ComputerId);
                    Console.WriteLine(singleComputer.Motherboard);
                }
            }


            // This will convert json to ComputerSnake model
            // IEnumerable<ComputerSnake>? computersSystem = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<ComputerSnake>>(computersJson);
            
            // if(computersSystem != null)
            // {
            // foreach(ComputerSnake singleSnake in computersSystem)
            // {
            //     Console.WriteLine(singleSnake.computer_id + "Snake");
            // }
            //     // This will convert ienum computersnake to computer model
            //     IEnumerable<Computer> computerResult = mapper.Map<IEnumerable<Computer>>(computersSystem);
            //     foreach(Computer singleComputer in computerResult)
            //     {
            //         Console.WriteLine(singleComputer.ComputerId);
            //     }
            // }

            // // Set JSON Serializer Options
            // JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions()
            // {
            //     PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            // };

            // // Deserialize using builtin System.Text.Json
            // // IEnumerable<Computer>? computers = JsonSerializer.Deserialize<IEnumerable<Computer>>(computersJson,jsonSerializerOptions);
            
            // // Deserialize using NewtonSoft 
            // IEnumerable<Computer>? computers = JsonConvert.DeserializeObject<IEnumerable<Computer>>(computersJson);

            // // Serialize options for NewtonSoft
            // JsonSerializerSettings settings = new JsonSerializerSettings()
            // {
            //     ContractResolver = new CamelCasePropertyNamesContractResolver()
            // };

            // // Serialize Back to Json now using newton soft
            // // Computers Copy and Computers Json will be same now
            // string computersCopyNewtonSoft = JsonConvert.SerializeObject(computers,settings);
            // File.WriteAllText("NewtonSoftNewtonSoft.json",computersCopyNewtonSoft);

            // string computersCopySystem = System.Text.Json.JsonSerializer.Serialize(computers,jsonSerializerOptions);
            // File.WriteAllText("NewtonSoftSystem.json",computersCopySystem);


            // if(computers != null)
            // {
            //     foreach(Computer singleComputer in computers)
            //     {
            //         // Execute sql from the deserialized to the database
            //         string sql = @"INSERT INTO TutorialAppSchema.Computer(
            //             Motherboard, 
            //             HasWifi,
            //             HasLTE,
            //             ReleaseDate,
            //             Price,
            //             VideoCard
            //         ) VALUES ('" + escapeSingleQuote(singleComputer.Motherboard)
            //                 + "','" + singleComputer.HasWifi
            //                 + "','" + singleComputer.HasLTE
            //                 + "','" + singleComputer.ReleaseDate
            //                 + "','" + singleComputer.Price
            //                 + "','" + escapeSingleQuote(singleComputer.VideoCard)
            //         + "')";
            //         // Console.WriteLine(sql);
            //         dapper.ExecuteSql(sql);
            //     }
            // }

            // DataContextEF entityFramework = new DataContextEF(config);

            // Query Sql server to get date from our sql server
            // string sqlCommand = "SELECT GETDATE()";
            // Dapper Query returns array of results so use QuerySingle
            // DateTime rightNow = dapper.LoadSingleData<DateTime>(sqlCommand);
            // Console.WriteLine("Sql command executed");
            // Console.WriteLine(rightNow);

            
            // Accessing Class
            Computer computer = new Computer(){
                Motherboard = "Z690",
                HasWifi = true,
                HasLTE = false,
                ReleaseDate = DateTime.Now,
                Price = 943.87m,
                VideoCard = "RTX2060"
            };

            // For multiple line string use @
            

            // Write sql statement in the file
            // File.WriteAllText("log.txt",sql);

            // Appending more line to file
            // using StreamWriter openFile = new("log.txt",append:true);
            // openFile.WriteLine(sql);

            // We need to close the file using StreamWriter to use new commands
            // openFile.Close();

            // We read the file using ReadAllText
            // Console.WriteLine(File.ReadAllText("log.txt"));

            // Output 
            // INSERT INTO TutorialAppSchema.Computer(
            //     Motherboard,
            //     HasWifi,
            //     HasLTE,
            //     ReleaseDate,
            //     Price,
            //     VideoCard
            // ) VALUES ('Z690','True','False','2023-11-22 4:19:25 PM','943.87','RTX2060')
            // Console.WriteLine(sql);
            // int res = dapper.ExecuteSqlWithRowCount(sql); // This executes the insert command to db server
            // Console.WriteLine(res);

            // The same thing for EntityFrame to add the above Computer Data to the table is
            // entityFramework.Add(computer);
            // entityFramework.SaveChanges();

            string sqlSelect = @"
            SELECT
                Computer.Motherboard, 
                Computer.HasWifi,
                Computer.HasLTE,
                Computer.ReleaseDate,
                Computer.Price,
                Computer.VideoCard 
            FROM TutorialAppSchema.Computer
            ";

            IEnumerable<Computer> computersDapper = dapper.LoadData<Computer>(sqlSelect);
            // IEnumerable<Computer>? computersEF = entityFramework.Computer?.ToList<Computer>();

            foreach(Computer singleComputer in computersDapper)
            {
            //     Console.WriteLine("'" + escapeSingleQuote(singleComputer.Motherboard)
            //         + "','" + singleComputer.HasWifi
            //         + "','" + singleComputer.HasLTE
            //         + "','" + singleComputer.ReleaseDate
            //         + "','" + singleComputer.Price
            //         + "','" + escapeSingleQuote(singleComputer.VideoCard)
            // + "'");
            }

            // From Entity Framework
            // if(computersEF != null)
            // {
            //     foreach(Computer singleComputer in computersEF)
            //     {
            //         Console.WriteLine("'" + singleComputer.ComputerId
            //             + "','" + singleComputer.Motherboard
            //             + "','" + singleComputer.HasWifi
            //             + "','" + singleComputer.HasLTE
            //             + "','" + singleComputer.ReleaseDate
            //             + "','" + singleComputer.Price
            //             + "','" + singleComputer.VideoCard
            //     + "'");
            //     }
            // }
            // Console.WriteLine(computer.HasLTE);

            // // Set any property
            // computer.HasLTE = true;

            // Console.WriteLine(computer.HasLTE);


            // // Get
            // Console.WriteLine(computer.VideoCard);
            // Console.WriteLine(computer.Motherboard);
            // Console.WriteLine(computer.ReleaseDate);
            // Variables
            //// 1 byte is made up of 8 bits 00000000 - these bits can be used to store a number as follows
            // //// Each bit can be worth 0 or 1 of the value it is placed in
            // ////// From the right we start with a value of 1 and double for each digit to the left
            // //// 00000000 = 0
            // //// 00000001 = 1
            // //// 00000010 = 2
            // //// 00000011 = 3
            // //// 00000100 = 4
            // //// 00000101 = 5
            // //// 00000110 = 6
            // //// 00000111 = 7
            // //// 00001000 = 8

            // // 1 byte (8 bit) unsigned, where signed means it can be negative
            // byte myByte = 255;
            // byte mySecondByte = 0;

            // // 1 byte (8 bit) signed, where signed means it can be negative
            // sbyte mySbyte = 127;
            // sbyte mySecondSbyte = -128;


            // // 2 byte (16 bit) unsigned, where signed means it can be negative
            // ushort myUshort = 65535;

            // // 2 byte (16 bit) signed, where signed means it can be negative
            // short myShort = -32768;

            // // 4 byte (32 bit) signed, where signed means it can be negative
            // int myInt = 2147483647;
            // int mySecondInt = -2147483648;

            // // 8 byte (64 bit) signed, where signed means it can be negative
            // long myLong = -9223372036854775808;


            // // 4 byte (32 bit) floating point number
            // float myFloat = 0.751f;
            // float mySecondFloat = 0.75f;

            // Console.WriteLine(myFloat - mySecondFloat);
            // // 8 byte (64 bit) floating point number
            // double myDouble = 0.751;
            // double mySecondDouble = 0.75d;

            // // 16 byte (128 bit) floating point number
            // decimal myDecimal = 0.751m;
            // decimal mySecondDecimal = 0.75m;

            // Console.WriteLine(myFloat - mySecondFloat);
            // Console.WriteLine(myDouble - mySecondDouble);
            // Console.WriteLine(myDecimal - mySecondDecimal);



            // string myString = "Hello World";
            // // Console.WriteLine(myString);
            // string myStringWithSymbols = "!@#$@^$%%^&(&%^*__)+%^@##$!@%123589071340698ughedfaoig137";
            // // Console.WriteLine(myStringWithSymbols);

            // bool myBool = true;

            // // Array
            // string[] myArr = new string[2];
            // myArr[0]="Hello";
            // // myArr[1]="1";

            // Console.WriteLine(myArr[1]);

            // // We can also declare array as

            // string[] myArr2 = new string[] {"apples","eggs"};
            // // or string[] myArr2 = {"apples","eggs"};


            // // List in C#

            // List<string> myList = new List<string>();
            // // Predefined items in list
            // List<string> myList2 = new List<string>() {"hey","hi"};
            // // We can add items in list
            // myList2.Add("New Item");

            // // Enumrable in C#

            // IEnumerable<string> myEnum = myList2;

            // // We can access enum by index instead we need to loop through

            // Console.WriteLine(myEnum.First());

            // // For Two Dimensional Array

            // string[,] my2D = new string[,] {
            //     {"Apple","Banana"},
            //     {"Item1","Item2"}
            // };


            // // Dictionary takes a key instead of index

            // Dictionary<string,string> myDict = new Dictionary<string, string>(){
            // {"Item1","Cheese"},
            // {"Item2","Milk"},
            // };

            // Console.WriteLine(myDict["Item1"]);
            // Console.WriteLine(myArr2[0]);

            // // Operators and Conditionals

            // Console.WriteLine(Math.Pow(5,2));
            // Console.WriteLine(Math.Sqrt(81));
            // Console.WriteLine("Hello \"World\"");

            // // To split a string to array we can use Split Operator

            // string a = "Separate by spaces please";
            // string[] stringArr = a.Split(' ');

            // Console.WriteLine(stringArr[0]);

            // // Equals Operator
            // int b = 20;
            // int c = 40;

            // Console.WriteLine(b.Equals(c));
            // Console.WriteLine(b.Equals(c/2));

            // Console.WriteLine(.004m-.001m);

            // // If statement
            // int a = 5;
            // int b = 10;

            // if(a > b)
            // {
            //     a+=10;
            // }

            // if(a < b)
            // {
            //     a-=10;
            // }

            // Console.WriteLine(a);

            // // We can also check for string

            // string c = "Cow";
            // string d = "cow";

            // if(c == d)
            // {
            //     Console.WriteLine("C and D are equal");
            // }
            // else if(d == c.ToLower())
            // {
            //     Console.WriteLine("C and D case are equal");
            // }
            // else
            // {
            //     Console.WriteLine("C and D are not equal");
            // }

            // // Switch Statement its only for defined set of values

            // switch(c)
            // {
            //     case "cow":
            //         Console.WriteLine("cow is small");
            //         break;
            //     case "Cow":
            //         Console.WriteLine("cow is capitalized");
            //         break;
            //     default:
            //         Console.WriteLine("Something else");
            //         break;
            // }

            // // Loops

            // int[] intsToCompress = new int[] {10,20,30,40,50};
            // int totalSum = 0;

            // // How to check loop is taking time
            // // Current time
            // DateTime startTime1 = DateTime.Now;

            // int manualSum = intsToCompress[0] + intsToCompress[1] + intsToCompress[2] + intsToCompress[3] + intsToCompress[4];
            // Console.WriteLine((DateTime.Now - startTime1).TotalSeconds);

            // DateTime startTime2 = DateTime.Now;

            // for(int i = 0; i < intsToCompress.Length; i++)
            // {
            //     totalSum += intsToCompress[i];
            //     Console.WriteLine(totalSum);
            // }

            // Console.WriteLine((DateTime.Now - startTime2).TotalSeconds);

            // // For each for arrays and collections

            // foreach(int item in intsToCompress)
            // {
            //     Console.WriteLine(item);
            // }

            // // While loop

            // int index = 0;

            // while(index < intsToCompress.Length)
            // {
            //     Console.WriteLine(intsToCompress[index]);
            //     index++; // This is necessary in while
            // }

            // // Do while

            // // declaring
            // index = 0;

            // // It runs the first time always
            // do
            // {
            //     Console.WriteLine(intsToCompress[index]);
            //     index++; // This is necessary in while
            // }while(index < intsToCompress.Length);

            // // Builtin Sum

            // totalSum = intsToCompress.Sum();
            // Console.WriteLine(totalSum);

            // Calling our GetSum method
            int[] intsToCompress = new int[] {10,20,30,40,50};

            int r = GetSum(intsToCompress);
            // Console.WriteLine(r);

            int[] arr = new int[]{1,2,3};
            // int item = 10;
            // Dont use item if declared in outer scope
            // foreach(int item1 in arr)
            // {
            //     Console.WriteLine(item1);
            //     // totalSum += item;            
            // }
            // Same variable for for loops can be used multiple times if its not already declared in outerscope
            // foreach(int item1 in arr)
            // {
            //     Console.WriteLine(item1);
            //     // totalSum += item;            
            // }

        
        }

        // New method outside the main class
        // Have to put static as Main in static and only accepts static methods
        static private int GetSum(int[] arr)
        {
            int totalSum = 0;

            foreach(int item in arr)
            {
                totalSum += item;            
            }

            return totalSum;
        }
        
        static private string escapeSingleQuote(string input)
        {
            // This will resolve sql single quote problem
            string output = input.Replace("'","''");

            return output;
        }
    }

    
}