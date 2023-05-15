using System;

public struct City
{
    public string name;
    public int cityAround;
    public int[] cityAroundName;
    public int covidLevel;

    public City(string Name,int CityAround,int[] CityAroundName,int CovidLevel){
        this.name = Name;
        this.cityAround = CityAround;
        this.cityAroundName = CityAroundName;
        this.covidLevel = CovidLevel;
    }
}



class Programe {

    static int GetPositiveNumber(){
        int number = int.Parse(Console.ReadLine());
        if (number < 0) { 
            Console.Write("Pls input position number");
            number = GetPositiveNumber(); 
        }
        return number;
    }

    static void Main(string[] args){
        Console.Write("How many city: ");
        int cityInMap = int.Parse(Console.ReadLine());
        City[] Citys = new City[cityInMap];

        for (int i = 0 ; i < cityInMap ; i++){
            Console.Write(i + " City Name: ");
            string cityName = Console.ReadLine();
            Console.Write("How many citys in range: ");
            int cityAround = GetPositiveNumber();

            int[] cityAroundName = new int[cityAround];
            // Set Defalut
            for (int j = 0 ; j < cityAround ; j++){
                cityAroundName[j] = -1;
            }

            //Get City In Range Name
            for (int j = 0 ; j < cityAround ; j++){

                static void InputCityTag(ref int[] cityAroundName, int cityInMap,int CurrenctCity,int j){
                    Console.Write("input citys number: ");
                    int tag = int.Parse(Console.ReadLine());
                    if (tag < 0 || tag == CurrenctCity || tag >= cityInMap || IsSame(cityAroundName,tag)){
                        Console.WriteLine("Invalid ID pls try again" + IsSame(cityAroundName,tag));
                        InputCityTag(ref cityAroundName,cityInMap,CurrenctCity,j);
                    } else {
                        cityAroundName[j] = tag;
                    }
                    
                }

                InputCityTag(ref cityAroundName,cityInMap,i,j);
            }

            Citys[i] = new City(cityName,cityAround,cityAroundName,0);
        }

        static void doAction(ref City[] Citys){
            Console.WriteLine();
            Console.WriteLine("--------------------------------------------------------------");
            PrintCityInfo(Citys);

            Console.Write("Input Action [Outbreak, Vaccinate, Lock down, Spread, Exit]: ");
            string Action = Console.ReadLine();
            switch (Action){
                case "Outbreak":
                    OutbreakAction(ref Citys);
                    doAction(ref Citys);
                    break;
                case "Spread":
                    Citys = SpreadAction(Citys);
                    doAction(ref Citys);
                    break;
                case "Vaccinate":
                    VaccinateAction(ref Citys);
                    doAction(ref Citys);
                    break;
                case "Lock Down":
                    LockDownAction(ref Citys);
                    doAction(ref Citys);
                    break;   
                case "Exit":
                    break;   
                default:
                    Console.WriteLine("Invalid");
                    doAction(ref Citys);
                    break;
            }
        }

        doAction(ref Citys);
    }

    static bool IsSame(int[] CityinRange, int input){
        for (int i = 0; i < CityinRange.GetLength(0); i++){
            //Console.WriteLine(CityinRange[i] +  "," + input);
            if (CityinRange[i] == input){
                return true;
            }
        }
        return false;
    }

    static void PrintCityInfo(City[] Citys){
        for (int i = 0; i < Citys.GetLength(0); i++){
            Console.WriteLine("No." + i + " : " + Citys[i].name + " Covid: " + Citys[i].covidLevel);
        }
    }
   

    static int IncreaseCovidLevel(int val, int incresses){
        int sum = val + incresses;
        if (sum > 3) { return 3; }else { return sum; } 
    }

    static int ReduceCovidLevel(int val, int incresses){
        int negative = val - incresses;
        if (negative < 0) { return 0; }else { return negative; } 
    }


    static void OutbreakAction(ref City[] Citys){
        Console.Write("Input city to outbreak: ");
        int targetCity = GetPositiveNumber();
        Citys[targetCity].covidLevel = IncreaseCovidLevel(Citys[targetCity].covidLevel,2);

        for (int i = 0; i < Citys[targetCity].cityAround; i++){
            Citys[Citys[targetCity].cityAroundName[i]].covidLevel = IncreaseCovidLevel(Citys[Citys[targetCity].cityAroundName[i]].covidLevel,1);
        }
    }

    static void VaccinateAction(ref City[] Citys){
        Console.Write("Input city to vaccinate: ");
        int targetCity = GetPositiveNumber();
        Citys[targetCity].covidLevel = 0;
    }

   
    static void LockDownAction(ref City[] Citys){
        Console.Write("Input city to lock down: ");
        int targetCity = GetPositiveNumber();

        Citys[targetCity].covidLevel = ReduceCovidLevel(Citys[targetCity].covidLevel,1);
        for (int i = 0; i < Citys[targetCity].cityAround; i++){
            Citys[Citys[targetCity].cityAroundName[i]].covidLevel = ReduceCovidLevel(Citys[Citys[targetCity].cityAroundName[i]].covidLevel,1);
        }
    }

     static bool IsCovidHigher(City[] Citys, int[] CityInRange,int currentLevel){
        City[] temp = Citys;
        if (CityInRange.GetLength(0) == 0) {return false;} 

        for (int i = 0; i < CityInRange.GetLength(0); i++){
            if (temp[CityInRange[i]].covidLevel > currentLevel) {
                return true;
            }
        }
        return false;
    }
    static City[] SpreadAction(City[] citys){
        City[] newData = citys;
      

        for (int i = citys.GetLength(0) - 1; i >= 0; i--){
            if (IsCovidHigher(citys,citys[i].cityAroundName,citys[i].covidLevel)){
                newData[i].covidLevel = IncreaseCovidLevel(newData[i].covidLevel,1);
            }
        }

        

        return newData;
    }
}